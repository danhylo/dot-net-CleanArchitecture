namespace ApiSample01.Domain.Services;

using ApiSample01.Domain.Exceptions;
using ApiSample01.Domain.Constants;

public static class WeatherForecastBusinessRules
{
    public const int MIN_DAYS = 0;
    public const int MAX_DAYS = 100;
    public const int MIN_START = 1;
    public const int MIN_LIMIT = 1;
    public const int MAX_LIMIT = 100;
    
    public static void ValidateWeatherRequest(int days, int start, int limit)
    {
        ValidateDays(days);
        ValidateStart(start);
        ValidateLimit(limit);
    }
    
    public static void ValidateDays(int days)
    {
        if (days < MIN_DAYS || days > MAX_DAYS)
            throw new ET002FieldSizeError("days", days, "int", MAX_DAYS, ApplicationConstants.WEATHER_API_NAME);
    }
    
    public static void ValidateStart(int start)
    {
        if (start < MIN_START)
            throw new ET002FieldSizeError("start", start, "int", MIN_START, ApplicationConstants.WEATHER_API_NAME);
    }
    
    public static void ValidateLimit(int limit)
    {
        if (limit < MIN_LIMIT || limit > MAX_LIMIT)
            throw new ET002FieldSizeError("limit", limit, "int", MAX_LIMIT, ApplicationConstants.WEATHER_API_NAME);
    }
}