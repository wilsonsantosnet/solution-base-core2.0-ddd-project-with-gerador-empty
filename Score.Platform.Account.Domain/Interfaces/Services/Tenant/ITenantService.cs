using Common.Domain.Interfaces;
using Score.Platform.Account.Domain.Entitys;
using Score.Platform.Account.Domain.Filter;

namespace Score.Platform.Account.Domain.Interfaces.Services
{
    public interface ITenantService : IServiceBase<Tenant, TenantFilter>
    {

        
    }
}
