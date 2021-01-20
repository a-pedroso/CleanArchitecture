using CleanArchitecture.Application.Common.Interfaces.Repositories;
using CleanArchitecture.Application.Common.Wrappers;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Features.Products.Queries.GetAllProducts
{
    public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, Result<PagedResponse<GetAllProductsDTO>>>
    {
        private readonly IProductRepositoryAsync _productRepository;
        public GetAllProductsQueryHandler(IProductRepositoryAsync productRepository)
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
