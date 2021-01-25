using CleanArchitecture.Application.Common.Wrappers;
using MediatR;
using System.Collections.Generic;

namespace CleanArchitecture.Application.Features.WeatherForecasts.Queries.GetWeatherForecasts
{
    public class GetWeatherForecastsQuery : IRequest<Result<IEnumerable<GetWeatherForecastDTO>>>
    {
    }
}
