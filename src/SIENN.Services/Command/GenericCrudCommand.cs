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
        protected readonly IGenericRepository<TEntity> _repository = null;
        protected readonly StoreDbContext _context = null;

        public GenericCrudCommand(StoreDbContext context)
        {
            _context = context;
            _repository = new GenericRepository<TEntity>(context);
        }

        public GenericCrudCommand(StoreDbContext context, IGenericRepository<TEntity> repository)
        {
            _context = context;
            _repository = repository;
        }

        protected async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync().ConfigureAwait(false);
        }

        public virtual async Task<int> AddAsync(TEntity entity)
        {
            await _repository.AddAsync(entity).ConfigureAwait(false);
            if ((await SaveAsync().ConfigureAwait(false)) == 0)
            {
                return 0;
            }
            else
            {
                return entity.Id;
            }
        }

        public virtual async Task UpdateAsync(TEntity entity)
        {
            _repository.Update(entity);

            await SaveAsync().ConfigureAwait(false);
        }

        public virtual async Task<bool> RemoveAsync(TEntity entity)
        {
            _repository.Remove(entity);

            return (await SaveAsync().ConfigureAwait(false)) > 0;
        }

        public virtual async Task<TEntity> GetAsync(int entityId)
        {
            return await _repository.GetAsync(entityId).ConfigureAwait(false);
        }

        public virtual async Task<int> CountAsync()
        {
            return await _repository.CountAsync().ConfigureAwait(false);
        }

        public virtual async Task<IEnumerable<TEntity>> GetRangeAsync(int skip, int take)
        {
            return await _repository.GetRangeAsync(skip, take);
        }

        public virtual IQueryable<TEntity> GetQueryable()
        {
            return _repository.GetQueryable();
        }
    }
}