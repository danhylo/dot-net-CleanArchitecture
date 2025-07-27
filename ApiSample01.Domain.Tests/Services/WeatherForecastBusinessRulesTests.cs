using ApiSample01.Domain.Services;
using ApiSample01.Domain.Exceptions;
using Xunit;

namespace ApiSample01.Domain.Tests.Services;

public class WeatherForecastBusinessRulesTests
{
    #region ValidateDays Tests

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(50)]
    [InlineData(100)]
    public void ValidateDays_ShouldNotThrow_WhenValidDays(int days)
    {
        // Act & Assert
        var exception = Record.Exception(() => WeatherForecastBusinessRules.ValidateDays(days));
        Assert.Null(exception);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(-10)]
    [InlineData(101)]
    [InlineData(200)]
    public void ValidateDays_ShouldThrowET002FieldSizeError_WhenInvalidDays(int days)
    {
        // Act & Assert
        var exception = Assert.Throws<ET002FieldSizeError>(() => 
            WeatherForecastBusinessRules.ValidateDays(days));
        
        Assert.Equal("days", exception.FieldName);
        Assert.Equal(days, exception.Value);
        Assert.Equal("int", exception.Type);
        Assert.Equal(100, exception.MaxSize);
    }

    #endregion

    #region ValidateStart Tests

    [Theory]
    [InlineData(1)]
    [InlineData(5)]
    [InlineData(100)]
    public void ValidateStart_ShouldNotThrow_WhenValidStart(int start)
    {
        // Act & Assert
        var exception = Record.Exception(() => WeatherForecastBusinessRules.ValidateStart(start));
        Assert.Null(exception);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(-10)]
    public void ValidateStart_ShouldThrowET002FieldSizeError_WhenInvalidStart(int start)
    {
        // Act & Assert
        var exception = Assert.Throws<ET002FieldSizeError>(() => 
            WeatherForecastBusinessRules.ValidateStart(start));
        
        Assert.Equal("start", exception.FieldName);
        Assert.Equal(start, exception.Value);
        Assert.Equal("int", exception.Type);
        Assert.Equal(1, exception.MaxSize);
    }

    #endregion

    #region ValidateLimit Tests

    [Theory]
    [InlineData(1)]
    [InlineData(50)]
    [InlineData(100)]
    public void ValidateLimit_ShouldNotThrow_WhenValidLimit(int limit)
    {
        // Act & Assert
        var exception = Record.Exception(() => WeatherForecastBusinessRules.ValidateLimit(limit));
        Assert.Null(exception);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(101)]
    [InlineData(200)]
    public void ValidateLimit_ShouldThrowET002FieldSizeError_WhenInvalidLimit(int limit)
    {
        // Act & Assert
        var exception = Assert.Throws<ET002FieldSizeError>(() => 
            WeatherForecastBusinessRules.ValidateLimit(limit));
        
        Assert.Equal("limit", exception.FieldName);
        Assert.Equal(limit, exception.Value);
        Assert.Equal("int", exception.Type);
        Assert.Equal(100, exception.MaxSize);
    }

    #endregion

    #region ValidateWeatherRequest Tests

    [Theory]
    [InlineData(5, 1, 10)]
    [InlineData(0, 1, 1)]
    [InlineData(100, 5, 100)]
    public void ValidateWeatherRequest_ShouldNotThrow_WhenAllParametersValid(int days, int start, int limit)
    {
        // Act & Assert
        var exception = Record.Exception(() => 
            WeatherForecastBusinessRules.ValidateWeatherRequest(days, start, limit));
        Assert.Null(exception);
    }

    [Fact]
    public void ValidateWeatherRequest_ShouldThrowET002FieldSizeError_WhenInvalidDays()
    {
        // Act & Assert
        var exception = Assert.Throws<ET002FieldSizeError>(() => 
            WeatherForecastBusinessRules.ValidateWeatherRequest(-1, 1, 10));
        
        Assert.Equal("days", exception.FieldName);
    }

    [Fact]
    public void ValidateWeatherRequest_ShouldThrowET002FieldSizeError_WhenInvalidStart()
    {
        // Act & Assert
        var exception = Assert.Throws<ET002FieldSizeError>(() => 
            WeatherForecastBusinessRules.ValidateWeatherRequest(5, 0, 10));
        
        Assert.Equal("start", exception.FieldName);
    }

    [Fact]
    public void ValidateWeatherRequest_ShouldThrowET002FieldSizeError_WhenInvalidLimit()
    {
        // Act & Assert
        var exception = Assert.Throws<ET002FieldSizeError>(() => 
            WeatherForecastBusinessRules.ValidateWeatherRequest(5, 1, 101));
        
        Assert.Equal("limit", exception.FieldName);
    }

    #endregion

    #region Constants Tests

    [Fact]
    public void Constants_ShouldHaveCorrectValues()
    {
        // Assert
        Assert.Equal(0, WeatherForecastBusinessRules.MIN_DAYS);
        Assert.Equal(100, WeatherForecastBusinessRules.MAX_DAYS);
        Assert.Equal(1, WeatherForecastBusinessRules.MIN_START);
        Assert.Equal(1, WeatherForecastBusinessRules.MIN_LIMIT);
        Assert.Equal(100, WeatherForecastBusinessRules.MAX_LIMIT);
    }

    #endregion
}