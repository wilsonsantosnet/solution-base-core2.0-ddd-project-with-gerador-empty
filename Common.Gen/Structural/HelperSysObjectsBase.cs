using Common.Domain;
using Common.Gen.Structural;
using Common.Gen.Utils;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace Common.Gen
{
    public abstract class HelperSysObjectsBase
    {

        public HelperSysObjectsBase()
        {
            this._camelCasingExceptions = new List<string> { "cpfcnpj", "cnpj", "cpf", "cep", "cpf_cnpj", "ie" };
        }

        protected DefineTemplateFolder _defineTemplateFolder;
        protected IEnumerable<string> _camelCasingExceptions;
        protected SqlConnection conn { get; set; }
        public IEnumerable<Context> Contexts { get; set; }

        public abstract void DefineTemplateByTableInfoFields(Context config, TableInfo tableInfo, UniqueListInfo infos);
        public abstract void DefineTemplateByTableInfo(Context config, TableInfo tableInfo);
        protected ArquitetureType ArquitetureType;
        public bool DisableCompleteFlow { get; set; }

        protected virtual void DefineAuditFields(params string[] fields)
        {
            Audit.SetAuditFields(fields);
        }

        public virtual string GetOutputClassRoot()
        {
            return ConfigurationManager.AppSettings["OutputClassRoot"] as string;
        }


        protected virtual ArquitetureType getArquitetureType()
        {
            return this.ArquitetureType;
        }

        public virtual void MakeClass(Context config)
        {
            MakeClass(config, string.Empty, true);
        }
        public virtual void MakeClass(Context config, bool UsePathProjects)
        {
            MakeClass(config, string.Empty, UsePathProjects);
        }
        public virtual void MakeClass(Context config, string RunOnlyThisClass, bool UsePathProjects)
        {
            config.UsePathProjects = UsePathProjects;
            this.DefineAuditFields(Audit.GetAuditFields());
            ExecuteTemplateByTableInfoFields(config, RunOnlyThisClass);
            ExecuteTemplatesByTableleInfo(config, RunOnlyThisClass);
            this.Dispose();

        }
        protected virtual string MakeClassName(TableInfo tableInfo)
        {
            return tableInfo.TableName;
        }
        protected virtual string MakePropertyName(string column, string className, int key)
        {

            if (column.ToLower() == "id")
                return string.Format("{0}Id", className);


            if (column.ToString().ToLower().StartsWith("id"))
            {
                var keyname = column.ToString().Replace("Id", "");
                return string.Format("{0}Id", keyname);
            }

            return column;
        }

        public void SetCamelCasingExceptions(IEnumerable<string> list)
        {
            this._camelCasingExceptions = list;
        }
        public void AddCamelCasingExceptions(string value)
        {
            var newList = new List<string>();
            var defaultList = this._camelCasingExceptions;
            newList.AddRange(defaultList);
            newList.Add(value);
            this._camelCasingExceptions = newList;
        }

        protected virtual string ParametersRequired(IEnumerable<Info> infos)
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

        protected virtual string ConstructorParametersRequiredBase(IEnumerable<Info> infos)
        {
            var parameters = this.ParametersRequired(infos);

            if (parameters.IsNotNullOrEmpty())
            {
                return @"public <#className#>Base(<#parametersRequired#>) 
        {
<#initParametersRequired#>
        }";
            }

            return string.Empty;

        }

        protected virtual string ConstructorParametersRequired(IEnumerable<Info> infos)
        {
            var parameters = this.ParametersRequired(infos);

            if (parameters.IsNotNullOrEmpty())
                return @" public <#className#>(<#parametersRequired#>) : base(<#parametersRequiredToBase#>) { }";

            return string.Empty;

        }


        protected virtual string GenericTagsTransformer(TableInfo tableInfo, Context configContext, string classBuilder, EOperation operation = EOperation.Undefined)
        {
            if (tableInfo.IsNull())
                throw new InvalidOperationException("cade o tableInfo");

            classBuilder = GenericTagsTransformerClass(configContext, tableInfo.ClassName, classBuilder);
            classBuilder = GenericTagsTransformerTableinfo(tableInfo, configContext, classBuilder, operation);

            return classBuilder;
        }

        protected virtual string GenericTagsTransformerClass(Context configContext, string className, string classBuilder)
        {
            classBuilder = classBuilder.Replace("<#authGuard#>", configContext.UseRouteGuardInFront ? " canActivate: [AuthGuard]," : string.Empty);
            classBuilder = classBuilder.Replace("<#namespaceRoot#>", configContext.NamespaceRoot);
            classBuilder = classBuilder.Replace("<#namespace#>", configContext.Namespace);
            classBuilder = classBuilder.Replace("<#domainSource#>", configContext.DomainSource);
            classBuilder = classBuilder.Replace("<#namespaceDomainSource#>", configContext.NamespaceDomainSource);
            classBuilder = classBuilder.Replace("<#module#>", configContext.Module);
            classBuilder = classBuilder.Replace("<#contextName#>", configContext.ContextName);
            classBuilder = classBuilder.Replace("<#contextNameLower#>", configContext.ContextName.ToLower());
            classBuilder = classBuilder.Replace("<#company#>", configContext.Company);
            classBuilder = classBuilder.Replace("<#className#>", className);
            classBuilder = classBuilder.Replace("<#classNameInstance#>", CamelCaseTransform(className));
            classBuilder = classBuilder.Replace("<#classNameLowerAndSeparator#>", ClassNameLowerAndSeparator(className));
            classBuilder = classBuilder.Replace("<#classNameLower#>", className.ToLowerCase());
            classBuilder = classBuilder.Replace("<#tab#>", Tabs.TabProp());
            classBuilder = classBuilder.Replace("<#groupComponentModules#>", MakeGroupModules(configContext, className));
            classBuilder = classBuilder.Replace("<#groupComponentModulesImport#>", MakeGroupModulesImport(configContext, className));
            classBuilder = classBuilder.Replace("<#apiRetrhow#>", ApiRetrhow(configContext));

            return classBuilder;
        }

        protected string ApiRetrhow(Context configContext)
        {
            return configContext.ApiRetrhow ? "throw new ExceptionRetrhow(responseEx, ex.Message, ex);" : "return responseEx;";
        }

        private static string MakeGroupModules(Context configContext, string className)
        {
            var groups = new UniqueListString();

            var _groups = configContext.TableInfo
                .Where(_ => _.ClassName == className)
                .Where(_ => _.GroupComponent != null)
                .SelectMany(_ => _.GroupComponent
                    .Where(__ => __.ComponentModule.ToLower() != className.ToLower())
                        .Select(__ => string.Format("        {0}Module,", __.ComponentModule)));


            var _groupsInfoFields = configContext.TableInfo
                .Where(_ => _.ClassName == className)
                .Where(_ => _.FieldsConfig != null)
                .SelectMany(_ => _.FieldsConfig
                    .Where(__ => __.GroupComponents.IsAny())
                    .SelectMany(__ => __.GroupComponents
                        .Where(___ => ___.ComponentModule.ToLower() != className.ToLower())
                            .Select(___ => string.Format("        {0}Module,", ___.ComponentModule))));

            groups.AddRange(_groupsInfoFields.Where(_ => _ != "        Module,"));
            groups.AddRange(_groups.Where(_ => _ != "        Module,"));

            if (groups.IsNotAny())
                return string.Empty;

            return string.Join(System.Environment.NewLine, groups);
        }

        private static string MakeGroupModulesImport(Context configContext, string className)
        {
            var groups = new UniqueListString();

            var _groups = configContext.TableInfo
                .Where(_ => _.ClassName == className)
                .Where(_ => _.GroupComponent != null)
                .SelectMany(_ => _.GroupComponent
                    .Where(___ => ___.ComponentModule.ToLower() != className.ToLower())
                        .Select(__ => string.Format("{0}", __.ComponentModule)));

            var _groupsInfoFields = configContext.TableInfo
                .Where(_ => _.ClassName == className)
                .Where(_ => _.FieldsConfig != null)
                .SelectMany(_ => _.FieldsConfig
                    .Where(__ => __.GroupComponents.IsAny())
                    .SelectMany(__ => __.GroupComponents
                        .Where(___ => ___.ComponentModule.ToLower() != className.ToLower())
                            .Select(___ => string.Format("{0}", ___.ComponentModule))));


            groups.AddRange(_groupsInfoFields.Where(_ => !string.IsNullOrEmpty(_)));
            groups.AddRange(_groups.Where(_ => !string.IsNullOrEmpty(_)));


            if (groups.IsNotAny())
                return string.Empty;

            return string.Join(System.Environment.NewLine, groups
                .Select(_ => string.Format("import {0} from '../{1}/{1}.module'", "{ " + _ + "Module}", _.ToLower())));
        }

        protected virtual string GenericTagsTransformerTableinfo(TableInfo tableInfo, Context configContext, string classBuilder, EOperation operation)
        {
            var IDomain = tableInfo.MakeCrud ? "IDomainCrud" : "IDomain";

            classBuilder = classBuilder.Replace("<#setId#>", tableInfo.Keys.IsAny() ? "if (id.IsSent()) dto.<#KeyName#> = id;" : string.Empty);
            classBuilder = classBuilder.Replace("<#filterIds#>", tableInfo.Keys.IsAny() && tableInfo.KeysTypes.Where(_ => _ != "int").IsNotAny() ? "if (filters.Ids.IsSent()) queryFilter = queryFilter.Where(_ => filters.GetIds().Contains(_.<#KeyName#>));" : string.Empty);
            classBuilder = classBuilder.Replace("<#KeyName#>", tableInfo.KeyName);
            classBuilder = classBuilder.Replace("<#IDomain#>", IDomain);
            classBuilder = classBuilder.Replace("<#inheritClassName#>", tableInfo.InheritClassName);
            classBuilder = classBuilder.Replace("<#classNameFormated#>", tableInfo.ClassNameFormated ?? tableInfo.ClassName);
            classBuilder = classBuilder.Replace("<#boundedContext#>", tableInfo.BoundedContext);
            classBuilder = classBuilder.Replace("<#KeyNameCamelCase#>", CamelCaseTransform(tableInfo.KeyName));
            classBuilder = classBuilder.Replace("<#KeyType#>", tableInfo.KeysTypes.IsAny() ? tableInfo.KeysTypes.FirstOrDefault() : "int");
            classBuilder = classBuilder.Replace("<#KeyNames#>", MakeKeyNames(tableInfo, operation));
            classBuilder = classBuilder.Replace("<#ParametersKeyNames#>", ParametersKeyNames(tableInfo, true, operation, "item", this.CamelCaseTransform));
            classBuilder = classBuilder.Replace("<#ParametersKeyNamesModel#>", ParametersKeyNames(tableInfo, true, operation, "model", this.CamelCaseTransform));
            classBuilder = classBuilder.Replace("<#ExpressionKeyNames#>", ExpressionKeyNames(tableInfo, true, operation));
            classBuilder = classBuilder.Replace("<#tablename#>", tableInfo.TableName);
            classBuilder = classBuilder.Replace("<#WhereSingle#>", MakeKeysFromGet(tableInfo));
            classBuilder = classBuilder.Replace("<#WhereSingleFilter#>", MakeKeysFromGet(tableInfo, "filters"));
            classBuilder = classBuilder.Replace("<#DataItemFieldName#>", tableInfo.DataItemFieldName);
            classBuilder = classBuilder.Replace("<#DataItemFieldId#>", tableInfo.Keys.IsAny() ? tableInfo.KeyName : tableInfo.DataItemFieldName);
            classBuilder = classBuilder.Replace("<#orderByKeys#>", OrderyByKeys(tableInfo));
            classBuilder = classBuilder.Replace("<#toolName#>", ToolName(tableInfo));
            classBuilder = classBuilder.Replace("<#setUserCreate#>", tableInfo.IsAuditField ? "" : "");
            classBuilder = classBuilder.Replace("<#setUserCreateOld#>", tableInfo.IsAuditField ? "" : "");
            classBuilder = classBuilder.Replace("<#setUserUpdate#>", tableInfo.IsAuditField ? "" : "");

            classBuilder = MakeReletedNamespace(tableInfo, configContext, classBuilder);

            return classBuilder;
        }

        public string GenericTagsTransformerRoutes(Context configContext, string classBuilder)
        {
            var routeItem = new List<string>();
            if (configContext.Routes.IsAny())
            {
                foreach (var item in configContext.Routes)
                {
                    var route = item.Route;
                    routeItem.Add(string.Format("            {0}", route));
                }
            }

            classBuilder = classBuilder.Replace("<#classCustomItems#>", string.Join(string.Format(",{0}", System.Environment.NewLine), routeItem));
            return classBuilder;
        }


        protected virtual string GenericTagsTransformerInfo(string propertyName, Context configContext, TableInfo tableInfo, Info info, string classBuilder, EOperation operation)
        {
            var viewinlist = !HelperRestrictions.RunRestrictions(tableInfo.FieldsConfigShow, tableInfo, info, propertyName, EOperation.Front_angular_Grid);

            if (IsPropertyNavigationTypeInstance(tableInfo, info.PropertyName) && HideKeyForGrid(configContext, info))
                viewinlist = false;

            if (IsVarcharMax(info) || IsStringLengthBig(info, configContext))
                viewinlist = false;

            if (HideKeyForGrid(configContext, info))
                viewinlist = false;

            var type = info.Type;
            var aux = string.Empty;

            if (operation == EOperation.Front_angular_Service_Fields)
            {
                var dataitem = HelperFieldConfig.FieldDataItem(tableInfo, propertyName);
                if (dataitem.IsAny())
                {
                    var jsonAux = this.DataitemAux(dataitem);
                    aux = string.Format(", aux : {0}", jsonAux);
                    type = "dataitem";
                }

                var password = HelperFieldConfig.IsPassword(tableInfo, propertyName);
                var confPassword = HelperFieldConfig.IsPasswordConfirmation(tableInfo, propertyName);
                if (password || confPassword)
                {
                    aux = string.Format(", aux : '******'");
                    type = "changevalue";
                }
            }

            if (operation == EOperation.Front_angular_FieldDetails)
            {
                if (IsPropertyNavigationTypeInstance(tableInfo, propertyName) && !tableInfo.IsCompositeKey)
                {
                    aux = string.Format(" [instance]=\"'{0}'\"  [key]=\"'{1}'\"", PropertyNavigationTypeInstance(tableInfo, propertyName), PropertyInstanceKey(tableInfo, propertyName));
                    type = "instance";
                }

                var dataitem = HelperFieldConfig.FieldDataItem(tableInfo, propertyName);
                if (dataitem.IsAny())
                {
                    aux = string.Format("[aux]=\"vm.infos.{0}.aux\"", propertyName);
                    type = "dataitem";
                }

                var password = HelperFieldConfig.IsPassword(tableInfo, propertyName);
                var confPassword = HelperFieldConfig.IsPasswordConfirmation(tableInfo, propertyName);
                if (password || confPassword)
                {
                    aux = string.Format("[aux]=\"vm.infos.{0}.aux\"", propertyName);
                    type = "changevalue";
                }

                var IsTextEditor = HelperFieldConfig.IsTextEditor(tableInfo, propertyName);
                if (IsTextEditor)
                {
                    type = "html";
                }

                var IsTextTag = HelperFieldConfig.IsTextTag(tableInfo, propertyName);
                if (IsTextTag)
                {
                    type = "tag";
                }

            }


            classBuilder = classBuilder.Replace("<#propertyName#>", propertyName);
            classBuilder = classBuilder.Replace("<#propertyNameFromDictionary#>", ChangePropertyNameFromDictionary(propertyName, configContext));
            classBuilder = classBuilder.Replace("<#type#>", type);
            classBuilder = classBuilder.Replace("<#aux#>", aux);
            classBuilder = classBuilder.Replace("<#navigationprop#>", !string.IsNullOrEmpty(PropertyNavigationTypeInstance(tableInfo, info.PropertyName)) ? "navigationProp:'" + PropertyNavigationTypeInstance(tableInfo, info.PropertyName) + "'" : string.Empty);
            classBuilder = classBuilder.Replace("<#ReletedClass#>", PropertyNavigationTypeInstance(tableInfo, info.PropertyName));

            classBuilder = classBuilder.Replace("<#typeTS#>", convertTypeToTypeTS(info.Type));
            classBuilder = classBuilder.Replace("<#isKey#>", info.IsKey == 1 ? "true" : "false");
            classBuilder = classBuilder.Replace("<#classNameInstance#>", CamelCaseTransform(info.ClassName));
            classBuilder = classBuilder.Replace("<#viewInList#>", viewinlist ? "true" : "false");

            return classBuilder;
        }

        private static bool HideKeyForForms(ConfigExecutetemplate configExecutetemplate, Info info)
        {
            return info.IsKey == 1 && !configExecutetemplate.ConfigContext.ShowKeysInForms && !configExecutetemplate.ConfigContext.ShowKeysInFront && !info.ShowFieldIsKey;
        }

        private static bool HideKeyForGrid(Context configContext, Info info)
        {
            return info.IsKey == 1 && !configContext.ShowKeysInGrid && !configContext.ShowKeysInFront && !info.ShowFieldIsKey;
        }

        private string ChangePropertyNameFromDictionary(string propertyName, Context configContext)
        {
            if (!configContext.DictionaryFields.IsAny())
                return propertyName;

            var dic = configContext.DictionaryFields.Where(_ => _.Key.ToUpperCase() == propertyName.ToUpperCase()).SingleOrDefault();
            if (dic.IsNotNull() && dic.Value.IsNotNull())
                return dic.Value;

            return propertyName;
        }

        protected string ClassNameLowerAndSeparator(string className)
        {
            return HelperSysObjectUtil.ClassNameLowerAndSeparator(className);
        }

        protected string BuilderPropertys(IEnumerable<Info> infos, string textTemplatePropertys, Context configContext)
        {
            var classBuilderPropertys = new List<string>();
            if (infos.IsAny())
            {
                foreach (var item in infos)
                {
                    if (Audit.IsAuditField(item.PropertyName))
                        continue;

                    var itempropert = textTemplatePropertys
                        .Replace("<#type#>", item.Type)
                        .Replace("<#propertyName#>", item.PropertyName)
                        .Replace("<#propertyNameFromDictionary#>", ChangePropertyNameFromDictionary(item.PropertyName, configContext));
                    classBuilderPropertys.Add(string.Format("{0}{1}", Tabs.TabModels(), itempropert));

                }
            }

            return string.Join(System.Environment.NewLine, classBuilderPropertys);
        }

        protected string BuilderSampleFilters(IEnumerable<Info> infos, string textTemplateFilters, string classBuilderFilters, Context configContext)
        {
            if (infos.IsAny())
            {
                foreach (var item in infos)
                {
                    var itemFilters = string.Empty;

                    if (item.Type == "string")
                    {
                        itemFilters = textTemplateFilters.Replace("<#propertyName#>", item.PropertyName);
                        itemFilters = itemFilters.Replace("<#condition#>", string.Format("_=>_.{0}.Contains(filters.{0})", item.PropertyName));
                        itemFilters = itemFilters.Replace("<#filtersRange#>", string.Empty);
                    }
                    else if (item.Type == "DateTime")
                    {

                        var itemFiltersBasic = textTemplateFilters.Replace("<#propertyName#>", String.Format("{0}", item.PropertyName));
                        if (item.DateTimeComparation == "==")
                            itemFiltersBasic = itemFiltersBasic.Replace("<#condition#>", string.Format(@"_=> _.{0} >= filters.{0}.AddHours(-filters.{0}.Hour).AddMinutes(-filters.{0}.Minute).AddSeconds(-filters.{0}.Second) && _.{0} <= filters.{0}.AddDays(1).AddHours(-filters.{0}.Hour).AddMinutes(-filters.{0}.Minute).AddSeconds(-filters.{0}.Second)", item.PropertyName, item.DateTimeComparation));
                        else
                            itemFiltersBasic = itemFiltersBasic.Replace("<#condition#>", string.Format("_=>_.{0} {1} filters.{0}", item.PropertyName, item.DateTimeComparation));
                        itemFiltersBasic = itemFiltersBasic.Replace("<#filtersRange#>", string.Empty);

                        var itemFiltersStart = textTemplateFilters.Replace("<#propertyName#>", String.Format("{0}Start", item.PropertyName));
                        itemFiltersStart = itemFiltersStart.Replace("<#condition#>", string.Format("_=>_.{0} >= filters.{0}Start ", item.PropertyName));
                        itemFiltersStart = itemFiltersStart.Replace("<#filtersRange#>", string.Empty);

                        var itemFiltersEnd = textTemplateFilters.Replace("<#propertyName#>", String.Format("{0}End", item.PropertyName));
                        itemFiltersEnd = itemFiltersEnd.Replace("<#condition#>", string.Format("_=>_.{0}  <= filters.{0}End", item.PropertyName));
                        itemFiltersEnd = itemFiltersEnd.Replace("<#filtersRange#>", string.Format("filters.{0}End = filters.{0}End.AddDays(1).AddMilliseconds(-1);", item.PropertyName));

                        itemFilters = String.Format("{0}{1}{2}{3}{4}{5}{6}{7}", itemFiltersBasic, System.Environment.NewLine, Tabs.TabSets(), itemFiltersStart, System.Environment.NewLine, Tabs.TabSets(), itemFiltersEnd, System.Environment.NewLine);

                    }
                    else if (item.Type == "DateTime?")
                    {
                        var itemFiltersBasic = textTemplateFilters.Replace("<#propertyName#>", String.Format("{0}", item.PropertyName));
                        if (item.DateTimeComparation == "==")
                            itemFiltersBasic = itemFiltersBasic.Replace("<#condition#>", string.Format(@"_=> _.{0} != null && _.{0} >= filters.{0}.Value.AddHours(-filters.{0}.Value.Hour).AddMinutes(-filters.{0}.Value.Minute).AddSeconds(-filters.{0}.Value.Second) && _.{0} <= filters.{0}.Value.AddDays(1).AddHours(-filters.{0}.Value.Hour).AddMinutes(-filters.{0}.Value.Minute).AddSeconds(-filters.{0}.Value.Second)", item.PropertyName, item.DateTimeComparation));
                        else
                            itemFiltersBasic = itemFiltersBasic.Replace("<#condition#>", string.Format("_=>_.{0} != null && _.{0}.Value {1} filters.{0}.Value", item.PropertyName, item.DateTimeComparation));
                        itemFiltersBasic = itemFiltersBasic.Replace("<#filtersRange#>", string.Empty);


                        var itemFiltersStart = textTemplateFilters.Replace("<#propertyName#>", String.Format("{0}Start", item.PropertyName));
                        itemFiltersStart = itemFiltersStart.Replace("<#condition#>", string.Format("_=>_.{0} != null && _.{0}.Value >= filters.{0}Start.Value", item.PropertyName));
                        itemFiltersStart = itemFiltersStart.Replace("<#filtersRange#>", string.Empty);

                        var itemFiltersEnd = textTemplateFilters.Replace("<#propertyName#>", String.Format("{0}End", item.PropertyName));
                        itemFiltersEnd = itemFiltersEnd.Replace("<#condition#>", string.Format("_=>_.{0} != null &&  _.{0}.Value <= filters.{0}End", item.PropertyName));
                        itemFiltersEnd = itemFiltersEnd.Replace("<#filtersRange#>", string.Format("filters.{0}End = filters.{0}End.Value.AddDays(1).AddMilliseconds(-1);", item.PropertyName));

                        itemFilters = String.Format("{0}{1}{2}{3}{4}{5}{6}{7}", itemFiltersBasic, System.Environment.NewLine, Tabs.TabSets(), itemFiltersStart, System.Environment.NewLine, Tabs.TabSets(), itemFiltersEnd, System.Environment.NewLine);

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

                    itemFilters = itemFilters.Replace("<#propertyNameFromDictionary#>", ChangePropertyNameFromDictionary(item.PropertyName, configContext)); ;

                    classBuilderFilters += string.Format("{0}{1}{2}", Tabs.TabSets(), itemFilters, System.Environment.NewLine);

                }
            }

            return classBuilderFilters;
        }

        protected string ToolName(TableInfo tableInfo)
        {
            return HelperSysObjectUtil.ToolName(tableInfo);
        }

        protected virtual string MakeKeysFromGet(TableInfo tableInfo, string classFilter = "model")
        {
            return HelperSysObjectUtil.MakeKeysFromGet(tableInfo, classFilter);
        }

        protected virtual string OrderyByKeys(TableInfo tableInfo, string classFilter = "model")
        {
            return HelperSysObjectUtil.OrderyByKeys(tableInfo, classFilter);
        }
        protected string MakeClassBuilderMapORM(TableInfo tableInfo, Context configContext, IEnumerable<Info> infos, string textTemplateClass, string textTemplateLength, string textTemplateRequired, string textTemplateMapper, string textTemplateManyToMany, string textTemplateCompositeKey, ref string classBuilderitemTemplateLength, ref string classBuilderitemTemplateRequired, ref string classBuilderitemplateMapper, ref string classBuilderitemplateMapperKey, ref string classBuilderitemplateManyToMany, ref string classBuilderitemplateCompositeKey)
        {
            var classBuilder = GenericTagsTransformer(tableInfo, configContext, textTemplateClass);

            classBuilderitemplateCompositeKey = MakeKey(infos, textTemplateCompositeKey, classBuilderitemplateCompositeKey);

            if (infos.IsAny())
            {

                foreach (var item in infos)
                {

                    if (IsString(item) && IsNotVarcharMax(item))
                    {
                        var itemTemplateLength = textTemplateLength
                            .Replace("<#propertyName#>", item.PropertyName)
                            .Replace("<#length#>", item.Length);

                        classBuilderitemTemplateLength += string.Format("{0}{1}{2}", Tabs.TabMaps(), itemTemplateLength, System.Environment.NewLine);
                    }

                    if (item.IsNullable == 0)
                    {
                        var itemTemplateRequired = textTemplateRequired
                           .Replace("<#propertyName#>", item.PropertyName);

                        classBuilderitemTemplateRequired += string.Format("{0}{1}{2}", Tabs.TabMaps(), itemTemplateRequired, System.Environment.NewLine);
                    }

                    if (item.IsKey == 1)
                    {
                        var itemplateMapper = textTemplateMapper
                            .Replace("<#propertyName#>", item.PropertyName)
                            .Replace("<#columnName#>", item.ColumnName);

                        classBuilderitemplateMapperKey += string.Format("{0}{1}{2}", Tabs.TabMaps(), itemplateMapper, System.Environment.NewLine);

                    }
                    else
                    {



                        var itemplateMapper = textTemplateMapper
                           .Replace("<#propertyName#>", item.PropertyName)
                           .Replace("<#columnName#>", item.ColumnName);

                        if (item.Type == "string")
                        {
                            var hasCoColumnType = string.Empty;
                            if (configContext.Arquiteture == ArquitetureType.TableModel)
                            {
                                if (item.Length != "-1")
                                    hasCoColumnType = string.Format(".HasColumnType(\"{0}\").HasMaxLength({1})", item.TypeOriginal, item.Length);
                            }
                            else if (configContext.Arquiteture == ArquitetureType.DDD)
                            {
                                hasCoColumnType = string.Format(".HasColumnType(\"{0}({1})\")", item.TypeOriginal, item.Length);
                                if (item.Length == "-1")
                                    hasCoColumnType = string.Format(".HasColumnType(\"{0}(max)\")", item.TypeOriginal);
                            }
                            else if (configContext.Arquiteture == ArquitetureType.ReadOnly)
                            {
                                hasCoColumnType = string.Format(".HasColumnType(\"{0}({1})\")", item.TypeOriginal, item.Length);
                                if (item.Length == "-1")
                                    hasCoColumnType = string.Format(".HasColumnType(\"{0}(max)\")", item.TypeOriginal);
                            }

                            itemplateMapper = itemplateMapper.Replace(";", string.Empty);
                            itemplateMapper = itemplateMapper + hasCoColumnType + ";";
                        }


                        classBuilderitemplateMapper += string.Format("{0}{1}{2}", Tabs.TabMaps(), itemplateMapper, System.Environment.NewLine);
                    }

                }

            }

            if (!string.IsNullOrEmpty(tableInfo.TableHelper))
            {

                var itemTemplateManyToMany = textTemplateManyToMany
                      .Replace("<#propertyNavigationLeft#>", tableInfo.ClassNameRigth)
                      .Replace("<#propertyNavigationRigth#>", tableInfo.ClassName)
                      .Replace("<#MapLeftKey#>", tableInfo.LeftKey)
                      .Replace("<#MapRightKey#>", tableInfo.RightKey)
                      .Replace("<#TableHelper#>", tableInfo.TableHelper);

                classBuilderitemplateManyToMany += string.Format("{0}{1}{2}", Tabs.TabMaps(), itemTemplateManyToMany, System.Environment.NewLine);

            }

            classBuilder = classBuilder.Replace("<#IsRequired#>", classBuilderitemTemplateRequired);
            classBuilder = classBuilder.Replace("<#HasMaxLength#>", classBuilderitemTemplateLength);
            classBuilder = classBuilder.Replace("<#Mapper#>", classBuilderitemplateMapper);
            classBuilder = classBuilder.Replace("<#keyName#>", classBuilderitemplateMapperKey);
            classBuilder = classBuilder.Replace("<#ManyToMany#>", classBuilderitemplateManyToMany);
            classBuilder = classBuilder.Replace("<#CompositeKey#>", classBuilderitemplateCompositeKey);
            return classBuilder;
        }
        protected bool IsRequired(Info item)
        {
            return item.IsNullable == 0;
        }


        protected bool IsPropertyNavigationTypeInstance(TableInfo tableInfo, string propertyName)
        {
            var classBuilderNavPropertys = string.Empty;
            if (tableInfo.ReletedClass.IsNotNull())
            {
                foreach (var item in tableInfo.ReletedClass)
                {
                    if (item.NavigationType == NavigationType.Instance && item.PropertyNameFk.ToLower() == propertyName.ToLower())
                        return true;
                }
            }

            return false;
        }

        protected string PropertyNavigationTypeInstance(TableInfo tableInfo, string propertyName)
        {
            var classBuilderNavPropertys = string.Empty;
            if (tableInfo.ReletedClass.IsNotNull())
            {
                foreach (var item in tableInfo.ReletedClass)
                {
                    if (item.NavigationType == NavigationType.Instance && item.PropertyNameFk.ToLower() == propertyName.ToLower())
                        return item.ClassName;
                }
            }

            return string.Empty;
        }

        protected string PropertyInstanceFieldFilterDefault(TableInfo tableInfo, string propertyName)
        {
            var classBuilderNavPropertys = string.Empty;
            if (tableInfo.ReletedClass.IsNotNull())
            {
                foreach (var item in tableInfo.ReletedClass)
                {
                    if (item.NavigationType == NavigationType.Instance && item.PropertyNameFk.ToLower() == propertyName.ToLower())
                        return item.FieldFilterDefault;
                }
            }

            return string.Empty;
        }


        protected bool IsPropertyNavigationTypeCollection(TableInfo tableInfo, string propertyName)
        {
            var classBuilderNavPropertys = string.Empty;
            if (tableInfo.ReletedClass.IsNotNull())
            {
                foreach (var item in tableInfo.ReletedClass)
                {
                    if (item.NavigationType == NavigationType.Collettion && item.PropertyNameFk.ToLower() != item.PropertyNamePk.ToLower())
                        return true;
                }
            }

            return false;
        }

        protected string PropertyNavigationTypeCollection(TableInfo tableInfo, string propertyName)
        {
            var classBuilderNavPropertys = string.Empty;
            if (tableInfo.ReletedClass.IsNotNull())
            {
                foreach (var item in tableInfo.ReletedClass)
                {
                    if (item.NavigationType == NavigationType.Collettion && item.PropertyNameFk.ToLower() != item.PropertyNamePk.ToLower())
                        return item.ClassName;
                }
            }

            return string.Empty;
        }

        protected string PropertyCollectionFieldFilterDefault(TableInfo tableInfo, string propertyName)
        {
            var classBuilderNavPropertys = string.Empty;
            if (tableInfo.ReletedClass.IsNotNull())
            {
                foreach (var item in tableInfo.ReletedClass)
                {
                    if (item.NavigationType == NavigationType.Collettion && item.PropertyNameFk.ToLower() != item.PropertyNamePk.ToLower())
                        return item.FieldFilterDefault;
                }
            }

            return string.Empty;
        }



        protected string PropertyInstanceKey(TableInfo tableInfo, string propertyName)
        {
            var classBuilderNavPropertys = string.Empty;
            if (tableInfo.ReletedClass.IsNotNull())
            {
                foreach (var item in tableInfo.ReletedClass)
                {
                    if (item.NavigationType == NavigationType.Instance && item.PropertyNameFk.ToLower() == propertyName.ToLower())
                        return item.PropertyNameFk;
                }
            }

            return null;
        }

        protected string MakePropertyNavigationDto(TableInfo tableInfo, Context configContext, string TextTemplateNavPropertysCollection, string TextTemplateNavPropertysInstace, string classBuilder)
        {
            var classBuilderNavPropertys = string.Empty;

            if (tableInfo.ReletedClass.IsNotNull())
            {
                foreach (var item in tableInfo.ReletedClass)
                {

                    var TextTemplateNavPropertys = item.NavigationType == NavigationType.Instance ? TextTemplateNavPropertysInstace : TextTemplateNavPropertysCollection;


                    if (item.ClassName == tableInfo.ClassName)
                        classBuilderNavPropertys += String.Format("{0}{1}", Tabs.TabModels(), TextTemplateNavPropertys
                            .Replace("<#className#>", item.ClassName)
                            .Replace("<#classNameNav#>", String.Format("{0}Self", item.ClassName)));

                    if (item.ClassName != tableInfo.ClassName)
                        classBuilderNavPropertys += String.Format("{0}{1}", Tabs.TabModels(), TextTemplateNavPropertys
                            .Replace("<#className#>", item.ClassName)
                            .Replace("<#classNameNav#>", item.ClassName));


                }
            }

            classBuilder = classBuilder
                .Replace("<#propertysNav#>", classBuilderNavPropertys);

            return classBuilder;
        }


        protected string ClearPropertyNavigationDto(TableInfo tableInfo, Context configContext, string TextTemplateNavPropertysCollection, string TextTemplateNavPropertysInstace, string classBuilder)
        {
            var classBuilderNavPropertys = string.Empty;
            classBuilder = classBuilder
                .Replace("<#propertysNav#>", classBuilderNavPropertys);

            return classBuilder;
        }
        protected string MakePropertyNavigationModels(TableInfo tableInfo, Context configContext, string TextTemplateNavPropertysCollection, string TextTemplateNavPropertysInstace, string classBuilder)
        {
            if (!configContext.MakeNavigationPropertys)
                return classBuilder
                .Replace("<#propertysNav#>", string.Empty);

            var classBuilderNavPropertys = string.Empty;
            var usingPropertysNav = string.Empty;

            foreach (var item in tableInfo.ReletedClass)
            {
                var TextTemplateNavPropertys = item.NavigationType == NavigationType.Instance ? TextTemplateNavPropertysInstace : TextTemplateNavPropertysCollection;

                if (item.ClassName == tableInfo.ClassName)
                    classBuilderNavPropertys += String.Format("{0}{1}", Tabs.TabModels(), TextTemplateNavPropertys
                        .Replace("<#className#>", item.ClassName)
                        .Replace("<#classNameNav#>", String.Format("{0}Self", item.ClassName)));

                if (item.ClassName != tableInfo.ClassName)
                    classBuilderNavPropertys += String.Format("{0}{1}", Tabs.TabModels(), TextTemplateNavPropertys
                        .Replace("<#className#>", item.ClassName)
                        .Replace("<#classNameNav#>", item.ClassName));


                if (item.Module != configContext.Module)
                {
                    var usingReference = String.Format("using {0}.Domain;{1}", item.Namespace, System.Environment.NewLine);
                    if (!usingPropertysNav.Contains(usingReference))
                        usingPropertysNav += usingReference;
                }

            }

            classBuilder = classBuilder
                .Replace("<#propertysNav#>", classBuilderNavPropertys);

            return classBuilder;

        }
        protected string MakeFilterDateRange(string TextTemplatePropertys, string classBuilderPropertys, Info item)
        {
            if (item.Type == "DateTime" || item.Type == "DateTime?")
            {
                classBuilderPropertys = AddPropertyFilter(TextTemplatePropertys, classBuilderPropertys, item, string.Format("{0}Start", item.PropertyName), item.Type);
                classBuilderPropertys = AddPropertyFilter(TextTemplatePropertys, classBuilderPropertys, item, string.Format("{0}End", item.PropertyName), item.Type);
            }
            return classBuilderPropertys;
        }
        protected string AddPropertyFilter(string TextTemplatePropertys, string classBuilderPropertys, Info item, string propertyName, string type)
        {
            var itempropert = TextTemplatePropertys.
                    Replace("<#type#>", type).
                    Replace("<#propertyName#>", propertyName);

            classBuilderPropertys += string.Format("{0}{1}{2}", Tabs.TabModels(), itempropert, System.Environment.NewLine);
            return classBuilderPropertys;
        }
        protected virtual string MakeValidationsAttributes(string TextTemplateValidation, string TextTemplateValidationLength, string TextTemplateValidationRequired, string classBuilderitemTemplateValidation, Info item, Context configContext)
        {
            if (IsRequired(item) && IsString(item) && IsNotVarcharMax(item))
            {
                var itemTemplateValidationLegth = TextTemplateValidationLength
                    .Replace("<#Length#>", item.Length)
                    .Replace("<#className#>", item.ClassName)
                    .Replace("<#propertyName#>", item.PropertyName)
                    .Replace("<#propertyNameFromDictionary#>", ChangePropertyNameFromDictionary(item.PropertyName, configContext));

                var itemTemplateValidationRequired = TextTemplateValidationRequired
                   .Replace("<#propertyName#>", item.PropertyName)
                   .Replace("<#className#>", item.ClassName)
                   .Replace("<#propertyNameFromDictionary#>", ChangePropertyNameFromDictionary(item.PropertyName, configContext));

                var itemTemplateValidation = TextTemplateValidation.Replace("<#propertyName#>", item.PropertyName).Replace("<#type#>", item.Type).Replace("<#tab#>", Tabs.TabModels());
                itemTemplateValidation = itemTemplateValidation.Replace("<#RequiredValidation#>", Tabs.TabModels(itemTemplateValidationRequired));
                itemTemplateValidation = itemTemplateValidation.Replace("<#MaxLengthValidation#>", Tabs.TabModels(itemTemplateValidationLegth));
                itemTemplateValidation = itemTemplateValidation.Replace("<#propertyNameFromDictionary#>", ChangePropertyNameFromDictionary(item.PropertyName, configContext));
                itemTemplateValidation = itemTemplateValidation.Replace("<#MaxLengthValidation#>", Tabs.TabModels(itemTemplateValidationLegth));

                classBuilderitemTemplateValidation += string.Format("{0}{1}", itemTemplateValidation, System.Environment.NewLine);
                return classBuilderitemTemplateValidation;
            }
            else if (IsRequired(item) && IsString(item) && IsVarcharMax(item))
            {

                var itemTemplateValidationRequired = TextTemplateValidationRequired
                   .Replace("<#propertyName#>", item.PropertyName)
                   .Replace("<#className#>", item.ClassName)
                   .Replace("<#propertyNameFromDictionary#>", ChangePropertyNameFromDictionary(item.PropertyName, configContext));

                var itemTemplateValidation = TextTemplateValidation.Replace("<#propertyName#>", item.PropertyName).Replace("<#type#>", item.Type).Replace("<#tab#>", Tabs.TabModels());
                itemTemplateValidation = itemTemplateValidation.Replace("<#RequiredValidation#>", Tabs.TabModels(itemTemplateValidationRequired));
                itemTemplateValidation = itemTemplateValidation.Replace("<#propertyNameFromDictionary#>", ChangePropertyNameFromDictionary(item.PropertyName, configContext));
                itemTemplateValidation = RemoveLine(itemTemplateValidation, "<#MaxLengthValidation#>");


                classBuilderitemTemplateValidation += string.Format("{0}{1}", itemTemplateValidation, System.Environment.NewLine);
                return classBuilderitemTemplateValidation;
            }

            else if (IsRequired(item) && IsNotString(item))
            {
                var itemTemplateValidationLegth = TextTemplateValidationLength
                    .Replace("<#Length#>", item.Length)
                    .Replace("<#propertyName#>", item.PropertyName)
                    .Replace("<#className#>", item.ClassName)
                    .Replace("<#propertyNameFromDictionary#>", ChangePropertyNameFromDictionary(item.PropertyName, configContext));


                var itemTemplateValidationRequired = TextTemplateValidationRequired
                   .Replace("<#propertyName#>", item.PropertyName)
                   .Replace("<#className#>", item.ClassName)
                   .Replace("<#propertyNameFromDictionary#>", ChangePropertyNameFromDictionary(item.PropertyName, configContext));

                var itemTemplateValidation = TextTemplateValidation.Replace("<#propertyName#>", item.PropertyName).Replace("<#type#>", item.Type).Replace("<#tab#>", Tabs.TabModels());
                itemTemplateValidation = itemTemplateValidation.Replace("<#RequiredValidation#>", Tabs.TabModels(itemTemplateValidationRequired));
                itemTemplateValidation = RemoveLine(itemTemplateValidation, "<#MaxLengthValidation#>");

                classBuilderitemTemplateValidation += string.Format("{0}{1}", itemTemplateValidation, System.Environment.NewLine);
                return classBuilderitemTemplateValidation;
            }

            else if (!IsRequired(item) && IsString(item) && IsNotVarcharMax(item))
            {
                var itemTemplateValidationLegth = TextTemplateValidationLength
                    .Replace("<#Length#>", item.Length)
                    .Replace("<#propertyName#>", item.PropertyName)
                    .Replace("<#className#>", item.ClassName);

                var itemTemplateValidation = TextTemplateValidation.Replace("<#propertyName#>", item.PropertyName).Replace("<#type#>", item.Type).Replace("<#tab#>", Tabs.TabModels());
                itemTemplateValidation = itemTemplateValidation.Replace("<#propertyNameFromDictionary#>", ChangePropertyNameFromDictionary(item.PropertyName, configContext));
                itemTemplateValidation = RemoveLine(itemTemplateValidation, "<#RequiredValidation#>");
                itemTemplateValidation = itemTemplateValidation.Replace("<#MaxLengthValidation#>", Tabs.TabModels(itemTemplateValidationLegth));

                classBuilderitemTemplateValidation += string.Format("{0}{1}", itemTemplateValidation, System.Environment.NewLine);
                return classBuilderitemTemplateValidation;
            }
            else
            {


                var itemTemplateValidation = TextTemplateValidation.Replace("<#propertyName#>", item.PropertyName).Replace("<#type#>", item.Type).Replace("<#tab#>", Tabs.TabModels());
                itemTemplateValidation = itemTemplateValidation.Replace("<#propertyNameFromDictionary#>", ChangePropertyNameFromDictionary(item.PropertyName, configContext));
                itemTemplateValidation = RemoveLine(itemTemplateValidation, "<#RequiredValidation#>");
                itemTemplateValidation = RemoveLine(itemTemplateValidation, "<#MaxLengthValidation#>");

                classBuilderitemTemplateValidation += string.Format("{0}{1}", itemTemplateValidation, System.Environment.NewLine);
                return classBuilderitemTemplateValidation;
            }

            return classBuilderitemTemplateValidation;
        }
        protected bool IsString(Info item)
        {
            return item.Type == "string";
        }
        protected bool IsNotVarcharMax(Info item)
        {
            return !IsVarcharMax(item);
        }
        protected bool IsVarcharMax(Info item)
        {
            return item.Length.Contains("-1");
        }
        protected bool IsStringLengthBig(Info info, Context configContext)
        {
            return Convert.ToInt32(info.Length) > configContext.LengthBigField || Convert.ToInt16(info.Length) == -1;
        }
        protected string DefineMoqMethd(string type)
        {

            switch (type.ToLower())
            {
                case "string":
                case "nchar":
                    return "MakeStringValueSuccess";
                case "int":
                case "int?":
                    return "MakeIntValueSuccess";
                case "decimal":
                case "decimal?":
                case "money":
                    return "MakeDecimalValueSuccess";
                case "float?":
                case "float":
                    return "MakeFloatValueSuccess";
                case "datetime":
                case "datetime?":
                    return "MakeDateTimeValueSuccess";
                case "bool":
                case "bool?":
                    return "MakeBoolValueSuccess";
                default:
                    break;
            }


            throw new InvalidOperationException("tipo não implementado");

        }
        protected string MakeReletedIntanceValues(TableInfo tableInfo, Context configContext, string TextTemplateReletedValues, string classBuilder)
        {
            var classBuilderReletedValues = string.Empty;

            foreach (var item in tableInfo.ReletedClass.Where(_ => _.NavigationType == NavigationType.Instance))
            {
                var itemvalue = TextTemplateReletedValues.
                       Replace("<#className#>", item.Table).
                       Replace("<#FKeyName#>", item.PropertyNameFk).
                       Replace("<#KeyName#>", item.PropertyNamePk);

                classBuilderReletedValues += string.Format("{0}{1}", itemvalue, System.Environment.NewLine);

            }

            classBuilder = classBuilder.Replace("<#reletedValues#>", classBuilderReletedValues);
            classBuilder = MakeReletedNamespace(tableInfo, configContext, classBuilder);

            return classBuilder;
        }
        protected string MakeKFilterByModel(TableInfo tableInfo)
        {
            var keys = string.Empty;
            if (tableInfo.Keys.IsAny())
            {
                foreach (var item in tableInfo.Keys)
                    keys += string.Format("{0} = first.{1},", item, item);
            }
            return keys;
        }


        protected void ExecuteTemplate(ConfigExecutetemplate config)
        {
            if (config.DisableGeneration)
                return;

            if (!config.OverrideFile)
            {
                var fileName = Path.GetFileName(config.PathOutput);
                if (File.Exists(config.PathOutput))
                {
                    return;
                }
            }

            if (config.TableInfo.CodeCustomImplemented)
                return;


            if (config.Layer == ELayer.Front && !config.TableInfo.MakeFront && !config.TableInfo.MakeFrontCrudBasic)
                return;


            if (config.Flow == EFlowTemplate.Static)
            {
                this.TemplateStaticFlow(config);
            }

            if (config.Flow == EFlowTemplate.Class)
            {
                if (!config.ConfigContext.RunOnlyThisClass)
                    this.TemplateClassFlow(config);
            }

            if (config.Flow == EFlowTemplate.Field)
            {
                this.TemplateFieldFlow(config);
            }

            if (config.Flow == EFlowTemplate.FieldType)
            {
                this.TemplateFieldTypesFlow(config);
            }

        }

        #region Flow


        protected void TemplateStaticFlow(ConfigExecutetemplate configExecutetemplate)
        {
            var classBuilder = this.TransformationTagsByClass(configExecutetemplate);
            using (var stream = new HelperStream(configExecutetemplate.PathOutput).GetInstance()) { stream.Write(classBuilder); }
        }

        protected void TemplateClassFlow(ConfigExecutetemplate configExecutetemplateClass)
        {
            var classBuilder = this.TransformationTagsByClass(new ConfigExecutetemplate
            {
                TableInfo = configExecutetemplateClass.TableInfo,
                ConfigContext = configExecutetemplateClass.ConfigContext,
                PathOutput = configExecutetemplateClass.PathOutput,
                Template = configExecutetemplateClass.Template,
            });

            foreach (var templateClass in configExecutetemplateClass.TemplateClassItem)
            {
                var classBuilderItems = new List<string>();

                var pathTemplateClassItem = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, this._defineTemplateFolder.Define(), templateClass.TemplateName);
                var textTemplateClassItem = Read.AllText(pathTemplateClassItem);

                foreach (var item in configExecutetemplateClass.ConfigContext.TableInfo)
                {
                    if (configExecutetemplateClass.Layer == ELayer.Front)
                    {
                        if (!item.MakeFront && !item.MakeFrontCrudBasic) continue;
                        if (item.MakeFrontBasic) continue;
                    }
                    if (templateClass.TypeTemplateClass == ETypeTemplateClass.Print && item.MakeFrontCrudNoPrint) continue;

                    classBuilderItems.Add(GenericTagsTransformerClass(configExecutetemplateClass.ConfigContext, item.ClassName, textTemplateClassItem));
                }

                if (configExecutetemplateClass.ExecuteProcess.IsNotNull())
                    classBuilder = configExecutetemplateClass.ExecuteProcess(configExecutetemplateClass.ConfigContext, classBuilder);

                var classBuilderItemsContent = string.Join(System.Environment.NewLine, classBuilderItems);
                classBuilder = classBuilder.Replace(!templateClass.TagTemplate.IsNullOrEmpaty() ? templateClass.TagTemplate : "<#classItems#>", classBuilderItemsContent);
            }

            using (var stream = new HelperStream(configExecutetemplateClass.PathOutput).GetInstance()) { stream.Write(classBuilder); }
        }

        protected virtual void TemplateFieldTypesFlow(ConfigExecutetemplate configExecutetemplate)
        {
            var classBuilder = TransformationTagsByClass(new ConfigExecutetemplate
            {
                ConfigContext = configExecutetemplate.ConfigContext,
                Operation = configExecutetemplate.Operation,
                TableInfo = configExecutetemplate.TableInfo,
                Template = configExecutetemplate.Template,
                PathOutput = configExecutetemplate.PathOutput,
            });

            if (configExecutetemplate.Infos.IsAny())
            {
                foreach (var templateField in configExecutetemplate.TemplateFields)
                {
                    var pathTemplateField = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, this._defineTemplateFolder.Define(configExecutetemplate.TableInfo), templateField.TemplateName);
                    var textTemplateField = Read.AllText(configExecutetemplate.TableInfo, pathTemplateField, this._defineTemplateFolder);
                    var classBuilderFields = string.Empty;

                    var groups = new List<Group>();

                    var groupDefined = configExecutetemplate.TableInfo.FieldsConfig
                      .Where(_ => _.Group.IsNotNull())
                      .GroupBy(_ => new { _.Group.Name, _.Group.Icon })
                      .Select(_ => new Group(_.Key.Name, _.Key.Icon));

                    if (groupDefined.IsAny())
                        groups.AddRange(groupDefined);

                    var groupsComponents = configExecutetemplate.TableInfo.GroupComponent;
                    if (groupsComponents.IsAny())
                        groups.AddRange(groupsComponents);


                    var order = 0;
                    foreach (var item in groups)
                        item.Order = order++;

                    if (groups.IsAny() && configExecutetemplate.Operation != EOperation.Front_angular_FieldFilter)
                    {

                        var pathTemplateNav = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, this._defineTemplateFolder.Define(configExecutetemplate.TableInfo), "field.nav.tab.tpl");
                        var textTemplateNav = Read.AllText(configExecutetemplate.TableInfo, pathTemplateNav, this._defineTemplateFolder);

                        var pathTemplateHeadNavItem = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, this._defineTemplateFolder.Define(configExecutetemplate.TableInfo), "field.nav.tab.head.item.tpl");
                        var textTemplateHeadNavItem = Read.AllText(configExecutetemplate.TableInfo, pathTemplateHeadNavItem, this._defineTemplateFolder);

                        var classBuilderNavHead = new List<string>();

                        var groupsNotDefined = configExecutetemplate.Infos
                           .Where(_ => !HelperFieldConfig.FieldInBlackList(configExecutetemplate.TableInfo, _, _.PropertyName, configExecutetemplate.Operation))
                           .Where(_ => !Audit.IsAuditField(_.PropertyName))
                           .Where(_ => _.IsKey == 0 || _.ShowFieldIsKey)
                           .Where(_ => _.Group.IsNull());


                        if (groupsNotDefined.IsAny())
                            groups.Add(configExecutetemplate.TableInfo.GroupNameOthers);


                        foreach (var group in groups.OrderBy(_ => _.Order))
                        {
                            var conditionalComponentGroupShow = string.Empty;

                            if (group is GroupComponent groupComponent)
                            {
                                if (groupComponent.DisableChkParent)
                                    conditionalComponentGroupShow = string.Empty;
                                else
                                    conditionalComponentGroupShow = string.Format("*ngIf='vm.model.{0} | existsRequest:\"{1}\" | async'", CamelCaseTransform(configExecutetemplate.TableInfo.KeyName), configExecutetemplate.TableInfo.ClassName);
                            }

                            classBuilderNavHead.Add(textTemplateHeadNavItem
                                .Replace("<#NavFirtItem#>", group == groups.OrderBy(_ => _.Order).FirstOrDefault() ? "active" : "")
                                .Replace("<#fieldItemsNavHeadName#>", group.Name)
                                .Replace("<#fieldItemsNavHeadIcon#>", group.Icon)
                                .Replace("<#fieldItemsNavHeadID#>", FieldItemsNavHeadID(configExecutetemplate, group))
                                .Replace("<#fieldItemsNavHeadShow#>", conditionalComponentGroupShow));
                        }

                        var classBuilderNavHeadText = string.Join(System.Environment.NewLine, classBuilderNavHead);
                        var classBuilderNavBodyText = this.TransformationTagsWithAllRestrictionsByFieldTypesWithGroups(configExecutetemplate, textTemplateField, groups);


                        classBuilderFields = textTemplateNav.Replace("<#navItemsHead#>", classBuilderNavHeadText).Replace("<#navItemsBody#>", classBuilderNavBodyText);
                    }
                    else
                    {
                        classBuilderFields = this.TransformationTagsWithAllRestrictionsByFieldTypes(configExecutetemplate, textTemplateField);
                    }

                    classBuilder = classBuilder.Replace(templateField.TagTemplate.IsNotNullOrEmpty() ? templateField.TagTemplate : "<#fieldItems#>", classBuilderFields);
                    classBuilder = classBuilder.Replace("<#cssClassContainer#>", groups.IsAny() ? "tab-pills-custom" : string.Empty);

                }
            }

            using (var stream = new HelperStream(configExecutetemplate.PathOutput).GetInstance()) { stream.Write(classBuilder); }
        }

        protected virtual void TemplateFieldFlow(ConfigExecutetemplate configExecutetemplate)
        {
            var classBuilder = TransformationTagsByClass(new ConfigExecutetemplate
            {
                ConfigContext = configExecutetemplate.ConfigContext,
                Operation = configExecutetemplate.Operation,
                TableInfo = configExecutetemplate.TableInfo,
                Template = configExecutetemplate.Template,
                PathOutput = configExecutetemplate.PathOutput,
            });

            if (configExecutetemplate.Infos.IsAny())
            {
                foreach (var templateField in configExecutetemplate.TemplateFields)
                {
                    var pathTemplateField = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, this._defineTemplateFolder.Define(configExecutetemplate.TableInfo), templateField.TemplateName);
                    var textTemplateField = Read.AllText(configExecutetemplate.TableInfo, pathTemplateField, this._defineTemplateFolder);

                    var classBuilderFields = string.Empty;
                    if (configExecutetemplate.WithRestrictions)
                        classBuilderFields = this.TransformationTagsdWithAllRestrictionsByFields(configExecutetemplate, textTemplateField);
                    else
                        classBuilderFields = this.TransformationTagsdWithOutRestrictionsByFields(configExecutetemplate, textTemplateField);

                    classBuilder = classBuilder.Replace(templateField.TagTemplate.IsNotNullOrEmpty() ? templateField.TagTemplate : "<#fieldItems#>", classBuilderFields);
                    classBuilder = classBuilder.Replace("<#cssClassContainer#>", string.Empty);
                }
            }
            else
            {
                foreach (var templateField in configExecutetemplate.TemplateFields)
                {
                    classBuilder = classBuilder.Replace(templateField.TagTemplate.IsNotNullOrEmpty() ? templateField.TagTemplate : "<#fieldItems#>", string.Empty);
                    classBuilder = classBuilder.Replace("<#cssClassContainer#>", string.Empty);
                }
            }

            using (var stream = new HelperStream(configExecutetemplate.PathOutput).GetInstance()) { stream.Write(classBuilder); }
        }



        #endregion


        #region TransformsFields

        public abstract string TransformFieldString(ConfigExecutetemplate configExecutetemplate, Info info, string propertyName, string textTemplate);
        public abstract string TransformFieldDateTime(ConfigExecutetemplate configExecutetemplate, Info info, string propertyName, string textTemplate, bool onlyDate = false);
        public abstract string TransformFieldBool(ConfigExecutetemplate configExecutetemplate, Info info, string propertyName, string textTemplate);
        public abstract string TransformFieldPropertyNavigation(ConfigExecutetemplate configExecutetemplate, Info info, string propertyName, string textTemplate);
        public abstract string TransformFieldHtml(ConfigExecutetemplate configExecutetemplate, Info info, string propertyName, string textTemplate);
        public abstract string TransformFieldUpload(ConfigExecutetemplate configExecutetemplate, Info info, string propertyName, string textTemplate);
        public abstract string TransformFieldTextStyle(ConfigExecutetemplate configExecutetemplate, Info info, string propertyName, string textTemplate);
        public abstract string TransformFieldTextEditor(ConfigExecutetemplate configExecutetemplate, Info info, string propertyName, string textTemplate);
        public abstract string TransformFieldTextTag(ConfigExecutetemplate configExecutetemplate, Info info, string propertyName, string textTemplate);
        public virtual string TransformField(ConfigExecutetemplate configExecutetemplate, TableInfo tableInfo, Info info, string propertyName, string textTemplate)
        {

            return GenericTagsTransformerInfo(propertyName, configExecutetemplate.ConfigContext, tableInfo, info, textTemplate, configExecutetemplate.Operation);
        }

        #endregion


        #region  transformations

        protected string TransformationTagsByClass(ConfigExecutetemplate configExecutetemplate)
        {

            var classBuilder = string.Empty;
            var pathTemplate = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, this._defineTemplateFolder.Define(configExecutetemplate.TableInfo), configExecutetemplate.Template);
            var textTemplate = Read.AllText(configExecutetemplate.TableInfo, pathTemplate, this._defineTemplateFolder);

            classBuilder = this.GenericTagsTransformer(configExecutetemplate.TableInfo, configExecutetemplate.ConfigContext, textTemplate, configExecutetemplate.Operation);

            return classBuilder;


        }

        protected string TransformationTagsWithAllRestrictionsByFieldTypesWithGroups(ConfigExecutetemplate configExecutetemplate, string textTemplate, IEnumerable<Group> groups)
        {
            var classBuilderFields = new List<string>();
            var pathTemplateBodyNavItem = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, this._defineTemplateFolder.Define(configExecutetemplate.TableInfo), "field.nav.tab.body.item.tpl");
            var textTemplateBodyNavItem = Read.AllText(configExecutetemplate.TableInfo, pathTemplateBodyNavItem, this._defineTemplateFolder);

            foreach (var group in groups.OrderBy(_ => _.Order))
            {
                var infos = configExecutetemplate.Infos;

                if (group.Name.ToLower() == configExecutetemplate.TableInfo.GroupNameOthers.Name.ToLower())
                {
                    infos = infos
                        .Where(_ => _.Group.IsNull())
                        .Where(_ => !HelperFieldConfig.FieldInBlackList(configExecutetemplate.TableInfo, _, _.PropertyName, configExecutetemplate.Operation))
                        .Where(_ => !Audit.IsAuditField(_.PropertyName))
                        .Where(_ => _.IsKey == 0 || _.ShowFieldIsKey);
                }
                else
                    infos = infos.Where(_ => _.Group.IsNotNull()).Where(_ => _.Group.Name == group.Name);


                var classBuilderFieldstab = new List<string>();

                var classBodyComponentGroup = "row";
                var conditionalComponentGroupShow = string.Empty;

                if (group is GroupComponent groupComponent)
                {
                    classBuilderFieldstab.Add(groupComponent.ComponentTag);
                    classBodyComponentGroup = "m-0 p-0";

                    if (groupComponent.DisableChkParent)
                        conditionalComponentGroupShow = string.Empty;
                    else
                        conditionalComponentGroupShow = string.Format("*ngIf='vm.model.{0} | existsRequest:\"{1}\" | async'", CamelCaseTransform(configExecutetemplate.TableInfo.KeyName), configExecutetemplate.TableInfo.ClassName);

                }
                else
                {
                    foreach (var info in infos)
                    {
                        if (Audit.IsAuditField(info.PropertyName))
                            continue;

                        if (HideKeyForForms(configExecutetemplate, info))
                            continue;

                        var itemTemplate = string.Empty;
                        var propertyName = configExecutetemplate.ConfigContext.CamelCasing ? CamelCaseTransform(info, configExecutetemplate.ConfigContext) : info.PropertyName;

                        if (configExecutetemplate.Operation == EOperation.Front_angular_FieldFilter)
                        {
                            var ignoreBigLength = HelperFieldConfig.IgnoreBigLength(configExecutetemplate.TableInfo, propertyName);
                            if (ignoreBigLength == false && (IsVarcharMax(info) || IsStringLengthBig(info, configExecutetemplate.ConfigContext)))
                                continue;
                        }

                        //Se a tabela esta configurada para mostrar todos, será avaliado se o campo esta em uma blacklist para não mostrar
                        if (configExecutetemplate.TableInfo.FieldsConfigShow == FieldConfigShow.ShowAll)
                        {
                            var fieldInBlackList = HelperFieldConfig.FieldInBlackList(configExecutetemplate.TableInfo, info, propertyName, configExecutetemplate.Operation);
                            if (fieldInBlackList)
                                continue;
                        }
                        else //Se a tabela esta configurada para esconder todos, será avaliado se o campo esta em uma allowlist para mostrar
                        {
                            var fieldInAllowList = HelperFieldConfig.FieldInAllowList(configExecutetemplate.TableInfo, info, propertyName, configExecutetemplate.Operation);
                            if (!fieldInAllowList)
                                continue;
                        }

                        var type = info.Type;
                        if (info.TypeCustom.IsSent())
                            type = info.TypeCustom;

                        if (type == "string")
                        {
                            itemTemplate = this.TransformFieldString(configExecutetemplate, info, propertyName, textTemplate);
                        }
                        else if (type == "Date" || type == "Date?")
                        {
                            itemTemplate = this.TransformFieldDateTime(configExecutetemplate, info, propertyName, textTemplate, true);
                        }
                        else if (type == "DateTime" || type == "DateTime?")
                        {
                            itemTemplate = this.TransformFieldDateTime(configExecutetemplate, info, propertyName, textTemplate);
                        }
                        else if (type == "bool" || type == "bool?")
                        {
                            itemTemplate = this.TransformFieldBool(configExecutetemplate, info, propertyName, textTemplate);
                        }
                        else if (IsPropertyNavigationTypeInstance(configExecutetemplate.TableInfo, info.PropertyName))
                        {
                            itemTemplate = this.TransformFieldPropertyNavigation(configExecutetemplate, info, propertyName, textTemplate);
                        }
                        else if (info.RelationOneToOne)
                        {
                            itemTemplate = this.TransformFieldPropertyNavigation(configExecutetemplate, info, propertyName, textTemplate);
                        }
                        else
                        {
                            itemTemplate = this.TransformFieldString(configExecutetemplate, info, propertyName, textTemplate);
                        }


                        var htmlCtrl = HelperFieldConfig.FieldHtml(configExecutetemplate.TableInfo, propertyName);
                        if (htmlCtrl.IsNotNull())
                        {
                            itemTemplate = this.TransformFieldHtml(configExecutetemplate, info, propertyName, textTemplate);
                        }

                        var isUpload = HelperFieldConfig.IsUpload(configExecutetemplate.TableInfo, propertyName);
                        if (isUpload)
                        {
                            itemTemplate = this.TransformFieldUpload(configExecutetemplate, info, propertyName, textTemplate);
                        }

                        var isTextStyle = HelperFieldConfig.IsTextStyle(configExecutetemplate.TableInfo, propertyName);
                        if (isTextStyle)
                        {
                            itemTemplate = this.TransformFieldTextStyle(configExecutetemplate, info, propertyName, textTemplate);
                        }

                        var isTextEditor = HelperFieldConfig.IsTextEditor(configExecutetemplate.TableInfo, propertyName);
                        if (isTextEditor)
                        {
                            itemTemplate = this.TransformFieldTextEditor(configExecutetemplate, info, propertyName, textTemplate);
                        }

                        var isTextTag = HelperFieldConfig.IsTextTag(configExecutetemplate.TableInfo, propertyName);
                        if (isTextTag)
                        {
                            itemTemplate = this.TransformFieldTextTag(configExecutetemplate, info, propertyName, textTemplate);
                        }

                        if (!itemTemplate.IsNullOrEmpaty())
                        {
                            var htmlAfter = HelperFieldConfig.GetHtmlAfterSection(configExecutetemplate.TableInfo, propertyName);
                            if (htmlAfter.IsNotNullOrEmpty())
                                htmlAfter = htmlAfter + Environment.NewLine;

                            classBuilderFieldstab.Add(string.Format("{0}{1}", itemTemplate, htmlAfter));

                        }
                    }
                }


                var classBuilderFieldstabText = textTemplateBodyNavItem
                    .Replace("<#NavFirtItem#>", group == groups.OrderBy(_ => _.Order).FirstOrDefault() ? "active" : "")
                    .Replace("<#fieldItemsNavHeadID#>", FieldItemsNavHeadID(configExecutetemplate, group))
                    .Replace("<#fieldItemsNavBody#>", string.Join(System.Environment.NewLine, classBuilderFieldstab))
                    .Replace("<#fieldItemsNavBodyClass#>", classBodyComponentGroup)
                    .Replace("<#fieldItemsNavHeadShow#>", conditionalComponentGroupShow);

                classBuilderFields.Add(classBuilderFieldstabText);
            }

            return string.Join(System.Environment.NewLine, classBuilderFields);
        }

        protected string TransformationTagsWithAllRestrictionsByFieldTypes(ConfigExecutetemplate configExecutetemplate, string textTemplate)
        {
            var classBuilderFields = new List<string>();

            foreach (var info in configExecutetemplate.Infos)
            {
                if (Audit.IsAuditField(info.PropertyName))
                    continue;

                if (HideKeyForForms(configExecutetemplate, info))
                    continue;

                var itemTemplate = string.Empty;
                var propertyName = configExecutetemplate.ConfigContext.CamelCasing ? CamelCaseTransform(info, configExecutetemplate.ConfigContext) : info.PropertyName;

                if (configExecutetemplate.Operation == EOperation.Front_angular_FieldFilter)
                {
                    var ignoreBigLength = HelperFieldConfig.IgnoreBigLength(configExecutetemplate.TableInfo, propertyName);
                    if (ignoreBigLength == false && (IsVarcharMax(info) || IsStringLengthBig(info, configExecutetemplate.ConfigContext)))
                        continue;
                }

                //Se a tabela esta configurada para mostrar todos, será avaliado se o campo esta em uma blacklist para não mostrar
                if (configExecutetemplate.TableInfo.FieldsConfigShow == FieldConfigShow.ShowAll)
                {
                    var fieldInBlackList = HelperFieldConfig.FieldInBlackList(configExecutetemplate.TableInfo, info, propertyName, configExecutetemplate.Operation);
                    if (fieldInBlackList)
                        continue;
                }
                else //Se a tabela esta configurada para esconder todos, será avaliado se o campo esta em uma allowlist para mostrar
                {
                    var fieldInAllowList = HelperFieldConfig.FieldInAllowList(configExecutetemplate.TableInfo, info, propertyName, configExecutetemplate.Operation);
                    if (!fieldInAllowList)
                        continue;
                }

                var type = info.Type;
                if (info.TypeCustom.IsSent())
                    type = info.TypeCustom;

                if (type == "string")
                {
                    itemTemplate = this.TransformFieldString(configExecutetemplate, info, propertyName, textTemplate);
                }
                else if (type == "Date" || type == "Date?")
                {
                    itemTemplate = this.TransformFieldDateTime(configExecutetemplate, info, propertyName, textTemplate, true);
                }
                else if (type == "DateTime" || type == "DateTime?")
                {
                    itemTemplate = this.TransformFieldDateTime(configExecutetemplate, info, propertyName, textTemplate);
                }
                else if (type == "bool" || type == "bool?")
                {
                    itemTemplate = this.TransformFieldBool(configExecutetemplate, info, propertyName, textTemplate);
                }
                else if (IsPropertyNavigationTypeInstance(configExecutetemplate.TableInfo, info.PropertyName))
                {
                    itemTemplate = this.TransformFieldPropertyNavigation(configExecutetemplate, info, propertyName, textTemplate);
                }
                else
                {
                    itemTemplate = this.TransformFieldString(configExecutetemplate, info, propertyName, textTemplate);
                }


                var htmlCtrl = HelperFieldConfig.FieldHtml(configExecutetemplate.TableInfo, propertyName);
                if (htmlCtrl.IsNotNull())
                {
                    itemTemplate = this.TransformFieldHtml(configExecutetemplate, info, propertyName, textTemplate);
                }

                var isUpload = HelperFieldConfig.IsUpload(configExecutetemplate.TableInfo, propertyName);
                if (isUpload)
                {
                    itemTemplate = this.TransformFieldUpload(configExecutetemplate, info, propertyName, textTemplate);
                }

                var isTextStyle = HelperFieldConfig.IsTextStyle(configExecutetemplate.TableInfo, propertyName);
                if (isTextStyle)
                {
                    itemTemplate = this.TransformFieldTextStyle(configExecutetemplate, info, propertyName, textTemplate);
                }

                var isTextEditor = HelperFieldConfig.IsTextEditor(configExecutetemplate.TableInfo, propertyName);
                if (isTextEditor)
                {
                    itemTemplate = this.TransformFieldTextEditor(configExecutetemplate, info, propertyName, textTemplate);
                }

                var isTextTag = HelperFieldConfig.IsTextTag(configExecutetemplate.TableInfo, propertyName);
                if (isTextTag)
                {
                    itemTemplate = this.TransformFieldTextTag(configExecutetemplate, info, propertyName, textTemplate);
                }

                if (!itemTemplate.IsNullOrEmpaty())
                {
                    var htmlAfter = HelperFieldConfig.GetHtmlAfterSection(configExecutetemplate.TableInfo, propertyName);
                    if (htmlAfter.IsNotNullOrEmpty())
                        htmlAfter = htmlAfter + Environment.NewLine;

                    classBuilderFields.Add(string.Format("{0}{1}", itemTemplate, htmlAfter));

                }
            }

            return string.Join(Environment.NewLine, classBuilderFields);
        }

        protected string TransformationTagsdWithAllRestrictionsByFields(ConfigExecutetemplate configExecutetemplate, string textTemplate)
        {
            var classBuilderFields = new List<string>();

            foreach (var info in configExecutetemplate.Infos)
            {
                if (Audit.IsAuditField(info.PropertyName))
                    continue;

                var itemTemplate = string.Empty;
                var propertyName = configExecutetemplate.ConfigContext.CamelCasing ? CamelCaseTransform(info, configExecutetemplate.ConfigContext) : info.PropertyName;

                //Se a tabela esta configurada para mostrar todos, será avaliado se o campo esta em uma blacklist para não mostrar
                if (configExecutetemplate.TableInfo.FieldsConfigShow == FieldConfigShow.ShowAll)
                {
                    var fieldInBlackList = HelperFieldConfig.FieldInBlackList(configExecutetemplate.TableInfo, info, propertyName, configExecutetemplate.Operation);
                    if (fieldInBlackList)
                        continue;
                }
                else //Se a tabela esta configurada para esconder todos, será avaliado se o campo esta em uma allowlist para mostrar
                {
                    var fieldInAllowList = HelperFieldConfig.FieldInAllowList(configExecutetemplate.TableInfo, info, propertyName, configExecutetemplate.Operation);
                    if (!fieldInAllowList)
                        continue;
                }

                itemTemplate = this.TransformField(configExecutetemplate, configExecutetemplate.TableInfo, info, propertyName, textTemplate);
                if (itemTemplate.IsNullOrEmpaty())
                    continue;

                classBuilderFields.Add(itemTemplate);
            }

            return string.Join(System.Environment.NewLine, classBuilderFields);
        }

        protected string TransformationTagsdWithOutRestrictionsByFields(ConfigExecutetemplate configExecutetemplate, string textTemplate)
        {
            var classBuilderFields = new List<string>();

            foreach (var info in configExecutetemplate.Infos)
            {
                if (Audit.IsAuditField(info.PropertyName))
                    continue;

                var itemTemplate = string.Empty;
                var propertyName = configExecutetemplate.ConfigContext.CamelCasing ? CamelCaseTransform(info, configExecutetemplate.ConfigContext) : info.PropertyName;

                itemTemplate = this.TransformField(configExecutetemplate, configExecutetemplate.TableInfo, info, propertyName, textTemplate);
                if (itemTemplate.IsNullOrEmpaty())
                    continue;

                classBuilderFields.Add(itemTemplate);
            }

            return string.Join(System.Environment.NewLine, classBuilderFields);
        }

        #endregion


        #region Helpers

        private static string FieldItemsNavHeadID(ConfigExecutetemplate configExecutetemplate, Group group)
        {
            var operation = configExecutetemplate.Operation == EOperation.Front_angular_FieldsCreate ? "create" : configExecutetemplate.Operation == EOperation.Front_angular_FieldsEdit ? "edit" : "other";
            var fieldItemsNavHeadID = string.Format("{0}-{1}-{2}", group.Name.ClearText(), configExecutetemplate.TableInfo.ClassName, operation);
            return fieldItemsNavHeadID;
        }

        private string MakePropertyName(string column, string className)
        {
            return MakePropertyName(column, className, 0);
        }

        private string MakeClassNameTemplateDefault(string tableName)
        {
            var result = tableName.Substring(7);
            result = CamelCaseTransform(result);
            result = ClearEnd(result);
            return result;
        }

        private bool Open(string connectionString, int attemps = 0)
        {
            try
            {
                this.conn = new SqlConnection(connectionString);
                this.conn.Open();
                return true;
            }
            catch (Exception ex)
            {
                attemps += 1;
                if (attemps <= 99)
                {
                    PrinstScn.WriteWarningLine(string.Format(">>>>> Retry Open Connection {0} Erro: [{1}]", attemps, ex.Message));
                    Thread.Sleep(2000 * attemps);
                    return Open(connectionString, attemps);
                }

                throw new InvalidOperationException($">>>>> Attemps Limit Reached - [{ex.Message}]");
            }

        }

        private bool Close(string connectionString, int attemps = 0)
        {
            if (this.conn.IsNotNull())
                this.conn.Close();

            return true;
        }

        private string MakePropertyNameForId(TableInfo tableConfig)
        {
            var propertyName = string.Format("{0}Id", tableConfig.ClassName);
            return propertyName;
        }

        private static string DefineExtFileName(ConfigExecutetemplate config)
        {
            var ext = Path.GetExtension(config.PathOutput);
            var fileNameWithOutExtension = Path.GetFileNameWithoutExtension(config.PathOutput);
            return config.PathOutput.Replace(fileNameWithOutExtension, string.Format("{0}.ext", fileNameWithOutExtension));
        }
        private string MakePropertyNameDefault(string columnName)
        {

            var newcolumnName = columnName.Substring(4);

            newcolumnName = TranslateNames(newcolumnName);

            newcolumnName = CamelCaseTransform(newcolumnName);

            newcolumnName = ClearEnd(newcolumnName);

            return newcolumnName;

        }
        protected virtual string ClearEnd(string value)
        {
            value = value.Replace("_X_", "");
            value = value.Replace("_", "");
            value = value.Replace("-", "");
            return value;
        }
        protected virtual string TranslateNames(string newcolumnName)
        {

            newcolumnName = newcolumnName.Contains("_cd_") ? String.Concat(newcolumnName.Replace("_cd_", "_"), "_Id_") : newcolumnName;
            newcolumnName = newcolumnName.Contains("_nm_") ? String.Concat(newcolumnName.Replace("_nm_", "_"), "_Nome_") : newcolumnName;
            newcolumnName = newcolumnName.Contains("_ds_") ? String.Concat(newcolumnName.Replace("_ds_", "_"), "_Descricao_") : newcolumnName;
            newcolumnName = newcolumnName.Contains("_nr_") ? String.Concat(newcolumnName.Replace("_nr_", "_"), "_Numero_") : newcolumnName;
            newcolumnName = newcolumnName.Contains("_pc_") ? String.Concat(newcolumnName.Replace("_pc_", "_"), "_Porcentagem_") : newcolumnName;
            newcolumnName = newcolumnName.Contains("_qt_") ? String.Concat(newcolumnName.Replace("_qt_", "_"), "_Quantidade_") : newcolumnName;
            newcolumnName = newcolumnName.Contains("_dt_") ? String.Concat(newcolumnName.Replace("_dt_", "_"), "_Data_") : newcolumnName;
            newcolumnName = newcolumnName.Contains("_fl_") ? String.Concat(newcolumnName.Replace("_fl_", "_"), "_Flag_") : newcolumnName;
            newcolumnName = newcolumnName.Contains("_vl_") ? String.Concat(newcolumnName.Replace("_vl_", "_"), "_Valor_") : newcolumnName;
            newcolumnName = newcolumnName.Contains("_in_") ? String.Concat(newcolumnName.Replace("_in_", "_"), "") : newcolumnName;
            newcolumnName = newcolumnName.Contains("_sg_") ? String.Concat(newcolumnName.Replace("_sg_", "_"), "_Sigla_") : newcolumnName;
            newcolumnName = newcolumnName.Contains("_tp_") ? String.Concat(newcolumnName.Replace("_tp_", "_"), "_Tipo_") : newcolumnName;

            return newcolumnName;
        }
        private string TranslateNamesPatnerX(string newcolumnName)
        {


            newcolumnName = newcolumnName.Contains("_X_") ? String.Concat(newcolumnName.Replace("_X_", ""), "") : newcolumnName;
            newcolumnName = newcolumnName.Contains("_cd") ? String.Concat(newcolumnName.Replace("_cd", ""), "_Id") : newcolumnName;

            return newcolumnName;
        }
        protected virtual string CamelCaseTransform(Info info, Context context)
        {
            if (info.CameCasingManual.IsNotNullOrEmpty())
                return info.CameCasingManual;
            if (context.DisabledCamelCasingException)
                return info.PropertyName.FirstCharToLower();

            return this.CamelCaseTransform(info.PropertyName);
        }
        protected virtual string CamelCaseTransform(string value)
        {
            return HelperSysObjectUtil.CamelCaseTransform(value);
        }

        private string makePrefixTable(TableInfo tableConfig)
        {
            if (!tableConfig.TableName.Contains("_X_"))
            {
                var prefix = tableConfig.TableName.Split('_')[1];
                return prefix;
            }
            return string.Empty;
        }
        private string makePrefixField(string columnName)
        {
            return columnName.Split('_')[0];
        }
        protected string RemoveLine(string itemTemplateValidation, string targetField)
        {
            return itemTemplateValidation
                .Replace(targetField + System.Environment.NewLine, string.Empty)
                .Replace(System.Environment.NewLine + targetField, string.Empty)
                .Replace(targetField, string.Empty);
        }
        private string GetModuleFromContextByTableName(string tableName, string module)
        {
            var result = this.Contexts
                .Where(_ => _.Module == module)
                .Where(_ => _.TableInfo
                    .Where(__ => __.TableName.Equals(tableName)).Any())
                .Select(_ => _.Module).FirstOrDefault();
            return result;
        }
        private string GetNameSpaceFromContextByTableName(string tableName, string module)
        {
            var _namespace = this.Contexts
                .Where(_ => _.Module == module)
                .Where(_ => _.TableInfo
                    .Where(__ => __.TableName.Equals(tableName)).Any())
                .Select(_ => _.Namespace).FirstOrDefault();
            return _namespace;
        }
        private string GetNameSpaceFromContextWitExposeParametersByTableName(string tableName, string module)
        {
            var namespaceApp = this.Contexts
                .Where(_ => _.Module == module)
                .Where(_ => _.TableInfo
                    .Where(__ => __.TableName.Equals(tableName))
                    .Where(___ => ___.MakeApp || ___.MakeFront || ___.MakeFront)
                    .Any())
                .Select(_ => _.Namespace).FirstOrDefault();
            return namespaceApp;
        }
        private string GetNameSpaceFromContextWithMakeDtoByTableName(string tableName, string module)
        {
            var namespaceDto = this.Contexts
               .Where(_ => _.Module == module)
               .Where(_ => _.TableInfo
                   .Where(__ => __.TableName.Equals(tableName))
                   .Where(___ => ___.MakeDto)
                   .Any())
               .Select(_ => _.Namespace).FirstOrDefault();
            return namespaceDto;
        }
        private bool AppExpose(string namespaceApp)
        {
            return !string.IsNullOrEmpty(namespaceApp);
        }
        private IEnumerable<Info> GetReletaedIntancesComplementedClasses(Context config, string currentTableName, int attemps = 0)
        {
            try
            {
                var commandText = new StringBuilder();
                commandText.Append("SELECT ");
                commandText.Append("KCU1.CONSTRAINT_NAME AS 'FK_Nome_Constraint' ");
                commandText.Append(",KCU1.TABLE_NAME AS 'FK_Nome_Tabela' ");
                commandText.Append(",KCU1.COLUMN_NAME AS 'FK_Nome_Coluna' ");
                commandText.Append(",FK.is_disabled AS 'FK_Esta_Desativada' ");
                commandText.Append(",KCU2.CONSTRAINT_NAME AS 'PK_Nome_Constraint_Referenciada' ");
                commandText.Append(",KCU2.TABLE_NAME AS 'PK_Nome_Tabela_Referenciada' ");
                commandText.Append(",KCU2.COLUMN_NAME AS 'PK_Nome_Coluna_Referenciada' ");
                commandText.Append("FROM INFORMATION_SCHEMA.REFERENTIAL_CONSTRAINTS RC ");
                commandText.Append("JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE KCU1 ");
                commandText.Append("ON KCU1.CONSTRAINT_CATALOG = RC.CONSTRAINT_CATALOG ");
                commandText.Append("AND KCU1.CONSTRAINT_SCHEMA = RC.CONSTRAINT_SCHEMA ");
                commandText.Append("AND KCU1.CONSTRAINT_NAME = RC.CONSTRAINT_NAME ");
                commandText.Append("JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE KCU2 ");
                commandText.Append("ON KCU2.CONSTRAINT_CATALOG = RC.UNIQUE_CONSTRAINT_CATALOG  ");
                commandText.Append("AND KCU2.CONSTRAINT_SCHEMA = RC.UNIQUE_CONSTRAINT_SCHEMA ");
                commandText.Append("AND KCU2.CONSTRAINT_NAME = RC.UNIQUE_CONSTRAINT_NAME ");
                commandText.Append("AND KCU2.ORDINAL_POSITION = KCU1.ORDINAL_POSITION ");
                commandText.Append("JOIN sys.foreign_keys FK on FK.name = KCU1.CONSTRAINT_NAME ");
                commandText.Append("Where ");
                commandText.Append(string.Format("KCU1.TABLE_NAME = '{0}' ", currentTableName));
                commandText.Append("Order by  ");
                commandText.Append("KCU1.TABLE_NAME ");

                var comando = new SqlCommand(commandText.ToString(), this.conn);
                var reader = comando.ExecuteReader();
                var reletedClasses = new List<Info>();
                while (reader.Read())
                {
                    var tableNamePk = reader["PK_Nome_Tabela_Referenciada"].ToString();
                    var classNamePk = reader["PK_Nome_Tabela_Referenciada"].ToString();
                    MakeReletedClass(config, reader, reletedClasses, currentTableName, tableNamePk, classNamePk, "PK_Nome_Coluna_Referenciada", "FK_Nome_Coluna", NavigationType.Instance);
                }

                comando.Dispose();
                reader.Close();
                return reletedClasses;
            }
            catch (Exception ex)
            {
                attemps += 1;
                if (attemps <= 99)
                {
                    PrinstScn.WriteWarningLine(string.Format(">>>>> Retry {0} GetReletaedIntancesComplementedClasses {1} Erro: [{2}]", currentTableName, attemps, ex.Message));
                    Thread.Sleep(2000 * attemps);
                    return GetReletaedIntancesComplementedClasses(config, currentTableName, attemps);
                }
                throw new InvalidOperationException($"Attemps Limit Reached - [{ex.Message}]");
            }


        }
        private IEnumerable<Info> GetReletaedClasses(Context config, string currentTableName, int attemps = 0)
        {
            try
            {
                var commandText = new StringBuilder().Append(string.Format("EXEC sp_fkeys '{0}'", currentTableName));

                var comando = new SqlCommand(commandText.ToString(), this.conn);
                var reader = comando.ExecuteReader();
                var reletedClasses = new List<Info>();
                while (reader.Read())
                {
                    var tableNameFK = reader["FKTABLE_NAME"].ToString();
                    var classNameFK = reader["FKTABLE_NAME"].ToString();
                    var navigationType = reader["FKCOLUMN_NAME"].ToString().Equals("Id") ? NavigationType.Instance : NavigationType.Collettion;
                    MakeReletedClass(config, reader, reletedClasses, tableNameFK, tableNameFK, classNameFK, "PKCOLUMN_NAME", "FKCOLUMN_NAME", navigationType);
                }
                comando.Dispose();
                reader.Close();
                return reletedClasses;

            }
            catch (Exception ex)
            {
                attemps += 1;
                if (attemps <= 99)
                {
                    PrinstScn.WriteWarningLine(string.Format(">>>>> Retry {0} GetReletaedClasses {1} - Erro: [{2}]", currentTableName, attemps, ex.Message));
                    Thread.Sleep(2000 * attemps);
                    return GetReletaedClasses(config, currentTableName, attemps);
                }

                throw new InvalidOperationException($"Attemps Limit Reached - [{ex.Message}]");
            }



        }

        private void MakeReletedClass(Context config, SqlDataReader reader, List<Info> reletedClasses, string currentTableName, string tableNamePK, string className, string PK_FieldName, string FK_FieldName, NavigationType navigationType)
        {
            var _module = GetModuleFromContextByTableName(tableNamePK, config.Module);
            var _namespace = GetNameSpaceFromContextByTableName(tableNamePK, config.Module);
            var _namespaceDto = GetNameSpaceFromContextWithMakeDtoByTableName(tableNamePK, config.Module);
            var _namespaceApp = GetNameSpaceFromContextWitExposeParametersByTableName(tableNamePK, config.Module);

            if (AppExpose(_namespaceApp))
            {
                var classNameNew = MakeClassName(new TableInfo { ClassName = className });

                reletedClasses.Add(new Info
                {
                    Table = tableNamePK,
                    ClassName = classNameNew,
                    Module = _module,
                    Namespace = _namespace,
                    NamespaceApp = _namespaceApp,
                    NamespaceDto = _namespaceDto,
                    PropertyNamePk = MakePropertyName(reader[PK_FieldName].ToString(), classNameNew),
                    PropertyNameFk = MakePropertyName(reader[FK_FieldName].ToString(), classNameNew),
                    NavigationType = navigationType
                });
            }
        }

        private string makeClassName(string tableName)
        {
            return MakeClassNameTemplateDefault(tableName);
        }
        private void IsValid(TableInfo tableInfo)
        {
            if (!tableInfo.Scaffold)
            {
                if (tableInfo.ClassName.IsNull())
                    throw new InvalidOperationException("Para gerar Classes com Scaffold false é preciso setar a propriedade className");
            }
        }
        private void ExecuteTemplatesByTableleInfo(Context config, string RunOnlyThisClass)
        {
            config.RunOnlyThisClass = !string.IsNullOrEmpty(RunOnlyThisClass);
            var tableInfoItens = config.TableInfo;
            //if (config.RunOnlyThisClass)
            //    tableInfoItens = config.TableInfo
            //        .Where(_ => _.TableName.ToLower() == RunOnlyThisClass.ToLower())
            //        .ToList();

            foreach (var tableInfo in tableInfoItens)
            {
                DefineTemplateByTableInfo(config, tableInfo);
            }
        }
        private void DefineInfoKey(TableInfo tableInfo, List<Info> infos)
        {

            var keys = infos.Where(_ => _.IsKey == 1);
            //if (keys.Count() == 0)
            //    throw new InvalidOperationException($"{tableInfo.TableName} not have key defined.");

            var Keys = new List<string>();
            var KeysTypes = new List<string>();

            foreach (var item in keys)
            {
                Keys.Add(item.PropertyName);
                KeysTypes.Add(item.Type);
            }

            tableInfo.Keys = Keys;
            tableInfo.KeysTypes = KeysTypes;

        }
        private void DeleteFilesNotFoudTable(Context config, TableInfo tableInfo)
        {
            var PathOutputMaps = PathOutput.PathOutputMaps(tableInfo, config);
            var PathOutputDomainModelsValidation = PathOutput.PathOutputDomainModelsValidation(tableInfo, config);
            var PathOutputDomainModels = PathOutput.PathOutputDomainModels(tableInfo, config);
            var PathOutputApp = PathOutput.PathOutputApp(tableInfo, config);
            var PathOutputUri = PathOutput.PathOutputUri(tableInfo, config);
            var PathOutputDto = PathOutput.PathOutputDto(tableInfo, config);
            var PathOutputApi = PathOutput.PathOutputApi(tableInfo, config);
            var PathOutputApplicationTest = PathOutput.PathOutputApplicationTest(tableInfo, config);
            var PathOutputApplicationTestMoq = PathOutput.PathOutputApplicationTestMoq(tableInfo, config);
            var PathOutputApiTest = PathOutput.PathOutputApiTest(tableInfo, config);

            File.Delete(PathOutputMaps);
            File.Delete(PathOutputDomainModelsValidation);
            File.Delete(PathOutputDomainModels);
            File.Delete(PathOutputApp);
            File.Delete(PathOutputUri);
            File.Delete(PathOutputDto);
            File.Delete(PathOutputApi);
            File.Delete(PathOutputApplicationTest);
            File.Delete(PathOutputApplicationTestMoq);
            File.Delete(PathOutputApiTest);

        }
        private void ExecuteTemplateByTableInfoFields(Context config, string RunOnlyThisClass)
        {
            var qtd = 0;
            config.RunOnlyThisClass = !string.IsNullOrEmpty(RunOnlyThisClass);
            var tableInfoItens = config.TableInfo;
            //if (config.RunOnlyThisClass)
            //    tableInfoItens = config.TableInfo
            //        .Where(_ => _.TableName.ToLower() == RunOnlyThisClass.ToLower())
            //        .ToList();


            foreach (var tableInfo in tableInfoItens)
            {
                this.Open(config.ConnectionString);

                qtd++;
                var infos = new UniqueListInfo();

                this.IsValid(tableInfo);

                var reletedClass = new List<Info>();

                reletedClass.AddRange(GetReletaedClasses(config, tableInfo.TableName));
                reletedClass.AddRange(GetReletaedIntancesComplementedClasses(config, tableInfo.TableName));

                tableInfo.ClassName = MakeClassName(tableInfo);
                tableInfo.ReletedClass = reletedClass;
                var dateTimeComparation = config.DateTimeComparation;

                if (config.RunOnlyThisClass)
                {
                    if (tableInfo.TableName == RunOnlyThisClass)
                        DicoveryFieldInfos(config, qtd, tableInfo, infos, dateTimeComparation);
                }
                else
                    DicoveryFieldInfos(config, qtd, tableInfo, infos, dateTimeComparation);

                this.Close(config.ConnectionString);
            }
        }

        private void DicoveryFieldInfos(Context config, int qtd, TableInfo tableInfo, UniqueListInfo infos, string dateTimeComparation)
        {
            if (tableInfo.Scaffold)
            {
                var reader = GetInfoSysObjectsComplete(config, tableInfo);
                while (reader.Read())
                {
                    var propertyName = this.MakePropertyName(reader["NomeColuna"].ToString(), tableInfo.ClassName, Convert.ToInt32(reader["Chave"]));
                    var typeCustom = default(string);

                    var fieldsConfig = tableInfo.FieldsConfig.IsAny() ? tableInfo.FieldsConfig.Where(_ => _.Name.ToUpper() == propertyName.ToUpper()).SingleOrDefault() : null;
                    if (fieldsConfig.IsNotNull() && fieldsConfig.TypeCustom.IsSent())
                        typeCustom = fieldsConfig.TypeCustom;

                    infos.Add(new Info
                    {
                        Table = reader["Tabela"].ToString(),
                        ClassName = tableInfo.ClassName,
                        ColumnName = reader["NomeColuna"].ToString(),
                        PropertyName = propertyName,
                        Length = reader["Tamanho"].ToString(),
                        IsKey = Convert.ToInt32(reader["Chave"]),
                        IsNullable = Convert.ToInt32(reader["Nulo"]),
                        Type = TypeConvertCSharp.Convert(reader["tipo"].ToString(), Convert.ToInt32(reader["Nulo"])),
                        TypeOriginal = reader["tipo"].ToString(),
                        TypeCustom = typeCustom,
                        Module = config.Module,
                        Namespace = config.Namespace,
                        DateTimeComparation = dateTimeComparation
                    });
                }

                reader.Close();
                this.conn.Close();

                DefinePropertyDefault(infos);
                DefineInfoKey(tableInfo, infos);
                DefineFieldFilterDefault(config, tableInfo);
                DefineDataItemFieldName(infos, tableInfo);
                tableInfo.IsAuditField = Audit.ExistsAuditFields(infos);


                if (infos.Count == 0)

                {
                    if (config.DeleteFilesNotFoundTable)
                        DeleteFilesNotFoudTable(config, tableInfo);

                    if (config.AlertNotFoundTable)
                        throw new Exception("Tabela " + tableInfo.TableName + " Não foi econtrada");

                }
            }



            Console.ForegroundColor = ConsoleColor.Gray;
            Console.CursorLeft = 10;
            PrinstScn.WriteLine(string.Format("{0} iniciada [{1}]", tableInfo.TableName, qtd));


            DefineTemplateByTableInfoFields(config, tableInfo, CastOrdenabledToUniqueListInfo(infos));
        }

        private static void DefinePropertyDefault(UniqueListInfo infos)
        {
            if (Audit.ExistsAuditFields(infos))
            {
                foreach (var item in infos)
                    item.PropertyName = Audit.DefinePropertyDefault(item.PropertyName);
            }
        }

        private SqlDataReader GetInfoSysObjectsComplete(Context config, TableInfo tableInfo, int attemps = 0)
        {

            try
            {
                var commandText = new StringBuilder();
                commandText.Append("SELECT ");
                commandText.Append(" dbo.sysobjects.name AS Tabela,");
                commandText.Append(" dbo.syscolumns.name AS NomeColuna,");
                commandText.Append(" dbo.syscolumns.length AS Tamanho,");
                commandText.Append(" isnull(pk.is_primary_key,0) AS Chave,");
                commandText.Append(" dbo.syscolumns.isnullable AS Nulo,");
                commandText.Append(" dbo.systypes.name AS Tipo");
                commandText.Append(" FROM ");
                commandText.Append(" dbo.syscolumns INNER JOIN");
                commandText.Append(" dbo.sysobjects ON dbo.syscolumns.id = dbo.sysobjects.id INNER JOIN");
                commandText.Append(" dbo.systypes ON dbo.syscolumns.xtype = dbo.systypes.xtype INNER JOIN");
                commandText.Append(" dbo.sysusers ON sysobjects.uid = sysusers.uid");
                commandText.Append(" LEFT JOIN (");
                commandText.Append(" Select Tablename, is_primary_key,ColumnName from (SELECT  i.name AS IndexName,");
                commandText.Append(" OBJECT_NAME(ic.OBJECT_ID) AS TableName,");
                commandText.Append(" COL_NAME(ic.OBJECT_ID,ic.column_id) AS ColumnName,");
                commandText.Append(" i.is_primary_key ");
                commandText.Append(" FROM sys.indexes AS i INNER JOIN ");
                commandText.Append(" sys.index_columns AS ic ON  i.OBJECT_ID = ic.OBJECT_ID");
                commandText.Append(" AND i.index_id = ic.index_id");
                commandText.Append(" WHERE   i.is_primary_key = 1) as TB_PRIMARYS) as pk");
                commandText.Append(" ON pk.tablename =  dbo.sysobjects.name and pk.ColumnName = dbo.syscolumns.name");
                commandText.Append(" WHERE ");
                commandText.Append(" (dbo.sysobjects.name = '" + tableInfo.TableName + "') ");
                commandText.Append(" AND ");
                commandText.Append(" (dbo.sysusers.name = '" + tableInfo.Schema + "') ");
                commandText.Append(" AND ");
                commandText.Append(" (dbo.systypes.status <> 1) ");
                commandText.Append(" ORDER BY ");
                commandText.Append(" dbo.sysobjects.name, ");
                commandText.Append(" dbo.syscolumns.colorder ");


                
                

                var comando = new SqlCommand(commandText.ToString(), this.conn);
                var reader = comando.ExecuteReader();
                return reader;
            }
            catch (Exception ex)
            {
                attemps += 1;
                if (attemps <= 99)
                {
                    this.Open(config.ConnectionString);
                    PrinstScn.WriteWarningLine(string.Format(">>>>> Retry {0} GetInfoSysObjectsComplete {1} Erro: [{2}]", tableInfo.TableName, attemps, ex.Message));
                    Thread.Sleep(2000 * attemps);
                    return GetInfoSysObjectsComplete(config, tableInfo, attemps);
                }

                throw new InvalidOperationException($"Attemps Limit Reached - [{ex.Message}]");
            }

           


        }

        private SqlDataReader GetInfoSysObjectsBasic(Context config, string tableName, int attemps = 0)
        {
            try
            {
                this.Open(config.ConnectionString);
                var commandText = new StringBuilder();
                commandText.Append("SELECT TOP 1");
                commandText.Append(" dbo.sysobjects.name AS Tabela,");
                commandText.Append(" dbo.syscolumns.name AS NomeColuna");
                commandText.Append(" FROM ");
                commandText.Append(" dbo.syscolumns INNER JOIN");
                commandText.Append(" dbo.sysobjects ON dbo.syscolumns.id = dbo.sysobjects.id ");
                commandText.Append(" WHERE ");
                commandText.Append(" dbo.sysobjects.name = '" + tableName + "' ");
                commandText.Append(" AND dbo.syscolumns.name not like '%id%' ");
                commandText.Append(" ORDER BY ");
                commandText.Append(" dbo.sysobjects.name, ");
                commandText.Append(" dbo.syscolumns.colorder ");

                var comando = new SqlCommand(commandText.ToString(), this.conn);
                var reader = comando.ExecuteReader();
                return reader;
            }
            catch (Exception ex)
            {
                attemps += 1;
                if (attemps <= 99)
                {
                    PrinstScn.WriteWarningLine(string.Format(">>>>> Retry {0} GetInfoSysObjectsBasic {1} Erro: [{2}]", tableName, attemps, ex.Message));
                    Thread.Sleep(2000 * attemps);
                    return GetInfoSysObjectsBasic(config, tableName, attemps);
                }

                throw new InvalidOperationException($"Attemps Limit Reached - [{ex.Message}]");
            }

        }

        private void DefineFieldFilterDefault(Context config, TableInfo tableInfo)
        {
            foreach (var rc in tableInfo.ReletedClass)
            {
                var reader = GetInfoSysObjectsBasic(config, rc.Table);
                while (reader.Read())
                    rc.FieldFilterDefault = reader["NomeColuna"].ToString();

            }
        }

        private void DefineDataItemFieldName(IEnumerable<Info> infos, TableInfo tableInfo)
        {
            var dataItemFieldName = infos
                .Where(_ => !_.ColumnName.ToLower().Contains("id"))
                .Take(1)
                .SingleOrDefault();

            if (dataItemFieldName.IsNotNull())
                tableInfo.DataItemFieldName = dataItemFieldName.PropertyName;
            else
                tableInfo.DataItemFieldName = tableInfo.Keys.FirstOrDefault();

        }

        protected UniqueListInfo CastOrdenabledToUniqueListInfo(UniqueListInfo infos)
        {
            var infosOrder = new UniqueListInfo();
            infosOrder.AddRange(infos.OrderBy(_ => _.Order));
            return infosOrder;
        }

        protected UniqueListInfo CastOrdenabledToUniqueListInfoFilter(UniqueListInfo infos)
        {
            var infosOrder = new UniqueListInfo();
            infosOrder.AddRange(infos.OrderBy(_ => _.OrderFilter));
            return infosOrder;
        }

        protected virtual string MakeKeyNames(TableInfo tableInfo, EOperation operation)
        {
            var keys = string.Empty;
            if (tableInfo.Keys.IsAny())
            {
                foreach (var item in tableInfo.Keys)
                    keys += string.Format("model.{0},", item);

                keys = keys.Substring(0, keys.Length - 1);
            }
            return keys;
        }

        protected virtual string ExpressionKeyNames(TableInfo tableInfo, bool camelCasing, EOperation operation = EOperation.Undefined)
        {
            return "define yourself";
        }

        protected virtual string ParametersKeyNames(TableInfo tableInfo, bool camelCasing, EOperation operation = EOperation.Undefined, string variable = "item", Func<string, string> camelCaseTransform = null)
        {
            return HelperSysObjectUtil.ParametersKeyNames(tableInfo, camelCasing, operation, variable, camelCaseTransform);
        }

        protected virtual string DataitemAux(Dictionary<string, string> dataitem)
        {
            var parameters = "[";
            if (dataitem.IsAny())
            {
                foreach (var item in dataitem)
                {
                    parameters += "{ id : '" + item.Key.Replace("'", "") + "', name: '" + item.Value.Replace("'", "") + "' },";
                }
            }
            parameters = parameters.Substring(0, parameters.Length - 1) + "]";
            return parameters;
        }

        private bool IsNotString(Info item)
        {
            return item.Type != "string";
        }
        private bool IsNotDataRage(string propertyName)
        {
            return !propertyName.Contains("Inicio") &&
                                    !propertyName.Contains("Start") &&
                                    !propertyName.Contains("End") &&
                                    !propertyName.Contains("Fim");
        }

        private string convertTypeToTypeTS(string type)
        {


            if (type == "string" || type == "DateTime" || type == "DateTime?" || type == "Guid" || type == "Guid?")
                return "string";

            else if (type == "bool")
                return "boolean";

            if (type == "bool?")
                return "boolean";

            else if (type == "int" || type == "float" || type == "decimal" || type == "Int16" || type == "Int32" || type == "Int64")
                return "number";

            else if (type == "int?" || type == "float?" || type == "decimal?" || type == "Int16?" || type == "Int32?" || type == "Int64?")
                return "number";


            return type;
        }
        private string MakeReletedNamespace(TableInfo tableInfo, Context configContext, string classBuilder)
        {
            return HelperSysObjectUtil.MakeReletedNamespace(tableInfo, configContext, classBuilder);
        }
        private string MakeKey(IEnumerable<Info> infos, string textTemplateCompositeKey, string classBuilderitemplateCompositeKey)
        {
            var compositeKey = infos.Where(_ => _.IsKey == 1);
            if (compositeKey.IsAny())
            {
                var CompositeKeys = string.Empty;
                foreach (var item in compositeKey)
                    CompositeKeys += string.Format("d.{0},", item.PropertyName);


                var itemTemplateCompositeKey = textTemplateCompositeKey
                          .Replace("<#Keys#>", CompositeKeys);

                classBuilderitemplateCompositeKey = string.Format("{0}{1}", Tabs.TabMaps(), itemTemplateCompositeKey);
            }
            return classBuilderitemplateCompositeKey;
        }

        protected bool FieldInBlackListSave(TableInfo tableInfo, string propertyName)
        {
            if (tableInfo.FieldsConfig.IsNotAny())
                return false;

            return tableInfo.FieldsConfig
                .Where(_ => _.Name.ToUpper() == propertyName.ToUpper())
                .Where(_ => _.Create == false)
                .IsAny();
        }

        protected bool FieldInBlackListFilter(TableInfo tableInfo, string propertyName)
        {
            if (tableInfo.FieldsConfig.IsNotAny())
                return false;

            return tableInfo.FieldsConfig
                .Where(_ => _.Name.ToUpper() == propertyName.ToUpper())
                .Where(_ => _.Filter == false)
                .IsAny();
        }


        #endregion

        protected void Dispose()
        {
            if (this.conn != null)
            {
                this.conn.Close();
                this.conn.Dispose();
            }
        }
    }
}
