using Common.Domain.Base;
using Common.Domain.Model;
using IdentityModel.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Common.API.Extensions
{
    public class RequestTokenMiddleware
    {
        private readonly RequestDelegate _next;

        public RequestTokenMiddleware(RequestDelegate next)
        {
            this._next = next;
        }

        public async Task Invoke(HttpContext context, CurrentUser currentUser, IOptions<ConfigSettingsBase> configSettingsBase)
        {
            var token = context.Request.Headers["Authorization"];
            if (!token.IsNullOrEmpaty())
            {
                var tokenClear = token.ToString().Replace("Bearer ", "");
                var jwt = new JwtSecurityTokenHandler();
                var canRead = jwt.CanReadToken(tokenClear);
                if (canRead)
                {
                    try
                    {
                        //var claims = await GetClaimsFromServer(configSettingsBase, tokenClear);
                        var claims = GetClaimsFromUserPrincipal(context);
                        //var claims = GetClaimsFromReadToken(tokenClear, jwt);

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

                        this.ConfigClaims(currentUser, tokenClear, claimsDictonary);
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }

                }
            }
            await this._next.Invoke(context);
        }

        protected virtual void ConfigClaims(CurrentUser currentUser, string tokenClear, Dictionary<string, object> claimsDictonary)
        {
            currentUser.Init(tokenClear, claimsDictonary);
        }

        private static IEnumerable<Claim> GetClaimsFromReadToken(string tokenClear, JwtSecurityTokenHandler jwt)
        {
            var result = jwt.ReadJwtToken(tokenClear);
            var claims = result.Claims;
            return claims;
        }

        private static IEnumerable<Claim> GetClaimsFromUserPrincipal(HttpContext context)
        {
            var caller = context.User;
            var claims = caller.Claims;
            return claims;
        }

        private static async Task<IEnumerable<Claim>> GetClaimsFromServer(IOptions<ConfigSettingsBase> configSettingsBase, string tokenClear)
        {
            var discoveryClient = new DiscoveryClient(configSettingsBase.Value.AuthorityEndPoint);
            var doc = await discoveryClient.GetAsync();
            var userInfoClient = new UserInfoClient(doc.UserInfoEndpoint);
            var response = await userInfoClient.GetAsync(tokenClear);
            var claims = response.Claims;
            return claims;
        }
    }

    public static class RequestTokenMiddlewareExtension
    {
        public static IApplicationBuilder AddTokenMiddleware(this IApplicationBuilder app)
        {
            return app.UseMiddleware<RequestTokenMiddleware>();
        }
    }
}
