namespace CleanArchitecture.Application.Features.WeatherForecasts.Queries.GetWeatherForecasts;

using CleanArchitecture.Application.Common.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

public class GetWeatherForecastsQueryHandler : IRequestHandler<GetWeatherForecastsQuery, Result<IEnumerable<GetWeatherForecastDTO>>>
{
    private static readonly string[] Summaries = new[]
    {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

    public Task<Result<IEnumerable<GetWeatherForecastDTO>>> Handle(GetWeatherForecastsQuery request, CancellationToken cancellationToken)
    {
        var rng = new Random();

        var vm = Enumerable.Range(1, 5).Select(index => new GetWeatherForecastDTO
        {
            Date = DateTime.Now.AddDays(index),
            TemperatureC = rng.Next(-20, 55),
            Summary = Summaries[rng.Next(Summaries.Length)]
        });

        return Task.FromResult(Result.Ok(vm));
    }
}
