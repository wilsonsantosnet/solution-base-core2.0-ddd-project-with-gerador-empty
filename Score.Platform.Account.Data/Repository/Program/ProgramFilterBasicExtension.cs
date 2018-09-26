using Score.Platform.Account.Domain.Entitys;
using Score.Platform.Account.Domain.Filter;
using System.Linq;

namespace Score.Platform.Account.Data.Repository
{
    public static class ProgramFilterBasicExtension
    {

        public static IQueryable<Program> WithBasicFilters(this IQueryable<Program> queryBase, ProgramFilter filters)
        {
            var queryFilter = queryBase;

			if (filters.Ids.IsSent()) queryFilter = queryFilter.Where(_ => filters.GetIds().Contains(_.ProgramId));

            if (filters.ProgramId.IsSent()) 
			{ 
				
				queryFilter = queryFilter.Where(_=>_.ProgramId == filters.ProgramId);
			}
            if (filters.Description.IsSent()) 
			{ 
				
				queryFilter = queryFilter.Where(_=>_.Description.Contains(filters.Description));
			}
            if (filters.Datasource.IsSent()) 
			{ 
				
				queryFilter = queryFilter.Where(_=>_.Datasource.Contains(filters.Datasource));
			}
            if (filters.DatabaseName.IsSent()) 
			{ 
				
				queryFilter = queryFilter.Where(_=>_.DatabaseName.Contains(filters.DatabaseName));
			}
            if (filters.ThemaId.IsSent()) 
			{ 
				
				queryFilter = queryFilter.Where(_=>_.ThemaId == filters.ThemaId);
			}
            if (filters.UserCreateId.IsSent()) 
			{ 
				
				queryFilter = queryFilter.Where(_=>_.UserCreateId == filters.UserCreateId);
			}
            if (filters.UserCreateDate.IsSent()) 
			{ 
				
				queryFilter = queryFilter.Where(_=>_.UserCreateDate >= filters.UserCreateDate);
			}
            if (filters.UserCreateDateStart.IsSent()) 
			{ 
				
				queryFilter = queryFilter.Where(_=>_.UserCreateDate >= filters.UserCreateDateStart );
			}
            if (filters.UserCreateDateEnd.IsSent()) 
			{ 
				filters.UserCreateDateEnd = filters.UserCreateDateEnd.AddDays(1).AddMilliseconds(-1);
				queryFilter = queryFilter.Where(_=>_.UserCreateDate  <= filters.UserCreateDateEnd);
			}

            if (filters.UserAlterId.IsSent()) 
			{ 
				
				queryFilter = queryFilter.Where(_=>_.UserAlterId != null && _.UserAlterId.Value == filters.UserAlterId);
			}
            if (filters.UserAlterDate.IsSent()) 
			{ 
				
				queryFilter = queryFilter.Where(_=>_.UserAlterDate != null && _.UserAlterDate.Value >= filters.UserAlterDate.Value);
			}
            if (filters.UserAlterDateStart.IsSent()) 
			{ 
				
				queryFilter = queryFilter.Where(_=>_.UserAlterDate != null && _.UserAlterDate.Value >= filters.UserAlterDateStart.Value);
			}
            if (filters.UserAlterDateEnd.IsSent()) 
			{ 
				filters.UserAlterDateEnd = filters.UserAlterDateEnd.Value.AddDays(1).AddMilliseconds(-1);
				queryFilter = queryFilter.Where(_=>_.UserAlterDate != null &&  _.UserAlterDate.Value <= filters.UserAlterDateEnd);
			}



            return queryFilter;
        }

		
    }
}