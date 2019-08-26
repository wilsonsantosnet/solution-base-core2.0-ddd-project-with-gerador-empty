using Common.Domain.Model;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Orm
{
    public static class ConnectionMt
    {
        public static string ConfigureConnectionString(CurrentUser user, IConfiguration config)
        {
            return ConfigureConnectionString(user.GetClaimByName<string>("databaseId"), config);
        }
        public static string ConfigureConnectionString(int databaseId, IConfiguration config, string stackName = "Default")
        {
            return ConfigureConnectionString(databaseId.ToString(), config, stackName);
        }
        public static string ConfigureConnectionString(string databaseId, IConfiguration config,string stackName = "Default")
        {
            if (databaseId.IsNotNullOrEmpty())
            {
                var cnsmt = config.GetSection($"ConfigConnectionString{databaseId}:{stackName}").Value;
                if (cnsmt.IsNotNullOrEmpty())
                    return cnsmt;
            }
            return config.GetSection($"ConfigConnectionString:{stackName}").Value;
        }

    }
}
