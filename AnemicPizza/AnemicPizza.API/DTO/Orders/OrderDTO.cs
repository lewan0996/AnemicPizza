using System.Collections.Generic;
using AnemicPizza.Core.Models.Ordering;
using AutoMapper;
#pragma warning disable 1591

namespace AnemicPizza.API.DTO.Orders
{
    [AutoMap(typeof(Order))]
    public class OrderDTO
    {
        public int Id { get; set; }
        public ClientDTO Client { get; set; }
        public AddressDTO Address { get; set; }
        public List<OrderItemDTO> Items { get; set; }
        public OrderStatus Status { get; set; }
    }
}
