using AnemicPizza.Domain.Models.Basket;
using AnemicPizza.Domain.Models.Ordering;
using AnemicPizza.Domain.Models.Products;
using Microsoft.EntityFrameworkCore;

namespace AnemicPizza.Infrastructure
{
    public class AppDbContext : DbContext
    {
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
