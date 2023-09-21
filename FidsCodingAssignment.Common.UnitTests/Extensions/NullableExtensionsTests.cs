using FidsCodingAssignment.Common.Extensions;
using Xunit;

namespace FidsCodingAssignment.Common.UnitTests.Extensions;

public class NullableExtensionsTests
{
    [Theory]
    [InlineData(null, true)]
    [InlineData(0, true)]
    [InlineData(1, false)]
    public void NullOrDefault_NullableInt_ReturnsExpected(int? value, bool expected)
    {
        var result = value.NullOrDefault();

        Assert.Equal(expected, result);
    }
    
    [Theory]
    [InlineData(null, true)]
    [InlineData(false, true)]
    [InlineData(true, false)]
    public void NullOrDefault_NullableBool_ReturnsExpected(bool? value, bool expected)
    {
        var result = value.NullOrDefault();

        Assert.Equal(expected, result);
    }
    
    [Theory]
    [InlineData(null, true)]
    [InlineData(default(double), true)]
    [InlineData(0.1, false)]
    public void NullOrDefault_NullableDouble_ReturnsExpected(double? value, bool expected)
    {
        var result = value.NullOrDefault();

        Assert.Equal(expected, result);
    }

    [Fact]
    public void NullOrDefault_NullDecimal_ReturnsTrue()
    {
        var value = (decimal?) null;
        
        var result = value.NullOrDefault();
        
        Assert.True(result);
    }
    
    [Fact]
    public void NullOrDefault_DefaultDecimal_ReturnsTrue()
    {
        decimal? value = 0m;
        
        var result = value.NullOrDefault();
        
        Assert.True(result);
    }
    
    [Fact]
    public void NullOrDefault_DecimalGreaterThan0_ReturnsFalse()
    {
        decimal? value = 0.1m;
        
        var result = value.NullOrDefault();
        
        Assert.False(result);
    }
}