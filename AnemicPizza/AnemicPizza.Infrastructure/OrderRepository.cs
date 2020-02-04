using System.Threading.Tasks;
using AnemicPizza.Domain.Models.Ordering;
using Microsoft.EntityFrameworkCore;

namespace AnemicPizza.Infrastructure
{
    public class OrderRepository : EFRepository<Order>
    {
        public OrderRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public override Task<Order> GetByIdAsync(int id)
        {
            return DbContext.Orders
                .Include(o => o.Items)
                .FirstOrDefaultAsync(o => o.Id == id);
        }
    }
}
