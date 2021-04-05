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
        [FactoryMethod(typeof(VectorValue), "Create a new row vector with the provided integer values")]
        public static VectorValue Row(params long[] entries) =>
            new(VectorKind.Row, entries.Select(Real.Integer));
        
        [FactoryMethod(typeof(VectorValue), "Create a new row vector with the provided double values")]
        public static VectorValue Row(params double[] entries) =>
            new(VectorKind.Row, entries.Select(Real.Decimal));
        
        [FactoryMethod(typeof(VectorValue), "Create a new row vector with the provided double values")]
        public static VectorValue Row(params RealValue[] entries) =>
            new(VectorKind.Row, entries);
        
        [FactoryMethod(typeof(VectorValue), "Create a new row vector with a specified length and prototype")]
        public static VectorValue Row(VectorPrototype prototype, int length) =>
          GetPrototype(VectorKind.Row, prototype, length);

        [FactoryMethod(typeof(VectorValue), "Create a new column vector with the provided integer values")]
        public static VectorValue Column(params long[] entries) =>
            new(VectorKind.Column, entries.Select(Real.Integer));
        
        [FactoryMethod(typeof(VectorValue), "Create a new column vector with the provided double values")]
        public static VectorValue Column(params double[] entries) =>
            new(VectorKind.Column, entries.Select(Real.Decimal));
        
        [FactoryMethod(typeof(VectorValue), "Create a new column vector with the provided values")]
        public static VectorValue Column(params RealValue[] entries) =>
            new(VectorKind.Column, entries);

        [FactoryMethod(typeof(VectorValue), "Create a new column vector with a specified length and prototype")]
        public static VectorValue Column(VectorPrototype prototype, int length) =>
           GetPrototype(VectorKind.Column, prototype, length);
        #endregion
        
        #region Private
        private static VectorValue GetPrototype(VectorKind kind, VectorPrototype prototype, int length) => length <= 0
            ? throw new ArgumentException("Length must be greater than 0", nameof(length))
            : prototype switch
            {
                VectorPrototype.Zero => 
                    new VectorValue(kind, Enumerable.Repeat(Real.Zero, length)),
                VectorPrototype.Random => 
                    new VectorValue(kind, Enumerable.Repeat(Real.Zero, length).Select(_ => Real.Decimal(_random.NextDouble()))),
                _ => throw new NotImplementedException($"No prototype generator exists for the vector prototype '{prototype}'")
            };

        private static readonly Random _random = new();
        #endregion
    }

    [ValueType(typeof(VectorValue), "Immutable type representing vectors")]
    public readonly struct VectorValue
    {
        #region Public
        [ValueTypeProperty(typeof(VectorValue), "Describes whether this vector is a row or column vector")]
        public VectorKind Kind { get; }

        [ValueTypeProperty(typeof(VectorValue), "The underlying decimal array representing this vector")]
        public IReadOnlyList<RealValue> Entries { get; }

        [ValueTypeProperty(typeof(VectorValue), "The number of entries in this vector")]
        public int Length { get; }

        public RealValue this[int i] => Entries[i];

        public override string ToString() => string.Concat($"[{string.Join(", ", Entries)}]", Kind == VectorKind.Column ? "^T" : string.Empty);

        // TODO: Make a mutator attribute
        public VectorValue Transpose() => Kind == VectorKind.Row
            ? Vector.Column(Entries.ToArray())
            : Vector.Row(Entries.ToArray());
        #endregion

        #region Internal
        internal VectorValue(VectorKind kind, IEnumerable<RealValue> entries)
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
