using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Repositories.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task< IQueryable<T>> GetAll();
        
       
        Task<T> add (T entity);
        Task update (T entity);
        Task delete (T entity);
        Task save();


    }
}
