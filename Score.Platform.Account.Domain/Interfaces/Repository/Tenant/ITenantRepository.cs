using Common.Domain.Interfaces;
using Score.Platform.Account.Domain.Entitys;
using Score.Platform.Account.Domain.Filter;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Score.Platform.Account.Domain.Interfaces.Repository
{
    public interface ITenantRepository : IRepository<Tenant>
    {
        IQueryable<Tenant> GetBySimplefilters(TenantFilter filters);

        Task<Tenant> GetById(TenantFilter tenant);
		
        Task<IEnumerable<dynamic>> GetDataItem(TenantFilter filters);

        Task<IEnumerable<dynamic>> GetDataListCustom(TenantFilter filters);

		Task<PaginateResult<dynamic>> GetDataListCustomPaging(TenantFilter filters);

        Task<dynamic> GetDataCustom(TenantFilter filters);
    }
}
