using System;
using static System.Convert;
using static System.Math;

namespace SciNet.Mathematics
{
    [Factory(typeof(RealValue), "Functions for operating on real numbers")]
    public static class Real
    {
        #region Factories
        public static RealValue Decimal(double value) => new(value);
        
        public static RealValue Integer(long value) => new(value);

        public static RealValue Rational(long numerator, long denominator) => new(numerator, denominator);
        #endregion

        #region Values
        [SpecialValue(typeof(RealValue), "An arbitrarily small decimal value")]
        public static RealValue Epsilon => new(double.Epsilon);
        
        [SpecialValue(typeof(RealValue), "The additive identity of the set of real numbers")]
        public static RealValue Zero => new(0);
        
        [SpecialValue(typeof(RealValue), "The multiplicative identity of the set of real numbers")]
        public static RealValue One => new(1);
        #endregion

        #region Transformations
        public static RealValue Absolute(RealValue value) =>
            Decimal(value.AbsoluteValue);
        public static RealValue SquareRoot(RealValue value) =>
            value.SquareRoot;
        
        public static RealValue Root(RealValue value, int root) =>
            value.Root(root);
        
        public static RealValue Square(RealValue value) =>
            value.Square;
        
        public static RealValue Power(RealValue value, int power) => 
            value.Power(power);
        
        public static RealValue ArcTangent(RealValue value) => value.ArcTangent;
        #endregion
    }

    [ValueType(typeof(RealValue), "Any value in the set of real numbers")]
    public readonly struct RealValue
    {
        #region Properties
        [ValueProperty(typeof(RealValue), "The signed value of this real number")]
        public double Value => IntegerValue ?? DecimalValue;
        
        [ValueProperty(typeof(RealValue), "The absolute value of this real number")]
        public double AbsoluteValue => Abs(IntegerValue ?? DecimalValue);

        [ValueProperty(typeof(RealValue), "The absolute value of this real number")]
        public long IntegralPart => IntegerValue ?? ToInt64(Round(DecimalValue));
        
        [ValueProperty(typeof(RealValue), "The absolute value of this real number")]
        public long FractionalPart => IntegerValue ?? ToInt64(DecimalValue - Round(DecimalValue));

        [ValueProperty(typeof(RealValue), "Returns true if this real number is an integer")]
        public bool IsInteger => IntegerValue != null;
        
        [ValueProperty(typeof(RealValue), "Returns true if this real number is a rational number")]
        public bool IsRational => IsInteger || Numerator != null && Denominator != null;
        
        [ValueProperty(typeof(RealValue), "The square root of this real number")]
        public RealValue SquareRoot => Real.Decimal(Pow(DecimalValue, 2));

        [ValueProperty(typeof(RealValue), "Returns the specified root of this real number")]
        public RealValue Root(int power) => Real.Decimal(Pow(DecimalValue, 1.0 / power));
        
        [ValueProperty(typeof(RealValue), "Returns the square of this real number")]
        public RealValue Square => Real.Decimal(Pow(DecimalValue, 2));
        
        [ValueProperty(typeof(RealValue), "Returns the specified power of this real number")]
        public RealValue Power(double power) => Real.Decimal(Pow(DecimalValue, power));
        
        [ValueProperty(typeof(RealValue), "Returns the arctangent of this real value in radians")]
        public RealValue ArcTangent => Real.Decimal(Atan(DecimalValue));
        #endregion

        #region Overrides
        public override bool Equals(object other) => other != null && this == other;

        public override int GetHashCode() => base.GetHashCode();
        
        public override string ToString() => IsInteger 
            ? $"{IntegerValue}" 
            : IsRational ? $"{Numerator}/{Denominator}" : $"{DecimalValue}";
        #endregion

        #region Operators
        public static bool operator ==(RealValue first, RealValue second) => first.IsInteger && second.IsInteger
            ? first.IntegerValue == second.IntegerValue
            : first.IsRational && second.IsRational
                ? first.Numerator == second.Numerator && first.Denominator == second.Denominator
                : Abs(first.Value - second.Value) <= Real.Epsilon.Value;
        
        public static bool operator !=(RealValue first, RealValue second) => first.IsInteger && second.IsInteger
            ? first.IntegerValue != second.IntegerValue
            : first.IsRational && second.IsRational
                ? first.Numerator != second.Numerator || first.Denominator != second.Denominator
                : Abs(first.Value - second.Value) <= Real.Epsilon.Value;

        public static bool operator ==(RealValue first, long second) => first.IsInteger
            ? first.IntegerValue == second
            : Abs(first.Value - ToDouble(second)) <= Real.Epsilon.Value;
        
        public static bool operator !=(RealValue first, long second) => first.IsInteger
            ? first.IntegerValue != second
            : Abs(first.Value - ToDouble(second)) <= Real.Epsilon.Value;
        
        public static bool operator ==(long first, RealValue second) => second.Denominator == 1
            ? first == second.Numerator
            : Abs(first - second.Value) <= Real.Epsilon.Value;
        
        public static bool operator !=(long first, RealValue second) =>
            Abs(first - second.Value) > Real.Epsilon.Value;
        
        public static bool operator ==(RealValue first, double second) => 
            Abs(first.Value - second) <= Real.Epsilon.Value;
        
        public static bool operator !=(RealValue first, double second) =>
            Abs(first.Value - second) > Real.Epsilon.Value;
        
        public static bool operator ==(double first, RealValue second) => 
            Abs(first - second.Value) <= Real.Epsilon.Value;
        
        public static bool operator !=(double first, RealValue second) =>
            Abs(first - second.Value) > Real.Epsilon.Value;
        
        public static bool operator ==(RealValue first, decimal second) => 
            Abs(first.Value - ToDouble(second)) <= Real.Epsilon.Value;
        
        public static bool operator !=(RealValue first, decimal second) =>
            Abs(first.Value - ToDouble(second)) > Real.Epsilon.Value;
        
        public static bool operator ==(decimal first, RealValue second) => 
            Abs(ToDouble(first) - second.Value) <= Real.Epsilon.Value;
        
        public static bool operator !=(decimal first, RealValue second) =>
            Abs(ToDouble(first) - second.Value) > Real.Epsilon.Value;
        
        public static bool operator ==(RealValue first, object second) => 
            Abs(first.Value - ToDouble(second)) <= Real.Epsilon.Value;
        
        public static bool operator !=(RealValue first, object second) =>
            Abs(first.Value - ToDouble(second)) > Real.Epsilon.Value;

        public static RealValue operator +(RealValue first, RealValue second) =>
            first.IntegerValue != null && second.IntegerValue != null
                ? Real.Integer(first.IntegerValue.Value + second.IntegerValue.Value)
                : Real.Decimal(first.Value + second.Value);
        
        public static RealValue operator *(RealValue first, RealValue second) =>
            first.IntegerValue != null && second.IntegerValue != null
                ? Real.Integer(first.IntegerValue.Value * second.IntegerValue.Value)
                : Real.Decimal(first.Value * second.Value);
        
        public static RealValue operator /(RealValue first, RealValue second) =>
            first.IntegerValue != null && second.IntegerValue != null
                ? Real.Rational(first.IntegerValue.Value, second.IntegerValue.Value)
                : Real.Decimal(first.Value / second.Value);
        #endregion

        #region Constructors
        internal RealValue(double value)
        {
            DecimalValue = value;
            IntegerValue = null;
            Numerator = null;
            Denominator = null;
        }
        
        internal RealValue(long value)
        {
            DecimalValue = value;
            IntegerValue = value;
            Numerator = null;
            Denominator = null;
        }
        
        internal RealValue(long numerator, long denominator)
        {
            DecimalValue = (double)numerator / denominator;
            IntegerValue = null;
            Numerator = numerator;
            Denominator = denominator;
        }
        #endregion

        #region Fields
        private double DecimalValue { get; }
        
        private long? IntegerValue { get; }
        
        private long? Numerator { get; }
        
        private long? Denominator { get; }
        #endregion
    }
}
