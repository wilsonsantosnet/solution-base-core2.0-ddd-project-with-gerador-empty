using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Gen
{
    static class PathOutput
    {

        #region ext

        public static string PathOutputMapsPartial(TableInfo tableInfo, Context configContext)
        {
            if (tableInfo.InheritQuery)
                return PathOutputMapsPartialinherit(tableInfo, configContext);

            var pathOutput = string.Empty;
            var pathBase = PathOutputBase.PathBase(configContext.OutputClassInfra, configContext.UsePathProjects);
            pathOutput = Path.Combine(pathBase, "Maps", string.Format("{0}Map.ext.{1}", tableInfo.ClassName, "cs"));
            PathOutputBase.MakeDirectory("Maps", pathBase);
            return pathOutput;
        }

        public static string PathOutputEntityMapExtension(TableInfo tableInfo, Context configContext)
        {

            var pathOutput = string.Empty;
            var pathBase = PathOutputBase.PathBase(configContext.OutputClassInfra, configContext.UsePathProjects);
            pathOutput = Path.Combine(pathBase, "Maps", tableInfo.ClassName, string.Format("{0}Map.ext.{1}", tableInfo.ClassName, "cs"));
            PathOutputBase.MakeDirectory(pathBase, "Maps", tableInfo.ClassName);
            return pathOutput;
        }


        public static string PathOutputMapsPartialinherit(TableInfo tableInfo, Context configContext)
        {
            var pathOutput = string.Empty;

            var pathBase = PathOutputBase.PathBase(configContext.OutputClassInfra, configContext.UsePathProjects);
            var fileName = tableInfo.ClassName;
            pathOutput = Path.Combine(pathBase, "Maps", tableInfo.BoundedContext, fileName, string.Format("{0}Map.ext.{1}", fileName, "cs"));
            PathOutputBase.MakeDirectory(pathBase, "Maps", tableInfo.BoundedContext, fileName);
            return pathOutput;


        }

        public static string PathOutputDomainModelsPartial(TableInfo tableInfo, Context configContext)
        {
            if (tableInfo.InheritQuery)
                return PathOutputDomainModelsPartialinherit(tableInfo, configContext);

            var pathOutput = string.Empty;
            var pathBase = PathOutputBase.PathBase(configContext.OutputClassDomain, configContext.UsePathProjects);

            var filename = tableInfo.ClassName;
            pathOutput = Path.Combine(pathBase, "Models", filename, string.Format("{0}.ext.{1}", filename, "cs"));
            PathOutputBase.MakeDirectory(pathBase, "Models", filename);

            return pathOutput;
        }


        public static string PathOutputDomainModelsPartialinherit(TableInfo tableInfo, Context configContext)
        {
            var pathOutput = string.Empty;
            var pathBase = PathOutputBase.PathBase(configContext.OutputClassDomain, configContext.UsePathProjects);

            var filename = tableInfo.ClassName;
            pathOutput = Path.Combine(pathBase, "Models", tableInfo.BoundedContext, "Entitys", filename, string.Format("{0}.ext.{1}", filename, "cs"));
            PathOutputBase.MakeDirectory(pathBase, "Models", tableInfo.BoundedContext, "Entitys", filename);

            return pathOutput;
        }

        public static string PathOutputDomainModelsValidationPartial(TableInfo tableInfo, Context configContext)
        {
            var pathOutput = string.Empty;

            var pathBase = PathOutputBase.PathBase(configContext.OutputClassDomain, configContext.UsePathProjects);

            pathOutput = Path.Combine(pathBase, "Models", tableInfo.ClassName, string.Format("{0}.Validation.ext.{1}", tableInfo.ClassName, "cs"));
            PathOutputBase.MakeDirectory(pathBase, "Models", tableInfo.ClassName);

            return pathOutput;

        }


        public static string PathOutputDomainModelsCustom(TableInfo tableInfo, Context configContext)
        {
            if (tableInfo.InheritQuery)
                return PathOutputDomainModelsCustomInherit(tableInfo, configContext);

            var pathOutput = string.Empty;
            var pathBase = PathOutputBase.PathBase(configContext.OutputClassDomain, configContext.UsePathProjects);
            var filename = tableInfo.ClassName;
            pathOutput = Path.Combine(pathBase, "Models", filename, string.Format("{0}Custom.ext.{1}", filename, "cs"));
            PathOutputBase.MakeDirectory(pathBase, "Models", filename);

            return pathOutput;

        }



        public static string PathOutputDomainModelsCustomInherit(TableInfo tableInfo, Context configContext)
        {
            var pathOutput = string.Empty;
            var pathBase = PathOutputBase.PathBase(configContext.OutputClassDomain, configContext.UsePathProjects);
            var filename = tableInfo.ClassName;
            pathOutput = Path.Combine(pathBase, "Models", tableInfo.BoundedContext, filename, string.Format("{0}Custom.ext.{1}", filename, "cs"));
            PathOutputBase.MakeDirectory(pathBase, "Models", tableInfo.BoundedContext, filename);

            return pathOutput;

        }

        public static string PathOutputCustomFilters(TableInfo tableInfo, Context configContext)
        {
            var pathOutput = string.Empty;
            var pathBase = PathOutputBase.PathBase(configContext.OutputClassDomain, configContext.UsePathProjects);

            var filename = string.Format("{0}CustomFiltersFiltersExtensions.", tableInfo.ClassName);
            pathOutput = Path.Combine(pathBase, "Models", tableInfo.BoundedContext, "Entitys", tableInfo.ClassName, string.Format("{0}.ext.{1}", filename, "cs"));
            PathOutputBase.MakeDirectory(pathBase, "Models", tableInfo.BoundedContext, "Entitys", tableInfo.ClassName);

            return pathOutput;
        }
        public static string PathOutputSimpleFilters(TableInfo tableInfo, Context configContext)
        {
            var pathOutput = string.Empty;
            var pathBase = PathOutputBase.PathBase(configContext.OutputClassDomain, configContext.UsePathProjects);

            var filename = string.Format("{0}SimpleFiltersFiltersExtensions.", tableInfo.ClassName);
            pathOutput = Path.Combine(pathBase, "Models", tableInfo.BoundedContext, "Entitys", tableInfo.ClassName, string.Format("{0}.ext.{1}", filename, "cs"));
            PathOutputBase.MakeDirectory(pathBase, "Models", tableInfo.BoundedContext, "Entitys", tableInfo.ClassName);

            return pathOutput;
        }

        public static string PathOutputDomainEntitysServiceExt(TableInfo tableInfo, Context configContext)
        {
            var pathOutput = string.Empty;

            var pathBase = PathOutputBase.PathBase(configContext.OutputClassDomain, configContext.UsePathProjects);
            pathOutput = Path.Combine(pathBase, "Services", tableInfo.ClassName, string.Format("{0}Service.ext.{1}", tableInfo.ClassName, "cs"));
            PathOutputBase.MakeDirectory(pathBase, "Services", tableInfo.ClassName);


            return pathOutput;
        }

        public static string PathOutputAppPartial(TableInfo tableInfo, Context configContext)
        {
            if (tableInfo.InheritQuery)
                return PathOutputAppPartialInherit(tableInfo, configContext);

            var pathOutput = string.Empty;
            var pathBase = PathOutputBase.PathBase(configContext.OutputClassApp, configContext.UsePathProjects);
            pathOutput = Path.Combine(pathBase, "Application", string.Format("{0}App.ext.{1}", tableInfo.ClassName, "cs"));
            PathOutputBase.MakeDirectory("Application", pathBase);
            return pathOutput;
        }

        public static string PathOutputAppPartialInherit(TableInfo tableInfo, Context configContext)
        {
            var pathOutput = string.Empty;
            var pathBase = PathOutputBase.PathBase(configContext.OutputClassApp, configContext.UsePathProjects);
            pathOutput = Path.Combine(pathBase, "Application", tableInfo.BoundedContext, string.Format("{0}App.ext.{1}", tableInfo.InheritClassName, "cs"));
            PathOutputBase.MakeDirectory(pathBase, "Application", tableInfo.BoundedContext);
            return pathOutput;
        }

        public static string PathOutputContainerPartial(Context configContext)
        {
            var pathOutput = string.Empty;

            var pathBase = PathOutputBase.PathBase(configContext.OutputClassApp, configContext.UsePathProjects);

            pathOutput = Path.Combine(pathBase, "Config", string.Format("ConfigContainer{0}.ext.{1}", configContext.Module, "cs"));
            PathOutputBase.MakeDirectory("Config", pathBase);

            return pathOutput;
        }

        public static string PathOutputContainerPartialWeb(Context configContext)
        {
            var pathOutput = string.Empty;

            var pathBase = PathOutputBase.PathBase(configContext.OutputClassApi, configContext.UsePathProjects);

            pathOutput = Path.Combine(pathBase, "Config", string.Format("ConfigContainerWeb{0}.ext.{1}", configContext.Module, "cs"));
            PathOutputBase.MakeDirectory("Config", pathBase);

            return pathOutput;
        }

        public static string PathOutputContainerPartialApi(Context configContext)
        {
            var pathOutput = string.Empty;

            var pathBase = PathOutputBase.PathBase(configContext.OutputClassApi, configContext.UsePathProjects);

            pathOutput = Path.Combine(pathBase, "Config", string.Format("ConfigContainer{0}.ext.{1}", configContext.ContextName, "cs"));
            PathOutputBase.MakeDirectory("Config", pathBase);

            return pathOutput;
        }

        public static string PathOutputDomainEntitysExt(TableInfo tableInfo, Context configContext)
        {
            var pathOutput = string.Empty;

            var pathBase = PathOutputBase.PathBase(configContext.OutputClassDomain, configContext.UsePathProjects);
            pathOutput = Path.Combine(pathBase, "Entitys", tableInfo.ClassName, string.Format("{0}.ext.{1}", tableInfo.ClassName, "cs"));
            PathOutputBase.MakeDirectory(pathBase, "Entitys", tableInfo.ClassName);


            return pathOutput;
        }

        public static string PathOutputApplicatioEntityApplicationService(TableInfo tableInfo, Context configContext)
        {
            var pathOutput = string.Empty;

            var pathBase = PathOutputBase.PathBase(configContext.OutputClassApp, configContext.UsePathProjects);
            pathOutput = Path.Combine(pathBase, "App", tableInfo.ClassName, string.Format("{0}ApplicationService.ext.{1}", tableInfo.ClassName, "cs"));
            PathOutputBase.MakeDirectory(pathBase, "App", tableInfo.ClassName);


            return pathOutput;
        }

        public static string PathOutputFilterPartial(TableInfo tableInfo, Context configContext)
        {
            var pathOutput = string.Empty;
            var pathBase = PathOutputBase.PathBase(configContext.OutputClassFilter, configContext.UsePathProjects);
            var fileName = tableInfo.ClassName;
            pathOutput = Path.Combine(pathBase, "Filters", string.Format("{0}Filter.ext.{1}", fileName, "cs"));
            PathOutputBase.MakeDirectory("Filters", pathBase);
            return pathOutput;
        }


        public static string PathOutputFilterPartialWithFolder(TableInfo tableInfo, Context configContext)
        {
            var pathOutput = string.Empty;
            var pathBase = PathOutputBase.PathBase(configContext.OutputClassFilter, configContext.UsePathProjects);
            var fileName = tableInfo.ClassName;
            pathOutput = Path.Combine(pathBase, "Filters", tableInfo.ClassName, string.Format("{0}Filter.ext.{1}", fileName, "cs"));
            PathOutputBase.MakeDirectory(pathBase, "Filters", tableInfo.ClassName);
            return pathOutput;
        }


        public static string PathOutputDtoSpecialized(TableInfo tableInfo, Context configContext)
        {
            if (tableInfo.InheritQuery)
                return PathOutputDtoSpecializedInherit(tableInfo, configContext);

            var pathOutput = string.Empty;
            var pathBase = PathOutputBase.PathBase(configContext.OutputClassDto, configContext.UsePathProjects);
            pathOutput = Path.Combine(pathBase, "Dto", tableInfo.ClassName, string.Format("{0}DtoSpecialized.ext.{1}", tableInfo.ClassName, "cs"));
            PathOutputBase.MakeDirectory(pathBase, "Dto", tableInfo.ClassName);
            return pathOutput;
        }

        public static string PathOutputDtoSpecializedInherit(TableInfo tableInfo, Context configContext)
        {
            var pathOutput = string.Empty;
            var pathBase = PathOutputBase.PathBase(configContext.OutputClassDto, configContext.UsePathProjects);
            pathOutput = Path.Combine(pathBase, "Dto", tableInfo.BoundedContext, tableInfo.ClassName, string.Format("{0}DtoSpecialized.ext.{1}", tableInfo.ClassName, "cs"));
            PathOutputBase.MakeDirectory(pathBase, "Dto", tableInfo.BoundedContext, tableInfo.ClassName);
            return pathOutput;
        }

        public static string PathOutputDtoSpecializedResult(TableInfo tableInfo, Context configContext)
        {
            if (tableInfo.InheritQuery)
                return PathOutputDtoSpecializedResultInherit(tableInfo, configContext);

            var pathOutput = string.Empty;
            var pathBase = PathOutputBase.PathBase(configContext.OutputClassDto, configContext.UsePathProjects);
            pathOutput = Path.Combine(pathBase, "Dto", tableInfo.ClassName, string.Format("{0}DtoSpecializedResult.ext.{1}", tableInfo.ClassName, "cs"));
            PathOutputBase.MakeDirectory(pathBase, "Dto", tableInfo.ClassName);
            return pathOutput;
        }

        public static string PathOutputDtoSpecializedResultInherit(TableInfo tableInfo, Context configContext)
        {
            var pathOutput = string.Empty;
            var pathBase = PathOutputBase.PathBase(configContext.OutputClassDto, configContext.UsePathProjects);
            pathOutput = Path.Combine(pathBase, "Dto", tableInfo.BoundedContext, tableInfo.ClassName, string.Format("{0}DtoSpecializedResult.ext.{1}", tableInfo.ClassName, "cs"));
            PathOutputBase.MakeDirectory(pathBase, "Dto", tableInfo.BoundedContext, tableInfo.ClassName);
            return pathOutput;
        }

        public static string PathOutputDtoSpecializedReport(TableInfo tableInfo, Context configContext)
        {
            if (tableInfo.InheritQuery)
                return PathOutputDtoSpecializedReportInherit(tableInfo, configContext);

            var pathOutput = string.Empty;
            var pathBase = PathOutputBase.PathBase(configContext.OutputClassDto, configContext.UsePathProjects);
            pathOutput = Path.Combine(pathBase, "Dto", tableInfo.ClassName, string.Format("{0}DtoSpecializedReport.ext.{1}", tableInfo.ClassName, "cs"));
            PathOutputBase.MakeDirectory(pathBase, "Dto", tableInfo.ClassName);

            return pathOutput;
        }

        public static string PathOutputDtoSpecializedReportInherit(TableInfo tableInfo, Context configContext)
        {
            var pathOutput = string.Empty;
            var pathBase = PathOutputBase.PathBase(configContext.OutputClassDto, configContext.UsePathProjects);
            pathOutput = Path.Combine(pathBase, "Dto", tableInfo.BoundedContext, tableInfo.ClassName, string.Format("{0}DtoSpecializedReport.ext.{1}", tableInfo.ClassName, "cs"));
            PathOutputBase.MakeDirectory(pathBase, "Dto", tableInfo.BoundedContext, tableInfo.ClassName);

            return pathOutput;
        }

        public static string PathOutputDtoSpecializedDetails(TableInfo tableInfo, Context configContext)
        {
            if (tableInfo.InheritQuery)
                return PathOutputDtoSpecializedDetailsInherit(tableInfo, configContext);

            var pathOutput = string.Empty;
            var pathBase = PathOutputBase.PathBase(configContext.OutputClassDto, configContext.UsePathProjects);
            pathOutput = Path.Combine(pathBase, "Dto", tableInfo.ClassName, string.Format("{0}DtoSpecializedDetails.ext.{1}", tableInfo.ClassName, "cs"));
            PathOutputBase.MakeDirectory(pathBase, "Dto", tableInfo.ClassName);

            return pathOutput;
        }

        public static string PathOutputDtoSpecializedDetailsInherit(TableInfo tableInfo, Context configContext)
        {
            var pathOutput = string.Empty;
            var pathBase = PathOutputBase.PathBase(configContext.OutputClassDto, configContext.UsePathProjects);
            pathOutput = Path.Combine(pathBase, "Dto", tableInfo.BoundedContext, tableInfo.ClassName, string.Format("{0}DtoSpecializedDetails.ext.{1}", tableInfo.ClassName, "cs"));
            PathOutputBase.MakeDirectory(pathBase, "Dto", tableInfo.BoundedContext, tableInfo.ClassName);

            return pathOutput;
        }


        public static string PathOutputApplicationTestMoqPartial(TableInfo tableInfo, Context configContext)
        {
            var pathOutput = string.Empty;

            var pathBase = PathOutputBase.PathBase(configContext.OutputClassTestsApp, configContext.UsePathProjects);

            pathOutput = Path.Combine(pathBase, "Moq", configContext.Module, string.Format("Helper{0}Moq.ext.{1}", tableInfo.ClassName, "cs"));
            PathOutputBase.MakeDirectory(Path.Combine("Moq", configContext.Module), pathBase);


            return pathOutput;
        }

        public static string PathOutputApiTestPartial(TableInfo tableInfo, Context configContext)
        {
            var pathOutput = string.Empty;

            var pathBase = PathOutputBase.PathBase(configContext.OutputClassTestsApi, configContext.UsePathProjects);

            pathOutput = Path.Combine(pathBase, configContext.Module, string.Format("UnitTest{0}Api.ext.{1}", tableInfo.ClassName, "cs"));
            PathOutputBase.MakeDirectory(configContext.Module, pathBase);


            return pathOutput;
        }


        #endregion


        public static string PathOutputEntityMapBase(TableInfo tableInfo, Context configContext)
        {

            var pathOutput = string.Empty;
            var pathBase = PathOutputBase.PathBase(configContext.OutputClassInfra, configContext.UsePathProjects);
            pathOutput = Path.Combine(pathBase, "Maps", tableInfo.ClassName, string.Format("{0}MapBase.{1}", tableInfo.ClassName, "cs"));
            PathOutputBase.MakeDirectory(pathBase, "Maps", tableInfo.ClassName);
            return pathOutput;
        }

        public static string PathOutputMaps(TableInfo tableInfo, Context configContext)
        {
            if (tableInfo.InheritQuery)
                return PathOutputMapsInherit(tableInfo, configContext);

            var pathOutput = string.Empty;
            var pathBase = PathOutputBase.PathBase(configContext.OutputClassInfra, configContext.UsePathProjects);
            pathOutput = Path.Combine(pathBase, "Maps", string.Format("{0}Map.{1}", tableInfo.ClassName, "cs"));
            PathOutputBase.MakeDirectory("Maps", pathBase);
            return pathOutput;
        }

        public static string PathOutputTransactionRepositoy(TableInfo tableInfo, Context configContext)
        {

            var pathOutput = string.Empty;
            var pathBase = PathOutputBase.PathBase(configContext.OutputClassInfra, configContext.UsePathProjects);
            pathOutput = Path.Combine(pathBase, "Repository", string.Format("{0}RepositoryTransaction.{1}", tableInfo.ClassName, "cs"));
            PathOutputBase.MakeDirectory("Repository", pathBase);
            return pathOutput;
        }

        public static string PathOutputMapsInherit(TableInfo tableInfo, Context configContext)
        {
            var pathOutput = string.Empty;
            var pathBase = PathOutputBase.PathBase(configContext.OutputClassInfra, configContext.UsePathProjects);
            pathOutput = Path.Combine(pathBase, "Maps", tableInfo.BoundedContext, tableInfo.ClassName, string.Format("{0}Map.{1}", tableInfo.ClassName, "cs"));
            PathOutputBase.MakeDirectory(pathBase, "Maps", tableInfo.BoundedContext, tableInfo.ClassName);
            return pathOutput;
        }

        public static string PathOutputContextsInherit(TableInfo tableInfo, Context configContext)
        {
            var pathOutput = string.Empty;

            var pathBase = PathOutputBase.PathBase(configContext.OutputClassInfra, configContext.UsePathProjects);
            var fileName = tableInfo.BoundedContext;
            pathOutput = Path.Combine(pathBase, "Maps", tableInfo.BoundedContext, string.Format("DbContext{0}.{1}", fileName, "cs"));
            PathOutputBase.MakeDirectory(pathBase, "Maps", tableInfo.BoundedContext);

            return pathOutput;
        }

        public static string PathOutputDbContext(Context configContext)
        {
            var pathOutput = string.Empty;
            var pathBase = PathOutputBase.PathBase(configContext.OutputClassInfra, configContext.UsePathProjects);
            pathOutput = Path.Combine(pathBase, "Context", string.Format("DbContext{0}.{1}", configContext.ContextName, "cs"));
            PathOutputBase.MakeDirectory("Context", pathBase);

            return pathOutput;
        }

        public static string PathOutputDbContextMt(Context configContext)
        {
            var pathOutput = string.Empty;
            var pathBase = PathOutputBase.PathBase(configContext.OutputClassInfra, configContext.UsePathProjects);

            pathOutput = Path.Combine(pathBase, "Context", string.Format("DbContextMutiTenancy{0}.{1}", configContext.ContextName, "cs"));
            PathOutputBase.MakeDirectory("Context", pathBase);

            return pathOutput;
        }

        public static string PathOutputDomainModelsValidation(TableInfo tableInfo, Context configContext)
        {
            var pathOutput = string.Empty;

            var pathBase = PathOutputBase.PathBase(configContext.OutputClassDomain, configContext.UsePathProjects);
            pathOutput = Path.Combine(pathBase, "Models", tableInfo.ClassName, string.Format("{0}.Validation.{1}", tableInfo.ClassName, "cs"));
            PathOutputBase.MakeDirectory(pathBase, "Models", tableInfo.ClassName);

            return pathOutput;

        }

        public static string PathOutputDomainModels(TableInfo tableInfo, Context configContext)
        {
            var pathOutput = string.Empty;

            var pathBase = PathOutputBase.PathBase(configContext.OutputClassDomain, configContext.UsePathProjects);
            pathOutput = Path.Combine(pathBase, "Models", tableInfo.ClassName, string.Format("{0}.{1}", tableInfo.ClassName, "cs"));
            PathOutputBase.MakeDirectory(pathBase, "Models", tableInfo.ClassName);


            return pathOutput;
        }

        public static string PathOutputDomainEntitysBase(TableInfo tableInfo, Context configContext)
        {
            var pathOutput = string.Empty;

            var pathBase = PathOutputBase.PathBase(configContext.OutputClassDomain, configContext.UsePathProjects);
            pathOutput = Path.Combine(pathBase, "Entitys", tableInfo.ClassName, string.Format("{0}Base.{1}", tableInfo.ClassName, "cs"));
            PathOutputBase.MakeDirectory(pathBase, "Entitys", tableInfo.ClassName);


            return pathOutput;
        }

        public static string PathOutputDomainEntitysValidatorSpecificationOld(TableInfo tableInfo, Context configContext)
        {
            var pathOutput = string.Empty;

            var pathBase = PathOutputBase.PathBase(configContext.OutputClassDomain, configContext.UsePathProjects);
            pathOutput = Path.Combine(pathBase, "Validations", tableInfo.ClassName, string.Format("{0}EstaConsistenteValidation.{1}", tableInfo.ClassName, "cs"));
            PathOutputBase.MakeDirectory(pathBase, "Validations", tableInfo.ClassName);


            return pathOutput;
        }

        public static string PathOutputDomainEntitysValidatorSpecification(TableInfo tableInfo, Context configContext)
        {
            var pathOutput = string.Empty;

            var pathBase = PathOutputBase.PathBase(configContext.OutputClassDomain, configContext.UsePathProjects);
            pathOutput = Path.Combine(pathBase, "Validations", tableInfo.ClassName, string.Format("{0}IsConsistentValidation.{1}", tableInfo.ClassName, "cs"));
            PathOutputBase.MakeDirectory(pathBase, "Validations", tableInfo.ClassName);


            return pathOutput;
        }

        public static string PathOutputDomainEntityCommand(TableInfo tableInfo, Context configContext, string command)
        {
            var pathOutput = string.Empty;

            var pathBase = PathOutputBase.PathBase(configContext.OutputClassDomain, configContext.UsePathProjects);
            pathOutput = Path.Combine(pathBase, "Commands", tableInfo.ClassName, string.Format("{0}Command.{1}", command, "cs"));
            PathOutputBase.MakeDirectory(pathBase, "Commands", tableInfo.ClassName);

            return pathOutput;
        }

        public static string PathOutputDomainEumCommand(TableInfo tableInfo, Context configContext)
        {
            var pathOutput = string.Empty;

            var pathBase = PathOutputBase.PathBase(configContext.OutputClassEnum, configContext.UsePathProjects);
            pathOutput = Path.Combine(pathBase, tableInfo.ClassName, string.Format("EnumCommand.{0}", "cs"));
            PathOutputBase.MakeDirectory(tableInfo.ClassName, pathBase);

            return pathOutput;
        }

        public static string PathOutputDomainEntityIsConsistentSpecification(TableInfo tableInfo, Context configContext, string especification)
        {
            var pathOutput = string.Empty;

            var pathBase = PathOutputBase.PathBase(configContext.OutputClassDomain, configContext.UsePathProjects);
            pathOutput = Path.Combine(pathBase, "Specification", tableInfo.ClassName, string.Format("{0}IsConsistentSpecification.{1}",  especification, "cs"));
            PathOutputBase.MakeDirectory(pathBase, "Specification", tableInfo.ClassName);


            return pathOutput;
        }

        public static string PathOutputDomainEntityIsSuitableSpecification(TableInfo tableInfo, Context configContext, string especification)
        {
            var pathOutput = string.Empty;

            var pathBase = PathOutputBase.PathBase(configContext.OutputClassDomain, configContext.UsePathProjects);
            pathOutput = Path.Combine(pathBase, "Specification", tableInfo.ClassName, string.Format("{0}IsSuitableSpecification.{1}", especification, "cs"));
            PathOutputBase.MakeDirectory(pathBase, "Specification", tableInfo.ClassName);

            return pathOutput;
        }

        public static string PathOutputDomainEntityWarningSpecification(TableInfo tableInfo, Context configContext, string especification)
        {
            var pathOutput = string.Empty;

            var pathBase = PathOutputBase.PathBase(configContext.OutputClassDomain, configContext.UsePathProjects);
            pathOutput = Path.Combine(pathBase, "Specification", tableInfo.ClassName, string.Format("{0}WarningSpecification.{1}", especification, "cs"));
            PathOutputBase.MakeDirectory(pathBase, "Specification", tableInfo.ClassName);

            return pathOutput;
        }

        public static string PathOutputDomainEntitysValidationWarningSpecification(TableInfo tableInfo, Context configContext)
        {
            var pathOutput = string.Empty;

            var pathBase = PathOutputBase.PathBase(configContext.OutputClassDomain, configContext.UsePathProjects);
            pathOutput = Path.Combine(pathBase, "Validations", tableInfo.ClassName, string.Format("{0}IsSuitableWarning.{1}", tableInfo.ClassName, "cs"));
            PathOutputBase.MakeDirectory(pathBase, "Validations", tableInfo.ClassName);


            return pathOutput;
        }

        public static string PathOutputDomainEntitysValidationWarningSpecificationOld(TableInfo tableInfo, Context configContext)
        {
            var pathOutput = string.Empty;

            var pathBase = PathOutputBase.PathBase(configContext.OutputClassDomain, configContext.UsePathProjects);
            pathOutput = Path.Combine(pathBase, "Validations", tableInfo.ClassName, string.Format("{0}AptoParaCadastroWarning.{1}", tableInfo.ClassName, "cs"));
            PathOutputBase.MakeDirectory(pathBase, "Validations", tableInfo.ClassName);


            return pathOutput;
        }

        public static string PathOutputDomainEntitysValidatorSpecificationRepository(TableInfo tableInfo, Context configContext)
        {
            var pathOutput = string.Empty;

            var pathBase = PathOutputBase.PathBase(configContext.OutputClassDomain, configContext.UsePathProjects);
            pathOutput = Path.Combine(pathBase, "Validations", tableInfo.ClassName, string.Format("{0}IsSuitableValidation.{1}", tableInfo.ClassName, "cs"));
            PathOutputBase.MakeDirectory(pathBase, "Validations", tableInfo.ClassName);


            return pathOutput;
        }

        public static string PathOutputDomainEntitysValidatorSpecificationRepositoryOld(TableInfo tableInfo, Context configContext)
        {
            var pathOutput = string.Empty;

            var pathBase = PathOutputBase.PathBase(configContext.OutputClassDomain, configContext.UsePathProjects);
            pathOutput = Path.Combine(pathBase, "Validations", tableInfo.ClassName, string.Format("{0}AptoParaCadastroValidation.{1}", tableInfo.ClassName, "cs"));
            PathOutputBase.MakeDirectory(pathBase, "Validations", tableInfo.ClassName);


            return pathOutput;
        }

        public static string PathOutputDomainEntitysServiceBase(TableInfo tableInfo, Context configContext)
        {
            var pathOutput = string.Empty;

            var pathBase = PathOutputBase.PathBase(configContext.OutputClassDomain, configContext.UsePathProjects);
            pathOutput = Path.Combine(pathBase, "Services", tableInfo.ClassName, string.Format("{0}ServiceBase.{1}", tableInfo.ClassName, "cs"));
            PathOutputBase.MakeDirectory(pathBase, "Services", tableInfo.ClassName);


            return pathOutput;
        }

        public static string PathOutputInfraEntitysRepository(TableInfo tableInfo, Context configContext)
        {
            var pathOutput = string.Empty;
            var pathBase = PathOutputBase.PathBase(configContext.OutputClassInfra, configContext.UsePathProjects);
            pathOutput = Path.Combine(pathBase, "Repository", tableInfo.ClassName, string.Format("{0}Repository.{1}", tableInfo.ClassName, "cs"));
            PathOutputBase.MakeDirectory(pathBase, "Repository", tableInfo.ClassName);
            return pathOutput;
        }

        public static string PathOutputInfraFilterBasicExtension(TableInfo tableInfo, Context configContext)
        {
            var pathOutput = string.Empty;

            var pathBase = PathOutputBase.PathBase(configContext.OutputClassInfra, configContext.UsePathProjects);
            pathOutput = Path.Combine(pathBase, "Repository", tableInfo.ClassName, string.Format("{0}FilterBasicExtension.{1}", tableInfo.ClassName, "cs"));
            PathOutputBase.MakeDirectory(pathBase, "Repository", tableInfo.ClassName);


            return pathOutput;
        }

        public static string PathOutputInfraFilterCustomExtension(TableInfo tableInfo, Context configContext)
        {
            var pathOutput = string.Empty;

            var pathBase = PathOutputBase.PathBase(configContext.OutputClassInfra, configContext.UsePathProjects);
            pathOutput = Path.Combine(pathBase, "Repository", tableInfo.ClassName, string.Format("{0}FilterCustomExtension.{1}", tableInfo.ClassName, "cs"));
            PathOutputBase.MakeDirectory(pathBase, "Repository", tableInfo.ClassName);


            return pathOutput;
        }

        public static string PathOutputInfraOrderByDomainExtension(TableInfo tableInfo, Context configContext)
        {
            var pathOutput = string.Empty;

            var pathBase = PathOutputBase.PathBase(configContext.OutputClassInfra, configContext.UsePathProjects);
            pathOutput = Path.Combine(pathBase, "Repository", tableInfo.ClassName, string.Format("{0}OrderByCustomExtension.{1}", tableInfo.ClassName, "cs"));
            PathOutputBase.MakeDirectory(pathBase, "Repository", tableInfo.ClassName);


            return pathOutput;
        }



        public static string PathOutputDomainIEntitysRepository(TableInfo tableInfo, Context configContext)
        {
            var pathOutput = string.Empty;
            var pathBase = PathOutputBase.PathBase(configContext.OutputClassDomain, configContext.UsePathProjects);
            pathOutput = Path.Combine(pathBase, "Interfaces", "Repository", tableInfo.ClassName, string.Format("I{0}Repository.{1}", tableInfo.ClassName, "cs"));
            PathOutputBase.MakeDirectory(pathBase, "Interfaces", "Repository", tableInfo.ClassName);


            return pathOutput;
        }

        public static string PathOutputDomainIEntitysService(TableInfo tableInfo, Context configContext)
        {
            var pathOutput = string.Empty;

            var pathBase = PathOutputBase.PathBase(configContext.OutputClassDomain, configContext.UsePathProjects);
            pathOutput = Path.Combine(pathBase, "Interfaces", "Services", tableInfo.ClassName, string.Format("I{0}Service.{1}", tableInfo.ClassName, "cs"));
            PathOutputBase.MakeDirectory(pathBase, "Interfaces", "Services", tableInfo.ClassName);


            return pathOutput;
        }

      

        public static string PathOutputAngularRoute(TableInfo tableInfo, Context configContext)
        {
            var pathOutput = string.Empty;
            var className = tableInfo.ClassName.ToLower();

            var pathBase = PathOutputBase.PathBase(configContext.OutputAngular, configContext.UsePathProjects);
            pathOutput = Path.Combine(pathBase, "js", "config", "route.config.js");
            PathOutputBase.MakeDirectory(pathBase, "js", "config");

            return pathOutput;
        }

        public static string PathOutputAngularAccountService(TableInfo tableInfo, Context configContext)
        {
            var pathOutput = string.Empty;
            var className = tableInfo.ClassName.ToLower();

            var pathBase = PathOutputBase.PathBase(configContext.OutputAngular, configContext.UsePathProjects);
            pathOutput = Path.Combine(pathBase, "js", "services", "account.service.js");
            PathOutputBase.MakeDirectory(pathBase, "js", "services");

            return pathOutput;
        }

        public static string PathOutputAngularController(TableInfo tableInfo, Context configContext)
        {
            var pathOutput = string.Empty;
            var className = tableInfo.ClassName.ToLower();

            var pathBase = PathOutputBase.PathBase(configContext.OutputAngular, configContext.UsePathProjects);
            pathOutput = Path.Combine(pathBase, "js", "controllers", tableInfo.ClassName, string.Format("{0}.{1}", className, "controller.js"));
            PathOutputBase.MakeDirectory(pathBase, "js", "controllers", tableInfo.ClassName);

            return pathOutput;
        }
        public static string PathOutputAngularControllerCustom(TableInfo tableInfo, Context configContext)
        {
            var pathOutput = string.Empty;
            var className = tableInfo.ClassName.ToLower();

            var pathBase = PathOutputBase.PathBase(configContext.OutputAngular, configContext.UsePathProjects);
            pathOutput = Path.Combine(pathBase, "js", "controllers", tableInfo.ClassName, string.Format("{0}.{1}", className, "custom.controller.js"));
            PathOutputBase.MakeDirectory(pathBase, "js", "controllers", tableInfo.ClassName);

            return pathOutput;
        }

        public static string PathOutputAngularView(TableInfo tableInfo, Context configContext)
        {
            var pathOutput = string.Empty;
            var className = tableInfo.ClassName.ToLower();

            var pathBase = PathOutputBase.PathBase(configContext.OutputAngular, configContext.UsePathProjects);
            pathOutput = Path.Combine(pathBase, "view", tableInfo.ClassName, string.Format("{0}.{1}", "index", "html"));
            PathOutputBase.MakeDirectory(pathBase, "view", tableInfo.ClassName);

            return pathOutput;
        }


        public static string PathOutputAngularViewGrid(TableInfo tableInfo, Context configContext)
        {
            var pathOutput = string.Empty;
            var className = tableInfo.ClassName.ToLower();

            var pathBase = PathOutputBase.PathBase(configContext.OutputAngular, configContext.UsePathProjects);
            pathOutput = Path.Combine(pathBase, "view", tableInfo.ClassName, string.Format("{0}.{1}", "grid", "html"));
            PathOutputBase.MakeDirectory(pathBase, "view", tableInfo.ClassName);

            return pathOutput;
        }

        public static string PathOutputAngularViewFilters(TableInfo tableInfo, Context configContext)
        {
            var pathOutput = string.Empty;
            var className = tableInfo.ClassName.ToLower();

            var pathBase = PathOutputBase.PathBase(configContext.OutputAngular, configContext.UsePathProjects);
            pathOutput = Path.Combine(pathBase, "view", tableInfo.ClassName, string.Format("{0}.{1}", "filters", "html"));
            PathOutputBase.MakeDirectory(pathBase, "view", tableInfo.ClassName);

            return pathOutput;
        }

        public static string PathOutputAngularLabelConstants(TableInfo tableInfo, Context configContext)
        {
            var pathOutput = string.Empty;
            var className = tableInfo.ClassName.ToLower();

            var pathBase = PathOutputBase.PathBase(configContext.OutputAngular, configContext.UsePathProjects);
            pathOutput = Path.Combine(pathBase, "js", "constants", string.Format("{0}.constants.{1}", tableInfo.ClassName.ToLower(), "js"));
            PathOutputBase.MakeDirectory(pathBase, "js", "constants");

            return pathOutput;
        }

        public static string PathOutputAngularModalCreate(TableInfo tableInfo, Context configContext)
        {
            var pathOutput = string.Empty;
            var className = tableInfo.ClassName.ToLower();

            var pathBase = PathOutputBase.PathBase(configContext.OutputAngular, configContext.UsePathProjects);
            pathOutput = Path.Combine(pathBase, "view", tableInfo.ClassName, string.Format("{0}.{1}", "modalCreate", "html"));
            PathOutputBase.MakeDirectory(pathBase, "view", tableInfo.ClassName);

            return pathOutput;
        }

        public static string PathOutputAngularModalEdit(TableInfo tableInfo, Context configContext)
        {
            var pathOutput = string.Empty;
            var className = tableInfo.ClassName.ToLower();

            var pathBase = PathOutputBase.PathBase(configContext.OutputAngular, configContext.UsePathProjects);
            pathOutput = Path.Combine(pathBase, "view", tableInfo.ClassName, string.Format("{0}.{1}", "modalEdit", "html"));
            PathOutputBase.MakeDirectory(pathBase, "view", tableInfo.ClassName);

            return pathOutput;
        }

        public static string PathOutputAngularFormCreate(TableInfo tableInfo, Context configContext)
        {
            var pathOutput = string.Empty;
            var className = tableInfo.ClassName.ToLower();

            var pathBase = PathOutputBase.PathBase(configContext.OutputAngular, configContext.UsePathProjects);
            pathOutput = Path.Combine(pathBase, "view", tableInfo.ClassName, string.Format("{0}.{1}", "formCreate", "html"));
            PathOutputBase.MakeDirectory(pathBase, "view", tableInfo.ClassName);

            return pathOutput;
        }

        public static string PathOutputAngularFormEdit(TableInfo tableInfo, Context configContext)
        {
            var pathOutput = string.Empty;
            var className = tableInfo.ClassName.ToLower();

            var pathBase = PathOutputBase.PathBase(configContext.OutputAngular, configContext.UsePathProjects);
            pathOutput = Path.Combine(pathBase, "view", tableInfo.ClassName, string.Format("{0}.{1}", "formEdit", "html"));
            PathOutputBase.MakeDirectory(pathBase, "view", tableInfo.ClassName);

            return pathOutput;
        }

        public static string PathOutputAngularInitModules(TableInfo tableInfo, Context configContext)
        {
            var pathOutput = string.Empty;
            var className = tableInfo.ClassName.ToLower();

            var pathBase = PathOutputBase.PathBase(configContext.OutputAngular, configContext.UsePathProjects);
            pathOutput = Path.Combine(pathBase, "js", "init", string.Format("{0}.{1}", "modules", "js"));
            PathOutputBase.MakeDirectory(pathBase, "js");

            return pathOutput;
        }

        public static string PathOutputAngularConstants(TableInfo tableInfo, Context configContext)
        {
            var pathOutput = string.Empty;
            var className = tableInfo.ClassName.ToLower();

            var pathBase = PathOutputBase.PathBase(configContext.OutputAngular, configContext.UsePathProjects);
            pathOutput = Path.Combine(pathBase, "js", "constants", string.Format("{0}.{1}", "constants", "js"));
            PathOutputBase.MakeDirectory(pathBase, "js");

            return pathOutput;
        }

        public static string PathOutputAngularbreadcrumbConstants(TableInfo tableInfo, Context configContext)
        {
            var pathOutput = string.Empty;
            var className = tableInfo.ClassName.ToLower();

            var pathBase = PathOutputBase.PathBase(configContext.OutputAngular, configContext.UsePathProjects);
            pathOutput = Path.Combine(pathBase, "js", "constants", string.Format("{0}.{1}", "breadcrumb.constants", "js"));
            PathOutputBase.MakeDirectory(pathBase, "js");

            return pathOutput;
        }

        public static string PathOutputAngularIndex(TableInfo tableInfo, Context configContext)
        {
            var pathOutput = string.Empty;
            var className = tableInfo.ClassName.ToLower();

            var pathBase = PathOutputBase.PathBase(configContext.OutputAngular, configContext.UsePathProjects);
            pathOutput = Path.Combine(pathBase, string.Format("{0}.{1}", "Index", "html"));

            return pathOutput;
        }

        public static string PathOutputAngularLayout(TableInfo tableInfo, Context configContext)
        {
            var pathOutput = string.Empty;
            var className = tableInfo.ClassName.ToLower();

            var pathBase = PathOutputBase.PathBase(configContext.OutputAngular, configContext.UsePathProjects);
            pathOutput = Path.Combine(pathBase, "Template", string.Format("{0}.{1}", "layout", "html"));
            PathOutputBase.MakeDirectory("Template", pathBase);
            return pathOutput;
        }


        public static string PathOutputAngularHomeCustomController(TableInfo tableInfo, Context configContext)
        {
            var pathOutput = string.Empty;
            var className = tableInfo.ClassName.ToLower();

            var pathBase = PathOutputBase.PathBase(configContext.OutputAngular, configContext.UsePathProjects);
            pathOutput = Path.Combine(pathBase, "js", "controllers", "Home", string.Format("{0}.{1}", "home.custom.controller", "js"));
            PathOutputBase.MakeDirectory(pathBase, "js", "controllers", "Home");
            return pathOutput;
        }

        public static string PathOutputAngularLoginCustomController(TableInfo tableInfo, Context configContext)
        {
            var pathOutput = string.Empty;
            var className = tableInfo.ClassName.ToLower();

            var pathBase = PathOutputBase.PathBase(configContext.OutputAngular, configContext.UsePathProjects);
            pathOutput = Path.Combine(pathBase, "js", "controllers", "Login", string.Format("{0}.{1}", "login.custom.controller", "js"));
            PathOutputBase.MakeDirectory(pathBase, "js", "controllers", "Login");
            return pathOutput;
        }


        public static string PathOutputAngularHomeView(TableInfo tableInfo, Context configContext)
        {
            var pathOutput = string.Empty;
            var className = tableInfo.ClassName.ToLower();

            var pathBase = PathOutputBase.PathBase(configContext.OutputAngular, configContext.UsePathProjects);
            pathOutput = Path.Combine(pathBase, "view", "Home", string.Format("{0}.{1}", "index", "html"));
            PathOutputBase.MakeDirectory(pathBase, "view", "Home");
            return pathOutput;
        }

        public static string PathOutputAngularLoginView(TableInfo tableInfo, Context configContext)
        {
            var pathOutput = string.Empty;
            var className = tableInfo.ClassName.ToLower();

            var pathBase = PathOutputBase.PathBase(configContext.OutputAngular, configContext.UsePathProjects);
            pathOutput = Path.Combine(pathBase, "view", "Login", string.Format("{0}.{1}", "index", "html"));
            PathOutputBase.MakeDirectory(pathBase, "view", "Login");
            return pathOutput;
        }

        public static string PathOutputAngularRouteConfigCustom(TableInfo tableInfo, Context configContext)
        {
            var pathOutput = string.Empty;
            var className = tableInfo.ClassName.ToLower();

            var pathBase = PathOutputBase.PathBase(configContext.OutputAngular, configContext.UsePathProjects);
            pathOutput = Path.Combine(pathBase, "js", "config", string.Format("{0}.{1}", "route.custom.config", "js"));
            PathOutputBase.MakeDirectory(pathBase, "js", "config");
            return pathOutput;
        }

        public static string PathOutputAngularValueConfig(TableInfo tableInfo, Context configContext)
        {
            var pathOutput = string.Empty;
            var className = tableInfo.ClassName.ToLower();

            var pathBase = PathOutputBase.PathBase(configContext.OutputAngular, configContext.UsePathProjects);
            pathOutput = Path.Combine(pathBase, "js", "config", string.Format("{0}.{1}", "value.config", "js"));
            PathOutputBase.MakeDirectory(pathBase, "js", "config");
            return pathOutput;
        }

        public static string PathOutputAngularTokenConstants(TableInfo tableInfo, Context configContext)
        {
            var pathOutput = string.Empty;
            var className = tableInfo.ClassName.ToLower();

            var pathBase = PathOutputBase.PathBase(configContext.OutputAngular, configContext.UsePathProjects);
            pathOutput = Path.Combine(pathBase, "js", "constants", string.Format("{0}.{1}", "token.constants", "js"));
            PathOutputBase.MakeDirectory(pathBase, "js", "constants");
            return pathOutput;
        }

        public static string PathOutputPackage(TableInfo tableInfo, Context configContext)
        {
            var pathOutput = string.Empty;
            var className = tableInfo.ClassName.ToLower();

            var pathBase = PathOutputBase.PathBase(configContext.OutputAngular, configContext.UsePathProjects);
            pathOutput = Path.Combine(pathBase, string.Format("{0}.{1}", "package", "json"));
            return pathOutput;
        }

        public static string PathOutputAngularBreadCrumbSharedView(TableInfo tableInfo, Context configContext)
        {
            var pathOutput = string.Empty;
            var className = tableInfo.ClassName.ToLower();

            var pathBase = PathOutputBase.PathBase(configContext.OutputAngular, configContext.UsePathProjects);
            pathOutput = Path.Combine(pathBase, "view", "Shared", string.Format("{0}.{1}", "_breadcrumb", "html"));
            PathOutputBase.MakeDirectory(pathBase, "view", "Shared");
            return pathOutput;
        }

        public static string PathOutputAngularExclusaoSharedView(TableInfo tableInfo, Context configContext)
        {
            var pathOutput = string.Empty;
            var className = tableInfo.ClassName.ToLower();

            var pathBase = PathOutputBase.PathBase(configContext.OutputAngular, configContext.UsePathProjects);
            pathOutput = Path.Combine(pathBase, "view", "Shared", string.Format("{0}.{1}", "_exclusao.modal", "html"));
            PathOutputBase.MakeDirectory(pathBase, "view", "Shared");
            return pathOutput;
        }

        public static string PathOutputAngularExecuteSharedView(TableInfo tableInfo, Context configContext)
        {
            var pathOutput = string.Empty;
            var className = tableInfo.ClassName.ToLower();

            var pathBase = PathOutputBase.PathBase(configContext.OutputAngular, configContext.UsePathProjects);
            pathOutput = Path.Combine(pathBase, "view", "Shared", string.Format("{0}.{1}", "_execute.modal", "html"));
            PathOutputBase.MakeDirectory(pathBase, "view", "Shared");
            return pathOutput;
        }

        public static string PathOutputAngularHeaderSharedView(TableInfo tableInfo, Context configContext)
        {
            var pathOutput = string.Empty;
            var className = tableInfo.ClassName.ToLower();

            var pathBase = PathOutputBase.PathBase(configContext.OutputAngular, configContext.UsePathProjects);
            pathOutput = Path.Combine(pathBase, "view", "Shared", string.Format("{0}.{1}", "_header", "html"));
            PathOutputBase.MakeDirectory(pathBase, "view", "Shared");
            return pathOutput;
        }

        public static string PathOutputAngularMenuSharedView(TableInfo tableInfo, Context configContext)
        {
            var pathOutput = string.Empty;
            var className = tableInfo.ClassName.ToLower();

            var pathBase = PathOutputBase.PathBase(configContext.OutputAngular, configContext.UsePathProjects);
            pathOutput = Path.Combine(pathBase, "view", "Shared", string.Format("{0}.{1}", "_menu", "html"));
            PathOutputBase.MakeDirectory(pathBase, "view", "Shared");
            return pathOutput;
        }

        public static string PathOutputAngularUnauthorizedSharedView(TableInfo tableInfo, Context configContext)
        {
            var pathOutput = string.Empty;
            var className = tableInfo.ClassName.ToLower();

            var pathBase = PathOutputBase.PathBase(configContext.OutputAngular, configContext.UsePathProjects);
            pathOutput = Path.Combine(pathBase, "view", "Shared", string.Format("{0}.{1}", "_unauthorized", "html"));
            PathOutputBase.MakeDirectory(pathBase, "view", "Shared");
            return pathOutput;
        }

        public static string PathOutputAngularMainCustomController(TableInfo tableInfo, Context configContext)
        {
            var pathOutput = string.Empty;
            var className = tableInfo.ClassName.ToLower();

            var pathBase = PathOutputBase.PathBase(configContext.OutputAngular, configContext.UsePathProjects);
            pathOutput = Path.Combine(pathBase, "js", "controllers", "shared", string.Format("{0}.{1}", "main.custom.controller", "js"));
            PathOutputBase.MakeDirectory(pathBase, "js", "controllers", "shared");
            return pathOutput;
        }

        public static string PathOutputAngularApp(TableInfo tableInfo, Context configContext)
        {
            var pathOutput = string.Empty;
            var className = tableInfo.ClassName.ToLower();

            var pathBase = PathOutputBase.PathBase(configContext.OutputAngular, configContext.UsePathProjects);
            pathOutput = Path.Combine(pathBase, "js", "init", string.Format("{0}.{1}", "app", "js"));
            PathOutputBase.MakeDirectory(pathBase, "js", "init");

            return pathOutput;
        }

        public static string PathOutputAngularEditInPage(TableInfo tableInfo, Context configContext)
        {
            var pathOutput = string.Empty;
            var className = tableInfo.ClassName.ToLower();

            var pathBase = PathOutputBase.PathBase(configContext.OutputAngular, configContext.UsePathProjects);
            pathOutput = Path.Combine(pathBase, "view", tableInfo.ClassName, string.Format("{0}.{1}", "edit", "html"));
            PathOutputBase.MakeDirectory(pathBase, "view", tableInfo.ClassName);

            return pathOutput;
        }

        public static string PathOutputAngularCreateInPage(TableInfo tableInfo, Context configContext)
        {
            var pathOutput = string.Empty;
            var className = tableInfo.ClassName.ToLower();

            var pathBase = PathOutputBase.PathBase(configContext.OutputAngular, configContext.UsePathProjects);
            pathOutput = Path.Combine(pathBase, "view", tableInfo.ClassName, string.Format("{0}.{1}", "create", "html"));
            PathOutputBase.MakeDirectory(pathBase, "view", tableInfo.ClassName);

            return pathOutput;
        }

        public static string PathOutputAngularDetails(TableInfo tableInfo, Context configContext)
        {
            var pathOutput = string.Empty;
            var className = tableInfo.ClassName.ToLower();

            var pathBase = PathOutputBase.PathBase(configContext.OutputAngular, configContext.UsePathProjects);
            pathOutput = Path.Combine(pathBase, "view", tableInfo.ClassName, string.Format("{0}.{1}", "details", "html"));
            PathOutputBase.MakeDirectory(pathBase, "view", tableInfo.ClassName);

            return pathOutput;
        }

        public static string PathOutputAngularDetailsField(TableInfo tableInfo, Context configContext)
        {
            var pathOutput = string.Empty;
            var className = tableInfo.ClassName.ToLower();

            var pathBase = PathOutputBase.PathBase(configContext.OutputAngular, configContext.UsePathProjects);
            pathOutput = Path.Combine(pathBase, "view", tableInfo.ClassName, string.Format("{0}.{1}", "detailsFields", "html"));
            PathOutputBase.MakeDirectory(pathBase, "view", tableInfo.ClassName);

            return pathOutput;
        }

        public static string PathOutputApp(TableInfo tableInfo, Context configContext)
        {
            if (tableInfo.InheritQuery)
                return PathOutputAppInherit(tableInfo, configContext);

            var pathOutput = string.Empty;
            var pathBase = PathOutputBase.PathBase(configContext.OutputClassApp, configContext.UsePathProjects);
            pathOutput = Path.Combine(pathBase, "Application", string.Format("{0}App.{1}", tableInfo.ClassName, "cs"));
            PathOutputBase.MakeDirectory("Application", pathBase);
            return pathOutput;
        }

        public static string PathOutputAppInherit(TableInfo tableInfo, Context configContext)
        {
            var pathOutput = string.Empty;
            var pathBase = PathOutputBase.PathBase(configContext.OutputClassApp, configContext.UsePathProjects);
            pathOutput = Path.Combine(pathBase, "Application", tableInfo.BoundedContext, string.Format("{0}App.{1}", tableInfo.InheritClassName, "cs"));
            PathOutputBase.MakeDirectory(pathBase, "Application", tableInfo.BoundedContext);
            return pathOutput;
        }

       

        public static string PathOutputContainer(Context configContext)
        {
            var pathOutput = string.Empty;

            var pathBase = PathOutputBase.PathBase(configContext.OutputClassApp, configContext.UsePathProjects);

            pathOutput = Path.Combine(pathBase, "Config", string.Format("ConfigContainer{0}.{1}", configContext.Module, "cs"));
            PathOutputBase.MakeDirectory("Config", pathBase);

            return pathOutput;
        }

        public static string PathOutputContainerWeb(Context configContext)
        {
            var pathOutput = string.Empty;

            var pathBase = PathOutputBase.PathBase(configContext.OutputClassApi, configContext.UsePathProjects);

            pathOutput = Path.Combine(pathBase, "Config", string.Format("ConfigContainerWeb{0}.{1}", configContext.Module, "cs"));
            PathOutputBase.MakeDirectory("Config", pathBase);

            return pathOutput;
        }

        public static string PathOutputContainerApi(Context configContext)
        {
            var pathOutput = string.Empty;

            var pathBase = PathOutputBase.PathBase(configContext.OutputClassApi, configContext.UsePathProjects);

            pathOutput = Path.Combine(pathBase, "Config", string.Format("ConfigContainer{0}.{1}", configContext.ContextName, "cs"));
            PathOutputBase.MakeDirectory("Config", pathBase);

            return pathOutput;
        }

        public static string PathOutputSsoConfig(Context configContext)
        {
            var pathOutput = string.Empty;

            var pathBase = PathOutputBase.PathBase(configContext.OutputClassSso, configContext.UsePathProjects);

            pathOutput = Path.Combine(pathBase, "Config.cs");

            return pathOutput;
        }

        public static string PathOutputCrossCustingAuthConfig(Context configContext)
        {
            var pathOutput = string.Empty;

            var pathBase = PathOutputBase.PathBase(configContext.OutputClassCrossCustingAuth, configContext.UsePathProjects);

            pathOutput = Path.Combine(pathBase, "ProfileCustom.cs");

            return pathOutput;
        }

       

        public static string PathOutputAutoMapper(Context configContext)
        {
            var pathOutput = string.Empty;

            var pathBase = PathOutputBase.PathBase(configContext.OutputClassApp, configContext.UsePathProjects);

            pathOutput = Path.Combine(pathBase, "Config", string.Format("AutoMapperConfig{0}.{1}", configContext.ContextName, "cs"));
            PathOutputBase.MakeDirectory("Config", pathBase);

            return pathOutput;
        }

        public static string PathOutputAutoMapperProfile(Context configContext, TableInfo tableInfo)
        {
            if (tableInfo.InheritQuery)
                return PathOutputAutoMapperProfileInherit(configContext, tableInfo);

            var pathOutput = string.Empty;
            var pathBase = PathOutputBase.PathBase(configContext.OutputClassApp, configContext.UsePathProjects);
            pathOutput = Path.Combine(pathBase, "Config", string.Format("DominioToDtoProfile{0}.{1}", configContext.ContextName, "cs"));
            PathOutputBase.MakeDirectory("Config", pathBase);
            return pathOutput;
        }

        public static string PathOutputAutoMapperProfileCustom(Context configContext, TableInfo tableInfo)
        {
            var pathOutput = string.Empty;
            var pathBase = PathOutputBase.PathBase(configContext.OutputClassApp, configContext.UsePathProjects);
            pathOutput = Path.Combine(pathBase, "Config", string.Format("DominioToDtoProfile{0}Custom.{1}", configContext.ContextName, "cs"));
            PathOutputBase.MakeDirectory("Config", pathBase);
            return pathOutput;
        }

        public static string PathOutputApplicationIEntityApplicationService(TableInfo tableInfo, Context configContext)
        {
            var pathOutput = string.Empty;

            var pathBase = PathOutputBase.PathBase(configContext.OutputClassApp, configContext.UsePathProjects);
            pathOutput = Path.Combine(pathBase, "Interfaces", tableInfo.ClassName, string.Format("I{0}ApplicationService.{1}", tableInfo.ClassName, "cs"));
            PathOutputBase.MakeDirectory(pathBase, "Interfaces", tableInfo.ClassName);


            return pathOutput;
        }

        public static string PathOutputApplicationIEntityApplicationServiceBase(TableInfo tableInfo, Context configContext)
        {
            var pathOutput = string.Empty;

            var pathBase = PathOutputBase.PathBase(configContext.OutputClassApp, configContext.UsePathProjects);
            pathOutput = Path.Combine(pathBase, "Interfaces", tableInfo.ClassName, string.Format("I{0}ApplicationServiceBase.{1}", tableInfo.ClassName, "cs"));
            PathOutputBase.MakeDirectory(pathBase, "Interfaces", tableInfo.ClassName);


            return pathOutput;
        }

      
        public static string PathOutputApplicatioEntityApplicationServiceBase(TableInfo tableInfo, Context configContext)
        {
            var pathOutput = string.Empty;

            var pathBase = PathOutputBase.PathBase(configContext.OutputClassApp, configContext.UsePathProjects);
            pathOutput = Path.Combine(pathBase, "App", tableInfo.ClassName, string.Format("{0}ApplicationServiceBase.{1}", tableInfo.ClassName, "cs"));
            PathOutputBase.MakeDirectory(pathBase, "App", tableInfo.ClassName);


            return pathOutput;
        }

        public static string PathOutputAutoMapperProfileInherit(Context configContext, TableInfo tableInfo)
        {
            var pathOutput = string.Empty;
            var pathBase = PathOutputBase.PathBase(configContext.OutputClassApp, configContext.UsePathProjects);
            pathOutput = Path.Combine(pathBase, "Config", string.Format("DominioToDtoProfile{0}.{1}", tableInfo.BoundedContext, "cs"));
            PathOutputBase.MakeDirectory("Config", pathBase);
            return pathOutput;
        }

        public static string PathOutputWebApiConfig(Context configContext)
        {
            var pathOutput = string.Empty;

            var pathBase = PathOutputBase.PathBase(configContext.OutputClassApi, configContext.UsePathProjects);

            pathOutput = Path.Combine(pathBase, "App_Start", string.Format("WebApiConfig.cs"));
            PathOutputBase.MakeDirectory("App_Start", pathBase);


            return pathOutput;
        }

        public static string PathOutputWebApiStart(Context configContext)
        {
            var pathOutput = string.Empty;
            var pathBase = PathOutputBase.PathBase(configContext.OutputClassApi, configContext.UsePathProjects);
            pathOutput = Path.Combine(pathBase, string.Format("Startup.cs"));
            return pathOutput;
        }


        public static string PathOutputApiCurrentUser(Context configContext, TableInfo tableInfo)
        {
            var pathOutput = string.Empty;
            var pathBase = PathOutputBase.PathBase(configContext.OutputClassApi, configContext.UsePathProjects);
            pathOutput = Path.Combine(pathBase, "Controllers", string.Format("CurrentUserController.{0}", "cs"));
            PathOutputBase.MakeDirectory("Controllers", pathBase);
            return pathOutput;
        }

        public static string PathOutputApiUpload(Context configContext, TableInfo tableInfo)
        {
            var pathOutput = string.Empty;
            var pathBase = PathOutputBase.PathBase(configContext.OutputClassApi, configContext.UsePathProjects);
            pathOutput = Path.Combine(pathBase, "Controllers", string.Format("UploadController.{0}", "cs"));
            PathOutputBase.MakeDirectory("Controllers", pathBase);
            return pathOutput;
        }

        public static string PathOutputApiDownload(Context configContext, TableInfo tableInfo)
        {
            var pathOutput = string.Empty;
            var pathBase = PathOutputBase.PathBase(configContext.OutputClassApi, configContext.UsePathProjects);
            pathOutput = Path.Combine(pathBase, "Controllers", string.Format("DownloadController.{0}", "cs"));
            PathOutputBase.MakeDirectory("Controllers", pathBase);
            return pathOutput;
        }

        public static string PathOutputApiHeath(Context configContext, TableInfo tableInfo)
        {
            var pathOutput = string.Empty;
            var pathBase = PathOutputBase.PathBase(configContext.OutputClassApi, configContext.UsePathProjects);
            pathOutput = Path.Combine(pathBase, "Controllers", string.Format("HealthController.{0}", "cs"));
            PathOutputBase.MakeDirectory("Controllers", pathBase);
            return pathOutput;
        }

        public static string PathOutputAppSettings(Context configContext)
        {
            var pathOutput = string.Empty;

            var pathBase = PathOutputBase.PathBase(configContext.OutputClassApi, configContext.UsePathProjects);
            pathOutput = Path.Combine(pathBase, string.Format("appsettings.json"));
            return pathOutput;
        }

        public static string PathOutputProjectApi(Context configContext)
        {
            var pathOutput = string.Empty;

            var pathBase = PathOutputBase.PathBase(configContext.OutputClassApi, configContext.UsePathProjects);
            pathOutput = Path.Combine(pathBase, string.Format("project.json"));
            return pathOutput;
        }

        public static string PathOutputProjectApp(Context configContext)
        {
            var pathOutput = string.Empty;

            var pathBase = PathOutputBase.PathBase(configContext.OutputClassApp, configContext.UsePathProjects);
            pathOutput = Path.Combine(pathBase, string.Format("project.json"));
            return pathOutput;
        }

        public static string PathOutputProjectDto(Context configContext)
        {
            var pathOutput = string.Empty;

            var pathBase = PathOutputBase.PathBase(configContext.OutputClassDto, configContext.UsePathProjects);
            pathOutput = Path.Combine(pathBase, string.Format("project.json"));
            return pathOutput;
        }

        public static string PathOutputProjectDomain(Context configContext)
        {
            var pathOutput = string.Empty;

            var pathBase = PathOutputBase.PathBase(configContext.OutputClassDomain, configContext.UsePathProjects);
            pathOutput = Path.Combine(pathBase, string.Format("project.json"));
            return pathOutput;
        }

        public static string PathOutputProjectFilter(Context configContext)
        {
            var pathOutput = string.Empty;

            var pathBase = PathOutputBase.PathBase(configContext.OutputClassFilter, configContext.UsePathProjects);
            pathOutput = Path.Combine(pathBase, string.Format("project.json"));
            return pathOutput;
        }

        public static string PathOutputProjectSummary(Context configContext)
        {
            var pathOutput = string.Empty;

            var pathBase = PathOutputBase.PathBase(configContext.OutputClassSummary, configContext.UsePathProjects);
            pathOutput = Path.Combine(pathBase, string.Format("project.json"));
            return pathOutput;
        }

        public static string PathOutputProjectInfra(Context configContext)
        {
            var pathOutput = string.Empty;

            var pathBase = PathOutputBase.PathBase(configContext.OutputClassInfra, configContext.UsePathProjects);
            pathOutput = Path.Combine(pathBase, string.Format("project.json"));
            return pathOutput;
        }

        public static string PathOutputConfigDomain(Context configContext)
        {
            var pathOutput = string.Empty;


            var pathBase = PathOutputBase.PathBase(configContext.OutputClassDomain, configContext.UsePathProjects);

            pathOutput = Path.Combine(pathBase, "Base", string.Format("ConfigDomain{0}.{1}", configContext.Module, "cs"));
            PathOutputBase.MakeDirectory("Base", pathBase);

            return pathOutput;
        }

        public static string PathOutputHelperValidationAuth(Context configContext)
        {
            var pathOutput = string.Empty;


            var pathBase = PathOutputBase.PathBase(configContext.OutputClassDomain, configContext.UsePathProjects);

            pathOutput = Path.Combine(pathBase, "Helpers", string.Format("HelperValidateAuth{0}.{1}", configContext.Module, "cs"));
            PathOutputBase.MakeDirectory("Helpers", pathBase);

            return pathOutput;
        }



        public static string PathOutputUri(TableInfo tableInfo, Context configContext)
        {
            if (tableInfo.InheritQuery)
                return PathOutputUriInherit(tableInfo, configContext);

            var pathOutput = string.Empty;
            var pathBase = PathOutputBase.PathBase(configContext.OutputClassUri, configContext.UsePathProjects);
            pathOutput = Path.Combine(pathBase, "Uri", configContext.Module, string.Format("{0}Uri.{1}", tableInfo.ClassName, "cs"));
            PathOutputBase.MakeDirectory(string.Format("Uri/{0}", configContext.Module), pathBase);
            return pathOutput;
        }

        public static string PathOutputUriInherit(TableInfo tableInfo, Context configContext)
        {
            var pathOutput = string.Empty;
            var pathBase = PathOutputBase.PathBase(configContext.OutputClassUri, configContext.UsePathProjects);
            pathOutput = Path.Combine(pathBase, "Uri", configContext.Module, string.Format("{0}Uri.{1}", tableInfo.InheritClassName, "cs"));
            PathOutputBase.MakeDirectory(string.Format("Uri/{0}", configContext.Module), pathBase);
            return pathOutput;
        }

        public static string PathOutputPreCompiledView(Context configContext)
        {
            var pathOutput = string.Empty;


            var pathBase = PathOutputBase.PathBase(configContext.OutputClassInfra, configContext.UsePathProjects);

            pathOutput = Path.Combine(pathBase, "Context", string.Format("MappingViewCache{0}Genereted.{1}", configContext.Module, "cs"));
            PathOutputBase.MakeDirectory("Context", pathBase);

            return pathOutput;
        }

        public static string PathOutputFilter(TableInfo tableInfo, Context configContext)
        {
            var pathOutput = string.Empty;
            var pathBase = PathOutputBase.PathBase(configContext.OutputClassFilter, configContext.UsePathProjects);
            pathOutput = Path.Combine(pathBase, "Filters", string.Format("{0}Filter.{1}", tableInfo.ClassName, "cs"));
            PathOutputBase.MakeDirectory("Filters", pathBase);
            return pathOutput;
        }

        public static string PathOutputFilterWithFolder(TableInfo tableInfo, Context configContext)
        {
            var pathOutput = string.Empty;
            var pathBase = PathOutputBase.PathBase(configContext.OutputClassFilter, configContext.UsePathProjects);
            pathOutput = Path.Combine(pathBase, "Filters", tableInfo.ClassName, string.Format("{0}FilterBase.{1}", tableInfo.ClassName, "cs"));
            PathOutputBase.MakeDirectory(pathBase, "Filters", tableInfo.ClassName);
            return pathOutput;
        }


        public static string PathOutputCustom(TableInfo tableInfo, Context configContext)
        {
            var pathOutput = string.Empty;
            var pathBase = PathOutputBase.PathBase(configContext.OutputClassCustom, configContext.UsePathProjects);
            pathOutput = Path.Combine(pathBase, tableInfo.ClassName, string.Format("{0}Custom.{1}", tableInfo.ClassName, "cs"));
            PathOutputBase.MakeDirectory(tableInfo.ClassName, pathBase);
            return pathOutput;
        }

        public static string PathOutputDto(TableInfo tableInfo, Context configContext)
        {
            if (tableInfo.InheritQuery)
                return PathOutputDtoInherit(tableInfo, configContext);

            var pathOutput = string.Empty;
            var pathBase = PathOutputBase.PathBase(configContext.OutputClassDto, configContext.UsePathProjects);
            pathOutput = Path.Combine(pathBase, "Dto", tableInfo.ClassName, string.Format("{0}Dto.{1}", tableInfo.ClassName, "cs"));
            PathOutputBase.MakeDirectory(pathBase, "Dto", tableInfo.ClassName);
            return pathOutput;
        }

        public static string PathOutputSummary(TableInfo tableInfo, Context configContext)
        {
            var pathOutput = string.Empty;
            var pathBase = PathOutputBase.PathBase(configContext.OutputClassSummary, configContext.UsePathProjects);
            var fileName = tableInfo.ClassName;
            pathOutput = Path.Combine(pathBase, "Summary", string.Format("{0}Summary.{1}", fileName, "cs"));
            PathOutputBase.MakeDirectory("Summary", pathBase);
            return pathOutput;
        }

        public static string PathOutputDtoInherit(TableInfo tableInfo, Context configContext)
        {
            var pathOutput = string.Empty;
            var pathBase = PathOutputBase.PathBase(configContext.OutputClassDto, configContext.UsePathProjects);
            pathOutput = Path.Combine(pathBase, "Dto", tableInfo.BoundedContext, tableInfo.ClassName, string.Format("{0}Dto.{1}", tableInfo.ClassName, "cs"));
            PathOutputBase.MakeDirectory(pathBase, "Dto", tableInfo.BoundedContext, tableInfo.ClassName);
            return pathOutput;
        }

      

        public static string PathOutputApi(TableInfo tableInfo, Context configContext)
        {
            if (tableInfo.InheritQuery)
                return PathOutputApiInherit(tableInfo, configContext);

            var pathOutput = string.Empty;
            var pathBase = PathOutputBase.PathBase(configContext.OutputClassApi, configContext.UsePathProjects);
            pathOutput = Path.Combine(pathBase, "Controllers", string.Format("{0}Controller.{1}", tableInfo.ClassName, "cs"));
            PathOutputBase.MakeDirectory("Controllers", pathBase);
            return pathOutput;
        }

        public static string PathOutputApiBase(TableInfo tableInfo, Context configContext)
        {
            if (tableInfo.InheritQuery)
                return PathOutputApiInherit(tableInfo, configContext);

            var pathOutput = string.Empty;
            var pathBase = PathOutputBase.PathBase(configContext.OutputClassApi, configContext.UsePathProjects);
            pathOutput = Path.Combine(pathBase, "Controllers", string.Format("{0}ControllerBase.{1}", tableInfo.ClassName, "cs"));
            PathOutputBase.MakeDirectory("Controllers", pathBase);
            return pathOutput;
        }

        public static string PathOutputApiTransaction(TableInfo tableInfo, Context configContext)
        {
            if (tableInfo.InheritQuery)
                return PathOutputApiInherit(tableInfo, configContext);

            var pathOutput = string.Empty;
            var pathBase = PathOutputBase.PathBase(configContext.OutputClassApi, configContext.UsePathProjects);
            pathOutput = Path.Combine(pathBase, "Controllers", string.Format("{0}TransactionController.{1}", tableInfo.ClassName, "cs"));
            PathOutputBase.MakeDirectory("Controllers", pathBase);
            return pathOutput;
        }

        public static string PathOutputApiMore(TableInfo tableInfo, Context configContext)
        {

            var pathOutput = string.Empty;
            var pathBase = PathOutputBase.PathBase(configContext.OutputClassApi, configContext.UsePathProjects);
            pathOutput = Path.Combine(pathBase, "Controllers", string.Format("{0}MoreController.{1}", tableInfo.ClassName, "cs"));
            PathOutputBase.MakeDirectory("Controllers", pathBase);
            return pathOutput;
        }

        public static string PathOutputApiMoreBase(TableInfo tableInfo, Context configContext)
        {

            var pathOutput = string.Empty;
            var pathBase = PathOutputBase.PathBase(configContext.OutputClassApi, configContext.UsePathProjects);
            pathOutput = Path.Combine(pathBase, "Controllers", string.Format("{0}MoreBaseController.{1}", tableInfo.ClassName, "cs"));
            PathOutputBase.MakeDirectory("Controllers", pathBase);
            return pathOutput;
        }

        public static string PathOutputApiInherit(TableInfo tableInfo, Context configContext)
        {
            var pathOutput = string.Empty;
            var pathBase = PathOutputBase.PathBase(configContext.OutputClassApi, configContext.UsePathProjects);
            pathOutput = Path.Combine(pathBase, "Controllers", tableInfo.BoundedContext, string.Format("{0}Controller.{1}", tableInfo.InheritClassName, "cs"));
            PathOutputBase.MakeDirectory(pathBase, "Controllers", tableInfo.BoundedContext);
            return pathOutput;
        }

        public static string PathOutputApplicationTest(TableInfo tableInfo, Context configContext)
        {
            var pathOutput = string.Empty;

            var pathBase = PathOutputBase.PathBase(configContext.OutputClassTestsApp, configContext.UsePathProjects);

            pathOutput = Path.Combine(pathBase, configContext.Module, string.Format("UnitTest{0}App.{1}", tableInfo.ClassName, "cs"));
            PathOutputBase.MakeDirectory(configContext.Module, pathBase);


            return pathOutput;
        }


        public static string PathOutputApplicationTestMoq(TableInfo tableInfo, Context configContext)
        {
            var pathOutput = string.Empty;

            var pathBase = PathOutputBase.PathBase(configContext.OutputClassTestsApp, configContext.UsePathProjects);

            pathOutput = Path.Combine(pathBase, "Moq", configContext.Module, string.Format("Helper{0}Moq.{1}", tableInfo.ClassName, "cs"));
            PathOutputBase.MakeDirectory(Path.Combine("Moq", configContext.Module), pathBase);


            return pathOutput;
        }

        public static string PathOutputApiTest(TableInfo tableInfo, Context configContext)
        {
            var pathOutput = string.Empty;

            var pathBase = PathOutputBase.PathBase(configContext.OutputClassTestsApi, configContext.UsePathProjects);

            pathOutput = Path.Combine(pathBase, configContext.Module, string.Format("UnitTest{0}Api.{1}", tableInfo.ClassName, "cs"));
            PathOutputBase.MakeDirectory(configContext.Module, pathBase);



            return pathOutput;
        }

      



    }
}
