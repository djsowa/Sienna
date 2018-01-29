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
    public class StoreController : Controller
    {

        private StoreDbContext _context;
        private IStoreControllerService<ProductViewModel, ProductFilterModel, Product> _storeControllerService;

        public StoreController(StoreDbContext context, IStoreControllerService<ProductViewModel, ProductFilterModel, Product> storeControllerService)
        {
            _context = context;
            _storeControllerService = storeControllerService;
        }

        [HttpGet]
        [Route("[action]/{skip:int:min(0)}/{take:int:min(1):max(50)}")]
        public virtual async Task<IActionResult> Products([FromRoute] int skip, [FromRoute] int take)
        {
            if (!ModelState.IsValid)
                return BadRequest("Bad query.");

            var result = await _storeControllerService.GetView(new ProductFilterModel() { Skip = skip, Take = take });

            if (result == null)
                return NotFound();

            return Ok(result);
        }
    }
}
