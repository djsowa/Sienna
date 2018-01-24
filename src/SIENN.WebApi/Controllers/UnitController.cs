using Microsoft.AspNetCore.Mvc;
using SIENN.DbAccess.Context;
using SIENN.DbAccess.Entity;
using SIENN.Services.Command;
using SIENN.Services.Models;

namespace SIENN.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class UnitController : BaseCrudController<UnitModel, ProductUnit>
    {
        public UnitController(StoreDbContext context) : base(context)
        {

        }

        protected override ProductUnit FromModel(UnitModel model, int id = 0)
        {
            return new ProductUnit()
            {
                Code = model.Code,
                Description = model.Description,
                Id = id == 0 ? model.Id : id
            };
        }

        protected override UnitModel ToModel(ProductUnit entity)
        {
            return new UnitModel()
            {
                Code = entity.Code,
                Description = entity.Description,
                Id = entity.Id
            };
        }
    }
}
