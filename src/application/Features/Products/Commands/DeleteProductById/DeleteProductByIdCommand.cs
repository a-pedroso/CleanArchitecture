using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Common.Interfaces.Repositories;
using CleanArchitecture.Application.Common.Wrappers;
using CleanArchitecture.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Features.Products.Commands.DeleteProductById
{
    public class DeleteProductByIdCommand : IRequest<Response<long>>
    {
        public long Id { get; set; }
    }

    public class DeleteProductByIdCommandHandler : IRequestHandler<DeleteProductByIdCommand, Response<long>>
    {
        private readonly IProductRepositoryAsync _productRepository;
        public DeleteProductByIdCommandHandler(IProductRepositoryAsync productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<Response<long>> Handle(DeleteProductByIdCommand command, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetByIdAsync(command.Id);
            if (product == null)
            {
                throw new NotFoundException(nameof(Product), command.Id);
            }

            await _productRepository.DeleteAsync(product);
            return Response<long>.Success(product.Id);
        }
    }
}
