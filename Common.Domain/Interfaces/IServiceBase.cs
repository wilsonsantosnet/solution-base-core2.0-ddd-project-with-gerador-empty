using Common.Domain.Base;
using Common.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Common.Domain.Interfaces
{
    public interface IServiceBase<T,TF>
    {
        Task<T> GetOne(TF filters);

        Task<IEnumerable<T>> GetByFilters(TF filters);

        Task<PaginateResult<T>> GetByFiltersPaging(TF filters);

        Task<T> SavePartial(T entity,  bool questionToContinue = false);

        Task<T> Save(T entity, bool questionToContinue = false);

        Task<IEnumerable<T>> Save(IEnumerable<T> entitys);

        Task<IEnumerable<T>> SavePartial(IEnumerable<T> entitys);

        void Remove(T entity);

        void Remove(IEnumerable<T> entitys);

        Summary GetSummary(PaginateResult<T> paginateResult);

        ConfirmEspecificationResult GetDomainConfirm(FilterBase filters = null);

        WarningSpecificationResult GetDomainWarning(FilterBase filters = null);

        ValidationSpecificationResult GetDomainValidation(FilterBase filters = null);

        void AddDomainValidation(IEnumerable<string> errors);

        void AddDomainValidation(string error);

        void SetTagNameCache(string _tagNameCache);

        string GetTagNameCache();

        Task<T> GetNewInstance(dynamic model, CurrentUser user);

        Task<T> GetUpdateInstance(dynamic model, CurrentUser user);
    }
}
