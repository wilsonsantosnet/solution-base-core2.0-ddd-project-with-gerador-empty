using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Gen
{
    public static class Read
    {

        public static string AllText(TableInfo tableInfo, string path, DefineTemplateFolder defineTemplateFolder)
        {
            var pathNew = path;
            if (!File.Exists(path))
            {
                var folderSpecial = defineTemplateFolder.Define(tableInfo);
                var folderDefault = defineTemplateFolder.GetDefaultTemplateFolder();
                if (folderSpecial != folderDefault)
                    pathNew = path.Replace(folderSpecial, folderDefault);
            }

            if (!File.Exists(pathNew))
                return string.Empty;

            return File.ReadAllText(pathNew);
        }
        public static string AllText(string path)
        {
            if (!File.Exists(path))
                return string.Empty;

            return File.ReadAllText(path);
        }


    }
}
