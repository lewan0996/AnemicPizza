using System.Collections.Generic;
using System.Threading.Tasks;
using AnemicPizza.Core.Models.Ordering;

namespace AnemicPizza.Core.Repositories
{
    public interface ISupplierRepository : IRepository<Supplier>
    {
        Task<Supplier> GetFirstFreeSupplier();
        Task<IReadOnlyList<Order>> GetSupplierOrdersAsync(int supplierId);
    }
}
