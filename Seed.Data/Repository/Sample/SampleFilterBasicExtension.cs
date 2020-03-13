using Seed.Domain.Entitys;
using Seed.Domain.Filter;
using System.Linq;

namespace Seed.Data.Repository
{
    public static class SampleFilterBasicExtension
    {

        public static IQueryable<Sample> WithBasicFilters(this IQueryable<Sample> queryBase, SampleFilter filters)
        {
            var queryFilter = queryBase;

			if (filters.Ids.IsSent()) queryFilter = queryFilter.Where(_ => filters.GetIds().Contains(_.SampleId));

            if (filters.SampleId.IsSent()) 
			{ 
				
				queryFilter = queryFilter.Where(_=>_.SampleId == filters.SampleId);
			}
            if (filters.Name.IsSent()) 
			{ 
				
				queryFilter = queryFilter.Where(_=>_.Name.Contains(filters.Name));
			}
            if (filters.Descricao.IsSent()) 
			{ 
				
				queryFilter = queryFilter.Where(_=>_.Descricao.Contains(filters.Descricao));
			}
            if (filters.SampleTypeId.IsSent()) 
			{ 
				
				queryFilter = queryFilter.Where(_=>_.SampleTypeId == filters.SampleTypeId);
			}
            if (filters.Ativo.IsSent()) 
			{ 
				
				queryFilter = queryFilter.Where(_=>_.Ativo != null && _.Ativo.Value == filters.Ativo);
			}
            if (filters.Age.IsSent()) 
			{ 
				
				queryFilter = queryFilter.Where(_=>_.Age != null && _.Age.Value == filters.Age);
			}
            if (filters.Category.IsSent()) 
			{ 
				
				queryFilter = queryFilter.Where(_=>_.Category != null && _.Category.Value == filters.Category);
			}
            if (filters.Datetime.IsSent()) 
			{ 
				
				queryFilter = queryFilter.Where(_=> _.Datetime != null && _.Datetime >= filters.Datetime.Value.AddHours(-filters.Datetime.Value.Hour).AddMinutes(-filters.Datetime.Value.Minute).AddSeconds(-filters.Datetime.Value.Second) && _.Datetime <= filters.Datetime.Value.AddDays(1).AddHours(-filters.Datetime.Value.Hour).AddMinutes(-filters.Datetime.Value.Minute).AddSeconds(-filters.Datetime.Value.Second));
			}
            if (filters.DatetimeStart.IsSent()) 
			{ 
				
				queryFilter = queryFilter.Where(_=>_.Datetime != null && _.Datetime.Value >= filters.DatetimeStart.Value);
			}
            if (filters.DatetimeEnd.IsSent()) 
			{ 
				filters.DatetimeEnd = filters.DatetimeEnd.Value.AddDays(1).AddMilliseconds(-1);
				queryFilter = queryFilter.Where(_=>_.Datetime != null &&  _.Datetime.Value <= filters.DatetimeEnd);
			}

            if (filters.Tags.IsSent()) 
			{ 
				
				queryFilter = queryFilter.Where(_=>_.Tags.Contains(filters.Tags));
			}
            if (filters.UserCreateId.IsSent()) 
			{ 
				
				queryFilter = queryFilter.Where(_=>_.UserCreateId == filters.UserCreateId);
			}
            if (filters.UserCreateDate.IsSent()) 
			{ 
				
				queryFilter = queryFilter.Where(_=> _.UserCreateDate >= filters.UserCreateDate.AddHours(-filters.UserCreateDate.Hour).AddMinutes(-filters.UserCreateDate.Minute).AddSeconds(-filters.UserCreateDate.Second) && _.UserCreateDate <= filters.UserCreateDate.AddDays(1).AddHours(-filters.UserCreateDate.Hour).AddMinutes(-filters.UserCreateDate.Minute).AddSeconds(-filters.UserCreateDate.Second));
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
				
				queryFilter = queryFilter.Where(_=> _.UserAlterDate != null && _.UserAlterDate >= filters.UserAlterDate.Value.AddHours(-filters.UserAlterDate.Value.Hour).AddMinutes(-filters.UserAlterDate.Value.Minute).AddSeconds(-filters.UserAlterDate.Value.Second) && _.UserAlterDate <= filters.UserAlterDate.Value.AddDays(1).AddHours(-filters.UserAlterDate.Value.Hour).AddMinutes(-filters.UserAlterDate.Value.Minute).AddSeconds(-filters.UserAlterDate.Value.Second));
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