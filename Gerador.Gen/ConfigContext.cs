using Common.Gen;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Score.Platform.Account.Gen
{
    public class ConfigContext
    {
        #region Config Contexts

        private Context ConfigContextDefault()
        {
            var contextName = "Score.Platform.Account";

            return new Context
            {

                ConnectionString = ConfigurationManager.ConnectionStrings["Score.Platform.Account"].ConnectionString,

                Namespace = "Score.Platform.Account",
                ContextName = contextName,
                ShowKeysInFront = false,
                LengthBigField = 250,
                OverrideFiles = true,
                UseRouteGuardInFront = true,

                OutputClassDomain = ConfigurationManager.AppSettings[string.Format("outputClassDomain")],
                OutputClassInfra = ConfigurationManager.AppSettings[string.Format("outputClassInfra")],
                OutputClassDto = ConfigurationManager.AppSettings[string.Format("outputClassDto")],
                OutputClassApp = ConfigurationManager.AppSettings[string.Format("outputClassApp")],
                OutputClassApi = ConfigurationManager.AppSettings[string.Format("outputClassApi")],
                OutputClassFilter = ConfigurationManager.AppSettings[string.Format("outputClassFilter")],
                OutputClassSummary = ConfigurationManager.AppSettings[string.Format("outputClassSummary")],
                OutputAngular = ConfigurationManager.AppSettings["OutputAngular"],
                OutputClassSso = ConfigurationManager.AppSettings["OutputClassSso"],
                OutputClassCrossCustingAuth = ConfigurationManager.AppSettings["OutputClassCrossCustingAuth"],

                Arquiteture = ArquitetureType.DDD,
                CamelCasing = true,
                MakeFront = true,
                AlertNotFoundTable = true,
                MakeToolsProfile = true,

                TableInfo = new UniqueListTableInfo
                {
                    new TableInfo().FromTable("Program").MakeBack().MakeFront(),
                    new TableInfo().FromTable("Tenant").MakeBack().MakeFront(),
                    new TableInfo().FromTable("Thema").MakeBack().MakeFront(),
                }
            };
        }



        public IEnumerable<Context> GetConfigContext()
        {

            return new List<Context>
            {

                ConfigContextDefault(),

            };

        }

        #endregion
    }
}
