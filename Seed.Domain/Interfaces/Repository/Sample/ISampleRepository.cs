using Common.Domain.Interfaces;
using Seed.Domain.Entitys;
using Seed.Domain.Filter;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Seed.Domain.Interfaces.Repository
{
    public interface ISampleRepository : IRepository<Sample>
    {
        IQueryable<Sample> GetBySimplefilters(SampleFilter filters);

        Task<Sample> GetById(SampleFilter sample);
		
        Task<IEnumerable<dynamic>> GetDataItem(SampleFilter filters);

        Task<IEnumerable<dynamic>> GetDataListCustom(SampleFilter filters);

		Task<PaginateResult<dynamic>> GetDataListCustomPaging(SampleFilter filters);

        Task<dynamic> GetDataCustom(SampleFilter filters);
    }
}
