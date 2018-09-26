using Hangfire.Dashboard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Score.Platform.Account.HangFire.Config
{
    public class AuthoritionDashboardFilter : IDashboardAuthorizationFilter
    {

        public bool Authorize(DashboardContext context)
        {
            var httpContext = context.GetHttpContext();

            // Allow all authenticated users to see the Dashboard (potentially dangerous).
            return httpContext.User.Identity.IsAuthenticated;
            //return true;
        }
    }
}
