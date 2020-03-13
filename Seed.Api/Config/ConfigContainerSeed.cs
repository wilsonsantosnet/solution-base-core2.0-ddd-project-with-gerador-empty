using Common.Cache;
using Common.Domain.Interfaces;
using Common.Orm;
using Microsoft.Extensions.DependencyInjection;
using Seed.Application;
using Seed.Application.Interfaces;
using Seed.Data.Context;
using Seed.Data.Repository;
using Seed.Domain.Interfaces.Repository;
using Seed.Domain.Interfaces.Services;
using Seed.Domain.Services;

namespace Seed.Api
{
    public static partial class ConfigContainerSeed
    {

        public static void Config(IServiceCollection services)
        {
            services.AddScoped<ICache, RedisComponent>();
            services.AddScoped<IUnitOfWork, UnitOfWork<DbContextSeed>>();
            
			services.AddScoped<ISampleApplicationService, SampleApplicationService>();
			services.AddScoped<ISampleService, SampleService>();
			services.AddScoped<ISampleRepository, SampleRepository>();

			services.AddScoped<ISampleTypeApplicationService, SampleTypeApplicationService>();
			services.AddScoped<ISampleTypeService, SampleTypeService>();
			services.AddScoped<ISampleTypeRepository, SampleTypeRepository>();



            RegisterOtherComponents(services);
        }
    }
}
