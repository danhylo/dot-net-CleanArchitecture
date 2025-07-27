namespace ApiSample01.Domain.ValueObjects;

using ApiSample01.Domain.Services;

public class LimitValue : ValueObject
{
    public int Value { get; }

    public LimitValue(int value)
    {
        WeatherForecastBusinessRules.ValidateLimit(value);
        Value = value;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public static implicit operator int(LimitValue limitValue) => limitValue.Value;
    public static implicit operator LimitValue(int value) => new(value);
}