using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;

namespace Common.Gen
{
    public class HelperSysObjectsTransaction : HelperSysObjectsBaseBack
    {

        public HelperSysObjectsTransaction(IEnumerable<Context> contexts) : this(contexts, "Templates\\Back")
        {

        }
        public HelperSysObjectsTransaction(IEnumerable<Context> contexts, string template)
        {
            this.Contexts = contexts;
            foreach (var item in contexts)
                item.UsePathProjects = true;

            this._defineTemplateFolder = new DefineTemplateFolder();
            this._defineTemplateFolder.SetTemplatePathBase(template);
            base.ArquitetureType = ArquitetureType.TransactionScript;

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

        public override void DefineTemplateByTableInfoFieldsBack(Context configContext, TableInfo tableInfo, UniqueListInfo infos)
        {
            var overrideFile = configContext.OverrideFiles;
            if (configContext.MakeBack)
            {
                ExecuteTemplate(new ConfigExecutetemplate
                {
                    TableInfo = tableInfo,
                    Infos = infos,
                    ConfigContext = configContext,
                    PathOutput = PathOutputTransactionScript.PathOutputTransactionScriptEntityRepository(tableInfo, configContext),
                    Template = DefineTemplateNameTransactionScript.TransactionScriptEntityRepository(tableInfo),
                    TemplateFields = new List<TemplateField> {
                        new TemplateField
                        {
                            TemplateName =  DefineTemplateNameTransactionScript.TransactionScriptParameters(tableInfo),
                            TagTemplate = "<#property#>"
                        }
                    },
                    WithRestrictions = false,
                    Operation = EOperation.Back_Transaction_EntityRepository,
                    Flow = EFlowTemplate.Field,
                    Layer = ELayer.Back,
                    OverrideFile = overrideFile
                });

                ExecuteTemplate(new ConfigExecutetemplate
                {
                    TableInfo = tableInfo,
                    ConfigContext = configContext,
                    Infos = infos,
                    PathOutput = PathOutputTransactionScript.PathOutputTransactionScriptDto(tableInfo, configContext),
                    Template = DefineTemplateNameTransactionScript.TransactionScriptDto(tableInfo),
                    TemplateFields = new List<TemplateField> {
                        new TemplateField
                        {
                            TemplateName =  DefineTemplateNameTransactionScript.TransactionScriptProperty(tableInfo),
                            TagTemplate = "<#property#>"
                        }
                    },
                    WithRestrictions = false,
                    Operation = EOperation.Back_Transaction_Dto,
                    Flow = EFlowTemplate.Field,
                    Layer = ELayer.Back


                });

                ExecuteTemplate(new ConfigExecutetemplate
                {
                    TableInfo = tableInfo,
                    ConfigContext = configContext,
                    Infos = infos,
                    PathOutput = PathOutputTransactionScript.PathOutputTransactionScriptDtoSpecialized(tableInfo, configContext),
                    Template = DefineTemplateNameTransactionScript.TransactionScriptDtoSpecialized(tableInfo),
                    TemplateFields = new List<TemplateField> {
                        new TemplateField
                        {
                            TemplateName =  DefineTemplateNameTransactionScript.TransactionScriptProperty(tableInfo),
                            TagTemplate = "<#property#>"
                        }
                    },
                    Operation = EOperation.Back_Transaction_DtoSpecialized,
                    Flow = EFlowTemplate.Field,
                    Layer = ELayer.Back,
                    OverrideFile = overrideFile
                });

                ExecuteTemplate(new ConfigExecutetemplate
                {
                    TableInfo = tableInfo,
                    ConfigContext = configContext,
                    Infos = infos,
                    PathOutput = PathOutputTransactionScript.PathOutputFilter(tableInfo, configContext),
                    Template = DefineTemplateNameTransactionScript.TransactionScriptFilter(tableInfo),
                    TemplateFields = new List<TemplateField> {
                        new TemplateField
                        {
                            TemplateName =  DefineTemplateNameTransactionScript.TransactionScriptProperty(tableInfo),
                            TagTemplate = "<#property#>"
                        }
                    },
                    WithRestrictions = false,
                    Operation = EOperation.Back_Transaction_Filter,
                    Flow = EFlowTemplate.Field,
                    Layer = ELayer.Back,
                });

                ExecuteTemplate(new ConfigExecutetemplate
                {
                    TableInfo = tableInfo,
                    ConfigContext = configContext,
                    Infos = infos,
                    PathOutput = PathOutputTransactionScript.PathOutputFilterPartial(tableInfo, configContext),
                    Template = DefineTemplateNameTransactionScript.TransactionScriptFilterPartial(tableInfo),
                    TemplateFields = new List<TemplateField> {
                        new TemplateField
                        {
                            TemplateName =  DefineTemplateNameTransactionScript.TransactionScriptProperty(tableInfo),
                            TagTemplate = "<#property#>"
                        }
                    },
                    Operation = EOperation.Back_Transaction_FilterPartial,
                    Flow = EFlowTemplate.Field,
                    Layer = ELayer.Back,
                    OverrideFile = overrideFile
                });

            }

        }

        public override void DefineTemplateByTableInfoBack(Context configContext, TableInfo tableInfo)
        {
            var overrideFile = configContext.OverrideFiles;
            if (configContext.MakeBack)
            {

                ExecuteTemplate(new ConfigExecutetemplate
                {
                    ConfigContext = configContext,
                    TableInfo = tableInfo,
                    PathOutput = PathOutputTransactionScript.PathOutputTransactionScriptApiContainer(tableInfo, configContext),
                    Template = DefineTemplateNameTransactionScript.TransactionScriptApiContainer(tableInfo),
                    TemplateClassItem = new List<TemplateClass>{
                        new TemplateClass {

                            TemplateName = DefineTemplateNameTransactionScript.TransactionScriptApiContainerInjections(tableInfo),
                            TagTemplate = "<#injections#>"
                        },

                    },
                    Flow = EFlowTemplate.Class,
                    Layer = ELayer.Back,
                    Operation = EOperation.Back_Transaction_ApiContainer,
                    OverrideFile = overrideFile
                });

                ExecuteTemplate(new ConfigExecutetemplate
                {
                    ConfigContext = configContext,
                    TableInfo = tableInfo,
                    PathOutput = PathOutputTransactionScript.PathOutputTransactionScriptApiContainerPartial(tableInfo, configContext),
                    Template = DefineTemplateNameTransactionScript.TransactionScriptApiContainerPartial(tableInfo),
                    Flow = EFlowTemplate.Class,
                    Layer = ELayer.Back,
                    Operation = EOperation.Back_Transaction_ApiContainerPartial
                });

                ExecuteTemplate(new ConfigExecutetemplate
                {
                    ConfigContext = configContext,
                    TableInfo = tableInfo,
                    PathOutput = PathOutputTransactionScript.PathOutputTransactionScriptApiAppSettings(tableInfo, configContext),
                    Template = DefineTemplateNameTransactionScript.TransactionScriptApiAppSettings(tableInfo),
                    Flow = EFlowTemplate.Static,
                    Layer = ELayer.Back,
                    Operation = EOperation.Back_Transaction_ApiAppSettings,
                    OverrideFile = overrideFile
                });

                ExecuteTemplate(new ConfigExecutetemplate
                {
                    TableInfo = tableInfo,
                    ConfigContext = configContext,
                    PathOutput = PathOutputTransactionScript.PathOutputTransactionScriptApi(tableInfo, configContext),
                    Template = DefineTemplateNameTransactionScript.TransactionScriptApi(tableInfo),
                    Operation = EOperation.Back_Transaction_Api,
                    Flow = EFlowTemplate.Static,
                    Layer = ELayer.Back,
                    OverrideFile = overrideFile
                });



                ExecuteTemplate(new ConfigExecutetemplate
                {
                    TableInfo = tableInfo,
                    ConfigContext = configContext,
                    PathOutput = PathOutputTransactionScript.PathOutputTransactionScriptIEntityRepository(tableInfo, configContext),
                    Template = DefineTemplateNameTransactionScript.TransactionScriptIEntityRepository(tableInfo),
                    Operation = EOperation.Back_Transaction_IEntityRepository,
                    Flow = EFlowTemplate.Static,
                    Layer = ELayer.Back,
                    OverrideFile = overrideFile
                });

                ExecuteTemplate(new ConfigExecutetemplate
                {
                    TableInfo = tableInfo,
                    ConfigContext = configContext,
                    PathOutput = PathOutputTransactionScript.PathOutputTransactionScriptApiStart(tableInfo, configContext),
                    Template = DefineTemplateNameTransactionScript.TransactionScriptApiStart(tableInfo),
                    Operation = EOperation.Back_Transaction_ApiStart,
                    Flow = EFlowTemplate.Static,
                    Layer = ELayer.Back,
                    OverrideFile = overrideFile
                });

                ExecuteTemplate(new ConfigExecutetemplate
                {
                    TableInfo = tableInfo,
                    ConfigContext = configContext,
                    PathOutput = PathOutputTransactionScript.PathOutputTransactionScriptApiCurrentUser(tableInfo, configContext),
                    Template = DefineTemplateNameTransactionScript.TransactionScriptApiCurrentUser(tableInfo),
                    Operation = EOperation.Back_Transaction_ApiCurrentUser,
                    Flow = EFlowTemplate.Static,
                    Layer = ELayer.Back,
                    OverrideFile = overrideFile
                });

                ExecuteTemplate(new ConfigExecutetemplate
                {
                    TableInfo = tableInfo,
                    ConfigContext = configContext,
                    PathOutput = PathOutputTransactionScript.PathOutputTransactionScriptApiDownload(tableInfo, configContext),
                    Template = DefineTemplateNameTransactionScript.TransactionScriptApiDownload(tableInfo),
                    Operation = EOperation.Back_Transaction_ApiDonwlod,
                    Flow = EFlowTemplate.Static,
                    Layer = ELayer.Back,
                    OverrideFile = overrideFile
                });


                ExecuteTemplate(new ConfigExecutetemplate
                {
                    TableInfo = tableInfo,
                    ConfigContext = configContext,
                    PathOutput = PathOutputTransactionScript.PathOutputTransactionScriptApiUpload(tableInfo, configContext),
                    Template = DefineTemplateNameTransactionScript.TransactionScriptApiUpload(tableInfo),
                    Operation = EOperation.Back_Transaction_ApiUpload,
                    Flow = EFlowTemplate.Static,
                    Layer = ELayer.Back,
                    OverrideFile = overrideFile
                });


                ExecuteTemplate(new ConfigExecutetemplate
                {
                    TableInfo = tableInfo,
                    ConfigContext = configContext,
                    PathOutput = PathOutputTransactionScript.PathOutputTransactionScriptApiHealth(tableInfo, configContext),
                    Template = DefineTemplateNameTransactionScript.TransactionScriptApiHealth(tableInfo),
                    Operation = EOperation.Back_Transaction_ApiHealth,
                    Flow = EFlowTemplate.Static,
                    Layer = ELayer.Back,
                    OverrideFile = overrideFile
                });
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

        protected override string CamelCaseTransform(string str)
        {
            return str;
        }

        #region Define Field by Types


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
