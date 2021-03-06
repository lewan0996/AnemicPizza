﻿using System.Threading.Tasks;
using AnemicPizza.Core.Models.Basket;
using Microsoft.EntityFrameworkCore;

namespace AnemicPizza.Infrastructure.Repositories
{
    public class BasketRepository : EFRepository<CustomerBasket>
    {
        public BasketRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
        public override Task<CustomerBasket> GetByIdAsync(int id)
        {
            return DbContext.CustomerBaskets
                .Include(b => b.Items)
                .ThenInclude(bi => bi.Product)
                .FirstOrDefaultAsync(b => b.Id == id);
        }
    }
}
