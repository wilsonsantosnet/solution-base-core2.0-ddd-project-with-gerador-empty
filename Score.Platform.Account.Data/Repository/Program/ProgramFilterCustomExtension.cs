using Common.Domain.Model;
using Score.Platform.Account.Domain.Entitys;
using Score.Platform.Account.Domain.Filter;
using System.Linq;

namespace Score.Platform.Account.Data.Repository
{
    public static class ProgramFilterCustomExtension
    {

        public static IQueryable<Program> WithCustomFilters(this IQueryable<Program> queryBase, ProgramFilter filters)
        {
            var queryFilter = queryBase;


            return queryFilter;
        }

		public static IQueryable<Program> WithLimitTenant(this IQueryable<Program> queryBase, CurrentUser user)
        {
            var tenantId = user.GetTenantId<int>();
			var queryFilter = queryBase;

            return queryFilter;
        }

    }
}

