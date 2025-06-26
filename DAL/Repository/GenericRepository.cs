using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Generic_Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Implementation
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected DbContext context;
        protected DbSet<T> dbSet;

        public GenericRepository(DbContext context)
        {
            this.context = context;
            dbSet = this.context.Set<T>();
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await dbSet.ToListAsync();
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await dbSet.FindAsync(id);
        }

        public async Task AddAsync(T entity)
        {
            await dbSet.AddAsync(entity);
        }

        public void Update(T entity)
        {
            dbSet.Update(entity);
        }

        public void Delete(T entity)
        {
            dbSet.Remove(entity);
        }

        public async Task SaveChangesAsync()
        {
            await context.SaveChangesAsync();
        }
    }
}
