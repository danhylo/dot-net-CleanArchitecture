namespace ApiSample01.Domain.Repositories;

using ApiSample01.Domain.Entities.WeatherForecast;

public interface IWeatherRepository
{
    Task<IEnumerable<WeatherForecast>> GetForecastsAsync(int days);
}