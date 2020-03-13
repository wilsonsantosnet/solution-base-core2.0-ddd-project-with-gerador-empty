using Common.Domain.Model;
using Seed.Domain.Entitys;
using Seed.Domain.Filter;
using System.Linq;

namespace Seed.Data.Repository
{
    public static class SampleTypeOrderCustomExtension
    {

        public static IQueryable<SampleType> OrderByDomain(this IQueryable<SampleType> queryBase, SampleTypeFilter filters)
        {
            return queryBase.OrderBy(_ => _.SampleTypeId);
        }

    }
}

