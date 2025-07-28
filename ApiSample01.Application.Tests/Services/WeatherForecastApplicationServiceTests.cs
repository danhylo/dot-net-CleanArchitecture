using ApiSample01.Application.Services;
using ApiSample01.Application.Interfaces;
using ApiSample01.Domain.Repositories;
using ApiSample01.Domain.Entities.WeatherForecast;
using ApiSample01.Domain.Exceptions;
using Moq;
using Xunit;

namespace ApiSample01.Application.Tests.Services;

public class WeatherForecastApplicationServiceTests
{
    private readonly Mock<IWeatherRepository> _mockRepository;
    private readonly WeatherForecastApplicationService _service;

    public WeatherForecastApplicationServiceTests()
    {
        _mockRepository = new Mock<IWeatherRepository>();
        _service = new WeatherForecastApplicationService(_mockRepository.Object);
    }

    #region Constructor Tests

    [Fact]
    public void Constructor_ShouldCreateInstance_WhenValidRepository()
    {
        // Act
        var service = new WeatherForecastApplicationService(_mockRepository.Object);

        // Assert
        Assert.NotNull(service);
        Assert.IsAssignableFrom<IWeatherForecastApplicationService>(service);
    }

    #endregion

    #region GetWeatherForecast Tests

    [Fact]
    public async Task GetWeatherForecast_ShouldReturnForecasts_WhenValidParameters()
    {
        // Arrange
        var forecasts = CreateMockForecasts(5);
        _mockRepository.Setup(r => r.GetForecastsAsync(5)).ReturnsAsync(forecasts);

        // Act
        var result = await _service.GetWeatherForecast(5, 1, 3);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(3, result.Count());
        _mockRepository.Verify(r => r.GetForecastsAsync(5), Times.Once);
    }

    [Fact]
    public async Task GetWeatherForecast_ShouldThrowException_WhenInvalidDays()
    {
        // Act & Assert
        await Assert.ThrowsAsync<ET002FieldSizeError>(() => 
            _service.GetWeatherForecast(-1, 1, 10));
    }

    [Fact]
    public async Task GetWeatherForecast_ShouldThrowException_WhenInvalidStart()
    {
        // Act & Assert
        await Assert.ThrowsAsync<ET002FieldSizeError>(() => 
            _service.GetWeatherForecast(5, 0, 10));
    }

    [Fact]
    public async Task GetWeatherForecast_ShouldThrowException_WhenInvalidLimit()
    {
        // Act & Assert
        await Assert.ThrowsAsync<ET002FieldSizeError>(() => 
            _service.GetWeatherForecast(5, 1, 0));
    }

    #endregion

    #region GetWeatherForecastApi Tests

    [Fact]
    public async Task GetWeatherForecastApi_ShouldReturnSuccessResult_WhenValidParameters()
    {
        // Arrange
        var forecasts = CreateMockForecasts(5);
        _mockRepository.Setup(r => r.GetForecastsAsync(5)).ReturnsAsync(forecasts);

        // Act
        var result = await _service.GetWeatherForecastApi(5, 1, 3);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Success);
        Assert.Equal(200, result.Success.HttpCode);
        Assert.Equal("OK", result.Success.HttpMessage);
        Assert.True(result.Success.Status);
        Assert.Equal(3, result.Success.Data.Count());
        Assert.NotNull(result.Success.Page);
        Assert.NotNull(result.Success.Transaction);
    }

    [Fact]
    public async Task GetWeatherForecastApi_ShouldReturnFailResult_WhenInvalidDays()
    {
        // Act
        var result = await _service.GetWeatherForecastApi(-1, 1, 10);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.NotNull(result.Error);
        Assert.Equal(400, result.Error.HttpCode);
        Assert.Equal("Bad Request", result.Error.HttpMessage);
        Assert.False(result.Error.Status);
    }

    [Fact]
    public async Task GetWeatherForecastApi_ShouldReturnFailResult_WhenRepositoryThrows()
    {
        // Arrange
        _mockRepository.Setup(r => r.GetForecastsAsync(It.IsAny<int>()))
                      .ThrowsAsync(new Exception("Repository error"));

        // Act
        var result = await _service.GetWeatherForecastApi(5, 1, 10);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.NotNull(result.Error);
    }

    [Fact]
    public async Task GetWeatherForecastApi_ShouldSetCorrectPageInfo_WhenCalled()
    {
        // Arrange
        var forecasts = CreateMockForecasts(10);
        _mockRepository.Setup(r => r.GetForecastsAsync(10)).ReturnsAsync(forecasts);

        // Act
        var result = await _service.GetWeatherForecastApi(10, 2, 5);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(2, result.Success.Page.Start);
        Assert.Equal(5, result.Success.Page.Limit);
        Assert.Equal(10, result.Success.Page.Total);
    }

    #endregion

    #region Helper Methods

    private static IEnumerable<WeatherForecast> CreateMockForecasts(int count)
    {
        var forecasts = new List<WeatherForecast>();
        var baseDate = DateTime.Now.Date;

        for (int i = 0; i < count; i++)
        {
            var date = DateOnly.FromDateTime(baseDate.AddDays(i));
            var tempC = 20 + i;
            var tempF = 32 + (int)(tempC / 0.5556);
            forecasts.Add(new WeatherForecast(date, tempC, tempF, "Test"));
        }

        return forecasts;
    }

    #endregion
}