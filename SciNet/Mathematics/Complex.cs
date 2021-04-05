using SciNet.Core;
using SciNet.Core.Attributes;
using static SciNet.Mathematics.Real;

namespace SciNet.Mathematics
{
    [Definition(typeof(ComplexValue), "Numbers with both real and imaginary parts")]
    public sealed class Complex : IDefinition
    {
        private Complex()
        {
        }

        [Factory(typeof(ComplexValue), "Complex value with real and imaginary parts")]
        public static ComplexValue Value(RealValue realPart, RealValue imaginaryPart)
        {
            return new(realPart, imaginaryPart);
        }

        [Factory(typeof(ComplexValue), "Complex value with real and imaginary decimal parts")]
        public static ComplexValue Value(double realPart, double imaginaryPart)
        {
            return new(Decimal(realPart), Decimal(imaginaryPart));
        }

        [Factory(typeof(ComplexValue), "Complex value with real and imaginary integer parts")]
        public static ComplexValue Value(long realPart, long imaginaryPart)
        {
            return new(Integer(realPart), Integer(imaginaryPart));
        }

        [Factory(typeof(ComplexValue), "Real value")]
        public static ComplexValue Value(RealValue value)
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