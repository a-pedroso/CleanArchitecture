using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Features.Products.Commands.CreateProduct;
using CleanArchitecture.Application.Features.Products.Commands.UpdateProduct;
using CleanArchitecture.Domain.Entities;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.IntegrationTests.Products.Commands
{
    using static Testing;

    public class UpdateProductTests : TestBase
    {
        [Test]
        public void ShouldRequireMinimumFields()
        {
            var command = new UpdateProductCommand();

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().Throw<ValidationException>();
        }

        [Test]
        public void ShouldRequireValidProductId()
        {
            var command = new UpdateProductCommand
            {
                Id = 99,
                Name = "New Name",
                Description = "New Desc"
            };

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().Throw<NotFoundException>();
        }

        [Test]
        public async Task ShouldUpdateProduct()
        {
            var userId = RunAsDefaultUserAsync();

            var response = await SendAsync(new CreateProductCommand
            {
                Name = "Product01",
                Description = "Product01 desc",
                Rate = 1,
                Barcode = "p01"
            });

            var command = new UpdateProductCommand
            {
                Id = response.Data,
                Name = "Updated Product Name",
                Description = "Updated Desc"
            };

            await SendAsync(command);

            var product = await FindAsync<Product>(response.Data);

            product.Name.Should().Be(command.Name);
            product.Description.Should().Be(command.Description);
            product.LastModifiedBy.Should().NotBeNull();
            product.LastModifiedBy.Should().Be(userId);
            product.LastModified.Should().NotBeNull();
            product.LastModified.Should().BeCloseTo(DateTime.Now, 1000);
        }
    }
}
