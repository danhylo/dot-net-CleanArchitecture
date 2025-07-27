using ApiSample01.Domain.Exceptions;
using Xunit;

namespace ApiSample01.Domain.Tests.Exceptions;

public class ET002FieldSizeErrorTests
{
    [Fact]
    public void Constructor_ShouldSetAllProperties_WhenValidParametersProvided()
    {
        // Arrange
        const string fieldName = "days";
        const int value = 150;
        const string type = "int";
        const int maxSize = 100;
        const string application = "WeatherForecastApi";

        // Act
        var exception = new ET002FieldSizeError(fieldName, value, type, maxSize, application);

        // Assert
        Assert.Equal(fieldName, exception.FieldName);
        Assert.Equal(value, exception.Value);
        Assert.Equal(type, exception.Type);
        Assert.Equal(maxSize, exception.MaxSize);
        Assert.Equal(application, exception.Application);
    }

    [Fact]
    public void Constructor_ShouldSetHttpProperties_WhenCalled()
    {
        // Arrange & Act
        var exception = new ET002FieldSizeError("days", 150, "int", 100, "WeatherForecastApi");

        // Assert
        Assert.Equal(400, exception.HttpCode);
        Assert.Equal("Bad Request", exception.HttpMessage);
        Assert.Equal("ET:002", exception.ErrorCode);
        Assert.False(exception.Status);
    }

    [Fact]
    public void Constructor_ShouldSetCorrectMessage_WhenCalled()
    {
        // Arrange
        const string fieldName = "limit";
        const int value = 200;
        const string type = "int";
        const int maxSize = 100;
        const string application = "WeatherForecastApi";
        const string expectedMessage = "Field size exceeds expected upper or infer limit: Field [limit], Value [200], Type: [int], MaxSize: [100]";

        // Act
        var exception = new ET002FieldSizeError(fieldName, value, type, maxSize, application);

        // Assert
        Assert.Equal(expectedMessage, exception.ErrorMessage);
        Assert.Equal(expectedMessage, exception.Message);
    }

    [Theory]
    [InlineData("days", -1, "int", 100, "WeatherForecastApi")]
    [InlineData("start", 0, "int", 1, "WeatherForecastApi")]
    [InlineData("limit", 101, "int", 100, "WeatherForecastApi")]
    public void Constructor_ShouldHandleDifferentFieldValidations_WhenCalled(
        string fieldName, int value, string type, int maxSize, string application)
    {
        // Act
        var exception = new ET002FieldSizeError(fieldName, value, type, maxSize, application);

        // Assert
        Assert.Equal(fieldName, exception.FieldName);
        Assert.Equal(value, exception.Value);
        Assert.Equal(type, exception.Type);
        Assert.Equal(maxSize, exception.MaxSize);
        Assert.Equal(application, exception.Application);
        Assert.Equal(400, exception.HttpCode);
        Assert.Equal("Bad Request", exception.HttpMessage);
        Assert.Equal("ET:002", exception.ErrorCode);
    }

    [Fact]
    public void Exception_ShouldInheritFromBaseException_WhenCreated()
    {
        // Act
        var exception = new ET002FieldSizeError("days", 150, "int", 100, "WeatherForecastApi");

        // Assert
        Assert.IsAssignableFrom<BaseException>(exception);
        Assert.IsAssignableFrom<Exception>(exception);
    }
}