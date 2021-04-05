using System;
using SciNet.Mathematics;
using Xunit;
using Xunit.Abstractions;
using static SciNet.Mathematics.Complex;
using static SciNet.Mathematics.Real;

namespace SciNet.Tests.Mathematics
{
    public class ComplexUnitTests
    {
        private readonly ITestOutputHelper _output;

        public ComplexUnitTests(ITestOutputHelper output)
        {
            _output = output;
        }

        [Theory]
        [InlineData(int.MinValue, int.MinValue)]
        [InlineData(-1, -1)]
        [InlineData(0, -1)]
        [InlineData(-1, 0)]
        [InlineData(0, 0)]
        [InlineData(0, 1)]
        [InlineData(1, 0)]
        [InlineData(1, 1)]
        [InlineData(int.MaxValue, int.MaxValue)]
        public void Factory_Complex_Integer_Positive(long real, long imaginary)
        {
            var complex = Value(real, imaginary);
            DumpComplexValue(complex);
            Assert.True(complex.RealPart == real);
            Assert.True(complex.ImaginaryPart == imaginary);
        }
        
        [Theory]
        [InlineData(int.MinValue)]
        [InlineData(-1)]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(int.MaxValue)]
        public void Factory_Real_Integer_Positive(long real)
        {
            var complex = Value(real);
            DumpComplexValue(complex);
            Assert.True(complex.RealPart == real);
            Assert.True(complex.Magnitude == real);
            Assert.True(complex.ImaginaryPart == Zero);
            Assert.True(complex.Argument == Zero);
        }

        private void DumpComplexValue(ComplexValue complex)
        {
            _output.WriteLine($"Value: {complex}");
            _output.WriteLine($"RealPart: {complex.RealPart}");
            _output.WriteLine($"ImaginaryPart: {complex.ImaginaryPart}");
            _output.WriteLine($"Magnitude: {complex.Magnitude}");
            _output.WriteLine($"Argument: {complex.Argument}");
        }
    }
}

