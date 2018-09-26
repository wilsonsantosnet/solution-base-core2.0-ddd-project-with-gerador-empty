using Common.API;
using Common.Domain.Base;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Score.Platform.Account.Application.Config;
using Score.Platform.Account.Data.Context;
using System.Globalization;
using Microsoft.AspNetCore.Localization;
using Common.API.Extensions;
using System.Collections.Generic;
using Score.Platform.Account.CrossCuting;
using Swashbuckle.AspNetCore.Swagger;
using Microsoft.Extensions.PlatformAbstractions;
using System.IO;

namespace Score.Platform.Account.Api
{
    public class Startup
    {
		private readonly IHostingEnvironment _env;

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
			this._env = env;
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
			//Camelcase para json
            services.AddMvc()
			.AddJsonOptions(options =>
			{
				options.SerializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver();
			});

            services.AddDbContext<DbContextScore>(
             options => options.UseSqlServer(
                 Configuration
                    .GetSection("ConfigConnectionString:Default").Value));

            services.AddDistributedRedisCache(options =>
            {
                options.Configuration = Configuration.GetSection("RedisConnStrings:Score").Value;
                options.InstanceName = "Score";
            });

            services.Configure<ConfigSettingsBase>(Configuration.GetSection("ConfigSettings"));
            services.AddSingleton<IConfiguration>(Configuration);
			services.AddSingleton(new EnviromentInfo
            {
                RootPath = this._env.ContentRootPath
            });

			// Config AuthorityEndPoint SSO
			 services.AddAuthentication("Bearer")
			.AddIdentityServerAuthentication(options =>
			{
				options.Authority = Configuration.GetSection("ConfigSettings:AuthorityEndPoint").Value;
				options.RequireHttpsMetadata = false;
				options.ApiName = "ssosa";
			});


            // Add cross-origin resource sharing services Configurations
            Cors.Enable(services);

            // Add application services.
            ConfigContainerScore.Config(services);			

            // Add framework services.
            services.AddMvc(options => { options.ModelBinderProviders.Insert(0, new DateTimePtBrModelBinderProvider()); })
            .AddJsonOptions(options =>
            {
                options.SerializerSettings.Converters.Add(new DateTimePtBrConverter());
            });

			//Policys
            services.AddAuthorizationPolicy(ProfileCustom.Define);

            // Configurando o serviço de documentação do Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1",
                    new Info
                    {
                        Title = "Score",
                        Version = "v1",
                        Description = "Score",
                        Contact = new Contact
                        {
                            Name = "Wilson Santos",
                            Url = "http://targetsoftware.com.br"
                        },

                    });

                var caminhoAplicacao = PlatformServices.Default.Application.ApplicationBasePath;
                var nomeAplicacao = PlatformServices.Default.Application.ApplicationName;
                var caminhoXmlDoc = Path.Combine(caminhoAplicacao, $"{nomeAplicacao}.xml");
                c.IncludeXmlComments(caminhoXmlDoc);

                c.AddSecurityDefinition("oauth2", new OAuth2Scheme
                {
                    Type = "oauth2",
                    Flow = "implicit",
                    AuthorizationUrl = "http://localhost:4000/connect/authorize",
                    Scopes = new Dictionary<string, string>
                    {
                        { "ssosa", "ssosa" },
                    }
                });

                c.OperationFilter<AuthorizeCheckOperationFilter>();

            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, IOptions<ConfigSettingsBase> configSettingsBase)
        {

			loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseDeveloperExceptionPage();

            var supportedCultures = new[]
            {
                new CultureInfo("pt-BR"),
            };

            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture(culture: "pt-BR", uiCulture: "pt-BR"),
                // Formatting numbers, dates, etc.
                SupportedCultures = supportedCultures,
                // UI strings that we have localized.
                SupportedUICultures = supportedCultures
            });
        	
			app.UseAuthentication();
            app.AddTokenMiddlewareCustom();
            app.UseMvc();

            //Ativando middlewares para uso do Swagger
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Score");
                c.OAuthClientId("swagger-dash");
                c.OAuthAppName("swagger Dashboard");

            });

            app.UseCors("AllowAnyOrigin");
            AutoMapperConfigScore.RegisterMappings();
        }

    }
}
