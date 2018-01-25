using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using SIENN.DbAccess.Entity;
using SIENN.Services.Models;

namespace SIENN.Services.Mappings
{


    public class ProductBaseModelToCategoryResolver : IValueResolver<ProductBaseModel, Product, ICollection<ProductToCategory>>
    {
        public ProductBaseModelToCategoryResolver()
        {

        }

        public ICollection<ProductToCategory> Resolve(ProductBaseModel source, Product destination, ICollection<ProductToCategory> destMember, ResolutionContext context)
        {
            var result = new List<ProductToCategory>(source.Categories.Select(c => new ProductToCategory(destination, c)));
            destination.Categories.Clear();
            result.ForEach(x => destination.Categories.Add(x));
            return result;
        }
    }

    public class ProductModelToCategoryResolver : IValueResolver<ProductModel, Product, ICollection<ProductToCategory>>
    {
        public ProductModelToCategoryResolver()
        {

        }

        public ICollection<ProductToCategory> Resolve(ProductModel source, Product destination, ICollection<ProductToCategory> destMember, ResolutionContext context)
        {
            var result = new List<ProductToCategory>(source.Categories.Select(c => new ProductToCategory(source.Id, c)));
            destination.Categories.Clear();
            result.ForEach(x => destination.Categories.Add(x));
            return result;
        }
    }
}