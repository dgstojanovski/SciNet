using System;
using System.ComponentModel;
using System.Linq;
using MetaNet.Core;
using MetaNet.Core.Attributes;
using static SciNet.Mathematics.Real;
using static SciNet.Mathematics.Complex;

namespace SciNet.Mathematics
{
    [Definition(typeof(VectorValue), "One-dimensional array of real numbers")]
    public sealed class Vector : IDefinition
    {
        [Option(typeof(VectorValue), "Represents the different possible kinds of vector, i.e. row and column")]
        public enum Types
        {
            [Description("A row of values, as in [a_1, a_2, ... a_n]")]
            Row = 0x1,

            [Description("A column of values, as in [a_1, a_2, ... a_n]^T")]
            Column = 0x2
        }

        private static readonly Random Generator = new();

        private Vector() { }

        [Factory(typeof(VectorValue), "Create a new row vector with the provided integer values")]
        public static VectorValue Row(params long[] entries)
        {
            return new(Types.Row, entries.Select(Integer).Select(ToComplex).ToArray());
        }
        
        [Factory(typeof(VectorValue), "Create a new row vector with the provided integer values")]
        public static VectorValue Row(params (long Real, long Imaginary)[] entries)
        {
            return new(Types.Row, entries.Select(Value).ToArray());
        }

        [Factory(typeof(VectorValue), "Create a new row vector with the provided double values")]
        public static VectorValue Row(params double[] entries)
        {
            return new(Types.Row, entries.Select(Decimal).Select(ToComplex).ToArray());
        }
        
        [Factory(typeof(VectorValue), "Create a new row vector with the provided complex decimal values")]
        public static VectorValue Row(params (double Real, double Imaginary)[] entries)
        {
            return new(Types.Row, entries.Select(Value).ToArray());
        }

        [Factory(typeof(VectorValue), "Create a new row vector with the provided double values")]
        public static VectorValue Row(params RealValue[] entries)
        {
            return new(Types.Row, entries.Select(ToComplex).ToArray());
        }
        
        [Factory(typeof(VectorValue), "Create a new row vector with the provided double values")]
        public static VectorValue Row(params ComplexValue[] entries)
        {
            return new(Types.Row, entries);
        }

        [Factory(typeof(VectorValue), "Create a new column vector with the provided integer values")]
        public static VectorValue Column(params long[] entries)
        {
            return new(Types.Column, entries.Select(Integer).Select(ToComplex).ToArray());
        }

        [Factory(typeof(VectorValue), "Create a new column vector with the provided double values")]
        public static VectorValue Column(params double[] entries)
        {
            return new(Types.Column, entries.Select(Decimal).Select(ToComplex).ToArray());
        }

        [Factory(typeof(VectorValue), "Create a new column vector with the provided values")]
        public static VectorValue Column(params RealValue[] entries)
        {
            return new(Types.Column, entries.Select(ToComplex).ToArray());
        }
        
        [Factory(typeof(VectorValue), "Create a new column vector with the provided values")]
        public static VectorValue Column(params ComplexValue[] entries)
        {
            return new(Types.Column, entries);
        }

        [Factory(typeof(VectorValue), "Create a vector of zeroes with the specified length")]
        public static VectorValue Zero(int length, Types type = Types.Row)
        {
            return length > 0
                ? new VectorValue(type, Enumerable.Repeat(Real.Zero, length).Select(ToComplex).ToArray())
                : throw new ArgumentException("Length must be greater than 0", nameof(length));
        }

        [Factory(typeof(VectorValue), "Create a vector of random values in the range (0, 1) with the specified length")]
        public static VectorValue Random(int length, Types type = Types.Row)
        {
            return length > 0
                ? new VectorValue(type, Enumerable.Range(0, length)
                    .Select(_ => Decimal(Generator.NextDouble()))
                    .Select(ToComplex)
                    .ToArray())
                : throw new ArgumentException("Length must be greater than 0", nameof(length));
        }
    }
}