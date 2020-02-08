using AnemicPizza.Core.Models.Basket;
using AnemicPizza.Core.Models.Ordering;
using AnemicPizza.Core.Models.Products;
using Microsoft.EntityFrameworkCore;

namespace AnemicPizza.Infrastructure
{
    public class AppDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<Pizza> Pizzas { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<CustomerBasket> CustomerBaskets { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }
    }
}
