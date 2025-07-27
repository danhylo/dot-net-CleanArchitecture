using System.ComponentModel.DataAnnotations;
using ApiSample01.Domain.Exceptions;
using ApiSample01.Domain.Constants;
using ApiSample01.Domain.Services;

namespace ApiSample01.Api.Attributes;

public class DaysValidationAttribute : ValidationAttribute
{
    public override bool IsValid(object? value)
    {
        if (value is int days)
        {
            if (days < WeatherForecastBusinessRules.MIN_DAYS || days > WeatherForecastBusinessRules.MAX_DAYS)
            {
                throw new ET002FieldSizeError("days", days, "int", WeatherForecastBusinessRules.MAX_DAYS, ApplicationConstants.WEATHER_API_NAME);
            }
        }
        return true;
    }
}