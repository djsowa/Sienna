using AutoMapper;
using SIENN.DbAccess.Entity;
using SIENN.Services.Models;

namespace SIENN.Services.Mappings
{
    public class StoreMappingProfile : Profile
    {
        public StoreMappingProfile()
        {
            CreateMap<ProductBaseModel, Product>();
            CreateMap<ProductModel, Product>();
            CreateMap<Product, ProductModel>();

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