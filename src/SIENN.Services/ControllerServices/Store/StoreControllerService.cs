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
    public class StoreControllerService : IStoreControllerService<ProductViewModel, ProductFilterModel, Product>
    {
        protected readonly StoreDbContext Context;
        protected readonly IMapper Mapper;
        protected IGenericRepository<Product> ProductRepository;

        public StoreControllerService(StoreDbContext context, IMapper mapper, IGenericRepository<Product> repository)
        {
            Context = context;
            Mapper = mapper;
            ProductRepository = repository;
        }

        private async Task<GenericList<ProductViewModel>> RunPagedQueryable(IQueryable<Product> totalCountQueryable, int skip, int take)
        {
            var currentPageQueryable = totalCountQueryable.Skip(skip).Take(take);

            var resultItems = (await currentPageQueryable.ToListAsync()).Select(FromEntityToViewResult).ToList();

            var result = new GenericList<ProductViewModel>(resultItems, await totalCountQueryable.CountAsync());

            return result;
        }

        public async Task<GenericList<ProductViewModel>> GetView(ProductFilterModel filter)
        {
            var query = ProductRepository.GetQueryable().OrderBy(p=>p.Id);

            return await RunPagedQueryable(query, filter.Skip, filter.Take);
        }

        public Product FromViewResultToEntity(ProductViewModel model, int id = 0)
        {
            throw new NotImplementedException();
        }

        public ProductViewModel FromEntityToViewResult(Product entity)
        {
            var model = Mapper.Map<Product, ProductModel>(entity);
            var result = new ProductViewModel(model, entity.ProductType.Code, entity.ProductType.Description,
                                                     entity.Unit.Code, entity.Unit.Description);

            return result;
        }
    }
}