using CleanArchitecture.Application.Common.Interfaces.Repositories;
using CleanArchitecture.Domain.Entities;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Features.Products
{
    public interface IProductRepository : IGenericRepository<Product, long>
    {
        Task<bool> IsUniqueBarcodeAsync(string barcode);
    }
}
