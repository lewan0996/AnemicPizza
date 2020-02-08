using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AnemicPizza.Core;
using AnemicPizza.Core.Models.Ordering;
using Microsoft.EntityFrameworkCore;

namespace AnemicPizza.Infrastructure.Repositories
{
    public class OrderRepository : EFRepository<Order>, IOrderRepository
    {
        public OrderRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public override Task<Order> GetByIdAsync(int id)
        {
            return DbContext.Orders
                .Include(o => o.Items)
                .Include(o => o.Supplier)
                .FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task<IReadOnlyList<Order>> GetOrdersByUserEmailAsync(string email)
        {
            return await DbContext.Orders
                .Where(o => o.Client.EmailAddress == email)
                .ToListAsync();
        }
    }
}
