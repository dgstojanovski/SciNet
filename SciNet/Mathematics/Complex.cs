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

    [ValueType(typeof(ComplexValue), "Complex value with both real and imaginary parts")]
    public readonly struct ComplexValue
    {
        #region Properties
        [ValueProperty(typeof(ComplexValue), "The real part of this complex value")]
        public RealValue RealPart { get; }
        
        [ValueProperty(typeof(ComplexValue), "The imaginary part of this complex value")]
        public RealValue ImaginaryPart { get; }

        [ValueProperty(typeof(ComplexValue), "Returns true if this complex value is also a real value")]
        public bool IsReal => ImaginaryPart == Zero;

        [ValueProperty(typeof(ComplexValue), "The magnitude this complex value represented as a cartesian vector")]
        public RealValue Magnitude => IsReal 
            ? RealPart 
            : SquareRoot(RealPart.Square + ImaginaryPart.Square);
        
        [ValueProperty(typeof(ComplexValue), "The angle this complex value represented as a cartesian vector")]
        public RealValue Argument => IsReal 
            ? Zero 
            : ArcTangent(ImaginaryPart / RealPart);
        #endregion

        #region Overrides
        public override string ToString() => IsReal
            ? RealPart.ToString()
            : ImaginaryPart.Value > 0
                ? $"({RealPart} + {(ImaginaryPart.IsInteger && (long)ImaginaryPart.AbsoluteValue == 1 ? string.Empty : ImaginaryPart.AbsoluteValue)}i)"
                : $"({RealPart} - {(ImaginaryPart.IsInteger && (long)ImaginaryPart.AbsoluteValue == 1 ? string.Empty : ImaginaryPart.AbsoluteValue)}i)";
        #endregion

        #region Constructors
        internal ComplexValue(RealValue realPart, RealValue imaginaryPart)
        {
            RealPart = realPart;
            ImaginaryPart = imaginaryPart;
        }
        #endregion
    }
}
