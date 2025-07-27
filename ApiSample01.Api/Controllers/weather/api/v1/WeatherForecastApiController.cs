using Microsoft.AspNetCore.Mvc;
using ApiSample01.Application.Dto;
using ApiSample01.Application.Interfaces;
using ApiSample01.Application.Common.Api.Base;
using ApiSample01.Api.Models;

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
    public async Task<IActionResult> Get([FromQuery] WeatherForecastRequest request)
    {
        var result = await _weatherForecastApplicationService.GetWeatherForecastApi(request.Days, request.Start, request.Limit);

        if (result.IsSuccess)
        {
            return Ok(result.Success);
        }

        return StatusCode(result.Error!.HttpCode, result.Error);
    }
}