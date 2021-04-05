﻿using System;
using SciNet.Core;

namespace SciNet.Mathematics
{
    [ValueType(typeof(RealValue), "Any value in the set of real numbers")]
    public readonly struct RealValue
    {
        #region Properties
        [ValueProperty(typeof(RealValue), "The signed value of this real number")]
        public double Value => IntegerValue ?? DecimalValue;
        
        [ValueProperty(typeof(RealValue), "The absolute value of this real number")]
        public double AbsoluteValue => Math.Abs(IntegerValue ?? DecimalValue);

        [ValueProperty(typeof(RealValue), "The absolute value of this real number")]
        public long IntegralPart => IntegerValue ?? Convert.ToInt64(Math.Round(DecimalValue));
        
        [ValueProperty(typeof(RealValue), "The absolute value of this real number")]
        public long FractionalPart => IntegerValue ?? Convert.ToInt64(DecimalValue - Math.Round(DecimalValue));

        [ValueProperty(typeof(RealValue), "Returns true if this real number is an integer")]
        public bool IsInteger => IntegerValue != null;
        
        [ValueProperty(typeof(RealValue), "Returns true if this real number is a rational number")]
        public bool IsRational => IsInteger || Numerator != null && Denominator != null;
        
        [ValueProperty(typeof(RealValue), "The square root of this real number")]
        public RealValue SquareRoot => Real.Decimal(Math.Pow(DecimalValue, 2));

        [ValueProperty(typeof(RealValue), "Returns the specified root of this real number")]
        public RealValue Root(int power) => Real.Decimal(Math.Pow(DecimalValue, 1.0 / power));
        
        [ValueProperty(typeof(RealValue), "Returns the square of this real number")]
        public RealValue Square => Real.Decimal(Math.Pow(DecimalValue, 2));
        
        [ValueProperty(typeof(RealValue), "Returns the specified power of this real number")]
        public RealValue Power(double power) => Real.Decimal(Math.Pow(DecimalValue, power));
        
        [ValueProperty(typeof(RealValue), "Returns the arctangent of this real value in radians")]
        public RealValue ArcTangent => Real.Decimal(Math.Atan(DecimalValue));
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