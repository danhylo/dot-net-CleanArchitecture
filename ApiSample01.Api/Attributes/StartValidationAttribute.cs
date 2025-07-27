using System.ComponentModel.DataAnnotations;
using ApiSample01.Domain.Exceptions;
using ApiSample01.Domain.Constants;
using ApiSample01.Domain.Services;

namespace ApiSample01.Api.Attributes;

public class StartValidationAttribute : ValidationAttribute
{
    public override bool IsValid(object? value)
    {
        if (value is int start)
        {
            if (start < WeatherForecastBusinessRules.MIN_START)
            {
                throw new ET002FieldSizeError("start", start, "int", WeatherForecastBusinessRules.MIN_START, ApplicationConstants.WEATHER_API_NAME);
            }
        }
        return true;
    }
}