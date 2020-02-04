using System.Collections.Generic;
using System.Threading.Tasks;
using AnemicPizza.Core.Models.Products;
using Microsoft.EntityFrameworkCore;

namespace AnemicPizza.Infrastructure
{
    public class PizzaRepository : EFRepository<Pizza>
    {
        public PizzaRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public override async Task<IReadOnlyList<Pizza>> GetAllAsync()
        {
            return await DbContext.Pizzas
                .Include(p => p.Ingredients)
                .ThenInclude(pi => pi.Ingredient)
                .ToListAsync();
        }
    }
}
