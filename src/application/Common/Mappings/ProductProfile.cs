using AutoMapper;
using CleanArchitecture.Application.Features.Products.Commands.CreateProduct;
using CleanArchitecture.Application.Features.Products.Queries.GetAllProducts;
using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Application.Common.Mappings
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, GetAllProductsViewModel>().ReverseMap();
            CreateMap<CreateProductCommand, Product>();
            CreateMap<GetAllProductsQuery, GetAllProductsParameter>();
        }
    }
}
