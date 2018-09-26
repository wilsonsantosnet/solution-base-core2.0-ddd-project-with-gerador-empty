using Common.Domain.Interfaces;
using Common.Domain.Model;
using Score.Platform.Account.Domain.Entitys;
using Score.Platform.Account.Domain.Interfaces.Repository;
using Score.Platform.Account.Domain.Interfaces.Services;

namespace Score.Platform.Account.Domain.Services
{
    public class TenantService : TenantServiceBase, ITenantService
    {

        public TenantService(ITenantRepository rep, ICache cache, CurrentUser user) 
            : base(rep, cache, user)
        {


        }
        
    }
}
