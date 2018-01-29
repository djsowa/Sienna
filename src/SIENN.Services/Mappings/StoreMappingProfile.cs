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
            AllowNullCollections = true;
            AllowNullDestinationValues = true;
            this.EnableNullPropagationForQueryMapping = true;

            CreateMap<ProductBaseModel, Product>()
                     .ForMember(p => p.Categories, m => m.ResolveUsing<ProductBaseModelToCategoryResolver>())
                     .ForMember(c => c.ProductType, m => m.Ignore())
                     .ForMember(c => c.Unit, m => m.Ignore())
                     .ForMember(c => c.Id, m => m.Ignore());


            CreateMap<ProductModel, Product>()
                    .ForMember(p => p.Categories, m => m.ResolveUsing<ProductModelToCategoryResolver>())
                    .ForMember(c => c.ProductType, m => m.Ignore())
                    .ForMember(c => c.Unit, m => m.Ignore());

            CreateMap<Product, ProductModel>()
                    .ForMember(p => p.Categories, m => m.MapFrom(s => s.Categories.Select(c => c.CategoryId)));

            CreateMap<CategoryBaseModel, ProductCategory>()
                     .ForMember(c => c.Products, m => m.Ignore())
                     .ForMember(c => c.Id, m => m.Ignore());

            CreateMap<CategoryModel, ProductCategory>();
            CreateMap<ProductCategory, CategoryModel>();


            CreateMap<UnitBaseModel, ProductUnit>()
                    .ForMember(c => c.Id, m => m.Ignore())
                    .ForMember(c => c.Products, m => m.Ignore());

            CreateMap<UnitModel, ProductUnit>()
                    .ForMember(c => c.Products, m => m.Ignore());
            CreateMap<ProductUnit, UnitModel>();

            CreateMap<TypeBaseModel, ProductType>()
                    .ForMember(c => c.Id, m => m.Ignore())
                    .ForMember(c => c.Products, m => m.Ignore());

            CreateMap<TypeModel, ProductType>().ForMember(c => c.Products, m => m.Ignore());
            CreateMap<ProductType, TypeModel>();
        }
    }
}