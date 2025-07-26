namespace ApiSample01.Domain.Services;

using ApiSample01.Domain.Entities.WeatherForecast;

public static class WeatherForecastDomainService
{
    private static readonly string[] ValidSummaries = new[]
    {
        "Frio", "Quente", "Chuvoso", "Seco", "Úmido"
    };

    private const int MinTemperature = -20;
    private const int MaxTemperature = 40;

    public static WeatherForecast CreateRandomForecast(DateOnly date)
    {
        var temperature = Random.Shared.Next(MinTemperature, MaxTemperature);
        var summary = ValidSummaries[Random.Shared.Next(ValidSummaries.Length)];
        var temperatureF = 32 + (int)(temperature / 0.5556);

        return new WeatherForecast(date, temperature, temperatureF, summary);
    }

    public static IEnumerable<WeatherForecast> GenerateForecasts(int days)
    {
        return Enumerable.Range(0, days)
            .Select(index => CreateRandomForecast(
                DateOnly.FromDateTime(DateTime.Now.AddDays(index))
            ));
    }
}