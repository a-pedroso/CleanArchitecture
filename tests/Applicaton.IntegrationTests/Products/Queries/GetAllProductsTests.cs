using CleanArchitecture.Application.Features.Products.Queries.GetAllProducts;
using CleanArchitecture.Domain.Entities;
using FluentAssertions;
using NUnit.Framework;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.IntegrationTests.Products.Queries
{
    using static Testing;

    public class GetAllProductsTests : TestBase
    {
        [Test]
        public async Task ShouldReturnAllProducts()
        {
            await AddAsync(new Product
            {
                Name = "Product01",
                Description = "Product01 desc",
                Rate = 0,
                Barcode = "12345"
            });

            var query = new GetAllProductsQuery() { PageNumber = 1, PageSize = 10 };

            var result = await SendAsync(query);

            result.Data.Data.Should().HaveCount(1);
        }
    }
}
