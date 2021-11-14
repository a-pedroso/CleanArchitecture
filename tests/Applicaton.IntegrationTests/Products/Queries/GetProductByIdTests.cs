using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Features.Products.Commands.CreateProduct;
using CleanArchitecture.Application.Features.Products.Queries.GetProductById;
using FluentAssertions;
using NUnit.Framework;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.IntegrationTests.Products.Queries;

using static Testing;

public class GetProductByIdTests : TestBase
{
    [Test]
    public void ShouldRequireValidProductId()
    {
        var command = new GetProductByIdQuery { Id = 99 };

        FluentActions.Invoking(() =>
            SendAsync(command)).Should().ThrowAsync<NotFoundException>();
    }

    [Test]
    public async Task ShouldGetProduct()
    {
        var command = new CreateProductCommand
        {
            Name = "Product01",
            Description = "Product01 desc",
            Rate = 1,
            Barcode = "p01"
        };

        var response = await SendAsync(command);

        var getResponse = await SendAsync(new GetProductByIdQuery
        {
            Id = response.Data
        });

        var product = getResponse.Data;

        product.Should().NotBeNull();
        product.Id.Should().Be(response.Data);
        product.Name.Should().Be(command.Name);
        product.Description.Should().Be(command.Description);
        product.Rate.Should().Be(command.Rate);
        product.Barcode.Should().Be(command.Barcode);
    }
}
