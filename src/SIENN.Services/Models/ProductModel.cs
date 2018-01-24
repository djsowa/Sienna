using System.Collections.Generic;
using System.Collections;
using SIENN.DbAccess.Entity;
using System.Linq;

namespace SIENN.Services.Models
{
    public class ProductModel
    {
        public ProductModel()
        {
            Categories = new List<int>();
        }

        public int Id { get; set; }
        public string Code { get; set; }
        public virtual List<int> Categories { get; set; }
        public int Unit { get; set; }
        public int Type { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public bool IsAvailable { get; set; }

        public virtual Product ToEntity(int productId = 0)
        {
            var product = new Product()
            {
                Code = Code,
                Description = Description,
                Price = Price,
                IsAvailable = IsAvailable,
                Id = productId == 0 ? Id : productId
            };

            if (productId > 0 || Id > 0)
                Categories.ForEach(c => product.Categories.Add(new ProductToCategory() { ProductId = productId, CategoryId = c }));

            product.Unit = new ProductUnit() { Id = Unit };
            product.ProductType = new ProductType() { Id = Type };

            return product;
        }


        public static ProductModel FromEntity(Product entity)
        {
            var product = new ProductModel()
            {
                Code = entity.Code,
                Description = entity.Description,
                Price = entity.Price,
                IsAvailable = entity.IsAvailable,
                Categories = new List<int>(entity.Categories.Select(c => c.CategoryId)),
                Unit = entity.Unit.Id,
                Type = entity.ProductType.Id
            };

            return product;
        }
    }
}