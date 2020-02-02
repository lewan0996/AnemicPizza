namespace AnemicPizza.Domain.Models.Products
{
    public class Product : Entity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual ProductType Type { get; set; }
        public float UnitPrice { get; set; }
        public virtual int AvailableQuantity { get; set; }
    }
}
