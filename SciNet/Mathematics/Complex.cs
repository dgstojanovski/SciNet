using SciNet.Core;
using SciNet.Core.Attributes;
using static SciNet.Mathematics.Real;

namespace SciNet.Mathematics
{
    [Definition(typeof(ComplexValue), "Numbers with both real and imaginary parts")]
    public sealed class Complex : IDefinition
    {
        [Factory(typeof(ComplexValue), "Complex value with real and imaginary parts")]
        public static ComplexValue Value(RealValue realPart, RealValue imaginaryPart) => 
            new(realPart, imaginaryPart);
        
        [Factory(typeof(ComplexValue), "Complex value with real and imaginary decimal parts")]
        public static ComplexValue Value(double realPart, double imaginaryPart) =>
            new (Decimal(realPart), Decimal(imaginaryPart));

        [Factory(typeof(ComplexValue), "Complex value with real and imaginary integer parts")]
        public static ComplexValue Value(long realPart, long imaginaryPart) =>
            new (Integer(realPart), Integer(imaginaryPart));

        [Factory(typeof(ComplexValue), "Real value")]
        public static ComplexValue Value(RealValue value) =>
            new (value, Zero);

        [Factory(typeof(ComplexValue), "Real decimal value")]
        public static ComplexValue Value(double value) =>
            new (Decimal(value), Zero);

        [Factory(typeof(ComplexValue), "Real integer value")]
        public static ComplexValue Value(long value) =>
            new (Integer(value), Zero);
        
        private Complex() { }
    }
}
