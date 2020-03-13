using Common.Domain.Model;
using Seed.Domain.Entitys;
using Seed.Domain.Filter;
using System.Linq;

namespace Seed.Data.Repository
{
    public static class SampleTypeFilterCustomExtension
    {

        public static IQueryable<SampleType> WithCustomFilters(this IQueryable<SampleType> queryBase, SampleTypeFilter filters)
        {
            var queryFilter = queryBase;


            return queryFilter;
        }

		public static IQueryable<SampleType> WithLimitTenant(this IQueryable<SampleType> queryBase, CurrentUser user)
        {
            var tenantId = user.GetTenantId<int>();
			var queryFilter = queryBase;

            return queryFilter;
        }

    }
}

