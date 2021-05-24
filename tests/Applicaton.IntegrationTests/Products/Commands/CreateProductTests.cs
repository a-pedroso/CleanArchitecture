using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Features.Products.Commands.CreateProduct;
using CleanArchitecture.Domain.Entities;
using FluentAssertions;
using FluentValidation;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.IntegrationTests.Products.Commands
{
    using static Testing;

    public class CreateProductTests : TestBase
    {
        [Test]
        public void ShouldRequireMinimumFields()
        {
            var command = new CreateProductCommand();

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().Throw<ValidationException>();
        }

        [Test]
        public async Task ShouldRequireUniqueBarcode()
        {
            await SendAsync(new CreateProductCommand
            {
                Name = "Product01",
                Description = "Product01 desc",
                Rate = 0,
                Barcode = "12345"
            });

            var command = new CreateProductCommand
            {
                Name = "Product02",
                Description = "Product02 desc",
                Rate = 0,
                Barcode = "12345"
            };

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().Throw<ValidationException>();
        }

        [Test]
        public async Task ShouldCreateProduct()
        {
            var userId = RunAsDefaultUserAsync();

            var command = new CreateProductCommand
            {
                Name = "Product01",
                Description = "Product01 desc",
                Rate = 1,
                Barcode = "p01"
            };

            var response = await SendAsync(command);

            var product = await FindAsync<Product>(response.Data);
            
            product.Should().NotBeNull();
            product.Id.Should().Be(response.Data);
            product.Name.Should().Be(command.Name);
            product.Rate.Should().Be(command.Rate);
            product.Description.Should().Be(command.Description);
            product.Barcode.Should().Be(command.Barcode);
            product.CreatedBy.Should().Be(userId);
            product.Created.Should().BeCloseTo(DateTime.Now, 10000);
        }
    }
}
