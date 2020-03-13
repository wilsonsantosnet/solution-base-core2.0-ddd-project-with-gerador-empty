using Common.Cache;
using Common.Domain.Interfaces;
using Common.Orm;
using Microsoft.Extensions.DependencyInjection;
using Seed.Application;
using Seed.Application.Interfaces;
using Seed.Data.Context;
using Seed.Data.Repository;
using Seed.Domain.Interfaces.Repository;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Seed.Domain.Interfaces.Services;
using Seed.Domain.Services;
using Common.Domain.Model;
using Common.Api;

namespace Seed.Api
{
    public static partial class ConfigContainerSeed 
    {

        public static void RegisterOtherComponents(IServiceCollection services)
        {
			services.AddScoped<CurrentUser>();
			services.AddScoped<IStorage, Storage>();
        }
    }
}
