using Common.Domain;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;



namespace Common.Gen
{
    public class FixRenameExt
    {

        private static List<string> _typeFilesExt;

        static FixRenameExt()
        {
            _typeFilesExt = new List<string>
            {
                "Repository.cs",
                "OrderByCustomExtension.cs",
                "FilterCustomExtension.cs",
                "IsSuitableValidation.cs",
                "IsSuitableWarning.cs",
                "IsConsistentValidation.cs"
            };
        }

        public static void Fix(HelperSysObjectsBase sysObject)
        {
            foreach (var item in sysObject.Contexts)
            {

                FixFileInFolder(item.OutputClassDomain);
                FixFileInFolder(item.OutputClassApp);
                FixFileInFolder(item.OutputClassApi);
                FixFileInFolder(item.OutputClassDto);
                FixFileInFolder(item.OutputClassSummary);
                FixFileInFolder(item.OutputClassFilter);
                FixFileInFolder(item.OutputClassInfra);
                FixFileInFolder(item.OutputClassSso);
                FixFileInFolder(item.OutputAngular);
                FixFileInFolder(item.OutputClassCrossCustingAuth);
               

            }

        }

        private static void FixFileInFolder(string root)
        {
            if (root.IsNullOrEmpaty())
                throw new InvalidOperationException("Path not degine");
            
            var dirs = Directory.GetDirectories(root);
            foreach (var item in dirs)
            {
                var subDirs = Directory.GetDirectories(item);
                if (subDirs.IsAny())
                    FixFileInFolder(item);

                var files = new DirectoryInfo(item).GetFiles();
                foreach (var file in files)
                {
                    

                    var found = _typeFilesExt.Where(_ => _ == file.Name).IsAny();
                    if (found)
                    {
                        var newFileName = file.FullName.Replace(Path.GetExtension(file.FullName),string.Format("ext.{0}", Path.GetExtension(file.FullName))) ;
                        file.CopyTo(newFileName, true);
                        file.Delete();
                    }
                }
            }

        }
        
    }
}
