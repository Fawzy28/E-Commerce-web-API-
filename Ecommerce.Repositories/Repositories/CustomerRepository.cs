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
    public class CustomerRepository : GenericRepository<Customer> , ICustomerRepository
    {
        EcContext context;
        DbSet<Customer> dbset;
        public CustomerRepository(EcContext _context) : base(_context)
        {
            context = _context;
            dbset = context.Customer;
        }
        public async Task<IQueryable<Customer>> GetOne(string Id)

        {
            if (dbset != null)
            {
                var result = await Task.FromResult(dbset.Where(c => c.Id == Id));
                return result;
            }
            return null;
        }
    }


}
