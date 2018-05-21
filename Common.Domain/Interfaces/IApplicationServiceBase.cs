using Common.Domain.Base;
using Common.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Common.Domain.Interfaces
{
    public interface IApplicationServiceBase
    {
        WarningSpecificationResult GetDomainWarning(FilterBase filters = null);

        ConfirmEspecificationResult GetDomainConfirm(FilterBase filters = null);

        ValidationSpecificationResult GetDomainValidation(FilterBase filters = null);
    }
    public interface IApplicationServiceBase<T> : IApplicationServiceBase
    {
        Task<T> GetOne(FilterBase filters);

        Task<SearchResult<T>> GetByFilters(FilterBase filters);

        Task<T> Save(T entity, bool questionToContinue = false);

        Task<T> SavePartial(T entity, bool questionToContinue = false);

        Task<IEnumerable<T>> SavePartial(IEnumerable<T> entitys);

        Task<IEnumerable<T>> Save(IEnumerable<T> entitys);

        Task<int> Remove(T entity);




    }
}
