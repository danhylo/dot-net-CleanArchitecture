using Microsoft.AspNetCore.Mvc;
using ApiSample01.Domain.DTOs;
using ApiSample01.Application.Interfaces;

namespace ApiSample01.Api.Controllers;

[ApiController]
[Route("api/previsao")]
 [Produces("application/json")]
public class WeatherForecastApiController : ControllerBase
{

    private readonly IWeatherForecastApplicationService _weatherForecastApplicationService;

    public WeatherForecastApiController(IWeatherForecastApplicationService weatherForecastApplicationService)
    {
        _weatherForecastApplicationService = weatherForecastApplicationService;
    }

    [HttpGet]
    public WeatherForecastApiResponseDto Get([FromQuery] int days = 2)
    {
        return _weatherForecastApplicationService.GetWeatherForecastApiResponse(days);
    }
}