using AutoMapper;
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
    }
}