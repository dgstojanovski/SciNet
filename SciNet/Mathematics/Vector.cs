using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace SciNet.Mathematics
{
    [Factory(typeof(VectorValue), "One-dimensional array of real numbers")]
    public static class Vector
    {
        #region Public
        [FactoryMethod(typeof(VectorValue), "Create a new row vector with the provided values")]
        public static VectorValue Row(params object[] entries) =>
           new(VectorKind.Row, entries.Select(Convert.ToDecimal));

        [FactoryMethod(typeof(VectorValue), "Create a new row vector with a specified length and prototype")]
        public static VectorValue Row(VectorPrototype prototype, int length) =>
          GetPrototype(VectorKind.Row, prototype, length);

        [FactoryMethod(typeof(VectorValue), "Create a new column vector with the provided values")]
        public static VectorValue Column(params object[] entries) =>
            new(VectorKind.Column, entries.Select(Convert.ToDecimal));

        [FactoryMethod(typeof(VectorValue), "Create a new column vector with a specified length and prototype")]
        public static VectorValue Column(VectorPrototype prototype, int length) =>
           GetPrototype(VectorKind.Column, prototype, length);

        [FactoryMethod(typeof(VectorValue), "Creates a zero vector with the specified orientation and length")]
        public static VectorValue Zero(VectorKind kind, int length) =>
            new(kind, Enumerable.Repeat<decimal>(0, length));

        [FactoryMethod(typeof(VectorValue), "Creates a random vector with the specified orientation and length")]
        public static VectorValue Random(VectorKind kind, int length) =>
            new(kind, Enumerable.Repeat<decimal>(0, length).Select(_ => Convert.ToDecimal(_random.NextDouble())));
        #endregion

        #region Internal
        internal static VectorValue GetPrototype(VectorKind kind, VectorPrototype prototype, int length) => length <= 0
            ? throw new ArgumentException("Length must be greater than 0", nameof(length))
            : prototype switch
            {
                VectorPrototype.Zero => Zero(kind, length),
                VectorPrototype.Random => Random(kind, length),
                _ => throw new NotImplementedException($"No prototype generator exists for the vector prototype '{prototype}'")
            };

        internal static readonly Random _random = new();
        #endregion
    }

    [ValueType(typeof(VectorValue), "Immutable type representing vectors")]
    public readonly struct VectorValue
    {
        #region Public
        [ValueTypeProperty(typeof(VectorValue), "Describes whether this vector is a row or column vector")]
        public VectorKind Kind { get; }

        [ValueTypeProperty(typeof(VectorValue), "The underlying decimal array representing this vector")]
        public IReadOnlyList<decimal> Entries { get; }

        [ValueTypeProperty(typeof(VectorValue), "The number of entries in this vector")]
        public int Length { get; }

        public override string ToString() => string.Concat($"[{string.Join(", ", Entries)}]", Kind == VectorKind.Column ? "^T" : string.Empty);

        // TODO: Make a mutator attribute
        public VectorValue Transpose() => Kind == VectorKind.Row
            ? Vector.Column(Entries.ToArray())
            : Vector.Row(Entries.ToArray());
        #endregion

        #region Internal
        internal VectorValue(VectorKind kind, IEnumerable<decimal> entries)
        {
            Kind = kind;
            Entries = entries.ToArray();
            Length = Entries.Count;
        }
        #endregion
    }

    #region Metadata
    // TODO: Make a prototype attribute that captures different constraints to validate
    [Description("Represents the diffent possible kinds of vector, i.e. row and column")]
    public enum VectorKind
    {
        [Description("A row of values, as in [a_1, a_2, ... a_n]")]
        Row = 0x1,

        [Description("A column of values, as in [a_1, a_2, ... a_n]^T")]
        Column = 0x2
    }

    [Description("Speak kinds of vectors, e.g. random vectors and zero vectors")]
    public enum VectorPrototype
    {
        [Description("A vector with all zero values")]
        Zero,

        [Description("A vector with random values in the range (0, 1)")]
        Random
    }
    #endregion
}
