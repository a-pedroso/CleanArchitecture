using CleanArchitecture.Application.Common.Wrappers;
using MediatR;

namespace CleanArchitecture.Application.Features.Products.Queries.GetAllProducts
{
    public class GetAllProductsQuery : PagedRequest, IRequest<Result<PagedResponse<GetAllProductsDTO>>>
    {
    }
}
