using System.Threading.Tasks;
using AnemicPizza.Core.Models.Ordering;
using Microsoft.EntityFrameworkCore;

namespace AnemicPizza.Infrastructure
{
    public class SupplierRepository : EFRepository<Supplier>
    {
        public SupplierRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public Task<Supplier> GetFirstFreeSupplier() // todo https://docs.microsoft.com/pl-pl/ef/core/saving/concurrency
        {
            return DbContext.Suppliers.FirstOrDefaultAsync(s => s.Status == SupplierStatus.Free);
        }
    }
}
