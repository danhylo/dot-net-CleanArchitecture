namespace ApiSample01.Domain.DTOs;

using ApiSample01.Domain.Entities.WeatherForecast;
using ApiSample01.Domain.Entities.Common.api.@base;

public class WeatherForecastApiResponseDto : ApiResponsePage<WeatherForecast>
{
}