using Common.Domain;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Common.Gen
{
    public class HelperMigationToAnotherProject
    {


        public static void DoMigation(HelperSysObjectsBase sysObject, string originPathRoot, string originNamespace)
        {

            var filesToMigrationsOrigin = new List<FileInfo>();
            var filesToMigrationsDestination = new List<string>();

            foreach (var ctx in sysObject.Contexts)
            {
                foreach (var item in ctx.TableInfo)
                {
                    var pathBase = PathOutputBase.PathBase(originPathRoot, ctx.UsePathProjects);
                    GetDirectorys(pathBase, filesToMigrationsOrigin, item);
                }
            }

            foreach (var ctx in sysObject.Contexts)
            {
                foreach (var file in filesToMigrationsOrigin)
                {
                    var newFilePath = file.FullName.Replace(originPathRoot, ctx.OutputClassRoot).Replace(originNamespace, ctx.Namespace);
                    filesToMigrationsDestination.Add(newFilePath);
                    PrinstScn.WriteLine("rename file {0} to list destination", file);

                    var pathBase = new FileInfo(newFilePath).DirectoryName;
                    if (!Directory.Exists(pathBase))
                        Directory.CreateDirectory(pathBase);

                    File.Copy(file.FullName, newFilePath, true);

                }
            }

            foreach (var item in filesToMigrationsDestination)
            {
                PrinstScn.WriteLine("Final Path : {0}", item);
            }

        }

        private static void GetDirectorys(string pathBase, List<FileInfo> filesToMigrationsOrigin, TableInfo tableInfo)
        {
            foreach (var dir in new DirectoryInfo(pathBase).GetDirectories())
            {
                var subDirectorys = new DirectoryInfo(dir.FullName).GetDirectories();
                if (subDirectorys.IsAny())
                {
                    foreach (var subDir in subDirectorys)
                    {
                        GetDirectorys(subDir.FullName, filesToMigrationsOrigin, tableInfo);
                    }
                }


                var files = dir.GetFiles().Where(_ => _.Name.Contains(tableInfo.TableName));
                foreach (var file in files)
                {
                    filesToMigrationsOrigin.Add(file);
                    PrinstScn.WriteLine("add {0} to list origin", file);
                }
            }
        }
    }
}
