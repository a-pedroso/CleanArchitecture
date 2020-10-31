﻿using AutoMapper;
using CleanArchitecture.Application.Common.DTO;
using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Common.Interfaces.Repositories;
using CleanArchitecture.Application.Common.Wrappers;
using CleanArchitecture.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Features.Products.Queries.GetProductById
{
    public class GetProductByIdQuery : IRequest<Response<ProductDTO>>
    {
        public int Id { get; set; }
    }

    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, Response<ProductDTO>>
    {
        private readonly IProductRepositoryAsync _productRepository;
        private readonly IMapper _mapper;

        public GetProductByIdQueryHandler(
            IProductRepositoryAsync productRepository,
            IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<Response<ProductDTO>> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetByIdAsync(query.Id);
            if (product == null)
            {
                throw new NotFoundException(nameof(Product), query.Id);
            }

            var dto = _mapper.Map<ProductDTO>(product);
            
            return Response<ProductDTO>.Success(dto);
        }
    }
}
