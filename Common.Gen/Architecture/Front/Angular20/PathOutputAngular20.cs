using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Gen
{
    static class PathOutputAngular20
    {

        private static string pathAppDirectory(string pathBase)
        {
            return Path.Combine(pathBase, "src", "app");
        }
        private static string pathAppFile(string pathBase, string fileName)
        {
            return Path.Combine(pathAppDirectory(pathBase), fileName);
        }

        private static string pathComponentDirectory(string pathBase, string className)
        {
            return Path.Combine(pathBase, "src", "app", "main", className);
        }
        private static string pathComponentFile(string pathBase, string className, string typeFile)
        {
            return Path.Combine(pathComponentDirectory(pathBase, className), string.Format("{0}.{1}", className, typeFile));
        }


        private static string pathSubComponentDirectory(string pathBase, string className, string typeDirectory)
        {
            return Path.Combine(pathComponentDirectory(pathBase, className), string.Format("{0}-{1}", className, typeDirectory));
        }
        private static string pathSubComponentFile(string pathBase, string className, string typeDirectory, string typeFile)
        {
            return Path.Combine(pathSubComponentDirectory(pathBase, className, typeDirectory), string.Format("{0}-{1}.{2}", className, typeDirectory, typeFile));
        }

        public static string PathOutputAngular20ComponentTs(TableInfo tableInfo, Context configContext)
        {
            var pathOutput = string.Empty;
            var className = tableInfo.ClassName.ToLower();
            var pathBase = PathOutputBase.PathBase(configContext.OutputAngular, configContext.UsePathProjects);

            pathOutput = pathComponentFile(pathBase, className, "component.ts");
            PathOutputBase.MakeDirectory(pathComponentDirectory(pathBase, className));

            return pathOutput;
        }

        public static string PathOutputAngular20ComponentHtml(TableInfo tableInfo, Context configContext)
        {
            var pathOutput = string.Empty;
            var className = tableInfo.ClassName.ToLower();
            var pathBase = PathOutputBase.PathBase(configContext.OutputAngular, configContext.UsePathProjects);

            pathOutput = pathComponentFile(pathBase, className, "component.html");
            PathOutputBase.MakeDirectory(pathComponentDirectory(pathBase, className));

            return pathOutput;
        }



        public static string PathOutputAngular20ViewModel(TableInfo tableInfo, Context configContext)
        {
            var pathOutput = string.Empty;
            var className = tableInfo.ClassName.ToLower();
            var pathBase = PathOutputBase.PathBase(configContext.OutputAngular, configContext.UsePathProjects);

            pathOutput = pathComponentFile(pathBase, className, "view-model.ts");
            PathOutputBase.MakeDirectory(pathComponentDirectory(pathBase, className));

            return pathOutput;
        }

        public static string PathOutputAngular20ComponentCss(TableInfo tableInfo, Context configContext)
        {
            var pathOutput = string.Empty;
            var className = tableInfo.ClassName.ToLower();
            var pathBase = PathOutputBase.PathBase(configContext.OutputAngular, configContext.UsePathProjects);

            pathOutput = pathComponentFile(pathBase, className, "component.css");
            PathOutputBase.MakeDirectory(pathComponentDirectory(pathBase, className));

            return pathOutput;
        }

        public static string PathOutputAngular20ComponentModuleTs(TableInfo tableInfo, Context configContext)
        {
            var pathOutput = string.Empty;
            var className = tableInfo.ClassName.ToLower();
            var pathBase = PathOutputBase.PathBase(configContext.OutputAngular, configContext.UsePathProjects);
            pathOutput = pathComponentFile(pathBase, className, "module.ts");
            PathOutputBase.MakeDirectory(pathComponentDirectory(pathBase, className));

            return pathOutput;
        }

        public static string PathOutputAngular20ComponentAppRouting(Context configContext)
        {
            var pathOutput = string.Empty;
            var pathBase = PathOutputBase.PathBase(configContext.OutputAngular, configContext.UsePathProjects);

            pathOutput = pathAppFile(pathBase, "app.routing.ts");
            PathOutputBase.MakeDirectory(pathAppDirectory(pathBase));

            return pathOutput;
        }

        public static string PathOutputAngular20MainServiceReources(Context configContext)
        {

            var pathOutput = string.Empty;
            var pathBase = PathOutputBase.PathBase(configContext.OutputAngular, configContext.UsePathProjects);

            pathOutput = pathComponentDirectory(pathBase, "main.service.resource.ts");
            PathOutputBase.MakeDirectory(pathAppDirectory(pathBase));

            return pathOutput;
        }

        public static string PathOutputAngular20ComponentAppCustomRouting(Context configContext)
        {
            var pathOutput = string.Empty;
            var pathBase = PathOutputBase.PathBase(configContext.OutputAngular, configContext.UsePathProjects);

            pathOutput = pathAppFile(pathBase, "app.custom.routing.ts");
            PathOutputBase.MakeDirectory(pathAppDirectory(pathBase));

            return pathOutput;
        }

        public static string PathOutputAngular20ComponentRoutingModuleTs(TableInfo tableInfo, Context configContext)
        {
            var pathOutput = string.Empty;
            var className = tableInfo.ClassName.ToLower();
            var pathBase = PathOutputBase.PathBase(configContext.OutputAngular, configContext.UsePathProjects);

            pathOutput = pathComponentFile(pathBase, className, "routing.module.ts");
            PathOutputBase.MakeDirectory(pathComponentDirectory(pathBase, className));

            return pathOutput;
        }

        public static string PathOutputAngular20ComponentServiceTs(TableInfo tableInfo, Context configContext)
        {
            var pathOutput = string.Empty;
            var className = tableInfo.ClassName.ToLower();
            var pathBase = PathOutputBase.PathBase(configContext.OutputAngular, configContext.UsePathProjects);

            pathOutput = pathComponentFile(pathBase, className, "service.ts");
            PathOutputBase.MakeDirectory(pathComponentDirectory(pathBase, className));

            return pathOutput;
        }

        public static string PathOutputAngular20ComponentServiceFieldsTs(TableInfo tableInfo, Context configContext)
        {
            var pathOutput = string.Empty;
            var className = tableInfo.ClassName.ToLower();
            var pathBase = PathOutputBase.PathBase(configContext.OutputAngular, configContext.UsePathProjects);

            pathOutput = pathComponentFile(pathBase, className, "service.fields.ts");
            PathOutputBase.MakeDirectory(pathComponentDirectory(pathBase, className));

            return pathOutput;
        }

        public static string PathOutputAngular20ComponentServiceFieldsCustomTs(TableInfo tableInfo, Context configContext)
        {
            var pathOutput = string.Empty;
            var className = tableInfo.ClassName.ToLower();
            var pathBase = PathOutputBase.PathBase(configContext.OutputAngular, configContext.UsePathProjects);

            pathOutput = pathComponentFile(pathBase, className, "service.fields.custom.ts");
            PathOutputBase.MakeDirectory(pathComponentDirectory(pathBase, className));

            return pathOutput;
        }


        public static string PathOutputAngular20SubComponentDetailsTs(TableInfo tableInfo, Context configContext)
        {
            var pathOutput = string.Empty;
            var className = tableInfo.ClassName.ToLower();

            var pathBase = PathOutputBase.PathBase(configContext.OutputAngular, configContext.UsePathProjects);
            pathOutput = pathSubComponentFile(pathBase, className, "details", "component.ts");
            PathOutputBase.MakeDirectory(pathSubComponentDirectory(pathBase, className, "details"));

            return pathOutput;
        }

        public static string PathOutputAngular20SubComponentDetailsHtml(TableInfo tableInfo, Context configContext)
        {
            var pathOutput = string.Empty;
            var className = tableInfo.ClassName.ToLower();

            var pathBase = PathOutputBase.PathBase(configContext.OutputAngular, configContext.UsePathProjects);
            pathOutput = pathSubComponentFile(pathBase, className, "details", "component.html");
            PathOutputBase.MakeDirectory(pathSubComponentDirectory(pathBase, className, "details"));

            return pathOutput;
        }

        public static string PathOutputAngular20SubComponentDetailsFieldsHtml(TableInfo tableInfo, Context configContext)
        {
            var pathOutput = string.Empty;
            var className = tableInfo.ClassName.ToLower();

            var pathBase = PathOutputBase.PathBase(configContext.OutputAngular, configContext.UsePathProjects);
            pathOutput = pathSubComponentFile(pathBase, className, "field-details", "component.html");
            PathOutputBase.MakeDirectory(pathSubComponentDirectory(pathBase, className, "field-details"));

            return pathOutput;
        }

        public static string PathOutputAngular20SubComponentGridFieldsHtml(TableInfo tableInfo, Context configContext)
        {
            var pathOutput = string.Empty;
            var className = tableInfo.ClassName.ToLower();

            var pathBase = PathOutputBase.PathBase(configContext.OutputAngular, configContext.UsePathProjects);
            pathOutput = pathSubComponentFile(pathBase, className, "field-grid", "component.html");
            PathOutputBase.MakeDirectory(pathSubComponentDirectory(pathBase, className, "field-grid"));

            return pathOutput;
        }

        public static string PathOutputAngular20SubComponentDetailsFieldsTs(TableInfo tableInfo, Context configContext)
        {
            var pathOutput = string.Empty;
            var className = tableInfo.ClassName.ToLower();

            var pathBase = PathOutputBase.PathBase(configContext.OutputAngular, configContext.UsePathProjects);
            pathOutput = pathSubComponentFile(pathBase, className, "field-details", "component.ts");
            PathOutputBase.MakeDirectory(pathSubComponentDirectory(pathBase, className, "field-details"));

            return pathOutput;
        }

        public static string PathOutputAngular20SubComponentGridFieldsTs(TableInfo tableInfo, Context configContext)
        {
            var pathOutput = string.Empty;
            var className = tableInfo.ClassName.ToLower();

            var pathBase = PathOutputBase.PathBase(configContext.OutputAngular, configContext.UsePathProjects);
            pathOutput = pathSubComponentFile(pathBase, className, "field-grid", "component.ts");
            PathOutputBase.MakeDirectory(pathSubComponentDirectory(pathBase, className, "field-details"));

            return pathOutput;
        }

        public static string PathOutputAngular20SubComponentDetailsFieldsCss(TableInfo tableInfo, Context configContext)
        {
            var pathOutput = string.Empty;
            var className = tableInfo.ClassName.ToLower();

            var pathBase = PathOutputBase.PathBase(configContext.OutputAngular, configContext.UsePathProjects);
            pathOutput = pathSubComponentFile(pathBase, className, "field-details", "component.css");
            PathOutputBase.MakeDirectory(pathSubComponentDirectory(pathBase, className, "field-details"));

            return pathOutput;
        }

        public static string PathOutputAngular20SubComponentGridFieldsCss(TableInfo tableInfo, Context configContext)
        {
            var pathOutput = string.Empty;
            var className = tableInfo.ClassName.ToLower();

            var pathBase = PathOutputBase.PathBase(configContext.OutputAngular, configContext.UsePathProjects);
            pathOutput = pathSubComponentFile(pathBase, className, "field-grid", "component.css");
            PathOutputBase.MakeDirectory(pathSubComponentDirectory(pathBase, className, "field-grid"));

            return pathOutput;
        }

        public static string PathOutputAngular20SubComponentDetailsCss(TableInfo tableInfo, Context configContext)
        {
            var pathOutput = string.Empty;
            var className = tableInfo.ClassName.ToLower();

            var pathBase = PathOutputBase.PathBase(configContext.OutputAngular, configContext.UsePathProjects);
            pathOutput = pathSubComponentFile(pathBase, className, "details", "component.css");
            PathOutputBase.MakeDirectory(pathSubComponentDirectory(pathBase, className, "details"));

            return pathOutput;
        }

        public static string PathOutputAngular20SubComponentEditTs(TableInfo tableInfo, Context configContext)
        {
            var pathOutput = string.Empty;
            var className = tableInfo.ClassName.ToLower();

            var pathBase = PathOutputBase.PathBase(configContext.OutputAngular, configContext.UsePathProjects);
            pathOutput = pathSubComponentFile(pathBase, className, "edit", "component.ts");
            PathOutputBase.MakeDirectory(pathSubComponentDirectory(pathBase, className, "edit"));

            return pathOutput;
        }

        public static string PathOutputAngular20SubComponentCreate(TableInfo tableInfo, Context configContext, string extension)
        {
            var pathOutput = string.Empty;
            var className = tableInfo.ClassName.ToLower();

            var pathBase = PathOutputBase.PathBase(configContext.OutputAngular, configContext.UsePathProjects);
            pathOutput = pathSubComponentFile(pathBase, className, "create", string.Format("component.{0}", extension));
            PathOutputBase.MakeDirectory(pathSubComponentDirectory(pathBase, className, "create"));

            return pathOutput;
        }

        public static string PathOutputAngular20SubComponentCreateContainer(TableInfo tableInfo, Context configContext, string extension)
        {
            var pathOutput = string.Empty;
            var className = tableInfo.ClassName.ToLower();

            var pathBase = PathOutputBase.PathBase(configContext.OutputAngular, configContext.UsePathProjects);
            pathOutput = pathSubComponentFile(pathBase, className, "container-create", string.Format("component.{0}", extension));
            PathOutputBase.MakeDirectory(pathSubComponentDirectory(pathBase, className, "container-create"));

            return pathOutput;
        }

        public static string PathOutputAngular20SubComponentDetailsContainer(TableInfo tableInfo, Context configContext, string extension)
        {
            var pathOutput = string.Empty;
            var className = tableInfo.ClassName.ToLower();

            var pathBase = PathOutputBase.PathBase(configContext.OutputAngular, configContext.UsePathProjects);
            pathOutput = pathSubComponentFile(pathBase, className, "container-details", string.Format("component.{0}", extension));
            PathOutputBase.MakeDirectory(pathSubComponentDirectory(pathBase, className, "container-details"));

            return pathOutput;
        }

        public static string PathOutputAngular20SubComponentEditContainer(TableInfo tableInfo, Context configContext, string extension)
        {
            var pathOutput = string.Empty;
            var className = tableInfo.ClassName.ToLower();

            var pathBase = PathOutputBase.PathBase(configContext.OutputAngular, configContext.UsePathProjects);
            pathOutput = pathSubComponentFile(pathBase, className, "container-edit", string.Format("component.{0}", extension));
            PathOutputBase.MakeDirectory(pathSubComponentDirectory(pathBase, className, "container-edit"));

            return pathOutput;
        }


        public static string PathOutputAngular20SubComponentEditHtml(TableInfo tableInfo, Context configContext)
        {
            var pathOutput = string.Empty;
            var className = tableInfo.ClassName.ToLower();

            var pathBase = PathOutputBase.PathBase(configContext.OutputAngular, configContext.UsePathProjects);
            pathOutput = pathSubComponentFile(pathBase, className, "edit", "component.html");
            PathOutputBase.MakeDirectory(pathSubComponentDirectory(pathBase, className, "edit"));

            return pathOutput;
        }

        public static string PathOutputAngular20SubComponentEditCss(TableInfo tableInfo, Context configContext)
        {
            var pathOutput = string.Empty;
            var className = tableInfo.ClassName.ToLower();

            var pathBase = PathOutputBase.PathBase(configContext.OutputAngular, configContext.UsePathProjects);
            pathOutput = pathSubComponentFile(pathBase, className, "edit", "component.css");
            PathOutputBase.MakeDirectory(pathSubComponentDirectory(pathBase, className, "edit"));

            return pathOutput;
        }


        public static string PathOutputAngular20SubComponentFieldCreateTs(TableInfo tableInfo, Context configContext)
        {
            var pathOutput = string.Empty;
            var className = tableInfo.ClassName.ToLower();

            var pathBase = PathOutputBase.PathBase(configContext.OutputAngular, configContext.UsePathProjects);
            pathOutput = pathSubComponentFile(pathBase, className, "field-create", "component.ts");
            PathOutputBase.MakeDirectory(pathSubComponentDirectory(pathBase, className, "field-create"));

            return pathOutput;
        }

        public static string PathOutputAngular20SubComponentFieldEditTs(TableInfo tableInfo, Context configContext)
        {
            var pathOutput = string.Empty;
            var className = tableInfo.ClassName.ToLower();

            var pathBase = PathOutputBase.PathBase(configContext.OutputAngular, configContext.UsePathProjects);
            pathOutput = pathSubComponentFile(pathBase, className, "field-edit", "component.ts");
            PathOutputBase.MakeDirectory(pathSubComponentDirectory(pathBase, className, "field-edit"));

            return pathOutput;
        }

        public static string PathOutputAngular20SubComponentFieldCreateHtml(TableInfo tableInfo, Context configContext)
        {
            var pathOutput = string.Empty;
            var className = tableInfo.ClassName.ToLower();

            var pathBase = PathOutputBase.PathBase(configContext.OutputAngular, configContext.UsePathProjects);
            pathOutput = pathSubComponentFile(pathBase, className, "field-create", "component.html");
            PathOutputBase.MakeDirectory(pathSubComponentDirectory(pathBase, className, "field-create"));

            return pathOutput;
        }


        public static string PathOutputAngular20SubComponentFieldEditdHtml(TableInfo tableInfo, Context configContext)
        {
            var pathOutput = string.Empty;
            var className = tableInfo.ClassName.ToLower();

            var pathBase = PathOutputBase.PathBase(configContext.OutputAngular, configContext.UsePathProjects);
            pathOutput = pathSubComponentFile(pathBase, className, "field-edit", "component.html");
            PathOutputBase.MakeDirectory(pathSubComponentDirectory(pathBase, className, "field-edit"));

            return pathOutput;
        }

        public static string PathOutputAngular20SubComponentFieldCreateCss(TableInfo tableInfo, Context configContext)
        {
            var pathOutput = string.Empty;
            var className = tableInfo.ClassName.ToLower();

            var pathBase = PathOutputBase.PathBase(configContext.OutputAngular, configContext.UsePathProjects);
            pathOutput = pathSubComponentFile(pathBase, className, "field-create", "component.css");
            PathOutputBase.MakeDirectory(pathSubComponentDirectory(pathBase, className, "field-create"));

            return pathOutput;
        }

        public static string PathOutputAngular20SubComponentFieldEditCss(TableInfo tableInfo, Context configContext)
        {
            var pathOutput = string.Empty;
            var className = tableInfo.ClassName.ToLower();

            var pathBase = PathOutputBase.PathBase(configContext.OutputAngular, configContext.UsePathProjects);
            pathOutput = pathSubComponentFile(pathBase, className, "field-edit", "component.css");
            PathOutputBase.MakeDirectory(pathSubComponentDirectory(pathBase, className, "field-edit"));

            return pathOutput;
        }

        public static string PathOutputAngular20SubComponentFieldFilterTs(TableInfo tableInfo, Context configContext)
        {
            var pathOutput = string.Empty;
            var className = tableInfo.ClassName.ToLower();

            var pathBase = PathOutputBase.PathBase(configContext.OutputAngular, configContext.UsePathProjects);
            pathOutput = pathSubComponentFile(pathBase, className, "field-filter", "component.ts");
            PathOutputBase.MakeDirectory(pathSubComponentDirectory(pathBase, className, "field-filter"));

            return pathOutput;
        }

        public static string PathOutputAngular20SubComponentFieldFilterHtml(TableInfo tableInfo, Context configContext)
        {
            var pathOutput = string.Empty;
            var className = tableInfo.ClassName.ToLower();

            var pathBase = PathOutputBase.PathBase(configContext.OutputAngular, configContext.UsePathProjects);
            pathOutput = pathSubComponentFile(pathBase, className, "field-filter", "component.html");
            PathOutputBase.MakeDirectory(pathSubComponentDirectory(pathBase, className, "field-filter"));

            return pathOutput;
        }

        public static string PathOutputAngular20SubComponentFilterContainerHtml(TableInfo tableInfo, Context configContext)
        {
            var pathOutput = string.Empty;
            var className = tableInfo.ClassName.ToLower();

            var pathBase = PathOutputBase.PathBase(configContext.OutputAngular, configContext.UsePathProjects);
            pathOutput = pathSubComponentFile(pathBase, className, "container-filter", "component.html");
            PathOutputBase.MakeDirectory(pathSubComponentDirectory(pathBase, className, "container-filter"));

            return pathOutput;
        }

        public static string PathOutputAngular20SubComponentFilterContainerCss(TableInfo tableInfo, Context configContext)
        {
            var pathOutput = string.Empty;
            var className = tableInfo.ClassName.ToLower();

            var pathBase = PathOutputBase.PathBase(configContext.OutputAngular, configContext.UsePathProjects);
            pathOutput = pathSubComponentFile(pathBase, className, "container-filter", "component.css");
            PathOutputBase.MakeDirectory(pathSubComponentDirectory(pathBase, className, "container-filter"));

            return pathOutput;
        }

        public static string PathOutputAngular20SubComponentFilterContainerTs(TableInfo tableInfo, Context configContext)
        {
            var pathOutput = string.Empty;
            var className = tableInfo.ClassName.ToLower();

            var pathBase = PathOutputBase.PathBase(configContext.OutputAngular, configContext.UsePathProjects);
            pathOutput = pathSubComponentFile(pathBase, className, "container-filter", "component.ts");
            PathOutputBase.MakeDirectory(pathSubComponentDirectory(pathBase, className, "container-filter"));

            return pathOutput;
        }

        public static string PathOutputAngular20SubComponentFieldFilterCss(TableInfo tableInfo, Context configContext)
        {
            var pathOutput = string.Empty;
            var className = tableInfo.ClassName.ToLower();

            var pathBase = PathOutputBase.PathBase(configContext.OutputAngular, configContext.UsePathProjects);
            pathOutput = pathSubComponentFile(pathBase, className, "field-filter", "component.css");
            PathOutputBase.MakeDirectory(pathSubComponentDirectory(pathBase, className, "field-filter"));

            return pathOutput;

        }

        public static string PathOutputAngular20SubComponentPrintTs(TableInfo tableInfo, Context configContext)
        {
            var pathOutput = string.Empty;
            var className = tableInfo.ClassName.ToLower();

            var pathBase = PathOutputBase.PathBase(configContext.OutputAngular, configContext.UsePathProjects);
            pathOutput = pathSubComponentFile(pathBase, className, "print", "component.ts");
            PathOutputBase.MakeDirectory(pathSubComponentDirectory(pathBase, className, "print"));

            return pathOutput;
        }

        public static string PathOutputAngular20SubComponentPrintHtml(TableInfo tableInfo, Context configContext)
        {
            var pathOutput = string.Empty;
            var className = tableInfo.ClassName.ToLower();

            var pathBase = PathOutputBase.PathBase(configContext.OutputAngular, configContext.UsePathProjects);
            pathOutput = pathSubComponentFile(pathBase, className, "print", "component.html");
            PathOutputBase.MakeDirectory(pathSubComponentDirectory(pathBase, className, "print"));

            return pathOutput;
        }

        public static string PathOutputAngular20SubComponentPrintCss(TableInfo tableInfo, Context configContext)
        {
            var pathOutput = string.Empty;
            var className = tableInfo.ClassName.ToLower();

            var pathBase = PathOutputBase.PathBase(configContext.OutputAngular, configContext.UsePathProjects);
            pathOutput = pathSubComponentFile(pathBase, className, "print", "component.css");
            PathOutputBase.MakeDirectory(pathSubComponentDirectory(pathBase, className, "print"));

            return pathOutput;
        }

        public static string PathOutputAngular20SubComponentPrintModule(TableInfo tableInfo, Context configContext)
        {

            var pathOutput = string.Empty;
            var className = tableInfo.ClassName.ToLower();

            var pathBase = PathOutputBase.PathBase(configContext.OutputAngular, configContext.UsePathProjects);
            pathOutput = pathSubComponentFile(pathBase, className, "print", "module.ts");
            PathOutputBase.MakeDirectory(pathSubComponentDirectory(pathBase, className, "print"));

            return pathOutput;
        }


        public static string PathOutputAngular20SubComponentPrintRoutingModule(TableInfo tableInfo, Context configContext)
        {

            var pathOutput = string.Empty;
            var className = tableInfo.ClassName.ToLower();

            var pathBase = PathOutputBase.PathBase(configContext.OutputAngular, configContext.UsePathProjects);
            pathOutput = pathSubComponentFile(pathBase, className, "print", "routing.module.ts");
            PathOutputBase.MakeDirectory(pathSubComponentDirectory(pathBase, className, "print"));

            return pathOutput;
        }


    }
}
