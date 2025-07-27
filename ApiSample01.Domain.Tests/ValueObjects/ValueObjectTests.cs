using ApiSample01.Domain.ValueObjects;
using Xunit;

namespace ApiSample01.Domain.Tests.ValueObjects;

public class ValueObjectTests
{
    #region Test ValueObject Implementations

    private class TestValueObject : ValueObject
    {
        public string Value1 { get; }
        public int Value2 { get; }

        public TestValueObject(string value1, int value2)
        {
            Value1 = value1;
            Value2 = value2;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value1;
            yield return Value2;
        }
    }

    private class SingleValueObject : ValueObject
    {
        public string Value { get; }

        public SingleValueObject(string value)
        {
            Value = value;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }

    private class NullableValueObject : ValueObject
    {
        public string? Value { get; }

        public NullableValueObject(string? value)
        {
            Value = value;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value ?? string.Empty;
        }
    }

    #endregion

    #region Equals Tests

    [Fact]
    public void Equals_ShouldReturnTrue_WhenSameValues()
    {
        // Arrange
        var obj1 = new TestValueObject("test", 123);
        var obj2 = new TestValueObject("test", 123);

        // Act & Assert
        Assert.True(obj1.Equals(obj2));
        Assert.True(obj2.Equals(obj1));
    }

    [Fact]
    public void Equals_ShouldReturnFalse_WhenDifferentValues()
    {
        // Arrange
        var obj1 = new TestValueObject("test", 123);
        var obj2 = new TestValueObject("test", 456);

        // Act & Assert
        Assert.False(obj1.Equals(obj2));
        Assert.False(obj2.Equals(obj1));
    }

    [Fact]
    public void Equals_ShouldReturnTrue_WhenSameReference()
    {
        // Arrange
        var obj1 = new TestValueObject("test", 123);

        // Act & Assert
        Assert.True(obj1.Equals(obj1));
    }

    [Fact]
    public void Equals_ShouldReturnFalse_WhenComparedWithNull()
    {
        // Arrange
        var obj1 = new TestValueObject("test", 123);

        // Act & Assert
        Assert.False(obj1.Equals(null));
    }

    [Fact]
    public void Equals_ShouldReturnFalse_WhenDifferentTypes()
    {
        // Arrange
        var obj1 = new TestValueObject("test", 123);
        var obj2 = new SingleValueObject("test");

        // Act & Assert
        Assert.False(obj1.Equals(obj2));
        Assert.False(obj2.Equals(obj1));
    }

    [Fact]
    public void Equals_ShouldReturnFalse_WhenComparedWithDifferentObjectType()
    {
        // Arrange
        var obj1 = new TestValueObject("test", 123);
        var obj2 = "test";

        // Act & Assert
        Assert.False(obj1.Equals(obj2));
    }

    #endregion

    #region GetHashCode Tests

    [Fact]
    public void GetHashCode_ShouldBeSame_WhenObjectsAreEqual()
    {
        // Arrange
        var obj1 = new TestValueObject("test", 123);
        var obj2 = new TestValueObject("test", 123);

        // Act & Assert
        Assert.Equal(obj1.GetHashCode(), obj2.GetHashCode());
    }

    [Fact]
    public void GetHashCode_ShouldBeDifferent_WhenObjectsAreDifferent()
    {
        // Arrange
        var obj1 = new TestValueObject("test", 123);
        var obj2 = new TestValueObject("test", 456);

        // Act & Assert
        Assert.NotEqual(obj1.GetHashCode(), obj2.GetHashCode());
    }

    [Fact]
    public void GetHashCode_ShouldHandleNullValues()
    {
        // Arrange
        var obj1 = new NullableValueObject(null);
        var obj2 = new NullableValueObject(null);

        // Act & Assert
        Assert.Equal(obj1.GetHashCode(), obj2.GetHashCode());
    }

    private class TestValueObjectWithOperators : ValueObject
    {
        public string Value { get; }

        public TestValueObjectWithOperators(string value)
        {
            Value = value;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        // Expose protected methods for testing
        public static bool TestEqualOperator(ValueObject? left, ValueObject? right)
        {
            return EqualOperator(left, right);
        }

        public static bool TestNotEqualOperator(ValueObject? left, ValueObject? right)
        {
            return NotEqualOperator(left, right);
        }
    }

    #endregion

    #region Protected Operators Tests

    [Fact]
    public void EqualOperator_ShouldReturnTrue_WhenBothNull()
    {
        // Act & Assert
        Assert.True(TestValueObjectWithOperators.TestEqualOperator(null, null));
    }

    [Fact]
    public void EqualOperator_ShouldReturnFalse_WhenOneIsNull()
    {
        // Arrange
        var obj = new TestValueObjectWithOperators("test");

        // Act & Assert
        Assert.False(TestValueObjectWithOperators.TestEqualOperator(obj, null));
        Assert.False(TestValueObjectWithOperators.TestEqualOperator(null, obj));
    }

    [Fact]
    public void EqualOperator_ShouldReturnTrue_WhenSameReference()
    {
        // Arrange
        var obj = new TestValueObjectWithOperators("test");

        // Act & Assert
        Assert.True(TestValueObjectWithOperators.TestEqualOperator(obj, obj));
    }

    [Fact]
    public void EqualOperator_ShouldReturnTrue_WhenEqualValues()
    {
        // Arrange
        var obj1 = new TestValueObjectWithOperators("test");
        var obj2 = new TestValueObjectWithOperators("test");

        // Act & Assert
        Assert.True(TestValueObjectWithOperators.TestEqualOperator(obj1, obj2));
    }

    [Fact]
    public void EqualOperator_ShouldReturnFalse_WhenDifferentValues()
    {
        // Arrange
        var obj1 = new TestValueObjectWithOperators("test1");
        var obj2 = new TestValueObjectWithOperators("test2");

        // Act & Assert
        Assert.False(TestValueObjectWithOperators.TestEqualOperator(obj1, obj2));
    }

    [Fact]
    public void NotEqualOperator_ShouldReturnFalse_WhenBothNull()
    {
        // Act & Assert
        Assert.False(TestValueObjectWithOperators.TestNotEqualOperator(null, null));
    }

    [Fact]
    public void NotEqualOperator_ShouldReturnTrue_WhenOneIsNull()
    {
        // Arrange
        var obj = new TestValueObjectWithOperators("test");

        // Act & Assert
        Assert.True(TestValueObjectWithOperators.TestNotEqualOperator(obj, null));
        Assert.True(TestValueObjectWithOperators.TestNotEqualOperator(null, obj));
    }

    [Fact]
    public void NotEqualOperator_ShouldReturnFalse_WhenEqualValues()
    {
        // Arrange
        var obj1 = new TestValueObjectWithOperators("test");
        var obj2 = new TestValueObjectWithOperators("test");

        // Act & Assert
        Assert.False(TestValueObjectWithOperators.TestNotEqualOperator(obj1, obj2));
    }

    [Fact]
    public void NotEqualOperator_ShouldReturnTrue_WhenDifferentValues()
    {
        // Arrange
        var obj1 = new TestValueObjectWithOperators("test1");
        var obj2 = new TestValueObjectWithOperators("test2");

        // Act & Assert
        Assert.True(TestValueObjectWithOperators.TestNotEqualOperator(obj1, obj2));
    }

    [Fact]
    public void EqualOperator_Line10_Branch1_BothNull_ReferenceEqualsTrue()
    {
        // Para chegar na linha 10: ambos devem ser null (linha 7 = false)
        // Branch 1: ReferenceEquals(null, null) = true (short-circuit)
        Assert.True(TestValueObjectWithOperators.TestEqualOperator(null, null));
    }

    [Fact]
    public void EqualOperator_Line10_Branch2_SameReference_ReferenceEqualsTrue()
    {
        // Para chegar na linha 10: ambos não-null (linha 7 = false)
        // Branch 2: ReferenceEquals(obj, obj) = true (short-circuit)
        var obj = new TestValueObjectWithOperators("test");
        Assert.True(TestValueObjectWithOperators.TestEqualOperator(obj, obj));
    }

    [Fact]
    public void EqualOperator_Line10_Branch3_DifferentRefsEqualValues_LeftNotNullTrue_EqualsTrue()
    {
        // Para chegar na linha 10: ambos não-null (linha 7 = false)
        // Branch 3: ReferenceEquals = false, left != null = true, left.Equals = true
        var obj1 = new TestValueObjectWithOperators("same");
        var obj2 = new TestValueObjectWithOperators("same");
        Assert.True(TestValueObjectWithOperators.TestEqualOperator(obj1, obj2));
    }

    [Fact]
    public void EqualOperator_Line10_Branch4_DifferentRefsDifferentValues_LeftNotNullTrue_EqualsFalse()
    {
        // Para chegar na linha 10: ambos não-null (linha 7 = false)
        // Branch 4: ReferenceEquals = false, left != null = true, left.Equals = false
        var obj1 = new TestValueObjectWithOperators("different1");
        var obj2 = new TestValueObjectWithOperators("different2");
        Assert.False(TestValueObjectWithOperators.TestEqualOperator(obj1, obj2));
    }



    #endregion

    #region Inheritance Tests

    [Fact]
    public void ValueObject_ShouldBeAbstract()
    {
        // Assert
        Assert.True(typeof(ValueObject).IsAbstract);
    }

    [Fact]
    public void ConcreteValueObjects_ShouldInheritFromValueObject()
    {
        // Arrange & Act
        var obj1 = new TestValueObject("test", 123);
        var obj2 = new SingleValueObject("test");

        // Assert
        Assert.IsAssignableFrom<ValueObject>(obj1);
        Assert.IsAssignableFrom<ValueObject>(obj2);
    }

    #endregion

    #region Edge Cases Tests

    [Fact]
    public void ValueObject_ShouldHandleEmptyEqualityComponents()
    {
        // Arrange
        var obj1 = new EmptyValueObject();
        var obj2 = new EmptyValueObject();

        // Act & Assert
        Assert.True(obj1.Equals(obj2));
        
        // Note: GetHashCode will throw for empty sequences due to Aggregate
        // This is expected behavior - ValueObjects should have at least one component
        var exception = Assert.Throws<InvalidOperationException>(() => obj1.GetHashCode());
        Assert.Equal("Sequence contains no elements", exception.Message);
    }

    [Fact]
    public void ValueObject_ShouldHandleComplexEqualityComponents()
    {
        // Arrange
        var obj1 = new ComplexValueObject("test", 123, new[] { 1, 2, 3 });
        var obj2 = new ComplexValueObject("test", 123, new[] { 1, 2, 3 });
        var obj3 = new ComplexValueObject("test", 123, new[] { 1, 2, 4 });

        // Act & Assert
        Assert.True(obj1.Equals(obj2));
        Assert.False(obj1.Equals(obj3));
    }

    [Fact]
    public void ValueObject_ShouldHandleSequenceOfComponents()
    {
        // Arrange
        var obj1 = new ComplexValueObject("test", 123, new[] { 1, 2, 3 });
        var obj2 = new ComplexValueObject("test", 123, new[] { 3, 2, 1 });

        // Act & Assert - Order matters in SequenceEqual
        Assert.False(obj1.Equals(obj2));
    }

    private class EmptyValueObject : ValueObject
    {
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield break;
        }
    }

    private class ComplexValueObject : ValueObject
    {
        public string Value1 { get; }
        public int Value2 { get; }
        public int[] Value3 { get; }

        public ComplexValueObject(string value1, int value2, int[] value3)
        {
            Value1 = value1;
            Value2 = value2;
            Value3 = value3;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value1;
            yield return Value2;
            foreach (var item in Value3)
            {
                yield return item;
            }
        }
    }

    #endregion
}