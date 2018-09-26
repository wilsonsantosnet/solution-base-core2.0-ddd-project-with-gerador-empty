using Common.Domain.Model;
using Score.Platform.Account.Domain.Entitys;
using Score.Platform.Account.Domain.Filter;
using System.Linq;

namespace Score.Platform.Account.Data.Repository
{
    public static class ProgramOrderCustomExtension
    {

        public static IQueryable<Program> OrderByDomain(this IQueryable<Program> queryBase, ProgramFilter filters)
        {
            return queryBase.OrderBy(_ => _.ProgramId);
        }

    }
}

