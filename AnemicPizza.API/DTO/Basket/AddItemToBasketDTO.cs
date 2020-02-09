using System.ComponentModel.DataAnnotations;
// ReSharper disable UnusedAutoPropertyAccessor.Global
#pragma warning disable 1591

namespace AnemicPizza.API.DTO.Basket
{
    public class AddItemToBasketDTO
    {
        [Range(1, int.MaxValue)]
        public int ProductId { get; set; }
        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }
    }
}
