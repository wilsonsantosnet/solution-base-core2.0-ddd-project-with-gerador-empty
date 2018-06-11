using Common.Gen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seed.Gen
{
    public class ConfigExternalResources
    {
        private readonly string Stack;

        public ConfigExternalResources()
        {
            this.Stack = "V2.0";
        }


        private ExternalResource ConfigExternarResourcesTemplatesBackDDDCore20(bool replaceLocalFilesApplication)
        {

            return new ExternalResource
            {
                ReplaceLocalFilesApplication = true,
                ResouceRepositoryName = "template-gerador-back-core2.0-DDD",
                ResourceUrlRepository = "https://github.com/wilsonsantosnet/template-gerador-back-core2.0-DDD.git",
                ResourceLocalPathFolderExecuteCloning = @"C:\Projetos\Outros\Repositorios",
                ResourceLocalPathDestinationFolrderApplication = @"C:\Projetos\seed-core2.0-ddd-project-with-gerador-empty\Gerador.Gen\Templates\Back"
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
                ResourceLocalPathDestinationFolrderApplication = @"C:\Projetos\seed-core2.0-ddd-project-with-gerador-empty\Gerador.Gen\Templates\Back"
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
                ResourceLocalPathDestinationFolrderApplication = @"C:\Projetos\seed-core2.0-ddd-project-with-gerador-empty\"
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
                ResourceLocalPathDestinationFolrderApplication = @"C:\Projetos\seed-core2.0-ddd-project-with-gerador-empty\"
            };

        }

        private ExternalResource ConfigExternarResourcesSeedLayoutBs4Angular20(bool replaceLocalFilesApplication)
        {

            return new ExternalResource
            {
                ReplaceLocalFilesApplication = true,
                DownloadOneTime = true,
                DownloadOneTimeFileVerify = "package.json",
                ResouceRepositoryName = "Seed-layout-front-bs4-angular2.0",
                ResourceUrlRepository = "https://github.com/wilsonsantosnet/Seed-layout-front-bs4-angular2.0.git",
                ResourceLocalPathFolderExecuteCloning = @"C:\Projetos\Outros\Repositorios",
                ResourceLocalPathDestinationFolrderApplication = @"C:\Projetos\seed-core2.0-ddd-project-with-gerador-empty\Seed.Spa.Ui"
            };

        }
        private ExternalResource ConfigExternarResourcesSeedLayoutBs4Angular20OnlyThisFiles(bool replaceLocalFilesApplication)
        {

            return new ExternalResource
            {
                ReplaceLocalFilesApplication = true,
                ResouceRepositoryName = "Seed-layout-front-bs4-angular2.0",
                ResourceUrlRepository = "https://github.com/wilsonsantosnet/Seed-layout-front-bs4-angular2.0.git",
                ResourceLocalPathFolderExecuteCloning = @"C:\Projetos\Outros\Repositorios",
                ResourceLocalPathDestinationFolrderApplication = @"C:\Projetos\seed-core2.0-ddd-project-with-gerador-empty\Seed.Spa.Ui",
                OnlyThisFiles = new List<string> {
                    "package.json",
                    ".angular-cli.json",
                    "Web.config",
                    "src\\typings.d.ts",
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
                    "src\\assets\\css\\main.css",
                    "src\\assets\\css\\main.css.map",
                    "src\\assets\\jquery.nestable.js",
                }

            };

        }
        private ExternalResource ConfigExternarResourcesTemplatesFrontBs4Angular20(bool replaceLocalFilesApplication)
        {

            return new ExternalResource
            {
                ReplaceLocalFilesApplication = replaceLocalFilesApplication,
                ResouceRepositoryName = "template-gerador-front-bs4-angular2.0",
                ResourceUrlRepository = "https://github.com/wilsonsantosnet/template-gerador-front-bs4-angular2.0.git",
                ResourceLocalPathFolderExecuteCloning = @"C:\Projetos\Outros\Repositorios",
                ResourceLocalPathDestinationFolrderApplication = @"C:\Projetos\seed-core2.0-ddd-project-with-gerador-empty\Gerador.Gen\Templates\Front",
            };

        }
        private ExternalResource ConfigExternarResourcesFrameworkAngula20Crud(bool replaceLocalFilesApplication)
        {

            return new ExternalResource
            {
                ReplaceLocalFilesApplication = true,
                ResouceRepositoryName = "framework-angular2.0-CRUD",
                ResourceUrlRepository = "https://github.com/wilsonsantosnet/framework-angular2.0-CRUD.git",
                ResourceLocalPathFolderExecuteCloning = @"C:\Projetos\Outros\Repositorios",
                ResourceLocalPathDestinationFolrderApplication = @"C:\Projetos\seed-core2.0-ddd-project-with-gerador-empty\Seed.Spa.Ui\src\app\common"
            };

        }

        private ExternalResource ConfigExternarResourcesSeedLayoutBs4Angular60(bool replaceLocalFilesApplication)
        {

            return new ExternalResource
            {
                ReplaceLocalFilesApplication = true,
                DownloadOneTime = true,
                DownloadOneTimeFileVerify = "package.json",
                ResouceRepositoryName = "Seed-layout-front-bs4-angular6.0",
                ResourceUrlRepository = "https://github.com/wilsonsantosnet/Seed-layout-front-bs4-angular6.0.git",
                ResourceLocalPathFolderExecuteCloning = @"C:\Projetos\Outros\Repositorios",
                ResourceLocalPathDestinationFolrderApplication = @"C:\Projetos\seed-core2.0-ddd-project-with-gerador-empty\Seed.Spa.Ui"
            };

        }
        private ExternalResource ConfigExternarResourcesSeedLayoutBs4Angular60OnlyThisFiles(bool replaceLocalFilesApplication)
        {

            return new ExternalResource
            {
                ReplaceLocalFilesApplication = true,
                ResouceRepositoryName = "Seed-layout-front-bs4-angular6.0",
                ResourceUrlRepository = "https://github.com/wilsonsantosnet/Seed-layout-front-bs4-angular6.0.git",
                ResourceLocalPathFolderExecuteCloning = @"C:\Projetos\Outros\Repositorios",
                ResourceLocalPathDestinationFolrderApplication = @"C:\Projetos\seed-core2.0-ddd-project-with-gerador-empty\Seed.Spa.Ui",
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
                }

            };

        }

        private ExternalResource ConfigExternarResourcesSeedLayoutCoreUIAngular60(bool replaceLocalFilesApplication)
        {

            return new ExternalResource
            {
                ReplaceLocalFilesApplication = true,
                DownloadOneTime = true,
                DownloadOneTimeFileVerify = "package.json",
                ResouceRepositoryName = "Seed-layout-front-coreui-angular6.0",
                ResourceUrlRepository = "https://github.com/wilsonsantosnet/Seed-layout-front-coreui-angular6.0.git",
                ResourceLocalPathFolderExecuteCloning = @"C:\Projetos\Outros\Repositorios",
                ResourceLocalPathDestinationFolrderApplication = @"C:\Projetos\seed-core2.0-ddd-project-with-gerador-empty\Seed.Spa.Ui"
            };

        }
        private ExternalResource ConfigExternarResourcesSeedLayoutCoreUIAngular60OnlyThisFiles(bool replaceLocalFilesApplication)
        {

            return new ExternalResource
            {
                ReplaceLocalFilesApplication = true,
                ResouceRepositoryName = "Seed-layout-front-coreui-angular6.0",
                ResourceUrlRepository = "https://github.com/wilsonsantosnet/Seed-layout-front-coreui-angular6.0.git",
                ResourceLocalPathFolderExecuteCloning = @"C:\Projetos\Outros\Repositorios",
                ResourceLocalPathDestinationFolrderApplication = @"C:\Projetos\seed-core2.0-ddd-project-with-gerador-empty\Seed.Spa.Ui",
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
                ResourceLocalPathDestinationFolrderApplication = @"C:\Projetos\seed-core2.0-ddd-project-with-gerador-empty\Gerador.Gen\Templates\Front",
            };

        }

        private ExternalResource ConfigExternarResourcesTemplatesFrontBs4Angular60(bool replaceLocalFilesApplication)
        {

            return new ExternalResource
            {
                ReplaceLocalFilesApplication = replaceLocalFilesApplication,
                ResouceRepositoryName = "template-gerador-front-bs4-angular6.0",
                ResourceUrlRepository = "https://github.com/wilsonsantosnet/template-gerador-front-bs4-angular6.0.git",
                ResourceLocalPathFolderExecuteCloning = @"C:\Projetos\Outros\Repositorios",
                ResourceLocalPathDestinationFolrderApplication = @"C:\Projetos\seed-core2.0-ddd-project-with-gerador-empty\Gerador.Gen\Templates\Front",
            };

        }
        private ExternalResource ConfigExternarResourcesFrameworkAngula60Crud(bool replaceLocalFilesApplication)
        {

            return new ExternalResource
            {
                ReplaceLocalFilesApplication = true,
                ResouceRepositoryName = "framework-angular6.0-CRUD",
                ResourceUrlRepository = "https://github.com/wilsonsantosnet/framework-angular6.0-CRUD.git",
                ResourceLocalPathFolderExecuteCloning = @"C:\Projetos\Outros\Repositorios",
                ResourceLocalPathDestinationFolrderApplication = @"C:\Projetos\seed-core2.0-ddd-project-with-gerador-empty\Seed.Spa.Ui\src\app\common"
            };

        }

        public IEnumerable<ExternalResource> GetConfigExternarReources()
        {
            var replaceLocalFilesApplication = true;

            if (this.Stack == "V1.0")
                return StackV10(replaceLocalFilesApplication);

            return StackV30(replaceLocalFilesApplication);

        }

        private IEnumerable<ExternalResource> StackV10(bool replaceLocalFilesApplication)
        {
            return new List<ExternalResource>
            {
               ConfigExternarResourcesTemplatesBackDDD(replaceLocalFilesApplication),
               ConfigExternarResourcesFrameworkCommon(replaceLocalFilesApplication),
               ConfigExternarResourcesTemplatesFrontBs4Angular20(replaceLocalFilesApplication),
               ConfigExternarResourcesFrameworkAngula20Crud(replaceLocalFilesApplication),
               ConfigExternarResourcesSeedLayoutBs4Angular20(replaceLocalFilesApplication),
               ConfigExternarResourcesSeedLayoutBs4Angular20OnlyThisFiles(replaceLocalFilesApplication)
            };
        }

        private IEnumerable<ExternalResource> StackV20(bool replaceLocalFilesApplication)
        {
            return new List<ExternalResource>
            {

               ConfigExternarResourcesTemplatesBackDDDCore20(replaceLocalFilesApplication),
               ConfigExternarResourcesFrameworkCommon20(replaceLocalFilesApplication),
               ConfigExternarResourcesTemplatesFrontBs4Angular60(replaceLocalFilesApplication),
               ConfigExternarResourcesFrameworkAngula60Crud(replaceLocalFilesApplication),
               ConfigExternarResourcesSeedLayoutBs4Angular60(replaceLocalFilesApplication),
               ConfigExternarResourcesSeedLayoutBs4Angular60OnlyThisFiles(replaceLocalFilesApplication)
            };
        }

        private IEnumerable<ExternalResource> StackV30(bool replaceLocalFilesApplication)
        {
            return new List<ExternalResource>
            {

               ConfigExternarResourcesTemplatesBackDDDCore20(replaceLocalFilesApplication),
               ConfigExternarResourcesFrameworkCommon20(replaceLocalFilesApplication),
               ConfigExternarResourcesTemplatesFrontCoreUiAngular60(replaceLocalFilesApplication),
               ConfigExternarResourcesFrameworkAngula60Crud(replaceLocalFilesApplication),
               ConfigExternarResourcesSeedLayoutCoreUIAngular60(replaceLocalFilesApplication),
               ConfigExternarResourcesSeedLayoutCoreUIAngular60OnlyThisFiles(replaceLocalFilesApplication)
            };
        }

    }
}
