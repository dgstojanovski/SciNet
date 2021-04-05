using System;
using System.ComponentModel;
using System.Linq;
using SciNet.Core;
using SciNet.Core.Attributes;
using static SciNet.Mathematics.Real;

namespace SciNet.Mathematics
{
    [Definition(typeof(VectorValue), "One-dimensional array of real numbers")]
    public sealed class Vector : IDefinition
    {
        [Factory(typeof(VectorValue), "Create a new row vector with the provided integer values")]
        public static VectorValue Row(params long[] entries) =>
            new(Types.Row, entries.Select(Integer));
        
        [Factory(typeof(VectorValue), "Create a new row vector with the provided double values")]
        public static VectorValue Row(params double[] entries) =>
            new(Types.Row, entries.Select(Decimal));
        
        [Factory(typeof(VectorValue), "Create a new row vector with the provided double values")]
        public static VectorValue Row(params RealValue[] entries) =>
            new(Types.Row, entries);
        
        [Factory(typeof(VectorValue), "Create a new column vector with the provided integer values")]
        public static VectorValue Column(params long[] entries) =>
            new(Types.Column, entries.Select(Integer));
        
        [Factory(typeof(VectorValue), "Create a new column vector with the provided double values")]
        public static VectorValue Column(params double[] entries) =>
            new(Types.Column, entries.Select(Decimal));
        
        [Factory(typeof(VectorValue), "Create a new column vector with the provided values")]
        public static VectorValue Column(params RealValue[] entries) =>
            new(Types.Column, entries);
        
        [Factory(typeof(VectorValue), "Create a vector of zeroes with the specified length")]
        public static VectorValue Zero(int length, Types type = Types.Row) => length > 0
            ? new VectorValue(type, Enumerable.Repeat(Real.Zero, length))
            : throw new ArgumentException("Length must be greater than 0", nameof(length));
        
        [Factory(typeof(VectorValue), "Create a vector of random values in the range (0, 1) with the specified length")]
        public static VectorValue Random(int length, Types type = Types.Row) => length > 0
            ? new VectorValue(type, Enumerable.Range(0, length).Select(_ => Decimal(Generator.NextDouble())))
            : throw new ArgumentException("Length must be greater than 0", nameof(length));
        
        [Option(typeof(VectorValue), "Represents the different possible kinds of vector, i.e. row and column")]
        public enum Types
        {
            [Description("A row of values, as in [a_1, a_2, ... a_n]")]
            Row = 0x1,

            [Description("A column of values, as in [a_1, a_2, ... a_n]^T")]
            Column = 0x2
        }
        
        private Vector() { }

        private static readonly Random Generator = new();
    }
}
