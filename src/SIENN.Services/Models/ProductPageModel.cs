using System.Collections.Generic;
using SIENN.DbAccess.Entity;
using System.Linq;

namespace SIENN.Services.Models
{
    public class ProductPageModel : ProductModel
    {
        public ProductPageModel()
        {
            CategoriesDictionary = new List<ProductDictionaryModel>();
        }
        public List<ProductDictionaryModel> CategoriesDictionary { get; set; }
        public ProductDictionaryModel UnitDictionary { get; set; }
        public ProductDictionaryModel TypeDictionary { get; set; }


        public new static ProductPageModel FromEntity(Product entity)
        {
            var product = new ProductPageModel()
            {
                Id = entity.Id,
                Code = entity.Code,
                Description = entity.Description,
                Price = entity.Price,
                IsAvailable = entity.IsAvailable,
                Categories = new List<int>(entity.Categories.Select(c => c.CategoryId)),
                Unit = entity.Unit.Id,
                Type = entity.ProductType.Id,
                CategoriesDictionary = new List<ProductDictionaryModel>(entity.Categories.Select(c => new ProductDictionaryModel(c.CategoryId, c.Category.Code, c.Category.Description))),
                UnitDictionary = new ProductDictionaryModel(entity.Unit.Id, entity.Unit.Code, entity.Unit.Description),
                TypeDictionary = new ProductDictionaryModel(entity.ProductType.Id, entity.ProductType.Code, entity.ProductType.Description),
            };

            return product;
        }
    }
}