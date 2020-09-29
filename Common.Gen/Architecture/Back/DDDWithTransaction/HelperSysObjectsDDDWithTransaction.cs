using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Gen
{
    public class HelperSysObjectsDDDWithTransaction : HelperSysObjectsBaseBack
    {

        private HelperSysObjectsTransaction _transaction;
        private HelperSysObjectsDDD _ddd;

        public HelperSysObjectsDDDWithTransaction(IEnumerable<Context> contexts)
        {
            this.Contexts = contexts;

            var contextTransaction = this.Contexts.Where(_ => _.Arquiteture == ArquitetureType.TransactionScript);
            var contextDDD = this.Contexts.Where(_ => _.Arquiteture == ArquitetureType.DDD);
            var TemplatePathBackDDD = contextDDD.IsAny() ? contextDDD.FirstOrDefault().TemplatePathBack : string.Empty;
            var TemplatePathBackTransaction = contextTransaction.IsAny() ?  contextTransaction.FirstOrDefault().TemplatePathBack: string.Empty;


            if (contextTransaction.IsAny())
                this._transaction = new HelperSysObjectsTransaction(contextTransaction, TemplatePathBackTransaction);

            if (contextDDD.IsAny())
                this._ddd = new HelperSysObjectsDDD(contextDDD, TemplatePathBackDDD);
        }
       
        public virtual HelperSysObjectsBase DefineFrontTemplateClass(Context config)
        {
            return new HelperSysObjectsAngular20(config);
        }

        public override void DefineTemplateByTableInfo(Context config, TableInfo tableInfo)
        {
            if (config.Arquiteture == ArquitetureType.TransactionScript)
                this._transaction.DefineTemplateByTableInfo(config, tableInfo);

            if (config.Arquiteture == ArquitetureType.DDD)
                this._ddd.DefineTemplateByTableInfo(config, tableInfo);
        }

        public override void DefineTemplateByTableInfoFields(Context config, TableInfo tableInfo, UniqueListInfo infos)
        {
            if (config.Arquiteture == ArquitetureType.TransactionScript)
                this._transaction.DefineTemplateByTableInfoFields(config, tableInfo, infos);

            if (config.Arquiteture == ArquitetureType.DDD)
                this._ddd.DefineTemplateByTableInfoFields(config, tableInfo, infos);
        }

        public override void DefineTemplateByTableInfoBack(Context config, TableInfo tableInfo)
        {
            if (config.Arquiteture == ArquitetureType.TransactionScript)
                this._transaction.DefineTemplateByTableInfoBack(config, tableInfo);

            if (config.Arquiteture == ArquitetureType.DDD)
                this._ddd.DefineTemplateByTableInfoBack(config, tableInfo);
        }

        public override void DefineTemplateByTableInfoFieldsBack(Context config, TableInfo tableInfo, UniqueListInfo infos)
        {
            if (config.Arquiteture == ArquitetureType.TransactionScript)
                this._transaction.DefineTemplateByTableInfoFieldsBack(config, tableInfo, infos);

            if (config.Arquiteture == ArquitetureType.DDD)
                this._ddd.DefineTemplateByTableInfoFieldsBack(config, tableInfo, infos);
        }

        public override void DefineTemplateByTableInfoFieldsFront(Context config, TableInfo tableInfo, UniqueListInfo infos)
        {
            if (config.Arquiteture == ArquitetureType.TransactionScript)
                this._transaction.DefineTemplateByTableInfoFieldsFront(config, tableInfo, infos);

            if (config.Arquiteture == ArquitetureType.DDD)
                this._ddd.DefineTemplateByTableInfoFieldsFront(config, tableInfo, infos);
        }

        public override void DefineTemplateByTableInfoFront(Context config, TableInfo tableInfo)
        {
            if (config.Arquiteture == ArquitetureType.TransactionScript)
                this._transaction.DefineTemplateByTableInfoFront(config, tableInfo);

            if (config.Arquiteture == ArquitetureType.DDD)
                this._ddd.DefineTemplateByTableInfoFront(config, tableInfo);
        }



        #region TransformField

        public override string TransformFieldBool(ConfigExecutetemplate configExecutetemplate, Info info, string propertyName, string textTemplate)
        {
            if (configExecutetemplate.ConfigContext.Arquiteture == ArquitetureType.TransactionScript)
                return this._transaction.TransformFieldBool(configExecutetemplate, info, propertyName, textTemplate);

            return this._ddd.TransformFieldBool(configExecutetemplate, info, propertyName, textTemplate);
        }

        public override string TransformFieldDateTime(ConfigExecutetemplate configExecutetemplate, Info info, string propertyName, string textTemplate, bool onlyDate = false)
        {
            if (configExecutetemplate.ConfigContext.Arquiteture == ArquitetureType.TransactionScript)
                return this._transaction.TransformFieldDateTime(configExecutetemplate, info, propertyName, textTemplate);

            return this._ddd.TransformFieldDateTime(configExecutetemplate, info, propertyName, textTemplate);

        }

        public override string TransformFieldHtml(ConfigExecutetemplate configExecutetemplate, Info info, string propertyName, string textTemplate)
        {
            if (configExecutetemplate.ConfigContext.Arquiteture == ArquitetureType.TransactionScript)
                return this._transaction.TransformFieldHtml(configExecutetemplate, info, propertyName, textTemplate);

            return this._ddd.TransformFieldHtml(configExecutetemplate, info, propertyName, textTemplate);
        }

        public override string TransformFieldPropertyNavigation(ConfigExecutetemplate configExecutetemplate, Info info, string propertyName, string textTemplate)
        {
            if (configExecutetemplate.ConfigContext.Arquiteture == ArquitetureType.TransactionScript)
                return this._transaction.TransformFieldPropertyNavigation(configExecutetemplate, info, propertyName, textTemplate);

            return this._ddd.TransformFieldPropertyNavigation(configExecutetemplate, info, propertyName, textTemplate);
        }

       
        public override string TransformFieldString(ConfigExecutetemplate configExecutetemplate, Info info, string propertyName, string textTemplate)
        {

            if (configExecutetemplate.ConfigContext.Arquiteture == ArquitetureType.TransactionScript)
                return this._transaction.TransformFieldString(configExecutetemplate, info, propertyName, textTemplate);

            return this._ddd.TransformFieldString(configExecutetemplate, info, propertyName, textTemplate);
        }

        public override string TransformFieldTextEditor(ConfigExecutetemplate configExecutetemplate, Info info, string propertyName, string textTemplate)
        {
            if (configExecutetemplate.ConfigContext.Arquiteture == ArquitetureType.TransactionScript)
                return this._transaction.TransformFieldTextEditor(configExecutetemplate, info, propertyName, textTemplate);

            return this._ddd.TransformFieldTextEditor(configExecutetemplate, info, propertyName, textTemplate);
        }

        public override string TransformFieldTextStyle(ConfigExecutetemplate configExecutetemplate, Info info, string propertyName, string textTemplate)
        {
            if (configExecutetemplate.ConfigContext.Arquiteture == ArquitetureType.TransactionScript)
                return this._transaction.TransformFieldTextStyle(configExecutetemplate, info, propertyName, textTemplate);

            return this._ddd.TransformFieldTextStyle(configExecutetemplate, info, propertyName, textTemplate);
        }

        public override string TransformFieldUpload(ConfigExecutetemplate configExecutetemplate, Info info, string propertyName, string textTemplate)
        {
            if (configExecutetemplate.ConfigContext.Arquiteture == ArquitetureType.TransactionScript)
                return this._transaction.TransformFieldUpload(configExecutetemplate, info, propertyName, textTemplate);

            return this._ddd.TransformFieldUpload(configExecutetemplate, info, propertyName, textTemplate);
        }

        public override string TransformFieldTextTag(ConfigExecutetemplate configExecutetemplate, Info info, string propertyName, string textTemplate)
        {
            throw new NotImplementedException();
        }

        #endregion

    }
}
