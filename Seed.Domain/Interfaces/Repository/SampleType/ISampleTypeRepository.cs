using Common.Domain.Interfaces;
using Seed.Domain.Entitys;
using Seed.Domain.Filter;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Seed.Domain.Interfaces.Repository
{
    public interface ISampleTypeRepository : IRepository<SampleType>
    {
        IQueryable<SampleType> GetBySimplefilters(SampleTypeFilter filters);

        Task<SampleType> GetById(SampleTypeFilter sampletype);
		
        Task<IEnumerable<dynamic>> GetDataItem(SampleTypeFilter filters);

        Task<IEnumerable<dynamic>> GetDataListCustom(SampleTypeFilter filters);

		Task<PaginateResult<dynamic>> GetDataListCustomPaging(SampleTypeFilter filters);

        Task<dynamic> GetDataCustom(SampleTypeFilter filters);
    }
}
