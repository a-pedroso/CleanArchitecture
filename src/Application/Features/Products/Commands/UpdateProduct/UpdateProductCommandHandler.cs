namespace CleanArchitecture.Application.Features.Products.Commands.UpdateProduct;

using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Common.Wrappers;
using CleanArchitecture.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, Result<long>>
{
    private readonly IProductRepository _productRepository;
    public UpdateProductCommandHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }
    public async Task<Result<long>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByIdAsync(request.Id);

        if (product == null)
        {
            throw new NotFoundException(nameof(Product), request.Id);
        }
        else
        {
            product.Name = request.Name;
            product.Rate = request.Rate;
            product.Description = request.Description;
            await _productRepository.UpdateAsync(product);
            return Result.Ok(product.Id);
        }
    }
}
