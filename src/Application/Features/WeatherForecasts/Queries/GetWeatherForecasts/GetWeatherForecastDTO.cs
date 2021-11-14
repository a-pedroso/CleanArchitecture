namespace CleanArchitecture.Application.Features.WeatherForecasts.Queries.GetWeatherForecasts;

using System;

public record GetWeatherForecastDTO
{
    public DateTime Date { get; init; }

    public int TemperatureC { get; init; }

    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

    public string Summary { get; init; }
}
