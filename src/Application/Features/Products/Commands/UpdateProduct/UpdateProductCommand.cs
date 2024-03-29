﻿namespace CleanArchitecture.Application.Features.Products.Commands.UpdateProduct;

using CleanArchitecture.Application.Common.Wrappers;
using MediatR;

public class UpdateProductCommand : IRequest<Result<long>>
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Rate { get; set; }
}
