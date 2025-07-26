using Microsoft.AspNetCore.Mvc;
using ApiSample01.Domain.DTOs;
using ApiSample01.Application.Interfaces;

namespace ApiSample01.Api.Controllers.weather.api.v1;

[ApiController]
[Route("weather/api/v1")]
 [Produces("application/json")]
public class WeatherForecastApiController : ControllerBase
{

    private readonly IWeatherForecastApplicationService _weatherForecastApplicationService;

    public WeatherForecastApiController(IWeatherForecastApplicationService weatherForecastApplicationService)
    {
        _weatherForecastApplicationService = weatherForecastApplicationService;
    }

    [HttpGet("forecast")]
    public WeatherForecastApiResponseDto Get([FromQuery] int days = 2, [FromQuery] int start = 1, [FromQuery] int limit = 100)
    {
        return _weatherForecastApplicationService.GetWeatherForecastApiResponse(days, start, limit);
    }
}