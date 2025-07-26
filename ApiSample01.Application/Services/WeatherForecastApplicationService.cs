namespace ApiSample01.Application.Services;

using ApiSample01.Domain.Entities.WeatherForecast;
using ApiSample01.Domain.Entities.Common.api.@base;
using ApiSample01.Domain.DTOs;
using ApiSample01.Domain.Services;
using ApiSample01.Application.Interfaces;

public class WeatherForecastApplicationService : IWeatherForecastApplicationService
{
    private static IEnumerable<WeatherForecast> GetForecastsFromDomain(int days)
    {
        return WeatherForecastDomainService.GenerateForecasts(days);
    }

    public IEnumerable<WeatherForecast> GetWeatherForecast(int days)
    {
        return GetForecastsFromDomain(days);
    }

    public WeatherForecastApiResponseDto GetWeatherForecastApiResponse(int days, int start, int limit)
    {
        // Validação de parâmetros
        ValidateParameters(days, start, limit);

        // Busca dados do Domain
        var allForecasts = GetForecastsFromDomain(days);

        // Aplica paginação
        var skip = (start - 1) * limit;
        var forecasts = allForecasts.Skip(skip).Take(limit);

        // Monta objetos auxiliares
        var page = new Page { Start = start, Limit = forecasts.Count(), Total = allForecasts.Count() };
        var transaction = new Transaction
        {
            LocalTransactionId = Guid.NewGuid().ToString(),
            LocalTransactionDate = DateTime.UtcNow
        };

        // Monta o DTO de resposta (orquestração)
        return new WeatherForecastApiResponseDto
        {
            HttpCode = 200,
            HttpMessage = "OK",
            Status = true,
            Data = forecasts,
            Page = page,
            Transaction = transaction
        };
    }
    
    private static void ValidateParameters(int days, int start, int limit)
    {
        if (days < 0)
            throw new ArgumentException("Days cannot be negative", nameof(days));
        if (start < 1)
            throw new ArgumentException("Start must be greater than 0", nameof(start));
        if (limit < 1)
            throw new ArgumentException("Limit must be greater than 0", nameof(limit));
    }
}