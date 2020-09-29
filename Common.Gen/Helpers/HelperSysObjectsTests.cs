using Common.Gen.Utils;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Gen
{
    public class HelperSysObjectsCleanTest : HelperSysObjectsBase
    {

        public HelperSysObjectsCleanTest(IEnumerable<Context> contexts)
        {
            this.Contexts = contexts;
            foreach (var item in contexts)
                item.UsePathProjects = true;

            this._defineTemplateFolder = new DefineTemplateFolder();
        }
        public override void DefineTemplateByTableInfoFields(Context config, TableInfo tableInfo, UniqueListInfo infos)
        {
            throw new NotImplementedException();
        }

        public override void DefineTemplateByTableInfo(Context config, TableInfo tableInfo)
        {
            throw new NotImplementedException();
        }

        #region Execute Templates

        private void ExecuteTemplateAppTests(TableInfo tableInfo, Context configContext, IEnumerable<Info> infos)
        {
            var pathOutput = PathOutput.PathOutputApplicationTest(tableInfo, configContext);

            if (!tableInfo.MakeTest)
                return;

            if (!tableInfo.MakeApp)
                return;

            var pathTemplateClass = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, this._defineTemplateFolder.Define(tableInfo), DefineTemplateName.AppTest(tableInfo));

            var textTemplateClass = Read.AllText(tableInfo, pathTemplateClass, this._defineTemplateFolder);

            var classBuilder = GenericTagsTransformer(tableInfo, configContext, textTemplateClass);
            using (var stream = new HelperStream(pathOutput).GetInstance())
            {
                stream.Write(classBuilder);
            }

        }
        private void ExecuteTemplateAppTestsMoq(TableInfo tableInfo, Context configContext, IEnumerable<Info> infos)
        {


            if (!tableInfo.MakeApp)
                return;


            var pathOutput = PathOutput.PathOutputApplicationTestMoq(tableInfo, configContext);
            var pathTemplateClass = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, this._defineTemplateFolder.Define(tableInfo), DefineTemplateName.AppTestMoq(tableInfo));
            if (!File.Exists(pathTemplateClass))
                return;

            var pathTemplateMoqValues = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, this._defineTemplateFolder.Define(tableInfo), DefineTemplateName.AppTestMoqValues(tableInfo));

            var textTemplateClass = Read.AllText(tableInfo, pathTemplateClass, this._defineTemplateFolder);
            var TextTemplateMoqValues = Read.AllText(tableInfo, pathTemplateMoqValues, this._defineTemplateFolder);

            var classBuilder = GenericTagsTransformer(tableInfo, configContext, textTemplateClass);

            var classBuilderMoqValues = string.Empty;

            foreach (var item in infos)
            {

                if (item.IsKey == 1)
                    continue;

                if (Audit.IsAuditField(item.PropertyName))
                    continue;

                var itemvalue = TextTemplateMoqValues.
                        Replace("<#propertyName#>", item.PropertyName).
                        Replace("<#length#>", IsString(item) && IsNotVarcharMax(item) ? item.Length : string.Empty).
                        Replace("<#moqMethod#>", DefineMoqMethd(item.Type));

                classBuilderMoqValues += string.Format("{0}{1}", itemvalue, System.Environment.NewLine);

            }

            classBuilder = classBuilder.Replace("<#moqValuesinsert#>", classBuilderMoqValues);

            using (var stream = new HelperStream(pathOutput).GetInstance())
            {
                stream.Write(classBuilder);
            }

        }
        private void ExecuteTemplateAppTestsMoqPartial(TableInfo tableInfo, Context configContext, IEnumerable<Info> infos)
        {
            var pathOutput = PathOutput.PathOutputApplicationTestMoqPartial(tableInfo, configContext);

            if (!tableInfo.MakeApp)
                return;

            if (File.Exists(pathOutput) && tableInfo.CodeCustomImplemented)
                return;

            if (File.Exists(pathOutput) && Convert.ToBoolean(ConfigurationManager.AppSettings["GerarMoqClassPartialExistentes"]) == false)
                return;

            var pathTemplateClass = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, this._defineTemplateFolder.Define(tableInfo), DefineTemplateName.AppTestMoqPartial(tableInfo));
            var pathTemplateReletedValues = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, this._defineTemplateFolder.Define(tableInfo), DefineTemplateName.AppTestReleted(tableInfo));

            var textTemplateClass = Read.AllText(tableInfo, pathTemplateClass, this._defineTemplateFolder);
            var TextTemplateReletedValues = Read.AllText(tableInfo, pathTemplateReletedValues, this._defineTemplateFolder);

            var classBuilder = GenericTagsTransformer(tableInfo, configContext, textTemplateClass);
            classBuilder = MakeReletedIntanceValues(tableInfo, configContext, TextTemplateReletedValues, classBuilder);

            var classBuilderMoqValues = string.Empty;


            using (var stream = new HelperStream(pathOutput).GetInstance())
            {
                stream.Write(classBuilder);
            }

        }
        private void ExecuteTemplateApiTests(TableInfo tableInfo, Context configContext, IEnumerable<Info> infos)
        {
            var pathOutput = PathOutput.PathOutputApiTest(tableInfo, configContext);

            if (!tableInfo.MakeTest)
                return;

            if (!tableInfo.MakeApi)
                return;


            var pathTemplateClass = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, this._defineTemplateFolder.Define(tableInfo), DefineTemplateName.ApiTest(tableInfo));
            if (!File.Exists(pathTemplateClass))
                return;

            var pathTemplateMoqValues = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, this._defineTemplateFolder.Define(tableInfo), DefineTemplateName.ApiTestMoqValues(tableInfo));

            var textTemplateClass = Read.AllText(tableInfo, pathTemplateClass, this._defineTemplateFolder);
            var TextTemplateMoqValues = Read.AllText(tableInfo, pathTemplateMoqValues, this._defineTemplateFolder);


            var classBuilder = GenericTagsTransformer(tableInfo, configContext, textTemplateClass);
            var classBuilderMoqValues = string.Empty;

            foreach (var item in infos)
            {

                if (item.IsKey == 1)
                    continue;

                if (Audit.IsAuditField(item.PropertyName))
                    continue;

                var itemvalue = TextTemplateMoqValues.
                        Replace("<#propertyName#>", item.PropertyName).
                        Replace("<#length#>", item.Type == "string" ? item.Length : string.Empty).
                        Replace("<#moqMethod#>", DefineMoqMethd(item.Type));

                classBuilderMoqValues += string.Format("{0}{1}{2}", Tabs.TabSets(), itemvalue, System.Environment.NewLine);

            }

            classBuilder = classBuilder.Replace("<#moqValuesinsert#>", classBuilderMoqValues);


            using (var stream = new HelperStream(pathOutput).GetInstance())
            {
                stream.Write(classBuilder);
            }

        }
        private void ExecuteTemplateApiTestsBasic(TableInfo tableInfo, Context configContext)
        {
            var pathOutput = PathOutput.PathOutputApiTest(tableInfo, configContext);

            if (File.Exists(pathOutput))
                return;

            if (!tableInfo.MakeTest)
                return;

            if (!tableInfo.MakeApi)
                return;


            var pathTemplateClass = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, this._defineTemplateFolder.Define(tableInfo), DefineTemplateName.ApiTest(tableInfo));
            var textTemplateClass = Read.AllText(tableInfo, pathTemplateClass, this._defineTemplateFolder);

            var classBuilder = GenericTagsTransformer(tableInfo, configContext, textTemplateClass);

            classBuilder = classBuilder.Replace("<#filterByModel#>", MakeKFilterByModel(tableInfo));

            using (var stream = new HelperStream(pathOutput).GetInstance())
            {
                stream.Write(classBuilder);
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
