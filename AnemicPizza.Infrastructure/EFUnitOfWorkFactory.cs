using AnemicPizza.Core;

namespace AnemicPizza.Infrastructure
{
    public class EFUnitOfWorkFactory : IUnitOfWorkFactory
    {
        private readonly AppDbContext _context;

        public EFUnitOfWorkFactory(AppDbContext context)
        {
            _context = context;
        }
        public IUnitOfWork Create()
        {
            var uow = new EFUnitOfWork(_context);
            uow.BeginTransaction();
            return uow;
        }
    }
}
