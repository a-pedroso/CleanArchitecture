namespace CleanArchitecture.Application.Features.Products.Queries.GetAllProducts
{
    using CleanArchitecture.Application.Common.Wrappers;
    using MediatR;

    public class GetAllProductsQuery : PagedRequest, IRequest<Result<PagedResponse<GetAllProductsDTO>>>
    {
    }
}
