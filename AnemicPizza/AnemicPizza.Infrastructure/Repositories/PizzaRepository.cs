using System.Collections.Generic;
using System.Threading.Tasks;
using AnemicPizza.Core.Models.Products;
using Microsoft.EntityFrameworkCore;

namespace AnemicPizza.Infrastructure.Repositories
{
    public class PizzaRepository : EFRepository<Pizza>
    {
        public PizzaRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public override Task<Pizza> GetByIdAsync(int id)
        {
            return DbContext.Pizzas
                .Include(p => p.Ingredients)
                .ThenInclude(pi => pi.Ingredient)
                .FirstOrDefaultAsync(p => p.Id == id);
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
