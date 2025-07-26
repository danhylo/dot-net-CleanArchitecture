using Microsoft.AspNetCore.Mvc;
using ApiSample01.Domain;
using ApiSample01.Application.Interfaces;

namespace ApiSample01.Api.Controllers;

[ApiController]
[Route("api/previsao")]
 [Produces("application/json")]
public class WeatherForecastController : ControllerBase
{

    private readonly IPrevisaoTempoService _previsaoTempoService;

    public WeatherForecastController(IPrevisaoTempoService previsaoTempoService)
    {
        _previsaoTempoService = previsaoTempoService;
    }

    [HttpGet]
    public IEnumerable<WeatherForecast> Get()
    {
        return _previsaoTempoService.ConsultarPrevisao();
    }
}
