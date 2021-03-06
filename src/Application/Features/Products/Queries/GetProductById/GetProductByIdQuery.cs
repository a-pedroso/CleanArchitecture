namespace CleanArchitecture.Application.Features.Products.Queries.GetProductById
{
    using CleanArchitecture.Application.Common.Wrappers;
    using MediatR;

    public class GetProductByIdQuery : IRequest<Result<GetProductByIdDTO>>
    {
        public long Id { get; set; }
    }
}
