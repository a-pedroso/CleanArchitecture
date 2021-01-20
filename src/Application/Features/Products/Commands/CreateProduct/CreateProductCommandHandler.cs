using CleanArchitecture.Application.Common.Interfaces.Repositories;
using CleanArchitecture.Application.Common.Wrappers;
using CleanArchitecture.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Features.Products.Commands.CreateProduct
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Result<long>>
    {
        private readonly IProductRepositoryAsync _productRepository;
        public CreateProductCommandHandler(IProductRepositoryAsync productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Result<long>> Handle(CreateProductCommand cmd, CancellationToken cancellationToken)
        {
            var product = new Product()
            {
                Name = cmd.Name,
                Barcode = cmd.Barcode,
                Description = cmd.Description,
                Rate = cmd.Rate
            };
            await _productRepository.AddAsync(product);
            return Result.Ok(product.Id);
        }
    }
}
