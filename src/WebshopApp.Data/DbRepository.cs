﻿using System;
using System.Collections.Generic;
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

        public async Task AddAsync(TEntity entity)
        {
            await this.dbSet.AddAsync(entity);
        }

        public IQueryable<TEntity> All()
        {
            return this.dbSet;
        }

        public void Delete(TEntity entity)
        {
            this.dbSet.Remove(entity);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await this.context.SaveChangesAsync();
        }

        public void Update(TEntity entity)
        {
            this.dbSet.Update(entity);
        }

        public void UpdateRange(ICollection<TEntity> entities)
        {
            this.dbSet.UpdateRange(entities);
        }

        public void Dispose()
        {
            this.context.Dispose();
        }
    }
}
