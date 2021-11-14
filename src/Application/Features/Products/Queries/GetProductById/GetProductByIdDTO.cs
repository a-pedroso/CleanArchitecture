namespace CleanArchitecture.Application.Features.Products.Queries.GetProductById;

using CleanArchitecture.Domain.Entities;

public record GetProductByIdDTO(
    long Id,
    string Name,
    string Barcode,
    string Description,
    decimal Rate)
{
    public static GetProductByIdDTO ToDto(Product product)
    {
        return new GetProductByIdDTO(
            product.Id,
            product.Name,
            product.Barcode,
            product.Description,
            product.Rate);
    }
}
