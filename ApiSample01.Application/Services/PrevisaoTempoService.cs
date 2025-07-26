namespace ApiSample01.Application.Services;

using ApiSample01.Domain.Entities;
using ApiSample01.Domain.Services;
using ApiSample01.Application.Interfaces;

public class PrevisaoTempoService : IPrevisaoTempoService
{
    public IEnumerable<WeatherForecast> ConsultarPrevisao()
    {
        // Application Service apenas orquestra, Domain Service contém as regras
        return WeatherForecastDomainService.GenerateForecasts(5);
    }
}
