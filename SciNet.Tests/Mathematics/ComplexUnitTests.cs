using Xunit;
using Xunit.Abstractions;
using static SciNet.Mathematics.Real;
using static SciNet.Mathematics.Complex;

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
        [InlineData(long.MinValue, long.MinValue)]
        [InlineData(-1, -1)]
        [InlineData(0, -1)]
        [InlineData(-1, 0)]
        [InlineData(0, 0)]
        [InlineData(0, 1)]
        [InlineData(1, 0)]
        [InlineData(1, 1)]
        [InlineData(long.MaxValue, long.MaxValue)]
        public void Factory_Complex_Integer_Positive(long real, long imaginary)
        {
            var complex = Value(real, imaginary);

            _output.WriteLine(complex.ToInline());
            _output.WriteLine(complex.ToJson(true));

            Assert.True(complex.RealPart == real);
            Assert.True(complex.ImaginaryPart == imaginary);
        }

        [Theory]
        [InlineData(double.MinValue, double.MinValue)]
        [InlineData(-1.0, -1.0)]
        [InlineData(0.0, -1.0)]
        [InlineData(-1.0, 0.0)]
        [InlineData(0.0, 0.0)]
        [InlineData(0.0, 1.0)]
        [InlineData(1.0, 0.0)]
        [InlineData(1.0, 1.0)]
        [InlineData(double.MaxValue, double.MaxValue)]
        public void Factory_Complex_Double_Positive(double real, double imaginary)
        {
            var complex = Value(real, imaginary);

            _output.WriteLine(complex.ToInline());
            _output.WriteLine(complex.ToJson(true));

            Assert.True(complex.RealPart == real);
            Assert.True(complex.ImaginaryPart == imaginary);
        }

        [Theory]
        [InlineData(double.MinValue, double.MinValue)]
        [InlineData(-1.0, -1.0)]
        [InlineData(0.0, -1.0)]
        [InlineData(-1.0, 0.0)]
        [InlineData(0.0, 0.0)]
        [InlineData(0.0, 1.0)]
        [InlineData(1.0, 0.0)]
        [InlineData(1.0, 1.0)]
        [InlineData(double.MaxValue, double.MaxValue)]
        public void Factory_Complex_Real_Positive(double real, double imaginary)
        {
            var complex = Value(Decimal(real), Decimal(imaginary));

            _output.WriteLine(complex.ToInline());
            _output.WriteLine(complex.ToJson(true));
            
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

            _output.WriteLine(complex.ToInline());
            _output.WriteLine(complex.ToJson(true));
            
            Assert.True(complex.RealPart == real);
            Assert.True(complex.Magnitude == real);
            Assert.True(complex.ImaginaryPart == Zero);
            Assert.True(complex.Argument == Zero);
        }

        [Theory]
        [InlineData(double.MinValue)]
        [InlineData(-1.0)]
        [InlineData(0.0)]
        [InlineData(1.0)]
        [InlineData(double.MaxValue)]
        public void Factory_Real_Decimal_Positive(double real)
        {
            var complex = Value(real);

            _output.WriteLine(complex.ToInline());
            _output.WriteLine(complex.ToJson(true));
            
            Assert.True(complex.RealPart == real);
            Assert.True(complex.Magnitude == real);
            Assert.True(complex.ImaginaryPart == Zero);
            Assert.True(complex.Argument == Zero);
        }
    }
}
