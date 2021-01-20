using CleanArchitecture.Application.Features.WeatherForecasts.Queries.GetWeatherForecasts;
using FluentAssertions;
using NUnit.Framework;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.IntegrationTests.WeatherForecasts.Queries
{
    using static Testing;

    public class GetWeatherForecastsTests : TestBase
    {
        [Test]
        public async Task ShouldReturnAllListsAndItems()
        {
            var query = new GetWeatherForecastsQuery();

            var result = await SendAsync(query);

            result.Should().NotBeNull();
            result.Data.Should().NotBeEmpty();
            result.Data.Should().HaveCount(5);
        }
    }
}
