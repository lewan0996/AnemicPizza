using AnemicPizza.Domain.Models.Basket;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AnemicPizza.Infrastructure.EntityTypeConfigurations
{
    public class BasketEntityTypeConfiguration : IEntityTypeConfiguration<CustomerBasket>
    {
        public void Configure(EntityTypeBuilder<CustomerBasket> builder)
        {
            builder.HasMany(b => b.Items)
                .WithOne(bi=>bi.Basket)
                .HasForeignKey(bi => bi.BasketId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
