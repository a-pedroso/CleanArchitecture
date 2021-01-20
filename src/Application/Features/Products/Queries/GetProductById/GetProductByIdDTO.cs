using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Application.Features.Products.Queries.GetProductById
{
    public record GetProductByIdDTO
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Barcode { get; set; }
        public string Description { get; set; }
        public decimal Rate { get; set; }

        public static GetProductByIdDTO ToDto(Product product)
        {
            return new GetProductByIdDTO()
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
