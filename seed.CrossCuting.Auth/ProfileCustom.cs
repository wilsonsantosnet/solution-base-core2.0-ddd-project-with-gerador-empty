using Common.Domain.Model;
using IdentityModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Seed.CrossCuting
{
    public class ProfileCustom 
    {

        public static Dictionary<string, object> Claims(Dictionary<string, object>  _claims)
        {
            return _claims;
        }


        public static List<Claim> ClaimsForAdmin(string name, string email)
        {

            var tools = new List<dynamic>
            {
                new { Icon = "fa fa-edit", Name = "BancoBrasilChave", Value = "/bancobrasilchave" },

            };

            var _toolsForAdmin = JsonConvert.SerializeObject(tools);

            return new List<Claim>
            {
                new Claim(JwtClaimTypes.Name, name),
                new Claim(JwtClaimTypes.Email, email),
                new Claim("tools", _toolsForAdmin),
                new Claim("role", "admin"),
            };
        }

        public static List<Claim> ClaimsForTenant(int tenantId, string name, string email)
        {

            var tools = new List<dynamic>
            {
                new { Icon = "fa fa-edit", Name = "Tool", Value = "#/Url" },
            };

            var _toolsForSubscriber = JsonConvert.SerializeObject(tools);

            return new List<Claim>
            {
                new Claim(JwtClaimTypes.Name, name),
                new Claim(JwtClaimTypes.Email, email),
                new Claim("tools",_toolsForSubscriber),
                new Claim("role","tenant"),
            };
        }

    }
}
