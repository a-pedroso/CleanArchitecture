using CleanArchitecture.Domain.Entities;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Common.Interfaces.Repositories
{
    public interface IProductRepositoryAsync : IGenericRepositoryAsync<Product, long>
    {
        Task<bool> IsUniqueBarcodeAsync(string barcode);
    }
}
