using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SIENN.DbAccess.Context;
using SIENN.DbAccess.Entity;
using SIENN.Services.Command;
using SIENN.Services.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using SIENN.DbAccess.Repositories;
using SIENN.Services.ControllerServices.Crud;
using SIENN.Services.ControllerServices.Search;

namespace SIENN.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class ProductController : BaseCrudController<ProductModel, ProductBaseModel, Product>
    {
        private readonly IProductSearchControllerService<ProductModel, ProductFilterModel> _searchControllerService;
        public ProductController(ICrudControllerService<ProductModel, ProductBaseModel, Product> crudControllerService,
                                 IProductSearchControllerService<ProductModel, ProductFilterModel> searchControllerService) : base(crudControllerService)
        {
            _searchControllerService = searchControllerService;
        }


        [HttpGet]
        [Route("[action]")]
        public virtual async Task<IActionResult> Search([FromQuery]ProductFilterModel filter)
        {
            if (!ModelState.IsValid)
                return BadRequest("Bad query.");

            var result = await _searchControllerService.GetAsync(filter);

            if (result == null)
                return NotFound();

            return Ok(result);
        }


        [HttpGet]
        [Route("[action]/{skip:int:min(0)}/{take:int:min(1):max(50)}")]
        public virtual async Task<IActionResult> Available([FromRoute] int skip, [FromRoute] int take)
        {
            var result = await _searchControllerService.GetAvailableAsync(skip, take);

            if (result == null)
                return NotFound();

            return Ok(result);
        }
    }
}
