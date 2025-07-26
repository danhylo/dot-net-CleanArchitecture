namespace ApiSample01.Application.Services;

using ApiSample01.Domain.Entities;
using ApiSample01.Application.Interfaces;

public class PrevisaoTempoService : IPrevisaoTempoService
{
    private static readonly string[] Summaries = new[]
    {
        "Frio", "Quente", "Chuvoso", "Seco", "Úmido"
    };

    public IEnumerable<WeatherForecast> ConsultarPrevisao()
    {
        return Enumerable.Range(1, 5).Select(index =>
            new WeatherForecast(
                DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                Random.Shared.Next(-20, 40),
                Summaries[Random.Shared.Next(Summaries.Length)]
            )
        );
    }
}
