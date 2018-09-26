using Common.Domain.Model;
using Score.Platform.Account.Domain.Entitys;
using Score.Platform.Account.Domain.Filter;
using System.Linq;

namespace Score.Platform.Account.Data.Repository
{
    public static class ThemaOrderCustomExtension
    {

        public static IQueryable<Thema> OrderByDomain(this IQueryable<Thema> queryBase, ThemaFilter filters)
        {
            return queryBase.OrderBy(_ => _.ThemaId);
        }

    }
}

