using System.Collections.Generic;

namespace AnemicPizza.Core.Models.Basket
{
    public class CustomerBasket : Entity
    {
        public IList<BasketItem> Items { get; set; }
    }
}
