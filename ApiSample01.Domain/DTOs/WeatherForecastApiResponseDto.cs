namespace ApiSample01.Domain.DTOs;

using ApiSample01.Domain.Entities.WeatherForecast;
using ApiSample01.Domain.Entities.Common.Api.Base;

public class WeatherForecastApiResponseDto : ApiResponsePage<IEnumerable<WeatherForecast>>
{
}