namespace CleanArchitecture.Application.Features.Products.Queries.GetAllProducts
{
    using CleanArchitecture.Domain.Entities;

    public record GetAllProductsDTO
    {
        public long Id { get; init; }
        public string Name { get; init; }
        public string Barcode { get; init; }
        public string Description { get; init; }
        public decimal Rate { get; init; }

        public static GetAllProductsDTO ToDto(Product product)
        {
            return new GetAllProductsDTO()
            {
                Id = product.Id,
                Barcode = product.Barcode,
                Description = product.Description,
                Name = product.Name,
                Rate = product.Rate
            };
        }
    }
}
