namespace ApiSample01.Application.Services;

using ApiSample01.Domain.Entities.WeatherForecast;
using ApiSample01.Domain.Entities.Common.Api.Base;
using ApiSample01.Domain.Exceptions;
using ApiSample01.Domain.Constants;
using ApiSample01.Application.Common.Extensions;
using ApiSample01.Application.Common.Helpers;
using ApiSample01.Domain.DTOs;
using ApiSample01.Domain.Services;
using ApiSample01.Application.Interfaces;
using ApiSample01.Application.DTOs;
using ApiSample01.Application.Validators;

public class WeatherForecastApplicationService : IWeatherForecastApplicationService
{


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
        catch (ET002FieldSizeError ex)
        {
            return Result.Fail<WeatherForecastApiResponseDto>(ErrorResponseHelper.CreateApiErrorResponse(ex));
        }
        catch (Exception ex)
        {
            return Result.Fail<WeatherForecastApiResponseDto>(ErrorResponseHelper.CreateApiErrorResponse(ex));
        }
    }

    private static IEnumerable<WeatherForecast> GetForecastsFromDomain(int days) => 
        WeatherForecastDomainService.GenerateForecasts(days);

    private static void ValidateParameters(int days, int start, int limit)
    {
        var request = new WeatherRequest { Days = days, Start = start, Limit = limit };
        var validator = new WeatherRequestValidator();
        
        validator.ValidateAndThrowCustom(request);
    }


}