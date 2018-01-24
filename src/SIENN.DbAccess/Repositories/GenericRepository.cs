using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using SIENN.DbAccess.Entity;

namespace SIENN.DbAccess.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        private DbSet<TEntity> _entities;
        private DbContext _context;

        public GenericRepository(DbContext context)
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
            return await _entities.Skip(start).Take(count).ToListAsync().ConfigureAwait(false);
        }

        public virtual async Task<IEnumerable<TEntity>> GetRangeAsync(int start, int count, Expression<Func<TEntity, bool>> predicate)
        {
            return await _entities.Where(predicate).Skip(start).Take(count).ToListAsync().ConfigureAwait(false);
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

        public void Update(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }

        public IQueryable<TEntity> GetQueryable()
        {
            return _entities.AsQueryable();
        }
    }
}
