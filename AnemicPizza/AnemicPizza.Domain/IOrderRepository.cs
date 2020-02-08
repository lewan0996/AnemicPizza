using System.Collections.Generic;
using System.Threading.Tasks;
using AnemicPizza.Core.Models.Ordering;

namespace AnemicPizza.Core
{
    public interface IOrderRepository : IRepository<Order>
    {
        Task<IReadOnlyList<Order>> GetOrdersByUserEmailAsync(string email);
    }
}
