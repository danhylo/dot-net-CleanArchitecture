namespace ApiSample01.Application.Interfaces;

using ApiSample01.Domain.Entities;

public interface IWeatherForecastApplicationService
{
    IEnumerable<WeatherForecast> ConsultarPrevisao();
}