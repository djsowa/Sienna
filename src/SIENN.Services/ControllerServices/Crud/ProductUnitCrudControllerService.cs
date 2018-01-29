using AutoMapper;
using SIENN.DbAccess.Context;
using SIENN.DbAccess.Entity;
using SIENN.DbAccess.Repositories;
using SIENN.Services.Models;

namespace SIENN.Services.ControllerServices.Crud
{
    public class ProductUnitCrudControllerService : GenericCrudControlerService<UnitModel, UnitBaseModel, ProductUnit>
    {
        public ProductUnitCrudControllerService(StoreDbContext context, IMapper mapper, IGenericRepository<ProductUnit> repository) : base(context, mapper, repository)
        {

        }
    }
}