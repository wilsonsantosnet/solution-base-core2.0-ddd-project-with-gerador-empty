using Microsoft.Extensions.DependencyInjection;
using Seed.HangFire.Interfaces;
using Seed.HangFire.Jobs;

namespace Seed.HangFire.Config
{
    public static partial class ConfigContainer
    {

        public static void Config(IServiceCollection services)
        {
            services.AddScoped<ISchedules<SampleJob>, SampleJob>();
            RegisterOtherComponents(services);
        }
    }
}
