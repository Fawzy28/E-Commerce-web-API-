using Ecommerce.Context.Context;
using Ecommerce.Models;
using Ecommerce.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Repositories.Repositories
{
    public class OrderRepository :GenericRepository<Order> , IOrderRepository
    {
        EcContext context;
        DbSet<Order> dbset;
        public OrderRepository(EcContext _context) : base(_context)
        {
            context = _context;
            dbset = context.Order;
        }

        public async Task<IQueryable<Order>> GetOne(int Id)
        {
            if (dbset != null)
            {
                var result = await Task.FromResult(dbset.Where(o => o.Id == Id));
                return result;
            }
            return null;
        }

    }
}
