using System.Collections.Generic;
using System.Threading.Tasks;
using AnemicPizza.Core.Exceptions;
using AnemicPizza.Core.Models.Ordering;
using AnemicPizza.Core.Repositories;
using AnemicPizza.Core.Services.Interfaces;

namespace AnemicPizza.Core.Services
{
    public class DeliveryService : IDeliveryService
    {
        private readonly IRepository<Order> _orderRepository;
        private readonly ISupplierRepository _supplierRepository;

        public DeliveryService(IRepository<Order> orderRepository, ISupplierRepository supplierRepository)
        {
            _orderRepository = orderRepository;
            _supplierRepository = supplierRepository;
        }

        public async Task StartDeliveryAsync(int orderId)
        {
            var order = await _orderRepository.GetByIdAsync(orderId);

            if (order == null)
            {
                throw new RecordNotFoundException(orderId, nameof(Order));
            }

            if (order.Status != OrderStatus.InPreparation)
            {
                throw new DomainException("Cannot ship an unprepared order");
            }

            var supplier = await _supplierRepository.GetFirstFreeSupplier();

            supplier.Status = SupplierStatus.Occupied;
            order.Supplier = supplier;

            order.Status = OrderStatus.InDelivery;
        }

        public Task<IReadOnlyList<Order>> GetSupplierOrdersAsync(int supplierId)
        {
            return _supplierRepository.GetSupplierOrdersAsync(supplierId);
        }

        public async Task FinishOrderDeliveryAsync(int orderId)
        {
            var order = await _orderRepository.GetByIdAsync(orderId);

            if (order.Status != OrderStatus.InDelivery)
            {
                throw new DomainException("Cannot finish delivery of an order whose delivery has not started");
            }

            order.Status = OrderStatus.Completed;
            order.Supplier.Status = SupplierStatus.Free;
        }
    }
}
