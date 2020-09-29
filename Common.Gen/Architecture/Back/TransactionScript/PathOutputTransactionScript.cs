using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Gen
{
    static class PathOutputTransactionScript
    {
        public static string PathOutputTransactionScriptDto(TableInfo tableInfo, Context configContext)
        {
            var pathOutput = string.Empty;
            var pathBase = PathOutputBase.PathBase(configContext.OutputClassDto, configContext.UsePathProjects);
            pathOutput = Path.Combine(pathBase, "DtoTransaction", tableInfo.ClassName, string.Format("{0}Dto.{1}", tableInfo.ClassName, "cs"));
            PathOutputBase.MakeDirectory(pathBase, "DtoTransaction",  tableInfo.ClassName);
            return pathOutput;
        }

        public static string PathOutputTransactionScriptDtoSpecialized(TableInfo tableInfo, Context configContext)
        {
            var pathOutput = string.Empty;
            var pathBase = PathOutputBase.PathBase(configContext.OutputClassDto, configContext.UsePathProjects);
            pathOutput = Path.Combine(pathBase, "DtoTransaction", tableInfo.ClassName, string.Format("{0}DtoSpecialized.ext.{1}", tableInfo.ClassName, "cs"));
            PathOutputBase.MakeDirectory(pathBase, "DtoTransaction", tableInfo.ClassName);
            return pathOutput;
        }

        public static string PathOutputFilter(TableInfo tableInfo, Context configContext)
        {
            var pathOutput = string.Empty;
            var pathBase = PathOutputBase.PathBase(configContext.OutputClassFilter, configContext.UsePathProjects);
            pathOutput = Path.Combine(pathBase, "FiltersTransaction", tableInfo.ClassName, string.Format("{0}Filter.{1}", tableInfo.ClassName, "cs"));
            PathOutputBase.MakeDirectory(pathBase, "FiltersTransaction", tableInfo.ClassName);
            return pathOutput;
        }

        public static string PathOutputFilterPartial(TableInfo tableInfo, Context configContext)
        {
            var pathOutput = string.Empty;
            var pathBase = PathOutputBase.PathBase(configContext.OutputClassFilter, configContext.UsePathProjects);
            var fileName = tableInfo.ClassName;
            pathOutput = Path.Combine(pathBase, "FiltersTransaction", tableInfo.ClassName, string.Format("{0}Filter.ext.{1}", fileName, "cs"));
            PathOutputBase.MakeDirectory(pathBase, "FiltersTransaction", tableInfo.ClassName);
            return pathOutput;
        }

        public static string PathOutputTransactionScriptApi(TableInfo tableInfo, Context configContext)
        {
            var pathOutput = string.Empty;
            var pathBase = PathOutputBase.PathBase(configContext.OutputClassApi, configContext.UsePathProjects);
            pathOutput = Path.Combine(pathBase, "ControllersTransaction", string.Format("{0}Controller.{1}", tableInfo.ClassName, "cs"));
            PathOutputBase.MakeDirectory("ControllersTransaction", pathBase);
            return pathOutput;
        }

        public static string PathOutputTransactionScriptApiContainer(TableInfo tableInfo, Context configContext)
        {
            var pathOutput = string.Empty;
            var pathBase = PathOutputBase.PathBase(configContext.OutputClassApi, configContext.UsePathProjects);
            pathOutput = Path.Combine(pathBase, "ConfigTransaction", string.Format("ConfigContainer{0}.{1}", configContext.Module, "cs"));
            PathOutputBase.MakeDirectory("ConfigTransaction", pathBase);

            return pathOutput;
        }

        public static string PathOutputTransactionScriptApiSettings(Context configContext)
        {
            var pathOutput = string.Empty;

            var pathBase = PathOutputBase.PathBase(configContext.OutputClassApi, configContext.UsePathProjects);
            pathOutput = Path.Combine(pathBase, string.Format("appsettings.json"));
            return pathOutput;
        }

        public static string PathOutputTransactionScriptApiContainerPartial(TableInfo tableInfo, Context configContext)
        {
            var pathOutput = string.Empty;
            var pathBase = PathOutputBase.PathBase(configContext.OutputClassApi, configContext.UsePathProjects);
            pathOutput = Path.Combine(pathBase, "ConfigTransaction", string.Format("ConfigContainer{0}.ext.{1}", configContext.Module, "cs"));
            PathOutputBase.MakeDirectory("ConfigTransaction", pathBase);

            return pathOutput;
        }
      

        public static string PathOutputTransactionScriptApiHealth(TableInfo tableInfo, Context configContext)
        {

            var pathOutput = string.Empty;
            var pathBase = PathOutputBase.PathBase(configContext.OutputClassApi, configContext.UsePathProjects);
            pathOutput = Path.Combine(pathBase, "Controllers", string.Format("healthController.{0}", "cs"));
            PathOutputBase.MakeDirectory("Controllers", pathBase);
            return pathOutput;
        }

        public static string PathOutputTransactionScriptApiDownload(TableInfo tableInfo, Context configContext)
        {
            var pathOutput = string.Empty;
            var pathBase = PathOutputBase.PathBase(configContext.OutputClassApi, configContext.UsePathProjects);
            pathOutput = Path.Combine(pathBase, "Controllers", string.Format("downloadController.{0}", "cs"));
            PathOutputBase.MakeDirectory("Controllers", pathBase);
            return pathOutput;
        }

        public static string PathOutputTransactionScriptApiUpload(TableInfo tableInfo, Context configContext)
        {
            var pathOutput = string.Empty;
            var pathBase = PathOutputBase.PathBase(configContext.OutputClassApi, configContext.UsePathProjects);
            pathOutput = Path.Combine(pathBase, "Controllers", string.Format("uploadController.{0}", "cs"));
            PathOutputBase.MakeDirectory("Controllers", pathBase);
            return pathOutput;
        }

        public static string PathOutputTransactionScriptApiStart(TableInfo tableInfo, Context configContext)
        {
            var pathOutput = string.Empty;
            var pathBase = PathOutputBase.PathBase(configContext.OutputClassApi, configContext.UsePathProjects);
            pathOutput = Path.Combine(pathBase, string.Format("Startup.cs"));
            return pathOutput;

        }

        public static string PathOutputTransactionScriptApiCurrentUser(TableInfo tableInfo, Context configContext)
        {
            var pathOutput = string.Empty;
            var pathBase = PathOutputBase.PathBase(configContext.OutputClassApi, configContext.UsePathProjects);
            pathOutput = Path.Combine(pathBase, "Controllers", string.Format("CurrentUserController.{0}", "cs"));
            PathOutputBase.MakeDirectory("Controllers", pathBase);
            return pathOutput;
        }

        public static string PathOutputTransactionScriptApiAppSettings(TableInfo tableInfo, Context configContext)
        {
            var pathOutput = string.Empty;
            var pathBase = PathOutputBase.PathBase(configContext.OutputClassApi, configContext.UsePathProjects);
            pathOutput = Path.Combine(pathBase, string.Format("appsettings.json"));
            return pathOutput;
        }

        public static string PathOutputTransactionScriptEntityRepository(TableInfo tableInfo, Context configContext)
        {
            var pathOutput = string.Empty;
            var pathBase = PathOutputBase.PathBase(configContext.OutputClassInfra, configContext.UsePathProjects);
            pathOutput = Path.Combine(pathBase, "RepositoryTransaction", tableInfo.ClassName, string.Format("{0}Repository.{1}", tableInfo.ClassName, "cs"));
            PathOutputBase.MakeDirectory(pathBase, "RepositoryTransaction", tableInfo.ClassName);
            return pathOutput;

        }

        public static string PathOutputTransactionScriptIEntityRepository(TableInfo tableInfo, Context configContext)
        {
            var pathOutput = string.Empty;
            var pathBase = PathOutputBase.PathBase(configContext.OutputClassInfra, configContext.UsePathProjects);
            pathOutput = Path.Combine(pathBase, "Interfaces", tableInfo.ClassName, string.Format("I{0}Repository.{1}", tableInfo.ClassName, "cs"));
            PathOutputBase.MakeDirectory(pathBase, "Interfaces", tableInfo.ClassName);
            return pathOutput;
        }
    }
}
