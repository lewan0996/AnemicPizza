using System;

namespace AnemicPizza.Core.Models.Products
{
    public class Product : Entity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual ProductType Type { get; set; }
        public float UnitPrice { get; set; }
        public virtual int AvailableQuantity { get; set; }

        public Product(string name, string description, float unitPrice, int availableQuantity=0)
        {
            Name = name;
            Description = description;
            UnitPrice = unitPrice;
            AvailableQuantity = availableQuantity;
        }

        protected Product() // For EF
        {
            
        }

        public void Validate()
        {
            if (string.IsNullOrWhiteSpace(Name))
            {
                throw new DomainException(new ArgumentException("Product name can't be empty.", nameof(Name)));
            }
            if (string.IsNullOrWhiteSpace(Description))
            {
                throw new DomainException(new ArgumentException("Product name can't be empty.", nameof(Description)));
            }
            if (AvailableQuantity <= 0)
            {
                throw new DomainException(new ArgumentException("The quantity of the product must be greater than 0",
                    nameof(AvailableQuantity)));
            }
        }
    }
}
