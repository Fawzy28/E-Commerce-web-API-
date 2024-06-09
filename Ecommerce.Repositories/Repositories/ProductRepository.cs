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
    public class ProductRepository :  GenericRepository<Product> , IProductRepository
    {
        EcContext context;
        DbSet<Product> dbset;
        public ProductRepository(EcContext _context) : base(_context) 
        {
            context = _context;
            dbset = context.Products;
        }

        public async Task<IQueryable<Product>> GetOne(int Id)
        {
            if (dbset != null)
            {
                var result = await Task.FromResult(dbset.Where(p => p.Id == Id));
                return result;
            }
            return null;
        }
        public async Task<IQueryable<Product>> GetByCateg (string categoryName)
        {
            if (dbset != null)
            {
                var result = await Task.FromResult(dbset.Where(p => p.CategoryName.Name == categoryName).Include(p => p.CategoryName));
                return result;
            }
            return null;
        }
    }
}
