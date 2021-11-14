using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Features.Products.Commands.CreateProduct;
using CleanArchitecture.Application.Features.Products.Commands.DeleteProductById;
using CleanArchitecture.Domain.Entities;
using FluentAssertions;
using NUnit.Framework;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.IntegrationTests.Products.Commands
{
    using static Testing;

    public class DeleteProductByIdTests : TestBase
    {
        [Test]
        public void ShouldRequireValidProductId()
        {
            var command = new DeleteProductByIdCommand { Id = 99 };

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().ThrowAsync<NotFoundException>();
        }

        [Test]
        public async Task ShouldDeleteProduct()
        {
            var response = await SendAsync(new CreateProductCommand
            {
                Name = "Product01",
                Description = "Product01 desc",
                Rate = 1,
                Barcode = "p01"
            });

            await SendAsync(new DeleteProductByIdCommand
            {
                Id = response.Data
            });

            var product = await FindAsync<Product>(response.Data);

            product.Should().BeNull();
        }
    }
}
