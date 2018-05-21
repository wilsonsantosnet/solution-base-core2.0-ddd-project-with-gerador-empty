using Common.API;
using Common.Domain.Base;
using Common.Domain.Model;
using IdentityServer4.Validation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;

namespace Sso.Server.Api
{
    public class Startup
    {
        public IConfigurationRoot Configuration { get; }
        private readonly IHostingEnvironment _env;

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                 .SetBasePath(env.ContentRootPath)
                 .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                 .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                 .AddEnvironmentVariables();

            Configuration = builder.Build();
            _env = env;
        }

        public void ConfigureServices(IServiceCollection services)
        {
        
            var cns =
             Configuration
                .GetSection("ConfigConnectionString:Default").Value;


            services.AddIdentityServer();
                //.AddSigningCredential(GetRSAParameters())
                //.AddTemporarySigningCredential()
                //.AddInMemoryApiResources(Config.GetApiResources())
                //.AddInMemoryIdentityResources(Config.GetIdentityResources())
                //.AddInMemoryClients(Config.GetClients(Configuration.GetSection("ConfigSettings").Get<ConfigSettingsBase>()));

            //for clarity of the next piece of code
            services.AddScoped<CurrentUser>();
            services.AddTransient<IResourceOwnerPasswordValidator, ResourceOwnerPasswordValidator>();
            services.Configure<ConfigSettingsBase>(Configuration.GetSection("ConfigSettings"));
            services.AddSingleton<IConfiguration>(Configuration);

            services.AddAuthentication()
            .AddGoogle(options =>
            {
                options.ClientId = "857854978384-sv33ngtei50k8fn5ea37rcddo08n0ior.apps.googleusercontent.com";
                options.ClientSecret = "x1SWT89gyn5LLLyMNFxEx_Ss";
            });
            // Add cross-origin resource sharing services Configurations
            Cors.Enable(services);
            services.AddMvc();


        }

        public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory, IOptions<ConfigSettingsBase> configSettingsBase)
        {
            loggerFactory.AddConsole(LogLevel.Debug);
            app.UseDeveloperExceptionPage();

            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();
            loggerFactory.AddFile("Logs/sm-sso-server-api-{Date}.log");

            app.UseCors("AllowAnyOrigin");

            app.UseIdentityServer();
            //app.UseGoogleAuthentication(new GoogleOptions
            //{
            //    AuthenticationScheme = "Google",
            //    SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme,
            //    ClientId = "857854978384-sv33ngtei50k8fn5ea37rcddo08n0ior.apps.googleusercontent.com",
            //    ClientSecret = "x1SWT89gyn5LLLyMNFxEx_Ss"
            //});
            app.UseAuthentication();
            app.UseStaticFiles();
            app.UseMvcWithDefaultRoute();
        }



        private X509Certificate2 GetRSAParameters()
        {
            var fileCert = Path.Combine(_env.ContentRootPath, "pfx", "ids4smbasic.pfx");
            if (!File.Exists(fileCert))
                throw new InvalidOperationException("Certificado não encontrado");

            var password = "vm123s456";
            return new X509Certificate2(fileCert, password, X509KeyStorageFlags.Exportable);
        }
    }
}
