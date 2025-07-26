namespace ApiSample01.Domain.DTOs;

using ApiSample01.Domain.Entities.WeatherForecast;
using ApiSample01.Domain.Entities.Common;

public class WeatherForecastApiResponseDto
{
    public int HttpCode { get; set; }
    public string HttpMessage { get; set; } = string.Empty;
    public bool Status { get; set; }
    public IEnumerable<WeatherForecast> Data { get; set; } = Enumerable.Empty<WeatherForecast>();
    public Page Page { get; set; } = new();
    public Transaction Transaction { get; set; } = new();
}