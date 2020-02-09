using AnemicPizza.Core.Models.Ordering;
using AutoMapper;
#pragma warning disable 1591

namespace AnemicPizza.API.DTO.Orders
{
    [AutoMap(typeof(OrderItem))]
    public class OrderItemDTO
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public float UnitPrice { get; set; }
    }
}
