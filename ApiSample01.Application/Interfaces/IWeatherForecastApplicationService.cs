namespace ApiSample01.Application.Interfaces;

using ApiSample01.Domain.Entities.WeatherForecast;
using ApiSample01.Domain.DTOs;
using ApiSample01.Domain.Entities.Common.Api.Base;

public interface IWeatherForecastApplicationService
{
    IEnumerable<WeatherForecast> GetWeatherForecast(int days, int start, int limit);
    Result<WeatherForecastApiResponseDto, ApiErrorResponse> GetWeatherForecastApi(int days, int start, int limit);
}