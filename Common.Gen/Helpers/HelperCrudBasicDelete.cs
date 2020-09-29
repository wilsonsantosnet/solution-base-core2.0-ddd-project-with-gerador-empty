using Common.Domain;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;



namespace Common.Gen
{
    public class HelperCrudBasicDelete
    {

        private static List<string> _foldersToExclude;
        private static List<string> _filesToExclude;

        static HelperCrudBasicDelete()
        {
            _foldersToExclude = new List<string>
            {
                "-container-details",
                "-container-filter",
                "-details",
                "-field-details",
                "-field-filter",
                "-create",
                "-edit",
                "-print"
            };

            _filesToExclude = new List<string>
            {
                ".routing.module.ts",
                ".module.ts",
                ".component.html"
            };
        }

        public static void Fix(HelperSysObjectsBase sysObject)
        {
            foreach (var ctx in sysObject.Contexts)
            {
                var tableInfos = ctx.TableInfo.Where(_ => _.MakeFrontCrudBasic == true);
                if (tableInfos.Any())
                {
                    foreach (var tbi in tableInfos)
                    {
                        var folderTarget = $"{ctx.OutputAngular}\\src\\app\\main\\{tbi.TableName.ToLower()}";
                        if (Directory.Exists(folderTarget))
                        {
                            DeleteFolders(tbi.TableName.ToLower(), folderTarget);
                            DeleteFiles(tbi.TableName.ToLower(), folderTarget);
                        }
                    }
                }
            }

        }
        private static void DeleteFiles(string entity, string root)
        {
            var files = new DirectoryInfo(root).GetFiles();
            foreach (var file in files)
            {
                if (_filesToExclude.Where(fileToExclude => file.Name.Contains($"{entity}{fileToExclude}")).IsAny())
                {
                    file.Delete();
                }
            }

        }
        private static void DeleteFolders(string entity, string root)
        {
            if (root.IsNullOrEmpaty())
                throw new InvalidOperationException("Path not define");

            var dirs = Directory.GetDirectories(root);
            foreach (var item in dirs)
            {
                var subDirs = Directory.GetDirectories(item);
                if (subDirs.IsNotAny())
                {
                    if (_foldersToExclude.Where(_ => item.Contains($"{entity}{_}")).IsAny())
                    {
                        var dirInfo = new DirectoryInfo(item);
                        dirInfo.Delete(true);
                    }
                }

                foreach (var subItem in subDirs)
                {
                    if (_foldersToExclude.Where(_ => subItem.Contains($"{entity}{_}")).IsAny())
                    {
                        var dirInfo = new DirectoryInfo(subItem);
                        dirInfo.Delete(true);
                    }
                }

            }

        }

    }
}
