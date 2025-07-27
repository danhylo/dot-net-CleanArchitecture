namespace ApiSample01.Application.Services;

using ApiSample01.Domain.Entities.WeatherForecast;
using ApiSample01.Domain.Entities.Common.Api.Base;
using ApiSample01.Application.Common.Extensions;
using ApiSample01.Application.Common.Helpers;
using ApiSample01.Domain.DTOs;
using ApiSample01.Domain.Services;
using ApiSample01.Application.Interfaces;

public class WeatherForecastApplicationService : IWeatherForecastApplicationService
{
    private const string APPLICATION_NAME = "Weather API";

    public IEnumerable<WeatherForecast> GetWeatherForecast(int days, int start, int limit)
    {
        ValidateParameters(days, start, limit);
        return GetForecastsFromDomain(days).Paginate(start, limit);
    }

    public Result<WeatherForecastApiResponseDto, ApiErrorResponse> GetWeatherForecastApi(int days, int start, int limit)
    {
        try
        {
            ValidateParameters(days, start, limit);
            var (forecasts, total) = GetForecastsFromDomain(days).PaginateWithTotal(start, limit);
            
            var page = new Page { Start = start, Limit = forecasts.Count(), Total = total };
            var transaction = TransactionHelper.CreateTransaction();

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
            return Result.Fail<WeatherForecastApiResponseDto>(ErrorResponseHelper.CreateErrorResponse(400, "Bad Request", "EN:001", ex.Message, APPLICATION_NAME, ex));
        }
        catch (Exception ex)
        {
            return Result.Fail<WeatherForecastApiResponseDto>(ErrorResponseHelper.CreateErrorResponse(500, "Internal Server Error", "EN:500", "Internal error", APPLICATION_NAME, ex));
        }
    }

    private static IEnumerable<WeatherForecast> GetForecastsFromDomain(int days) => 
        WeatherForecastDomainService.GenerateForecasts(days);

    private static void ValidateParameters(int days, int start, int limit)
    {
        if (days < 0) throw new ArgumentException("Days cannot be negative", nameof(days));
        if (start < 1) throw new ArgumentException("Start must be greater than 0", nameof(start));
        if (limit < 1) throw new ArgumentException("Limit must be greater than 0", nameof(limit));
    }


}