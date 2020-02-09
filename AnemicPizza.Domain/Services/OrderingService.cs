using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AnemicPizza.Core.Exceptions;
using AnemicPizza.Core.Models.Basket;
using AnemicPizza.Core.Models.Ordering;
using AnemicPizza.Core.Models.Products;
using AnemicPizza.Core.Repositories;
using AnemicPizza.Core.Services.Interfaces;

namespace AnemicPizza.Core.Services
{
    public class OrderingService : IOrderingService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IRepository<Product> _productRepository;

        public OrderingService(IOrderRepository orderRepository, IRepository<Product> productRepository)
        {
            _orderRepository = orderRepository;
            _productRepository = productRepository;
        }

        public async Task CreateOrderAsync(string clientFirstName, string clientLastName, string clientEmailAddress,
            string clientPhoneNumber, string city, string addressLine1, string addressLine2, short zipCode,
            IDictionary<int, BasketItem> basketItems)
        {
            var client = new Client
            {
                FirstName = clientFirstName,
                LastName = clientLastName,
                EmailAddress = clientEmailAddress,
                PhoneNumber = clientPhoneNumber
            };

            var address = new Address
            {
                City = city,
                AddressLine1 = addressLine1,
                AddressLine2 = addressLine2,
                ZipCode = zipCode
            };

            var orderItems = basketItems
                .Select(kv => 
                    new OrderItem
                    {
                        ProductId = kv.Key,
                        Quantity = kv.Value.Quantity, 
                        UnitPrice = kv.Value.Product.UnitPrice
                    })
                .ToList();

            var order = new Order
            {
                Client = client, 
                Address = address, 
                Items = orderItems,
                Status = OrderStatus.New
            };

            await _orderRepository.AddAsync(order);

            var isOrderValid = await ValidateOrderAsync(basketItems);

            order.Status = isOrderValid ? OrderStatus.InPreparation : OrderStatus.Cancelled;
        }

        public Task<IReadOnlyList<Order>> GetOrdersByUserEmailAsync(string email)
        {
            return _orderRepository.GetOrdersByUserEmailAsync(email);
        }

        private async Task<bool> ValidateOrderAsync(IDictionary<int, BasketItem> basketItems)
        {
            foreach (var (productId, basketItem) in basketItems)
            {
                var product = await _productRepository.GetByIdAsync(productId);

                if (product == null)
                {
                    throw new RecordNotFoundException(productId, nameof(Product));
                }

                var requestedQuantity = basketItem.Quantity;

                if (requestedQuantity > product.AvailableQuantity)
                {
                    return false;
                }
            }

            foreach (var (productId, basketItem) in basketItems)
            {
                var product = await _productRepository.GetByIdAsync(productId);

                var requestedQuantity = basketItem.Quantity;

                product.TakeFromWarehouse(requestedQuantity);
            }

            return true;
        }
    }
}
