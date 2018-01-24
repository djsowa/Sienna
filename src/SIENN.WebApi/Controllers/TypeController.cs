using Microsoft.AspNetCore.Mvc;
using SIENN.DbAccess.Context;
using SIENN.DbAccess.Entity;
using SIENN.Services.Command;
using SIENN.Services.Models;

namespace SIENN.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class TypeController : BaseCrudController<TypeModel, ProductType>
    {
        public TypeController(StoreDbContext context) : base(context)
        {

        }

        protected override ProductType FromModel(TypeModel model, int id = 0)
        {
            return new ProductType()
            {
                Code = model.Code,
                Description = model.Description,
                Id = id == 0 ? model.Id : id
            };
        }

        protected override TypeModel ToModel(ProductType entity)
        {
            return new TypeModel()
            {
                Code = entity.Code,
                Description = entity.Description,
                Id = entity.Id
            };
        }
    }
}
