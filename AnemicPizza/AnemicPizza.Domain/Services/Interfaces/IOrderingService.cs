using System.Collections.Generic;
using System.Threading.Tasks;
using AnemicPizza.Core.Models.Basket;
using AnemicPizza.Core.Models.Ordering;

namespace AnemicPizza.Core.Services.Interfaces
{
    public interface IOrderingService
    {
        Task CreateOrderAsync(string clientFirstName, string clientLastName, string clientEmailAddress,
            string clientPhoneNumber, string city, string addressLine1, string addressLine2, short zipCode,
            IDictionary<int, BasketItem> basketItems);

        Task<IReadOnlyList<Order>> GetOrdersByUserEmailAsync(string email);
    }
}
