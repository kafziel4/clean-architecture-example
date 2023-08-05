using AutoMapper;
using Catalog.API.DTOs;
using Catalog.Core.Entities;

namespace Catalog.API.Mappings
{
    public class DomainToDtoMappingProfile : Profile
    {
        public DomainToDtoMappingProfile()
        {
            CreateMap<Category, CategoryWithProductsDto>();
            CreateMap<Category, CategoryWithoutProductsDto>();
            CreateMap<CategoryForCreationDto, Category>();
            CreateMap<CategoryForUpdateDto, Category>();

            CreateMap<Product, ProductDto>();
            CreateMap<ProductForCreationDto, Product>();
            CreateMap<ProductForUpdateDto, Product>();
        }

    }
}
