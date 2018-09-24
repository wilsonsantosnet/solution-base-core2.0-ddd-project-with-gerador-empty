using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Seed.HangFire.Config
{
    public static partial class ConfigContainer
    {

        public static void Config(IServiceCollection services)
        {

            RegisterOtherComponents(services);
        }
    }
}
