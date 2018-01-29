using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SIENN.DbAccess.Context;
using SIENN.DbAccess.Entity;
using SIENN.DbAccess.Repositories;
using SIENN.Services.Command;
using SIENN.Services.Models;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using System;

namespace SIENN.Services.ControllerServices.Search
{
    public class ProductSearchControllerService : IProductSearchControllerService<ProductModel, ProductFilterModel>
    {
        protected readonly StoreDbContext Context;
        protected readonly IMapper Mapper;
        protected readonly IGenericRepository<Product> Repository;

        public ProductSearchControllerService(StoreDbContext context, IMapper mapper, IGenericRepository<Product> repository)
        {
            Context = context;
            Mapper = mapper;
            Repository = repository;
        }

        public virtual ProductModel FromEntityToResult(Product entity)
        {
            return Mapper.Map<Product, ProductModel>(entity);
        }

        public virtual Product FromResultToEntity(ProductModel model, int id = 0)
        {
            var entity = Mapper.Map<ProductModel, Product>(model);

            if (id > 0)
                entity.Id = id;

            return entity;
        }



        private async Task<GenericList<ProductModel>> RunPagedQueryable(IQueryable<Product> totalCountQueryable, int skip, int take)
        {
            var currentPageQueryable = totalCountQueryable.Skip(skip).Take(take);

            var resultItems = (await currentPageQueryable.ToListAsync()).Select(FromEntityToResult).ToList();

            var result = new GenericList<ProductModel>(resultItems, await totalCountQueryable.CountAsync());

            return result;
        }

        public async Task<GenericList<ProductModel>> GetAsync(ProductFilterModel filter)
        {
            var query = Repository.GetQueryable();

            if (filter.UnitId > 0)
                query = query.Where(p => p.UnitId == filter.UnitId);

            if (filter.ProductTypeId > 0)
                query = query.Where(p => p.ProductTypeId == filter.ProductTypeId);

            if (filter.Categories != null && filter.Categories.Any())
            {
                //checks count of assigned categories to the product, with the same category IDs as in filter, 
                //compare count from DB and from user request.
                //if match then product have all of filtered categories assigned.
                query = query.Where(p => p.Categories.Count(c => (filter.Categories.Contains(c.CategoryId))) ==
                                         filter.Categories.Count
                                   );
            }

            return await RunPagedQueryable(query, filter.Skip, filter.Take);
        }

        public async Task<GenericList<ProductModel>> GetAvailableAsync(int skip, int take)
        {
            var availableOnlyQuery = Repository.GetQueryable().Where(p => p.IsAvailable == true)
                                                              .OrderBy(p => p.Id);

            return await RunPagedQueryable(availableOnlyQuery, skip, skip);
        }
    }
}