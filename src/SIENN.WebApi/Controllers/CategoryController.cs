using Microsoft.AspNetCore.Mvc;
using SIENN.DbAccess.Context;
using SIENN.DbAccess.Entity;
using SIENN.Services.Command;
using SIENN.Services.Models;

namespace SIENN.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class CategoryController : BaseCrudController<CategoryModel, ProductCategory>
    {
        public CategoryController(StoreDbContext context) : base(context)
        {

        }

        protected override ProductCategory FromModel(CategoryModel model, int id = 0)
        {
            return new ProductCategory()
            {
                Code = model.Code,
                Description = model.Description,
                Id = id == 0 ? model.Id : id
            };
        }

        protected override CategoryModel ToModel(ProductCategory entity)
        {
            return new CategoryModel()
            {
                Code = entity.Code,
                Description = entity.Description,
                Id = entity.Id
            };
        }
    }
}
