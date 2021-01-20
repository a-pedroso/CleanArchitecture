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
    public class GetAllProductsQuery : PagedRequest, IRequest<Result<PagedResponse<ProductDTO>>>
    {
    }

    public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, Result<PagedResponse<ProductDTO>>>
    {
        private readonly IProductRepositoryAsync _productRepository;
        private readonly IMapper _mapper;
        public GetAllProductsQueryHandler(IProductRepositoryAsync productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<Result<PagedResponse<ProductDTO>>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            var pagedResponse = await _productRepository.GetPagedReponseAsync(request.PageNumber, request.PageSize);

            PagedResponse<ProductDTO> response = new PagedResponse<ProductDTO>(
                pagedResponse.PageNumber,
                pagedResponse.PageSize,
                pagedResponse.TotalCount,
                _mapper.Map<IReadOnlyList<ProductDTO>>(pagedResponse.Data));

            return Result.Ok(response);
        }
    }
}
