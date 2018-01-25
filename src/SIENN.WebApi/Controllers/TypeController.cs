using Microsoft.AspNetCore.Mvc;
using SIENN.DbAccess.Context;
using SIENN.DbAccess.Entity;
using SIENN.Services.Command;
using SIENN.Services.ControllerServices;
using SIENN.Services.Models;

namespace SIENN.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class TypeController : BaseCrudController<TypeModel, TypeBaseModel, ProductType>
    {
        public TypeController(ICrudControllerService<TypeModel, TypeBaseModel, ProductType> crudControllerService) : base(crudControllerService)
        {

        }        
    }
}
