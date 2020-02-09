using System.Linq;
using System.Threading.Tasks;
using AnemicPizza.Core.Models.Products;
using Microsoft.EntityFrameworkCore;

namespace AnemicPizza.Infrastructure.Repositories
{
    public class ProductRepository : EFRepository<Product>
    {
        public ProductRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public override async Task<Product> GetByIdAsync(int id)
        {
            var product = await base.GetByIdAsync(id);

            if (product is Pizza pizza)
            {
                DbContext.Entry(pizza)
                    .Collection(p => p.Ingredients)
                    .Query().OfType<PizzaIngredient>()
                    .Include(pi => pi.Ingredient)
                    .Load();

                return pizza;
            }

            return product;
        }
    }
}
