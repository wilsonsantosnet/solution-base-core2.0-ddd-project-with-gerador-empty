//using Common.Domain.Base;
//using System;
//using System.Collections.Generic;
//using System.Linq;

//public class PaginatedDataIEnumerable<T> : List<T> where T : class
//{
//    public int PageIndex { get; private set; }
//    public int PageSize { get; private set; }
//    public int TotalCount { get; private set; }
//    public int TotalPages { get; private set; }

//    public PaginatedDataIEnumerable(IEnumerable<T> source, FilterBase filter, int totalCount)
//    {
//        this.Paging(source, filter, totalCount);
//    }
//    public PaginatedDataIEnumerable(IEnumerable<T> source, FilterBase filter)
//    {
//        this.Paging(source, filter, source.Count());
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


//    private void Paging(IEnumerable<T> source, FilterBase filter, int totalCount)
//    {
//        this.PageIndex = filter.PageIndex.IsMoreThanZero() ? filter.PageIndex - 1 : 0;
//        this.PageSize = filter.PageSize;

//        this.TotalCount = totalCount;
//        this.TotalPages = (int)Math.Ceiling(this.TotalCount / (double)this.PageSize);

//        var result = source.Skip(filter.PageSkipped).Take(this.PageSize);
//        this.AddRange(result);
//    }

//}

