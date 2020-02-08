using System.Collections.Generic;
using System.Threading.Tasks;
using AnemicPizza.Core.Models.Ordering;

namespace AnemicPizza.Core.Services.Interfaces
{
    public interface IDeliveryService
    {
        Task StartDeliveryAsync(int orderId);
        Task<IReadOnlyList<Order>> GetSupplierOrdersAsync(int supplierId);
        Task FinishOrderDeliveryAsync(int orderId);
    }
}
