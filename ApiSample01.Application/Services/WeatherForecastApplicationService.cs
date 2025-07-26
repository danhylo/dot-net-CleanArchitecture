namespace ApiSample01.Application.Services;

using ApiSample01.Domain.Entities;
using ApiSample01.Domain.Entities.Common;
using ApiSample01.Domain.DTOs;
using ApiSample01.Domain.Services;
using ApiSample01.Application.Interfaces;

public class WeatherForecastApplicationService : IWeatherForecastApplicationService
{
    public IEnumerable<WeatherForecast> GetWeatherForecast(int days)
    {
        // Application Service apenas orquestra, Domain Service contém as regras
        return WeatherForecastDomainService.GenerateForecasts(days);
    }

    public WeatherForecastApiResponseDto GetWeatherForecastApiResponse(int days)
    {
        // Busca dados do Domain
        var forecasts = WeatherForecastDomainService.GenerateForecasts(days);
        
        // Monta o DTO de resposta (orquestração)
        return new WeatherForecastApiResponseDto
        {
            HttpCode = 200,
            HttpMessage = "OK",
            Status = true,
            Data = forecasts,
            Page = new Page { Start = 1, Limit = days, Total = days },
            Transaction = new Transaction 
            { 
                LocalTransactionId = Guid.NewGuid().ToString(),
                LocalTransactionDate = DateTime.UtcNow 
            }
        };
    }
}