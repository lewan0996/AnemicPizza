using AnemicPizza.Core.Models.Basket;
using AutoMapper;

namespace AnemicPizza.API.DTO.Basket
{
    [AutoMap(typeof(BasketItem))]
    public class BasketItemDTO
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
