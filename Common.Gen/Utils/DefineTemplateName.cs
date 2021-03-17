using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Gen
{
    public static class DefineTemplateName
    {
        public static string AppPartial(TableInfo tableInfo)
        {
            return "app.partial";
        }

        public static string App(TableInfo tableInfo)
        {
            return tableInfo.MakeCrud ? "app.crud" : "app";
        }

        public static string Maps(TableInfo tableInfo)
        {
            return "maps";
        }

        public static string EntityMapBase(TableInfo tableInfo)
        {
            return "entitymapbase";
        }

        public static string MapsLength(TableInfo tableInfo)
        {
            return "maps.length";
        }

        public static string MapsRequired(TableInfo tableInfo)
        {
            return "maps.required";
        }

        public static string MapsManyToMany(TableInfo tableInfo)
        {
            return "maps.manytomany";
        }

        public static string MapsMapper(TableInfo tableInfo)
        {
            return "maps.mapper";
        }

        public static string MapsCompositeKey(TableInfo tableInfo)
        {
            return "maps.compositekey";
        }

        public static string FiltersPartial(TableInfo tableInfo)
        {
            return "filter.partial";
        }

        public static string ModelPartial(TableInfo tableInfo)
        {
            return tableInfo.MakeCrud ? "models.partial.crud" : "models.partial";
        }

        public static string Models(TableInfo tableInfo)
        {
            return tableInfo.MakeCrud ? "models.crud" : "models";
        }

        public static string EntityBase(TableInfo tableInfo)
        {
            return "entitybase";
        }

        public static string EntityFilterBasicExtension(TableInfo tableInfo)
        {
            return "entityfilterbasicextension";
        }

        public static string EntityFilterCustomExtension(TableInfo tableInfo)
        {
            return "entityfiltercustomextension";
        }

        public static string EntityOrderByDomainExtension(TableInfo tableInfo)
        {
            return "entityordercustomextension";
        }

        public static string EntityValidatorSpecificationRepository(TableInfo tableInfo)
        {
            return "entityvalidatorspecificationrepository";
        }

        public static string EntityValidatorSpecification(TableInfo tableInfo)
        {
            return "entityvalidatorspecification";
        }

        public static string EntityIsConsistentSpecification(TableInfo tableInfo)
        {
            return "entityisconsistentspecification";
        }

        public static string EntityWarningSpecification(TableInfo tableInfo)
        {
            return "entitywarningspecification";
        }

        public static string EntityCommand(TableInfo tableInfo)
        {
            return "entitycommand";
        }

        public static string EnumCommand(TableInfo tableInfo)
        {
            return "enumcommand";
        }

        public static string EntityIsSuitableSpecification(TableInfo tableInfo)
        {
            return "entityissuitablespecification";
        }

        public static string EntityValidationWarningSpecification(TableInfo tableInfo)
        {
            return "entityvalidationwarningspecification";
        }

        public static string EntityServiceBase(TableInfo tableInfo)
        {
            return "entityservicebase";
        }

        public static string EntityServiceExt(TableInfo tableInfo)
        {
            return "entityserviceext";
        }

        public static string IEntityRepository(TableInfo tableInfo)
        {
            return "ientityrepository";
        }

        public static string IEntityApplicationService(TableInfo tableInfo)
        {
            return "ientityapplicationservice";
        }

        public static string IEntityApplicationServiceBase(TableInfo tableInfo)
        {
            return "ientityapplicationservicebase";
        }

        public static string IEntityService(TableInfo tableInfo)
        {
            return "ientityservice";
        }

        public static string EntityApplicationService(TableInfo tableInfo)
        {
            return "entityapplicationservice";
        }

        public static string EntityApplicationServiceBase(TableInfo tableInfo)
        {
            return "entityapplicationservicebase";
        }

        public static string EntityRepository(TableInfo tableInfo)
        {
            return "entityrepository";
        }

        public static string EntityExt(TableInfo tableInfo)
        {
            return "entityext";
        }

        public static string ModelsNavPropertyCollection(TableInfo tableInfo)
        {
            return "models.nav.property.collection";
        }

        public static string ModelsNavPropertyInstance(TableInfo tableInfo)
        {
            return "models.nav.property.instance";
        }


        public static string AngularRoute(TableInfo tableInfo)
        {
            return "routes";
        }

        public static string AngularAccountServices(TableInfo tableInfo)
        {
            return "account.services";
        }


        public static string AngularRouteItem(TableInfo tableInfo)
        {
            return "routes.item";
        }

        public static string AngularController(TableInfo tableInfo)
        {
            return "controller";
        }

        public static string AngularControllerCustom(TableInfo tableInfo)
        {
            return "controller.custom";
        }

        public static string AngularView(TableInfo tableInfo)
        {
            return "view";
        }

        public static string AngularViewGrid(TableInfo tableInfo)
        {
            return "view.grid";
        }

        public static string AngularViewFilters(TableInfo tableInfo)
        {
            return "view.filters";
        }

        public static string AngularLabelConstants(TableInfo tableInfo)
        {
            return "labels.constants";
        }

        public static string AngularForm(TableInfo tableInfo)
        {
            return "form";
        }

        public static string AngularModule(TableInfo tableInfo)
        {
            return "modules";
        }

        public static string AngularConstants(TableInfo tableInfo)
        {
            return "constants";
        }

        public static string AngularbreadcrumbConstants(TableInfo tableInfo)
        {
            return "breadcrumb.constants";
        }

        public static string AngularIndex(TableInfo tableInfo)
        {
            return "index";
        }

        public static string AngularLayout(TableInfo tableInfo)
        {
            return "layout";
        }

        public static string AngularMainController(TableInfo tableInfo)
        {
            return "main.controller";
        }

        public static string AngularHomeController(TableInfo tableInfo)
        {
            return "home.controller";
        }

        public static string AngularLoginController(TableInfo tableInfo)
        {
            return "login.controller";
        }

        public static string AngularHomeView(TableInfo tableInfo)
        {
            return "view.home";
        }

        public static string AngularLoginView(TableInfo tableInfo)
        {
            return "view.login";
        }

        public static string AngularRouteConfigCustom(TableInfo tableInfo)
        {
            return "route.custom.config";
        }

        public static string AngularValueConfig(TableInfo tableInfo)
        {
            return "value.config";
        }

        public static string AngularTokenConstants(TableInfo tableInfo)
        {
            return "token.constants";
        }

        public static string AngularPackage(TableInfo tableInfo)
        {
            return "package";
        }

        public static string AngularBreadCrumbSharedView(TableInfo tableInfo)
        {
            return "_breadcrumb";
        }

        public static string AngularExclusaoSharedView(TableInfo tableInfo)
        {
            return "_exclusao.modal";
        }

        public static string AngularExecuteSharedView(TableInfo tableInfo)
        {
            return "_execute.modal";
        }

        public static string AngularHeaderSharedView(TableInfo tableInfo)
        {
            return "_header";
        }

        public static string AngularMenuSharedView(TableInfo tableInfo)
        {
            return "_menu";
        }

        public static string AngularUnauthorizedSharedView(TableInfo tableInfo)
        {
            return "_unauthorized";
        }

        public static string AngularApp(TableInfo tableInfo)
        {
            return "app";
        }

        public static string AngularEditInPage(TableInfo tableInfo)
        {
            return "edit";
        }

        public static string AngularCreateInPage(TableInfo tableInfo)
        {
            return "create";
        }

        public static string AngularDetails(TableInfo tableInfo)
        {
            return "details";
        }


        public static string AngularFilter(TableInfo tableInfo)
        {
            return "filter";
        }

        public static string AngularTableHead(TableInfo tableInfo)
        {
            return "table.head";
        }

        public static string AngularTableBody(TableInfo tableInfo)
        {
            return "table.body";
        }

        public static string AngularModal(TableInfo tableInfo)
        {
            return "modal";
        }

        public static string AngularModalCreate(TableInfo tableInfo)
        {
            return "modal.create";
        }

        public static string AngularModalEdit(TableInfo tableInfo)
        {
            return "modal.edit";
        }

        public static string AngularDetaislSection(TableInfo tableInfo)
        {
            return "details.section";
        }


        public static string ModelsValidation(TableInfo tableInfo)
        {
            return "models.validation";
        }

        public static string ModelsValidationProperty(TableInfo tableInfo)
        {
            return "models.validation.property";
        }

        public static string ModelsValidationLength(TableInfo tableInfo)
        {
            return "models.validation.length";
        }

        public static string ModelsValidationRequired(TableInfo tableInfo)
        {
            return "models.validation.required";
        }

        public static string ModelsPartialValidation(TableInfo tableInfo)
        {
            return "models.partial.validation";
        }

        public static string ModelsPartialValidationProperty(TableInfo tableInfo)
        {
            return "models.validation.property";
        }

        public static string ModelsPartialValidationLength(TableInfo tableInfo)
        {
            return "models.validation.length";
        }

        public static string ModelsPartialValidationRequired(TableInfo tableInfo)
        {
            return "models.validation.required";
        }

        public static string SimpleFilters(TableInfo tableInfo)
        {
            return "models.simplefilters";
        }

        public static string FiltersExpression(TableInfo tableInfo)
        {
            return "models.filters.expression";
        }

        public static string FilterBasicExtension(TableInfo tableInfo)
        {
            return "filterbasicextension";
        }

        public static string AuditCall(TableInfo tableInfo)
        {
            return "models.audit.call";
        }

        public static string ApiCrud(TableInfo tableInfo)
        {
            return tableInfo.MakeCrud ? "api.crud" : "api";
        }

        public static string ApiBase(TableInfo tableInfo)
        {
            return "apibase";
        }

        public static string Api(TableInfo tableInfo)
        {
            return "api";
        }

        public static string ApiMore(TableInfo tableInfo)
        {
            return "api.more";
        }

        public static string ApiMoreBase(TableInfo tableInfo)
        {
            return "apibase.more";
        }

        public static string DtoNavPropertyInstance(TableInfo tableInfo)
        {
            return "dto.nav.property.instance";
        }

        public static string DtoSpecialized(TableInfo tableInfo)
        {
            return "dto.specialized";
        }

        public static string DtoNavPropertyCollection(TableInfo tableInfo)
        {
            return "dto.nav.property.collection";
        }

        public static string DtoSpecializedReport(TableInfo tableInfo)
        {
            return "dto.specialized.report";
        }

        public static string DtoSpecilizedResult(TableInfo tableInfo)
        {
            return "dto.specialized.result";
        }

        public static string ModelsProperty(TableInfo tableInfo)
        {
            return "models.property";
        }

        public static string EntityProperty(TableInfo tableInfo)
        {
            return "entitypropertys";
        }

        public static string EntityMethodSeters(TableInfo tableInfo)
        {
            return "entitymethodseters";
        }

        public static string ModelsAudit(TableInfo tableInfo)
        {
            return "models.audit";
        }

        public static string Summary(TableInfo tableInfo)
        {
            return "summary";
        }

        public static string ModelsCustom(TableInfo tableInfo)
        {
            return "models.custom";
        }

        public static string Dto(TableInfo tableInfo)
        {
            return "dto";
        }

        public static string Custom(TableInfo tableInfo)
        {
            return "custom";
        }

        public static string CustomItems(TableInfo tableInfo)
        {
            return "custom.item";
        }

        public static string Filter(TableInfo tableInfo)
        {
            return "filter";
        }

        public static string ApiGet(TableInfo tableInfo)
        {
            return "api.get";
        }

        public static string ApiCustomMethod(TableInfo tableInfo)
        {
            return "api.custom.method";
        }

        public static string IAppCustomMethod(TableInfo tableInfo)
        {
            return "ientityapplicationservice.custom.method";
        }

        public static string AppCustomMethod(TableInfo tableInfo)
        {
            return "entityapplicationservice.custom.method";
        }

        public static string MapPartial(TableInfo tableInfo)
        {
            return "maps.partial";
        }

        public static string EntityMapExtension(TableInfo tableInfo)
        {
            return "entitymapext";
        }

        public static string AutoMapperProfile(TableInfo tableInfo)
        {
            return "profile";
        }

        public static string AutoMapperProfileCustom(TableInfo tableInfo)
        {
            return "profilecustom";
        }

        public static string PrecompiledViewMain()
        {
            return "precompiledview.main";
        }

        public static string PrecompiledViewBasic()
        {
            return "precompiledview.basic";
        }

        public static string PrecompiledViewConditional()
        {
            return "precompiledview.conditional";
        }

        public static string PrecompiledView()
        {
            return "precompiledview.view";
        }

        public static string Automapper(TableInfo tableInfo)
        {
            return "automapper";
        }

        public static string ApiStart(TableInfo tableInfo)
        {
            return "api.start";
        }

        public static string ApiCurrentUser(TableInfo tableInfo)
        {
            return "api.currentuser";
        }

        public static string ApiUpload(TableInfo tableInfo)
        {
            return "api.upload";
        }

        public static string ApiDownalod(TableInfo tableInfo)
        {
            return "api.download";
        }

        public static string ApiHealth(TableInfo tableInfo)
        {
            return "api.health";
        }

        public static string Appsettings(TableInfo tableInfo)
        {
            return "appsettings.json";
        }

        public static string ProjectApi(TableInfo tableInfo)
        {
            return "projectapi";
        }

        public static string ProjectApp(TableInfo tableInfo)
        {
            return "projectapp";
        }

        public static string ProjectDto(TableInfo tableInfo)
        {
            return "projectdto";
        }

        public static string ProjectDomain(TableInfo tableInfo)
        {
            return "projectdomain";
        }

        public static string ProjectFilter(TableInfo tableInfo)
        {
            return "projectfilter";
        }

        public static string ProjectSummary(TableInfo tableInfo)
        {
            return "projectsummary";
        }

        public static string ProjectData(TableInfo tableInfo)
        {
            return "projectdata";
        }

        public static string ConfigDomain(TableInfo tableInfo)
        {
            return "config.domain";
        }

        public static string HelperValidateAuth(TableInfo tableInfo)
        {
            return "helper.validate.auth";
        }

        public static string ProfileRegisters(TableInfo tableInfo)
        {
            return "profile.registers";
        }

        public static string ProfileRegistersSpecilize(TableInfo tableInfo)
        {
            return "profile.registers.specilize";
        }

        public static string ProfileRegistersSpecilizeResult(TableInfo tableInfo)
        {
            return "profile.registers.specilize.result";
        }

        public static string ProfileRegistersSpecilizeReport(TableInfo tableInfo)
        {
            return "profile.registers.specilize.report";
        }

        public static string ProfileRegistersSpecilizeDetails(TableInfo tableInfo)
        {
            return "profile.registers.specilize.details";
        }

        public static string DtoSpecializedDetails(TableInfo tableInfo)
        {
            return "dto.specialized.details";
        }

        public static string ModelCustomFilters(TableInfo tableInfo)
        {
            return "models.customfilters";
        }

        public static string Uri(TableInfo tableInfo)
        {
            return "uri";
        }

        public static string ApiTransaction(TableInfo tableInfo)
        {
            return "api.transaction";
        }

        public static string RepositoryTransaction(TableInfo tableInfo)
        {
            return "repository.transaction";
        }

        public static string Context(TableInfo tableInfo)
        {
            return "context";
        }

        public static string ContextMt(TableInfo tableInfo)
        {
            return "contextmultitenancy";
        }

        public static string ContextMappers(TableInfo tableInfo)
        {
            return "context.mappers";
        }

        public static string Container(TableInfo tableInfo)
        {
            return "container";
        }

        public static string ContainerWeb(TableInfo tableInfo)
        {
            return "container.web";
        }

        public static string Sso(TableInfo tableInfo)
        {
            return "sso.configauth";
        }

        public static string CrossCutingProfileCustom(TableInfo tableInfo)
        {
            return "crosscuting.profilecustom.auth";
        }


        public static string AppTest(TableInfo tableInfo)
        {
            return tableInfo.MakeCrud ? "app.test.crud" : "app.test";
        }

        public static string AppTestMoq(TableInfo tableInfo)
        {
            return "app.test.moq";
        }

        public static string AppTestMoqValues(TableInfo tableInfo)
        {
            return "app.test.moqvalues";
        }

        public static string AppTestMoqPartial(TableInfo tableInfo)
        {
            return "app.test.moq.partial";
        }

        public static string AppTestReleted(TableInfo tableInfo)
        {
            return "app.test.reletedvalues";
        }

        public static string ApiTest(TableInfo tableInfo)
        {
            return tableInfo.MakeCrud ? "api.test.crud" : "api.test";
        }

        public static string ApiTestMoqValues(TableInfo tableInfo)
        {
            return "api.test.moqvalues";
        }

        public static string ContainerInjections(TableInfo tableInfo)
        {
            return "container.injections";
        }

        public static string ContainerInjectionsWeb(TableInfo tableInfo)
        {
            return "container.injections.web";
        }

        public static string ContainerPartial(TableInfo tableInfo)
        {
            return "container.partial";
        }

        public static string ContainerPartialWeb(TableInfo tableInfo)
        {
            return "container.web.partial";
        }
    }
}
