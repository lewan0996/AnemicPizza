using System.Collections.Generic;
using AnemicPizza.Core.Models.Basket;
using AutoMapper;
#pragma warning disable 1591

namespace AnemicPizza.API.DTO.Basket
{
    [AutoMap(typeof(CustomerBasket))]
    public class BasketDTO
    {
        public int Id { get; set; }
        public List<BasketItemDTO> Items { get; set; }
    }
}
