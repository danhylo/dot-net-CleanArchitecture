using ApiSample01.Domain.Services;
using ApiSample01.Domain.Entities.WeatherForecast;
using Xunit;

namespace ApiSample01.Domain.Tests.Services;

public class WeatherForecastDomainServiceTests
{
    #region CreateRandomForecast Tests

    [Fact]
    public void CreateRandomForecast_ShouldReturnWeatherForecast_WhenCalled()
    {
        // Arrange
        var date = DateOnly.FromDateTime(DateTime.Now);

        // Act
        var forecast = WeatherForecastDomainService.CreateRandomForecast(date);

        // Assert
        Assert.NotNull(forecast);
        Assert.Equal(date, forecast.Date);
    }

    [Fact]
    public void CreateRandomForecast_ShouldHaveValidTemperatureRange_WhenCalled()
    {
        // Arrange
        var date = DateOnly.FromDateTime(DateTime.Now);

        // Act
        var forecast = WeatherForecastDomainService.CreateRandomForecast(date);

        // Assert
        Assert.InRange(forecast.TemperatureC, -20, 40);
    }

    [Fact]
    public void CreateRandomForecast_ShouldHaveValidSummary_WhenCalled()
    {
        // Arrange
        var date = DateOnly.FromDateTime(DateTime.Now);
        var validSummaries = new[] { "Frio", "Quente", "Chuvoso", "Seco", "Úmido" };

        // Act
        var forecast = WeatherForecastDomainService.CreateRandomForecast(date);

        // Assert
        Assert.Contains(forecast.Summary, validSummaries);
    }

    [Fact]
    public void CreateRandomForecast_ShouldHaveCorrectFahrenheitConversion_WhenCalled()
    {
        // Arrange
        var date = DateOnly.FromDateTime(DateTime.Now);

        // Act
        var forecast = WeatherForecastDomainService.CreateRandomForecast(date);

        // Assert
        var expectedF = 32 + (int)(forecast.TemperatureC / 0.5556);
        Assert.Equal(expectedF, forecast.TemperatureF);
    }

    [Theory]
    [InlineData("2024-01-01")]
    [InlineData("2024-06-15")]
    [InlineData("2024-12-31")]
    public void CreateRandomForecast_ShouldReturnCorrectDate_WhenDifferentDatesProvided(string dateString)
    {
        // Arrange
        var date = DateOnly.Parse(dateString);

        // Act
        var forecast = WeatherForecastDomainService.CreateRandomForecast(date);

        // Assert
        Assert.Equal(date, forecast.Date);
    }

    #endregion

    #region GenerateForecasts Tests

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(5)]
    [InlineData(10)]
    public void GenerateForecasts_ShouldReturnCorrectCount_WhenValidDaysProvided(int days)
    {
        // Act
        var forecasts = WeatherForecastDomainService.GenerateForecasts(days);

        // Assert
        Assert.Equal(days, forecasts.Count());
    }

    [Fact]
    public void GenerateForecasts_ShouldReturnEmptyCollection_WhenZeroDays()
    {
        // Act
        var forecasts = WeatherForecastDomainService.GenerateForecasts(0);

        // Assert
        Assert.Empty(forecasts);
    }

    [Fact]
    public void GenerateForecasts_ShouldReturnConsecutiveDates_WhenMultipleDays()
    {
        // Arrange
        var days = 5;
        var today = DateTime.Now.Date;

        // Act
        var forecasts = WeatherForecastDomainService.GenerateForecasts(days).ToList();

        // Assert
        for (int i = 0; i < days; i++)
        {
            var expectedDate = DateOnly.FromDateTime(today.AddDays(i));
            Assert.Equal(expectedDate, forecasts[i].Date);
        }
    }

    [Fact]
    public void GenerateForecasts_ShouldReturnValidForecasts_WhenCalled()
    {
        // Arrange
        var days = 3;
        var validSummaries = new[] { "Frio", "Quente", "Chuvoso", "Seco", "Úmido" };

        // Act
        var forecasts = WeatherForecastDomainService.GenerateForecasts(days).ToList();

        // Assert
        Assert.All(forecasts, forecast =>
        {
            Assert.InRange(forecast.TemperatureC, -20, 40);
            Assert.Contains(forecast.Summary, validSummaries);
            Assert.NotNull(forecast.Summary);
        });
    }

    [Fact]
    public void GenerateForecasts_ShouldReturnDifferentForecasts_WhenCalledMultipleTimes()
    {
        // Act
        var forecasts1 = WeatherForecastDomainService.GenerateForecasts(5).ToList();
        var forecasts2 = WeatherForecastDomainService.GenerateForecasts(5).ToList();

        // Assert - At least one forecast should be different (temperature or summary)
        var hasDifference = forecasts1.Zip(forecasts2, (f1, f2) => 
            f1.TemperatureC != f2.TemperatureC || f1.Summary != f2.Summary)
            .Any(isDifferent => isDifferent);
        
        Assert.True(hasDifference, "Generated forecasts should have some randomness");
    }

    #endregion

    #region Integration Tests

    [Fact]
    public void GenerateForecasts_ShouldUseCreateRandomForecast_WhenCalled()
    {
        // Arrange
        var days = 1;
        var today = DateOnly.FromDateTime(DateTime.Now);

        // Act
        var forecasts = WeatherForecastDomainService.GenerateForecasts(days).ToList();
        var singleForecast = WeatherForecastDomainService.CreateRandomForecast(today);

        // Assert
        Assert.Single(forecasts);
        Assert.Equal(today, forecasts[0].Date);
        Assert.Equal(today, singleForecast.Date);
    }

    #endregion
}