using AnemicPizza.Core.Models.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AnemicPizza.Infrastructure.EntityTypeConfigurations
{
    internal class IngredientEntityTypeConfiguration : IEntityTypeConfiguration<Ingredient>
    {
        public void Configure(EntityTypeBuilder<Ingredient> builder)
        {
            builder.Property(p => p.Type).HasConversion<string>();
        }
    }
}
