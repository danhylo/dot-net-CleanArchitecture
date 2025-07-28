using ApiSample01.Api.Models;
using ApiSample01.Api.Attributes;
using System.ComponentModel.DataAnnotations;
using Xunit;

namespace ApiSample01.Api.Tests.Models;

public class WeatherForecastRequestTests
{
    #region Constructor Tests

    [Fact]
    public void Constructor_ShouldSetDefaultValues_WhenCreated()
    {
        // Act
        var request = new WeatherForecastRequest();

        // Assert
        Assert.Equal(2, request.Days);
        Assert.Equal(1, request.Start);
        Assert.Equal(100, request.Limit);
    }

    #endregion

    #region Properties Tests

    [Fact]
    public void Properties_ShouldBeSettable_WhenAssigned()
    {
        // Arrange
        var request = new WeatherForecastRequest();

        // Act
        request.Days = 5;
        request.Start = 2;
        request.Limit = 50;

        // Assert
        Assert.Equal(5, request.Days);
        Assert.Equal(2, request.Start);
        Assert.Equal(50, request.Limit);
    }

    [Theory]
    [InlineData(1, 1, 10)]
    [InlineData(50, 5, 50)]
    [InlineData(100, 10, 100)]
    public void Properties_ShouldAcceptValidValues(int days, int start, int limit)
    {
        // Arrange
        var request = new WeatherForecastRequest();

        // Act
        request.Days = days;
        request.Start = start;
        request.Limit = limit;

        // Assert
        Assert.Equal(days, request.Days);
        Assert.Equal(start, request.Start);
        Assert.Equal(limit, request.Limit);
    }

    #endregion

    #region Validation Attributes Tests

    [Fact]
    public void Days_ShouldHaveDaysValidationAttribute()
    {
        // Act
        var property = typeof(WeatherForecastRequest).GetProperty(nameof(WeatherForecastRequest.Days));
        var attributes = property?.GetCustomAttributes(typeof(DaysValidationAttribute), false);

        // Assert
        Assert.NotNull(attributes);
        Assert.Single(attributes);
    }

    [Fact]
    public void Start_ShouldHaveStartValidationAttribute()
    {
        // Act
        var property = typeof(WeatherForecastRequest).GetProperty(nameof(WeatherForecastRequest.Start));
        var attributes = property?.GetCustomAttributes(typeof(StartValidationAttribute), false);

        // Assert
        Assert.NotNull(attributes);
        Assert.Single(attributes);
    }

    [Fact]
    public void Limit_ShouldHaveLimitValidationAttribute()
    {
        // Act
        var property = typeof(WeatherForecastRequest).GetProperty(nameof(WeatherForecastRequest.Limit));
        var attributes = property?.GetCustomAttributes(typeof(LimitValidationAttribute), false);

        // Assert
        Assert.NotNull(attributes);
        Assert.Single(attributes);
    }

    #endregion

    #region Model Validation Tests

    [Fact]
    public void Model_ShouldBeValid_WithDefaultValues()
    {
        // Arrange
        var request = new WeatherForecastRequest();
        var context = new ValidationContext(request);
        var results = new List<ValidationResult>();

        // Act
        var isValid = Validator.TryValidateObject(request, context, results, true);

        // Assert
        Assert.True(isValid);
        Assert.Empty(results);
    }

    [Theory]
    [InlineData(1, 1, 1)]
    [InlineData(50, 5, 50)]
    [InlineData(100, 10, 100)]
    public void Model_ShouldBeValid_WithValidValues(int days, int start, int limit)
    {
        // Arrange
        var request = new WeatherForecastRequest
        {
            Days = days,
            Start = start,
            Limit = limit
        };
        var context = new ValidationContext(request);
        var results = new List<ValidationResult>();

        // Act
        var isValid = Validator.TryValidateObject(request, context, results, true);

        // Assert
        Assert.True(isValid);
        Assert.Empty(results);
    }

    [Theory]
    [InlineData(-1, 1, 10)]
    [InlineData(101, 1, 10)]
    public void Model_ShouldThrowException_WithInvalidDays(int days, int start, int limit)
    {
        // Arrange
        var request = new WeatherForecastRequest
        {
            Days = days,
            Start = start,
            Limit = limit
        };
        var context = new ValidationContext(request);
        var results = new List<ValidationResult>();

        // Act & Assert
        Assert.Throws<Domain.Exceptions.ET002FieldSizeError>(() => 
            Validator.TryValidateObject(request, context, results, true));
    }

    [Theory]
    [InlineData(5, 0, 10)]
    [InlineData(5, -1, 10)]
    public void Model_ShouldThrowException_WithInvalidStart(int days, int start, int limit)
    {
        // Arrange
        var request = new WeatherForecastRequest
        {
            Days = days,
            Start = start,
            Limit = limit
        };
        var context = new ValidationContext(request);
        var results = new List<ValidationResult>();

        // Act & Assert
        Assert.Throws<Domain.Exceptions.ET002FieldSizeError>(() => 
            Validator.TryValidateObject(request, context, results, true));
    }

    [Theory]
    [InlineData(5, 1, 0)]
    [InlineData(5, 1, 101)]
    public void Model_ShouldThrowException_WithInvalidLimit(int days, int start, int limit)
    {
        // Arrange
        var request = new WeatherForecastRequest
        {
            Days = days,
            Start = start,
            Limit = limit
        };
        var context = new ValidationContext(request);
        var results = new List<ValidationResult>();

        // Act & Assert
        Assert.Throws<Domain.Exceptions.ET002FieldSizeError>(() => 
            Validator.TryValidateObject(request, context, results, true));
    }

    #endregion

    #region Type Tests

    [Fact]
    public void Properties_ShouldHaveCorrectTypes()
    {
        // Act
        var daysProperty = typeof(WeatherForecastRequest).GetProperty(nameof(WeatherForecastRequest.Days));
        var startProperty = typeof(WeatherForecastRequest).GetProperty(nameof(WeatherForecastRequest.Start));
        var limitProperty = typeof(WeatherForecastRequest).GetProperty(nameof(WeatherForecastRequest.Limit));

        // Assert
        Assert.Equal(typeof(int), daysProperty?.PropertyType);
        Assert.Equal(typeof(int), startProperty?.PropertyType);
        Assert.Equal(typeof(int), limitProperty?.PropertyType);
    }

    [Fact]
    public void Properties_ShouldHavePublicGettersAndSetters()
    {
        // Act
        var daysProperty = typeof(WeatherForecastRequest).GetProperty(nameof(WeatherForecastRequest.Days));
        var startProperty = typeof(WeatherForecastRequest).GetProperty(nameof(WeatherForecastRequest.Start));
        var limitProperty = typeof(WeatherForecastRequest).GetProperty(nameof(WeatherForecastRequest.Limit));

        // Assert
        Assert.True(daysProperty?.CanRead);
        Assert.True(daysProperty?.CanWrite);
        Assert.True(startProperty?.CanRead);
        Assert.True(startProperty?.CanWrite);
        Assert.True(limitProperty?.CanRead);
        Assert.True(limitProperty?.CanWrite);
    }

    #endregion
}