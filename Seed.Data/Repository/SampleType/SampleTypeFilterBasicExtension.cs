using Seed.Domain.Entitys;
using Seed.Domain.Filter;
using System.Linq;

namespace Seed.Data.Repository
{
    public static class SampleTypeFilterBasicExtension
    {

        public static IQueryable<SampleType> WithBasicFilters(this IQueryable<SampleType> queryBase, SampleTypeFilter filters)
        {
            var queryFilter = queryBase;

			if (filters.Ids.IsSent()) queryFilter = queryFilter.Where(_ => filters.GetIds().Contains(_.SampleTypeId));

            if (filters.SampleTypeId.IsSent()) 
			{ 
				
				queryFilter = queryFilter.Where(_=>_.SampleTypeId == filters.SampleTypeId);
			}
            if (filters.Name.IsSent()) 
			{ 
				
				queryFilter = queryFilter.Where(_=>_.Name.Contains(filters.Name));
			}


            return queryFilter;
        }

		
    }
}