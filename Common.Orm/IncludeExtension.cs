using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Common.Orm
{
    public static class IncludeExtension
    {

        public static Expression<Func<T, object>>[] Add<T>(this Expression<Func<T, object>>[] source, params Expression<Func<T, object>>[] includes)
        {
            var newIncludes = source.ToList();
            newIncludes.AddRange(includes.ToList());
            return newIncludes.ToArray();
        }
    }
}
