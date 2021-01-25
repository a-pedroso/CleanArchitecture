//using AutoMapper;
//using CleanArchitecture.Application.Common.DTO;
//using CleanArchitecture.Application.Features.Products.Commands.CreateProduct;
//using CleanArchitecture.Domain.Entities;

//namespace CleanArchitecture.Application.Common.Mappings
//{
//    public class ProductProfile : Profile
//    {
//        public ProductProfile()
//        {
//            CreateMap<Product, ProductDTO>()
//                .ReverseMap()
//                .ForMember(x => x.Created, opt => opt.Ignore())
//                .ForMember(x => x.CreatedBy, opt => opt.Ignore())
//                .ForMember(x => x.LastModified, opt => opt.Ignore())
//                .ForMember(x => x.LastModifiedBy, opt => opt.Ignore());

//            CreateMap<CreateProductCommand, Product>()
//                .ForMember(x => x.Id, opt => opt.Ignore())
//                .ForMember(x => x.Created, opt => opt.Ignore())
//                .ForMember(x => x.CreatedBy, opt => opt.Ignore())
//                .ForMember(x => x.LastModified, opt => opt.Ignore())
//                .ForMember(x => x.LastModifiedBy, opt => opt.Ignore());
//        }
//    }
//}
