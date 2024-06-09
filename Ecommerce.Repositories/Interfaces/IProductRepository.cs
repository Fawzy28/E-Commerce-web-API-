using Ecommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Repositories.Interfaces
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        Task<IQueryable<Product>> GetByCateg (string categName);

        Task<IQueryable<Product>> GetOne(int id);
    }
}
