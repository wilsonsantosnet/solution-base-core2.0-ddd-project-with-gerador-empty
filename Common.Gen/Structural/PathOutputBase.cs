using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Gen
{
    static class PathOutputBase
    {
        public static string PathBase(string pathProject,Boolean UsePathProjects)
        {
            var pathOutputLocal = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Output");

            if (UsePathProjects)
                return String.IsNullOrEmpty(pathProject) ? pathOutputLocal : pathProject;

            return pathOutputLocal;
        }

        public static void MakeDirectory(string pathBase, params string[] paths)
        {
            PathOutputBase.MakeDirectory(Path.Combine(paths), pathBase);
        }

        public static void MakeDirectory(string directoryName, string pathBase)
        {

            var pathToGenerate = Path.Combine(pathBase, directoryName);

            if (!Directory.Exists(pathToGenerate))
                Directory.CreateDirectory(pathToGenerate);

        }



    }
}
