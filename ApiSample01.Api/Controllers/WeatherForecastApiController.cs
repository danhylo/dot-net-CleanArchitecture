using Microsoft.AspNetCore.Mvc;
using ApiSample01.Domain.Entities;
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
    public IEnumerable<WeatherForecast> Get()
    {
        return _weatherForecastApplicationService.GetWeatherForecast();
    }
}