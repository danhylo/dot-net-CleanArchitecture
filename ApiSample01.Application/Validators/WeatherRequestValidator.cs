namespace ApiSample01.Application.Validators;

using FluentValidation;
using ApiSample01.Application.DTOs;

public class WeatherRequestValidator : AbstractValidator<WeatherRequest>
{
    public WeatherRequestValidator()
    {
        RuleFor(x => x.Days)
            .InclusiveBetween(0, 100)
            .WithMessage("Days must be between 0 and 100");

        RuleFor(x => x.Start)
            .GreaterThan(0)
            .WithMessage("Start must be greater than 0");

        RuleFor(x => x.Limit)
            .InclusiveBetween(1, 100)
            .WithMessage("Limit must be between 1 and 100");
    }
}