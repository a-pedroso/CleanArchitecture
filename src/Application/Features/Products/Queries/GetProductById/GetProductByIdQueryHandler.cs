namespace CleanArchitecture.Application.Features.Products.Queries.GetProductById;

using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Common.Wrappers;
using CleanArchitecture.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, Result<GetProductByIdDTO>>
{
    private readonly IProductRepository _productRepository;

    public GetProductByIdQueryHandler(
        IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<Result<GetProductByIdDTO>> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByIdAsync(query.Id);
        if (product == null)
        {
            throw new NotFoundException(nameof(Product), query.Id);
        }

        var dto = GetProductByIdDTO.ToDto(product);

        return Result.Ok(dto);
    }
}
