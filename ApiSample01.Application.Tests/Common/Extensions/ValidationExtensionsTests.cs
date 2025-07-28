namespace ApiSample01.Application.Tests.Common.Extensions;

using ApiSample01.Application.Common.Extensions;
using FluentValidation;
using FluentValidation.Results;
using Moq;

public class ValidationExtensionsTests
{
    [Fact]
    public void ValidateAndThrowCustom_WhenValidationPasses_ShouldNotThrow()
    {
        // Arrange
        var validator = new Mock<IValidator<string>>();
        validator.Setup(v => v.Validate(It.IsAny<string>()))
                .Returns(new ValidationResult());

        // Act & Assert
        var exception = Record.Exception(() => validator.Object.ValidateAndThrowCustom("test"));
        Assert.Null(exception);
    }

    [Fact]
    public void ValidateAndThrowCustom_WhenValidationFails_ShouldThrowArgumentException()
    {
        // Arrange
        var validator = new Mock<IValidator<string>>();
        var validationFailure = new ValidationFailure("Property", "Error message");
        var validationResult = new ValidationResult(new[] { validationFailure });
        
        validator.Setup(v => v.Validate(It.IsAny<string>()))
                .Returns(validationResult);

        // Act & Assert
        var exception = Assert.Throws<ArgumentException>(() => 
            validator.Object.ValidateAndThrowCustom("test"));
        
        Assert.Equal("Error message", exception.Message);
    }

    [Fact]
    public void ValidateAndThrowCustom_WhenMultipleValidationErrors_ShouldThrowWithFirstError()
    {
        // Arrange
        var validator = new Mock<IValidator<string>>();
        var validationFailures = new[]
        {
            new ValidationFailure("Property1", "First error"),
            new ValidationFailure("Property2", "Second error")
        };
        var validationResult = new ValidationResult(validationFailures);
        
        validator.Setup(v => v.Validate(It.IsAny<string>()))
                .Returns(validationResult);

        // Act & Assert
        var exception = Assert.Throws<ArgumentException>(() => 
            validator.Object.ValidateAndThrowCustom("test"));
        
        Assert.Equal("First error", exception.Message);
    }
}