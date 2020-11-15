using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Features.Products.Commands.CreateProduct;
using FluentAssertions;
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
    }
}
