using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
using Common.Gen.Utils;

namespace Common.Gen
{

    public class HelperSysObjectsTableModel : HelperSysObjectsBaseBack
    {
        public HelperSysObjectsTableModel(Context context, string templatePathBase)
        {
            var _contexts = new List<Context> {
                context
            };

            this.Contexts = _contexts;
            context.UsePathProjects = true;
            this._defineTemplateFolder = new DefineTemplateFolder();
            this._defineTemplateFolder.SetTemplatePathBase(templatePathBase);
            base.ArquitetureType = ArquitetureType.TableModel;

        }
        public HelperSysObjectsTableModel(Context context) : this(context, "Templates\\Back") { }
        public HelperSysObjectsTableModel(IEnumerable<Context> contexts) : this(contexts, "Templates\\Back") { }
        public HelperSysObjectsTableModel(IEnumerable<Context> contexts, string template)
        {
            this.Contexts = contexts;
            foreach (var item in contexts)
                item.UsePathProjects = true;

            this._defineTemplateFolder = new DefineTemplateFolder();
            this._defineTemplateFolder.SetTemplatePathBase(template);

           
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

        public override void DefineTemplateByTableInfoFieldsFront(Context config, TableInfo tableInfo, UniqueListInfo infos)
        {
            if (config.MakeFront)
            {
                var front = this.DefineFrontTemplateClass(config);
                front.SetCamelCasingExceptions(base._camelCasingExceptions);
                front.DefineTemplateByTableInfoFields(config, tableInfo, infos);
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

        public override void DefineTemplateByTableInfoFieldsBack(Context config, TableInfo tableInfo, UniqueListInfo infos)
        {
            this.ExecuteTemplateModels(tableInfo, config, infos);
            this.ExecuteTemplateModelsPartial(tableInfo, config, infos);
            this.ExecuteTemplateSimpleFilters(tableInfo, config, infos);
            this.ExecuteTemplateModelsValiadation(tableInfo, config, infos);
            this.ExecuteTemplateModelsValiadationPartial(tableInfo, config, infos);
            this.ExecuteTemplateModelsCustom(tableInfo, config, infos);
            this.ExecuteTemplateFilter(tableInfo, config, infos);
            this.ExecuteTemplateFilterPartial(tableInfo, config, infos);
            this.ExecuteTemplateMaps(tableInfo, config, infos);
            this.ExecuteTemplateMapsPartial(tableInfo, config, infos);
            this.ExecuteTemplateDbContextInherit(tableInfo, config);
            this.ExecuteTemplateApp(tableInfo, config, infos);
            this.ExecuteTemplateAppPartial(tableInfo, config, infos);
            this.ExecuteTemplateDto(tableInfo, config, infos);
            this.ExecuteTemplateDtoSpecialized(tableInfo, config, infos);
            this.ExecuteTemplateDtoSpecializedResult(tableInfo, config, infos);
            this.ExecuteTemplateDtoSpecializedReport(tableInfo, config, infos);
            this.ExecuteTemplateDtoSpecializedDetails(tableInfo, config, infos);
            this.ExecuteTemplateApi(tableInfo, config, infos);
            this.ExecuteTemplateSummary(tableInfo, config);
        }

        public override void DefineTemplateByTableInfoBack(Context config, TableInfo tableInfo)
        {
            ExecuteTemplateCustomFilters(tableInfo, config);
            ExecuteConfigDomain(tableInfo, config);
            ExecuteHelperValidationAuth(tableInfo, config);
            ExecuteTemplateDbContext(tableInfo, config);
            ExecuteTemplateApiConfig(tableInfo, config);
            ExecuteTemplateAutoMapperProfile(tableInfo, config);
            ExecuteTemplateAutoMapperProfileCustom(tableInfo, config);
            ExecuteTemplateContainer(tableInfo, config);
            ExecuteTemplateContainerPartial(tableInfo, config);
            ExecuteTemplateAutoMapper(tableInfo, config);
            ExecuteTemplateUri(tableInfo, config);
            ExecuteTemplateDbContextGenerateViews(tableInfo, config);

            ExecuteTemplateApiTransaction(tableInfo, config);
            ExecuteTemplateRepositoryTransaction(tableInfo, config);
            ExecuteTemplateContainerWeb(tableInfo, config);
            ExecuteTemplateContainerPartialWeb(tableInfo, config);

        }


        public virtual HelperSysObjectsBase DefineFrontTemplateClass(Context config)
        {
            return new HelperSysObjectsAngular20(config);
        }


        #region Execute Templates
        private void ExecuteTemplateApp(TableInfo tableInfo, Context configContext, IEnumerable<Info> infos)
        {
            var pathOutput = PathOutput.PathOutputApp(tableInfo, configContext);
            if (File.Exists(pathOutput) && tableInfo.CodeCustomImplemented)
                return;

            if (!tableInfo.MakeApp)
                return;

            var pathTemplateClass = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, this._defineTemplateFolder.Define(tableInfo), DefineTemplateName.App(tableInfo));
            if (!File.Exists(pathTemplateClass))
                return;

            var TextTemplateAppClass = Read.AllText(tableInfo, pathTemplateClass, this._defineTemplateFolder);
            var classBuilder = GenericTagsTransformer(tableInfo, configContext, TextTemplateAppClass);

            using (var stream = new HelperStream(pathOutput).GetInstance())
            {
                stream.Write(classBuilder);
            }

        }
        private void ExecuteTemplateFilter(TableInfo tableInfo, Context configContext, IEnumerable<Info> infos)
        {
            if (tableInfo.InheritQuery)
                return;

            var pathOutput = PathOutput.PathOutputFilter(tableInfo, configContext);
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
            var pathOutput = PathOutput.PathOutputFilterPartial(tableInfo, configContext);

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
        private void ExecuteTemplateAppPartial(TableInfo tableInfo, Context configContext, IEnumerable<Info> infos)
        {
            var pathOutput = PathOutput.PathOutputAppPartial(tableInfo, configContext);

            if (!tableInfo.MakeApp)
                return;

            if (File.Exists(pathOutput) && Convert.ToBoolean(ConfigurationManager.AppSettings["GerarAppPartialExistentes"]) == false)
                return;

            var pathTemplateClass = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, this._defineTemplateFolder.Define(tableInfo), DefineTemplateName.AppPartial(tableInfo));
            if (!File.Exists(pathTemplateClass))
                return;


            var TextTemplateAppClass = Read.AllText(tableInfo, pathTemplateClass, this._defineTemplateFolder);
            var classBuilder = GenericTagsTransformer(tableInfo, configContext, TextTemplateAppClass);


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


            var pathTemplateClass = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, this._defineTemplateFolder.Define(tableInfo), DefineTemplateName.ApiCrud(tableInfo));
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
                classBuilderApiGet = classBuilderApiGet.Replace("<#inheritClassName#>", tableInfo.InheritClassName);
                classBuilderApiGet = classBuilderApiGet.Replace("<#KeyName#>", tableInfo.Keys.FirstOrDefault());
                classBuilderApiGet = classBuilderApiGet.Replace("<#KeyType#>", tableInfo.KeysTypes.FirstOrDefault());
            }
            classBuilder = classBuilder.Replace("<#ApiGet#>", classBuilderApiGet);

            using (var stream = new HelperStream(pathOutput).GetInstance())
            {
                stream.Write(classBuilder);
            }

        }
        private void ExecuteTemplateDto(TableInfo tableInfo, Context configContext, IEnumerable<Info> infos)
        {
            if (!tableInfo.MakeDto)
                return;

            var pathOutput = PathOutput.PathOutputDto(tableInfo, configContext);
            if ((File.Exists(pathOutput) && tableInfo.CodeCustomImplemented) || (File.Exists(pathOutput) && tableInfo.InheritQuery))
                return;

            var pathTemplateClass = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, this._defineTemplateFolder.Define(tableInfo), DefineTemplateName.Dto(tableInfo));
            if (!File.Exists(pathTemplateClass))
                return;

            var pathTemplatePropertys = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, this._defineTemplateFolder.Define(tableInfo), DefineTemplateName.ModelsProperty(tableInfo));
            var textTemplateClass = Read.AllText(tableInfo, pathTemplateClass, this._defineTemplateFolder);
            var TextTemplatePropertys = Read.AllText(tableInfo, pathTemplatePropertys, this._defineTemplateFolder);


            var classBuilder = GenericTagsTransformer(tableInfo, configContext, textTemplateClass);

            var classBuilderPropertys = string.Empty;

            if (infos.IsAny())
            {

                foreach (var item in infos)
                {
                    if (Audit.IsAuditField(item.PropertyName))
                        continue;

                    if (item.IsKey == 1)
                    {
                        classBuilder = classBuilder.Replace("<#KeyName#>", item.PropertyName);
                        var cast = item.Type == "string" ? ".ToString()" : string.Empty;
                        classBuilder = classBuilder.Replace("<#toString()#>", cast);
                        var expressionInclusion = item.Type == "string" ? string.Format("string.IsNullOrEmpty(this.{0})", item.PropertyName) : string.Format("this.{0} == 0", item.PropertyName);
                    }

                    var itempropert = TextTemplatePropertys.
                            Replace("<#type#>", item.Type).
                            Replace("<#propertyName#>", item.PropertyName);

                    classBuilderPropertys += string.Format("{0}{1}{2}", Tabs.TabModels(), itempropert, System.Environment.NewLine);

                }
            }

            classBuilder = classBuilder.Replace("<#property#>", classBuilderPropertys);


            using (var stream = new HelperStream(pathOutput).GetInstance())
            {
                stream.Write(classBuilder);
            }

        }
        private void ExecuteTemplateDtoSpecialized(TableInfo tableInfo, Context configContext, IEnumerable<Info> infos)
        {
            var pathOutput = PathOutput.PathOutputDtoSpecialized(tableInfo, configContext);

            if (!tableInfo.MakeDto)
                return;


            if (File.Exists(pathOutput) && Convert.ToBoolean(ConfigurationManager.AppSettings["GerarDtoEspecializadosExistentes"]) == false)
                return;

            var pathTemplateClass = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, this._defineTemplateFolder.Define(tableInfo), DefineTemplateName.DtoSpecialized(tableInfo));
            if (!File.Exists(pathTemplateClass))
                return;

            var pathTemplateNavPropertysCollection = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, this._defineTemplateFolder.Define(tableInfo), DefineTemplateName.DtoNavPropertyCollection(tableInfo));
            var pathTemplateNavPropertysInstance = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, this._defineTemplateFolder.Define(tableInfo), DefineTemplateName.DtoNavPropertyInstance(tableInfo));


            var textTemplateClass = Read.AllText(tableInfo, pathTemplateClass, this._defineTemplateFolder);
            var TextTemplateNavPropertysCollections = Read.AllText(tableInfo, pathTemplateNavPropertysCollection, this._defineTemplateFolder);
            var TextTemplateNavPropertysInstance = Read.AllText(tableInfo, pathTemplateNavPropertysInstance, this._defineTemplateFolder);


            var classBuilder = GenericTagsTransformer(tableInfo, configContext, textTemplateClass);

            var classBuilderPropertys = string.Empty;

            classBuilder = ClearPropertyNavigationDto(tableInfo, configContext, TextTemplateNavPropertysCollections, TextTemplateNavPropertysInstance, classBuilder);

            using (var stream = new HelperStream(pathOutput).GetInstance())
            {
                stream.Write(classBuilder);
            }

        }
        private void ExecuteTemplateDtoSpecializedResult(TableInfo tableInfo, Context configContext, IEnumerable<Info> infos)
        {
            var pathOutput = PathOutput.PathOutputDtoSpecializedResult(tableInfo, configContext);
            if (!tableInfo.MakeDto)
                return;


            if (File.Exists(pathOutput) && Convert.ToBoolean(ConfigurationManager.AppSettings["GerarDtoEspecializadosDeResultadoExistentes"]) == false)
                return;

            var pathTemplateClass = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, this._defineTemplateFolder.Define(tableInfo), DefineTemplateName.DtoSpecilizedResult(tableInfo));
            if (!File.Exists(pathTemplateClass))
                return;

            var pathTemplateNavPropertysCollection = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, this._defineTemplateFolder.Define(tableInfo), DefineTemplateName.DtoNavPropertyCollection(tableInfo));
            var pathTemplateNavPropertysInstance = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, this._defineTemplateFolder.Define(tableInfo), DefineTemplateName.DtoNavPropertyInstance(tableInfo));


            var textTemplateClass = Read.AllText(tableInfo, pathTemplateClass, this._defineTemplateFolder);
            var TextTemplateNavPropertysCollections = Read.AllText(tableInfo, pathTemplateNavPropertysCollection, this._defineTemplateFolder);
            var TextTemplateNavPropertysInstance = Read.AllText(tableInfo, pathTemplateNavPropertysInstance, this._defineTemplateFolder);


            var classBuilder = GenericTagsTransformer(tableInfo, configContext, textTemplateClass);

            var classBuilderPropertys = string.Empty;

            classBuilder = ClearPropertyNavigationDto(tableInfo, configContext, TextTemplateNavPropertysCollections, TextTemplateNavPropertysInstance, classBuilder);

            using (var stream = new HelperStream(pathOutput).GetInstance())
            {
                stream.Write(classBuilder);
            }

        }
        private void ExecuteTemplateDtoSpecializedReport(TableInfo tableInfo, Context configContext, IEnumerable<Info> infos)
        {
            var pathOutput = PathOutput.PathOutputDtoSpecializedReport(tableInfo, configContext);

            if (!tableInfo.MakeDto)
                return;


            if (File.Exists(pathOutput) && Convert.ToBoolean(ConfigurationManager.AppSettings["GerarDtoEspecializadosDeRelatorioExistentes"]) == false)
                return;


            var pathTemplateClass = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, this._defineTemplateFolder.Define(tableInfo), DefineTemplateName.DtoSpecializedReport(tableInfo));
            if (!File.Exists(pathTemplateClass))
                return;

            var pathTemplateNavPropertysCollection = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, this._defineTemplateFolder.Define(tableInfo), DefineTemplateName.DtoNavPropertyCollection(tableInfo));
            var pathTemplateNavPropertysInstance = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, this._defineTemplateFolder.Define(tableInfo), DefineTemplateName.DtoNavPropertyInstance(tableInfo));


            var textTemplateClass = Read.AllText(tableInfo, pathTemplateClass, this._defineTemplateFolder);
            var TextTemplateNavPropertysCollections = Read.AllText(tableInfo, pathTemplateNavPropertysCollection, this._defineTemplateFolder);
            var TextTemplateNavPropertysInstance = Read.AllText(tableInfo, pathTemplateNavPropertysInstance, this._defineTemplateFolder);


            var classBuilder = GenericTagsTransformer(tableInfo, configContext, textTemplateClass);

            var classBuilderPropertys = string.Empty;

            classBuilder = ClearPropertyNavigationDto(tableInfo, configContext, TextTemplateNavPropertysCollections, TextTemplateNavPropertysInstance, classBuilder);

            using (var stream = new HelperStream(pathOutput).GetInstance())
            {
                stream.Write(classBuilder);
            }

        }
        private void ExecuteTemplateDtoSpecializedDetails(TableInfo tableInfo, Context configContext, IEnumerable<Info> infos)
        {
            var pathOutput = PathOutput.PathOutputDtoSpecializedDetails(tableInfo, configContext);

            if (!tableInfo.MakeDto)
                return;


            if (File.Exists(pathOutput) && Convert.ToBoolean(ConfigurationManager.AppSettings["GerarDtoEspecializadosDeDetalhesExistentes"]) == false)
                return;

            var pathTemplateClass = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, this._defineTemplateFolder.Define(tableInfo), DefineTemplateName.DtoSpecializedDetails(tableInfo));
            if (!File.Exists(pathTemplateClass))
                return;

            var pathTemplateNavPropertysCollection = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, this._defineTemplateFolder.Define(tableInfo), DefineTemplateName.DtoNavPropertyCollection(tableInfo));
            var pathTemplateNavPropertysInstance = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, this._defineTemplateFolder.Define(tableInfo), DefineTemplateName.DtoNavPropertyInstance(tableInfo));


            var textTemplateClass = Read.AllText(tableInfo, pathTemplateClass, this._defineTemplateFolder);
            var TextTemplateNavPropertysCollections = Read.AllText(tableInfo, pathTemplateNavPropertysCollection, this._defineTemplateFolder);
            var TextTemplateNavPropertysInstance = Read.AllText(tableInfo, pathTemplateNavPropertysInstance, this._defineTemplateFolder);


            var classBuilder = GenericTagsTransformer(tableInfo, configContext, textTemplateClass);

            var classBuilderPropertys = string.Empty;

            classBuilder = ClearPropertyNavigationDto(tableInfo, configContext, TextTemplateNavPropertysCollections, TextTemplateNavPropertysInstance, classBuilder);

            using (var stream = new HelperStream(pathOutput).GetInstance())
            {
                stream.Write(classBuilder);
            }

        }
        private void ExecuteTemplateAutoMapper(TableInfo tableInfo, Context configContext)
        {

            if (!tableInfo.MakeApp)
                return;

            var pathOutput = PathOutput.PathOutputAutoMapper(configContext);
            if (File.Exists(pathOutput))
                return;

            var pathTemplateClass = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, this._defineTemplateFolder.Define(tableInfo), DefineTemplateName.Automapper(tableInfo));
            if (!File.Exists(pathTemplateClass))
                return;

            var textTemplateClass = Read.AllText(tableInfo, pathTemplateClass, this._defineTemplateFolder);
            var classBuilder = GenericTagsTransformer(tableInfo, configContext, textTemplateClass);
            using (var stream = new HelperStream(pathOutput).GetInstance())
            {
                stream.Write(classBuilder);
            }

        }
        private void ExecuteTemplateApiConfig(TableInfo tableInfo, Context configContext)
        {
            if (!tableInfo.MakeApi)
                return;


            if (Convert.ToBoolean(ConfigurationManager.AppSettings["GerarWebApiConfig"]) == false)
                return;

            var pathOutput = PathOutput.PathOutputWebApiConfig(configContext);
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
        private void ExecuteConfigDomain(TableInfo tableInfo, Context configContext)
        {

            var pathOutput = PathOutput.PathOutputConfigDomain(configContext);
            if (File.Exists(pathOutput))
                return;

            var pathTemplateClass = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, this._defineTemplateFolder.Define(tableInfo), DefineTemplateName.ConfigDomain(tableInfo));
            if (!File.Exists(pathTemplateClass))
                return;

            var textTemplateClass = Read.AllText(tableInfo, pathTemplateClass, this._defineTemplateFolder);
            var classBuilder = GenericTagsTransformer(tableInfo, configContext, textTemplateClass);

            using (var stream = new HelperStream(pathOutput).GetInstance())
            {
                stream.Write(classBuilder);
            }

        }
        private void ExecuteHelperValidationAuth(TableInfo tableInfo, Context configContext)
        {

            var pathOutput = PathOutput.PathOutputHelperValidationAuth(configContext);
            if (File.Exists(pathOutput))
                return;

            var pathTemplateClass = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, this._defineTemplateFolder.Define(tableInfo), DefineTemplateName.HelperValidateAuth(tableInfo));
            if (!File.Exists(pathTemplateClass))
                return;

            var textTemplateClass = Read.AllText(tableInfo, pathTemplateClass, this._defineTemplateFolder);
            var classBuilder = GenericTagsTransformer(tableInfo, configContext, textTemplateClass);

            using (var stream = new HelperStream(pathOutput).GetInstance())
            {
                stream.Write(classBuilder);
            }

        }
        private void ExecuteTemplateCustomFilters(TableInfo tableInfo, Context configContext)
        {
            if (!tableInfo.InheritQuery)
                return;

            var pathOutput = PathOutput.PathOutputCustomFilters(tableInfo, configContext);
            if (File.Exists(pathOutput))
                return;

            var pathTemplateClass = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, this._defineTemplateFolder.Define(tableInfo), DefineTemplateName.ModelCustomFilters(tableInfo));
            if (!File.Exists(pathTemplateClass))
                return;

            var textTemplateClass = Read.AllText(tableInfo, pathTemplateClass, this._defineTemplateFolder);
            var classBuilder = GenericTagsTransformer(tableInfo, configContext, textTemplateClass);

            using (var stream = new HelperStream(pathOutput).GetInstance())
            {
                stream.Write(classBuilder);
            }

        }
        private void ExecuteTemplateAutoMapperProfile(TableInfo tableInfo, Context configContext)
        {
            if (!tableInfo.MakeDto)
                return;

            var pathOutput = PathOutput.PathOutputAutoMapperProfile(configContext, tableInfo);
            var pathTemplateClass = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, this._defineTemplateFolder.Define(tableInfo), DefineTemplateName.AutoMapperProfile(tableInfo));
            if (!File.Exists(pathTemplateClass))
                return;

            var pathTemplateMappers = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, this._defineTemplateFolder.Define(tableInfo), DefineTemplateName.ProfileRegisters(tableInfo));
            var pathTemplateMappersSpecilize = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, this._defineTemplateFolder.Define(tableInfo), DefineTemplateName.ProfileRegistersSpecilize(tableInfo));
            var pathTemplateMappersSpecilizeResult = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, this._defineTemplateFolder.Define(tableInfo), DefineTemplateName.ProfileRegistersSpecilizeResult(tableInfo));
            var pathTemplateMappersSpecilizeReport = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, this._defineTemplateFolder.Define(tableInfo), DefineTemplateName.ProfileRegistersSpecilizeReport(tableInfo));
            var pathTemplateMappersSpecilizeDetails = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, this._defineTemplateFolder.Define(tableInfo), DefineTemplateName.ProfileRegistersSpecilizeDetails(tableInfo));

            var textTemplateClass = Read.AllText(tableInfo, pathTemplateClass, this._defineTemplateFolder);
            var TextTemplateMappers = Read.AllText(tableInfo, pathTemplateMappers, this._defineTemplateFolder);
            var TextTemplateMappersSpecilize = Read.AllText(tableInfo, pathTemplateMappersSpecilize, this._defineTemplateFolder);
            var TextTemplateMappersSpecilizeResult = Read.AllText(tableInfo, pathTemplateMappersSpecilizeResult, this._defineTemplateFolder);
            var TextTemplateMappersSpecilizeReport = Read.AllText(tableInfo, pathTemplateMappersSpecilizeReport, this._defineTemplateFolder);
            var TextTemplateMappersSpecilizeDetails = Read.AllText(tableInfo, pathTemplateMappersSpecilizeDetails, this._defineTemplateFolder);

            var classBuilder = GenericTagsTransformer(tableInfo, configContext, textTemplateClass);

            var classBuilderMappers = string.Empty;


            foreach (var item in configContext.TableInfo)
            {
                var className = item.ClassName;

                if (!string.IsNullOrEmpty(className))
                {
                    var itemMappaer = TextTemplateMappers.
                            Replace("<#className#>", className);

                    var itemMappaerSpecilize = TextTemplateMappersSpecilize.
                           Replace("<#className#>", className);

                    var itemMappaerSpecilizeResult = TextTemplateMappersSpecilizeResult.
                        Replace("<#className#>", className);

                    var itemMappaerSpecilizeReport = TextTemplateMappersSpecilizeReport.
                        Replace("<#className#>", className);

                    var itemMappaerSpecilizeDetails = TextTemplateMappersSpecilizeDetails.
                        Replace("<#className#>", className);


                    classBuilderMappers += string.Format("{0}{1}{2}", Tabs.TabSets(), itemMappaer, System.Environment.NewLine);
                    classBuilderMappers += string.Format("{0}{1}{2}", Tabs.TabSets(), itemMappaerSpecilize, System.Environment.NewLine);
                    classBuilderMappers += string.Format("{0}{1}{2}", Tabs.TabSets(), itemMappaerSpecilizeResult, System.Environment.NewLine);
                    classBuilderMappers += string.Format("{0}{1}{2}", Tabs.TabSets(), itemMappaerSpecilizeReport, System.Environment.NewLine);
                    classBuilderMappers += string.Format("{0}{1}{2}", Tabs.TabSets(), itemMappaerSpecilizeDetails, System.Environment.NewLine);
                }
            }



            classBuilder = classBuilder.Replace("<#registers#>", classBuilderMappers);


            using (var stream = new HelperStream(pathOutput).GetInstance())
            {
                stream.Write(classBuilder);
            }

        }

        private void ExecuteTemplateAutoMapperProfileCustom(TableInfo tableInfo, Context configContext)
        {
            if (!tableInfo.MakeDto)
                return;

            var pathOutput = PathOutput.PathOutputAutoMapperProfileCustom(configContext, tableInfo);
            var pathTemplateClass = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, this._defineTemplateFolder.Define(tableInfo), DefineTemplateName.AutoMapperProfileCustom(tableInfo));
            if (!File.Exists(pathTemplateClass))
                return;

            var textTemplateClass = Read.AllText(tableInfo, pathTemplateClass, this._defineTemplateFolder);
            var classBuilder = GenericTagsTransformer(tableInfo, configContext, textTemplateClass);

            using (var stream = new HelperStream(pathOutput).GetInstance())
            {
                stream.Write(classBuilder);
            }

        }

        private void ExecuteTemplateUri(TableInfo tableInfo, Context configContext)
        {

            var pathOutput = PathOutput.PathOutputUri(tableInfo, configContext);

            if (!tableInfo.MakeApi)
                return;

            var pathTemplateClass = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, this._defineTemplateFolder.Define(tableInfo), DefineTemplateName.Uri(tableInfo));

            var textTemplateClass = Read.AllText(tableInfo, pathTemplateClass, this._defineTemplateFolder);
            var classBuilder = GenericTagsTransformer(tableInfo, configContext, textTemplateClass);

            using (var stream = new HelperStream(pathOutput).GetInstance())
            {
                stream.Write(classBuilder);
            }

        }

        private void ExecuteTemplateApiTransaction(TableInfo tableInfo, Context configContext)
        {

            var pathOutput = PathOutput.PathOutputApiTransaction(tableInfo, configContext);

            if (!tableInfo.MakeApi)
                return;

            var pathTemplateClass = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, this._defineTemplateFolder.Define(tableInfo), DefineTemplateName.ApiTransaction(tableInfo));
            if (!File.Exists(pathTemplateClass))
                return;

            var textTemplateClass = Read.AllText(tableInfo, pathTemplateClass, this._defineTemplateFolder);
            var classBuilder = GenericTagsTransformer(tableInfo, configContext, textTemplateClass);

            using (var stream = new HelperStream(pathOutput).GetInstance())
            {
                stream.Write(classBuilder);
            }

        }

        private void ExecuteTemplateRepositoryTransaction(TableInfo tableInfo, Context configContext)
        {

            var pathOutput = PathOutput.PathOutputTransactionRepositoy(tableInfo, configContext);

            if (!tableInfo.MakeApi)
                return;

            var pathTemplateClass = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, this._defineTemplateFolder.Define(tableInfo), DefineTemplateName.RepositoryTransaction(tableInfo));
            if (!File.Exists(pathTemplateClass))
                return;


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

            if (tableInfo.InheritQuery)
                return;

            var pathOutput = PathOutput.PathOutputDbContext(configContext);
            var pathTemplateClass = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, this._defineTemplateFolder.Define(tableInfo), DefineTemplateName.Context(tableInfo));
            if (!File.Exists(pathTemplateClass))
                return;

            var pathTemplateRegister = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, this._defineTemplateFolder.Define(tableInfo), DefineTemplateName.ContextMappers(tableInfo));


            var textTemplateClass = Read.AllText(tableInfo, pathTemplateClass, this._defineTemplateFolder);
            var TextTemplateMappers = Read.AllText(tableInfo, pathTemplateRegister, this._defineTemplateFolder);
            if (configContext.Module.IsNullOrEmpty())
                textTemplateClass = textTemplateClass.Replace("<#module#>", configContext.ProjectName);

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
        private void ExecuteTemplateContainerWeb(TableInfo tableInfo, Context configContext)
        {
            if (!tableInfo.MakeApi)
                return;

            var pathOutput = PathOutput.PathOutputContainerWeb(configContext);

            var pathTemplateClass = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, this._defineTemplateFolder.Define(tableInfo), DefineTemplateName.ContainerWeb(tableInfo));
            if (!File.Exists(pathTemplateClass))
                return;

            var pathTemplateInjections = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, this._defineTemplateFolder.Define(tableInfo), DefineTemplateName.ContainerInjectionsWeb(tableInfo));


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
                            Replace("<#contextName#>", configContext.ContextName).
                            Replace("<#namespaceDomainSource#>", configContext.NamespaceDomainSource);


                    classBuilderMappers += string.Format("{0}{1}{2}", Tabs.TabModels(), itemInjections, System.Environment.NewLine);
                }
            }


            classBuilder = classBuilder.Replace("<#injections#>", classBuilderMappers);


            using (var stream = new HelperStream(pathOutput).GetInstance())
            {
                stream.Write(classBuilder);
            }

        }
        private void ExecuteTemplateContainer(TableInfo tableInfo, Context configContext)
        {
            if (!tableInfo.MakeApp)
                return;

            var pathOutput = PathOutput.PathOutputContainer(configContext);

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
                            Replace("<#contextName#>", configContext.ContextName).
                            Replace("<#namespaceDomainSource#>", configContext.NamespaceDomainSource);


                    classBuilderMappers += string.Format("{0}{1}{2}", Tabs.TabModels(), itemInjections, System.Environment.NewLine);
                }
            }


            classBuilder = classBuilder.Replace("<#injections#>", classBuilderMappers);


            using (var stream = new HelperStream(pathOutput).GetInstance())
            {
                stream.Write(classBuilder);
            }

        }
        private void ExecuteTemplateContainerPartialWeb(TableInfo tableInfo, Context configContext)
        {

            var pathOutput = PathOutput.PathOutputContainerPartialWeb(configContext);

            if (!tableInfo.MakeApi)
                return;

            if (File.Exists(pathOutput) && tableInfo.CodeCustomImplemented)
                return;
            
            var pathTemplateClass = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, this._defineTemplateFolder.Define(tableInfo), DefineTemplateName.ContainerPartialWeb(tableInfo));
            if (!File.Exists(pathTemplateClass))
                return;

            var textTemplateClass = Read.AllText(tableInfo, pathTemplateClass, this._defineTemplateFolder);

            var classBuilder = GenericTagsTransformer(tableInfo, configContext, textTemplateClass);
            using (var stream = new HelperStream(pathOutput).GetInstance())
            {
                stream.Write(classBuilder);
            }

        }
        private void ExecuteTemplateContainerPartial(TableInfo tableInfo, Context configContext)
        {

            var pathOutput = PathOutput.PathOutputContainerPartial(configContext);

            if (!tableInfo.MakeApp)
                return;

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
        private void ExecuteTemplateSimpleFilters(TableInfo tableInfo, Context configContext, IEnumerable<Info> infos)
        {

            if (!tableInfo.InheritQuery)
                return;

            var pathOutput = PathOutput.PathOutputSimpleFilters(tableInfo, configContext);
            var pathTemplateClass = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, this._defineTemplateFolder.Define(tableInfo), DefineTemplateName.SimpleFilters(tableInfo));
            if (!File.Exists(pathTemplateClass))
                return;

            var pathTemplateFilters = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, this._defineTemplateFolder.Define(tableInfo), DefineTemplateName.FiltersExpression(tableInfo));

            var textTemplateClass = Read.AllText(tableInfo, pathTemplateClass, this._defineTemplateFolder);
            var textTemplateFilters = Read.AllText(tableInfo, pathTemplateFilters, this._defineTemplateFolder);

            var classBuilder = GenericTagsTransformer(tableInfo, configContext, textTemplateClass);
            var classBuilderFilters = string.Empty;

            classBuilderFilters = BuilderSampleFilters(infos, textTemplateFilters, classBuilderFilters, configContext);

            classBuilder = classBuilder.Replace("<#filtersExpressions#>", classBuilderFilters);


            using (var stream = new HelperStream(pathOutput).GetInstance())
            {
                stream.Write(classBuilder);
            }

        }
        private void ExecuteTemplateModels(TableInfo tableInfo, Context configContext, IEnumerable<Info> infos)
        {
            if (tableInfo.CodeCustomImplemented)
                return;

            if (!tableInfo.MakeDomain)
                return;

            if (tableInfo.InheritQuery)
                return;

            var pathOutput = PathOutput.PathOutputDomainModels(tableInfo, configContext);
            var pathTemplateClass = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, this._defineTemplateFolder.Define(tableInfo), DefineTemplateName.Models(tableInfo));
            if (!File.Exists(pathTemplateClass))
                return;

            var pathTemplatePropertys = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, this._defineTemplateFolder.Define(tableInfo), DefineTemplateName.ModelsProperty(tableInfo));
            var pathTemplateFilters = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, this._defineTemplateFolder.Define(tableInfo), DefineTemplateName.FiltersExpression(tableInfo));
            var pathTemplateAuditCall = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, this._defineTemplateFolder.Define(tableInfo), DefineTemplateName.AuditCall(tableInfo));

            if (!File.Exists(pathTemplateClass))
                return;

            var textTemplateClass = Read.AllText(tableInfo, pathTemplateClass, this._defineTemplateFolder);
            var textTemplatePropertys = Read.AllText(tableInfo, pathTemplatePropertys, this._defineTemplateFolder);
            var textTemplateFilters = Read.AllText(tableInfo, pathTemplateFilters, this._defineTemplateFolder);
            var TextTemplateAuditCall = Read.AllText(tableInfo, pathTemplateAuditCall, this._defineTemplateFolder);

            var classBuilder = GenericTagsTransformer(tableInfo, configContext, textTemplateClass);

            var classBuilderPropertys = string.Empty;
            var classBuilderFilters = string.Empty;
            var textTemplateAudit = string.Empty;

            var generateAudit = Audit.ExistsAuditFieldsDefault(infos);

            if (infos.IsAny())
            {
                foreach (var item in infos)
                {
                    if (item.IsKey == 1)
                    {
                        classBuilder = classBuilder.
                            Replace("<#KeyName#>", item.PropertyName).
                            Replace("<#KeyNameType#>", item.Type);

                        var cast = item.Type == "string" ? ".ToString()" : string.Empty;
                        classBuilder = classBuilder.Replace("<#toString()#>", cast);
                    }

                    var itemFilters = string.Empty;

                    if (item.Type == "string")
                    {
                        itemFilters = textTemplateFilters.Replace("<#propertyName#>", item.PropertyName);
                        itemFilters = itemFilters.Replace("<#condition#>", string.Format("_=>_.{0}.Contains(filters.{0})", item.PropertyName));
                        itemFilters = itemFilters.Replace("<#filtersRange#>", string.Empty);
                    }
                    else if (item.Type == "DateTime")
                    {
                        var itemFiltersStart = textTemplateFilters.Replace("<#propertyName#>", String.Format("{0}Start", item.PropertyName));
                        itemFiltersStart = itemFiltersStart.Replace("<#condition#>", string.Format("_=>_.{0} >= filters.{0}Start ", item.PropertyName));
                        itemFiltersStart = itemFiltersStart.Replace("<#filtersRange#>", string.Empty);

                        var itemFiltersEnd = textTemplateFilters.Replace("<#propertyName#>", String.Format("{0}End", item.PropertyName));
                        itemFiltersEnd = itemFiltersEnd.Replace("<#condition#>", string.Format("_=>_.{0}  <= filters.{0}End", item.PropertyName));
                        itemFiltersEnd = itemFiltersEnd.Replace("<#filtersRange#>", string.Format("filters.{0}End = filters.{0}End.AddDays(1).AddMilliseconds(-1);", item.PropertyName));

                        itemFilters = String.Format("{0}{1}{2}{3}{4}", itemFiltersStart, System.Environment.NewLine, Tabs.TabSets(), itemFiltersEnd, System.Environment.NewLine);

                    }
                    else if (item.Type == "DateTime?")
                    {
                        var itemFiltersStart = textTemplateFilters.Replace("<#propertyName#>", String.Format("{0}Start", item.PropertyName));
                        itemFiltersStart = itemFiltersStart.Replace("<#condition#>", string.Format("_=>_.{0} != null && _.{0}.Value >= filters.{0}Start.Value", item.PropertyName));
                        itemFiltersStart = itemFiltersStart.Replace("<#filtersRange#>", string.Empty);

                        var itemFiltersEnd = textTemplateFilters.Replace("<#propertyName#>", String.Format("{0}End", item.PropertyName));
                        itemFiltersEnd = itemFiltersEnd.Replace("<#condition#>", string.Format("_=>_.{0} != null &&  _.{0}.Value <= filters.{0}End", item.PropertyName));
                        itemFiltersEnd = itemFiltersEnd.Replace("<#filtersRange#>", string.Format("filters.{0}End = filters.{0}End.Value.AddDays(1).AddMilliseconds(-1);", item.PropertyName));

                        itemFilters = String.Format("{0}{1}{2}{3}{4}", itemFiltersStart, System.Environment.NewLine, Tabs.TabSets(), itemFiltersEnd, System.Environment.NewLine);

                    }
                    else if (item.Type == "bool?")
                    {
                        itemFilters = textTemplateFilters.Replace("<#propertyName#>", item.PropertyName);
                        itemFilters = itemFilters.Replace("<#condition#>", string.Format("_=>_.{0} != null && _.{0}.Value == filters.{0}", item.PropertyName));
                        itemFilters = itemFilters.Replace("<#filtersRange#>", string.Empty);
                    }
                    else if (item.Type == "int?" || item.Type == "Int64?" || item.Type == "Int16?" || item.Type == "decimal?" || item.Type == "float?")
                    {
                        itemFilters = textTemplateFilters.Replace("<#propertyName#>", item.PropertyName);
                        itemFilters = itemFilters.Replace("<#condition#>", string.Format("_=>_.{0} != null && _.{0}.Value == filters.{0}", item.PropertyName));
                        itemFilters = itemFilters.Replace("<#filtersRange#>", string.Empty);
                    }
                    else
                    {
                        itemFilters = textTemplateFilters.Replace("<#propertyName#>", item.PropertyName);
                        itemFilters = itemFilters.Replace("<#condition#>", string.Format("_=>_.{0} == filters.{0}", item.PropertyName));
                        itemFilters = itemFilters.Replace("<#filtersRange#>", string.Empty);
                    }


                    classBuilderFilters += string.Format("{0}{1}{2}", Tabs.TabSets(), itemFilters, System.Environment.NewLine);

                    var itempropert = textTemplatePropertys.
                            Replace("<#type#>", item.Type).
                            Replace("<#propertyName#>", item.PropertyName);
                    classBuilderPropertys += string.Format("{0}{1}{2}", Tabs.TabModels(), itempropert, System.Environment.NewLine);

                    textTemplateAudit = Audit.MakeAuditRow(tableInfo, generateAudit, item, textTemplateAudit, this._defineTemplateFolder);

                }
            }

            classBuilder = classBuilder.Replace("<#callAudit#>", generateAudit ? TextTemplateAuditCall : string.Empty);
            classBuilder = classBuilder.Replace("<#audit#>", textTemplateAudit);
            classBuilder = classBuilder.Replace("<#IAudit#>", generateAudit ? " IAudit, " : string.Empty);

            classBuilder = classBuilder.Replace("<#property#>", classBuilderPropertys);

            classBuilder = classBuilder.Replace("<#filtersExpressions#>", classBuilderFilters);


            using (var stream = new HelperStream(pathOutput).GetInstance())
            {
                stream.Write(classBuilder);
            }

        }
        private void ExecuteTemplateModelsPartial(TableInfo tableInfo, Context configContext, IEnumerable<Info> infos)
        {
            if (!tableInfo.MakeDomain)
                return;

            var pathOutput = PathOutput.PathOutputDomainModelsPartial(tableInfo, configContext);
            if (File.Exists(pathOutput) && tableInfo.CodeCustomImplemented)
                return;

            if (File.Exists(pathOutput) && Convert.ToBoolean(ConfigurationManager.AppSettings["GerarModelPartialExistentes"]) == false)
                return;

            var pathTemplateClass = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, this._defineTemplateFolder.Define(tableInfo), DefineTemplateName.ModelPartial(tableInfo));
            if (!File.Exists(pathTemplateClass))
                return;

            var pathTemplateNavPropertysCollection = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, this._defineTemplateFolder.Define(tableInfo), DefineTemplateName.ModelsNavPropertyCollection(tableInfo));
            var pathTemplateNavPropertysInstance = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, this._defineTemplateFolder.Define(tableInfo), DefineTemplateName.ModelsNavPropertyInstance(tableInfo));

            var textTemplateClass = Read.AllText(tableInfo, pathTemplateClass, this._defineTemplateFolder);
            var TextTemplateNavPropertysCollections = Read.AllText(tableInfo, pathTemplateNavPropertysCollection, this._defineTemplateFolder);
            var TextTemplateNavPropertysInstance = Read.AllText(tableInfo, pathTemplateNavPropertysInstance, this._defineTemplateFolder);

            var classBuilderitemTemplateValidation = string.Empty;

            var classBuilder = GenericTagsTransformer(tableInfo, configContext, textTemplateClass);
            classBuilder = classBuilder.Replace("<#WhereSingle#>", MakeKeysFromGet(tableInfo));


            classBuilder = MakePropertyNavigationModels(tableInfo, configContext, TextTemplateNavPropertysCollections, TextTemplateNavPropertysInstance, classBuilder);

            using (var stream = new HelperStream(pathOutput).GetInstance())
            {
                stream.Write(classBuilder);
            }

        }
        private void ExecuteTemplateModelsValiadation(TableInfo tableInfo, Context configContext, IEnumerable<Info> infos)
        {
            if (tableInfo.InheritQuery)
                return;

            var pathOutput = PathOutput.PathOutputDomainModelsValidation(tableInfo, configContext);

            if (!tableInfo.MakeDomain)
                return;

            if (!tableInfo.MakeCrud)
                return;


            var pathTemplateClass = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, this._defineTemplateFolder.Define(tableInfo), DefineTemplateName.ModelsValidation(tableInfo));
            var pathTemplatevalidation = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, this._defineTemplateFolder.Define(tableInfo), DefineTemplateName.ModelsValidationProperty(tableInfo));
            var pathTemplatevalidationLength = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, this._defineTemplateFolder.Define(tableInfo), DefineTemplateName.ModelsValidationLength(tableInfo));
            var pathTemplatevalidationRequired = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, this._defineTemplateFolder.Define(tableInfo), DefineTemplateName.ModelsValidationRequired(tableInfo));

            var textTemplateClass = Read.AllText(tableInfo, pathTemplateClass, this._defineTemplateFolder);
            var TextTemplateValidation = Read.AllText(tableInfo, pathTemplatevalidation, this._defineTemplateFolder);
            var TextTemplateValidationLength = Read.AllText(tableInfo, pathTemplatevalidationLength, this._defineTemplateFolder);
            var TextTemplateValidationRequired = Read.AllText(tableInfo, pathTemplatevalidationRequired, this._defineTemplateFolder);

            var classBuilderitemTemplateValidation = string.Empty;
            var classBuilder = GenericTagsTransformer(tableInfo, configContext, textTemplateClass);

            if (infos.IsAny())
            {

                foreach (var item in infos)
                {

                    if (item.IsKey == 1)
                        continue;

                    if (item.PropertyName.IsNotNull() && item.PropertyName.EndsWith("Id"))
                        continue;

                    if (Audit.IsAuditField(item.PropertyName))
                        continue;

                    classBuilderitemTemplateValidation = MakeValidationsAttributes(TextTemplateValidation, TextTemplateValidationLength, TextTemplateValidationRequired, classBuilderitemTemplateValidation, item, configContext);

                }
            }

            classBuilderitemTemplateValidation = classBuilderitemTemplateValidation.Replace("<#className#>", tableInfo.ClassName);
            classBuilder = classBuilder.Replace("<#property#>", classBuilderitemTemplateValidation);

            using (var stream = new HelperStream(pathOutput).GetInstance())
            {
                stream.Write(classBuilder);
            }

        }
        private void ExecuteTemplateModelsValiadationPartial(TableInfo tableInfo, Context configContext, IEnumerable<Info> infos)
        {
            if (tableInfo.InheritQuery)
                return;

            var pathOutput = PathOutput.PathOutputDomainModelsValidationPartial(tableInfo, configContext);

            if (!tableInfo.MakeDomain)
                return;

            if (File.Exists(pathOutput) && tableInfo.CodeCustomImplemented)
                return;

            if (File.Exists(pathOutput) && Convert.ToBoolean(ConfigurationManager.AppSettings["GerarValidationsPartialExistentes"]) == false)
                return;

            if (!tableInfo.MakeCrud)
                return;


            var pathTemplateClass = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, this._defineTemplateFolder.Define(tableInfo), DefineTemplateName.ModelsPartialValidation(tableInfo));
            if (!File.Exists(pathTemplateClass))
                return;

            var pathTemplatevalidation = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, this._defineTemplateFolder.Define(tableInfo), DefineTemplateName.ModelsPartialValidationProperty(tableInfo));
            var pathTemplatevalidationLength = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, this._defineTemplateFolder.Define(tableInfo), DefineTemplateName.ModelsPartialValidationLength(tableInfo));
            var pathTemplatevalidationRequired = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, this._defineTemplateFolder.Define(tableInfo), DefineTemplateName.ModelsPartialValidationRequired(tableInfo));

            var textTemplateClass = Read.AllText(tableInfo, pathTemplateClass, this._defineTemplateFolder);
            var TextTemplateValidation = Read.AllText(tableInfo, pathTemplatevalidation, this._defineTemplateFolder);
            var TextTemplateValidationLength = Read.AllText(tableInfo, pathTemplatevalidationLength, this._defineTemplateFolder);
            var TextTemplateValidationRequired = Read.AllText(tableInfo, pathTemplatevalidationRequired, this._defineTemplateFolder);

            var classBuilderitemTemplateValidation = string.Empty;
            var classBuilder = GenericTagsTransformer(tableInfo, configContext, textTemplateClass);

            if (infos.IsAny())
            {
                foreach (var item in infos)
                {

                    if (item.IsKey == 1)
                        continue;

                    if (Audit.IsAuditField(item.PropertyName))
                        continue;

                    if (item.PropertyName.IsNotNull() && item.PropertyName.ToLower().EndsWith("id"))
                        classBuilderitemTemplateValidation = MakeValidationsAttributes(TextTemplateValidation, TextTemplateValidationLength, TextTemplateValidationRequired, classBuilderitemTemplateValidation, item, configContext);

                }
            }

            classBuilderitemTemplateValidation = classBuilderitemTemplateValidation.Replace("<#className#>", tableInfo.ClassName);
            classBuilder = classBuilder.Replace("<#property#>", classBuilderitemTemplateValidation);

            using (var stream = new HelperStream(pathOutput).GetInstance())
            {
                stream.Write(classBuilder);
            }

        }
        private void ExecuteTemplateModelsCustom(TableInfo tableInfo, Context configContext, IEnumerable<Info> infos)
        {
            if (tableInfo.InheritQuery)
                return;

            var pathOutput = PathOutput.PathOutputDomainModelsCustom(tableInfo, configContext);

            if (!tableInfo.MakeDomain)
                return;

            if (File.Exists(pathOutput) && tableInfo.CodeCustomImplemented)
                return;

            if (File.Exists(pathOutput) && Convert.ToBoolean(ConfigurationManager.AppSettings["GerarModelCustomExistentes"]) == false)
                return;

            var pathTemplateClass = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, this._defineTemplateFolder.Define(tableInfo), DefineTemplateName.ModelsCustom(tableInfo));
            if (!File.Exists(pathTemplateClass))
                return;

            var textTemplateClass = Read.AllText(tableInfo, pathTemplateClass, this._defineTemplateFolder);
            var classBuilder = GenericTagsTransformer(tableInfo, configContext, textTemplateClass);

            using (var stream = new HelperStream(pathOutput).GetInstance())
            {
                stream.Write(classBuilder);
            }

        }
        private void ExecuteTemplateSummary(TableInfo tableInfo, Context configContext)
        {
            var pathOutput = PathOutput.PathOutputSummary(tableInfo, configContext);

            if (!tableInfo.MakeSummary)
                return;

            if (File.Exists(pathOutput) || tableInfo.CodeCustomImplemented)
                return;

            var pathTemplateClass = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, this._defineTemplateFolder.Define(tableInfo), DefineTemplateName.Summary(tableInfo));
            var textTemplateClass = Read.AllText(tableInfo, pathTemplateClass, this._defineTemplateFolder);
            var classBuilder = GenericTagsTransformer(tableInfo, configContext, textTemplateClass);

            using (var stream = new HelperStream(pathOutput).GetInstance())
            {
                stream.Write(classBuilder);
            }

        }
        private void ExecuteTemplateDbContextInherit(TableInfo tableInfo, Context configContext)
        {
            if (!tableInfo.InheritQuery)
                return;

            if (!tableInfo.MakeDomain)
                return;

            var pathOutput = PathOutput.PathOutputContextsInherit(tableInfo, configContext);
            if (File.Exists(pathOutput))
                return;

            var pathTemplateClass = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, this._defineTemplateFolder.Define(tableInfo), DefineTemplateName.Context(tableInfo));
            if (!File.Exists(pathTemplateClass))
                return;

            var textTemplateClass = Read.AllText(tableInfo, pathTemplateClass, this._defineTemplateFolder);
            if (configContext.Module.IsNullOrEmpty())
                textTemplateClass = textTemplateClass.Replace("<#module#>", configContext.ProjectName);

            var classBuilder = GenericTagsTransformer(tableInfo, configContext, textTemplateClass);


            using (var stream = new HelperStream(pathOutput).GetInstance())
            {
                stream.Write(classBuilder);
            }

        }
        private void ExecuteTemplateMapsPartial(TableInfo tableInfo, Context configContext, IEnumerable<Info> infos)
        {
            var pathOutput = PathOutput.PathOutputMapsPartial(tableInfo, configContext);

            if (!tableInfo.MakeDomain)
                return;

            if (!tableInfo.Scaffold)
                return;

            if (File.Exists(pathOutput) && tableInfo.CodeCustomImplemented)
                return;

            if (File.Exists(pathOutput) && Convert.ToBoolean(ConfigurationManager.AppSettings["GerarMapperPartialExistentes"]) == false)
                return;

            var pathTemplateClass = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, this._defineTemplateFolder.Define(tableInfo), DefineTemplateName.MapPartial(tableInfo));
            var textTemplateClass = Read.AllText(tableInfo, pathTemplateClass, this._defineTemplateFolder);

            var classBuilder = GenericTagsTransformer(tableInfo, configContext, textTemplateClass);


            using (var stream = new HelperStream(pathOutput).GetInstance())
            {
                stream.Write(classBuilder);
            }

        }

        private void ExecuteTemplateDbContextGenerateViews(TableInfo tableInfo, Context configContext)
        {
            var pathOutput = PathOutput.PathOutputPreCompiledView(configContext);
            var pathTemplateClass = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, this._defineTemplateFolder.Define(), DefineTemplateName.PrecompiledViewBasic());
            if (!File.Exists(pathTemplateClass))
                return;

            var textTemplateClass = Read.AllText(tableInfo, pathTemplateClass, this._defineTemplateFolder);
            var classBuilder = GenericTagsTransformer(tableInfo, configContext, textTemplateClass);

            using (var stream = new HelperStream(pathOutput).GetInstance())
            {
                stream.Write(classBuilder);
            }

        }
        private void ExecuteTemplateMaps(TableInfo tableInfo, Context configContext, IEnumerable<Info> infos)
        {
            if (!tableInfo.MakeDomain)
                return;

            if (!tableInfo.Scaffold)
                return;


            var pathOutput = PathOutput.PathOutputMaps(tableInfo, configContext);
            var pathTemplateClass = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, this._defineTemplateFolder.Define(tableInfo), DefineTemplateName.Maps(tableInfo));
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

            string classBuilder = MakeClassBuilderMapORM(tableInfo, configContext, infos, textTemplateClass, textTemplateLength, textTemplateRequired, textTemplateMapper, textTemplateManyToMany, textTemplateCompositeKey, ref classBuilderitemTemplateLength, ref classBuilderitemTemplateRequired, ref classBuilderitemplateMapper, ref classBuilderitemplateMapperKey, ref classBuilderitemplateManyToMany, ref classBuilderitemplateCompositeKey);

            using (var stream = new HelperStream(pathOutput).GetInstance())
            {
                stream.Write(classBuilder);
            }


        }

        #endregion

        #region helpers

        private string MakePropertyNameKeyAlias(string column, string className)
        {

            if (column.ToLower() == "id")
                return string.Format("{0}Id", className);


            return column;
        }
        private void MigatePathFileDomain(Context config, TableInfo tableInfo)
        {
            var fileBaseName = tableInfo.ClassName;
            var pathBase = Path.GetDirectoryName(PathOutput.PathOutputDomainModels(tableInfo, config)).Replace(fileBaseName, "");

            var FileVariants = new string[] {
                    string.Format("{0}.cs", fileBaseName),
                    string.Format("{0}.ext.cs", fileBaseName),
                    string.Format("{0}.Validation.cs", fileBaseName),
                    string.Format("{0}.Validation.ext.cs", fileBaseName),
                    string.Format("{0}Custom.ext.cs", fileBaseName),
                };

            MigatePathFile(pathBase, fileBaseName, FileVariants);

        }
        private void MigatePathFileDto(Context config, TableInfo tableInfo)
        {
            var fileBaseName = tableInfo.ClassName;
            var pathBase = Path.GetDirectoryName(PathOutput.PathOutputDto(tableInfo, config)).Replace(fileBaseName, "");

            var FileVariants = new string[] {
                    string.Format("{0}Dto.cs", fileBaseName),
                    string.Format("{0}DtoSpecialized.ext.cs", fileBaseName),
                    string.Format("{0}DtoSpecializedResult.ext.cs", fileBaseName),
                    string.Format("{0}DtoSpecializedReport.ext.cs", fileBaseName),
                    string.Format("{0}DtoSpecializedDetails.ext.cs", fileBaseName),
                };

            MigatePathFile(pathBase, fileBaseName, FileVariants);

        }
        private void MigatePathFile(string pathBase, string fileBaseName, string[] FileVariants)
        {

            var folder = Path.Combine(pathBase, fileBaseName);
            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);

            foreach (var file in FileVariants)
            {
                var fileFound = new DirectoryInfo(pathBase).GetFiles(file).SingleOrDefault();
                if (fileFound != null)
                {
                    if (File.Exists(fileFound.FullName))
                    {
                        fileFound.CopyTo(Path.Combine(folder, file));
                        fileFound.Delete();
                    }
                }

            }
        }
        private bool IsPrimaryKey(SqlDataReader reader)
        {
            return Convert.ToInt32(reader["Chave"]) == 1;
        }
        private bool IsOneToOneRelation(SqlDataReader reader)
        {
            return reader["FKCOLUMN_NAME"].ToString() == reader["PKCOLUMN_NAME"].ToString();
        }
        private void ClearContextCodeFiles(Context config, string path, string subfolder, Func<TableInfo, bool> predicate, string noDeleteFilesPartner = null)
        {
            if (!config.ClearAllFiles)
                return;

            if (String.IsNullOrEmpty(path))
                return;

            var directory = new DirectoryInfo(Path.Combine(path, subfolder));
            if (!directory.Exists)
                return;

            var files = directory.GetFiles().Where(_ => _.Extension == ".cs");
            foreach (var item in files)
            {

                if (!String.IsNullOrEmpty(noDeleteFilesPartner))
                    if (item.Name.Contains(noDeleteFilesPartner))
                        continue;


                if (item.Name.Contains(".ext"))
                    continue;

                var deleteDenied = this.Contexts
                    .Where(_ => _.TableInfo
                        .Where(__ => item.Name.Contains(__.TableName))
                        .Where(predicate).Any()).Any();


                if (deleteDenied)
                    continue;


                item.Delete();
            }

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
