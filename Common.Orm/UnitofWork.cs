using Common.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Threading.Tasks;

namespace Common.Orm
{
    public class UnitOfWork<T> : IUnitOfWork
    {

        private DbContext _ctx;
        private bool _disposed;
        private IDbContextTransaction _transaction;

        public UnitOfWork(T _ctx)
        {
            this._ctx = _ctx as DbContext;
        }

        public void BeginTransaction()
        {
            this._transaction = this._ctx.Database.BeginTransaction();
            _disposed = false;
        }

        public int Commit()
        {
            var result = this._ctx.SaveChanges();
            this._transaction.Commit();
            return result;
        }

        public async Task<int> CommitAsync()
        {
            var result = await this._ctx.SaveChangesAsync();
            this._transaction.Commit();
            return result;
        }

        protected virtual void Dispose(bool disposing)
        {

            if (!_disposed)
            {
                if (disposing)
                {
                    _ctx.Dispose();
                }
            }

        }

    }
}
