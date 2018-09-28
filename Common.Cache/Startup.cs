using Microsoft.Extensions.DependencyInjection;

namespace Common.Cache
{
    public class Startup
    {
        public void ConfigureDevelopmentServices(IServiceCollection services)
        {
            services.AddDistributedMemoryCache();
        }

        public void ConfigureStagingServices(IServiceCollection services)
        {

            services.AddDistributedRedisCache(options =>
            {
                options.Configuration = "localhost";
                options.InstanceName = "SampleInstance";
            });
        }
       
    }
}
