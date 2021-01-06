using AutoMapper;
using CleanArchitecture.Application.Common.DTO;
using CleanArchitecture.Application.Common.Interfaces.Repositories;
using CleanArchitecture.Application.Common.Wrappers;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Features.Products.Queries.GetAllProducts
{
    public class GetAllProductsQuery : PagedRequest, IRequest<PagedResponse<IEnumerable<ProductDTO>>>
    {
    }

    public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, PagedResponse<IEnumerable<ProductDTO>>>
    {
        private readonly IProductRepositoryAsync _productRepository;
        private readonly IMapper _mapper;
        public GetAllProductsQueryHandler(IProductRepositoryAsync productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<PagedResponse<IEnumerable<ProductDTO>>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            var pagedResponse = await _productRepository.GetPagedReponseAsync(request.PageNumber, request.PageSize);
            var productViewModel = _mapper.Map<IEnumerable<ProductDTO>>(pagedResponse.Data);
            
            return PagedResponse<IEnumerable<ProductDTO>>.Success(productViewModel, request.PageNumber, request.PageSize, pagedResponse.TotalCount);
        }
    }
}
