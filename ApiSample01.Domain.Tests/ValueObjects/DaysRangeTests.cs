using ApiSample01.Domain.ValueObjects;
using ApiSample01.Domain.Exceptions;
using Xunit;

namespace ApiSample01.Domain.Tests.ValueObjects;

public class DaysRangeTests
{
    #region Constructor Tests

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(50)]
    [InlineData(100)]
    public void Constructor_ShouldCreateDaysRange_WhenValidValue(int value)
    {
        // Act
        var daysRange = new DaysRange(value);

        // Assert
        Assert.Equal(value, daysRange.Value);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(-10)]
    [InlineData(101)]
    [InlineData(200)]
    public void Constructor_ShouldThrowET002FieldSizeError_WhenInvalidValue(int value)
    {
        // Act & Assert
        var exception = Assert.Throws<ET002FieldSizeError>(() => new DaysRange(value));
        
        Assert.Equal("days", exception.FieldName);
        Assert.Equal(value, exception.Value);
    }

    #endregion

    #region Implicit Conversion Tests

    [Theory]
    [InlineData(0)]
    [InlineData(5)]
    [InlineData(100)]
    public void ImplicitConversion_ToInt_ShouldReturnValue(int value)
    {
        // Arrange
        var daysRange = new DaysRange(value);

        // Act
        int result = daysRange;

        // Assert
        Assert.Equal(value, result);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(5)]
    [InlineData(100)]
    public void ImplicitConversion_FromInt_ShouldCreateDaysRange(int value)
    {
        // Act
        DaysRange daysRange = value;

        // Assert
        Assert.Equal(value, daysRange.Value);
    }

    [Fact]
    public void ImplicitConversion_FromInt_ShouldThrow_WhenInvalidValue()
    {
        // Act & Assert
        Assert.Throws<ET002FieldSizeError>(() =>
        {
            DaysRange daysRange = -1;
        });
    }

    #endregion

    #region Equality Tests

    [Fact]
    public void Equals_ShouldReturnTrue_WhenSameValue()
    {
        // Arrange
        var daysRange1 = new DaysRange(5);
        var daysRange2 = new DaysRange(5);

        // Act & Assert
        Assert.Equal(daysRange1, daysRange2);
        Assert.True(daysRange1.Equals(daysRange2));
    }

    [Fact]
    public void Equals_ShouldReturnFalse_WhenDifferentValue()
    {
        // Arrange
        var daysRange1 = new DaysRange(5);
        var daysRange2 = new DaysRange(10);

        // Act & Assert
        Assert.NotEqual(daysRange1, daysRange2);
        Assert.False(daysRange1.Equals(daysRange2));
    }

    [Fact]
    public void GetHashCode_ShouldBeSame_WhenSameValue()
    {
        // Arrange
        var daysRange1 = new DaysRange(5);
        var daysRange2 = new DaysRange(5);

        // Act & Assert
        Assert.Equal(daysRange1.GetHashCode(), daysRange2.GetHashCode());
    }

    [Fact]
    public void GetHashCode_ShouldBeDifferent_WhenDifferentValue()
    {
        // Arrange
        var daysRange1 = new DaysRange(5);
        var daysRange2 = new DaysRange(10);

        // Act & Assert
        Assert.NotEqual(daysRange1.GetHashCode(), daysRange2.GetHashCode());
    }

    #endregion

    #region ValueObject Behavior Tests

    [Fact]
    public void ValueObject_ShouldInheritFromValueObject()
    {
        // Arrange & Act
        var daysRange = new DaysRange(5);

        // Assert
        Assert.IsAssignableFrom<ValueObject>(daysRange);
    }

    [Fact]
    public void ValueObject_ShouldImplementEqualityCorrectly()
    {
        // Arrange
        var daysRange1 = new DaysRange(5);
        var daysRange2 = new DaysRange(5);
        var daysRange3 = new DaysRange(10);

        // Assert
        Assert.True(daysRange1.Equals(daysRange2));
        Assert.False(daysRange1.Equals(daysRange3));
        Assert.Equal(daysRange1.GetHashCode(), daysRange2.GetHashCode());
    }

    #endregion

    #region Edge Cases Tests

    [Fact]
    public void Constructor_ShouldWork_WithMinimumValue()
    {
        // Act
        var daysRange = new DaysRange(0);

        // Assert
        Assert.Equal(0, daysRange.Value);
    }

    [Fact]
    public void Constructor_ShouldWork_WithMaximumValue()
    {
        // Act
        var daysRange = new DaysRange(100);

        // Assert
        Assert.Equal(100, daysRange.Value);
    }

    [Fact]
    public void ImplicitConversion_ShouldWorkInMathOperations()
    {
        // Arrange
        var daysRange = new DaysRange(5);

        // Act
        int result = daysRange + 10;

        // Assert
        Assert.Equal(15, result);
    }

    #endregion
}