using System.Collections.Generic;

namespace AnemicPizza.Domain.Models.Ordering
{
    public class Order : Entity
    {
        public Client Client { get; set; }
        public Address Address { get; set; }
        public IList<OrderItem> Items { get; set; }
        public OrderStatus Status { get; set; }
        public Supplier Supplier { get; set; }
    }

    public enum OrderStatus
    {
        New = 1,
        InPreparation = 2,
        InDelivery = 3,
        Completed = 4,
        Cancelled = 5
    }
}
