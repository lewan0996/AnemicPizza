using AnemicPizza.Domain.Models.Products;

namespace AnemicPizza.Domain.Models.Ordering
{
    public class OrderItem : Entity
    {
        public Product Product { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public float UnitPrice { get; set; }
    }
}
