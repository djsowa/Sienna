using System.Linq;
using AutoMapper;
using SIENN.DbAccess.Entity;
using SIENN.Services.Models;

namespace SIENN.Services.Mappings
{
    public class StoreMappingProfile : Profile
    {
        public StoreMappingProfile()
        {

            CreateMap<ProductBaseModel, Product>()
                     .ForMember(p => p.Categories, m => m.ResolveUsing<ProductBaseModelToCategoryResolver>());

            CreateMap<ProductModel, Product>()
                    .ForMember(p => p.Categories, m => m.ResolveUsing<ProductModelToCategoryResolver>());

            CreateMap<Product, ProductModel>()
                    .ForMember(p => p.Categories, m => m.MapFrom(s => s.Categories.Select(c => c.CategoryId)));

            CreateMap<CategoryBaseModel, ProductCategory>();
            CreateMap<CategoryModel, ProductCategory>();
            CreateMap<ProductCategory, CategoryModel>();

            CreateMap<UnitBaseModel, ProductUnit>();
            CreateMap<UnitModel, ProductUnit>();
            CreateMap<ProductUnit, UnitModel>();

            CreateMap<TypeBaseModel, ProductType>();
            CreateMap<TypeModel, ProductType>();
            CreateMap<ProductType, TypeModel>();
        }
    }
}