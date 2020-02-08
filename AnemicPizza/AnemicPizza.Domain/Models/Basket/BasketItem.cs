using AnemicPizza.Core.Models.Products;

namespace AnemicPizza.Core.Models.Basket
{
    public class BasketItem : Entity
    {
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public CustomerBasket Basket { get; set; }
        public int BasketId { get; set; }
    }
}
