using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SIENN.DbAccess.Context;
using SIENN.DbAccess.Entity;
using SIENN.DbAccess.Repositories;
using SIENN.Services.Models;

namespace SIENN.Services.ControllerServices
{
    public class ProductCrudControllerService : GenericCrudControlerService<ProductModel, ProductBaseModel, Product>
    {
        public ProductCrudControllerService(StoreDbContext context, IMapper mapper) : base(context, mapper)
        {

        }

        public ProductCrudControllerService(StoreDbContext context, IMapper mapper, IGenericRepository<Product> repository) : base(context, mapper, repository)
        {

        }

        public override async Task<Tuple<int, ProductModel>> AddAsync(ProductBaseModel request)
        {
            await ValidateAddOrUpdate(request);

            return await base.AddAsync(request);
        }

        public override async Task<ProductModel> UpdateAsync(int id, ProductBaseModel request)
        {
            await ValidateAddOrUpdate(request);

            return await base.UpdateAsync(id, request);
        }


        //Checks if foregin key IDs exists in DB
        protected virtual async Task ValidateAddOrUpdate(ProductBaseModel model)
        {
            if (!Context.ProductTypes.Any(x => x.Id == model.ProductTypeId))
                throw new InvalidOperationException("Selected productTypeId for product doesn't exists.");

            if (!Context.Unints.Any(x => x.Id == model.UnitId))
                throw new InvalidOperationException("Selected unitId for product doesn't exists.");

            //check if all categories selected for product exists in DB.
            var exitingCount = await Context.Categories.CountAsync(c => (model.Categories.Contains(c.Id)));

            if (exitingCount != model.Categories.Count)
                throw new InvalidOperationException("One or more of selected categories for product doesn't exists.");
        }
    }
}