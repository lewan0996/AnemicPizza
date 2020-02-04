using AnemicPizza.Core.Models.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AnemicPizza.Infrastructure.EntityTypeConfigurations
{
    public class PizzaIngredientEntityTypeConfiguration : IEntityTypeConfiguration<PizzaIngredient>
    {
        public void Configure(EntityTypeBuilder<PizzaIngredient> builder)
        {
            builder.HasKey(pi => new { pi.PizzaId, pi.IngredientId });
            builder.HasOne(pi => pi.Pizza)
                .WithMany(p => p.Ingredients)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(pi => pi.Ingredient)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict); // Because of TPH there is a cycle
        }
    }
}
