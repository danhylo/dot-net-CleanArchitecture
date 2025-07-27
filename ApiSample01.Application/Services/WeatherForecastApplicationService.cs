namespace ApiSample01.Application.Services;

using ApiSample01.Domain.Entities.WeatherForecast;
using ApiSample01.Domain.Entities.Common.api.@base;
using ApiSample01.Application.Common.Extensions;
using ApiSample01.Application.Common;
using ApiSample01.Domain.DTOs;
using ApiSample01.Domain.Services;
using ApiSample01.Application.Interfaces;

public class WeatherForecastApplicationService : IWeatherForecastApplicationService
{
    public IEnumerable<WeatherForecast> GetWeatherForecast(int days, int start, int limit)
    {
        // Validação de parâmetros
        ValidateParameters(days, start, limit);
        
        // Busca dados do Domain e aplica paginação
        return GetForecastsFromDomain(days).Paginate(start, limit);
    }

    public Result<WeatherForecastApiResponseDto, ApiErrorResponse> GetWeatherForecastApiResponse(int days, int start, int limit)
    {
        try
        {
            // Validação de parâmetros
            ValidateParameters(days, start, limit);

            // Busca dados do Domain e aplica paginação
            var (forecasts, total) = GetForecastsFromDomain(days).PaginateWithTotal(start, limit);
            
            // Monta objetos auxiliares
            var page = new Page { Start = start, Limit = forecasts.Count(), Total = total };
            var transaction = new Transaction
            {
                LocalTransactionId = Guid.NewGuid().ToString(),
                LocalTransactionDate = DateTime.UtcNow
            };

            // Monta o DTO de resposta (orquestração)
            var response = new WeatherForecastApiResponseDto
            {
                HttpCode = 200,
                HttpMessage = "OK",
                Status = true,
                Data = forecasts,
                Page = page,
                Transaction = transaction
            };
            
            return Result.Ok(response);
        }
        catch (ArgumentException ex)
        {
            return Result.Fail<WeatherForecastApiResponseDto>(CreateErrorResponse(400, "Bad Request", "EN:001", ex.Message, "Weather API", ex));
        }
        catch (Exception ex)
        {
            return Result.Fail<WeatherForecastApiResponseDto>(CreateErrorResponse(500, "Internal Server Error", "EN:500", "Internal error", "Weather API", ex));
        }
    }
 
    private static IEnumerable<WeatherForecast> GetForecastsFromDomain(int days)
    {
        return WeatherForecastDomainService.GenerateForecasts(days);
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

    private static ApiErrorResponse CreateErrorResponse(int httpCode, string httpMessage, string errorCode, string errorMessage, string application, Exception ex)
    {
        return new ApiErrorResponse
        {
            HttpCode = httpCode,
            HttpMessage = httpMessage,
            Status = false,
            Error = new Error
            {
                Code = errorCode,
                Message = errorMessage,
                Application = application
            },
            Transaction = new Transaction
            {
                LocalTransactionId = Guid.NewGuid().ToString(),
                LocalTransactionDate = DateTime.UtcNow
            }
        };
    }
}