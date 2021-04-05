using SciNet.Core;
using SciNet.Core.Attributes;
using static SciNet.Mathematics.Real;

namespace SciNet.Mathematics
{
    [Definition(typeof(ComplexValue), "Numbers with both real and imaginary parts")]
    public sealed class Complex : IDefinition
    {
        private Complex() { }

        [Factory(typeof(ComplexValue), "Complex value with real and imaginary parts")]
        public static ComplexValue Value(RealValue real, RealValue imaginary)
        {
            return new(real, imaginary);
        }
        
        [Factory(typeof(ComplexValue), "Complex value with real and imaginary parts")]
        public static ComplexValue Value((RealValue Real, RealValue Imaginary) value)
        {
            var (real, imaginary) = value;
            return new ComplexValue(real, imaginary);
        }

        [Factory(typeof(ComplexValue), "Complex value with real and imaginary decimal parts")]
        public static ComplexValue Value(double real, double imaginary)
        {
            return new(Decimal(real), Decimal(imaginary));
        }
        
        [Factory(typeof(ComplexValue), "Complex value with real and imaginary decimal parts")]
        public static ComplexValue Value((double Real, double Imaginary) value)
        {
            var (real, imaginary) = value;
            return new ComplexValue(Decimal(real), Decimal(imaginary));
        }

        [Factory(typeof(ComplexValue), "Complex value with real and imaginary integer parts")]
        public static ComplexValue Value(long real, long imaginary)
        {
            return new(Integer(real), Integer(imaginary));
        }
        
        [Factory(typeof(ComplexValue), "Complex value with real and imaginary integer parts")]
        public static ComplexValue Value((long Real, long Imaginary) value)
        {
            var (real, imaginary) = value;
            return new ComplexValue(Integer(real), Integer(imaginary));
        }

        [Factory(typeof(ComplexValue), "Real value")]
        public static ComplexValue ToComplex(RealValue value)
        {
            return new(value, Zero);
        }

        [Factory(typeof(ComplexValue), "Real decimal value")]
        public static ComplexValue Value(double value)
        {
            return new(Decimal(value), Zero);
        }

        [Factory(typeof(ComplexValue), "Real integer value")]
        public static ComplexValue Value(long value)
        {
            return new(Integer(value), Zero);
        }
    }
}
