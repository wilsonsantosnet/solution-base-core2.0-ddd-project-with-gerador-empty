using Common.Domain.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.API.Extensions
{
    public static class SeachResultExtension
    {

        public static SearchResult<T> ToSearchResult<T>(this PaginateResult<T> paginatedResult)
        {

            return new SearchResult<T>()
            {
                DataList = paginatedResult.ResultPaginatedData,
                Summary = new Summary
                {
                    PageSize = paginatedResult.PageSize,
                    Total = paginatedResult.TotalCount
                }
            };

        }

    }
}
