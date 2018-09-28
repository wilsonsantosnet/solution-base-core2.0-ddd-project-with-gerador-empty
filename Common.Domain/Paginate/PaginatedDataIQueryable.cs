//using Common.Domain.Base;
//using Common.Domain.Interfaces;
//using System;
//using System.Collections.Generic;
//using System.Linq;

//public class PaginatedDataIQueryable<T> : List<T> where T : class
//{
//    public int PageIndex { get; private set; }
//    public int PageSize { get; private set; }
//    public int TotalCount { get; private set; }
//    public int TotalPages { get; private set; }

//    public PaginatedDataIQueryable(IQueryable<T> source, FilterBase filter, IRepository<T> rep, int totalCount)
//    {
//        this.Paging(source, filter, rep, totalCount);
//    }
//    public PaginatedDataIQueryable(IQueryable<T> source, FilterBase filter, IRepository<T> rep)
//    {
//        this.Paging(source, filter, rep);
//    }

//    public bool HasPreviousPage
//    {
//        get
//        {
//            return (PageIndex > 0);
//        }
//    }

//    public bool HasNextPage
//    {
//        get
//        {
//            return (PageIndex + 1 < TotalPages);
//        }
//    }

//    private async void Paging(IQueryable<T> source, FilterBase filter, IRepository<T> rep)
//    {
//        var count = await rep.CountAsync(source);
//        Paging(source, filter, rep, count);
//    }
//    private async void Paging(IQueryable<T> source, FilterBase filter, IRepository<T> rep, int totalCount)
//    {
//        this.PageIndex = filter.PageIndex.IsMoreThanZero() ? filter.PageIndex - 1 : 0;
//        this.PageSize = filter.PageSize;

//        this.TotalCount = totalCount;
//        this.TotalPages = (int)Math.Ceiling(this.TotalCount / (double)this.PageSize);

//        var result = await rep.ToListAsync(source.Skip(filter.PageSkipped).Take(this.PageSize));
//        this.AddRange(result);
//    }

//}

