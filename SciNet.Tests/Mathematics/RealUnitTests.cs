using System;
using Xunit;
using SciNet.Mathematics;
using Xunit.Abstractions;
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
        [InlineData(int.MaxValue)]
        [InlineData(int.MinValue)]
        public void Factory_Decimal_Positive(decimal value)
        {
            var real = Decimal(Convert.ToDouble(value));

            DumpRealValue(real);

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
        [InlineData(int.MaxValue)]
        [InlineData(int.MinValue)]
        public void Factory_Double_Positive(double value)
        {
            var real = Decimal(value);
            
            DumpRealValue(real);
            
            Assert.True(real == value);
            Assert.True(value == real);
        }
        
        private readonly ITestOutputHelper _output;

        public RealUnitTests(ITestOutputHelper output)
        {
            _output = output;
        }

        private void DumpRealValue(RealValue real)
        {
            _output.WriteLine($"Value: {real}");
            _output.WriteLine($"AbsoluteValue: {real.AbsoluteValue}");
            _output.WriteLine($"IntegralPart: {real.IntegralPart}");
            _output.WriteLine($"FractionalPart: {real.FractionalPart}");
            _output.WriteLine($"Square: {real.Square}");
            _output.WriteLine($"SquareRoot: {real.SquareRoot}");
        }
    }
}
