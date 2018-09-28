using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class PaginateResult<T>
{

    public IEnumerable<T> ResultPaginatedData { get; set; }

    public IQueryable<T> Source { get; set; }

    public int TotalCount { get; set; }
    
    public int PageSize { get; set; }

}

