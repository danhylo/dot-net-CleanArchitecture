namespace ApiSample01.Application.Dto;

using ApiSample01.Domain.ValueObjects;

public class WeatherForecastApiRequestDto
{
    public DaysRange Days { get; }
    public StartValue Start { get; }
    public LimitValue Limit { get; }

    public WeatherForecastApiRequestDto(int days, int start, int limit)
    {
        Days = new DaysRange(days);
        Start = new StartValue(start);
        Limit = new LimitValue(limit);
    }
}