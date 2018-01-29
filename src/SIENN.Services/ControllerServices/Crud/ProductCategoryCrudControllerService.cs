using AutoMapper;
using SIENN.DbAccess.Context;
using SIENN.DbAccess.Entity;
using SIENN.DbAccess.Repositories;
using SIENN.Services.Models;

namespace SIENN.Services.ControllerServices.Crud
{
    public class ProductCategoryCrudControllerService : GenericCrudControlerService<CategoryModel, CategoryBaseModel, ProductCategory>
    {
        public ProductCategoryCrudControllerService(StoreDbContext context, IMapper mapper, IGenericRepository<ProductCategory> repository) : base(context, mapper, repository)
        {

        }
    }
}