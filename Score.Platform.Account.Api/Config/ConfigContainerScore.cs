using Common.Cache;
using Common.Domain.Interfaces;
using Common.Orm;
using Microsoft.Extensions.DependencyInjection;
using Score.Platform.Account.Application;
using Score.Platform.Account.Application.Interfaces;
using Score.Platform.Account.Data.Context;
using Score.Platform.Account.Data.Repository;
using Score.Platform.Account.Domain.Interfaces.Repository;
using Score.Platform.Account.Domain.Interfaces.Services;
using Score.Platform.Account.Domain.Services;

namespace Score.Platform.Account.Api
{
    public static partial class ConfigContainerScore
    {

        public static void Config(IServiceCollection services)
        {
            services.AddScoped<ICache, RedisComponent>();
            services.AddScoped<IUnitOfWork, UnitOfWork<DbContextScore>>();
            
			services.AddScoped<IProgramApplicationService, ProgramApplicationService>();
			services.AddScoped<IProgramService, ProgramService>();
			services.AddScoped<IProgramRepository, ProgramRepository>();

			services.AddScoped<ITenantApplicationService, TenantApplicationService>();
			services.AddScoped<ITenantService, TenantService>();
			services.AddScoped<ITenantRepository, TenantRepository>();

			services.AddScoped<IThemaApplicationService, ThemaApplicationService>();
			services.AddScoped<IThemaService, ThemaService>();
			services.AddScoped<IThemaRepository, ThemaRepository>();



            RegisterOtherComponents(services);
        }
    }
}
