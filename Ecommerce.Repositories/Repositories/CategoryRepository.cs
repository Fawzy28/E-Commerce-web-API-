using Ecommerce.Context.Context;
using Ecommerce.Models;
using Ecommerce.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Repositories.Repositories
{
    public class CategoryRepository : GenericRepository<Category> , ICategoryRepository
    {
        EcContext context;
        DbSet<Category> dbset;
        public CategoryRepository(EcContext _context) : base(_context)
        {
            context = _context;
            dbset = context.Category;
        }

        public async Task<IQueryable<Category>> GetOne(int Id)
        {
            if (dbset != null)
            {
                var result = await Task.FromResult(dbset.Where(c => c.Id == Id));
                if (result != null)
                { return result; }
            }
            return null;
        }
    }
}
