namespace ApiSample01.Application.Interfaces;

using ApiSample01.Domain;

public interface IPrevisaoTempoService
{
    IEnumerable<WeatherForecast> ConsultarPrevisao();
}
