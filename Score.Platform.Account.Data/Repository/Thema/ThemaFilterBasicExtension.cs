using Score.Platform.Account.Domain.Entitys;
using Score.Platform.Account.Domain.Filter;
using System.Linq;

namespace Score.Platform.Account.Data.Repository
{
    public static class ThemaFilterBasicExtension
    {

        public static IQueryable<Thema> WithBasicFilters(this IQueryable<Thema> queryBase, ThemaFilter filters)
        {
            var queryFilter = queryBase;

			if (filters.Ids.IsSent()) queryFilter = queryFilter.Where(_ => filters.GetIds().Contains(_.ThemaId));

            if (filters.ThemaId.IsSent()) 
			{ 
				
				queryFilter = queryFilter.Where(_=>_.ThemaId == filters.ThemaId);
			}
            if (filters.Name.IsSent()) 
			{ 
				
				queryFilter = queryFilter.Where(_=>_.Name.Contains(filters.Name));
			}
            if (filters.Description.IsSent()) 
			{ 
				
				queryFilter = queryFilter.Where(_=>_.Description.Contains(filters.Description));
			}


            return queryFilter;
        }

		
    }
}