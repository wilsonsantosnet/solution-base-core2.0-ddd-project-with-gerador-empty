using Common.Domain.Model;
using Score.Platform.Account.Domain.Entitys;
using Score.Platform.Account.Domain.Filter;
using System.Linq;

namespace Score.Platform.Account.Data.Repository
{
    public static class ThemaFilterCustomExtension
    {

        public static IQueryable<Thema> WithCustomFilters(this IQueryable<Thema> queryBase, ThemaFilter filters)
        {
            var queryFilter = queryBase;


            return queryFilter;
        }

		public static IQueryable<Thema> WithLimitTenant(this IQueryable<Thema> queryBase, CurrentUser user)
        {
            var tenantId = user.GetTenantId<int>();
			var queryFilter = queryBase;

            return queryFilter;
        }

    }
}

