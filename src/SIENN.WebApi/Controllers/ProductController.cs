using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SIENN.DbAccess.Context;
using SIENN.DbAccess.Entity;
using SIENN.Services.Command;
using SIENN.Services.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace SIENN.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class ProductController : BaseCrudController<ProductModel, Product>
    {
        public ProductController(StoreDbContext context) : base(context)
        {

        }

        [HttpGet]
        [Route("{id:int:min(1)}")]
        public override async Task<IActionResult> Get([FromRoute] int id)
        {
            var query = _commad.GetQueryable();

            var result = await query.Where(p => p.Id == id)
                                    .Include(p => p.Unit)
                                    .Include(p => p.ProductType)
                                    .Include(p => p.Categories)
                                    .ThenInclude(c => c.Category)
                                    .SingleOrDefaultAsync();

            if (result == null)
            {
                return NotFound();
            }

            return Ok(ToPageModel(result));
        }

        [HttpPost]
        public override async Task<IActionResult> Add([FromBody] ProductModel record)
        {
            //Map to Entity
            var productEntity = FromModel(record);

            //get list of connected categories
            var categories = await _context.Categories.Where(c => record.Categories.Contains(c.Id)).ToListAsync();

            //assign navigation property
            productEntity.Unit = _context.Unints.Single(unit => unit.Id == record.Unit);
            productEntity.ProductType = _context.ProductTypes.Single(unit => unit.Id == record.Type);

            await _commad.AddAsync(productEntity);

            categories.ForEach(c => productEntity.Categories.Add(new ProductToCategory(productEntity.Id, c.Id)));

            await _commad.UpdateAsync(productEntity);

            return CreatedAtAction("Get", new { id = productEntity.Id }, ToPageModel(productEntity));
        }
        protected override Product FromModel(ProductModel model, int id = 0)
        {
            return model.ToEntity(id);
        }

        protected override ProductModel ToModel(Product entity)
        {
            return ProductModel.FromEntity(entity);
        }

        protected ProductPageModel ToPageModel(Product entity)
        {
            return ProductPageModel.FromEntity(entity);
        }
    }
}
