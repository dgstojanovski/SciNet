using System;

namespace SciNet.Mathematics
{
    public static class Complex
    {
        public static ComplexValue Value(RealValue realPart, RealValue imaginaryPart) => 
            new(realPart, imaginaryPart);

        public static ComplexValue Value(double realPart, double imaginaryPart) =>
            new (Real.Decimal(realPart), Real.Decimal(imaginaryPart));

        public static ComplexValue Value(long realPart, long imaginaryPart) =>
            new (Real.Integer(realPart), Real.Integer(imaginaryPart));
        
        public static ComplexValue Value(double value) =>
            new (Real.Decimal(value), Real.Zero);
        
        public static ComplexValue Value(long value) =>
            new (Real.Integer(value), Real.Zero);
    }

    public readonly struct ComplexValue
    {
        public RealValue RealPart { get; }
        
        public RealValue ImaginaryPart { get; }

        public bool IsReal => ImaginaryPart == Real.Zero;

        public RealValue Magnitude => IsReal 
            ? RealPart 
            : Real.Root(RealPart.Power(2) + ImaginaryPart.Power(2), 2);
        
        public RealValue Argument => IsReal 
            ? Real.Zero 
            : Real.ArcTangent(RealPart / ImaginaryPart);

        public override string ToString() => IsReal
            ? RealPart.ToString()
            : $"({RealPart} + {ImaginaryPart}i)";

        internal ComplexValue(RealValue realPart, RealValue imaginaryPart)
        {
            RealPart = realPart;
            ImaginaryPart = imaginaryPart;
        }
    }
}
