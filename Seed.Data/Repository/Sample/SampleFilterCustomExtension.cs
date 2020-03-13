using Common.Domain.Model;
using Seed.Domain.Entitys;
using Seed.Domain.Filter;
using System.Linq;

namespace Seed.Data.Repository
{
    public static class SampleFilterCustomExtension
    {

        public static IQueryable<Sample> WithCustomFilters(this IQueryable<Sample> queryBase, SampleFilter filters)
        {
            var queryFilter = queryBase;


            return queryFilter;
        }

		public static IQueryable<Sample> WithLimitTenant(this IQueryable<Sample> queryBase, CurrentUser user)
        {
            var tenantId = user.GetTenantId<int>();
			var queryFilter = queryBase;

            return queryFilter;
        }

    }
}

