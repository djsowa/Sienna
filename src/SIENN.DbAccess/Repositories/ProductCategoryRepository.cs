using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SIENN.DbAccess.Context;
using SIENN.DbAccess.Entity;

namespace SIENN.DbAccess.Repositories
{
    public class ProductCategoryRepository : GenericRepository<ProductCategory>
    {
        public ProductCategoryRepository(StoreDbContext context) : base(context)
        {
        }

        public override async Task AddAsync(ProductCategory entity)
        {
            if (await _entities.AnyAsync(p => p.Code == entity.Code))
            {
                throw new InvalidOperationException("Category with the same code already exists.");
            }

            await base.AddAsync(entity);
        }

        public override void Update(ProductCategory entity)
        {
            if (_entities.Any(p => p.Code == entity.Code && p.Id != entity.Id))
            {
                throw new InvalidOperationException("Category with the same code already exists.");
            }

            base.Update(entity);
        }
    }
}