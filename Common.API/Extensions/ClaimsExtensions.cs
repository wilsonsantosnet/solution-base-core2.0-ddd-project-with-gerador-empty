using Common.Domain.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Common.API.Extensions
{
    public static class ClaimsExtensions
    {

        public static IEnumerable<Tool> GetTools(this IDictionary<string, object> source)
        {
            if (source.Where(_ => _.Key == "tools").IsAny())
            {
                var toolsClaim = source.Where(_ => _.Key == "tools").SingleOrDefault();
                if (toolsClaim.IsNotNull())
                    return JsonConvert.DeserializeObject<IEnumerable<Tool>>(toolsClaim.Value.ToString());
            }

            return null;
        }

        public static IDictionary<string, object> ConvertToDictionary(this IEnumerable<Claim> claims)
        {
            var claimsDictonary = new Dictionary<string, object>();
            if (claims.IsAny())
            {
                foreach (var item in claims
                    .Select(_ => new KeyValuePair<string, object>(_.Type, _.Value)))
                {
                    if (!claimsDictonary.ContainsKey(item.Key))
                        claimsDictonary.Add(item.Key, item.Value);
                }

            }
            return claimsDictonary;
        }


    }
}
