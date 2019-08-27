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
using System.Linq;
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
                //.AddInMemoryApiResources(Config.GetApiResources())
                //.AddInMemoryIdentityResources(Config.GetIdentityResources())
                //.AddInMemoryClients(Config.GetClients(Configuration.GetSection("ConfigSettings").Get<ConfigSettingsBase>()));

            //Configurations
            services.Configure<ConfigSettingsBase>(Configuration.GetSection("ConfigSettings"));
            services.Configure<ConfigConnectionStringBase>(Configuration.GetSection("ConfigConnectionString"));
            //Container DI
            services.AddScoped<CurrentUser>();
            services.AddTransient<IUserCredentialServices, UserCredentialServices>();
            services.AddTransient<IResourceOwnerPasswordValidator, ResourceOwnerPasswordValidator>();
            services.AddSingleton<IConfiguration>(Configuration);
            services.AddLogging(loggingBuilder =>
            {
                loggingBuilder.AddConfiguration(Configuration.GetSection("Logging"));
                loggingBuilder.AddConsole();
                loggingBuilder.AddDebug();
            });

            services.AddAuthentication()
            .AddGoogle(options =>
            {
                options.ClientId = "857854978384-sv33ngtei50k8fn5ea37rcddo08n0ior.apps.googleusercontent.com";
                options.ClientSecret = "x1SWT89gyn5LLLyMNFxEx_Ss";
            });
            // Add cross-origin resource sharing services Configurations
            var sp = services.BuildServiceProvider();
            var configuration = sp.GetService<IOptions<ConfigSettingsBase>>();
            Cors.Enable(services, configuration.Value.ClientAuthorityEndPoint.ToArray());

            services.AddMvc();


        }

        public void Configure(IApplicationBuilder app,IHostingEnvironment env, ILoggerFactory loggerFactory, IOptions<ConfigSettingsBase> configSettingsBase)
        {

            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            loggerFactory.AddFile(Configuration.GetSection("Logging"));

            app.UseCors("AllowStackOrigin");
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
