using AnemicPizza.Core.Models;
using AnemicPizza.Core.Models.Ordering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AnemicPizza.Infrastructure.EntityTypeConfigurations
{
    public class SupplierEntityTypeConfiguration : IEntityTypeConfiguration<Supplier>
    {
        public void Configure(EntityTypeBuilder<Supplier> builder)
        {
            builder.Property(s => s.Status).HasConversion<string>();

            var seedData = new[]
            {
                new Supplier {FirstName = "Jan", LastName = "Kowalski", Status = SupplierStatus.Free},
                new Supplier {FirstName = "Adam", LastName = "Nowak", Status = SupplierStatus.Free}
            };

            var idPropertyInfo = typeof(Entity).GetProperty(nameof(Entity.Id));

            // ReSharper disable once PossibleNullReferenceException
            idPropertyInfo.SetValue(seedData[0], 1);
            idPropertyInfo.SetValue(seedData[1], 2);

            builder.HasData(seedData);
        }
    }
}
