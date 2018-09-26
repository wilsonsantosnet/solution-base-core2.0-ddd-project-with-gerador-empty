using Common.Domain.Interfaces;
using Score.Platform.Account.Domain.Entitys;
using Score.Platform.Account.Domain.Filter;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Score.Platform.Account.Domain.Interfaces.Repository
{
    public interface IProgramRepository : IRepository<Program>
    {
        IQueryable<Program> GetBySimplefilters(ProgramFilter filters);

        Task<Program> GetById(ProgramFilter program);
		
        Task<IEnumerable<dynamic>> GetDataItem(ProgramFilter filters);

        Task<IEnumerable<dynamic>> GetDataListCustom(ProgramFilter filters);

		Task<PaginateResult<dynamic>> GetDataListCustomPaging(ProgramFilter filters);

        Task<dynamic> GetDataCustom(ProgramFilter filters);
    }
}
