namespace ApiSample01.Application.Interfaces;

using ApiSample01.Domain.Entities.WeatherForecast;
using ApiSample01.Domain.DTOs;

public interface IWeatherForecastApplicationService
{
    IEnumerable<WeatherForecast> GetWeatherForecast(int days);
    WeatherForecastApiResponseDto GetWeatherForecastApiResponse(int days, int start, int limit);
}