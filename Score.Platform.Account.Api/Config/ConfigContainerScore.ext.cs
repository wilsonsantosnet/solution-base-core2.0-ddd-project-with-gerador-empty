using Common.Cache;
using Common.Domain.Interfaces;
using Common.Orm;
using Microsoft.Extensions.DependencyInjection;
using Score.Platform.Account.Application;
using Score.Platform.Account.Application.Interfaces;
using Score.Platform.Account.Data.Context;
using Score.Platform.Account.Data.Repository;
using Score.Platform.Account.Domain.Interfaces.Repository;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Score.Platform.Account.Domain.Interfaces.Services;
using Score.Platform.Account.Domain.Services;
using Common.Domain.Model;

namespace Score.Platform.Account.Api
{
    public static partial class ConfigContainerScore 
    {

        public static void RegisterOtherComponents(IServiceCollection services)
        {
			services.AddScoped<CurrentUser>();
        }
    }
}
