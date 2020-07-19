using Common.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Common.Domain.Interfaces
{
    public interface IRepositoryExtensions<TEntity, TFilter>
    {

        IQueryable<TEntity> GetBySimplefilters(TFilter filters);

        Task<TEntity> GetById(TFilter company);

        Task<IEnumerable<dynamic>> GetDataItem(TFilter filters);

        Task<IEnumerable<dynamic>> GetDataListCustom(TFilter filters);

        Task<PaginateResult<dynamic>> GetDataListCustomPaging(TFilter filters);

        Task<dynamic> GetDataCustom(TFilter filters);

    }
    public interface IRepository<T>
    {
        IQueryable<T> GetAll();
        IQueryable<T> GetAll(params Expression<Func<T, object>>[] includes);
        IQueryable<T> GetAllAsTracking(params Expression<Func<T, object>>[] includes);
        Task<PaginateResult<T>> PagingAndDefineFields(FilterBase filters, IQueryable<T> queryFilter);
        T Add(T entity);
        T Update(T entity);
        void DetachLocal(Func<T,bool> predicate);
        IEnumerable<T> Update(IEnumerable<T> entitys);
        void Remove(T entity);
        void RemoveRangeAndCommit(IEnumerable<T> entitys);
        void RemoveRange(IEnumerable<T> entitys);
        Task<List<T2>> ToListAsync<T2>(IQueryable<T2> source);
        Task<int> CountAsync<T2>(IQueryable<T2> source);
        Task<decimal> SumAsync<T2>(IQueryable<T2> source, Expression<Func<T2, decimal>> selector);
        Task<T2> SingleOrDefaultAsync<T2>(IQueryable<T2> source);
        Task<T2> FirstOrDefaultAsync<T2>(IQueryable<T2> source);
        Task<int> CommitAsync();

    }
}
