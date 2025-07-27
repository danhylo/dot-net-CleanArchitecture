using ApiSample01.Domain.ValueObjects;
using ApiSample01.Domain.Exceptions;
using Xunit;

namespace ApiSample01.Domain.Tests.ValueObjects;

public class LimitValueTests
{
    #region Constructor Tests

    [Theory]
    [InlineData(1)]
    [InlineData(50)]
    [InlineData(100)]
    public void Constructor_ShouldCreateLimitValue_WhenValidValue(int value)
    {
        // Act
        var limitValue = new LimitValue(value);

        // Assert
        Assert.Equal(value, limitValue.Value);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(101)]
    [InlineData(200)]
    public void Constructor_ShouldThrowET002FieldSizeError_WhenInvalidValue(int value)
    {
        // Act & Assert
        var exception = Assert.Throws<ET002FieldSizeError>(() => new LimitValue(value));
        
        Assert.Equal("limit", exception.FieldName);
        Assert.Equal(value, exception.Value);
    }

    #endregion

    #region Implicit Conversion Tests

    [Theory]
    [InlineData(1)]
    [InlineData(50)]
    [InlineData(100)]
    public void ImplicitConversion_ToInt_ShouldReturnValue(int value)
    {
        // Arrange
        var limitValue = new LimitValue(value);

        // Act
        int result = limitValue;

        // Assert
        Assert.Equal(value, result);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(50)]
    [InlineData(100)]
    public void ImplicitConversion_FromInt_ShouldCreateLimitValue(int value)
    {
        // Act
        LimitValue limitValue = value;

        // Assert
        Assert.Equal(value, limitValue.Value);
    }

    [Fact]
    public void ImplicitConversion_FromInt_ShouldThrow_WhenInvalidValue()
    {
        // Act & Assert
        Assert.Throws<ET002FieldSizeError>(() =>
        {
            LimitValue limitValue = 0;
        });
    }

    #endregion

    #region Equality Tests

    [Fact]
    public void Equals_ShouldReturnTrue_WhenSameValue()
    {
        // Arrange
        var limitValue1 = new LimitValue(50);
        var limitValue2 = new LimitValue(50);

        // Act & Assert
        Assert.Equal(limitValue1, limitValue2);
        Assert.True(limitValue1.Equals(limitValue2));
    }

    [Fact]
    public void Equals_ShouldReturnFalse_WhenDifferentValue()
    {
        // Arrange
        var limitValue1 = new LimitValue(50);
        var limitValue2 = new LimitValue(100);

        // Act & Assert
        Assert.NotEqual(limitValue1, limitValue2);
        Assert.False(limitValue1.Equals(limitValue2));
    }

    [Fact]
    public void GetHashCode_ShouldBeSame_WhenSameValue()
    {
        // Arrange
        var limitValue1 = new LimitValue(50);
        var limitValue2 = new LimitValue(50);

        // Act & Assert
        Assert.Equal(limitValue1.GetHashCode(), limitValue2.GetHashCode());
    }

    [Fact]
    public void GetHashCode_ShouldBeDifferent_WhenDifferentValue()
    {
        // Arrange
        var limitValue1 = new LimitValue(50);
        var limitValue2 = new LimitValue(100);

        // Act & Assert
        Assert.NotEqual(limitValue1.GetHashCode(), limitValue2.GetHashCode());
    }

    #endregion

    #region ValueObject Behavior Tests

    [Fact]
    public void ValueObject_ShouldInheritFromValueObject()
    {
        // Arrange & Act
        var limitValue = new LimitValue(50);

        // Assert
        Assert.IsAssignableFrom<ValueObject>(limitValue);
    }

    [Fact]
    public void ValueObject_ShouldImplementEqualityCorrectly()
    {
        // Arrange
        var limitValue1 = new LimitValue(50);
        var limitValue2 = new LimitValue(50);
        var limitValue3 = new LimitValue(100);

        // Assert
        Assert.True(limitValue1.Equals(limitValue2));
        Assert.False(limitValue1.Equals(limitValue3));
        Assert.Equal(limitValue1.GetHashCode(), limitValue2.GetHashCode());
    }

    #endregion

    #region Edge Cases Tests

    [Fact]
    public void Constructor_ShouldWork_WithMinimumValue()
    {
        // Act
        var limitValue = new LimitValue(1);

        // Assert
        Assert.Equal(1, limitValue.Value);
    }

    [Fact]
    public void Constructor_ShouldWork_WithMaximumValue()
    {
        // Act
        var limitValue = new LimitValue(100);

        // Assert
        Assert.Equal(100, limitValue.Value);
    }

    [Fact]
    public void ImplicitConversion_ShouldWorkInMathOperations()
    {
        // Arrange
        var limitValue = new LimitValue(50);

        // Act
        int result = limitValue * 2;

        // Assert
        Assert.Equal(100, result);
    }

    [Fact]
    public void ImplicitConversion_ShouldWorkInComparisons()
    {
        // Arrange
        var limitValue = new LimitValue(50);

        // Act & Assert
        Assert.True(limitValue > 25);
        Assert.True(limitValue < 75);
        Assert.True(limitValue == 50);
    }

    #endregion
}