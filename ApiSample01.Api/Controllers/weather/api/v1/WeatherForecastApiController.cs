using Microsoft.AspNetCore.Mvc;
using ApiSample01.Domain.DTOs;
using ApiSample01.Application.Interfaces;
using ApiSample01.Application.Common;
using ApiSample01.Domain.Entities.Common.api.@base;

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
    [ProducesResponseType(typeof(WeatherForecastApiResponseDto), 200)]
    [ProducesResponseType(typeof(ApiErrorResponse), 207)]
    [ProducesResponseType(typeof(ApiErrorResponse), 400)]
    [ProducesResponseType(typeof(ApiErrorResponse), 500)]
    public IActionResult Get([FromQuery] int days = 2, [FromQuery] int start = 1, [FromQuery] int limit = 100)
    {
        var result = _weatherForecastApplicationService.GetWeatherForecastApiResponse(days, start, limit);

        if (result.IsSuccess)
        {
            return Ok(result.Success);
        }

        return StatusCode(result.Error!.HttpCode, result.Error);
    }
}