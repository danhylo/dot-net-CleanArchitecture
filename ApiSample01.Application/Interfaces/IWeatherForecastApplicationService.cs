namespace ApiSample01.Application.Interfaces;

using ApiSample01.Domain.Entities.WeatherForecast;
using ApiSample01.Domain.Entities.Dto;
using ApiSample01.Domain.Entities.Common.Api.Base;

public interface IWeatherForecastApplicationService
{
    Task<IEnumerable<WeatherForecast>> GetWeatherForecast(int days, int start, int limit);
    Task<Result<WeatherForecastApiResponseDto, ApiErrorResponse>> GetWeatherForecastApi(int days, int start, int limit);
}