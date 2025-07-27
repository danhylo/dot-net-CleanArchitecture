namespace ApiSample01.Application.Common.Extensions;

using FluentValidation;
using ApiSample01.Domain.Exceptions;
using ApiSample01.Domain.Constants;

public static class ValidationExtensions
{
    public static void ValidateAndThrowCustom<T>(this IValidator<T> validator, T instance)
    {
        var result = validator.Validate(instance);
        
        if (!result.IsValid)
        {
            var firstError = result.Errors.First();
            var propertyName = firstError.PropertyName.ToLower();
            var attemptedValue = firstError.AttemptedValue;
            
            throw propertyName switch
            {
                "days" => new ET002FieldSizeError("days", (int)attemptedValue, "int", 100, ApplicationConstants.WEATHER_API_NAME),
                "start" => new ET002FieldSizeError("start", (int)attemptedValue, "int", 100, ApplicationConstants.WEATHER_API_NAME),
                "limit" => new ET002FieldSizeError("limit", (int)attemptedValue, "int", 100, ApplicationConstants.WEATHER_API_NAME),
                _ => new ArgumentException(firstError.ErrorMessage)
            };
        }
    }
}