using Score.Platform.Account.Domain.Entitys;
using Score.Platform.Account.Domain.Filter;
using System.Linq;

namespace Score.Platform.Account.Data.Repository
{
    public static class TenantFilterBasicExtension
    {

        public static IQueryable<Tenant> WithBasicFilters(this IQueryable<Tenant> queryBase, TenantFilter filters)
        {
            var queryFilter = queryBase;

			if (filters.Ids.IsSent()) queryFilter = queryFilter.Where(_ => filters.GetIds().Contains(_.TenantId));

            if (filters.TenantId.IsSent()) 
			{ 
				
				queryFilter = queryFilter.Where(_=>_.TenantId == filters.TenantId);
			}
            if (filters.Name.IsSent()) 
			{ 
				
				queryFilter = queryFilter.Where(_=>_.Name.Contains(filters.Name));
			}
            if (filters.Email.IsSent()) 
			{ 
				
				queryFilter = queryFilter.Where(_=>_.Email.Contains(filters.Email));
			}
            if (filters.Password.IsSent()) 
			{ 
				
				queryFilter = queryFilter.Where(_=>_.Password.Contains(filters.Password));
			}
            if (filters.Active.IsSent()) 
			{ 
				
				queryFilter = queryFilter.Where(_=>_.Active == filters.Active);
			}
            if (filters.ProgramId.IsSent()) 
			{ 
				
				queryFilter = queryFilter.Where(_=>_.ProgramId == filters.ProgramId);
			}
            if (filters.GuidResetPassword.IsSent()) 
			{ 
				
				queryFilter = queryFilter.Where(_=>_.GuidResetPassword == filters.GuidResetPassword);
			}
            if (filters.DateResetPassword.IsSent()) 
			{ 
				
				queryFilter = queryFilter.Where(_=>_.DateResetPassword != null && _.DateResetPassword.Value >= filters.DateResetPassword.Value);
			}
            if (filters.DateResetPasswordStart.IsSent()) 
			{ 
				
				queryFilter = queryFilter.Where(_=>_.DateResetPassword != null && _.DateResetPassword.Value >= filters.DateResetPasswordStart.Value);
			}
            if (filters.DateResetPasswordEnd.IsSent()) 
			{ 
				filters.DateResetPasswordEnd = filters.DateResetPasswordEnd.Value.AddDays(1).AddMilliseconds(-1);
				queryFilter = queryFilter.Where(_=>_.DateResetPassword != null &&  _.DateResetPassword.Value <= filters.DateResetPasswordEnd);
			}

            if (filters.ChangePasswordNextLogin.IsSent()) 
			{ 
				
				queryFilter = queryFilter.Where(_=>_.ChangePasswordNextLogin == filters.ChangePasswordNextLogin);
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