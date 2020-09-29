using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace Common.Gen
{
    public class FixRenameFiles
    {

        private static readonly List<string> _foldersIgnore;
        private static readonly List<string> _filesIgnore;

        static FixRenameFiles()
        {
            _foldersIgnore = new List<string>
            {
                ".git",
                ".vs",
                "bin",
                "obj",
                "wwwroot",
                "upload",
                "node_modules",
                "environments",
                "assets"
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

        public static void Fix(string root, IDictionary<string, string> fromTo, bool replaceinContentFile)
        {
            if (root.IsNullOrEmpty())
                throw new InvalidOperationException("OutputClassRoot not define un app.config on gerador");

            foreach (var item in fromTo)
            {
                FixFileInFolder(root, item.Key, item.Value, replaceinContentFile);
            }

        }

        private static void FixFileInFolder(string root, string termOrigin, string termDestination, bool replaceinContentFile)
        {
            FileFix(root, termOrigin, termDestination, replaceinContentFile);

            var dirs = new DirectoryInfo(root).GetDirectories();
            foreach (var dir in dirs)
            {
               
                if (_foldersIgnore.Contains(dir.Name.ToLower()))
                    continue;

                var newPath = Path.Combine(Path.GetDirectoryName(dir.FullName), Regex.Replace(Path.GetFileName(dir.FullName), termOrigin, termDestination, RegexOptions.None));
                if (dir.FullName != newPath)
                    dir.MoveTo(newPath);

                Console.WriteLine($"FixFileInFolder newPath: {newPath} dir.FullName:{dir.FullName}");


                var subDirs = Directory.GetDirectories(dir.FullName);
                if (subDirs.IsAny())
                    FixFileInFolder(dir.FullName, termOrigin, termDestination, replaceinContentFile);

                var dirRoot = dir.FullName;
                FileFix(dirRoot, termOrigin, termDestination, replaceinContentFile);
            }

        }

        private static void FileFix(string dirRoot, string termOrigin, string termDestination, bool replaceinContentFile)
        {
            var files = new DirectoryInfo(dirRoot).GetFiles();
            foreach (var file in files)
            {
                if (_filesIgnore.Contains(file.Name.ToLower()))
                    continue;

                var newFileName = Path.Combine(Path.GetDirectoryName(file.FullName), Regex.Replace(Path.GetFileName(file.FullName), termOrigin, termDestination, RegexOptions.None));
                var newPath = Path.GetDirectoryName(newFileName);
                if (!Directory.Exists(newPath))
                    Directory.CreateDirectory(newPath);

                if (file.FullName != newFileName)
                {
                    file.CopyTo(newFileName, true);
                    file.Delete();
                }

                Console.WriteLine($"FileFix newFileName: {newFileName} file.FullName: {file.FullName}");

                if (replaceinContentFile)
                    FixContentFile(termOrigin, termDestination, newFileName);
            }
        }

        public static void FixContentFile(string termOrigin, string termDestination, string newFileName)
        {
            var contentBody = File.ReadAllText(newFileName);
            contentBody = Regex.Replace(contentBody, termOrigin, termDestination, RegexOptions.None);
            using (var writer = new HelperStream(newFileName).GetInstance())
            {
                writer.Write(contentBody);
            }

            Console.WriteLine($"FixContentFile newFileName {newFileName}");
        }
    }
}
