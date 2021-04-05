using SciNet.Core;

namespace SciNet.Mathematics
{
    [ValueType(typeof(ComplexValue), "Complex value with both real and imaginary parts")]
    public readonly struct ComplexValue
    {
        #region Properties
        [ValueProperty(typeof(ComplexValue), "The real part of this complex value")]
        public RealValue RealPart { get; }
        
        [ValueProperty(typeof(ComplexValue), "The imaginary part of this complex value")]
        public RealValue ImaginaryPart { get; }

        [ValueProperty(typeof(ComplexValue), "Returns true if this complex value is also a real value")]
        public bool IsReal => ImaginaryPart == Real.Zero;

        [ValueProperty(typeof(ComplexValue), "The magnitude this complex value represented as a cartesian vector")]
        public RealValue Magnitude => IsReal 
            ? RealPart 
            : Real.SquareRoot(RealPart.Square + ImaginaryPart.Square);
        
        [ValueProperty(typeof(ComplexValue), "The angle this complex value represented as a cartesian vector")]
        public RealValue Argument => IsReal 
            ? Real.Zero 
            : Real.ArcTangent(ImaginaryPart / RealPart);
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