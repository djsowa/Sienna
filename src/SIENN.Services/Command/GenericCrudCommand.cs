using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SIENN.DbAccess.Context;
using SIENN.DbAccess.Entity;
using SIENN.DbAccess.Repositories;

namespace SIENN.Services.Command
{
    public class GenericCrudCommand<TEntity> where TEntity : BaseEntity
    {
        protected readonly IGenericRepository<TEntity> Repository;
        protected readonly StoreDbContext Context;

        public GenericCrudCommand(StoreDbContext context)
        {
            Context = context;
            Repository = new GenericRepository<TEntity>(context);
        }

        public GenericCrudCommand(StoreDbContext context, IGenericRepository<TEntity> repository)
        {
            Context = context;
            Repository = repository;
        }

        protected async Task<int> SaveAsync()
        {
            return await Context.SaveChangesAsync().ConfigureAwait(false);
        }

        public virtual async Task<int> AddAsync(TEntity entity)
        {
            try
            {
                await Repository.AddAsync(entity).ConfigureAwait(false);
                if ((await SaveAsync().ConfigureAwait(false)) == 0)
                {
                    return 0;
                }
                else
                {
                    return entity.Id;
                }
            }
            catch (System.Exception er)
            {
                Console.WriteLine($"{er}");
                return 0;
            }
        }

        public virtual async Task UpdateAsync(TEntity entity)
        {
            Repository.Update(entity);

            await SaveAsync().ConfigureAwait(false);
        }

        public virtual async Task<bool> RemoveAsync(TEntity entity)
        {
            Repository.Remove(entity);

            return (await SaveAsync().ConfigureAwait(false)) > 0;
        }

        public virtual async Task<TEntity> GetAsync(int entityId)
        {
            return await Repository.GetAsync(entityId).ConfigureAwait(false);
        }

        public virtual async Task<int> CountAsync()
        {
            return await Repository.CountAsync().ConfigureAwait(false);
        }

        public virtual async Task<IEnumerable<TEntity>> GetRangeAsync(int skip, int take)
        {
            return await Repository.GetRangeAsync(skip, take);
        }

        public virtual IQueryable<TEntity> GetQueryable()
        {
            return Repository.GetQueryable();
        }
    }
}