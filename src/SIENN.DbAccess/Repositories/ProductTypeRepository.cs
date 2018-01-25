using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SIENN.DbAccess.Context;
using SIENN.DbAccess.Entity;

namespace SIENN.DbAccess.Repositories
{
    public class ProductTypeRepository : GenericRepository<ProductType>
    {
        public ProductTypeRepository(StoreDbContext context) : base(context)
        {
        }

        public override async Task AddAsync(ProductType entity)
        {
            if (await _entities.AnyAsync(p => p.Code == entity.Code))
            {
                throw new InvalidOperationException("Type with the same code already exists.");
            }

            await base.AddAsync(entity);
        }

        public override void Update(ProductType entity)
        {
            if (_entities.Any(p => p.Code == entity.Code && p.Id != entity.Id))
            {
                throw new InvalidOperationException("Type with the same code already exists.");
            }

            base.Update(entity);
        }
    }
}