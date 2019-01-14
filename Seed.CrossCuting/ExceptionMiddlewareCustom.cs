using Common.API;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;

namespace Seed.CrossCuting
{
    public static class ExceptionMiddlewareCustom
    {
        public static Action<IApplicationBuilder> ExceptionMiddlewareHaldler(ILogger logger)
        {
            return errorApp =>
            {
                errorApp.Run(async context =>
                {
                    try
                    {
                        var errorFeature = context.Features.Get<IExceptionHandlerFeature>();
                        var exception = errorFeature.Error as ExceptionRetrhow;
                        context.Response.ContentType = "application/json; charset=utf-8";
                        context.Response.StatusCode = exception.ObjectResult.StatusCode.Value;
                        context.Response.Headers.Add("Access-Control-Allow-Origin", "*");

                        logger.LogError(exception.Message);
                        await context.Response.WriteAsync(JsonConvert.SerializeObject(exception.ObjectResult.Value, new JsonSerializerSettings
                        {
                            ContractResolver = new CamelCasePropertyNamesContractResolver()
                        }), System.Text.Encoding.UTF8);
                    }
                    catch(Exception ex) {
                        logger.LogError(ex.Message);
                    }
                });
            };
        }

    }

    public static class ExceptionMiddlewareCustomExtension
    {
        public static IApplicationBuilder AddExceptionMiddlewareCustom(this IApplicationBuilder app, ILogger logger)
        {
            return app.UseExceptionHandler(ExceptionMiddlewareCustom.ExceptionMiddlewareHaldler(logger));
        }
    }
}
