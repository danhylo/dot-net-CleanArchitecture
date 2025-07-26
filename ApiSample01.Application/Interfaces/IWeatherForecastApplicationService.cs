namespace ApiSample01.Application.Interfaces;

using ApiSample01.Domain.Entities;
using ApiSample01.Domain.DTOs;

public interface IWeatherForecastApplicationService
{
    IEnumerable<WeatherForecast> GetWeatherForecast(int days);
    WeatherForecastApiResponseDto GetWeatherForecastApiResponse(int days);
}