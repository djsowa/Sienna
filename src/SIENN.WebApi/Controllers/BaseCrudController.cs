using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SIENN.DbAccess.Context;
using SIENN.DbAccess.Entity;
using SIENN.DbAccess.Repositories;
using SIENN.Services.Command;
using SIENN.Services.ControllerServices;
using SIENN.Services.Models;

namespace SIENN.WebApi.Controllers
{
    public abstract class BaseCrudController<TResultModel, TRequestModel, TEntity> : Controller where TEntity : BaseEntity
                                                                                                where TResultModel : class
                                                                                                where TRequestModel : class
    {
        protected ICrudControllerService<TResultModel, TRequestModel, TEntity> CrudControllerService;

        protected BaseCrudController(ICrudControllerService<TResultModel, TRequestModel, TEntity> crudControllerService)
        {
            CrudControllerService = crudControllerService;
        }

        [HttpGet]
        [Route("{id:int:min(1)}")]
        public virtual async Task<IActionResult> Get([FromRoute] int id)
        {
            var result = await CrudControllerService.GetAsync(id);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpGet]
        [Route("{skip:int:min(0)}/{take:int:min(1):max(200)}")]
        public virtual async Task<IActionResult> List([FromRoute] int skip, [FromRoute] int take)
        {
            var result = await CrudControllerService.List(skip, take);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpPost]
        public virtual async Task<IActionResult> Add([FromBody] TRequestModel record)
        {
            if (!ModelState.IsValid)
                return this.BadRequest("Invalid data.");

            Tuple<int, TResultModel> result = null;

            try
            {
                result = await CrudControllerService.AddAsync(record);
            }
            catch (System.Exception er)
            {
                return BadRequest($"{er.Message}{er.InnerException?.Message}");
            }

            if (result == null)
                return BadRequest("Value wasn't added to database.");

            return CreatedAtAction("Get", new { id = result.Item1 }, result.Item2);
        }

        [HttpPut]
        [Route("{id:int:min(1)}")]
        public async Task<IActionResult> Update([FromRoute]int id, [FromBody] TRequestModel record)
        {
            if (!ModelState.IsValid)
                return this.BadRequest("Invalid data.");

            TResultModel result = null;

            try
            {
                result = await CrudControllerService.UpdateAsync(id, record); ;
            }
            catch (System.Exception er)
            {
                return BadRequest($"{er.Message}{er.InnerException?.Message}");
            }

            return CreatedAtAction("Get", new { id = id }, result);
        }

        [HttpDelete]
        [Route("{id:int:min(1)}")]
        public async Task<IActionResult> Remove([FromRoute]int id)
        {
            var wasDeleted = await CrudControllerService.RemoveAsync(id);

            if (wasDeleted)
                return Ok();

            return BadRequest();
        }
    }
}
