using System;
using System.Globalization;

namespace SciNet.Mathematics
{
    [Factory(typeof(RealValue), "Functions for operating on real numbers")]
    public static class Real
    {
        public static RealValue Decimal(decimal value) => new(value);
        public static RealValue Decimal(double value) => new(value);
        
        public static RealValue Integer(long value) => new(value);

        public static RealValue Rational(long numerator, long denominator) => new(numerator, denominator);

        public static RealValue Epsilon => new((decimal) double.Epsilon);
        
        public static RealValue Zero => new(0);
    }

    [ValueType(typeof(RealValue), "Any value in the set of real numbers")]
    public readonly struct RealValue
    {
        public decimal Value => Denominator == 1 
            ? Numerator 
            : Denominator != 0 
                ? (decimal)Numerator / Denominator 
                : DecimalValue;

        public bool IsRational => Denominator != 0;

        public static bool operator ==(RealValue first, RealValue second) => first.IsRational && second.IsRational
            ? first.Numerator == second.Numerator && first.Denominator == second.Denominator
            : Math.Abs(first.Value - second.Value) <= Real.Epsilon.Value;
        
        public static bool operator !=(RealValue first, RealValue second) => first.IsRational && second.IsRational
            ? first.Numerator != second.Numerator || first.Denominator != second.Denominator
            : Math.Abs(first.Value - second.Value) > Real.Epsilon.Value;
        
        public static bool operator ==(RealValue first, long second) => first.Denominator == 1
            ? first.Numerator == second
            : Math.Abs(first.Value - Convert.ToDecimal(second)) <= Real.Epsilon.Value;
        
        public static bool operator !=(RealValue first, long second) =>
            Math.Abs(first.Value - Convert.ToDecimal(second)) > Real.Epsilon.Value;
        
        public static bool operator ==(long first, RealValue second) => second.Denominator == 1
            ? first == second.Numerator
            : Math.Abs(first - second.Value) <= Real.Epsilon.Value;
        
        public static bool operator !=(long first, RealValue second) =>
            Math.Abs(first - second.Value) > Real.Epsilon.Value;
        
        public static bool operator ==(RealValue first, double second) => 
            Math.Abs(first.Value - Convert.ToDecimal(second)) <= Real.Epsilon.Value;
        
        public static bool operator !=(RealValue first, double second) =>
            Math.Abs(first.Value - Convert.ToDecimal(second)) > Real.Epsilon.Value;
        
        public static bool operator ==(double first, RealValue second) => 
            Math.Abs(Convert.ToDecimal(first) - second.Value) <= Real.Epsilon.Value;
        
        public static bool operator !=(double first, RealValue second) =>
            Math.Abs(Convert.ToDecimal(first) - second.Value) > Real.Epsilon.Value;
        
        public static bool operator ==(RealValue first, decimal second) => 
            Math.Abs(first.Value - second) <= Real.Epsilon.Value;
        
        public static bool operator !=(RealValue first, decimal second) =>
            Math.Abs(first.Value - second) > Real.Epsilon.Value;
        
        public static bool operator ==(decimal first, RealValue second) => 
            Math.Abs(first - second.Value) <= Real.Epsilon.Value;
        
        public static bool operator !=(decimal first, RealValue second) =>
            Math.Abs(first - second.Value) > Real.Epsilon.Value;
        
        public static bool operator ==(RealValue first, object second) => 
            Math.Abs(first.Value - Convert.ToDecimal(second)) <= Real.Epsilon.Value;
        
        public static bool operator !=(RealValue first, object second) =>
            Math.Abs(first.Value - Convert.ToDecimal(second)) > Real.Epsilon.Value;

        public override bool Equals(object other) => other != null && this == other;

        public override int GetHashCode() => base.GetHashCode();

        public override string ToString() => IsRational 
            ? $"{Numerator}/{Denominator}" 
            : DecimalValue.ToString(CultureInfo.CurrentCulture);
        
        internal RealValue(decimal value)
        {
            DecimalValue = value;
            Numerator = 0;
            Denominator = 0;
        }
        
        internal RealValue(double value)
        {
            DecimalValue = Convert.ToDecimal(value);
            Numerator = 0;
            Denominator = 0;
        }
        
        internal RealValue(long value)
        {
            DecimalValue = value;
            Numerator = value;
            Denominator = 1;
        }
        
        internal RealValue(long numerator, long denominator)
        {
            DecimalValue = (decimal)numerator / denominator;
            Numerator = numerator;
            Denominator = denominator;
        }
        
        private decimal DecimalValue { get; }
        
        private long Numerator { get; }
        
        private long Denominator { get; }
    }
}
