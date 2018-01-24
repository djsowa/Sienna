using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SIENN.DbAccess.Entity;
using SIENN.DbAccess.Repositories;

namespace SIENN.Services.Command
{
    public class GenericCrudCommand<TEntity> where TEntity : BaseEntity
    {
        protected readonly IGenericRepository<TEntity> _repository = null;
        protected readonly DbContext _context = null;

        public GenericCrudCommand(DbContext context)
        {
            _context = context;
            _repository = new GenericRepository<TEntity>(context);
        }

        public GenericCrudCommand(DbContext context, IGenericRepository<TEntity> repository)
        {
            _context = context;
            _repository = repository;
        }

        protected async Task<int> Save()
        {
            return await _context.SaveChangesAsync().ConfigureAwait(false);
        }

        public virtual async Task<int> AddAsync(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Added;

            if ((await Save().ConfigureAwait(false)) == 0)
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

            await Save().ConfigureAwait(false);
        }

        public virtual async Task RemoveAsync(TEntity entity)
        {
            _repository.Remove(entity);

            await Save().ConfigureAwait(false);
        }

        public virtual async Task<TEntity> GetAsync(int entityId)
        {
            return await _repository.GetAsync(entityId).ConfigureAwait(false);
        }

        public virtual IQueryable<TEntity> GetQueryable()
        {
            return _repository.GetQueryable();
        }
    }
}