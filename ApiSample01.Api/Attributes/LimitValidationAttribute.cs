using System.ComponentModel.DataAnnotations;
using ApiSample01.Domain.Exceptions;
using ApiSample01.Domain.Constants;
using ApiSample01.Domain.Services;

namespace ApiSample01.Api.Attributes;

public class LimitValidationAttribute : ValidationAttribute
{
    public override bool IsValid(object? value)
    {
        if (value is int limit)
        {
            if (limit < WeatherForecastBusinessRules.MIN_LIMIT || limit > WeatherForecastBusinessRules.MAX_LIMIT)
            {
                throw new ET002FieldSizeError("limit", limit, "int", WeatherForecastBusinessRules.MAX_LIMIT, ApplicationConstants.WEATHER_API_NAME);
            }
        }
        return true;
    }
}