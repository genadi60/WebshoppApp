using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebshopApp.Data.Common;

namespace WebshopApp.Data
{
    public class DbRepository<TEntity> : IRepository<TEntity>, IDisposable
        where TEntity : class
    {
        private readonly WebshopAppContext context;
        private readonly DbSet<TEntity> dbSet;

        public DbRepository(WebshopAppContext context)
        {
            this.context = context;
            this.dbSet = this.context.Set<TEntity>();
        }

        public Task AddAsync(TEntity entity)
        {
            return this.dbSet.AddAsync(entity);
        }

        public IQueryable<TEntity> All()
        {
            return this.dbSet;
        }

        public async Task<int> Delete(TEntity entity)
        {
            this.dbSet.Remove(entity);
            var result = await this.context.SaveChangesAsync();
            return result;
        }

        public Task<int> SaveChangesAsync()
        {
            return this.context.SaveChangesAsync();
        }

        public Task<int> Update(TEntity entity)
        {
            this.dbSet.Update(entity);
            return this.context.SaveChangesAsync();
        }

        public void Dispose()
        {
            this.context.Dispose();
        }
    }
}
