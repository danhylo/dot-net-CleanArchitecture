using Microsoft.AspNetCore.Mvc;
using ApiSample01.Api.Controllers.weather.api.v1;
using ApiSample01.Application.Interfaces;
using ApiSample01.Application.Dto;
using ApiSample01.Application.Common.Base;
using ApiSample01.Application.Common.Api.Base;
using ApiSample01.Api.Models;
using Moq;
using Xunit;

namespace ApiSample01.Api.Tests.Controllers;

public class WeatherForecastApiControllerTests
{
    private readonly Mock<IWeatherForecastApplicationService> _mockService;
    private readonly WeatherForecastApiController _controller;

    public WeatherForecastApiControllerTests()
    {
        _mockService = new Mock<IWeatherForecastApplicationService>();
        _controller = new WeatherForecastApiController(_mockService.Object);
    }

    #region Constructor Tests

    [Fact]
    public void Constructor_ShouldCreateInstance_WhenValidService()
    {
        // Act
        var controller = new WeatherForecastApiController(_mockService.Object);

        // Assert
        Assert.NotNull(controller);
        Assert.IsAssignableFrom<ControllerBase>(controller);
    }

    #endregion

    #region Get Method Tests

    [Fact]
    public async Task Get_ShouldReturnOk_WhenServiceReturnsSuccess()
    {
        // Arrange
        var request = new WeatherForecastRequest { Days = 5, Start = 1, Limit = 10 };
        var response = CreateSuccessResponse();
        var result = Result<WeatherForecastApiResponseDto, ApiErrorResponse>.Ok(response);
        
        _mockService.Setup(s => s.GetWeatherForecastApi(5, 1, 10)).ReturnsAsync(result);

        // Act
        var actionResult = await _controller.Get(request);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(actionResult);
        Assert.Equal(200, okResult.StatusCode);
        Assert.Equal(response, okResult.Value);
        _mockService.Verify(s => s.GetWeatherForecastApi(5, 1, 10), Times.Once);
    }

    [Fact]
    public async Task Get_ShouldReturnBadRequest_WhenServiceReturnsError()
    {
        // Arrange
        var request = new WeatherForecastRequest { Days = -1, Start = 1, Limit = 10 };
        var error = CreateErrorResponse(400, "Bad Request");
        var result = Result<WeatherForecastApiResponseDto, ApiErrorResponse>.Fail(error);
        
        _mockService.Setup(s => s.GetWeatherForecastApi(-1, 1, 10)).ReturnsAsync(result);

        // Act
        var actionResult = await _controller.Get(request);

        // Assert
        var statusResult = Assert.IsType<ObjectResult>(actionResult);
        Assert.Equal(400, statusResult.StatusCode);
        Assert.Equal(error, statusResult.Value);
    }

    [Fact]
    public async Task Get_ShouldReturnInternalServerError_WhenServiceReturns500()
    {
        // Arrange
        var request = new WeatherForecastRequest { Days = 5, Start = 1, Limit = 10 };
        var error = CreateErrorResponse(500, "Internal Server Error");
        var result = Result<WeatherForecastApiResponseDto, ApiErrorResponse>.Fail(error);
        
        _mockService.Setup(s => s.GetWeatherForecastApi(5, 1, 10)).ReturnsAsync(result);

        // Act
        var actionResult = await _controller.Get(request);

        // Assert
        var statusResult = Assert.IsType<ObjectResult>(actionResult);
        Assert.Equal(500, statusResult.StatusCode);
        Assert.Equal(error, statusResult.Value);
    }

    [Fact]
    public async Task Get_ShouldUseDefaultValues_WhenRequestHasDefaults()
    {
        // Arrange
        var request = new WeatherForecastRequest(); // Uses default values
        var response = CreateSuccessResponse();
        var result = Result<WeatherForecastApiResponseDto, ApiErrorResponse>.Ok(response);
        
        _mockService.Setup(s => s.GetWeatherForecastApi(2, 1, 100)).ReturnsAsync(result);

        // Act
        var actionResult = await _controller.Get(request);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(actionResult);
        Assert.Equal(200, okResult.StatusCode);
        _mockService.Verify(s => s.GetWeatherForecastApi(2, 1, 100), Times.Once);
    }

    [Theory]
    [InlineData(1, 1, 50)]
    [InlineData(10, 2, 25)]
    [InlineData(100, 5, 10)]
    public async Task Get_ShouldPassCorrectParameters_WhenCalled(int days, int start, int limit)
    {
        // Arrange
        var request = new WeatherForecastRequest { Days = days, Start = start, Limit = limit };
        var response = CreateSuccessResponse();
        var result = Result<WeatherForecastApiResponseDto, ApiErrorResponse>.Ok(response);
        
        _mockService.Setup(s => s.GetWeatherForecastApi(days, start, limit)).ReturnsAsync(result);

        // Act
        await _controller.Get(request);

        // Assert
        _mockService.Verify(s => s.GetWeatherForecastApi(days, start, limit), Times.Once);
    }

    #endregion

    #region Controller Attributes Tests

    [Fact]
    public void Controller_ShouldHaveApiControllerAttribute()
    {
        // Act
        var attributes = typeof(WeatherForecastApiController).GetCustomAttributes(typeof(ApiControllerAttribute), false);

        // Assert
        Assert.Single(attributes);
    }

    [Fact]
    public void Controller_ShouldHaveCorrectRoute()
    {
        // Act
        var attributes = typeof(WeatherForecastApiController).GetCustomAttributes(typeof(RouteAttribute), false);
        var routeAttribute = attributes.FirstOrDefault() as RouteAttribute;

        // Assert
        Assert.NotNull(routeAttribute);
        Assert.Equal("weather/api/v1", routeAttribute.Template);
    }

    [Fact]
    public void Get_ShouldHaveHttpGetAttribute()
    {
        // Act
        var method = typeof(WeatherForecastApiController).GetMethod(nameof(WeatherForecastApiController.Get));
        var attributes = method?.GetCustomAttributes(typeof(HttpGetAttribute), false);
        var httpGetAttribute = attributes?.FirstOrDefault() as HttpGetAttribute;

        // Assert
        Assert.NotNull(httpGetAttribute);
        Assert.Equal("forecast", httpGetAttribute.Template);
    }

    #endregion

    #region Helper Methods

    private static WeatherForecastApiResponseDto CreateSuccessResponse()
    {
        return new WeatherForecastApiResponseDto
        {
            HttpCode = 200,
            HttpMessage = "OK",
            Status = true,
            Data = new List<Domain.Entities.WeatherForecast.WeatherForecast>(),
            Page = new Page { Start = 1, Limit = 10, Total = 0 },
            Transaction = new Transaction { LocalTransactionId = Guid.NewGuid().ToString(), LocalTransactionDate = DateTime.UtcNow }
        };
    }

    private static ApiErrorResponse CreateErrorResponse(int httpCode, string message)
    {
        return new ApiErrorResponse
        {
            HttpCode = httpCode,
            HttpMessage = message,
            Status = false,
            Error = new Error
            {
                Code = "TEST:001",
                Message = "Test error",
                Application = "TestApp"
            },
            Transaction = new Transaction { LocalTransactionId = Guid.NewGuid().ToString(), LocalTransactionDate = DateTime.UtcNow }
        };
    }

    #endregion
}