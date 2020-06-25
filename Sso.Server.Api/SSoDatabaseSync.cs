using Common.Domain.Base;
using Common.Domain.Interfaces;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;


namespace Sso.Server.Api
{
    public static class SSoDatabaseSync
    {
        public static void InitializeDatabase(IApplicationBuilder app,
            ConfigSettingsBase settings, 
            ICache cache, 
            IHostingEnvironment env)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                serviceScope.ServiceProvider.GetRequiredService<PersistedGrantDbContext>();
                var context = serviceScope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();
                context.Database.Migrate();

                Update(settings, cache, env, context);

            }
        }

        public static void Update(ConfigSettingsBase settings, ICache cache, IHostingEnvironment env, ConfigurationDbContext context)
        {
            
            foreach (var client in Config.GetClients(settings))
            {
                if (context.Clients.Where(_ => _.ClientId == client.ClientId).IsNotAny())
                    context.Clients.Add(client.ToEntity());

            }
            context.SaveChanges();

            foreach (var ir in Config.GetIdentityResources())
            {
                if (context.IdentityResources.Where(_ => _.Name == ir.Name).IsNotAny())
                    context.IdentityResources.Add(ir.ToEntity());

            }
            context.SaveChanges();

            foreach (var ar in Config.GetApiResources())
            {
                if (context.ApiResources.Where(_ => _.Name == ar.Name).IsNotAny())
                    context.ApiResources.Add(ar.ToEntity());

            }
            context.SaveChanges();
        }

       
    }
}
