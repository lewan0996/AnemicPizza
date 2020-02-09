using System.Collections.Generic;
using System.Threading.Tasks;
using AnemicPizza.Core.Models;

namespace AnemicPizza.Core.Repositories
{
    public interface IRepository<T> where T: Entity
    {
        Task AddAsync(T item);
        Task<T> GetByIdAsync(int id);
        Task<IReadOnlyList<T>> GetAllAsync();
        void Delete(T item);
        //IUnitOfWork UnitOfWork { get; } //todo UnitOfWork
    }
}
