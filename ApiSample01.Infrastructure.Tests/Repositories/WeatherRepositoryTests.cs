using ApiSample01.Infrastructure.Repositories;
using ApiSample01.Domain.Repositories;
using ApiSample01.Domain.Entities.WeatherForecast;
using Xunit;

namespace ApiSample01.Infrastructure.Tests.Repositories;

public class WeatherRepositoryTests
{
    #region Constructor Tests

    [Fact]
    public void Constructor_ShouldCreateInstance_WhenCalled()
    {
        // Act
        var repository = new WeatherRepository();

        // Assert
        Assert.NotNull(repository);
        Assert.IsAssignableFrom<IWeatherRepository>(repository);
    }

    #endregion

    #region GetForecastsAsync Tests

    [Fact]
    public async Task GetForecastsAsync_ShouldReturnForecasts_WhenValidDays()
    {
        // Arrange
        var repository = new WeatherRepository();
        const int days = 5;

        // Act
        var result = await repository.GetForecastsAsync(days);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(days, result.Count());
    }

    [Fact]
    public async Task GetForecastsAsync_ShouldReturnEmptyCollection_WhenZeroDays()
    {
        // Arrange
        var repository = new WeatherRepository();
        const int days = 0;

        // Act
        var result = await repository.GetForecastsAsync(days);

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result);
    }

    [Fact]
    public async Task GetForecastsAsync_ShouldReturnValidWeatherForecasts_WhenCalled()
    {
        // Arrange
        var repository = new WeatherRepository();
        const int days = 3;

        // Act
        var result = await repository.GetForecastsAsync(days);
        var forecasts = result.ToList();

        // Assert
        Assert.All(forecasts, forecast =>
        {
            Assert.IsType<WeatherForecast>(forecast);
            Assert.NotNull(forecast.Summary);
            Assert.InRange(forecast.TemperatureC, -20, 40);
        });
    }

    [Fact]
    public async Task GetForecastsAsync_ShouldReturnTaskFromResult_WhenCalled()
    {
        // Arrange
        var repository = new WeatherRepository();
        const int days = 1;

        // Act
        var task = repository.GetForecastsAsync(days);

        // Assert
        Assert.NotNull(task);
        Assert.True(task.IsCompleted);
        
        var result = await task;
        Assert.NotNull(result);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(10)]
    [InlineData(50)]
    [InlineData(100)]
    public async Task GetForecastsAsync_ShouldReturnCorrectCount_WhenDifferentDays(int days)
    {
        // Arrange
        var repository = new WeatherRepository();

        // Act
        var result = await repository.GetForecastsAsync(days);

        // Assert
        Assert.Equal(days, result.Count());
    }

    [Fact]
    public async Task GetForecastsAsync_ShouldReturnConsecutiveDates_WhenMultipleDays()
    {
        // Arrange
        var repository = new WeatherRepository();
        const int days = 5;
        var today = DateTime.Now.Date;

        // Act
        var result = await repository.GetForecastsAsync(days);
        var forecasts = result.ToList();

        // Assert
        for (int i = 0; i < days; i++)
        {
            var expectedDate = DateOnly.FromDateTime(today.AddDays(i));
            Assert.Equal(expectedDate, forecasts[i].Date);
        }
    }

    #endregion

    #region Interface Implementation Tests

    [Fact]
    public void WeatherRepository_ShouldImplementIWeatherRepository()
    {
        // Arrange & Act
        var repository = new WeatherRepository();

        // Assert
        Assert.IsAssignableFrom<IWeatherRepository>(repository);
    }

    [Fact]
    public void GetForecastsAsync_ShouldHaveCorrectSignature()
    {
        // Arrange
        var repository = new WeatherRepository();
        var method = typeof(WeatherRepository).GetMethod(nameof(WeatherRepository.GetForecastsAsync));

        // Assert
        Assert.NotNull(method);
        Assert.Equal(typeof(Task<IEnumerable<WeatherForecast>>), method.ReturnType);
        
        var parameters = method.GetParameters();
        Assert.Single(parameters);
        Assert.Equal(typeof(int), parameters[0].ParameterType);
        Assert.Equal("days", parameters[0].Name);
    }

    #endregion
}