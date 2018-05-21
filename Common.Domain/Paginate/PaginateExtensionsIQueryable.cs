//using Common.Domain;
//using Common.Domain.Base;
//using Common.Domain.Interfaces;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;


//public static class PaginateExtensionsIQueryable
//{
//    public static PaginateResult<T> PaginateNew<T>(this IQueryable<T> querySorted, IQueryable<T> query, FilterBase filter, IRepository<T> rep, int totalCount = 0) where T : class
//    {
//        var paginationResult = new PaginatedDataIQueryable<T>(querySorted, filter, rep, totalCount);

//        return new PaginateResult<T>
//        {
//            ResultPaginatedData = paginationResult,
//            TotalCount = paginationResult.TotalCount,
//            Source = query
//        };
//    }

//    public static PaginateResult<T> PaginateNew<T>(this IQueryable<T> source, FilterBase filter, IRepository<T> rep) where T : class
//    {
//        var paginationResult = new PaginatedDataIQueryable<T>(source, filter, rep);

//        return new PaginateResult<T>
//        {
//            ResultPaginatedData = paginationResult,
//            TotalCount = paginationResult.TotalCount,
//            Source = source
//        };
//    }

//    public static IEnumerable<T> Paginate<T>(this IQueryable<T> source, FilterBase filter, IRepository<T> rep) where T : class
//    {
//        var paginationResult = new PaginatedDataIQueryable<T>(source, filter, rep);
//        return paginationResult;
//    }

//    public static async Task<PaginateResult<T>> Paging<T>(this IQueryable<T> querySorted, FilterBase filter, IRepository<T> rep) where T : class
//    {
//        return await querySorted.Paging(filter, querySorted, rep);
//    }

//    public static async Task<PaginateResult<T>> Paging<T>(this IQueryable<T> querySorted, FilterBase filter, IQueryable<T> query, IRepository<T> rep) where T : class
//    {
//        var totalCount = await rep.CountAsync(query);
//        if (totalCount <= filter.PageSize)
//        {
//            return new PaginateResult<T>
//            {
//                ResultPaginatedData = querySorted,
//                TotalCount = totalCount,
//                Source = query
//            };
//        }

//        if (filter.IsPagination)
//            return querySorted.PaginateNew(querySorted, filter, rep, totalCount);

//        return new PaginateResult<T>
//        {
//            ResultPaginatedData = querySorted,
//            TotalCount = totalCount,
//            Source = query
//        };
//    }


//}








