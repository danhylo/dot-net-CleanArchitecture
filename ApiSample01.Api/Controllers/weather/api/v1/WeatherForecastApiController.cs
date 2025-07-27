using Microsoft.AspNetCore.Mvc;
using ApiSample01.Domain.Entities.Dto;
using ApiSample01.Application.Interfaces;
using ApiSample01.Domain.Entities.Common.Api.Base;

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
    public async Task<IActionResult> Get([FromQuery] int days = 2, [FromQuery] int start = 1, [FromQuery] int limit = 100)
    {
        var result = await _weatherForecastApplicationService.GetWeatherForecastApi(days, start, limit);

        if (result.IsSuccess)
        {
            return Ok(result.Success);
        }

        return StatusCode(result.Error!.HttpCode, result.Error);
    }
}