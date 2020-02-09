using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AnemicPizza.Core.Models.Ordering;
using AnemicPizza.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace AnemicPizza.Infrastructure.Repositories
{
    public class SupplierRepository : EFRepository<Supplier>, ISupplierRepository
    {
        public SupplierRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public Task<Supplier> GetFirstFreeSupplier() // todo https://docs.microsoft.com/pl-pl/ef/core/saving/concurrency
        {
            return DbContext.Suppliers.FirstOrDefaultAsync(s => s.Status == SupplierStatus.Free);
        }

        public async Task<IReadOnlyList<Order>> GetSupplierOrdersAsync(int supplierId)
        {
            return await DbContext.Orders.Where(o => o.SupplierId == supplierId).ToListAsync();
        }
    }
}
