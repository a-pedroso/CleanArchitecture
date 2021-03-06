namespace CleanArchitecture.Application.Features.WeatherForecasts.Queries.GetWeatherForecasts
{
    using CleanArchitecture.Application.Common.Wrappers;
    using MediatR;
    using System.Collections.Generic;

    public class GetWeatherForecastsQuery : IRequest<Result<IEnumerable<GetWeatherForecastDTO>>>
    {
    }
}
