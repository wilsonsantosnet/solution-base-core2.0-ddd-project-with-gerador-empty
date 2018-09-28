using System.Collections.Generic;
using System.Threading.Tasks;

namespace Common.Domain.Interfaces
{
    public interface IBookCopy<T>
    {
        Task<bool> Copy(IEnumerable<T> model, string distinationTableName);
    }
}