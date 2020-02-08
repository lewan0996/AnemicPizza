using AnemicPizza.Core.Models.Products;

namespace AnemicPizza.Core.Models.Ordering
{
    public class OrderItem : Entity
    {
        public Product Product { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public float UnitPrice { get; set; }
    }
}
