namespace ApiSample01.Domain.ValueObjects;

using ApiSample01.Domain.Services;

public class DaysRange : ValueObject
{
    public int Value { get; }

    public DaysRange(int value)
    {
        WeatherForecastBusinessRules.ValidateDays(value);
        Value = value;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public static implicit operator int(DaysRange daysRange) => daysRange.Value;
    public static implicit operator DaysRange(int value) => new(value);
}