using Microsoft.AspNetCore.Mvc;
using SIENN.DbAccess.Context;
using SIENN.DbAccess.Entity;
using SIENN.Services.Command;
using SIENN.Services.ControllerServices.Crud;
using SIENN.Services.Models;

namespace SIENN.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class UnitController : BaseCrudController<UnitModel, UnitBaseModel, ProductUnit>
    {
        public UnitController(ICrudControllerService<UnitModel, UnitBaseModel, ProductUnit> crudControllerService) : base(crudControllerService)
        {

        }        
    }
}
