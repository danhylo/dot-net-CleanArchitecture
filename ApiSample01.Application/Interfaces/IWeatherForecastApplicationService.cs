namespace ApiSample01.Application.Interfaces;

using ApiSample01.Domain.Entities.WeatherForecast;
using ApiSample01.Application.Dto;
using ApiSample01.Application.Common.Api.Base;
using ApiSample01.Application.Common.Base;

public interface IWeatherForecastApplicationService
{
    Task<IEnumerable<WeatherForecast>> GetWeatherForecast(int days, int start, int limit);
    Task<Result<WeatherForecastApiResponseDto, ApiErrorResponse>> GetWeatherForecastApi(int days, int start, int limit);
}