namespace CleanArchitecture.Application.Features.Products.Queries.GetAllProducts
{
    using CleanArchitecture.Application.Common.Wrappers;
    using MediatR;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, Result<PagedResponse<GetAllProductsDTO>>>
    {
        private readonly IProductRepository _productRepository;
        public GetAllProductsQueryHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Result<PagedResponse<GetAllProductsDTO>>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            var pagedResponse = await _productRepository.GetPagedReponseAsync(request.PageNumber, request.PageSize);

            PagedResponse<GetAllProductsDTO> response = new PagedResponse<GetAllProductsDTO>(
                pagedResponse.PageNumber,
                pagedResponse.PageSize,
                pagedResponse.TotalCount,
                pagedResponse.Data.Select(s => GetAllProductsDTO.ToDto(s)).ToList().AsReadOnly());

            return Result.Ok(response);
        }
    }
}
