using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Gen
{
    static class PathOutputAngular
    {
        public static bool UsePathProjects { get; set; }

        #region save

        public static string PathOutputAngularSaveComponent(TableInfo tableInfo, Context configContext)
        {
            var pathOutput = string.Empty;
            var className = tableInfo.ClassName.ToLower();

            var pathBase = configContext.OutputAngular;
            pathOutput = Path.Combine(pathBase, "views", tableInfo.ClassName.ToLower(), "save", string.Format("{0}-save.component.{1}", tableInfo.ClassName.ToLower(), "ts"));
            PathOutputBase.MakeDirectory(pathBase, "views", tableInfo.ClassName.ToLower(), "save");

            return pathOutput;
        }

        public static string PathOutputAngularSaveGeneratedComponent(TableInfo tableInfo, Context configContext)
        {
            var pathOutput = string.Empty;
            var className = tableInfo.ClassName.ToLower();

            var pathBase = configContext.OutputAngular;
            pathOutput = Path.Combine(pathBase, "views", tableInfo.ClassName.ToLower(), "save", "generated", string.Format("{0}-save.generated.component.{1}", tableInfo.ClassName.ToLower(), "ts"));
            PathOutputBase.MakeDirectory(pathBase, "views", tableInfo.ClassName.ToLower(), "save", "generated");

            return pathOutput;
        }


        public static string PathOutputAngularSaveGeneratedComponentHtml(TableInfo tableInfo, Context configContext)
        {
            var pathOutput = string.Empty;
            var className = tableInfo.ClassName.ToLower();

            var pathBase = configContext.OutputAngular;
            pathOutput = Path.Combine(pathBase, "views", tableInfo.ClassName.ToLower(), "save", "generated", string.Format("{0}-save.generated.component.{1}", tableInfo.ClassName.ToLower(), "html"));
            PathOutputBase.MakeDirectory(pathBase, "views", tableInfo.ClassName.ToLower(), "save", "generated");

            return pathOutput;
        }

        public static string PathOutputAngularSaveComponentHtml(TableInfo tableInfo, Context configContext)
        {
            var pathOutput = string.Empty;
            var className = tableInfo.ClassName.ToLower();

            var pathBase = configContext.OutputAngular;
            pathOutput = Path.Combine(pathBase, "views", tableInfo.ClassName.ToLower(), "save", string.Format("{0}-save.component.{1}", tableInfo.ClassName.ToLower(), "html"));
            PathOutputBase.MakeDirectory(pathBase, "views", tableInfo.ClassName.ToLower(), "save");

            return pathOutput;
        }

        #endregion

        #region grid

        public static string PathOutputAngularGridComponent(TableInfo tableInfo, Context configContext)
        {
            var pathOutput = string.Empty;
            var className = tableInfo.ClassName.ToLower();

            var pathBase = configContext.OutputAngular;
            pathOutput = Path.Combine(pathBase, "views", tableInfo.ClassName.ToLower(), "grid", string.Format("{0}-grid.component.{1}", tableInfo.ClassName.ToLower(), "ts"));
            PathOutputBase.MakeDirectory(pathBase, "views", tableInfo.ClassName.ToLower(), "grid");

            return pathOutput;
        }

        public static string PathOutputAngularGridGeneratedComponent(TableInfo tableInfo, Context configContext)
        {
            var pathOutput = string.Empty;
            var className = tableInfo.ClassName.ToLower();

            var pathBase = configContext.OutputAngular;
            pathOutput = Path.Combine(pathBase, "views", tableInfo.ClassName.ToLower(), "grid", "generated", string.Format("{0}-grid.generated.component.{1}", tableInfo.ClassName.ToLower(), "ts"));
            PathOutputBase.MakeDirectory(pathBase, "views", tableInfo.ClassName.ToLower(), "grid", "generated");

            return pathOutput;
        }


        public static string PathOutputAngularGridGeneratedComponentHtml(TableInfo tableInfo, Context configContext)
        {
            var pathOutput = string.Empty;
            var className = tableInfo.ClassName.ToLower();

            var pathBase = configContext.OutputAngular;
            pathOutput = Path.Combine(pathBase, "views", tableInfo.ClassName.ToLower(), "grid", "generated", string.Format("{0}-grid.generated.component.{1}", tableInfo.ClassName.ToLower(), "html"));
            PathOutputBase.MakeDirectory(pathBase, "views", tableInfo.ClassName.ToLower(), "grid", "generated");

            return pathOutput;
        }

        public static string PathOutputAngularGridComponentHtml(TableInfo tableInfo, Context configContext)
        {
            var pathOutput = string.Empty;
            var className = tableInfo.ClassName.ToLower();

            var pathBase = configContext.OutputAngular;
            pathOutput = Path.Combine(pathBase, "views", tableInfo.ClassName.ToLower(), "grid", string.Format("{0}-grid.component.{1}", tableInfo.ClassName.ToLower(), "html"));
            PathOutputBase.MakeDirectory(pathBase, "views", tableInfo.ClassName.ToLower(), "grid");

            return pathOutput;
        }

        #endregion

        #region delete

        public static string PathOutputAngularDeleteComponent(TableInfo tableInfo, Context configContext)
        {
            var pathOutput = string.Empty;
            var className = tableInfo.ClassName.ToLower();

            var pathBase = configContext.OutputAngular;
            pathOutput = Path.Combine(pathBase, "views", tableInfo.ClassName.ToLower(), "delete", string.Format("{0}-delete.component.{1}", tableInfo.ClassName.ToLower(), "ts"));
            PathOutputBase.MakeDirectory(pathBase, "views", tableInfo.ClassName.ToLower(), "delete");

            return pathOutput;
        }

        public static string PathOutputAngularDeleteGeneratedComponent(TableInfo tableInfo, Context configContext)
        {
            var pathOutput = string.Empty;
            var className = tableInfo.ClassName.ToLower();

            var pathBase = configContext.OutputAngular;
            pathOutput = Path.Combine(pathBase, "views", tableInfo.ClassName.ToLower(), "delete", "generated", string.Format("{0}-delete.generated.component.{1}", tableInfo.ClassName.ToLower(), "ts"));
            PathOutputBase.MakeDirectory(pathBase, "views", tableInfo.ClassName.ToLower(), "delete", "generated");

            return pathOutput;
        }


        public static string PathOutputAngularDeleteGeneratedComponentHtml(TableInfo tableInfo, Context configContext)
        {
            var pathOutput = string.Empty;
            var className = tableInfo.ClassName.ToLower();

            var pathBase = configContext.OutputAngular;
            pathOutput = Path.Combine(pathBase, "views", tableInfo.ClassName.ToLower(), "delete", "generated", string.Format("{0}-delete.generated.component.{1}", tableInfo.ClassName.ToLower(), "html"));
            PathOutputBase.MakeDirectory(pathBase, "views", tableInfo.ClassName.ToLower(), "delete", "generated");

            return pathOutput;
        }

        #endregion

        #region filter

        public static string PathOutputAngularFilterComponent(TableInfo tableInfo, Context configContext)
        {
            var pathOutput = string.Empty;
            var className = tableInfo.ClassName.ToLower();

            var pathBase = configContext.OutputAngular;
            pathOutput = Path.Combine(pathBase, "views", tableInfo.ClassName.ToLower(), "filter", string.Format("{0}-filter.component.{1}", tableInfo.ClassName.ToLower(), "ts"));
            PathOutputBase.MakeDirectory(pathBase, "views", tableInfo.ClassName.ToLower(), "filter");

            return pathOutput;
        }

        public static string PathOutputAngularFilterGeneratedComponent(TableInfo tableInfo, Context configContext)
        {
            var pathOutput = string.Empty;
            var className = tableInfo.ClassName.ToLower();

            var pathBase = configContext.OutputAngular;
            pathOutput = Path.Combine(pathBase, "views", tableInfo.ClassName.ToLower(), "filter", "generated", string.Format("{0}-filter.generated.component.{1}", tableInfo.ClassName.ToLower(), "ts"));
            PathOutputBase.MakeDirectory(pathBase, "views", tableInfo.ClassName.ToLower(), "filter", "generated");

            return pathOutput;
        }

        public static string PathOutputAngularFilterGeneratedComponentHtml(TableInfo tableInfo, Context configContext)
        {
            var pathOutput = string.Empty;
            var className = tableInfo.ClassName.ToLower();

            var pathBase = configContext.OutputAngular;
            pathOutput = Path.Combine(pathBase, "views", tableInfo.ClassName.ToLower(), "filter", "generated", string.Format("{0}-filter.generated.component.{1}", tableInfo.ClassName.ToLower(), "html"));
            PathOutputBase.MakeDirectory(pathBase, "views", tableInfo.ClassName.ToLower(), "filter", "generated");

            return pathOutput;
        }

        public static string PathOutputAngularFilterComponentHtml(TableInfo tableInfo, Context configContext)
        {
            var pathOutput = string.Empty;
            var className = tableInfo.ClassName.ToLower();

            var pathBase = configContext.OutputAngular;
            pathOutput = Path.Combine(pathBase, "views", tableInfo.ClassName.ToLower(), "filter", string.Format("{0}-filter.component.{1}", tableInfo.ClassName.ToLower(), "html"));
            PathOutputBase.MakeDirectory(pathBase, "views", tableInfo.ClassName.ToLower(), "filter");

            return pathOutput;
        }

        #endregion

        public static string PathOutputAngularRoutingGenerated(TableInfo tableInfo, Context configContext)
        {
            var pathOutput = string.Empty;
            var className = tableInfo.ClassName.ToLower();

            var pathBase = configContext.OutputAngular;
            pathOutput = Path.Combine(pathBase, "routing", string.Format("routing.generated.module.{0}", "ts"));
            PathOutputBase.MakeDirectory("routing", pathBase);

            return pathOutput;
        }

        public static string PathOutputAngularModelGenerated(TableInfo tableInfo, Context configContext)
        {
            var pathOutput = string.Empty;
            var className = tableInfo.ClassName.ToLower();

            var pathBase = configContext.OutputAngular;
            pathOutput = Path.Combine(pathBase, "models", string.Format("{0}.generated.{1}", tableInfo.ClassName.ToLower(), "ts"));
            PathOutputBase.MakeDirectory("models", pathBase);

            return pathOutput;
        }

        public static string PathOutputAngularModelFilterGenerated(TableInfo tableInfo, Context configContext)
        {
            var pathOutput = string.Empty;
            var className = tableInfo.ClassName.ToLower();

            var pathBase = configContext.OutputAngular;
            pathOutput = Path.Combine(pathBase, "models", string.Format("{0}.generated.filter.{1}", tableInfo.ClassName.ToLower(), "ts"));
            PathOutputBase.MakeDirectory("models", pathBase);

            return pathOutput;
        }

        public static string PathOutputAngularModel(TableInfo tableInfo, Context configContext)
        {
            var pathOutput = string.Empty;
            var className = tableInfo.ClassName.ToLower();

            var pathBase = configContext.OutputAngular;
            pathOutput = Path.Combine(pathBase, "views", tableInfo.ClassName.ToLower(), "models", string.Format("{0}.{1}", tableInfo.ClassName.ToLower(), "ts"));
            PathOutputBase.MakeDirectory(pathBase, "views", tableInfo.ClassName.ToLower(), "models");

            return pathOutput;
        }

        public static string PathOutputAngularModelFilter(TableInfo tableInfo, Context configContext)
        {
            var pathOutput = string.Empty;
            var className = tableInfo.ClassName.ToLower();

            var pathBase = configContext.OutputAngular;
            pathOutput = Path.Combine(pathBase, "views", tableInfo.ClassName.ToLower(), "models", string.Format("{0}.filter.{1}", tableInfo.ClassName.ToLower(), "ts"));
            PathOutputBase.MakeDirectory(pathBase, "views", tableInfo.ClassName.ToLower(), "models");

            return pathOutput;
        }


        public static string PathOutputAngularRouting(TableInfo tableInfo, Context configContext)
        {
            var pathOutput = string.Empty;
            var className = tableInfo.ClassName.ToLower();

            var pathBase = configContext.OutputAngular;
            pathOutput = Path.Combine(pathBase, "views", tableInfo.ClassName.ToLower(), "routing", string.Format("{0}-routing.module.{1}", tableInfo.ClassName.ToLower(), "ts"));
            PathOutputBase.MakeDirectory(pathBase, "views", tableInfo.ClassName.ToLower(), "routing");

            return pathOutput;
        }
        public static string PathOutputAngularComponent(TableInfo tableInfo, Context configContext)
        {
            var pathOutput = string.Empty;
            var className = tableInfo.ClassName.ToLower();

            var pathBase = configContext.OutputAngular;
            pathOutput = Path.Combine(pathBase, "views", tableInfo.ClassName.ToLower(), string.Format("{0}.component.{1}", tableInfo.ClassName.ToLower(), "ts"));
            PathOutputBase.MakeDirectory(pathBase, "views", tableInfo.ClassName.ToLower());

            return pathOutput;
        }

        public static string PathOutputAngularComponentHtml(TableInfo tableInfo, Context configContext)
        {
            var pathOutput = string.Empty;
            var className = tableInfo.ClassName.ToLower();

            var pathBase = configContext.OutputAngular;
            pathOutput = Path.Combine(pathBase, "views", tableInfo.ClassName.ToLower(), string.Format("{0}.component.{1}", tableInfo.ClassName.ToLower(), "html"));
            PathOutputBase.MakeDirectory(pathBase, "views", tableInfo.ClassName.ToLower());

            return pathOutput;
        }

        public static string PathOutputAngularModule(TableInfo tableInfo, Context configContext)
        {
            var pathOutput = string.Empty;
            var className = tableInfo.ClassName.ToLower();

            var pathBase = configContext.OutputAngular;
            pathOutput = Path.Combine(pathBase, "views", tableInfo.ClassName.ToLower(), string.Format("{0}.module.{1}", tableInfo.ClassName.ToLower(), "ts"));
            PathOutputBase.MakeDirectory(pathBase, "views", tableInfo.ClassName.ToLower());

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
