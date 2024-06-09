using Ecommerce.Context.Context;
using Ecommerce.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace Ecommerce.Repositories.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        DbSet<T> dbset;
        EcContext context;
        public GenericRepository(EcContext _context)
        {
            context = _context;
            dbset = context.Set<T>();

        }

        public async Task<IQueryable<T>> GetAll()
        {
            if (dbset != null)
            {
                var result = await Task.FromResult(dbset.Select(e => e));
                return result;
            }
            return null;

        }
       

        public async Task<T> add(T entity)
        {
            var res = await dbset.AddAsync(entity); 
            context.SaveChanges();

            var addedEntity = res.Entity;
            return addedEntity;

        }

        public async Task update(T entity)
        {
            await Task.FromResult(dbset.Update(entity));
            context.SaveChanges();
            
        }

        public async Task delete(T entity)
        {
            await Task.FromResult(dbset.Remove(entity));
            context.SaveChanges();
        }

        public async Task save()
        {
            await Task.FromResult(context.SaveChanges());
        }


    }
}