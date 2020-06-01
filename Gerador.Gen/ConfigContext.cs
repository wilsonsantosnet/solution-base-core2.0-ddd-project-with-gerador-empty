using Common.Gen;
using System.Collections.Generic;
using System.Configuration;

namespace Seed.Gen
{
    public class ConfigContext
    {
        #region Config Contexts

        private Context ConfigContextDefault()
        {
            var contextName = "Seed";

            return new Context
            {

                ConnectionString = ConfigurationManager.ConnectionStrings["Seed"].ConnectionString,

                Namespace = "Seed",
                ContextName = contextName,
                ShowKeysInFront = false,
                LengthBigField = 250,
                OverrideFiles = false,
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

                Routes = new List<RouteConfig> {
                    new RouteConfig{ Route = "{ path: 'sampledash',  canActivate: [AuthGuard], loadChildren: () => import('./main/sampledash/sampledash.module').then(m => m.SampleDashModule) }" }
                },

                TableInfo = new UniqueListTableInfo
                {
                   new TableInfo().FromTable("Sample").MakeBack().MakeFront().AndConfigureThisFields(new List<FieldConfig> {
                       new FieldConfig
                       {
                           Name = "Valor",
                           Attributes = new List<string>{ "[textMask]='{mask: vm.masks.maskMoney}'" }
                       }
                   }),
                   new TableInfo().FromTable("SampleType").MakeBack().MakeFrontCrudBasic(),
                   new TableInfo().FromClass("SampleDash").MakeFrontBasic(),
                }
            };
        }


        private Context ConfigContextCustom()
        {
            var contextName = "Seed";

            return new Context
            {

                ConnectionString = ConfigurationManager.ConnectionStrings["Seed"].ConnectionString,

                Namespace = "Seed",
                ContextName = contextName,
                ShowKeysInFront = false,
                LengthBigField = 250,
                OverrideFiles = false,
                UseRouteGuardInFront = true,

                OutputClassDomain = ConfigurationManager.AppSettings[string.Format("outputClassDomain")],
                OutputClassInfra = ConfigurationManager.AppSettings[string.Format("outputClassInfra")],
                OutputClassDto = ConfigurationManager.AppSettings[string.Format("outputClassDto")],
                OutputClassApp = ConfigurationManager.AppSettings[string.Format("outputClassApp")],
                OutputClassApi = ConfigurationManager.AppSettings[string.Format("outputClassApi")],
                OutputClassFilter = ConfigurationManager.AppSettings[string.Format("outputClassFilter")],
                OutputClassSummary = ConfigurationManager.AppSettings[string.Format("outputClassSummary")],
                OutputAngular = ConfigurationManager.AppSettings["OutputAngularCustom"],
                OutputClassSso = ConfigurationManager.AppSettings["OutputClassSso"],
                OutputClassCrossCustingAuth = ConfigurationManager.AppSettings["OutputClassCrossCustingAuth"],

                Arquiteture = ArquitetureType.DDD,
                CamelCasing = true,
                MakeFront = true,
                AlertNotFoundTable = true,
                MakeToolsProfile = false,

                Routes = new List<RouteConfig> {
                    new RouteConfig{ Route = "{ path: 'sampledash',  canActivate: [AuthGuard], loadChildren: () => import('./main/sampledash/sampledash.module').then(m => m.SampleDashModule) }" }
                },

                TableInfo = new UniqueListTableInfo
                {
                   new TableInfo().FromClass("SampleDash").MakeFrontBasic(),
                }
            };
        }



        public IEnumerable<Context> GetConfigContext()
        {
            return new List<Context>
            {
                ConfigContextDefault(),
                ConfigContextCustom(),
            };

        }

        #endregion
    }
}
