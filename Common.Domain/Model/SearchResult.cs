using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Domain.Model
{
    public class SearchResult<T>
    {
        public IEnumerable<T> DataList { get; set; }

        public Summary Summary { get; set; }

    }
}
