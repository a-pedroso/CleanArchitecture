namespace CleanArchitecture.Application.Features.Products.Commands.DeleteProductById;

using CleanArchitecture.Application.Common.Wrappers;
using MediatR;

public class DeleteProductByIdCommand : IRequest<Result<long>>
{
    public long Id { get; set; }
}
