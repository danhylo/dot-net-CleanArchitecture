namespace ApiSample01.Application.Services;

using ApiSample01.Domain.Entities.WeatherForecast;
using ApiSample01.Domain.Entities.Common.Api.Base;
using ApiSample01.Domain.Exceptions;
using ApiSample01.Domain.Constants;
using ApiSample01.Application.Common.Extensions;
using ApiSample01.Application.Common.Helpers;
using ApiSample01.Domain.Entities.Dto;
using ApiSample01.Domain.Services;
using ApiSample01.Application.Interfaces;
using ApiSample01.Domain.Repositories;

public class WeatherForecastApplicationService : IWeatherForecastApplicationService
{
    private readonly IWeatherRepository _weatherRepository;

    public WeatherForecastApplicationService(IWeatherRepository weatherRepository)
    {
        _weatherRepository = weatherRepository;
    }


    public async Task<IEnumerable<WeatherForecast>> GetWeatherForecast(int days, int start, int limit)
    {
        var request = new WeatherForecastApiRequestDto(days, start, limit);
        var forecasts = await GetForecastsFromDomain(request.Days);
        return forecasts.Paginate(request.Start, request.Limit);
    }

    public async Task<Result<WeatherForecastApiResponseDto, ApiErrorResponse>> GetWeatherForecastApi(int days, int start, int limit)
    {
        try
        {
            var request = new WeatherForecastApiRequestDto(days, start, limit);
            var forecastsData = await GetForecastsFromDomain(request.Days);
            var (forecasts, total) = forecastsData.PaginateWithTotal(request.Start, request.Limit);
            
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

    private async Task<IEnumerable<WeatherForecast>> GetForecastsFromDomain(int days) => 
        await _weatherRepository.GetForecastsAsync(days);




}