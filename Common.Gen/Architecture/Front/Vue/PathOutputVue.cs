using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Gen
{
    static class PathOutputVue
    {
        public static bool UsePathProjects { get; set; }

        public static string PathOutputVueViewComponent(TableInfo tableInfo, Context configContext, string partial)
        {
            var pathOutput = string.Empty;
            var className = tableInfo.ClassName.ToLower();

            var pathBase = configContext.OutputAngular;
            pathOutput = Path.Combine(pathBase, "views", className, string.Format("{0}.partial.vue", partial));
            PathOutputBase.MakeDirectory(pathBase, "views", tableInfo.ClassName.ToLower());

            return pathOutput;
        }

        public static string PathOutputVueIndexViewComponent(TableInfo tableInfo, Context configContext)
        {
            var pathOutput = string.Empty;
            var className = tableInfo.ClassName.ToLower();

            var pathBase = configContext.OutputAngular;
            pathOutput = Path.Combine(pathBase, "views", className, string.Format("index.vue"));
            PathOutputBase.MakeDirectory(pathBase, "views", tableInfo.ClassName.ToLower());

            return pathOutput;
        }

        public static string PathOutputVueRouterViewComponent(TableInfo tableInfo, Context configContext)
        {
            var pathOutput = string.Empty;
            var className = tableInfo.ClassName.ToLower();

            var pathBase = configContext.OutputAngular;
            pathOutput = Path.Combine(pathBase, "router", string.Format("generated.js"));
            PathOutputBase.MakeDirectory(pathBase, "router");

            return pathOutput;
        }

        private static string PathBase(string pathProject)
        {
            var pathOutputLocal = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Output");
            if (UsePathProjects)
                return String.IsNullOrEmpty(pathProject) ? pathOutputLocal : pathProject;

            return pathOutputLocal;
        }
        
        
    }
}
