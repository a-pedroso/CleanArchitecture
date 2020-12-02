using CleanArchitecture.Application.Common.DTO;
using CleanArchitecture.Application.Features.WeatherForecasts.Queries.GetWeatherForecasts;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CleanArchitecture.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IMediator _mediator;

        public WeatherForecastController(
            ILogger<WeatherForecastController> logger,
            IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IEnumerable<WeatherForecastDTO>> Get()
        {
            _logger.LogDebug("getting WeatherForecast");
            return await _mediator.Send(new GetWeatherForecastsQuery());
        }
    }
}
