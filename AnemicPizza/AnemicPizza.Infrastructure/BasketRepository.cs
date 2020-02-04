using System.Threading.Tasks;
using AnemicPizza.Domain.Models.Basket;
using Microsoft.EntityFrameworkCore;

namespace AnemicPizza.Infrastructure
{
    public class BasketRepository : EFRepository<CustomerBasket>
    {
        public BasketRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
        public override Task<CustomerBasket> GetByIdAsync(int id)
        {
            return DbContext.CustomerBaskets
                .Include(b => b.Items)
                .FirstOrDefaultAsync(b => b.Id == id);
        }
    }
}
