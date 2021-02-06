using CleanArchitecture.Application.Features.Products;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace CleanArchitecture.Infrastructure.Persistence.Repositories
{
    public class ProductRepository : GenericRepository<Product, long>, IProductRepository
    {
        private readonly DbSet<Product> _products;

        public ProductRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _products = dbContext.Set<Product>();
        }

        public async Task<bool> IsUniqueBarcodeAsync(string barcode)
        {
            return await _products.AllAsync(p => p.Barcode != barcode);
        }
    }
}
