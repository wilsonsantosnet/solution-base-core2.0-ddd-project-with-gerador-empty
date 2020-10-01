using Common.Domain;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Common.Gen
{
    public class FixRenameSeed
    {

        private static readonly List<string> _foldersIgnore;
        private static readonly List<string> _filesIgnore;

        static FixRenameSeed()
        {
            _foldersIgnore = new List<string>
            {
                ".git",
                ".vs",
                "bin",
                "obj",
                "wwwroot",
                "upload",
                "environments",
                "assets",
                "node_modules",
                "Common.API",
                "Common.Cache",
                "Common.Configuration",
                "Common.Cripto",
                "Common.Domain",
                "Common.Dto",
                "Common.Gen",
                "Common.Mail",
                "Common.Orm",
                "Common.Payment",
                "Common.Validation",
            };

            _filesIgnore = new List<string>
            {
                ".gitignore",
                "readme.md",
                "common.dll",
                "common.domain.dll",
                "common.gen.dll",
                "ids4sm.pfx",
                "ids4smbasic.pfx",
                "angular.json",
                "tsconfig.app.json",
                "tsconfig.spec.json",
                ".editorconfig",
                "package.json",
                "package-lock.json",
                "README.md",
                "browserslist",
                "index.html",
                "karma.conf.js",
                "main.ts",
                "polyfills.ts",
                "styles.css",
                "test.ts",
                "tsconfig.app.json",
                "tsconfig.spec.json",
                "tslint.json",
            };
        }

        public static void Fix(string root, string projectName, bool replaceinContentFile)
        {

            if (root.IsNullOrEmpty())
                throw new InvalidOperationException("OutputClassRoot not define un app.config on gerador");

            FixFileInFolder(root, projectName, replaceinContentFile);
            Console.WriteLine("cleaning start");
            System.Threading.Thread.Sleep(10000);
            ClearEnd(root);
            Console.WriteLine("cleaning end");
        }

        public static void ClearEnd(string root)
        {

            var foldersSeed = new DirectoryInfo(root).GetDirectories();
            foreach (var item in foldersSeed)
            {
                if (item.Name.ToLower().Contains("seed"))
                    item.Delete(true);
            }
        }

        private static void FixFileInFolder(string root, string projectName, bool replaceinContentFile)
        {
            FileFix(projectName, root, replaceinContentFile);

            var dirs = new DirectoryInfo(root).GetDirectories();
            foreach (var dir in dirs)
            {

                if (_foldersIgnore.Select(_=>_.ToLower()).Contains(dir.Name.ToLower()))
                    continue;

                var newPath = Path.Combine(Path.GetDirectoryName(dir.FullName), Regex.Replace(Path.GetFileName(dir.FullName), "Seed", projectName, RegexOptions.IgnoreCase));
                if (dir.FullName != newPath)
                    if (!Directory.Exists(newPath))
                        dir.MoveTo(newPath);

                var subDirs = Directory.GetDirectories(dir.FullName);
                if (subDirs.IsAny())
                    FixFileInFolder(dir.FullName, projectName, replaceinContentFile);

                var dirRoot = dir.FullName;
                FileFix(projectName, dirRoot, replaceinContentFile);
            }

        }

        private static void FileFix(string projectName, string dirRoot, bool replaceinContentFile)
        {
            var files = new DirectoryInfo(dirRoot).GetFiles();
            foreach (var file in files)
            {


                var newFileName = Path.Combine(Path.GetDirectoryName(file.FullName), Regex.Replace(Path.GetFileName(file.FullName), "Seed", projectName, RegexOptions.IgnoreCase));
                var newPath = Path.GetDirectoryName(newFileName);
                if (!Directory.Exists(newPath))
                    Directory.CreateDirectory(newPath);

                Console.WriteLine($"{file.Name} >> {new FileInfo(newFileName).Name}");

                if (file.FullName != newFileName)
                {
                    file.CopyTo(newFileName, true);
                    file.Delete();
                }

                if (_filesIgnore.Contains(file.Name.ToLower()))
                    continue;

                if (replaceinContentFile)
                    FixContentFile(projectName, newFileName);

            }
        }

        public static void FixContentFile(string projectName, string newFileName)
        {
            Console.WriteLine($"{projectName} >> {new FileInfo(newFileName).Name} >> change content.");
            var contentBody = File.ReadAllText(newFileName);
            contentBody = Regex.Replace(contentBody, "Seed", projectName, RegexOptions.IgnoreCase);
            using (var writer = new HelperStream(newFileName).GetInstance())
            {
                writer.Write(contentBody);
            }
        }

        public static IEnumerable<string> FixCollectionFile(IEnumerable<string> files, string projectName)
        {
            var filesFix = new List<string>();
            foreach (var file in files)
            {
                filesFix.Add(Regex.Replace(file, "Seed", projectName, RegexOptions.IgnoreCase));
            }

            return filesFix;
        }
    }
}
