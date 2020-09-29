using Common.Gen;
using System.Collections.Generic;

namespace Seed.Gen
{
    public class ConfigExternalResources
    {

        public ConfigExternalResources()
        {

        }

        private ExternalResource ConfigExternarResourcesTemplatesBackDDDCore20(bool replaceLocalFilesApplication)
        {

            return new ExternalResource
            {
                ReplaceLocalFilesApplication = true,
                ResouceRepositoryName = "template-gerador-back-core2.0-DDD",
                ResourceUrlRepository = "https://github.com/wilsonsantosnet/template-gerador-back-core2.0-DDD.git",
                ResourceLocalPathFolderExecuteCloning = @"C:\Projetos\Outros\Repositorios",
                ResourceLocalPathDestinationFolrderApplication = @"C:\Projetos\solution-base-core2.0-ddd-project-with-gerador-empty\Gerador.Gen\Templates\Back"
            };

        }

        private ExternalResource ConfigExternarResourcesTemplatesBackDDD(bool replaceLocalFilesApplication)
        {

            return new ExternalResource
            {
                ReplaceLocalFilesApplication = true,
                ResouceRepositoryName = "template-gerador-back-DDD",
                ResourceUrlRepository = "https://github.com/wilsonsantosnet/template-gerador-back-DDD.git",
                ResourceLocalPathFolderExecuteCloning = @"C:\Projetos\Outros\Repositorios",
                ResourceLocalPathDestinationFolrderApplication = @"C:\Projetos\solution-base-core2.0-ddd-project-with-gerador-empty\Gerador.Gen\Templates\Back"
            };

        }

        private ExternalResource ConfigExternarResourcesFrameworkCommon(bool replaceLocalFilesApplication)
        {

            return new ExternalResource
            {
                ReplaceLocalFilesApplication = true,
                OnlyFoldersContainsThisName = "Common",
                ResouceRepositoryName = "framework-core-common",
                ResourceUrlRepository = "https://github.com/wilsonsantosnet/framework-core-common.git",
                ResourceLocalPathFolderExecuteCloning = @"C:\Projetos\Outros\Repositorios",
                ResourceLocalPathDestinationFolrderApplication = @"C:\Projetos\solution-base-core2.0-ddd-project-with-gerador-empty\"
            };

        }

        private ExternalResource ConfigExternarResourcesFrameworkCommon20(bool replaceLocalFilesApplication)
        {

            return new ExternalResource
            {
                ReplaceLocalFilesApplication = true,
                OnlyFoldersContainsThisName = "Common",
                ResouceRepositoryName = "framework-core2.0-common",
                ResourceUrlRepository = "https://github.com/wilsonsantosnet/framework-core2.0-common.git",
                ResourceLocalPathFolderExecuteCloning = @"C:\Projetos\Outros\Repositorios",
                ResourceLocalPathDestinationFolrderApplication = @"C:\Projetos\solution-base-core2.0-ddd-project-with-gerador-empty\"
            };

        }

        private ExternalResource ConfigExternarResourcesProjectBaseLayoutCoreUIAngular60(bool replaceLocalFilesApplication)
        {

            return new ExternalResource
            {
                ReplaceLocalFilesApplication = true,
                DownloadOneTime = true,
                DownloadOneTimeFileVerify = "package.json",
                ResouceRepositoryName = "project-base-layout-front-coreui-angular6.0",
                ResourceUrlRepository = "https://github.com/wilsonsantosnet/project-base-layout-front-coreui-angular6.0.git",
                ResourceLocalPathFolderExecuteCloning = @"C:\Projetos\Outros\Repositorios",
                ResourceLocalPathDestinationFolrderApplication = @"C:\Projetos\solution-base-core2.0-ddd-project-with-gerador-empty\Seed.Spa.Ui"
            };

        }

        private ExternalResource ConfigExternarResourcesProjectBaseLayoutCoreUIAngular60OnlyThisFiles(bool replaceLocalFilesApplication)
        {

            return new ExternalResource
            {
                ReplaceLocalFilesApplication = true,
                ResouceRepositoryName = "project-base-layout-front-coreui-angular6.0",
                ResourceUrlRepository = "https://github.com/wilsonsantosnet/project-base-layout-front-coreui-angular6.0.git",
                ResourceLocalPathFolderExecuteCloning = @"C:\Projetos\Outros\Repositorios",
                ResourceLocalPathDestinationFolrderApplication = @"C:\Projetos\solution-base-core2.0-ddd-project-with-gerador-empty\Seed.Spa.Ui",
                OnlyThisFiles = new List<string> {
                    "package.json",
                    "Web.config",
                    "angular.json",
                    "src\\app\\app.component.css",
                    "src\\app\\app.component.html",
                    "src\\app\\app.component.ts",
                    "src\\app\\app.module.ts",
                    "src\\app\\global.service.culture.ts",
                    "src\\app\\global.service.ts",
                    "src\\app\\startup.service.ts",
                    "src\\app\\util\\util-shared.module.ts",
                    "src\\app\\main\\main.component.css",
                    "src\\app\\main\\main.component.html",
                    "src\\app\\main\\main.component.ts",
                    "src\\app\\main\\main.service.ts",
                    "src\\assets\\jquery.nestable.js",
                    "src\\app\\util\\enum\\enum.service.ts"
                }

            };

        }

        private ExternalResource ConfigExternarResourcesTemplatesFrontCoreUiAngular60(bool replaceLocalFilesApplication)
        {

            return new ExternalResource
            {
                ReplaceLocalFilesApplication = replaceLocalFilesApplication,
                ResouceRepositoryName = "template-gerador-front-coreui-angular6.0",
                ResourceUrlRepository = "https://github.com/wilsonsantosnet/template-gerador-front-coreui-angular6.0.git",
                ResourceLocalPathFolderExecuteCloning = @"C:\Projetos\Outros\Repositorios",
                ResourceLocalPathDestinationFolrderApplication = @"C:\Projetos\solution-base-core2.0-ddd-project-with-gerador-empty\Gerador.Gen\Templates\Front",
            };

        }

        private ExternalResource ConfigExternarResourcesFrameworkAngula60Crud(bool replaceLocalFilesApplication)
        {

            return new ExternalResource
            {
                ReplaceLocalFilesApplication = replaceLocalFilesApplication,
                ResouceRepositoryName = "framework-angular6.0-CRUD",
                ResourceUrlRepository = "https://github.com/wilsonsantosnet/framework-angular6.0-CRUD.git",
                ResourceLocalPathFolderExecuteCloning = @"C:\Projetos\Outros\Repositorios",
                ResourceLocalPathDestinationFolrderApplication = @"C:\Projetos\solution-base-core2.0-ddd-project-with-gerador-empty\Seed.Spa.Ui\src\app\common"
            };

        }

        private ExternalResource ConfigExternarResourcesProjectCustomLayoutCoreUIAngular80(bool replaceLocalFilesApplication)
        {

            return new ExternalResource
            {
                ReplaceLocalFilesApplication = replaceLocalFilesApplication,
                DownloadOneTime = true,
                DownloadOneTimeFileVerify = "package.json",
                ResouceRepositoryName = "project-custom-layout-front-coreui-angular8.0",
                ResourceUrlRepository = "https://github.com/wilsonsantosnet/project-custom-layout-front-coreui-angular8.0.git",
                ResourceLocalPathFolderExecuteCloning = @"C:\Projetos\Outros\Repositorios",
                ResourceLocalPathDestinationFolrderApplication = @"C:\Projetos\solution-base-core2.0-ddd-project-with-gerador-empty\Seed.Spa.Ui.Custom"
            };

        }

        private ExternalResource ConfigExternarResourcesProjectBaseItensSolutionOnlyThisFiles(bool replaceLocalFilesApplication)
        {

            return new ExternalResource
            {
                ReplaceLocalFilesApplication = true,
                ResouceRepositoryName = "solution-base-core2.0-ddd-project-with-gerador-empty",
                ResourceUrlRepository = "https://github.com/wilsonsantosnet/solution-base-core2.0-ddd-project-with-gerador-empty.git",
                ResourceLocalPathFolderExecuteCloning = @"C:\Projetos\Outros\Repositorios",
                ResourceLocalPathDestinationFolrderApplication = @"C:\Projetos\solution-base-core2.0-ddd-project-with-gerador-empty",
                OnlyThisFiles = new List<string> {
                    "Itens Solutions\\Common.Domain.dll",
                    "Itens Solutions\\Common.Gen.dll",
                    "Itens Solutions\\Common.dll",
                }

            };

        }

        public IEnumerable<ExternalResource> GetConfigExternarReources()
        {
            var replaceLocalFilesApplication = true;
            return StackV30(replaceLocalFilesApplication);

        }

        private IEnumerable<ExternalResource> StackV30(bool replaceLocalFilesApplication)
        {
            return new List<ExternalResource>
            {
               ConfigExternarResourcesTemplatesBackDDDCore20(replaceLocalFilesApplication),
               ConfigExternarResourcesFrameworkCommon20(replaceLocalFilesApplication),
               ConfigExternarResourcesTemplatesFrontCoreUiAngular60(replaceLocalFilesApplication),
               ConfigExternarResourcesFrameworkAngula60Crud(replaceLocalFilesApplication),
               ConfigExternarResourcesProjectBaseLayoutCoreUIAngular60(replaceLocalFilesApplication),
               ConfigExternarResourcesProjectBaseLayoutCoreUIAngular60OnlyThisFiles(replaceLocalFilesApplication),
               ConfigExternarResourcesProjectBaseItensSolutionOnlyThisFiles(replaceLocalFilesApplication),
               ConfigExternarResourcesProjectCustomLayoutCoreUIAngular80(replaceLocalFilesApplication)
            };
        }

    }
}
