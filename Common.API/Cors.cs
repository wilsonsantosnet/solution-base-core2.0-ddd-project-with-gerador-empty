using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Cors.Internal;
using Microsoft.Extensions.DependencyInjection;

namespace Common.API
{
    public static class Cors
    {

        public static void Enable(IServiceCollection services, params string[] origins)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAnyOrigin",
                    builder => builder
                    .AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod());
            });

            services.AddCors(options =>
            {
                options.AddPolicy("AllowStackOrigin",
                    builder => builder
                    .WithOrigins(origins)
                    .AllowAnyHeader()
                    .AllowAnyMethod());
            });

            services.Configure<MvcOptions>(options =>
            {
                options.Filters.Add(new CorsAuthorizationFilterFactory("AllowAnyOrigin"));
                options.Filters.Add(new CorsAuthorizationFilterFactory("AllowStackOrigin"));
            });

        }
    }
}
