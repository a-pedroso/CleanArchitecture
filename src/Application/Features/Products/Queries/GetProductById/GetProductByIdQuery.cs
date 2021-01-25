using CleanArchitecture.Application.Common.Wrappers;
using MediatR;

namespace CleanArchitecture.Application.Features.Products.Queries.GetProductById
{
    public class GetProductByIdQuery : IRequest<Result<GetProductByIdDTO>>
    {
        public long Id { get; set; }
    }
}
