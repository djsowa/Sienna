using AutoMapper;
using SIENN.DbAccess.Context;
using SIENN.DbAccess.Entity;
using SIENN.DbAccess.Repositories;
using SIENN.Services.Models;

namespace SIENN.Services.ControllerServices
{
    public class ProductTypeCrudControllerService : GenericCrudControlerService<TypeModel, TypeBaseModel, ProductType>
    {
        public ProductTypeCrudControllerService(StoreDbContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public ProductTypeCrudControllerService(StoreDbContext context, IMapper mapper, IGenericRepository<ProductType> repository) : base(context, mapper, repository)
        {

        }
    }
}