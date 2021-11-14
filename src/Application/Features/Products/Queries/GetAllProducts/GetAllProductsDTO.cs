namespace CleanArchitecture.Application.Features.Products.Queries.GetAllProducts;

using CleanArchitecture.Domain.Entities;

public record GetAllProductsDTO(
    long Id,
    string Name,
    string Barcode,
    string Description,
    decimal Rate)
{
    public static GetAllProductsDTO ToDto(Product product)
    {
        return new GetAllProductsDTO(
            product.Id,
            product.Barcode,
            product.Description,
            product.Name,
            product.Rate);
    }
}
