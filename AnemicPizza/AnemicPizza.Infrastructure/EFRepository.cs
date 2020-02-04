using System.Collections.Generic;
using System.Threading.Tasks;
using AnemicPizza.Domain;
using AnemicPizza.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace AnemicPizza.Infrastructure
{
    public class EFRepository<T> : IRepository<T> where T : Entity
    {
        protected readonly AppDbContext DbContext;

        public EFRepository(AppDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public virtual async Task AddAsync(T item)
        {
            await DbContext.Set<T>().AddAsync(item);
        }

        public virtual async Task<T> GetByIdAsync(int id)
        {
            return await DbContext.Set<T>().FindAsync(id);
        }

        public virtual async Task<IReadOnlyList<T>> GetAll()
        {
            return await DbContext.Set<T>().ToListAsync();
        }

        public virtual void Delete(T item)
        {
            DbContext.Set<T>().Remove(item);
        }
    }
}
