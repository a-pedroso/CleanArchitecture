using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Common.Interfaces.Repositories;
using CleanArchitecture.Application.Common.Wrappers;
using CleanArchitecture.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Features.Products.Queries.GetProductById
{
    public class GetProductByIdQuery : IRequest<Response<Product>>
    {
        public int Id { get; set; }
        public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, Response<Product>>
        {
            private readonly IProductRepositoryAsync _productRepository;
            public GetProductByIdQueryHandler(IProductRepositoryAsync productRepository)
            {
                _productRepository = productRepository;
            }
            public async Task<Response<Product>> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
            {
                var product = await _productRepository.GetByIdAsync(query.Id);
                if (product == null)
                {
                    throw new NotFoundException(nameof(Product), query.Id);
                }
                return Response<Product>.Success(product);
            }
        }
    }
}
