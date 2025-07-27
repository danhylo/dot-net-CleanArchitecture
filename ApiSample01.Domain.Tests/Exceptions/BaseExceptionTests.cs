using ApiSample01.Domain.Exceptions;
using Xunit;

namespace ApiSample01.Domain.Tests.Exceptions;

public class BaseExceptionTests
{
    #region Test Exception Implementation

    private class TestBaseException : BaseException
    {
        public TestBaseException(string message) : base(message)
        {
            HttpCode = 400;
            HttpMessage = "Bad Request";
            ErrorCode = "TEST:001";
            Application = "TestApp";
        }

        public TestBaseException(string message, Exception innerException) : base(message, innerException)
        {
            HttpCode = 500;
            HttpMessage = "Internal Server Error";
            ErrorCode = "TEST:002";
            Application = "TestApp";
        }
    }

    #endregion

    #region Constructor Tests

    [Fact]
    public void Constructor_ShouldSetMessage_WhenMessageProvided()
    {
        // Arrange
        const string message = "Test error message";

        // Act
        var exception = new TestBaseException(message);

        // Assert
        Assert.Equal(message, exception.Message);
        Assert.Equal(message, exception.ErrorMessage);
    }

    [Fact]
    public void Constructor_ShouldSetMessageAndInnerException_WhenBothProvided()
    {
        // Arrange
        const string message = "Test error message";
        var innerException = new ArgumentException("Inner exception");

        // Act
        var exception = new TestBaseException(message, innerException);

        // Assert
        Assert.Equal(message, exception.Message);
        Assert.Equal(message, exception.ErrorMessage);
        Assert.Equal(innerException, exception.InnerException);
    }

    #endregion

    #region Properties Tests

    [Fact]
    public void Properties_ShouldHaveDefaultValues_WhenNotSet()
    {
        // Act
        var exception = new TestBaseException("test");

        // Assert
        Assert.Equal(400, exception.HttpCode);
        Assert.Equal("Bad Request", exception.HttpMessage);
        Assert.False(exception.Status);
        Assert.Equal("TEST:001", exception.ErrorCode);
        Assert.Equal("test", exception.ErrorMessage);
        Assert.Equal("TestApp", exception.Application);
    }

    [Fact]
    public void Status_ShouldAlwaysBeFalse()
    {
        // Act
        var exception = new TestBaseException("test");

        // Assert
        Assert.False(exception.Status);
    }

    #endregion

    #region Inheritance Tests

    [Fact]
    public void BaseException_ShouldBeAbstract()
    {
        // Assert
        Assert.True(typeof(BaseException).IsAbstract);
    }

    [Fact]
    public void BaseException_ShouldInheritFromException()
    {
        // Act
        var exception = new TestBaseException("test");

        // Assert
        Assert.IsAssignableFrom<Exception>(exception);
        Assert.IsAssignableFrom<BaseException>(exception);
    }

    #endregion

    #region Exception Behavior Tests

    [Fact]
    public void Exception_ShouldBeThrowable()
    {
        // Arrange
        const string message = "Test exception";

        // Act & Assert
        try
        {
            throw new TestBaseException(message);
        }
        catch (TestBaseException ex)
        {
            Assert.Equal(message, ex.Message);
        }
    }

    [Fact]
    public void Exception_ShouldPreserveStackTrace()
    {
        // Arrange
        TestBaseException? caughtException = null;

        // Act
        try
        {
            ThrowTestException();
        }
        catch (TestBaseException ex)
        {
            caughtException = ex;
        }

        // Assert
        Assert.NotNull(caughtException);
        Assert.Contains(nameof(ThrowTestException), caughtException.StackTrace ?? string.Empty);
    }

    [Fact]
    public void Exception_ShouldHandleInnerException()
    {
        // Arrange
        var innerException = new InvalidOperationException("Inner error");
        const string message = "Outer error";

        // Act
        var exception = new TestBaseException(message, innerException);

        // Assert
        Assert.Equal(message, exception.Message);
        Assert.Equal(innerException, exception.InnerException);
        Assert.Equal("Inner error", exception.InnerException.Message);
    }

    private static void ThrowTestException()
    {
        throw new TestBaseException("Test exception for stack trace");
    }

    #endregion

    #region Property Protection Tests

    [Fact]
    public void Properties_ShouldBeProtectedSet()
    {
        // This test verifies that properties have protected setters
        // by checking that they can be set in derived classes but not externally
        
        // Act
        var exception = new TestBaseException("test");

        // Assert - Properties should be readable but not settable from outside
        Assert.Equal(400, exception.HttpCode);
        Assert.Equal("Bad Request", exception.HttpMessage);
        Assert.Equal("TEST:001", exception.ErrorCode);
        Assert.Equal("TestApp", exception.Application);
        
        // Verify properties don't have public setters
        var httpCodeProperty = typeof(BaseException).GetProperty(nameof(BaseException.HttpCode));
        var httpMessageProperty = typeof(BaseException).GetProperty(nameof(BaseException.HttpMessage));
        var errorCodeProperty = typeof(BaseException).GetProperty(nameof(BaseException.ErrorCode));
        var applicationProperty = typeof(BaseException).GetProperty(nameof(BaseException.Application));
        
        // Verify properties have protected setters (can be set in derived classes)
        Assert.NotNull(httpCodeProperty);
        Assert.NotNull(httpMessageProperty);
        Assert.NotNull(errorCodeProperty);
        Assert.NotNull(applicationProperty);
        
        // Properties should have setters that are protected
        Assert.True(httpCodeProperty.SetMethod?.IsFamily ?? false);
        Assert.True(httpMessageProperty.SetMethod?.IsFamily ?? false);
        Assert.True(errorCodeProperty.SetMethod?.IsFamily ?? false);
        Assert.True(applicationProperty.SetMethod?.IsFamily ?? false);
    }

    [Fact]
    public void Status_ShouldBeReadOnly()
    {
        // Act
        var exception = new TestBaseException("test");

        // Assert
        var statusProperty = typeof(BaseException).GetProperty(nameof(BaseException.Status));
        Assert.Null(statusProperty?.SetMethod); // No setter at all
        Assert.False(exception.Status);
    }

    #endregion

    #region Edge Cases Tests

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("Very long error message that exceeds normal length to test if the exception can handle long messages properly")]
    public void Constructor_ShouldHandleDifferentMessageLengths(string message)
    {
        // Act
        var exception = new TestBaseException(message);

        // Assert
        Assert.Equal(message, exception.Message);
        Assert.Equal(message, exception.ErrorMessage);
    }

    [Fact]
    public void Constructor_ShouldHandleNullInnerException()
    {
        // Arrange
        const string message = "Test message";

        // Act
        var exception = new TestBaseException(message, null!);

        // Assert
        Assert.Equal(message, exception.Message);
        Assert.Null(exception.InnerException);
    }

    #endregion
}