using System;
using AnemicPizza.Core.Exceptions;
// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace AnemicPizza.Core.Models.Products
{
    public class Product : Entity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual ProductType Type { get; private set; }
        public float UnitPrice { get; set; }
        private int _availableQuantity;
        public virtual int AvailableQuantity
        {
            get => _availableQuantity;
            set => _availableQuantity = value;
        }

        public Product(string name, string description, float unitPrice, ProductType type, int availableQuantity = 0)
        {
            Name = name;
            Description = description;
            UnitPrice = unitPrice;
            Type = type;
            _availableQuantity = availableQuantity;
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

        public virtual void TakeFromWarehouse(int quantity)
        {
            if (AvailableQuantity - quantity <= 0)
            {
                throw new DomainException(new ArgumentException("The quantity of the product must be greater than 0",
                    nameof(quantity)));
            }
            AvailableQuantity -= quantity;
        }
    }
}
