using CleanArchitecture.Application.Common.Wrappers;
using MediatR;

namespace CleanArchitecture.Application.Features.Products.Commands.DeleteProductById
{
    public class DeleteProductByIdCommand : IRequest<Result<long>>
    {
        public long Id { get; set; }
    }
}
