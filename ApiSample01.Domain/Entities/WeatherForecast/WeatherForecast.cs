namespace ApiSample01.Domain.Entities.WeatherForecast;

public class WeatherForecast
{
    public DateOnly Date { get; }
    public int TemperatureC { get; }
    public int TemperatureF { get; }
    public string? Summary { get; }

    public WeatherForecast(DateOnly date, int temperatureC, int temperatureF, string? summary)
    {
        Date = date;
        TemperatureC = temperatureC;
        TemperatureF = temperatureF;
        Summary = summary;
    }
}
