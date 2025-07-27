namespace ApiSample01.Domain.ValueObjects;

using ApiSample01.Domain.Services;

public class StartValue : ValueObject
{
    public int Value { get; }

    public StartValue(int value)
    {
        WeatherForecastBusinessRules.ValidateStart(value);
        Value = value;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public static implicit operator int(StartValue startValue) => startValue.Value;
    public static implicit operator StartValue(int value) => new(value);
}