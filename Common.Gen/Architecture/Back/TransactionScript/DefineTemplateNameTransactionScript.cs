using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Gen
{
    public static class DefineTemplateNameTransactionScript
    {

        public static string TransactionScriptDto(TableInfo tableInfo)
        {
            return "dto";
        }

        public static string TransactionScriptProperty(TableInfo tableInfo)
        {
            return "property";
        }
        public static string TransactionScriptParameters(TableInfo tableInfo)
        {
            return "EntityRepository.parameters";
        }


        public static string TransactionScriptDtoSpecialized(TableInfo tableInfo)
        {
            return "dto.specialized";
        }

        public static string TransactionScriptFilter(TableInfo tableInfo)
        {
            return "Filter";
        }

        public static string TransactionScriptFilterPartial(TableInfo tableInfo)
        {
            return "Filter.partial";
        }


        public static string TransactionScriptApi(TableInfo tableInfo)
        {
            return "api";
        }

        public static string TransactionScriptApiHealth(TableInfo tableInfo)
        {
            return "api.health";
        }

        public static string TransactionScriptApiContainer(TableInfo tableInfo)
        {
            return "container";
        }

        public static string TransactionScriptApiContainerPartial(TableInfo tableInfo)
        {
            return "container.partial";
        }

        public static string TransactionScriptApiAppSettings(TableInfo tableInfo)
        {
            return "appsettings.json";
        }

        public static string TransactionScriptApiContainerInjections(TableInfo tableInfo)
        {
            return "container.injections";
        }


        public static string TransactionScriptApiDownload(TableInfo tableInfo)
        {
            return "api.download";
        }

        public static string TransactionScriptApiUpload(TableInfo tableInfo)
        {
            return "api.upload";
        }

        public static string TransactionScriptApiStart(TableInfo tableInfo)
        {
            return "api.start";
        }

        public static string TransactionScriptApiCurrentUser(TableInfo tableInfo)
        {
            return "api.currentuser";
        }



        public static string TransactionScriptEntityRepository(TableInfo tableInfo)
        {
            return "entityrepository";
        }

        public static string TransactionScriptIEntityRepository(TableInfo tableInfo)
        {
            return "ientityrepository";
        }
    }
}
