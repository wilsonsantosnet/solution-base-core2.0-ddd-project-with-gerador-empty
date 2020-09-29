using Common.Gen.Utils;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;


namespace Common.Gen
{
    public class HelperSysObjectsAngularJs : HelperSysObjectsBaseFront
    {


        public HelperSysObjectsAngularJs(Context context) : this(context, "Templates\\Front")
        {

        }
        public HelperSysObjectsAngularJs(Context context, string template)
        {
            var _contexts = new List<Context> {
                context
            };

            this.Contexts = _contexts;
            context.UsePathProjects = true;
            this._defineTemplateFolder = new DefineTemplateFolder();
            this._defineTemplateFolder.SetTemplatePathBase(template);
        }
        public HelperSysObjectsAngularJs(IEnumerable<Context> contexts)
        {
            this.Contexts = contexts;
            foreach (var item in contexts)
                item.UsePathProjects = true;
        }

        public override void DefineTemplateByTableInfoFields(Context configContext, TableInfo tableInfo, UniqueListInfo infos)
        {
            base.DefineTemplateByTableInfoFields(configContext, tableInfo, infos);

            ExecuteTemplateAngularModalCreate(tableInfo, configContext, infos);
            ExecuteTemplateAngularModalEdit(tableInfo, configContext, infos);
            ExecuteTemplateAngularDetails(tableInfo, configContext, infos);
            ExecuteTemplateAngularFormCreate(tableInfo, configContext, infos);
            ExecuteTemplateAngularFormEdit(tableInfo, configContext, infos);
            ExecuteTemplateAngularDetailsFields(tableInfo, configContext, infos);
            ExecuteTemplateAngularEditInPage(tableInfo, configContext, infos);
            ExecuteTemplateAngularCreateInPage(tableInfo, configContext, infos);
            ExecuteTemplateAngularView(tableInfo, configContext, infos);
            ExecuteTemplateAngularViewFilters(tableInfo, configContext, infos);
            ExecuteTemplateAngularViewGrid(tableInfo, configContext, infos);
            ExecuteTemplateAngularLabelsConstants(tableInfo, configContext, infos);
        }
        public override void DefineTemplateByTableInfo(Context config, TableInfo tableInfo)
        {
            ExecuteTemplateAngularController(tableInfo, config);
            ExecuteTemplateAngularControllerCustom(tableInfo, config);
            ExecuteTemplateAngularRoutes(tableInfo, config);
            ExecuteTemplateAngularAccountService(tableInfo, config);
            ExecuteTemplateAngularInitApp(tableInfo, config);
            ExecuteTemplateAngularInitModules(tableInfo, config);
            ExecuteTemplateAngularConstants(tableInfo, config);
            ExecuteTemplateAngularbreadcrumbConstants(tableInfo, config);
            ExecuteTemplateAngularIndex(tableInfo, config);
            ExecuteTemplateAngularLayout(tableInfo, config);
            ExecuteTemplateAngularMainCustomController(tableInfo, config);
            ExecuteTemplateAngularHomeCustomController(tableInfo, config);
            ExecuteTemplateAngularLoginCustomController(tableInfo, config);
            ExecuteTemplateAngularHomeView(tableInfo, config);
            ExecuteTemplateAngularLoginView(tableInfo, config);
            ExecuteTemplateAngularRouteConfigCustom(tableInfo, config);
            ExecuteTemplateAngularValueConfig(tableInfo, config);
            ExecuteTemplateAngularTokenConstants(tableInfo, config);
            ExecuteTemplateAngularPackage(tableInfo, config);
            ExecuteTemplateAngularBreadCrumbSharedView(tableInfo, config);
            ExecuteTemplateAngularExclusaoSharedView(tableInfo, config);
            ExecuteTemplateAngularExecuteSharedView(tableInfo, config);
            ExecuteTemplateAngularHeaderSharedView(tableInfo, config);
            ExecuteTemplateAngularMenuSharedView(tableInfo, config);
            ExecuteTemplateAngularUnauthorizedSharedView(tableInfo, config);
        }


        #region Execute Templates

        private void ExecuteTemplateAngularHomeView(TableInfo tableInfo, Context configContext)
        {
            if (tableInfo.CodeCustomImplemented)
                return;

            if (!tableInfo.MakeFront)
                return;

            var pathOutput = PathOutput.PathOutputAngularHomeView(tableInfo, configContext);
            if (File.Exists(pathOutput))
                return;

            var pathTemplate = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, this._defineTemplateFolder.Define(tableInfo), DefineTemplateName.AngularHomeView(tableInfo));
            var textTemplate = Read.AllText(tableInfo, pathTemplate, this._defineTemplateFolder);

            var classBuilderModal = GenericTagsTransformer(tableInfo, configContext, textTemplate);
            var classBuilderForm = string.Empty;


            using (var stream = new HelperStream(pathOutput).GetInstance()) { stream.Write(classBuilderModal); }
        }

        private void ExecuteTemplateAngularLoginView(TableInfo tableInfo, Context configContext)
        {
            if (tableInfo.CodeCustomImplemented)
                return;

            if (!tableInfo.MakeFront)
                return;

            var pathOutput = PathOutput.PathOutputAngularLoginView(tableInfo, configContext);
            if (File.Exists(pathOutput))
                return;

            var pathTemplate = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, this._defineTemplateFolder.Define(tableInfo), DefineTemplateName.AngularLoginView(tableInfo));
            var textTemplate = Read.AllText(tableInfo, pathTemplate, this._defineTemplateFolder);

            var classBuilderModal = GenericTagsTransformer(tableInfo, configContext, textTemplate);
            var classBuilderForm = string.Empty;


            using (var stream = new HelperStream(pathOutput).GetInstance()) { stream.Write(classBuilderModal); }
        }

        private void ExecuteTemplateAngularRouteConfigCustom(TableInfo tableInfo, Context configContext)
        {
            if (tableInfo.CodeCustomImplemented)
                return;

            if (!tableInfo.MakeFront)
                return;

            var pathOutput = PathOutput.PathOutputAngularRouteConfigCustom(tableInfo, configContext);
            if (File.Exists(pathOutput))
                return;

            var pathTemplate = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, this._defineTemplateFolder.Define(tableInfo), DefineTemplateName.AngularRouteConfigCustom(tableInfo));
            var textTemplate = Read.AllText(tableInfo, pathTemplate, this._defineTemplateFolder);
            var classBuilderModal = GenericTagsTransformer(tableInfo, configContext, textTemplate);
            var classBuilderForm = string.Empty;


            using (var stream = new HelperStream(pathOutput).GetInstance()) { stream.Write(classBuilderModal); }
        }

        private void ExecuteTemplateAngularValueConfig(TableInfo tableInfo, Context configContext)
        {
            if (tableInfo.CodeCustomImplemented)
                return;

            if (!tableInfo.MakeFront)
                return;

            var pathOutput = PathOutput.PathOutputAngularValueConfig(tableInfo, configContext);
            if (File.Exists(pathOutput))
                return;

            var pathTemplate = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, this._defineTemplateFolder.Define(tableInfo), DefineTemplateName.AngularValueConfig(tableInfo));
            var textTemplate = Read.AllText(tableInfo, pathTemplate, this._defineTemplateFolder);
            var classBuilderModal = GenericTagsTransformer(tableInfo, configContext, textTemplate);
            var classBuilderForm = string.Empty;


            using (var stream = new HelperStream(pathOutput).GetInstance()) { stream.Write(classBuilderModal); }
        }

        private void ExecuteTemplateAngularTokenConstants(TableInfo tableInfo, Context configContext)
        {
            if (tableInfo.CodeCustomImplemented)
                return;

            if (!tableInfo.MakeFront)
                return;

            var pathOutput = PathOutput.PathOutputAngularTokenConstants(tableInfo, configContext);
            if (File.Exists(pathOutput))
                return;

            var pathTemplate = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, this._defineTemplateFolder.Define(tableInfo), DefineTemplateName.AngularTokenConstants(tableInfo));
            var textTemplate = Read.AllText(tableInfo, pathTemplate, this._defineTemplateFolder);
            var classBuilderModal = GenericTagsTransformer(tableInfo, configContext, textTemplate);
            var classBuilderForm = string.Empty;


            using (var stream = new HelperStream(pathOutput).GetInstance()) { stream.Write(classBuilderModal); }
        }

        private void ExecuteTemplateAngularPackage(TableInfo tableInfo, Context configContext)
        {
            if (tableInfo.CodeCustomImplemented)
                return;

            if (!tableInfo.MakeFront)
                return;

            var pathOutput = PathOutput.PathOutputPackage(tableInfo, configContext);
            if (File.Exists(pathOutput))
                return;

            var pathTemplate = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, this._defineTemplateFolder.Define(tableInfo), DefineTemplateName.AngularPackage(tableInfo));
            var textTemplate = Read.AllText(tableInfo, pathTemplate, this._defineTemplateFolder);
            var classBuilderModal = GenericTagsTransformer(tableInfo, configContext, textTemplate);
            var classBuilderForm = string.Empty;


            using (var stream = new HelperStream(pathOutput).GetInstance()) { stream.Write(classBuilderModal); }
        }


        private void ExecuteTemplateAngularBreadCrumbSharedView(TableInfo tableInfo, Context configContext)
        {
            if (tableInfo.CodeCustomImplemented)
                return;

            if (!tableInfo.MakeFront)
                return;

            var pathOutput = PathOutput.PathOutputAngularBreadCrumbSharedView(tableInfo, configContext);
            if (File.Exists(pathOutput))
                return;

            var pathTemplate = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, this._defineTemplateFolder.Define(tableInfo), DefineTemplateName.AngularBreadCrumbSharedView(tableInfo));
            var textTemplate = Read.AllText(tableInfo, pathTemplate, this._defineTemplateFolder);
            var classBuilderModal = GenericTagsTransformer(tableInfo, configContext, textTemplate);
            var classBuilderForm = string.Empty;


            using (var stream = new HelperStream(pathOutput).GetInstance()) { stream.Write(classBuilderModal); }
        }

        private void ExecuteTemplateAngularExclusaoSharedView(TableInfo tableInfo, Context configContext)
        {
            if (tableInfo.CodeCustomImplemented)
                return;

            if (!tableInfo.MakeFront)
                return;

            var pathOutput = PathOutput.PathOutputAngularExclusaoSharedView(tableInfo, configContext);
            if (File.Exists(pathOutput))
                return;

            var pathTemplate = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, this._defineTemplateFolder.Define(tableInfo), DefineTemplateName.AngularExclusaoSharedView(tableInfo));
            var textTemplate = Read.AllText(tableInfo, pathTemplate, this._defineTemplateFolder);
            var classBuilderModal = GenericTagsTransformer(tableInfo, configContext, textTemplate);
            var classBuilderForm = string.Empty;


            using (var stream = new HelperStream(pathOutput).GetInstance()) { stream.Write(classBuilderModal); }
        }

        private void ExecuteTemplateAngularExecuteSharedView(TableInfo tableInfo, Context configContext)
        {
            if (tableInfo.CodeCustomImplemented)
                return;

            if (!tableInfo.MakeFront)
                return;

            var pathOutput = PathOutput.PathOutputAngularExecuteSharedView(tableInfo, configContext);
            if (File.Exists(pathOutput))
                return;

            var pathTemplate = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, this._defineTemplateFolder.Define(tableInfo), DefineTemplateName.AngularExecuteSharedView(tableInfo));
            var textTemplate = Read.AllText(tableInfo, pathTemplate, this._defineTemplateFolder);
            var classBuilderModal = GenericTagsTransformer(tableInfo, configContext, textTemplate);
            var classBuilderForm = string.Empty;


            using (var stream = new HelperStream(pathOutput).GetInstance()) { stream.Write(classBuilderModal); }
        }

        private void ExecuteTemplateAngularHeaderSharedView(TableInfo tableInfo, Context configContext)
        {
            if (tableInfo.CodeCustomImplemented)
                return;

            if (!tableInfo.MakeFront)
                return;

            var pathOutput = PathOutput.PathOutputAngularHeaderSharedView(tableInfo, configContext);
            if (File.Exists(pathOutput))
                return;

            var pathTemplate = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, this._defineTemplateFolder.Define(tableInfo), DefineTemplateName.AngularHeaderSharedView(tableInfo));
            var textTemplate = Read.AllText(tableInfo, pathTemplate, this._defineTemplateFolder);
            var classBuilderModal = GenericTagsTransformer(tableInfo, configContext, textTemplate);
            var classBuilderForm = string.Empty;


            using (var stream = new HelperStream(pathOutput).GetInstance()) { stream.Write(classBuilderModal); }
        }

        private void ExecuteTemplateAngularMenuSharedView(TableInfo tableInfo, Context configContext)
        {
            if (tableInfo.CodeCustomImplemented)
                return;

            if (!tableInfo.MakeFront)
                return;

            var pathOutput = PathOutput.PathOutputAngularMenuSharedView(tableInfo, configContext);
            if (File.Exists(pathOutput))
                return;

            var pathTemplate = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, this._defineTemplateFolder.Define(tableInfo), DefineTemplateName.AngularMenuSharedView(tableInfo));
            var textTemplate = Read.AllText(tableInfo, pathTemplate, this._defineTemplateFolder);
            var classBuilderModal = GenericTagsTransformer(tableInfo, configContext, textTemplate);
            var classBuilderForm = string.Empty;


            using (var stream = new HelperStream(pathOutput).GetInstance()) { stream.Write(classBuilderModal); }
        }

        private void ExecuteTemplateAngularUnauthorizedSharedView(TableInfo tableInfo, Context configContext)
        {
            if (tableInfo.CodeCustomImplemented)
                return;

            if (!tableInfo.MakeFront)
                return;

            var pathOutput = PathOutput.PathOutputAngularUnauthorizedSharedView(tableInfo, configContext);
            if (File.Exists(pathOutput))
                return;

            var pathTemplate = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, this._defineTemplateFolder.Define(tableInfo), DefineTemplateName.AngularUnauthorizedSharedView(tableInfo));
            var textTemplate = Read.AllText(tableInfo, pathTemplate, this._defineTemplateFolder);
            var classBuilderModal = GenericTagsTransformer(tableInfo, configContext, textTemplate);
            var classBuilderForm = string.Empty;


            using (var stream = new HelperStream(pathOutput).GetInstance()) { stream.Write(classBuilderModal); }
        }


        private void ExecuteTemplateAngularHomeCustomController(TableInfo tableInfo, Context configContext)
        {
            if (tableInfo.CodeCustomImplemented)
                return;

            if (!tableInfo.MakeFront)
                return;

            var pathOutput = PathOutput.PathOutputAngularHomeCustomController(tableInfo, configContext);
            if (File.Exists(pathOutput))
                return;

            var pathTemplate = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, this._defineTemplateFolder.Define(tableInfo), DefineTemplateName.AngularHomeController(tableInfo));
            var textTemplateModules = Read.AllText(tableInfo, pathTemplate, this._defineTemplateFolder);

            var classBuilderModal = GenericTagsTransformer(tableInfo, configContext, textTemplateModules);
            var classBuilderForm = string.Empty;


            using (var stream = new HelperStream(pathOutput).GetInstance()) { stream.Write(classBuilderModal); }
        }

        private void ExecuteTemplateAngularLoginCustomController(TableInfo tableInfo, Context configContext)
        {
            if (tableInfo.CodeCustomImplemented)
                return;

            if (!tableInfo.MakeFront)
                return;

            var pathOutput = PathOutput.PathOutputAngularLoginCustomController(tableInfo, configContext);
            if (File.Exists(pathOutput))
                return;

            var pathTemplate = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, this._defineTemplateFolder.Define(tableInfo), DefineTemplateName.AngularLoginController(tableInfo));
            var textTemplateModules = Read.AllText(tableInfo, pathTemplate, this._defineTemplateFolder);

            var classBuilderModal = GenericTagsTransformer(tableInfo, configContext, textTemplateModules);
            var classBuilderForm = string.Empty;


            using (var stream = new HelperStream(pathOutput).GetInstance()) { stream.Write(classBuilderModal); }
        }
        private void ExecuteTemplateAngularMainCustomController(TableInfo tableInfo, Context configContext)
        {
            if (tableInfo.CodeCustomImplemented)
                return;

            if (!tableInfo.MakeFront)
                return;

            var pathOutput = PathOutput.PathOutputAngularMainCustomController(tableInfo, configContext);
            if (File.Exists(pathOutput))
                return;

            var pathTemplate = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, this._defineTemplateFolder.Define(tableInfo), DefineTemplateName.AngularMainController(tableInfo));
            var textTemplate = Read.AllText(tableInfo, pathTemplate, this._defineTemplateFolder);

            var classBuilderModal = GenericTagsTransformer(tableInfo, configContext, textTemplate);
            var classBuilderForm = string.Empty;


            using (var stream = new HelperStream(pathOutput).GetInstance()) { stream.Write(classBuilderModal); }
        }
        private void ExecuteTemplateAngularIndex(TableInfo tableInfo, Context configContext)
        {
            if (tableInfo.CodeCustomImplemented)
                return;

            if (!tableInfo.MakeFront)
                return;

            var pathOutput = PathOutput.PathOutputAngularIndex(tableInfo, configContext);
            if (File.Exists(pathOutput))
                return;

            var pathTemplateModules = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, this._defineTemplateFolder.Define(tableInfo), DefineTemplateName.AngularIndex(tableInfo));

            var textTemplateModules = Read.AllText(tableInfo, pathTemplateModules, this._defineTemplateFolder);

            var classBuilderModal = GenericTagsTransformer(tableInfo, configContext, textTemplateModules);
            var classBuilderForm = string.Empty;


            using (var stream = new HelperStream(pathOutput).GetInstance()) { stream.Write(classBuilderModal); }
        }
        private void ExecuteTemplateAngularLayout(TableInfo tableInfo, Context configContext)
        {
            if (tableInfo.CodeCustomImplemented)
                return;

            if (!tableInfo.MakeFront)
                return;

            var pathOutput = PathOutput.PathOutputAngularLayout(tableInfo, configContext);

            var pathTemplateModules = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, this._defineTemplateFolder.Define(tableInfo), DefineTemplateName.AngularLayout(tableInfo));

            var textTemplateModules = Read.AllText(tableInfo, pathTemplateModules, this._defineTemplateFolder);

            var classBuilderModal = GenericTagsTransformer(tableInfo, configContext, textTemplateModules);
            var classBuilderForm = string.Empty;


            using (var stream = new HelperStream(pathOutput).GetInstance()) { stream.Write(classBuilderModal); }
        }
        private void ExecuteTemplateAngularConstants(TableInfo tableInfo, Context configContext)
        {
            if (tableInfo.CodeCustomImplemented)
                return;

            if (!tableInfo.MakeFront)
                return;

            var pathOutput = PathOutput.PathOutputAngularConstants(tableInfo, configContext);
            if (File.Exists(pathOutput))
                return;

            var pathTemplate = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, this._defineTemplateFolder.Define(tableInfo), DefineTemplateName.AngularConstants(tableInfo));
            var textTemplateModules = Read.AllText(tableInfo, pathTemplate, this._defineTemplateFolder);
            var classBuilder = GenericTagsTransformer(tableInfo, configContext, textTemplateModules);

            classBuilder = classBuilder.Replace("<#successHandleAPI#>", this.successHandleAPI(configContext));
            classBuilder = classBuilder.Replace("<#getDataAPI#>", this.getDataAPI(configContext));
            classBuilder = classBuilder.Replace("<#getErrorsAPI#>", this.getErrosAPI(configContext));
            classBuilder = classBuilder.Replace("<#getDataListAPI#>", this.getDataListAPI(configContext));
            classBuilder = classBuilder.Replace("<#getDataItemsAPI#>", this.getDataItemsAPI(configContext));
            classBuilder = classBuilder.Replace("<#getDataItemFieldsAPI#>", this.getDataItemFieldsAPI(configContext));
            classBuilder = classBuilder.Replace("<#makeGetMoreResourceBaseUrlAPI#>", this.makeGetMoreResourceBaseUrlAPI(configContext));

            using (var stream = new HelperStream(pathOutput).GetInstance()) { stream.Write(classBuilder); }
        }

        private void ExecuteTemplateAngularbreadcrumbConstants(TableInfo tableInfo, Context configContext)
        {
            if (tableInfo.CodeCustomImplemented)
                return;

            if (!tableInfo.MakeFront)
                return;

            var pathOutput = PathOutput.PathOutputAngularbreadcrumbConstants(tableInfo, configContext);
            if (File.Exists(pathOutput))
                return;

            var pathTemplate = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, this._defineTemplateFolder.Define(tableInfo), DefineTemplateName.AngularbreadcrumbConstants(tableInfo));
            var textTemplateModules = Read.AllText(tableInfo, pathTemplate, this._defineTemplateFolder);
            var classBuilder = GenericTagsTransformer(tableInfo, configContext, textTemplateModules);
            using (var stream = new HelperStream(pathOutput).GetInstance()) { stream.Write(classBuilder); }
        }

        private string successHandleAPI(Context configContext)
        {
            var successHandleAPI = @"self.ViewModel.FilterResult = data.DataList;
            self.Pagination.TotalItens = data.Summary.Total;";

            if (base.ArquitetureType == ArquitetureType.DDD)
            {
                successHandleAPI = @"self.ViewModel.FilterResult = data.dataList;
                self.Pagination.TotalItens = data.summary.total;";
            }

            return successHandleAPI;
        }

        private string getDataAPI(Context configContext)
        {
            var getDataAPI = @"return data.Data;";

            if (base.ArquitetureType == ArquitetureType.DDD)
            {
                getDataAPI = @"return data.data;";
            }

            return getDataAPI;
        }

        private string getDataListAPI(Context configContext)
        {
            var getDataAPI = @"return data.DataList;";

            if (base.ArquitetureType == ArquitetureType.DDD)
            {
                getDataAPI = @"return data.dataList;";
            }

            return getDataAPI;
        }

        private string getErrosAPI(Context configContext)
        {
            var getDataAPI = @"return err.data !== null ? err.data.Errors : null;";

            if (base.ArquitetureType == ArquitetureType.DDD)
            {
                getDataAPI = @"return err.data.result.errors;";
            }

            return getDataAPI;
        }

        private string makeGetMoreResourceBaseUrlAPI(Context configContext)
        {
            var makeGetMoreResourceBaseUrlAPI = @"return String.format(""{0}/{1}/{2}"", Uri.makeUri(), filterBehavior, Uri.queryStringFilter(filter));";

            if (base.ArquitetureType == ArquitetureType.DDD)
            {
                makeGetMoreResourceBaseUrlAPI = @"return String.format(""{0}/more/{2}"", Uri.makeUri(), Uri.queryStringFilter(filter));";
            }

            return makeGetMoreResourceBaseUrlAPI;
        }

        private string getDataItemsAPI(Context configContext)
        {
            var getDataItemsAPI = @"for (var i = 0; i < data.DataList.length; i++)
                  data.DataList[i].Id = parseInt(data.DataList[i].Id);
              scope.vm[""DataItem"" + attr.dataitem] = data.DataList;";

            if (base.ArquitetureType == ArquitetureType.DDD)
            {
                getDataItemsAPI = @"for (var i = 0; i < data.dataList.length; i++)
                  data.dataList[i].id = parseInt(data.dataList[i].id);
              scope.vm[""DataItem"" + attr.dataitem] = data.dataList;";
            }

            return getDataItemsAPI;
        }

        private string getDataItemFieldsAPI(Context configContext)
        {
            var getDataItemFieldsAPI = @"return 'item.Id as item.Name';";

            if (base.ArquitetureType == ArquitetureType.DDD)
            {
                getDataItemFieldsAPI = @"return 'item.id as item.name';";
            }

            return getDataItemFieldsAPI;
        }

        private string getEditFunc(TableInfo tableInfo, Context configContext)
        {
            var getEditType = @"Edit(item.<#KeyName#>)";

            if (tableInfo.IsCompositeKey)
            {
                getEditType = @"EditByFilter(" + ParametersKeyNames(tableInfo, configContext.CamelCasing) + ")";
            }

            return getEditType;
        }

        private string getDetailsFunc(TableInfo tableInfo, Context configContext)
        {
            var getDetailsType = @"Details(item.<#KeyName#>)";

            if (tableInfo.IsCompositeKey)
            {
                getDetailsType = @"DetailsByFilter(" + ParametersKeyNames(tableInfo, configContext.CamelCasing) + ")";
            }

            return getDetailsType;
        }

        private string getPrintFunc(TableInfo tableInfo, Context configContext)
        {
            var getDetailsType = @"Print(item.<#KeyName#>)";

            if (tableInfo.IsCompositeKey)
            {
                getDetailsType = @"PrintByFilter(" + ParametersKeyNames(tableInfo, configContext.CamelCasing) + ")";
            }

            return getDetailsType;
        }

        private string getDeleteFunc(TableInfo tableInfo, Context configContext)
        {
            var getDetailsType = @"Delete({ <#KeyName#> : item.<#KeyName#> })";

            if (tableInfo.IsCompositeKey)
            {
                getDetailsType = @"Delete(" + ParametersKeyNames(tableInfo, configContext.CamelCasing) + ")";
            }

            return getDetailsType;
        }


        private void ExecuteTemplateAngularInitApp(TableInfo tableInfo, Context configContext)
        {
            if (tableInfo.CodeCustomImplemented)
                return;

            if (!tableInfo.MakeFront)
                return;

            var pathOutput = PathOutput.PathOutputAngularApp(tableInfo, configContext);

            var pathTemplateApp = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, this._defineTemplateFolder.Define(tableInfo), DefineTemplateName.AngularApp(tableInfo));

            var textTemplateApp = Read.AllText(tableInfo, pathTemplateApp, this._defineTemplateFolder);

            var classBuilderModal = GenericTagsTransformer(tableInfo, configContext, textTemplateApp);
            var classBuilderForm = string.Empty;


            using (var stream = new HelperStream(pathOutput).GetInstance()) { stream.Write(classBuilderModal); }
        }
        private void ExecuteTemplateAngularInitModules(TableInfo tableInfo, Context configContext)
        {
            if (tableInfo.CodeCustomImplemented)
                return;

            if (!tableInfo.MakeFront)
                return;

            var pathOutput = PathOutput.PathOutputAngularInitModules(tableInfo, configContext);
            if (File.Exists(pathOutput))
                return;

            var pathTemplateModules = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, this._defineTemplateFolder.Define(tableInfo), DefineTemplateName.AngularModule(tableInfo));

            var textTemplateModules = Read.AllText(tableInfo, pathTemplateModules, this._defineTemplateFolder);

            var classBuilderModal = GenericTagsTransformer(tableInfo, configContext, textTemplateModules);
            var classBuilderForm = string.Empty;


            using (var stream = new HelperStream(pathOutput).GetInstance()) { stream.Write(classBuilderModal); }
        }
        private void ExecuteTemplateAngularModalCreate(TableInfo tableInfo, Context configContext, IEnumerable<Info> infos)
        {
            if (tableInfo.CodeCustomImplemented)
                return;

            if (!tableInfo.MakeFront)
                return;

            var pathOutput = PathOutput.PathOutputAngularModalCreate(tableInfo, configContext);
            if (File.Exists(pathOutput))
                return;

            var pathTemplateModal = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, this._defineTemplateFolder.Define(tableInfo), DefineTemplateName.AngularModalCreate(tableInfo));
            var textTemplateModal = Read.AllText(tableInfo, pathTemplateModal, this._defineTemplateFolder);
            var classBuilderModal = GenericTagsTransformer(tableInfo, configContext, textTemplateModal);

            using (var stream = new HelperStream(pathOutput).GetInstance()) { stream.Write(classBuilderModal); }
        }

        private void ExecuteTemplateAngularModalEdit(TableInfo tableInfo, Context configContext, IEnumerable<Info> infos)
        {
            if (tableInfo.CodeCustomImplemented)
                return;

            if (!tableInfo.MakeFront)
                return;

            var pathOutput = PathOutput.PathOutputAngularModalEdit(tableInfo, configContext);
            if (File.Exists(pathOutput))
                return;

            var pathTemplateModal = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, this._defineTemplateFolder.Define(tableInfo), DefineTemplateName.AngularModalEdit(tableInfo));
            var textTemplateModal = Read.AllText(tableInfo, pathTemplateModal, this._defineTemplateFolder);
            var classBuilderModal = GenericTagsTransformer(tableInfo, configContext, textTemplateModal);

            using (var stream = new HelperStream(pathOutput).GetInstance()) { stream.Write(classBuilderModal); }
        }

        private void ExecuteTemplateAngularFormCreate(TableInfo tableInfo, Context configContext, IEnumerable<Info> infos)
        {
            if (tableInfo.CodeCustomImplemented)
                return;

            if (!tableInfo.MakeFront)
                return;

            var pathOutput = PathOutput.PathOutputAngularFormCreate(tableInfo, configContext);

            var pathTemplateForm = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, this._defineTemplateFolder.Define(tableInfo), DefineTemplateName.AngularForm(tableInfo));
            var textTemplateForm = Read.AllText(tableInfo, pathTemplateForm, this._defineTemplateFolder);

            var classBuilderModal = GenericTagsTransformer(tableInfo, configContext, textTemplateForm);
            var classBuilderForm = string.Empty;

            if (infos.IsAny())
            {
                foreach (var item in infos)
                {
                    if (Audit.IsAuditField(item.PropertyName))
                        continue;

                    if (item.IsKey == 1 && !IsPropertyNavigationTypeInstance(tableInfo, item.PropertyName))
                        continue;

                    var itemForm = string.Empty;
                    var propertyName = configContext.CamelCasing ? CamelCaseTransform(item.PropertyName) : item.PropertyName;

                    if (item.TypeCustom.IsSent())
                        item.Type = item.TypeCustom;

                    //var fieldInBlackListCreate = HelperFieldConfig.FieldInBlackListCreate(tableInfo, item, propertyName);
                    var fieldInBlackListCreate = HelperRestrictions.FieldVerification(tableInfo, item, propertyName, "Create", "False");
                    if (fieldInBlackListCreate)
                        continue;

                    var isStringLengthBig = base.IsStringLengthBig(item, configContext);
                    if (item.Type == "string")
                    {

                        var str = HelperControlHtml.MakeInputHtml(tableInfo, item, isStringLengthBig, propertyName);
                        itemForm = FormFieldReplace(configContext, tableInfo, item, textTemplateForm, str);
                        itemForm = itemForm.Replace("<#propertyName#>", propertyName)
                            .Replace("<#className#>", tableInfo.ClassName);
                    }

                  

                    if (item.Type == "DateTime" || item.Type == "DateTime?")
                    {
                        var str = HelperControlHtml.MakeDatetimepiker(IsRequired(item));
                        itemForm = FormFieldReplace(configContext, tableInfo, item, textTemplateForm, str);
                        itemForm = itemForm.Replace("<#propertyName#>", propertyName)
                            .Replace("<#className#>", tableInfo.ClassName);
                    }
                    else if (item.Type == "Date")
                    {
                        var str = HelperControlHtml.MakeDatepiker(IsRequired(item));
                        itemForm = FormFieldReplace(configContext, tableInfo, item, textTemplateForm, str);
                        itemForm = itemForm.Replace("<#propertyName#>", propertyName)
                            .Replace("<#className#>", tableInfo.ClassName);
                    }
                    else if (item.Type == "bool" || item.Type == "bool?")
                    {

                        if (HelperFieldConfig.IsRadio(tableInfo, propertyName))
                        {

                            var str = HelperControlHtml.MakeRadio(IsRequired(item));
                            itemForm = FormFieldReplace(configContext, tableInfo, item, textTemplateForm, str);
                            itemForm = itemForm.Replace("<#propertyName#>", propertyName)
                                .Replace("<#className#>", tableInfo.ClassName);

                        }
                        else
                        {

                            var str = HelperControlHtml.MakeCheckbox(IsRequired(item));
                            itemForm = FormFieldReplace(configContext, tableInfo, item, textTemplateForm, str);
                            itemForm = itemForm.Replace("<#propertyName#>", propertyName)
                                .Replace("<#className#>", tableInfo.ClassName);
                        }

                    }

                    else
                    {
                        if (IsPropertyNavigationTypeInstance(tableInfo, item.PropertyName))
                        {
                            var isSelectSearch = HelperFieldConfig.IsSelectSearch(tableInfo, propertyName);
                            if (isSelectSearch)
                            {
                                var str = HelperControlHtml.MakeDropDownSeach(IsRequired(item));
                                itemForm = FormFieldReplace(configContext, tableInfo, item, textTemplateForm, str);
                                itemForm = itemForm
                                    .Replace("<#propertyName#>", propertyName)
                                    .Replace("<#ReletedClass#>", PropertyNavigationTypeInstance(tableInfo, item.PropertyName));
                            }
                            else
                            {
                                var str = HelperControlHtml.MakeDropDown(IsRequired(item));
                                itemForm = FormFieldReplace(configContext, tableInfo, item, textTemplateForm, str);
                                itemForm = itemForm
                                    .Replace("<#propertyName#>", propertyName)
                                    .Replace("<#ReletedClass#>", PropertyNavigationTypeInstance(tableInfo, item.PropertyName));
                            }
                        }
                        else
                        {
                            var str = HelperControlHtml.MakeInputHtml(tableInfo, item, isStringLengthBig, propertyName);
                            itemForm = FormFieldReplace(configContext, tableInfo, item, textTemplateForm, str);
                            itemForm = itemForm.Replace("<#propertyName#>", propertyName)
                                .Replace("<#className#>", tableInfo.ClassName);

                        }

                    }

                    var htmlCtrl = HelperFieldConfig.FieldHtml(tableInfo, propertyName);
                    if (htmlCtrl.IsNotNull())
                    {
                        var str = HelperControlHtml.MakeCtrl(htmlCtrl.HtmlField);
                        itemForm = FormFieldReplace(configContext, tableInfo, item, textTemplateForm, str);
                        itemForm = itemForm.Replace("<#propertyName#>", propertyName)
                            .Replace("<#className#>", tableInfo.ClassName);
                    }

                    var isUpload = HelperFieldConfig.IsUpload(tableInfo, propertyName);
                    if (isUpload)
                    {
                        var str = HelperControlHtml.MakeUpload(item);
                        itemForm = FormFieldReplace(configContext, tableInfo, item, textTemplateForm, str);
                        itemForm = itemForm.Replace("<#propertyName#>", propertyName)
                            .Replace("<#className#>", tableInfo.ClassName);
                    }


                    var isTextStyle = HelperFieldConfig.IsTextStyle(tableInfo, propertyName);
                    if (isTextStyle)
                    {
                        var str = HelperControlHtml.MakeTextStyle(IsRequired(item));
                        itemForm = FormFieldReplace(configContext, tableInfo, item, textTemplateForm, str);
                        itemForm = itemForm
                            .Replace("<#propertyName#>", propertyName)
                            .Replace("<#className#>", tableInfo.ClassName);
                    }

                    var isTextEditor = HelperFieldConfig.IsTextEditor(tableInfo, propertyName);
                    if (isTextEditor)
                    {
                        var str = HelperControlHtml.MakeTextEditor(IsRequired(item));
                        itemForm = FormFieldReplace(configContext, tableInfo, item, textTemplateForm, str);
                        itemForm = itemForm
                            .Replace("<#propertyName#>", propertyName)
                            .Replace("<#ReletedClass#>", PropertyNavigationTypeInstance(tableInfo, item.PropertyName));
                    }

                    classBuilderForm += string.Format("{0}{1}", itemForm, System.Environment.NewLine);
                }
            }

            classBuilderModal = classBuilderForm.Substring(0, classBuilderForm.LastIndexOf(Environment.NewLine));

            using (var stream = new HelperStream(pathOutput).GetInstance()) { stream.Write(classBuilderModal); }
        }



        private void ExecuteTemplateAngularFormEdit(TableInfo tableInfo, Context configContext, IEnumerable<Info> infos)
        {
            if (tableInfo.CodeCustomImplemented)
                return;

            if (!tableInfo.MakeFront)
                return;

            var pathOutput = PathOutput.PathOutputAngularFormEdit(tableInfo, configContext);

            var pathTemplateForm = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, this._defineTemplateFolder.Define(tableInfo), DefineTemplateName.AngularForm(tableInfo));
            var textTemplateForm = Read.AllText(tableInfo, pathTemplateForm, this._defineTemplateFolder);

            var classBuilderModal = GenericTagsTransformer(tableInfo, configContext, textTemplateForm);
            var classBuilderForm = string.Empty;

            if (infos.IsAny())
            {
                foreach (var item in infos)
                {
                    if (Audit.IsAuditField(item.PropertyName))
                        continue;

                    if (item.IsKey == 1 && !IsPropertyNavigationTypeInstance(tableInfo, item.PropertyName))
                        continue;

                    var itemForm = string.Empty;
                    var propertyName = configContext.CamelCasing ? CamelCaseTransform(item.PropertyName) : item.PropertyName;

                    //var fieldInBlackListEdit = HelperFieldConfig.FieldInBlackListEdit(tableInfo, item, propertyName);
                    var fieldInBlackListEdit = HelperRestrictions.FieldVerification(tableInfo, item, propertyName, "Edit", "False");


                    if (fieldInBlackListEdit)
                        continue;

                    var isStringLengthBig = IsStringLengthBig(item, configContext);
                    if (item.Type == "string")
                    {
                        var str = HelperControlHtml.MakeInputHtml(tableInfo, item, isStringLengthBig, propertyName);
                        itemForm = FormFieldReplace(configContext, tableInfo, item, textTemplateForm, str);
                        itemForm = itemForm.Replace("<#propertyName#>", propertyName)
                            .Replace("<#className#>", tableInfo.ClassName);

                    }
                    if (item.Type == "DateTime" || item.Type == "DateTime?")
                    {
                        var str = HelperControlHtml.MakeDatetimepiker(IsRequired(item));
                        itemForm = FormFieldReplace(configContext, tableInfo, item, textTemplateForm, str);
                        itemForm = itemForm.Replace("<#propertyName#>", propertyName)
                            .Replace("<#className#>", tableInfo.ClassName);
                    }
                    else if (item.Type == "Date")
                    {
                        var str = HelperControlHtml.MakeDatepiker(IsRequired(item));
                        itemForm = FormFieldReplace(configContext, tableInfo, item, textTemplateForm, str);
                        itemForm = itemForm.Replace("<#propertyName#>", propertyName)
                            .Replace("<#className#>", tableInfo.ClassName);
                    }
                    else if (item.Type == "bool" || item.Type == "bool?")
                    {

                        if (HelperFieldConfig.IsRadio(tableInfo, propertyName))
                        {
                            var str = HelperControlHtml.MakeRadio(IsRequired(item));
                            itemForm = FormFieldReplace(configContext, tableInfo, item, textTemplateForm, str);
                            itemForm = itemForm.Replace("<#propertyName#>", propertyName)
                                .Replace("<#className#>", tableInfo.ClassName);

                        }
                        else
                        {

                            var str = HelperControlHtml.MakeCheckbox(IsRequired(item));
                            itemForm = FormFieldReplace(configContext, tableInfo, item, textTemplateForm, str);
                            itemForm = itemForm.Replace("<#propertyName#>", propertyName)
                                .Replace("<#className#>", tableInfo.ClassName);
                        }
                    }

                    else
                    {
                        if (IsPropertyNavigationTypeInstance(tableInfo, item.PropertyName))
                        {
                            var isSelectSearch = HelperFieldConfig.IsSelectSearch(tableInfo, propertyName);
                            if (isSelectSearch)
                            {
                                var str = HelperControlHtml.MakeDropDownSeach(IsRequired(item));
                                itemForm = FormFieldReplace(configContext, tableInfo, item, textTemplateForm, str);
                                itemForm = itemForm
                                    .Replace("<#propertyName#>", propertyName)
                                    .Replace("<#ReletedClass#>", PropertyNavigationTypeInstance(tableInfo, item.PropertyName));
                            }
                            else
                            {
                                var str = HelperControlHtml.MakeDropDown(IsRequired(item));
                                itemForm = FormFieldReplace(configContext, tableInfo, item, textTemplateForm, str);
                                itemForm = itemForm
                                    .Replace("<#propertyName#>", propertyName)
                                    .Replace("<#ReletedClass#>", PropertyNavigationTypeInstance(tableInfo, item.PropertyName));
                            }
                        }
                        else
                        {
                            var str = HelperControlHtml.MakeInputHtml(tableInfo, item, isStringLengthBig, propertyName);
                            itemForm = FormFieldReplace(configContext, tableInfo, item, textTemplateForm, str);
                            itemForm = itemForm.Replace("<#propertyName#>", propertyName)
                                .Replace("<#className#>", tableInfo.ClassName);

                        }
                    }



                    var htmlCtrl = HelperFieldConfig.FieldHtml(tableInfo, propertyName);
                    if (htmlCtrl.IsNotNull())
                    {
                        var str = HelperControlHtml.MakeCtrl(htmlCtrl.HtmlField);
                        itemForm = FormFieldReplace(configContext, tableInfo, item, textTemplateForm, str);
                        itemForm = itemForm.Replace("<#propertyName#>", propertyName)
                            .Replace("<#className#>", tableInfo.ClassName);
                    }

                    var isUpload = HelperFieldConfig.IsUpload(tableInfo, propertyName);
                    if (isUpload)
                    {
                        var str = HelperControlHtml.MakeUpload(item);
                        itemForm = FormFieldReplace(configContext, tableInfo, item, textTemplateForm, str);
                        itemForm = itemForm.Replace("<#propertyName#>", propertyName)
                            .Replace("<#className#>", tableInfo.ClassName);
                    }

                    var isTextStyle = HelperFieldConfig.IsTextStyle(tableInfo, propertyName);
                    if (isTextStyle)
                    {
                        var str = HelperControlHtml.MakeTextStyle(IsRequired(item));
                        itemForm = FormFieldReplace(configContext, tableInfo, item, textTemplateForm, str);
                        itemForm = itemForm
                            .Replace("<#propertyName#>", propertyName)
                            .Replace("<#className#>", tableInfo.ClassName);
                    }


                    var isTextEditor = HelperFieldConfig.IsTextEditor(tableInfo, propertyName);
                    if (isTextEditor)
                    {
                        var str = HelperControlHtml.MakeTextEditor(IsRequired(item));
                        itemForm = FormFieldReplace(configContext, tableInfo, item, textTemplateForm, str);
                        itemForm = itemForm
                            .Replace("<#propertyName#>", propertyName)
                            .Replace("<#ReletedClass#>", PropertyNavigationTypeInstance(tableInfo, item.PropertyName));
                    }

                    classBuilderForm += string.Format("{0}{1}", itemForm, System.Environment.NewLine);
                }
            }

            classBuilderModal = classBuilderForm.Substring(0, classBuilderForm.LastIndexOf(Environment.NewLine));
            using (var stream = new HelperStream(pathOutput).GetInstance()) { stream.Write(classBuilderModal); }
        }


        private void ExecuteTemplateAngularEditInPage(TableInfo tableInfo, Context configContext, IEnumerable<Info> infos)
        {
            if (tableInfo.CodeCustomImplemented)
                return;

            if (!tableInfo.MakeFront)
                return;

            var pathOutput = PathOutput.PathOutputAngularEditInPage(tableInfo, configContext);
            if (File.Exists(pathOutput))
                return;


            var pathTemplate = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, this._defineTemplateFolder.Define(tableInfo), DefineTemplateName.AngularEditInPage(tableInfo));
            var textTemplateEditInPage = Read.AllText(tableInfo, pathTemplate, this._defineTemplateFolder);
            var classBuilderEditInPage = GenericTagsTransformer(tableInfo, configContext, textTemplateEditInPage);

            using (var stream = new HelperStream(pathOutput).GetInstance()) { stream.Write(classBuilderEditInPage); }
        }
        private void ExecuteTemplateAngularCreateInPage(TableInfo tableInfo, Context configContext, IEnumerable<Info> infos)
        {
            if (tableInfo.CodeCustomImplemented)
                return;

            if (!tableInfo.MakeFront)
                return;

            var pathOutput = PathOutput.PathOutputAngularCreateInPage(tableInfo, configContext);
            if (File.Exists(pathOutput))
                return;

            var pathTemplate = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, this._defineTemplateFolder.Define(tableInfo), DefineTemplateName.AngularCreateInPage(tableInfo));
            var textTemplateCreateInPage = Read.AllText(tableInfo, pathTemplate, this._defineTemplateFolder);
            var classBuilderCreateInPage = GenericTagsTransformer(tableInfo, configContext, textTemplateCreateInPage);

            using (var stream = new HelperStream(pathOutput).GetInstance()) { stream.Write(classBuilderCreateInPage); }
        }
        private void ExecuteTemplateAngularDetails(TableInfo tableInfo, Context configContext, IEnumerable<Info> infos)
        {
            if (tableInfo.CodeCustomImplemented)
                return;

            if (!tableInfo.MakeFront)
                return;

            var pathOutput = PathOutput.PathOutputAngularDetails(tableInfo, configContext);
            if (File.Exists(pathOutput))
                return;

            var pathTemplateDetails = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, this._defineTemplateFolder.Define(tableInfo), DefineTemplateName.AngularDetails(tableInfo));
            var textTemplateDetails = Read.AllText(tableInfo, pathTemplateDetails, this._defineTemplateFolder);
            var classBuilderDetails = GenericTagsTransformer(tableInfo, configContext, textTemplateDetails);

            using (var stream = new HelperStream(pathOutput).GetInstance()) { stream.Write(classBuilderDetails); }
        }

        private void ExecuteTemplateAngularDetailsFields(TableInfo tableInfo, Context configContext, IEnumerable<Info> infos)
        {
            if (tableInfo.CodeCustomImplemented)
                return;

            if (!tableInfo.MakeFront)
                return;

            var pathOutput = PathOutput.PathOutputAngularDetailsField(tableInfo, configContext);

            var pathTemplateDetailsSection = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, this._defineTemplateFolder.Define(tableInfo), DefineTemplateName.AngularDetaislSection(tableInfo));

            var textTemplateDetailsFields = Read.AllText(tableInfo, pathTemplateDetailsSection, this._defineTemplateFolder);
            var classBuilderDetailsFields = string.Empty;

            if (infos.IsAny())
            {
                foreach (var item in infos)
                {
                    if (Audit.IsAuditField(item.PropertyName))
                        continue;

                    if (item.IsKey == 1 && !IsPropertyNavigationTypeInstance(tableInfo, item.PropertyName))
                        continue;

                    var itemForm = string.Empty;
                    var propertyName = configContext.CamelCasing ? CamelCaseTransform(item.PropertyName) : item.PropertyName;
                    //var fieldInBlackListDetails = HelperFieldConfig.FieldInBlackListDetails(tableInfo, item, propertyName);
                    var fieldInBlackListDetails = HelperRestrictions.FieldVerification(tableInfo, item, propertyName, "Details", "False");

                    if (fieldInBlackListDetails)
                        continue;

                    if (IsPropertyNavigationTypeInstance(tableInfo, item.PropertyName))
                    {

                        itemForm = textTemplateDetailsFields.Replace("<#propertyName#>", propertyName);
                        itemForm = itemForm.Replace("<#type#>", "propertyInstance")
                            .Replace("<#moreattr#>", string.Format("{0}= \"{1}\" {2}= \"{3}\" ", "bind-property-name", propertyName, "bind-releted-class", PropertyNavigationTypeInstance(tableInfo, item.PropertyName)))
                            .Replace("<#moredesc#>", string.Empty);
                    }
                    else if (HelperFieldConfig.IsTextEditor(tableInfo, item.PropertyName))
                    {

                        itemForm = textTemplateDetailsFields.Replace("<#propertyName#>", propertyName);
                        itemForm = itemForm.Replace("<#type#>", "textEditor")
                                            .Replace("<#moreattr#>", string.Empty)
                                            .Replace("<#moredesc#>", string.Empty); 

                    }
                    else if (item.Type == "bool" || item.Type == "bool?")
                    {
                        itemForm = textTemplateDetailsFields.Replace("<#propertyName#>", propertyName);
                        itemForm = itemForm.Replace("<#type#>", item.Type)
                                            .Replace("<#moreattr#>", string.Empty)
                                            .Replace("<#moredesc#>", "?");

                    }
                    else
                    {
                        if (!item.TypeCustom.IsNullOrEmpaty())
                            item.Type = item.TypeCustom;

                        itemForm = textTemplateDetailsFields.Replace("<#propertyName#>", propertyName);
                        itemForm = itemForm.Replace("<#type#>", item.Type)
                                            .Replace("<#moreattr#>", string.Empty)
                                            .Replace("<#moredesc#>", string.Empty);
                    }

                    var dataitem = HelperFieldConfig.FieldDataItem(tableInfo, propertyName);
                    if (dataitem.IsAny())
                    {

                        var options = "                <dt>{{ ::vm.Labels.<#propertyName#> }}</dt>" + System.Environment.NewLine;
                        options += "                <dd>";
                        foreach (var _item in dataitem)
                        {
                            options += string.Format("<span class='label label-success' ng-if='vm.Model.{0}==" + _item.Key + "'>" + _item.Value + "</span>", propertyName);
                        }
                        options += "</dd>";

                        itemForm = options.Replace("<#propertyName#>", propertyName);
                        itemForm = itemForm.Replace("<#type#>", item.Type)
                                            .Replace("<#moreattr#>", string.Empty)
                                            .Replace("<#moredesc#>", string.Empty); 
                    }

                    classBuilderDetailsFields += string.Format("{0}{1}", itemForm, System.Environment.NewLine);
                }
            }

            using (var stream = new HelperStream(pathOutput).GetInstance()) { stream.Write(classBuilderDetailsFields); }
        }


        private void ExecuteTemplateAngularView(TableInfo tableInfo, Context configContext, IEnumerable<Info> infos)
        {
            if (tableInfo.CodeCustomImplemented)
                return;

            if (!tableInfo.MakeFront)
                return;

            var pathOutput = PathOutput.PathOutputAngularView(tableInfo, configContext);
            if (File.Exists(pathOutput))
                return;

            var pathTemplateView = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, this._defineTemplateFolder.Define(tableInfo), DefineTemplateName.AngularView(tableInfo));

            var textTemplateView = Read.AllText(tableInfo, pathTemplateView, this._defineTemplateFolder);

            var classBuilderView = GenericTagsTransformer(tableInfo, configContext, textTemplateView);

            using (var stream = new HelperStream(pathOutput).GetInstance()) { stream.Write(classBuilderView); }
        }

        private void ExecuteTemplateAngularViewFilters(TableInfo tableInfo, Context configContext, IEnumerable<Info> infos)
        {
            if (tableInfo.CodeCustomImplemented)
                return;

            if (!tableInfo.MakeFront)
                return;

            var pathOutput = PathOutput.PathOutputAngularViewFilters(tableInfo, configContext);


            var pathTemplateView = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, this._defineTemplateFolder.Define(tableInfo), DefineTemplateName.AngularViewFilters(tableInfo));
            var pathTemplateFilter = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, this._defineTemplateFolder.Define(tableInfo), DefineTemplateName.AngularFilter(tableInfo));

            var textTemplateView = Read.AllText(tableInfo, pathTemplateView, this._defineTemplateFolder);
            var textTemplateFilter = Read.AllText(tableInfo, pathTemplateFilter, this._defineTemplateFolder);

            var classBuilderView = GenericTagsTransformer(tableInfo, configContext, textTemplateView);
            var classBuilderFilter = string.Empty;

            if (infos.IsAny())
            {
                foreach (var item in infos)
                {
                    if (Audit.IsAuditField(item.PropertyName))
                        continue;

                    var itemFilterFields = string.Empty;
                    var itemFilterFieldsStart = string.Empty;
                    var itemFilterFieldsEnd = string.Empty;
                    var itemTableFieldsHead = string.Empty;
                    var itemTableFieldsBody = string.Empty;

                    var propertyName = configContext.CamelCasing ? CamelCaseTransform(item.PropertyName) : item.PropertyName;
                    //var fieldInBlackListFilter = HelperFieldConfig.FieldInBlackListFilter(tableInfo,item, propertyName);
                    var fieldInBlackListFilter = HelperRestrictions.FieldVerification(tableInfo, item, propertyName, "Filter", "False");
                    if (fieldInBlackListFilter)
                        continue;

                    if (item.Type == "string")
                    {
                        if (IsVarcharMax(item) || IsStringLengthBig(item, configContext))
                            continue;

                        itemFilterFields = textTemplateFilter
                            .Replace("<#propertyName#>", propertyName)
                            .Replace("<#labelAux#>", string.Empty)
                            .Replace("<#filterfield#>", HelperControlHtml.MakeInputFilterHtml()
                            .Replace("<#propertyName#>", propertyName)
                            .Replace("<#ClassName#>", item.ClassName));

                    }
                    else if (item.Type == "DateTime" || item.Type == "DateTime?")
                    {
                        itemFilterFieldsStart = textTemplateFilter
                            .Replace("<#propertyName#>", propertyName)
                            .Replace("<#labelAux#>", "Inicio")
                            .Replace("<#filterfield#>", HelperControlHtml.MakeDatapikerFilter(string.Format("{0}Start", propertyName))
                            .Replace("<#propertyName#>", propertyName)
                            .Replace("<#ClassName#>", item.ClassName));

                        itemFilterFieldsEnd = textTemplateFilter
                           .Replace("<#propertyName#>", propertyName)
                           .Replace("<#labelAux#>", "Fim")
                           .Replace("<#filterfield#>", HelperControlHtml.MakeDatapikerFilter(string.Format("{0}End", propertyName))
                           .Replace("<#propertyName#>", propertyName)
                           .Replace("<#ClassName#>", item.ClassName));

                    }

                    else if (item.Type == "bool" || item.Type == "bool?")
                    {
                        itemFilterFields = textTemplateFilter
                            .Replace("<#propertyName#>", propertyName)
                            .Replace("<#labelAux#>", string.Empty)
                            .Replace("<#filterfield#>", HelperControlHtml.MakeCheckboxFilter()
                            .Replace("<#propertyName#>", propertyName)
                            .Replace("<#ClassName#>", item.ClassName));

                    }

                    else
                    {
                        if (IsPropertyNavigationTypeInstance(tableInfo, item.PropertyName))
                        {
                            itemFilterFields = textTemplateFilter
                                   .Replace("<#propertyName#>", propertyName)
                                   .Replace("<#labelAux#>", string.Empty)
                                   .Replace("<#filterfield#>", HelperControlHtml.MakeDropDownFilter()
                                   .Replace("<#propertyName#>", propertyName)
                                   .Replace("<#ClassName#>", item.ClassName))
                                   .Replace("<#ReletedClass#>", PropertyNavigationTypeInstance(tableInfo, item.PropertyName));

                        }
                        else
                        {
                            itemFilterFields = textTemplateFilter
                                    .Replace("<#propertyName#>", propertyName)
                                    .Replace("<#labelAux#>", string.Empty)
                                    .Replace("<#filterfield#>", HelperControlHtml.MakeInputFilterHtml()
                                    .Replace("<#propertyName#>", propertyName)
                                    .Replace("<#ClassName#>", item.ClassName));

                        }

                    }

                    var HtmlCtrl = HelperFieldConfig.FieldHtml(tableInfo, propertyName);
                    if (HtmlCtrl.IsNotNull())
                    {
                        itemFilterFields = textTemplateFilter
                                    .Replace("<#propertyName#>", propertyName)
                                    .Replace("<#labelAux#>", string.Empty)
                                    .Replace("<#filterfield#>", HelperControlHtml.MakeCtrlFilter(HtmlCtrl.HtmlFilter)
                                    .Replace("<#propertyName#>", propertyName)
                                    .Replace("<#ClassName#>", item.ClassName));
                    }



                    if (!itemFilterFields.IsNullOrEmpaty())
                        classBuilderFilter += string.Format("{0}{1}{2}", Tabs.TabFilters(), itemFilterFields, System.Environment.NewLine);
                    else if (!itemFilterFieldsStart.IsNullOrEmpaty() && !itemFilterFieldsEnd.IsNullOrEmpaty())
                    {
                        classBuilderFilter += string.Format("{0}{1}{2}", Tabs.TabFilters(), itemFilterFieldsStart, System.Environment.NewLine);
                        classBuilderFilter += string.Format("{0}{1}{2}", Tabs.TabFilters(), itemFilterFieldsEnd, System.Environment.NewLine);
                    }
                }
            }

            classBuilderView = classBuilderView.Replace("<#FiltersViews#>", classBuilderFilter.Substring(0, classBuilderFilter.LastIndexOf(Environment.NewLine)));

            using (var stream = new HelperStream(pathOutput).GetInstance()) { stream.Write(classBuilderView); }
        }

        private void ExecuteTemplateAngularViewGrid(TableInfo tableInfo, Context configContext, IEnumerable<Info> infos)
        {
            if (tableInfo.CodeCustomImplemented)
                return;

            if (!tableInfo.MakeFront)
                return;

            var pathOutput = PathOutput.PathOutputAngularViewGrid(tableInfo, configContext);


            var pathTemplateView = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, this._defineTemplateFolder.Define(tableInfo), DefineTemplateName.AngularViewGrid(tableInfo));
            var pathTemplateTableHead = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, this._defineTemplateFolder.Define(tableInfo), DefineTemplateName.AngularTableHead(tableInfo));
            var pathTemplateTableBody = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, this._defineTemplateFolder.Define(tableInfo), DefineTemplateName.AngularTableBody(tableInfo));

            var textTemplateView = Read.AllText(tableInfo, pathTemplateView, this._defineTemplateFolder);
            var textTemplateTableHead = Read.AllText(tableInfo, pathTemplateTableHead, this._defineTemplateFolder);
            var textTemplateTableBody = Read.AllText(tableInfo, pathTemplateTableBody, this._defineTemplateFolder);

            textTemplateView = textTemplateView.Replace("<#EditFunc#>", this.getEditFunc(tableInfo, configContext));
            textTemplateView = textTemplateView.Replace("<#DetailsFunc#>", this.getDetailsFunc(tableInfo, configContext));
            textTemplateView = textTemplateView.Replace("<#PrintFunc#>", this.getPrintFunc(tableInfo, configContext));
            textTemplateView = textTemplateView.Replace("<#DeleteFunc#>", this.getDeleteFunc(tableInfo, configContext));

            var classBuilderView = GenericTagsTransformer(tableInfo, configContext, textTemplateView);
            var classBuilderTableHead = string.Empty;
            var classBuilderTableBody = string.Empty;

            if (infos.IsAny())
            {
                foreach (var item in infos)
                {
                    if (Audit.IsAuditField(item.PropertyName))
                        continue;

                    var itemTableFieldsHead = string.Empty;
                    var itemTableFieldsBody = string.Empty;

                    var propertyName = configContext.CamelCasing ? CamelCaseTransform(item.PropertyName) : item.PropertyName;
                    //var fieldInBlackListGrid = HelperFieldConfig.FieldInBlackListGrid(tableInfo,item, propertyName);
                    var fieldInBlackListGrid = HelperRestrictions.FieldVerification(tableInfo, item, propertyName, "List", "False");

                    if (fieldInBlackListGrid)
                        continue;

                    if (item.Type == "string")
                    {
                        if (IsVarcharMax(item) || IsStringLengthBig(item, configContext))
                            continue;

                        itemTableFieldsHead = textTemplateTableHead.Replace("<#propertyName#>", propertyName);

                        var _isPassword = HelperFieldConfig.IsPassword(tableInfo, item.PropertyName);
                        var _isPasswordConfimation = HelperFieldConfig.IsPasswordConfirmation(tableInfo, item.PropertyName);

                        if (_isPassword || _isPasswordConfimation)
                            itemTableFieldsBody = textTemplateTableBody.Replace("<#propertyName#>", "******");
                        else
                        {
                            itemTableFieldsBody = textTemplateTableBody.Replace("<#propertyName#>", "{{ ::item." + propertyName + " }}");
                        }


                    }
                    if (item.Type == "DateTime" || item.Type == "DateTime?")
                    {
                        itemTableFieldsHead = textTemplateTableHead.Replace("<#propertyName#>", propertyName);
                        itemTableFieldsBody = textTemplateTableBody.Replace("<#propertyName#>", "<p bind-custom-value='item." + propertyName + "' bind-custom-type=date />");
                    }

                    if (item.Type == "int" || item.Type == "int?")
                    {
                        if (!IsPropertyNavigationTypeInstance(tableInfo, item.PropertyName))
                        {
                            if (item.IsKey != 1)
                            {
                                itemTableFieldsHead = textTemplateTableHead.Replace("<#propertyName#>", propertyName);
                                itemTableFieldsBody = textTemplateTableBody.Replace("<#propertyName#>", "<p bind-custom-value='item." + propertyName + "' bind-custom-type=integer />");

                            }
                        }
                    }

                    if (item.Type == "decimal" || item.Type == "decimal?")
                    {
                        itemTableFieldsHead = textTemplateTableHead.Replace("<#propertyName#>", propertyName);
                        itemTableFieldsBody = textTemplateTableBody.Replace("<#propertyName#>", "<p bind-custom-value='item." + propertyName + "' bind-custom-type=decimal />");
                    }

                    else if (item.Type == "bool")
                    {
                        itemTableFieldsHead = textTemplateTableHead.Replace("<#propertyName#>", propertyName);
                        itemTableFieldsBody = textTemplateTableBody
                            .Replace("<#propertyName#>", string.Format(
                                @"<span class='label label-danger' ng-if='!item.{0}'>Não</span><span class='label label-success' ng-if='item.{0}'>Sim</span>",
                                propertyName));
                    }

                    var dataitem = HelperFieldConfig.FieldDataItem(tableInfo, propertyName);
                    if (dataitem.IsAny())
                    {
                        itemTableFieldsHead = textTemplateTableHead.Replace("<#propertyName#>", propertyName);
                        var options = string.Empty;
                        foreach (var _item in dataitem)
                        {
                            options += string.Format("<span class='label label-success' ng-if='item.{0}==" + _item.Key + "'>" + _item.Value + "</span>", propertyName);
                        }

                        itemTableFieldsBody = textTemplateTableBody
                            .Replace("<#propertyName#>", options);
                    }

                    if (itemTableFieldsHead.IsNotNullOrEmpty() && itemTableFieldsBody.IsNotNullOrEmpty())
                    {
                        classBuilderTableHead += string.Format("{0}{1}{2}", "                ", itemTableFieldsHead, System.Environment.NewLine);
                        classBuilderTableBody += string.Format("{0}{1}{2}", "                ", itemTableFieldsBody, System.Environment.NewLine);
                    }
                }
            }

            if (classBuilderTableHead.IsNotNullOrEmpty())
                classBuilderView = classBuilderView.Replace("<#TableViewHead#>", classBuilderTableHead.Substring(0, classBuilderTableHead.LastIndexOf(Environment.NewLine)));

            if (classBuilderTableBody.IsNotNullOrEmpty())
                classBuilderView = classBuilderView.Replace("<#TableViewBody#>", classBuilderTableBody.Substring(0, classBuilderTableBody.LastIndexOf(Environment.NewLine)));

            using (var stream = new HelperStream(pathOutput).GetInstance()) { stream.Write(classBuilderView); }
        }

        private void ExecuteTemplateAngularLabelsConstants(TableInfo tableInfo, Context configContext, IEnumerable<Info> infos)
        {
            if (tableInfo.CodeCustomImplemented)
                return;

            if (!tableInfo.MakeFront)
                return;

            var pathOutput = PathOutput.PathOutputAngularLabelConstants(tableInfo, configContext);
            if (File.Exists(pathOutput))
                return;

            var pathTemplate = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, this._defineTemplateFolder.Define(tableInfo), DefineTemplateName.AngularLabelConstants(tableInfo));
            if (!File.Exists(pathTemplate))
                return;


            var textTemplateConstants = Read.AllText(tableInfo, pathTemplate, this._defineTemplateFolder);
            var classBuilderView = GenericTagsTransformer(tableInfo, configContext, textTemplateConstants);
            var classBuilderLabels = string.Empty;
            var classBuilderAttributes = string.Empty;

            if (infos.IsAny())
            {
                foreach (var item in infos)
                {
                    var itemFilterFields = string.Empty;
                    var propertyName = configContext.CamelCasing ? CamelCaseTransform(item.PropertyName) : item.PropertyName;
                    classBuilderLabels += Tabs.TabJs() + propertyName + " : '" + propertyName + "'," + System.Environment.NewLine;
                    classBuilderAttributes += Tabs.TabJs() + propertyName + " : ''," + System.Environment.NewLine;
                }
            }

            classBuilderView = classBuilderView.Replace("<#labels#>", classBuilderLabels);
            classBuilderView = classBuilderView.Replace("<#attributes#>", classBuilderAttributes);

            using (var stream = new HelperStream(pathOutput).GetInstance())
            {
                stream.Write(classBuilderView);
            }
        }
        private void ExecuteTemplateAngularController(TableInfo tableInfo, Context configContext)
        {
            if (tableInfo.CodeCustomImplemented)
                return;

            if (!tableInfo.MakeFront)
                return;

            var pathOutput = PathOutput.PathOutputAngularController(tableInfo, configContext);
            var pathTemplateController = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, this._defineTemplateFolder.Define(tableInfo), DefineTemplateName.AngularController(tableInfo));
            var textTemplateController = Read.AllText(tableInfo, pathTemplateController, this._defineTemplateFolder);
            var classBuilderController = GenericTagsTransformer(tableInfo, configContext, textTemplateController);

            using (var stream = new HelperStream(pathOutput).GetInstance()) { stream.Write(classBuilderController); }
        }
        private void ExecuteTemplateAngularControllerCustom(TableInfo tableInfo, Context configContext)
        {
            if (tableInfo.CodeCustomImplemented)
                return;

            if (!tableInfo.MakeFront)
                return;

            var pathOutput = PathOutput.PathOutputAngularControllerCustom(tableInfo, configContext);
            if (File.Exists(pathOutput))
                return;

            if (File.Exists(pathOutput) && Convert.ToBoolean(ConfigurationManager.AppSettings["GerarFrontControllerCustom"]) == false)
                return;

            var pathTemplateControllerCustom = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, this._defineTemplateFolder.Define(tableInfo), DefineTemplateName.AngularControllerCustom(tableInfo));
            var textTemplateControllerCustom = Read.AllText(tableInfo, pathTemplateControllerCustom, this._defineTemplateFolder);
            var classBuilderControllerCustom = GenericTagsTransformer(tableInfo, configContext, textTemplateControllerCustom);

            using (var stream = new HelperStream(pathOutput).GetInstance()) { stream.Write(classBuilderControllerCustom); }
        }
        private void ExecuteTemplateAngularRoutes(TableInfo tableInfo, Context configContext)
        {
            if (!tableInfo.MakeFront)
                return;

            if (tableInfo.CodeCustomImplemented)
                return;

            var pathOutput = PathOutput.PathOutputAngularRoute(tableInfo, configContext);

            var pathTemplateClass = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, this._defineTemplateFolder.Define(tableInfo), DefineTemplateName.AngularRoute(tableInfo));
            var textTemplateClass = Read.AllText(tableInfo, pathTemplateClass, this._defineTemplateFolder);

            var pathTemplateClassItem = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, this._defineTemplateFolder.Define(tableInfo), DefineTemplateName.AngularRouteItem(tableInfo));
            var textTemplateClassItem = Read.AllText(tableInfo, pathTemplateClassItem, this._defineTemplateFolder);

            var classBuilder = GenericTagsTransformer(tableInfo, configContext, textTemplateClass);
            var itemBuilder = string.Empty;

            foreach (var item in configContext.TableInfo)
            {
                var str = textTemplateClassItem.Replace("<#className#>", item.TableName);
                itemBuilder += string.Format("{0}{1}{2}", Tabs.TabSets(), str, System.Environment.NewLine);
            }

            classBuilder = classBuilder.Replace("<#RouteViewItem#>", itemBuilder);

            using (var stream = new HelperStream(pathOutput).GetInstance())
            {
                stream.Write(classBuilder);
            }

        }

        private void ExecuteTemplateAngularAccountService(TableInfo tableInfo, Context configContext)
        {
            if (!tableInfo.MakeFront)
                return;

            if (tableInfo.CodeCustomImplemented)
                return;

            var pathOutput = PathOutput.PathOutputAngularAccountService(tableInfo, configContext);
            if (File.Exists(pathOutput))
                return;

            var pathTemplateClass = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, this._defineTemplateFolder.Define(tableInfo), DefineTemplateName.AngularAccountServices(tableInfo));
            var textTemplateClass = Read.AllText(tableInfo, pathTemplateClass, this._defineTemplateFolder);
            var classBuilder = GenericTagsTransformer(tableInfo, configContext, textTemplateClass);

            using (var stream = new HelperStream(pathOutput).GetInstance())
            {
                stream.Write(classBuilder);
            }

        }
        protected override string GenericTagsTransformer(TableInfo tableInfo, Context configContext, string classBuilder, EOperation operation = EOperation.Undefined)
        {

            var keyName = tableInfo.Keys.IsAny() ? tableInfo.Keys.FirstOrDefault() : string.Empty;
            if (configContext.CamelCasing)
                keyName = CamelCaseTransform(keyName);

            classBuilder = classBuilder.Replace("<#KeyName#>", keyName);

            return base.GenericTagsTransformer(tableInfo, configContext, classBuilder, operation);
        }


        #endregion

        #region TransformField


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

        #endregion

        #region helpers

        protected override string CamelCaseTransform(string str)
        {
            var ex = this._camelCasingExceptions.Where(_ => _.ToUpper() == str.ToUpper()).SingleOrDefault();
            if (ex.IsNotNull())
                return ex;

            return str.FirstCharToLower();

        }

        public override string TransformFieldTextTag(ConfigExecutetemplate configExecutetemplate, Info info, string propertyName, string textTemplate)
        {
            throw new NotImplementedException();
        }


        #endregion

    }
}
