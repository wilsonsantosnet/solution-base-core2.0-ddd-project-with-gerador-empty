using Common.Domain.Model;
using Seed.Domain.Entitys;
using Seed.Domain.Filter;
using System.Linq;

namespace Seed.Data.Repository
{
    public static class SampleOrderCustomExtension
    {

        public static IQueryable<Sample> OrderByDomain(this IQueryable<Sample> queryBase, SampleFilter filters)
        {
            return queryBase.OrderBy(_ => _.SampleId);
        }

    }
}

