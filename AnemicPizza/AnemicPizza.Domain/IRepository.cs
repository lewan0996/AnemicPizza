using System.Collections.Generic;
using System.Threading.Tasks;
using AnemicPizza.Domain.Models;

namespace AnemicPizza.Domain
{
    public interface IRepository<T> where T: Entity
    {
        Task AddAsync(T item);
        Task<T> GetByIdAsync(int id);
        Task<IReadOnlyList<T>> GetAll();
        void Delete(T item);
        //IUnitOfWork UnitOfWork { get; } //todo UnitOfWork
    }
}
