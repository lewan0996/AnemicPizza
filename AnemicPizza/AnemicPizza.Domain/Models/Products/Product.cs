namespace AnemicPizza.Core.Models.Products
{
    public class Product : Entity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual ProductType Type { get; set; }
        public float UnitPrice { get; set; }
        public virtual int AvailableQuantity { get; set; }

        public Product(string name, string description, float unitPrice, int availableQuantity)
        {
            Name = name;
            Description = description;
            UnitPrice = unitPrice;
            AvailableQuantity = availableQuantity;
        }

        protected Product() // For EF
        {
            
        }
    }
}
