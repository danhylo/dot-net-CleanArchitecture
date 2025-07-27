namespace ApiSample01.Application.Common.Extensions;

using FluentValidation;

public static class ValidationExtensions
{
    public static void ValidateAndThrowCustom<T>(this IValidator<T> validator, T instance)
    {
        var result = validator.Validate(instance);
        
        if (!result.IsValid)
        {
            var firstError = result.Errors.First();
            throw new ArgumentException(firstError.ErrorMessage);
        }
    }
}