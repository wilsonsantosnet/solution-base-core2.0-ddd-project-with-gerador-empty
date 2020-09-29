using Common.Gen.Utils;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;

namespace Common.Gen
{
    public class HelperSysObjectsReadOnly : HelperSysObjectsBaseBack
    {

        public HelperSysObjectsReadOnly(IEnumerable<Context> contexts, string template)
        {
            this.Contexts = contexts;
            foreach (var item in contexts)
                item.UsePathProjects = true;

            this._defineTemplateFolder = new DefineTemplateFolder();
            this._defineTemplateFolder.SetTemplatePathBase(template);
            base.ArquitetureType = ArquitetureType.ReadOnly;
        }

        public override void DefineTemplateByTableInfo(Context config, TableInfo tableInfo)
        {
            this.DefineTemplateByTableInfoBack(config, tableInfo);
            this.DefineTemplateByTableInfoFront(config, tableInfo);
        }

        public override void DefineTemplateByTableInfoFields(Context config, TableInfo tableInfo, UniqueListInfo infos)
        {
            this.DefineTemplateByTableInfoFieldsBack(config, tableInfo, infos);
            this.DefineTemplateByTableInfoFieldsFront(config, tableInfo, infos);
        }

        public override void DefineTemplateByTableInfoFieldsBack(Context config, TableInfo tableInfo, UniqueListInfo infos)
        {
            if (config.MakeBack)
            {
                this.ExecuteTemplateEntitysBase(tableInfo, config, infos);
                this.ExecuteTemplateEntitysExt(tableInfo, config, infos);

                this.ExecuteTemplateIEntityRepository(tableInfo, config, infos);
                this.ExecuteTemplateEntityRepository(tableInfo, config, infos);

                this.ExecuteTemplateFilter(tableInfo, config, infos);
                this.ExecuteTemplateFilterPartial(tableInfo, config, infos);
                this.ExecuteTemplateFilterBasicExtension(tableInfo, config, infos);
                this.ExecuteTemplateFilterCustomExtension(tableInfo, config, infos);
                this.ExecuteTemplateOrderByDomainExtension(tableInfo, config, infos);

                this.ExecuteTemplateEntityMapBase(tableInfo, config, infos);
                this.ExecuteTemplateEntityMapExtension(tableInfo, config, infos);

                this.ExecuteTemplateApi(tableInfo, config, infos);
                this.ExecuteTemplateApiMore(tableInfo, config, infos);
            }

        }

        public override void DefineTemplateByTableInfoBack(Context config, TableInfo tableInfo)
        {
            if (config.MakeBack)
            {
                this.ExecuteTemplateDbContext(tableInfo, config);
                this.ExecuteTemplateApiStart(tableInfo, config);
                this.ExecuteTemplateApiCurrentUser(tableInfo, config);
                this.ExecuteTemplateApiDownload(tableInfo, config);
                this.ExecuteTemplateApiHealth(tableInfo, config);
                this.ExecuteTemplateApiAppSettings(tableInfo, config);
                this.ExecuteTemplateContainer(tableInfo, config);
                this.ExecuteTemplateContainerPartial(tableInfo, config);
            }
        }

        public override void DefineTemplateByTableInfoFront(Context config, TableInfo tableInfo)
        {
            if (config.MakeFront)
            {
                var front = this.DefineFrontTemplateClass(config);
                front.SetCamelCasingExceptions(base._camelCasingExceptions);
                front.DefineTemplateByTableInfo(config, tableInfo);
            }
        }

        public override void DefineTemplateByTableInfoFieldsFront(Context config, TableInfo tableInfo, UniqueListInfo infos)
        {
            if (config.MakeFront)
            {
                var front = this.DefineFrontTemplateClass(config);
                front.SetCamelCasingExceptions(base._camelCasingExceptions);
                front.DefineTemplateByTableInfoFields(config, tableInfo, infos);
            }
        }

        public virtual HelperSysObjectsBase DefineFrontTemplateClass(Context config)
        {
            return new HelperSysObjectsAngular20(config);
        }

        #region Execute Templates

        private void ExecuteTemplateEntitysBase(TableInfo tableInfo, Context configContext, IEnumerable<Info> infos)
        {
            if (tableInfo.CodeCustomImplemented)
                return;

            if (tableInfo.MovedBaseClassToShared)
                return;

            if (!tableInfo.MakeDomain)
                return;

            var pathOutput = PathOutput.PathOutputDomainEntitysBase(tableInfo, configContext);
            var pathTemplateClass = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, this._defineTemplateFolder.Define(tableInfo), DefineTemplateName.EntityBase(tableInfo));
            if (!File.Exists(pathTemplateClass))
                return;

            var pathTemplatePropertys = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, this._defineTemplateFolder.Define(tableInfo), DefineTemplateName.EntityProperty(tableInfo));
            if (!File.Exists(pathTemplatePropertys))
                return;

            var textTemplateClass = Read.AllText(tableInfo, pathTemplateClass, this._defineTemplateFolder);
            var textTemplatePropertys = Read.AllText(tableInfo, pathTemplatePropertys, this._defineTemplateFolder);



            var classBuilder = textTemplateClass;
            classBuilder = classBuilder.Replace("<#classBaseEntity#>", this.InheritEntityBase(infos));
            classBuilder = classBuilder.Replace("<#parametersRequired#>", this.ParametersRequired(infos));
            classBuilder = classBuilder.Replace("<#ctorparametersRequired#>", base.ConstructorParametersRequired(infos));
            classBuilder = classBuilder.Replace("<#ctorparametersRequiredBase#>", base.ConstructorParametersRequiredBase(infos));
            classBuilder = classBuilder.Replace("<#property#>", base.BuilderPropertys(infos, textTemplatePropertys, configContext));
            classBuilder = classBuilder.Replace("<#initParametersRequired#>", this.InitParametersRequired(infos));
            classBuilder = classBuilder.Replace("<#parametersRequired#>", this.ParametersRequired(infos));
            classBuilder = classBuilder.Replace("<#methodsSeters#>", this.MethodsSeters(infos, tableInfo));
            classBuilder = base.GenericTagsTransformer(tableInfo, configContext, classBuilder);


            using (var stream = new HelperStream(pathOutput).GetInstance())
            {
                stream.Write(classBuilder);
            }

        }

        private void ExecuteTemplateEntitysExt(TableInfo tableInfo, Context configContext, IEnumerable<Info> infos)
        {
            if (tableInfo.CodeCustomImplemented)
                return;

            if (!tableInfo.MakeDomain)
                return;

            var pathOutput = PathOutput.PathOutputDomainEntitysExt(tableInfo, configContext);
            if (File.Exists(pathOutput) && tableInfo.CodeCustomImplemented)
                return;

            if (File.Exists(pathOutput) && Convert.ToBoolean(ConfigurationManager.AppSettings["GerarModelPartialExistentes"]) == false)
                return;

            var pathTemplateClass = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, this._defineTemplateFolder.Define(tableInfo), DefineTemplateName.EntityExt(tableInfo));
            if (!File.Exists(pathTemplateClass))
                return;

            var pathTemplatePropertys = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, this._defineTemplateFolder.Define(tableInfo), DefineTemplateName.EntityProperty(tableInfo));
            var textTemplateClass = Read.AllText(tableInfo, pathTemplateClass, this._defineTemplateFolder);
            var textTemplatePropertys = Read.AllText(tableInfo, pathTemplatePropertys, this._defineTemplateFolder);


            var classBuilder = textTemplateClass;
            classBuilder = classBuilder.Replace("<#classBaseEntity#>", this.InheritEntityBase(infos));
            classBuilder = classBuilder.Replace("<#ctorparametersRequired#>", this.ConstructorParametersRequired(infos));
            classBuilder = classBuilder.Replace("<#ctorparametersRequiredBase#>", this.ConstructorParametersRequiredBase(infos));
            classBuilder = classBuilder.Replace("<#parametersRequired#>", this.ParametersRequired(infos));
            classBuilder = classBuilder.Replace("<#parametersRequiredToBase#>", this.ParametersRequiredToBase(infos));
            classBuilder = classBuilder.Replace("<#property#>", base.BuilderPropertys(infos, textTemplatePropertys, configContext));
            classBuilder = classBuilder.Replace("<#initParametersRequired#>", this.InitParametersRequired(infos));
            classBuilder = classBuilder.Replace("<#methodsSeters#>", this.MethodsSeters(infos, tableInfo));
            classBuilder = classBuilder.Replace("<#parametersRequiredConstruction#>", this.ParametersRequiredConstruction(infos));
            classBuilder = classBuilder.Replace("<#methodsSetersConstruction#>", this.MethodsSetersConstruction(infos));
            classBuilder = base.GenericTagsTransformer(tableInfo, configContext, classBuilder);



            using (var stream = new HelperStream(pathOutput).GetInstance())
            {
                stream.Write(classBuilder);
            }

        }

        private void ExecuteTemplateIEntityRepository(TableInfo tableInfo, Context configContext, IEnumerable<Info> infos)
        {
            if (tableInfo.CodeCustomImplemented)
                return;

            if (!tableInfo.MakeDomain)
                return;

            var pathOutput = PathOutput.PathOutputDomainIEntitysRepository(tableInfo, configContext);
            if (File.Exists(pathOutput))
                return;

            var pathTemplateClass = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, this._defineTemplateFolder.Define(tableInfo), DefineTemplateName.IEntityRepository(tableInfo));
            if (!File.Exists(pathTemplateClass))
                return;


            var textTemplateClass = Read.AllText(tableInfo, pathTemplateClass, this._defineTemplateFolder);
            var classBuilder = base.GenericTagsTransformer(tableInfo, configContext, textTemplateClass);
            using (var stream = new HelperStream(pathOutput).GetInstance())
            {
                stream.Write(classBuilder);
            }

        }

        private void ExecuteTemplateEntityRepository(TableInfo tableInfo, Context configContext, IEnumerable<Info> infos)
        {
            if (tableInfo.CodeCustomImplemented)
                return;

            if (!tableInfo.MakeDomain)
                return;


            var pathOutput = PathOutput.PathOutputInfraEntitysRepository(tableInfo, configContext);
            if (File.Exists(pathOutput))
                return;

            var pathTemplateClass = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, this._defineTemplateFolder.Define(tableInfo), DefineTemplateName.EntityRepository(tableInfo));
            if (!File.Exists(pathTemplateClass))
                return;


            var textTemplateClass = Read.AllText(tableInfo, pathTemplateClass, this._defineTemplateFolder);
            var classBuilder = base.GenericTagsTransformer(tableInfo, configContext, textTemplateClass);
            classBuilder = classBuilder.Replace("<#fieldGetByFilters#>", classBuilder);
            using (var stream = new HelperStream(pathOutput).GetInstance())
            {
                stream.Write(classBuilder);
            }

        }

        private void ExecuteTemplateFilterBasicExtension(TableInfo tableInfo, Context configContext, IEnumerable<Info> infos)
        {
            if (tableInfo.CodeCustomImplemented)
                return;

            if (!tableInfo.MakeDomain)
                return;


            var pathOutput = PathOutput.PathOutputInfraFilterBasicExtension(tableInfo, configContext);
            var pathTemplateClass = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, this._defineTemplateFolder.Define(tableInfo), DefineTemplateName.EntityFilterBasicExtension(tableInfo));


            if (!File.Exists(pathTemplateClass))
                return;

            var pathTemplateFilters = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, this._defineTemplateFolder.Define(tableInfo), DefineTemplateName.FilterBasicExtension(tableInfo));
            var textTemplateFilters = Read.AllText(tableInfo, pathTemplateFilters, this._defineTemplateFolder);
            var textTemplateClass = Read.AllText(tableInfo, pathTemplateClass, this._defineTemplateFolder);

            var classBuilder = base.GenericTagsTransformer(tableInfo, configContext, textTemplateClass);
            var classBuilderFilters = string.Empty;

            classBuilderFilters = base.BuilderSampleFilters(infos, textTemplateFilters, classBuilderFilters, configContext);

            classBuilder = classBuilder.Replace("<#filtersExpressions#>", classBuilderFilters);

            using (var stream = new HelperStream(pathOutput).GetInstance())
            {
                stream.Write(classBuilder);
            }

        }

        private void ExecuteTemplateFilterCustomExtension(TableInfo tableInfo, Context configContext, IEnumerable<Info> infos)
        {
            if (tableInfo.CodeCustomImplemented)
                return;

            if (!tableInfo.MakeDomain)
                return;

            var pathOutput = PathOutput.PathOutputInfraFilterCustomExtension(tableInfo, configContext);
            if (File.Exists(pathOutput))
                return;

            var pathTemplateClass = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, this._defineTemplateFolder.Define(tableInfo), DefineTemplateName.EntityFilterCustomExtension(tableInfo));
            if (!File.Exists(pathTemplateClass))
                return;

            var textTemplateClass = Read.AllText(tableInfo, pathTemplateClass, this._defineTemplateFolder);
            var classBuilder = base.GenericTagsTransformer(tableInfo, configContext, textTemplateClass);

            using (var stream = new HelperStream(pathOutput).GetInstance())
            {
                stream.Write(classBuilder);
            }

        }

        private void ExecuteTemplateOrderByDomainExtension(TableInfo tableInfo, Context configContext, IEnumerable<Info> infos)
        {
            if (tableInfo.CodeCustomImplemented)
                return;

            if (!tableInfo.MakeDomain)
                return;


            var pathOutput = PathOutput.PathOutputInfraOrderByDomainExtension(tableInfo, configContext);
            if (File.Exists(pathOutput))
                return;

            var pathTemplateClass = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, this._defineTemplateFolder.Define(tableInfo), DefineTemplateName.EntityOrderByDomainExtension(tableInfo));
            if (!File.Exists(pathTemplateClass))
                return;

            var textTemplateClass = Read.AllText(tableInfo, pathTemplateClass, this._defineTemplateFolder);
            var classBuilder = base.GenericTagsTransformer(tableInfo, configContext, textTemplateClass);

            using (var stream = new HelperStream(pathOutput).GetInstance())
            {
                stream.Write(classBuilder);
            }

        }

        private void ExecuteTemplateEntityMapBase(TableInfo tableInfo, Context configContext, IEnumerable<Info> infos)
        {
            if (!tableInfo.MakeDomain)
                return;

            if (!tableInfo.Scaffold)
                return;

            var pathOutput = PathOutput.PathOutputEntityMapBase(tableInfo, configContext);
            var pathTemplateClass = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, this._defineTemplateFolder.Define(tableInfo), DefineTemplateName.EntityMapBase(tableInfo));
            if (!File.Exists(pathTemplateClass))
                return;

            var pathTemplateLength = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, this._defineTemplateFolder.Define(tableInfo), DefineTemplateName.MapsLength(tableInfo));
            var pathTemplateRequired = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, this._defineTemplateFolder.Define(tableInfo), DefineTemplateName.MapsRequired(tableInfo));
            var pathTemplateMapper = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, this._defineTemplateFolder.Define(tableInfo), DefineTemplateName.MapsMapper(tableInfo));
            var pathTemplateManyToMany = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, this._defineTemplateFolder.Define(tableInfo), DefineTemplateName.MapsManyToMany(tableInfo));
            var pathTemplateCompositeKey = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, this._defineTemplateFolder.Define(tableInfo), DefineTemplateName.MapsCompositeKey(tableInfo));

            var textTemplateClass = Read.AllText(tableInfo, pathTemplateClass, this._defineTemplateFolder);
            var textTemplateLength = Read.AllText(tableInfo, pathTemplateLength, this._defineTemplateFolder);
            var textTemplateRequired = Read.AllText(tableInfo, pathTemplateRequired, this._defineTemplateFolder);
            var textTemplateMapper = Read.AllText(tableInfo, pathTemplateMapper, this._defineTemplateFolder);
            var textTemplateManyToMany = Read.AllText(tableInfo, pathTemplateManyToMany, this._defineTemplateFolder);
            var textTemplateCompositeKey = Read.AllText(tableInfo, pathTemplateCompositeKey, this._defineTemplateFolder);

            var classBuilderitemTemplateLength = string.Empty;
            var classBuilderitemTemplateRequired = string.Empty;
            var classBuilderitemplateMapper = string.Empty;
            var classBuilderitemplateMapperKey = string.Empty;
            var classBuilderitemplateManyToMany = string.Empty;
            var classBuilderitemplateCompositeKey = string.Empty;

            string classBuilder = base.MakeClassBuilderMapORM(tableInfo, configContext, infos, textTemplateClass, textTemplateLength, textTemplateRequired, textTemplateMapper, textTemplateManyToMany, textTemplateCompositeKey, ref classBuilderitemTemplateLength, ref classBuilderitemTemplateRequired, ref classBuilderitemplateMapper, ref classBuilderitemplateMapperKey, ref classBuilderitemplateManyToMany, ref classBuilderitemplateCompositeKey);

            using (var stream = new HelperStream(pathOutput).GetInstance())
            {
                stream.Write(classBuilder);
            }


        }

        private void ExecuteTemplateEntityMapExtension(TableInfo tableInfo, Context configContext, IEnumerable<Info> infos)
        {
            var pathOutput = PathOutput.PathOutputEntityMapExtension(tableInfo, configContext);

            if (!tableInfo.MakeDomain)
                return;

            if (!tableInfo.Scaffold)
                return;

            if (File.Exists(pathOutput) && tableInfo.CodeCustomImplemented)
                return;

            if (File.Exists(pathOutput) && Convert.ToBoolean(ConfigurationManager.AppSettings["GerarMapperPartialExistentes"]) == false)
                return;

            var pathTemplateClass = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, this._defineTemplateFolder.Define(tableInfo), DefineTemplateName.EntityMapExtension(tableInfo));
            var textTemplateClass = Read.AllText(tableInfo, pathTemplateClass, this._defineTemplateFolder);

            var classBuilder = GenericTagsTransformer(tableInfo, configContext, textTemplateClass);


            using (var stream = new HelperStream(pathOutput).GetInstance())
            {
                stream.Write(classBuilder);
            }

        }

        private void ExecuteTemplateDbContext(TableInfo tableInfo, Context configContext)
        {
            if (!tableInfo.MakeDomain)
                return;

            var pathOutput = PathOutput.PathOutputDbContext(configContext);
            var pathTemplateClass = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, this._defineTemplateFolder.Define(tableInfo), DefineTemplateName.Context(tableInfo));
            if (!File.Exists(pathTemplateClass))
                return;

            var pathTemplateRegister = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, this._defineTemplateFolder.Define(tableInfo), DefineTemplateName.ContextMappers(tableInfo));


            var textTemplateClass = Read.AllText(tableInfo, pathTemplateClass, this._defineTemplateFolder);
            var TextTemplateMappers = Read.AllText(tableInfo, pathTemplateRegister, this._defineTemplateFolder);
            textTemplateClass = textTemplateClass.Replace("<#module#>", configContext.ContextName);

            var classBuilder = GenericTagsTransformer(tableInfo, configContext, textTemplateClass);


            var classBuilderMappers = string.Empty;


            foreach (var item in configContext.TableInfo.Where(_ => _.Scaffold))
            {

                var itemMappaer = TextTemplateMappers.
                        Replace("<#className#>", item.ClassName);

                classBuilderMappers += string.Format("{0}{1}{2}", Tabs.TabMaps(), itemMappaer, System.Environment.NewLine);

            }

            classBuilder = classBuilder.Replace("<#mappers#>", classBuilderMappers);


            using (var stream = new HelperStream(pathOutput).GetInstance())
            {
                stream.Write(classBuilder);
            }

        }

        private void ExecuteTemplateFilter(TableInfo tableInfo, Context configContext, IEnumerable<Info> infos)
        {
            if (tableInfo.MovedBaseClassToShared)
                return;

            var pathOutput = PathOutput.PathOutputFilterWithFolder(tableInfo, configContext);
            var pathTemplateClass = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, this._defineTemplateFolder.Define(tableInfo), DefineTemplateName.Filter(tableInfo));
            var pathTemplatePropertys = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, this._defineTemplateFolder.Define(tableInfo), DefineTemplateName.ModelsProperty(tableInfo));

            var textTemplateClass = Read.AllText(tableInfo, pathTemplateClass, this._defineTemplateFolder);
            var TextTemplatePropertys = Read.AllText(tableInfo, pathTemplatePropertys, this._defineTemplateFolder);
            if (!File.Exists(pathTemplateClass))
                return;

            var classBuilder = GenericTagsTransformer(tableInfo, configContext, textTemplateClass);

            var classBuilderPropertys = string.Empty;

            if (infos.IsAny())
            {

                foreach (var item in infos)
                {
                    classBuilderPropertys = MakeFilterDateRange(TextTemplatePropertys, classBuilderPropertys, item);

                    if (item.Type == "bool")
                        classBuilderPropertys = AddPropertyFilter(TextTemplatePropertys, classBuilderPropertys, item, item.PropertyName, "bool?");
                    else
                        classBuilderPropertys = AddPropertyFilter(TextTemplatePropertys, classBuilderPropertys, item, item.PropertyName, item.Type);

                }
            }

            classBuilder = classBuilder.Replace("<#property#>", classBuilderPropertys);

            using (var stream = new HelperStream(pathOutput).GetInstance())
            {

                stream.Write(classBuilder);
            }

        }

        private void ExecuteTemplateFilterPartial(TableInfo tableInfo, Context configContext, IEnumerable<Info> infos)
        {
            var pathOutput = PathOutput.PathOutputFilterPartialWithFolder(tableInfo, configContext);

            if (File.Exists(pathOutput) && Convert.ToBoolean(ConfigurationManager.AppSettings["GerarFiltersPartialExistentes"]) == false)
                return;

            var pathTemplateClass = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, this._defineTemplateFolder.Define(tableInfo), DefineTemplateName.FiltersPartial(tableInfo));
            if (!File.Exists(pathTemplateClass))
                return;

            var textTemplateClass = Read.AllText(tableInfo, pathTemplateClass, this._defineTemplateFolder);
            var classBuilder = GenericTagsTransformer(tableInfo, configContext, textTemplateClass);


            using (var stream = new HelperStream(pathOutput).GetInstance())
            {

                stream.Write(classBuilder);
            }

        }

        private string CustomMethods(TableInfo tableInfo, Context configContext, string classBuilder, string pathTemplateCustoMethod)
        {
            var customMethods = string.Empty;
            if (tableInfo.MethodConfig.IsAny())
            {
                var textTemplate = Read.AllText(tableInfo, pathTemplateCustoMethod, this._defineTemplateFolder);
                foreach (var item in tableInfo.MethodConfig)
                    customMethods += this.TransformerCustomMethod(tableInfo, configContext, textTemplate, item);
            }

            return classBuilder.Replace("<#customMethods#>", customMethods);
        }

        private void ExecuteTemplateContainer(TableInfo tableInfo, Context configContext)
        {
            if (!tableInfo.MakeApi)
                return;

            var pathOutput = PathOutput.PathOutputContainerApi(configContext);

            var pathTemplateClass = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, this._defineTemplateFolder.Define(tableInfo), DefineTemplateName.Container(tableInfo));
            if (!File.Exists(pathTemplateClass))
                return;

            var pathTemplateInjections = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, this._defineTemplateFolder.Define(tableInfo), DefineTemplateName.ContainerInjections(tableInfo));


            var textTemplateClass = Read.AllText(tableInfo, pathTemplateClass, this._defineTemplateFolder);
            var TextTemplateInjections = Read.AllText(tableInfo, pathTemplateInjections, this._defineTemplateFolder);

            if (configContext.Module.IsNullOrEmpty())
                textTemplateClass = textTemplateClass.Replace("<#domainSource#>", configContext.ProjectName);
            var classBuilder = GenericTagsTransformer(tableInfo, configContext, textTemplateClass);

            var classBuilderMappers = string.Empty;

            foreach (var item in configContext.TableInfo)
            {
                if (!string.IsNullOrEmpty(item.ClassName))
                {
                    var itemInjections = TextTemplateInjections.
                            Replace("<#namespace#>", configContext.Namespace).
                            Replace("<#module#>", configContext.Module.IsNullOrEmpty() ? configContext.ProjectName : configContext.Module).
                            Replace("<#className#>", item.ClassName).
                            Replace("<#domainSource#>", configContext.DomainSource.IsNullOrEmpty() ? configContext.ProjectName : configContext.DomainSource).
                            Replace("<#namespaceDomainSource#>", configContext.NamespaceDomainSource);


                    classBuilderMappers += string.Format("{0}{1}", itemInjections, System.Environment.NewLine);
                }
            }


            classBuilder = classBuilder.Replace("<#injections#>", classBuilderMappers);


            using (var stream = new HelperStream(pathOutput).GetInstance())
            {
                stream.Write(classBuilder);
            }

        }

        private void ExecuteTemplateContainerPartial(TableInfo tableInfo, Context configContext)
        {
            if (!tableInfo.MakeApi)
                return;

            var pathOutput = PathOutput.PathOutputContainerPartialApi(configContext);
            if (File.Exists(pathOutput) && tableInfo.CodeCustomImplemented)
                return;

            if (File.Exists(pathOutput) && Convert.ToBoolean(ConfigurationManager.AppSettings["GerarContainerClassPartialExistentes"]) == false)
                return;

            var pathTemplateClass = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, this._defineTemplateFolder.Define(tableInfo), DefineTemplateName.ContainerPartial(tableInfo));
            if (!File.Exists(pathTemplateClass))
                return;

            var textTemplateClass = Read.AllText(tableInfo, pathTemplateClass, this._defineTemplateFolder);

            var classBuilder = GenericTagsTransformer(tableInfo, configContext, textTemplateClass);
            using (var stream = new HelperStream(pathOutput).GetInstance())
            {
                stream.Write(classBuilder);
            }

        }

        private void ExecuteTemplateApi(TableInfo tableInfo, Context configContext, IEnumerable<Info> infos)
        {

            var pathOutput = PathOutput.PathOutputApi(tableInfo, configContext);
            if (!tableInfo.MakeApi)
                return;

            if (File.Exists(pathOutput) && tableInfo.CodeCustomImplemented)
                return;

            if (File.Exists(pathOutput) && Convert.ToBoolean(ConfigurationManager.AppSettings["GerarApiExistentes"]) == false)
                return;


            var pathTemplateClass = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, this._defineTemplateFolder.Define(tableInfo), DefineTemplateName.ApiBase(tableInfo));
            var pathTemplateApiGet = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, this._defineTemplateFolder.Define(tableInfo), DefineTemplateName.ApiGet(tableInfo));
            if (!File.Exists(pathTemplateClass))
                return;

            var textTemplateClass = Read.AllText(tableInfo, pathTemplateClass, this._defineTemplateFolder);
            var TextTemplateApiGet = Read.AllText(tableInfo, pathTemplateApiGet, this._defineTemplateFolder);

            var classBuilder = GenericTagsTransformer(tableInfo, configContext, textTemplateClass);

            var classBuilderApiGet = string.Empty;
            if (!tableInfo.IsCompositeKey && tableInfo.Keys.IsAny())
            {
                classBuilderApiGet = TextTemplateApiGet;
                classBuilderApiGet = classBuilderApiGet.Replace("<#className#>", tableInfo.ClassName);
                classBuilderApiGet = classBuilderApiGet.Replace("<#namespace#>", configContext.Namespace);
                classBuilderApiGet = classBuilderApiGet.Replace("<#inheritClassName#>", tableInfo.InheritClassName);
                classBuilderApiGet = classBuilderApiGet.Replace("<#KeyName#>", tableInfo.Keys.FirstOrDefault());
                classBuilderApiGet = classBuilderApiGet.Replace("<#KeyType#>", tableInfo.KeysTypes.FirstOrDefault());
            }
            classBuilder = classBuilder.Replace("<#ApiGet#>", classBuilderApiGet);

            var pathTemplateCustoMethod = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, this._defineTemplateFolder.Define(tableInfo), DefineTemplateName.ApiCustomMethod(tableInfo));
            classBuilder = this.CustomMethods(tableInfo, configContext, classBuilder, pathTemplateCustoMethod);

            if (!tableInfo.Authorize)
                classBuilder = classBuilder.Replace("[Authorize]", string.Empty);


            using (var stream = new HelperStream(pathOutput).GetInstance())
            {
                stream.Write(classBuilder);
            }

        }

        private string TransformerCustomMethod(TableInfo tableInfo, Context configContext, string customMethods, MethodConfig item)
        {
            customMethods = customMethods.Replace("<#customMethodsVerb#>", item.Verb);
            customMethods = customMethods.Replace("<#customMethodsRoute#>", item.Route);
            customMethods = customMethods.Replace("<#customMethodsSignatureControllerTemplate#>", item.SignatureControllerTemplate);
            customMethods = customMethods.Replace("<#customMethodsSignatureAppTemplate#>", item.SignatureAppTemplate);
            customMethods = customMethods.Replace("<#customMethodsDto#>", item.Dto);
            customMethods = customMethods.Replace("<#customMethodsName#>", item.CallTemplate);
            customMethods = customMethods.Replace("<#customMethodsparameterReturn#>", item.ParameterReturn);

            customMethods = customMethods.Replace("<#className#>", tableInfo.ClassName);
            customMethods = customMethods.Replace("<#namespace#>", configContext.Namespace);

            customMethods += System.Environment.NewLine;
            customMethods += System.Environment.NewLine;

            return customMethods;
        }

        private void ExecuteTemplateApiMore(TableInfo tableInfo, Context configContext, IEnumerable<Info> infos)
        {

            var pathOutput = PathOutput.PathOutputApiMore(tableInfo, configContext);
            if (!tableInfo.MakeApi)
                return;

            if (File.Exists(pathOutput) && tableInfo.CodeCustomImplemented)
                return;

            if (File.Exists(pathOutput) && Convert.ToBoolean(ConfigurationManager.AppSettings["GerarApiExistentes"]) == false)
                return;


            var pathTemplateClass = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, this._defineTemplateFolder.Define(tableInfo), DefineTemplateName.ApiMore(tableInfo));
            if (!File.Exists(pathTemplateClass))
                return;

            var textTemplateClass = Read.AllText(tableInfo, pathTemplateClass, this._defineTemplateFolder);

            var classBuilder = GenericTagsTransformer(tableInfo, configContext, textTemplateClass);
            if (!tableInfo.Authorize)
                classBuilder = classBuilder.Replace("[Authorize]", string.Empty);

            using (var stream = new HelperStream(pathOutput).GetInstance())
            {
                stream.Write(classBuilder);
            }

        }

        private void ExecuteTemplateApiAppSettings(TableInfo tableInfo, Context configContext)
        {
            if (!tableInfo.MakeApi)
                return;

            var pathOutput = PathOutput.PathOutputAppSettings(configContext);
            if (File.Exists(pathOutput))
                return;

            var pathTemplateClass = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, this._defineTemplateFolder.Define(tableInfo), DefineTemplateName.Appsettings(tableInfo));
            if (!File.Exists(pathTemplateClass))
                return;

            var textTemplateClass = Read.AllText(tableInfo, pathTemplateClass, this._defineTemplateFolder);
            var classBuilder = GenericTagsTransformer(tableInfo, configContext, textTemplateClass);

            using (var stream = new HelperStream(pathOutput).GetInstance())
            {
                stream.Write(classBuilder);
            }

        }

        private void ExecuteTemplateApiHealth(TableInfo tableInfo, Context configContext)
        {
            if (!tableInfo.MakeApi)
                return;

            var pathOutput = PathOutput.PathOutputApiHeath(configContext, tableInfo);
            if (File.Exists(pathOutput))
                return;

            var pathTemplateClass = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, this._defineTemplateFolder.Define(tableInfo), DefineTemplateName.ApiHealth(tableInfo));
            if (!File.Exists(pathTemplateClass))
                return;

            var textTemplateClass = Read.AllText(tableInfo, pathTemplateClass, this._defineTemplateFolder);
            var classBuilder = GenericTagsTransformer(tableInfo, configContext, textTemplateClass);

            using (var stream = new HelperStream(pathOutput).GetInstance())
            {
                stream.Write(classBuilder);
            }

        }

        private void ExecuteTemplateApiDownload(TableInfo tableInfo, Context configContext)
        {
            if (!tableInfo.MakeApi)
                return;

            var pathOutput = PathOutput.PathOutputApiDownload(configContext, tableInfo);
            if (File.Exists(pathOutput))
                return;

            var pathTemplateClass = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, this._defineTemplateFolder.Define(tableInfo), DefineTemplateName.ApiDownalod(tableInfo));
            if (!File.Exists(pathTemplateClass))
                return;

            var textTemplateClass = Read.AllText(tableInfo, pathTemplateClass, this._defineTemplateFolder);
            var classBuilder = GenericTagsTransformer(tableInfo, configContext, textTemplateClass);

            using (var stream = new HelperStream(pathOutput).GetInstance())
            {
                stream.Write(classBuilder);
            }

        }

        private void ExecuteTemplateApiCurrentUser(TableInfo tableInfo, Context configContext)
        {
            if (!tableInfo.MakeApi)
                return;

            var pathOutput = PathOutput.PathOutputApiCurrentUser(configContext, tableInfo);
            if (File.Exists(pathOutput))
                return;

            var pathTemplateClass = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, this._defineTemplateFolder.Define(tableInfo), DefineTemplateName.ApiCurrentUser(tableInfo));
            if (!File.Exists(pathTemplateClass))
                return;

            var textTemplateClass = Read.AllText(tableInfo, pathTemplateClass, this._defineTemplateFolder);
            var classBuilder = GenericTagsTransformer(tableInfo, configContext, textTemplateClass);

            using (var stream = new HelperStream(pathOutput).GetInstance())
            {
                stream.Write(classBuilder);
            }

        }

        private void ExecuteTemplateApiStart(TableInfo tableInfo, Context configContext)
        {
            if (!tableInfo.MakeApi)
                return;

            var pathOutput = PathOutput.PathOutputWebApiStart(configContext);
            if (File.Exists(pathOutput))
                return;

            var pathTemplateClass = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, this._defineTemplateFolder.Define(tableInfo), DefineTemplateName.ApiStart(tableInfo));
            if (!File.Exists(pathTemplateClass))
                return;

            var textTemplateClass = Read.AllText(tableInfo, pathTemplateClass, this._defineTemplateFolder);
            var classBuilder = GenericTagsTransformer(tableInfo, configContext, textTemplateClass);

            using (var stream = new HelperStream(pathOutput).GetInstance())
            {
                stream.Write(classBuilder);
            }

        }

        #endregion

        #region helpers
        private string InheritEntityBase(IEnumerable<Info> infos)
        {
            return Audit.ExistsAuditFieldsDefault(infos) ? "DomainBaseWithUserCreate" : "DomainBase";
        }

        private string ParametersRequired(IEnumerable<Info> infos)
        {
            var _parametersRequired = string.Empty;
            foreach (var item in infos)
            {
                if (Audit.IsAuditField(item.PropertyName))
                    continue;

                if (IsRequired(item))
                    _parametersRequired += string.Format("{0} {1}, ", item.Type, item.PropertyName.ToLower());
            }

            return !_parametersRequired.IsNullOrEmpaty() ? _parametersRequired.Substring(0, _parametersRequired.Length - 2) : _parametersRequired;
        }

        private string MethodsSetersConstruction(IEnumerable<Info> infos)
        {
            var _methods = string.Empty;
            foreach (var item in infos)
            {
                if (Audit.IsAuditField(item.PropertyName))
                    continue;

                if (!IsRequired(item))
                    _methods += string.Format("{0}construction.Setar{1}(data.{1});{2}", Tabs.TabModelsPlus(), item.PropertyName, System.Environment.NewLine);
            }

            return _methods;
        }

        private string ParametersRequiredToBase(IEnumerable<Info> infos)
        {
            var _parametersRequired = string.Empty;
            foreach (var item in infos)
            {
                if (Audit.IsAuditField(item.PropertyName))
                    continue;

                if (IsRequired(item))
                    _parametersRequired += string.Format("{0}, ", item.PropertyName.ToLower());
            }

            return !_parametersRequired.IsNullOrEmpaty() ? _parametersRequired.Substring(0, _parametersRequired.Length - 2) : _parametersRequired;
        }

        private string InitParametersRequired(IEnumerable<Info> infos)
        {
            var _parametersRequired = string.Empty;
            foreach (var item in infos)
            {
                if (Audit.IsAuditField(item.PropertyName))
                    continue;

                if (IsRequired(item))
                    _parametersRequired += string.Format("{0}this.{1} = {2};{3}", Tabs.TabSets(), item.PropertyName, item.PropertyName.ToLower(), System.Environment.NewLine);
            }

            return _parametersRequired;
        }

        private string ParametersRequiredConstruction(IEnumerable<Info> infos)
        {
            var _parametersRequired = string.Empty;
            foreach (var item in infos)
            {
                if (Audit.IsAuditField(item.PropertyName))
                    continue;

                if (IsRequired(item))
                    _parametersRequired += string.Format("data.{0},{1}                                        ", item.PropertyName, System.Environment.NewLine);
            }

            return !_parametersRequired.IsNullOrEmpaty() ? _parametersRequired.Substring(0, _parametersRequired.Length - 43) : _parametersRequired;
        }

        private string MethodsSeters(IEnumerable<Info> infos, TableInfo tableInfo)
        {

            var pathTemplateMethosSeters = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, this._defineTemplateFolder.Define(tableInfo), DefineTemplateName.EntityMethodSeters(tableInfo));
            var textTemplateMethosSeters = Read.AllText(tableInfo, pathTemplateMethosSeters, this._defineTemplateFolder);

            var _methods = string.Empty;
            foreach (var item in infos)
            {
                if (Audit.IsAuditField(item.PropertyName))
                    continue;

                if (!IsRequired(item))
                    _methods += string.Format("{0}{1}", textTemplateMethosSeters.Replace("<#propertyName#>", item.PropertyName).Replace("<#propertyNameLower#>", item.PropertyName.ToLower()).Replace("<#type#>", item.Type), System.Environment.NewLine);
            }

            return _methods;
        }

        public override string TransformFieldString(ConfigExecutetemplate configExecutetemplate, Info info, string propertyName, string textTemplate)
        {
            throw new NotImplementedException();
        }

        public override string TransformFieldDateTime(ConfigExecutetemplate configExecutetemplate, Info info, string propertyName, string textTemplate, bool onlyDate = false)
        {
            throw new NotImplementedException();
        }

        public override string TransformFieldBool(ConfigExecutetemplate configExecutetemplate, Info info, string propertyName, string textTemplate)
        {
            throw new NotImplementedException();
        }

        public override string TransformFieldPropertyNavigation(ConfigExecutetemplate configExecutetemplate, Info info, string propertyName, string textTemplate)
        {
            throw new NotImplementedException();
        }

        public override string TransformFieldHtml(ConfigExecutetemplate configExecutetemplate, Info info, string propertyName, string textTemplate)
        {
            throw new NotImplementedException();
        }

        public override string TransformFieldUpload(ConfigExecutetemplate configExecutetemplate, Info info, string propertyName, string textTemplate)
        {
            throw new NotImplementedException();
        }

        public override string TransformFieldTextStyle(ConfigExecutetemplate configExecutetemplate, Info info, string propertyName, string textTemplate)
        {
            throw new NotImplementedException();
        }

        public override string TransformFieldTextEditor(ConfigExecutetemplate configExecutetemplate, Info info, string propertyName, string textTemplate)
        {
            throw new NotImplementedException();
        }

        public override string TransformFieldTextTag(ConfigExecutetemplate configExecutetemplate, Info info, string propertyName, string textTemplate)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
