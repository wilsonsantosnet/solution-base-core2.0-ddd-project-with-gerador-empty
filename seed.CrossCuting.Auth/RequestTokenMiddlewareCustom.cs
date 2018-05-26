using Common.API.Extensions;
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

namespace Seed.CrossCuting
{
    public class RequestTokenMiddlewareCustom : RequestTokenMiddleware
    {
        public RequestTokenMiddlewareCustom(RequestDelegate next) : base(next)
        {

        }

        protected override void ConfigClaims(CurrentUser currentUser, string tokenClear, Dictionary<string, object> claimsDictonary)
        {

            ProfileCustom.Claims(claimsDictonary);
            base.ConfigClaims(currentUser, tokenClear, claimsDictonary);
        }


    }

    public static class RequestTokenMiddlewareExtensionCustom
    {
        public static IApplicationBuilder AddTokenMiddlewareCustom(this IApplicationBuilder app)
        {
            return app.UseMiddleware<RequestTokenMiddlewareCustom>();
        }
    }

}
