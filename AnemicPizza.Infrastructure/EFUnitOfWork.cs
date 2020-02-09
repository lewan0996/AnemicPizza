using AnemicPizza.Core;
using Microsoft.EntityFrameworkCore.Storage;

namespace AnemicPizza.Infrastructure
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private IDbContextTransaction _transaction;
        private bool _shouldRollback;
        private bool _disposed;

        public EFUnitOfWork(AppDbContext context)
        {
            _context = context;
            _shouldRollback = true;
        }
        public void Dispose()
        {
            if (_disposed)
            {
                return;
            }

            if (_shouldRollback)
            {
                _transaction?.Rollback();
            }
            else
            {
                _transaction.Commit();
            }

            _transaction?.Dispose();
            _context.Dispose();

            _disposed = true;
        }

        public void BeginTransaction()
        {
            _transaction = _context.Database.BeginTransaction();
        }

        public void Commit()
        {
            _shouldRollback = true;
            _context.SaveChanges();
            _shouldRollback = false;
        }
    }
}
