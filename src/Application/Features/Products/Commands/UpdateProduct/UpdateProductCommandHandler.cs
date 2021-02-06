using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Common.Interfaces.Repositories;
using CleanArchitecture.Application.Common.Wrappers;
using CleanArchitecture.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Features.Products.Commands.UpdateProduct
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, Result<long>>
    {
        private readonly IProductRepository _productRepository;
        public UpdateProductCommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<Result<long>> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetByIdAsync(command.Id);

            if (product == null)
            {
                throw new NotFoundException(nameof(Product), command.Id);
            }
            else
            {
                product.Name = command.Name;
                product.Rate = command.Rate;
                product.Description = command.Description;
                await _productRepository.UpdateAsync(product);
                return Result.Ok(product.Id);
            }
        }
    }
}
