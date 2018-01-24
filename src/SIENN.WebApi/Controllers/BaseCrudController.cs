using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SIENN.DbAccess.Context;
using SIENN.DbAccess.Entity;
using SIENN.Services.Command;

namespace SIENN.WebApi.Controllers
{
    public abstract class BaseCrudController<TModel, TEntity> : Controller where TEntity : BaseEntity
                                                                           where TModel : class
    {
        protected readonly StoreDbContext _context;
        protected readonly GenericCrudCommand<TEntity> _commad;
        protected BaseCrudController(StoreDbContext context)
        {
            _context = context;
            _commad = new GenericCrudCommand<TEntity>(_context);
        }

        protected BaseCrudController(StoreDbContext context, GenericCrudCommand<TEntity> command)
        {
            _context = context;
            _commad = command;
        }

        [HttpGet]
        [Route("{id:int:min(1)}")]
        public virtual async Task<IActionResult> Get([FromRoute] int id)
        {
            var result = await _commad.GetAsync(id);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(ToModel(result));
        }

        [HttpPost]
        public virtual async Task<IActionResult> Add([FromBody] TModel record)
        {
            var id = await _commad.AddAsync(FromModel(record));

            var entity = await _commad.GetAsync(id);

            return CreatedAtAction("Get", new { id = id }, ToModel(entity));
        }

        [HttpPut]
        [Route("{id:int:min(1)}")]
        public async Task<IActionResult> Update([FromRoute]int id, [FromBody] TModel record)
        {
            await _commad.UpdateAsync(FromModel(record, id));

            var entity = await _commad.GetAsync(id);

            return CreatedAtAction("Get", new { id = id }, ToModel(entity));
        }

        [HttpDelete]
        [Route("{id:int:min(1)}")]
        public async Task<IActionResult> Remove([FromRoute]int id)
        {
            var entity = await _commad.GetAsync(id);

            await _commad.RemoveAsync(entity);

            return Ok();
        }

        protected abstract TEntity FromModel(TModel model, int id = 0);
        protected abstract TModel ToModel(TEntity entity);

    }
}
