using System;
using System.Globalization;

namespace SciNet.Mathematics
{
    [Factory(typeof(RealValue), "Functions for operating on real numbers")]
    public static class Real
    {
        public static RealValue Decimal(double value) => new(value);
        
        public static RealValue Integer(long value) => new(value);

        public static RealValue Rational(long numerator, long denominator) => new(numerator, denominator);

        public static RealValue Epsilon => new(double.Epsilon);
        
        public static RealValue Zero => new(0);
        
        public static RealValue Root(RealValue value, int root) => Decimal(Math.Pow(Convert.ToDouble(value.Value), 1.0 / root));
        
        public static RealValue Power(RealValue value, int power) => value.Power(power);
        
        public static RealValue ArcTangent(RealValue value) => value.ArcTangent;
    }

    [ValueType(typeof(RealValue), "Any value in the set of real numbers")]
    public readonly struct RealValue
    {
        public double Value => IntegerValue ?? DecimalValue;

        public bool IsInteger => IntegerValue != null;
        
        public bool IsRational => IsInteger || Numerator != null && Denominator != null;

        public RealValue Root(int power) => Real.Decimal(Math.Pow(DecimalValue, 1.0 / power));
        
        public RealValue Power(double power) => Real.Decimal(Math.Pow(DecimalValue, power));
        
        public RealValue ArcTangent => Real.Decimal(Math.Atan(DecimalValue));

        public static bool operator ==(RealValue first, RealValue second) => first.IsInteger && second.IsInteger
            ? first.IntegerValue == second.IntegerValue
            : first.IsRational && second.IsRational
                ? first.Numerator == second.Numerator && first.Denominator == second.Denominator
                : Math.Abs(first.Value - second.Value) <= Real.Epsilon.Value;
        
        public static bool operator !=(RealValue first, RealValue second) => first.IsInteger && second.IsInteger
            ? first.IntegerValue != second.IntegerValue
            : first.IsRational && second.IsRational
                ? first.Numerator != second.Numerator || first.Denominator != second.Denominator
                : Math.Abs(first.Value - second.Value) <= Real.Epsilon.Value;

        public static bool operator ==(RealValue first, long second) => first.IsInteger
            ? first.IntegerValue == second
            : Math.Abs(first.Value - Convert.ToDouble(second)) <= Real.Epsilon.Value;
        
        public static bool operator !=(RealValue first, long second) => first.IsInteger
            ? first.IntegerValue != second
            : Math.Abs(first.Value - Convert.ToDouble(second)) <= Real.Epsilon.Value;
        
        public static bool operator ==(long first, RealValue second) => second.Denominator == 1
            ? first == second.Numerator
            : Math.Abs(first - second.Value) <= Real.Epsilon.Value;
        
        public static bool operator !=(long first, RealValue second) =>
            Math.Abs(first - second.Value) > Real.Epsilon.Value;
        
        public static bool operator ==(RealValue first, double second) => 
            Math.Abs(first.Value - second) <= Real.Epsilon.Value;
        
        public static bool operator !=(RealValue first, double second) =>
            Math.Abs(first.Value - second) > Real.Epsilon.Value;
        
        public static bool operator ==(double first, RealValue second) => 
            Math.Abs(first - second.Value) <= Real.Epsilon.Value;
        
        public static bool operator !=(double first, RealValue second) =>
            Math.Abs(first - second.Value) > Real.Epsilon.Value;
        
        public static bool operator ==(RealValue first, decimal second) => 
            Math.Abs(first.Value - Convert.ToDouble(second)) <= Real.Epsilon.Value;
        
        public static bool operator !=(RealValue first, decimal second) =>
            Math.Abs(first.Value - Convert.ToDouble(second)) > Real.Epsilon.Value;
        
        public static bool operator ==(decimal first, RealValue second) => 
            Math.Abs(Convert.ToDouble(first) - second.Value) <= Real.Epsilon.Value;
        
        public static bool operator !=(decimal first, RealValue second) =>
            Math.Abs(Convert.ToDouble(first) - second.Value) > Real.Epsilon.Value;
        
        public static bool operator ==(RealValue first, object second) => 
            Math.Abs(first.Value - Convert.ToDouble(second)) <= Real.Epsilon.Value;
        
        public static bool operator !=(RealValue first, object second) =>
            Math.Abs(first.Value - Convert.ToDouble(second)) > Real.Epsilon.Value;

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

        public override bool Equals(object other) => other != null && this == other;

        public override int GetHashCode() => base.GetHashCode();

        public override string ToString() => IsInteger 
            ? $"{IntegerValue}" 
            : IsRational ? $"{Numerator}/{Denominator}" : $"{DecimalValue}";
       
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
        
        private double DecimalValue { get; }
        
        private long? IntegerValue { get; }
        
        private long? Numerator { get; }
        
        private long? Denominator { get; }
    }
}
