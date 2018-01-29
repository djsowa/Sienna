using AutoMapper;
using SIENN.DbAccess.Context;
using SIENN.DbAccess.Entity;
using SIENN.DbAccess.Repositories;
using SIENN.Services.Models;

namespace SIENN.Services.ControllerServices.Crud
{
    public class ProductTypeCrudControllerService : GenericCrudControlerService<TypeModel, TypeBaseModel, ProductType>
    {
        public ProductTypeCrudControllerService(StoreDbContext context, IMapper mapper, IGenericRepository<ProductType> repository) : base(context, mapper, repository)
        {

        }
    }
}