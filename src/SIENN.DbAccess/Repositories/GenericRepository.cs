using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using SIENN.DbAccess.Entity;
using SIENN.DbAccess.Context;

namespace SIENN.DbAccess.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        protected readonly DbSet<TEntity> _entities;
        protected readonly DbContext _context;

        public GenericRepository(StoreDbContext context)
        {
            _context = context;
            _entities = context.Set<TEntity>();
        }

        public virtual async Task<TEntity> GetAsync(int id)
        {
            return await _entities.FindAsync(id).ConfigureAwait(false);
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _entities.ToListAsync().ConfigureAwait(false);
        }

        public virtual async Task<IEnumerable<TEntity>> GetRangeAsync(int start, int count)
        {
            return await _entities.OrderBy(x=>x.Id)
                                  .Skip(start).Take(count)
                                  .ToListAsync().ConfigureAwait(false);
        }

        public virtual async Task<IEnumerable<TEntity>> GetRangeAsync(int start, int count, 
                                                                      Expression<Func<TEntity, bool>> predicate)
        {
            return await _entities.Where(predicate)
                                  .OrderBy(x=>x.Id)
                                  .Skip(start).Take(count)
                                  .ToListAsync().ConfigureAwait(false);
        }

        public virtual IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return _entities.Where(predicate);
        }

        public virtual async Task<int> CountAsync()
        {
            return await _entities.CountAsync().ConfigureAwait(false);
        }

        public virtual async Task AddAsync(TEntity entity)
        {            
            await _entities.AddAsync(entity).ConfigureAwait(false);
        }

        public virtual void Remove(TEntity entity)
        {
            _entities.Remove(entity);
        }

        public virtual void Update(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }

        public virtual IQueryable<TEntity> GetQueryable()
        {
            return _entities.AsQueryable();
        }
    }
}
