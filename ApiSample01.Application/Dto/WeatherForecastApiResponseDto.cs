namespace ApiSample01.Application.Dto;

using ApiSample01.Domain.Entities.WeatherForecast;
using ApiSample01.Application.Common.Api.Base;

public class WeatherForecastApiResponseDto : ApiResponsePage<IEnumerable<WeatherForecast>>
{
}