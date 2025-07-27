using ApiSample01.Domain.ValueObjects;
using ApiSample01.Domain.Exceptions;
using Xunit;

namespace ApiSample01.Domain.Tests.ValueObjects;

public class StartValueTests
{
    #region Constructor Tests

    [Theory]
    [InlineData(1)]
    [InlineData(5)]
    [InlineData(100)]
    [InlineData(1000)]
    public void Constructor_ShouldCreateStartValue_WhenValidValue(int value)
    {
        // Act
        var startValue = new StartValue(value);

        // Assert
        Assert.Equal(value, startValue.Value);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(-10)]
    public void Constructor_ShouldThrowET002FieldSizeError_WhenInvalidValue(int value)
    {
        // Act & Assert
        var exception = Assert.Throws<ET002FieldSizeError>(() => new StartValue(value));
        
        Assert.Equal("start", exception.FieldName);
        Assert.Equal(value, exception.Value);
    }

    #endregion

    #region Implicit Conversion Tests

    [Theory]
    [InlineData(1)]
    [InlineData(5)]
    [InlineData(100)]
    public void ImplicitConversion_ToInt_ShouldReturnValue(int value)
    {
        // Arrange
        var startValue = new StartValue(value);

        // Act
        int result = startValue;

        // Assert
        Assert.Equal(value, result);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(5)]
    [InlineData(100)]
    public void ImplicitConversion_FromInt_ShouldCreateStartValue(int value)
    {
        // Act
        StartValue startValue = value;

        // Assert
        Assert.Equal(value, startValue.Value);
    }

    [Fact]
    public void ImplicitConversion_FromInt_ShouldThrow_WhenInvalidValue()
    {
        // Act & Assert
        Assert.Throws<ET002FieldSizeError>(() =>
        {
            StartValue startValue = 0;
        });
    }

    #endregion

    #region Equality Tests

    [Fact]
    public void Equals_ShouldReturnTrue_WhenSameValue()
    {
        // Arrange
        var startValue1 = new StartValue(5);
        var startValue2 = new StartValue(5);

        // Act & Assert
        Assert.Equal(startValue1, startValue2);
        Assert.True(startValue1.Equals(startValue2));
    }

    [Fact]
    public void Equals_ShouldReturnFalse_WhenDifferentValue()
    {
        // Arrange
        var startValue1 = new StartValue(5);
        var startValue2 = new StartValue(10);

        // Act & Assert
        Assert.NotEqual(startValue1, startValue2);
        Assert.False(startValue1.Equals(startValue2));
    }

    [Fact]
    public void GetHashCode_ShouldBeSame_WhenSameValue()
    {
        // Arrange
        var startValue1 = new StartValue(5);
        var startValue2 = new StartValue(5);

        // Act & Assert
        Assert.Equal(startValue1.GetHashCode(), startValue2.GetHashCode());
    }

    [Fact]
    public void GetHashCode_ShouldBeDifferent_WhenDifferentValue()
    {
        // Arrange
        var startValue1 = new StartValue(5);
        var startValue2 = new StartValue(10);

        // Act & Assert
        Assert.NotEqual(startValue1.GetHashCode(), startValue2.GetHashCode());
    }

    #endregion

    #region ValueObject Behavior Tests

    [Fact]
    public void ValueObject_ShouldInheritFromValueObject()
    {
        // Arrange & Act
        var startValue = new StartValue(5);

        // Assert
        Assert.IsAssignableFrom<ValueObject>(startValue);
    }

    [Fact]
    public void ValueObject_ShouldImplementEqualityCorrectly()
    {
        // Arrange
        var startValue1 = new StartValue(5);
        var startValue2 = new StartValue(5);
        var startValue3 = new StartValue(10);

        // Assert
        Assert.True(startValue1.Equals(startValue2));
        Assert.False(startValue1.Equals(startValue3));
        Assert.Equal(startValue1.GetHashCode(), startValue2.GetHashCode());
    }

    #endregion

    #region Edge Cases Tests

    [Fact]
    public void Constructor_ShouldWork_WithMinimumValue()
    {
        // Act
        var startValue = new StartValue(1);

        // Assert
        Assert.Equal(1, startValue.Value);
    }

    [Fact]
    public void Constructor_ShouldWork_WithLargeValue()
    {
        // Act
        var startValue = new StartValue(1000);

        // Assert
        Assert.Equal(1000, startValue.Value);
    }

    [Fact]
    public void ImplicitConversion_ShouldWorkInMathOperations()
    {
        // Arrange
        var startValue = new StartValue(5);

        // Act
        int result = startValue + 10;

        // Assert
        Assert.Equal(15, result);
    }

    [Fact]
    public void ImplicitConversion_ShouldWorkInComparisons()
    {
        // Arrange
        var startValue = new StartValue(5);

        // Act & Assert
        Assert.True(startValue > 1);
        Assert.True(startValue < 10);
        Assert.True(startValue >= 5);
        Assert.True(startValue <= 5);
    }

    [Fact]
    public void ImplicitConversion_ShouldWorkInPaginationCalculation()
    {
        // Arrange
        var startValue = new StartValue(3);
        var limit = 10;

        // Act - Simulating pagination calculation: skip = (start - 1) * limit
        int skip = (startValue - 1) * limit;

        // Assert
        Assert.Equal(20, skip);
    }

    #endregion
}