using Microsoft.AspNetCore.Mvc;
using SIENN.DbAccess.Context;
using SIENN.DbAccess.Entity;
using SIENN.Services.Command;
using SIENN.Services.ControllerServices;
using SIENN.Services.Models;

namespace SIENN.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class CategoryController : BaseCrudController<CategoryModel, CategoryBaseModel, ProductCategory>
    {
        public CategoryController(ICrudControllerService<CategoryModel, CategoryBaseModel, ProductCategory> crudControllerService) : base(crudControllerService)
        {

        }       
    }
}
