using Common.Domain.Interfaces;
using Score.Platform.Account.Domain.Entitys;
using Score.Platform.Account.Domain.Filter;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Score.Platform.Account.Domain.Interfaces.Repository
{
    public interface IThemaRepository : IRepository<Thema>
    {
        IQueryable<Thema> GetBySimplefilters(ThemaFilter filters);

        Task<Thema> GetById(ThemaFilter thema);
		
        Task<IEnumerable<dynamic>> GetDataItem(ThemaFilter filters);

        Task<IEnumerable<dynamic>> GetDataListCustom(ThemaFilter filters);

		Task<PaginateResult<dynamic>> GetDataListCustomPaging(ThemaFilter filters);

        Task<dynamic> GetDataCustom(ThemaFilter filters);
    }
}
