using System;
using Xunit;
using static SciNet.Mathematics.Real;
using static SciNet.Mathematics.Complex;

namespace SciNet.Tests.Mathematics
{
    public class RealUnitTests
    {
        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(long.MaxValue)]
        [InlineData(long.MinValue)]
        public void Factory_Integer_Positive(long value)
        {
            var real = Integer(value);
            Assert.True(real == value);
            Assert.True(value == real);
        }
        
        [Theory]
        [InlineData(-1)]
        [InlineData(-1.0)]
        [InlineData(0)]
        [InlineData(0.0)]
        [InlineData(1)]
        [InlineData(1.0)]
        [InlineData(2)]
        [InlineData(2.0)]
        [InlineData(3.14159)]
        [InlineData(long.MaxValue)]
        [InlineData(long.MinValue)]
        public void Factory_Decimal_Positive(decimal value)
        {
            var real = Decimal(Convert.ToDouble(value));
            Assert.True(real == value);
            Assert.True(value == real);
        }
        
        [Theory]
        [InlineData(-1)]
        [InlineData(-1.0)]
        [InlineData(0)]
        [InlineData(0.0)]
        [InlineData(1)]
        [InlineData(1.0)]
        [InlineData(2)]
        [InlineData(2.0)]
        [InlineData(3.14159)]
        [InlineData(long.MaxValue)]
        [InlineData(long.MinValue)]
        public void Factory_Double_Positive(double value)
        {
            var real = Decimal(value);
            Assert.True(real == value);
            Assert.True(value == real);
        }
    }
}
