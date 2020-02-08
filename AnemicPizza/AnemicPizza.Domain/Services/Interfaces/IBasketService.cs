using System.Threading.Tasks;
using AnemicPizza.Core.Models.Basket;

namespace AnemicPizza.Core.Services.Interfaces
{
    public interface IBasketService
    {
        Task<CustomerBasket> AddItemToBasketAsync(int? basketId, int productId, int quantity);
        Task RemoveItemFromBasketAsync(int basketId, int productId);
        Task SetQuantityOfBasketItemAsync(int basketId, int productId, int quantity);
        Task CheckoutAsync(int basketId, string firstName, string lastName, string emailAddress, string phoneNumber,
            string city, string addressLine1, string addressLine2, short zipCode);

        Task<CustomerBasket> GetBasketAsync(int basketId);
    }
}
