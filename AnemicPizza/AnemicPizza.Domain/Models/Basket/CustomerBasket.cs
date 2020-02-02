using System.Collections.Generic;

namespace AnemicPizza.Domain.Models.Basket
{
    public class CustomerBasket : Entity
    {
        public IList<BasketItem> Items { get; set; }
    }
}
