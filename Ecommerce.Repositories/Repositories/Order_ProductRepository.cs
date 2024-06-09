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
    public class Order_ProductRepository : GenericRepository<Order_Product> , IOrder_productRepository
    {
        EcContext context;
        DbSet<Order_Product> dbset;
        public Order_ProductRepository(EcContext _context) : base(_context)
        {
            context = _context;
        }
    }
}
