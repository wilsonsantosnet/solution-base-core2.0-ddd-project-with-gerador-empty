using Common.Gen;
using Common.Gen.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Seed.Gen
{


    public class ConfigExternalResources
    {
        private readonly string _basicPathProject;

        public ConfigExternalResources()
        {
            this._basicPathProject = ConfigurationManager.AppSettings[string.Format("PathProject")];
        }

        private string CombineUri(params string[] paths)
        {
            var newPathSlices = paths.SelectMany(_ => _.Split(@"\")).ToArray();
            var patch = Path.Combine(newPathSlices);
            if (Directory.Exists(patch))
                return new Uri(patch).LocalPath;

            return patch;

        }
        private ExternalResource ConfigExternarResourcesTemplatesBackDDDCore20(bool replaceLocalFilesApplication)
        {


            return new ExternalResource
            {
                ReplaceLocalFilesApplication = true,
                ResouceRepositoryName = "template-gerador-back-core2.0-DDD",
                ResourceUrlRepository = "https://github.com/wilsonsantosnet/template-gerador-back-core2.0-DDD.git",
                ResourceLocalPathFolderExecuteCloning = CombineUri(this._basicPathProject, @"Outros\Repositorios"),
                ResourceLocalPathDestinationFolrderApplication = CombineUri(this._basicPathProject, @"solution-base-core2.0-ddd-project-with-gerador-empty\Gerador.Gen\Templates\Back"),
            };

        }

        private ExternalResource ConfigExternarResourcesTemplatesBackDDD(bool replaceLocalFilesApplication)
        {

            return new ExternalResource
            {
                ReplaceLocalFilesApplication = true,
                ResouceRepositoryName = "template-gerador-back-DDD",
                ResourceUrlRepository = "https://github.com/wilsonsantosnet/template-gerador-back-DDD.git",
                ResourceLocalPathFolderExecuteCloning = CombineUri(this._basicPathProject, @"Outros\Repositorios"),
                ResourceLocalPathDestinationFolrderApplication = CombineUri(this._basicPathProject, @"solution-base-core2.0-ddd-project-with-gerador-empty\Gerador.Gen\Templates\Back"),
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
                ResourceLocalPathFolderExecuteCloning = CombineUri(this._basicPathProject, @"Outros\Repositorios"),
                ResourceLocalPathDestinationFolrderApplication = CombineUri(this._basicPathProject, @"solution-base-core2.0-ddd-project-with-gerador-empty\"),
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
                ResourceLocalPathFolderExecuteCloning = CombineUri(this._basicPathProject, @"Outros\Repositorios"),
                ResourceLocalPathDestinationFolrderApplication = CombineUri(this._basicPathProject, @"solution-base-core2.0-ddd-project-with-gerador-empty\"),
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
                ResourceLocalPathFolderExecuteCloning = CombineUri(this._basicPathProject, @"Outros\Repositorios"),
                ResourceLocalPathDestinationFolrderApplication = CombineUri(this._basicPathProject, @"solution-base-core2.0-ddd-project-with-gerador-empty\Seed.Spa.Ui"),
            };

        }

        private ExternalResource ConfigExternarResourcesProjectBaseLayoutCoreUIAngular60OnlyThisFiles(bool replaceLocalFilesApplication)
        {

            return new ExternalResource
            {
                ReplaceLocalFilesApplication = true,
                ResouceRepositoryName = "project-base-layout-front-coreui-angular6.0",
                ResourceUrlRepository = "https://github.com/wilsonsantosnet/project-base-layout-front-coreui-angular6.0.git",
                ResourceLocalPathFolderExecuteCloning = CombineUri(this._basicPathProject, @"Outros\Repositorios"),
                ResourceLocalPathDestinationFolrderApplication = CombineUri(this._basicPathProject, @"solution-base-core2.0-ddd-project-with-gerador-empty\Seed.Spa.Ui"),
                OnlyThisFiles = new List<string> {
                    CombineUri(@"package.json"),
                    CombineUri(@"Web.config"),
                    CombineUri(@"angular.json"),
                    CombineUri(@"src\app\app.component.css"),
                    CombineUri(@"src\app\app.component.html"),
                    CombineUri(@"src\app\app.component.ts"),
                    CombineUri(@"src\app\app.module.ts"),
                    CombineUri(@"src\app\global.service.culture.ts"),
                    CombineUri(@"src\app\global.service.ts"),
                    CombineUri(@"src\app\startup.service.ts"),
                    CombineUri(@"src\app\util\util-shared.module.ts"),
                    CombineUri(@"src\app\main\main.component.css"),
                    CombineUri(@"src\app\main\main.component.html"),
                    CombineUri(@"src\app\main\main.component.ts"),
                    CombineUri(@"src\app\main\main.service.ts"),
                    CombineUri(@"src\assets\jquery.nestable.js"),
                    CombineUri(@"src\app\util\enum\\enum.service.ts"),
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
                ResourceLocalPathFolderExecuteCloning = CombineUri(this._basicPathProject, @"Outros\Repositorios"),
                ResourceLocalPathDestinationFolrderApplication = CombineUri(this._basicPathProject, @"solution-base-core2.0-ddd-project-with-gerador-empty\Gerador.Gen\Templates\Front"),
            };

        }

        private ExternalResource ConfigExternarResourcesFrameworkAngula60Crud(bool replaceLocalFilesApplication)
        {

            return new ExternalResource
            {
                ReplaceLocalFilesApplication = replaceLocalFilesApplication,
                ResouceRepositoryName = "framework-angular6.0-CRUD",
                ResourceUrlRepository = "https://github.com/wilsonsantosnet/framework-angular6.0-CRUD.git",
                ResourceLocalPathFolderExecuteCloning = CombineUri(this._basicPathProject, @"Outros\Repositorios"),
                ResourceLocalPathDestinationFolrderApplication = CombineUri(this._basicPathProject, @"solution-base-core2.0-ddd-project-with-gerador-empty\Seed.Spa.Ui\src\app\common"),
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
                ResourceLocalPathFolderExecuteCloning = CombineUri(this._basicPathProject, @"Outros\Repositorios"),
                ResourceLocalPathDestinationFolrderApplication = CombineUri(this._basicPathProject, @"solution-base-core2.0-ddd-project-with-gerador-empty\Seed.Spa.Ui.Custom"),
            };

        }

        private ExternalResource ConfigExternarResourcesProjectBaseItensSolutionOnlyThisFiles(bool replaceLocalFilesApplication)
        {

            return new ExternalResource
            {
                ReplaceLocalFilesApplication = true,
                ResouceRepositoryName = "solution-base-core2.0-ddd-project-with-gerador-empty",
                ResourceUrlRepository = "https://github.com/wilsonsantosnet/solution-base-core2.0-ddd-project-with-gerador-empty.git",
                ResourceLocalPathFolderExecuteCloning = CombineUri(this._basicPathProject, @"Outros\Repositorios"),
                ResourceLocalPathDestinationFolrderApplication = CombineUri(this._basicPathProject, @"solution-base-core2.0-ddd-project-with-gerador-empty"),
                OnlyThisFiles = new List<string> {
                    CombineUri(@"Itens Solutions\Common.Domain.dll"),
                    CombineUri(@"Itens Solutions\Common.Gen.dll"),
                    CombineUri(@"Itens Solutions\Common.dll"),
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
