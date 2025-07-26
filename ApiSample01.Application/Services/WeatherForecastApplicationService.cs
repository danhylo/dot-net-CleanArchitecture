namespace ApiSample01.Application.Services;

using ApiSample01.Domain.Entities;
using ApiSample01.Domain.Services;
using ApiSample01.Application.Interfaces;

public class WeatherForecastApplicationService : IWeatherForecastApplicationService
{
    public IEnumerable<WeatherForecast> GetWeatherForecast()
    {
        // Application Service apenas orquestra, Domain Service contém as regras
        return WeatherForecastDomainService.GenerateForecasts(5);
    }
}