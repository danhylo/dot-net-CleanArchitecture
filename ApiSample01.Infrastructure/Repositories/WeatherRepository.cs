namespace ApiSample01.Infrastructure.Repositories;

using ApiSample01.Domain.Entities.WeatherForecast;
using ApiSample01.Domain.Repositories;
using ApiSample01.Domain.Services;

public class WeatherRepository : IWeatherRepository
{
    public Task<IEnumerable<WeatherForecast>> GetForecastsAsync(int days)
    {
        var forecasts = WeatherForecastDomainService.GenerateForecasts(days);
        return Task.FromResult(forecasts);
    }
}