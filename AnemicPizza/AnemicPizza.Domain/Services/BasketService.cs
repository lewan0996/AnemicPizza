using System;
using System.Linq;
using System.Threading.Tasks;
using AnemicPizza.Core.Exceptions;
using AnemicPizza.Core.Models.Basket;
using AnemicPizza.Core.Repositories;
using AnemicPizza.Core.Services.Interfaces;

namespace AnemicPizza.Core.Services
{
    public class BasketService : IBasketService
    {
        private readonly IRepository<CustomerBasket> _basketRepository;
        private readonly IOrderingService _orderingService;

        public BasketService(IRepository<CustomerBasket> basketRepository, IOrderingService orderingService)
        {
            _basketRepository = basketRepository;
            _orderingService = orderingService;
        }

        public async Task<CustomerBasket> AddItemToBasketAsync(int? basketId, int productId, int quantity)
        {
            CustomerBasket customerBasket;
            if (basketId == null)
            {
                customerBasket = new CustomerBasket();
                await _basketRepository.AddAsync(customerBasket);
            }
            else
            {
                customerBasket = await _basketRepository.GetByIdAsync(basketId.Value);
            }

            var basketItem = new BasketItem {ProductId = productId, Quantity = quantity, Basket = customerBasket};

            var productExistsInBasket = customerBasket.Items
                .Any(bi => bi.ProductId == productId);

            if (productExistsInBasket)
            {
                var existingBasketItem = customerBasket.Items.First(item => item.ProductId == productId);
                existingBasketItem.Quantity += quantity;
            }
            else
            {
                customerBasket.Items.Add(basketItem);
            }

            return customerBasket;
        }

        public async Task RemoveItemFromBasketAsync(int basketId, int productId)
        {
            var basket = await _basketRepository.GetByIdAsync(basketId);

            if (basket == null)
            {
                throw new RecordNotFoundException(basketId, nameof(CustomerBasket));
            }

            var basketItemToRemove = basket.Items.FirstOrDefault(item => item.ProductId == productId);
            if (basketItemToRemove == null)
            {
                throw new RecordNotFoundException(productId, nameof(BasketItem));
            }

            basket.Items.Remove(basketItemToRemove);
        }

        public async Task SetQuantityOfBasketItemAsync(int basketId, int productId, int quantity)
        {
            if (quantity <= 0)
            {
                throw new DomainException(new ArgumentOutOfRangeException(nameof(quantity),
                    "Quantity of a basket item must be greater than zero"));
            }

            var basket = await _basketRepository.GetByIdAsync(basketId);

            if (basket == null)
            {
                throw new RecordNotFoundException(basketId, nameof(CustomerBasket));
            }

            var basketItemToUpdate = basket.Items.FirstOrDefault(item => item.ProductId == productId);
            
            if (basketItemToUpdate == null)
            {
                throw new RecordNotFoundException(productId, nameof(BasketItem));
            }

            basketItemToUpdate.Quantity = quantity;
        }

        public async Task CheckoutAsync(int basketId, string firstName, string lastName, string emailAddress, string phoneNumber, string city,
            string addressLine1, string addressLine2, short zipCode)
        {
            var basket = await _basketRepository.GetByIdAsync(basketId);

            if (basket == null)
            {
                throw new RecordNotFoundException(basketId, nameof(CustomerBasket));
            }

            await _orderingService.CreateOrderAsync(firstName, lastName, emailAddress, phoneNumber, city, addressLine1,
                addressLine2, zipCode, basket.Items.ToDictionary(bi => bi.ProductId, bi => bi));

            basket.Items.Clear();
        }

        public Task<CustomerBasket> GetBasketAsync(int basketId)
        {
            return _basketRepository.GetByIdAsync(basketId);
        }
    }
}
