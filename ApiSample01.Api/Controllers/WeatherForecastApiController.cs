using Microsoft.AspNetCore.Mvc;
using ApiSample01.Domain.Entities;
using ApiSample01.Application.Interfaces;

namespace ApiSample01.Api.Controllers;

[ApiController]
[Route("api/previsao")]
 [Produces("application/json")]
public class WeatherForecastApiController : ControllerBase
{

    private readonly IWeatherForecastApplicationService _previsaoTempoService;

    public WeatherForecastApiController(IWeatherForecastApplicationService previsaoTempoService)
    {
        _previsaoTempoService = previsaoTempoService;
    }

    [HttpGet]
    public IEnumerable<WeatherForecast> Get()
    {
        return _previsaoTempoService.GetWeatherForecast();
    }
}