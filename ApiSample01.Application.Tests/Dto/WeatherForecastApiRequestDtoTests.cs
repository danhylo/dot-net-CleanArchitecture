using ApiSample01.Application.Dto;
using ApiSample01.Domain.ValueObjects;
using ApiSample01.Domain.Exceptions;
using Xunit;

namespace ApiSample01.Application.Tests.Dto;

public class WeatherForecastApiRequestDtoTests
{
    #region Constructor Tests

    [Fact]
    public void Constructor_ShouldCreateDto_WhenValidParameters()
    {
        // Arrange
        const int days = 5;
        const int start = 1;
        const int limit = 10;

        // Act
        var dto = new WeatherForecastApiRequestDto(days, start, limit);

        // Assert
        Assert.NotNull(dto);
        Assert.Equal(days, dto.Days.Value);
        Assert.Equal(start, dto.Start.Value);
        Assert.Equal(limit, dto.Limit.Value);
    }

    [Fact]
    public void Constructor_ShouldCreateValueObjects_WhenCalled()
    {
        // Act
        var dto = new WeatherForecastApiRequestDto(5, 1, 10);

        // Assert
        Assert.IsType<DaysRange>(dto.Days);
        Assert.IsType<StartValue>(dto.Start);
        Assert.IsType<LimitValue>(dto.Limit);
    }

    [Theory]
    [InlineData(0, 1, 10)]
    [InlineData(50, 1, 50)]
    [InlineData(100, 5, 100)]
    public void Constructor_ShouldWork_WithValidRanges(int days, int start, int limit)
    {
        // Act
        var dto = new WeatherForecastApiRequestDto(days, start, limit);

        // Assert
        Assert.Equal(days, dto.Days.Value);
        Assert.Equal(start, dto.Start.Value);
        Assert.Equal(limit, dto.Limit.Value);
    }

    #endregion

    #region Validation Tests

    [Fact]
    public void Constructor_ShouldThrowException_WhenInvalidDays()
    {
        // Act & Assert
        Assert.Throws<ET002FieldSizeError>(() => new WeatherForecastApiRequestDto(-1, 1, 10));
        Assert.Throws<ET002FieldSizeError>(() => new WeatherForecastApiRequestDto(101, 1, 10));
    }

    [Fact]
    public void Constructor_ShouldThrowException_WhenInvalidStart()
    {
        // Act & Assert
        Assert.Throws<ET002FieldSizeError>(() => new WeatherForecastApiRequestDto(5, 0, 10));
        Assert.Throws<ET002FieldSizeError>(() => new WeatherForecastApiRequestDto(5, -1, 10));
    }

    [Fact]
    public void Constructor_ShouldThrowException_WhenInvalidLimit()
    {
        // Act & Assert
        Assert.Throws<ET002FieldSizeError>(() => new WeatherForecastApiRequestDto(5, 1, 0));
        Assert.Throws<ET002FieldSizeError>(() => new WeatherForecastApiRequestDto(5, 1, 101));
    }

    #endregion

    #region Properties Tests

    [Fact]
    public void Properties_ShouldBeReadOnly_WhenAccessed()
    {
        // Arrange
        var dto = new WeatherForecastApiRequestDto(5, 1, 10);

        // Act & Assert
        var daysProperty = typeof(WeatherForecastApiRequestDto).GetProperty(nameof(dto.Days));
        var startProperty = typeof(WeatherForecastApiRequestDto).GetProperty(nameof(dto.Start));
        var limitProperty = typeof(WeatherForecastApiRequestDto).GetProperty(nameof(dto.Limit));

        Assert.Null(daysProperty?.SetMethod);
        Assert.Null(startProperty?.SetMethod);
        Assert.Null(limitProperty?.SetMethod);
    }

    [Fact]
    public void Properties_ShouldHaveCorrectTypes()
    {
        // Arrange
        var dto = new WeatherForecastApiRequestDto(5, 1, 10);

        // Act & Assert
        Assert.IsType<DaysRange>(dto.Days);
        Assert.IsType<StartValue>(dto.Start);
        Assert.IsType<LimitValue>(dto.Limit);
    }

    #endregion

    #region Edge Cases Tests

    [Fact]
    public void Constructor_ShouldWork_WithMinimumValues()
    {
        // Act
        var dto = new WeatherForecastApiRequestDto(0, 1, 1);

        // Assert
        Assert.Equal(0, dto.Days.Value);
        Assert.Equal(1, dto.Start.Value);
        Assert.Equal(1, dto.Limit.Value);
    }

    [Fact]
    public void Constructor_ShouldWork_WithMaximumValues()
    {
        // Act
        var dto = new WeatherForecastApiRequestDto(100, 1000, 100);

        // Assert
        Assert.Equal(100, dto.Days.Value);
        Assert.Equal(1000, dto.Start.Value);
        Assert.Equal(100, dto.Limit.Value);
    }

    #endregion
}