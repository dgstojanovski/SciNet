using SciNet.Core;
using static SciNet.Mathematics.Real;

namespace SciNet.Mathematics
{
    [Factory(typeof(ComplexValue), "Numbers with both real and imaginary parts")]
    public static class Complex
    {
        [FactoryMethod(typeof(ComplexValue), "Complex value with real and imaginary parts")]
        public static ComplexValue Value(RealValue realPart, RealValue imaginaryPart) => 
            new(realPart, imaginaryPart);
        
        [FactoryMethod(typeof(ComplexValue), "Complex value with real and imaginary decimal parts")]
        public static ComplexValue Value(double realPart, double imaginaryPart) =>
            new (Decimal(realPart), Decimal(imaginaryPart));

        [FactoryMethod(typeof(ComplexValue), "Complex value with real and imaginary integer parts")]
        public static ComplexValue Value(long realPart, long imaginaryPart) =>
            new (Integer(realPart), Integer(imaginaryPart));

        [FactoryMethod(typeof(ComplexValue), "Real value")]
        public static ComplexValue Value(RealValue value) =>
            new (value, Zero);

        [FactoryMethod(typeof(ComplexValue), "Real decimal value")]
        public static ComplexValue Value(double value) =>
            new (Decimal(value), Zero);

        [FactoryMethod(typeof(ComplexValue), "Real integer value")]
        public static ComplexValue Value(long value) =>
            new (Integer(value), Zero);
    }
}
