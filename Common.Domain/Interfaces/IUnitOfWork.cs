using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Common.Domain.Interfaces
{
    public interface IUnitOfWork
    {

        void BeginTransaction();

        int Commit();

        Task<int> CommitAsync();

    }
}
