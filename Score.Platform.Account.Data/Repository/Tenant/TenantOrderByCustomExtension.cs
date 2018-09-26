using Common.Domain.Model;
using Score.Platform.Account.Domain.Entitys;
using Score.Platform.Account.Domain.Filter;
using System.Linq;

namespace Score.Platform.Account.Data.Repository
{
    public static class TenantOrderCustomExtension
    {

        public static IQueryable<Tenant> OrderByDomain(this IQueryable<Tenant> queryBase, TenantFilter filters)
        {
            return queryBase.OrderBy(_ => _.TenantId);
        }

    }
}

