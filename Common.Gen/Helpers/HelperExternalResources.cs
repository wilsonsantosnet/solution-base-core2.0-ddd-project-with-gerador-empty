using Common.Domain;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;

namespace Common.Gen
{

    public class ExternalResource
    {

        public ExternalResource()
        {
            this.ReplaceLocalFilesApplication = true;
            this.DownloadOneTime = false;
            this.DownloadOneTimeFileVerify = string.Empty;
        }

        public bool ReplaceLocalFilesApplication { get; set; }
        public bool DownloadOneTime { get; set; }
        public bool SolutionBase { get; set; }
        public string DownloadOneTimeFileVerify { get; set; }

        public string ResouceRepositoryName { get; set; }
        public string ResourceUrlRepository { get; set; }
        public string ResourceLocalPathFolderExecuteCloning { get; set; }
        public string ResourceLocalPathDestinationFolrderApplication { get; set; }
        public string OnlyFoldersContainsThisName { get; set; }

        public IEnumerable<string> OnlyThisFiles { get; set; }

        public string ResourceLocalPathFolderCloningRepository
        {
            get
            {
                return Path.Combine(this.ResourceLocalPathFolderExecuteCloning, this.ResouceRepositoryName);
            }
        }


    }

    public static class HelperExternalResources
    {

        public static void CloneAndCopy(IEnumerable<ExternalResource> resources)
        {
            if (resources.IsAny())
            {
                foreach (var resource in resources.Where(_ => !_.SolutionBase))
                {
                    if (!ContinueFlow(resource))
                        continue;

                    Clone(resource);

                    if (resource.OnlyThisFiles.IsAny())
                    {
                        foreach (var item in resource.OnlyThisFiles)
                        {
                            var pathSource = Path.Combine(resource.ResourceLocalPathFolderExecuteCloning, resource.ResouceRepositoryName, item);
                            var fileSource = new FileInfo(pathSource);
                            var fileDestination = Path.Combine(resource.ResourceLocalPathDestinationFolrderApplication, item);

                            var directoryBaseDestination = Path.GetDirectoryName(fileDestination);
                            if (!Directory.Exists(directoryBaseDestination))
                                Directory.CreateDirectory(directoryBaseDestination);

                            fileSource.CopyTo(fileDestination, true);
                        }

                    }
                    else if (resource.ReplaceLocalFilesApplication)
                    {
                        bool isWindows = System.Runtime.InteropServices.RuntimeInformation
                                               .IsOSPlatform(OSPlatform.Windows);

                        if (isWindows)
                            HelperCmd.ExecuteCommand(string.Format("robocopy {0} {1}  /s /e /xd *\"bin\" *\"obj\" *\".git\" /xf *\".sln\" *\".md\" ", resource.ResourceLocalPathFolderCloningRepository, resource.ResourceLocalPathDestinationFolrderApplication), 10000);
                        else
                        {
                            if (!Directory.Exists(resource.ResourceLocalPathDestinationFolrderApplication))
                                Directory.CreateDirectory(resource.ResourceLocalPathDestinationFolrderApplication);
                            string.Format("rsync -arv --exclude='bin' --exclude='obj' --exclude='*.git' --exclude='*.sln' --exclude='*.md' {0}/ {1}", resource.ResourceLocalPathFolderCloningRepository, resource.ResourceLocalPathDestinationFolrderApplication).Bash();
                        }
                    }

                }
            }

        }

        public static void CloneOnly(IEnumerable<ExternalResource> resources)
        {
            foreach (var resource in resources.Where(_ => !_.SolutionBase))
            {
                Clone(resource);
            }

        }

        public static IEnumerable<string> CloneAndCopyStructureForNewContext(IEnumerable<ExternalResource> resources)
        {
            var filesOutput = new List<string>();

            var resource = resources.Where(_ => _.SolutionBase).SingleOrDefault();
            Clone(resource);

            var directorys = new DirectoryInfo(resource.ResourceLocalPathFolderCloningRepository).GetDirectories();
            foreach (var item in directorys)
            {
                if (item.Name.ToLower().Contains("seed"))
                    CopyFiles(filesOutput, resource, item);

            }

            return filesOutput;

        }

        private static void CopyFiles(List<string> filesOutput, ExternalResource resource, DirectoryInfo item)
        {
            var files = item.GetFiles();
            foreach (var file in files)
            {
                var pathDestination = Path.Combine(resource.ResourceLocalPathDestinationFolrderApplication, item.Name);

                var sourceFile = file.FullName;
                var destinationFile = Path.Combine(pathDestination, file.Name);
                if (File.Exists(sourceFile))
                {
                    if (!Directory.Exists(pathDestination))
                        Directory.CreateDirectory(pathDestination);

                    File.Copy(sourceFile, destinationFile, resource.ReplaceLocalFilesApplication);
                    PrinstScn.WriteLine("{0} foi copiado", destinationFile);
                    filesOutput.Add(destinationFile);
                }
            }
        }

        public static string CopySolutionFile(IEnumerable<ExternalResource> resources, string contextName)
        {
            var resource = resources.Where(_ => _.SolutionBase).SingleOrDefault();
            var solutionSeedSource = Path.Combine(resource.ResourceLocalPathFolderCloningRepository, "Seed.sln");
            if (File.Exists(solutionSeedSource))
            {
                var solutionSeedDestination = Path.Combine(resource.ResourceLocalPathDestinationFolrderApplication, string.Format("{0}.sln", contextName));
                File.Copy(solutionSeedSource, solutionSeedDestination);
                PrinstScn.WriteLine("{0} foi copiado", solutionSeedDestination);
                return solutionSeedDestination;
            }
            return null;
        }

        public static void UpdateLocalRepository(IEnumerable<ExternalResource> resources)
        {
            bool isWindows = System.Runtime.InteropServices.RuntimeInformation
                                               .IsOSPlatform(OSPlatform.Windows);

            foreach (var resource in resources)
            {

                if (!ContinueFlow(resource))
                    continue;

                if (resource.OnlyFoldersContainsThisName.IsNotNullOrEmpty())
                {
                    var foldersActual = new DirectoryInfo(resource.ResourceLocalPathDestinationFolrderApplication).GetDirectories()
                            .Where(_ => _.Name.Contains(resource.OnlyFoldersContainsThisName));

                    foreach (var folderActual in foldersActual)
                    {
                        if (isWindows)
                            HelperCmd.ExecuteCommand(string.Format("robocopy {0} {1} /s /e /xd *\"bin\" *\"obj\"", folderActual.FullName, string.Format("{0}\\{1}", resource.ResourceLocalPathFolderCloningRepository, folderActual.Name)), 10000);
                        else
                        {
                            if (!Directory.Exists(Path.Combine(resource.ResourceLocalPathFolderCloningRepository, folderActual.Name)))
                                Directory.CreateDirectory(Path.Combine(resource.ResourceLocalPathFolderCloningRepository, folderActual.Name));

                            string.Format("rsync -arv --exclude='bin' --exclude='obj' {0}/ {1}", folderActual.FullName, Path.Combine(resource.ResourceLocalPathFolderCloningRepository, folderActual.Name)).Bash();
                        }
                    }
                }
                else if (resource.OnlyThisFiles.IsAny())
                {
                    foreach (var item in resource.OnlyThisFiles)
                    {
                        var pathSource = Path.Combine(resource.ResourceLocalPathDestinationFolrderApplication, item);
                        var fileSource = new FileInfo(pathSource);
                        var fileDestination = Path.Combine(resource.ResourceLocalPathFolderExecuteCloning, resource.ResouceRepositoryName, item);

                        var directoryBaseDestination = Path.GetDirectoryName(fileDestination);
                        if (!Directory.Exists(directoryBaseDestination))
                            Directory.CreateDirectory(directoryBaseDestination);

                        if (fileSource.Exists)
                            fileSource.CopyTo(fileDestination, true);
                    }
                }
                else
                {
                    if (isWindows)
                        HelperCmd.ExecuteCommand(string.Format("robocopy {0} {1} /s /e", resource.ResourceLocalPathDestinationFolrderApplication, resource.ResourceLocalPathFolderCloningRepository), 10000);
                    else
                    {
                        if (!Directory.Exists(resource.ResourceLocalPathFolderCloningRepository))
                            Directory.CreateDirectory(resource.ResourceLocalPathFolderCloningRepository);
                        string.Format("rsync -arv {0}/ {1}", resource.ResourceLocalPathDestinationFolrderApplication, resource.ResourceLocalPathFolderCloningRepository).Bash();
                    }
                }
            }

        }

        #region helper

        private static void Clone(ExternalResource resource)
        {
            bool isWindows = System.Runtime.InteropServices.RuntimeInformation
                                                         .IsOSPlatform(OSPlatform.Windows);

            if (Directory.Exists(resource.ResourceLocalPathFolderCloningRepository))
            {
                if (isWindows)
                    HelperCmd.ExecuteCommand(string.Format("RMDIR {0} /S /Q", resource.ResourceLocalPathFolderCloningRepository), 10000);
                else
                    string.Format("rm -rf {0} ", resource.ResourceLocalPathFolderCloningRepository).Bash();

            }

            var command = string.Format("git clone {0} {1}", resource.ResourceUrlRepository, resource.ResourceLocalPathFolderCloningRepository);

            if (isWindows)
                HelperCmd.ExecuteCommand(command, 10000);
            else
                command.Bash();
        }

        private static void Bkp(ExternalResource resource)
        {
            bool isWindows = System.Runtime.InteropServices.RuntimeInformation
                                              .IsOSPlatform(OSPlatform.Windows);

            var bkpPathFolder = string.Format("{0}\\{1}-BKP", AppDomain.CurrentDomain.BaseDirectory, resource.ResouceRepositoryName);

            if (resource.OnlyFoldersContainsThisName.IsNotNullOrEmpty())
            {
                var foldersActual = new DirectoryInfo(resource.ResourceLocalPathDestinationFolrderApplication).GetDirectories()
                    .Where(_ => _.Name.Contains(resource.OnlyFoldersContainsThisName));

                foreach (var folderActual in foldersActual)
                {
                    if (Directory.Exists(folderActual.FullName))
                    {
                        if (isWindows)
                            HelperCmd.ExecuteCommand(string.Format("robocopy {0} {1} /s /e /xd *\"bin\" *\"obj\"", folderActual.FullName, string.Format("{0}\\{1}", bkpPathFolder, folderActual.Name)), 10000);

                        else
                        {
                            if (!Directory.Exists(Path.Combine(bkpPathFolder, folderActual.Name)))
                                Directory.CreateDirectory(Path.Combine(bkpPathFolder, folderActual.Name));
                            string.Format("rsync -arv {0}/ {1}", folderActual.FullName, Path.Combine(bkpPathFolder, folderActual.Name)).Bash();
                        }

                    }
                }
            }
            else
            {
                if (Directory.Exists(resource.ResourceLocalPathDestinationFolrderApplication))
                {
                    if (isWindows)
                        HelperCmd.ExecuteCommand(string.Format("robocopy {0} {1} /s /e", resource.ResourceLocalPathDestinationFolrderApplication, bkpPathFolder), 10000);
                    else
                    {
                        if (!Directory.Exists(bkpPathFolder))
                            Directory.CreateDirectory(bkpPathFolder);

                        string.Format("rsync -arv {0}/ {1}", resource.ResourceLocalPathDestinationFolrderApplication, bkpPathFolder).Bash();
                    }
                }
            }
        }

        private static bool ContinueFlow(ExternalResource resource)
        {
            var continueFlow = true;
            if (resource.DownloadOneTime)
            {
                var fileVerify = Path.Combine(resource.ResourceLocalPathDestinationFolrderApplication, resource.DownloadOneTimeFileVerify);

                if (File.Exists(fileVerify))
                    continueFlow = false;
            }


            return continueFlow;
        }

        #endregion

    }
}
