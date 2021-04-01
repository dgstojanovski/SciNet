using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using SciNet.Mathematics.Numerical.Metadata;

namespace SciNet.Mathematics.Numerical
{
    public static class Vector
    {
        [Description("Create a new row vector with the provided values")]
        public static Vector<T> Row<T>(params T[] entries) where T : struct, IConvertible, IComparable<T>, IEquatable<T> =>
           new(VectorKind.Row, entries);

        [Description("Create a new row vector with a specified length and prototype")]
        public static Vector<decimal> Row(VectorPrototype prototype, int length) =>
          GetPrototype(VectorKind.Row, prototype, length);

        [Description("Create a new column vector with the provided values")]
        public static Vector<T> Column<T>(params T[] entries) where T : struct, IConvertible, IComparable<T>, IEquatable<T> =>
            new(VectorKind.Column, entries);

        [Description("Create a new column vector with a specified length and prototype")]
        public static Vector<decimal> Column(VectorPrototype prototype, int length) =>
           GetPrototype(VectorKind.Column, prototype, length);

        [Description("Creates a zero vector with the specified orientation and length")]
        public static Vector<decimal> Zero(VectorKind kind, int length) =>
            new(kind, Enumerable.Repeat<decimal>(0, length));

        [Description("Creates a zero vector with the specified orientation and length")]
        public static Vector<decimal> Random(VectorKind kind, int length) =>
            new(kind, Enumerable.Repeat<decimal>(0, length).Select(_ => Convert.ToDecimal(_random.NextDouble())));

        #region Internal
        internal static Vector<decimal> GetPrototype(VectorKind kind, VectorPrototype prototype, int length) => prototype switch
        {
            VectorPrototype.Zero => Zero(kind, length),
            VectorPrototype.Random => Random(kind, length),
            _ => throw new NotImplementedException($"No prototype generator exists for the vector prototype '{prototype}'")
        };

        internal static readonly Random _random = new();
        #endregion
    }

    public readonly struct Vector<T> where T : struct, IConvertible, IComparable<T>, IEquatable<T>
    {
        #region Properties
        [Description("Describes whether this vector is a row or column vector")]
        public VectorKind Kind { get; }

        [Description("The underlying decimal array representing this vector")]
        public IReadOnlyList<T> Entries { get; }

        [Description("The number of entries in this vector")]
        public ulong Length { get; }

        [Description("Canonical string representation of this vector this vector")]
        public override string ToString() => string.Concat($"[{string.Join(", ", Entries)}]", Kind == VectorKind.Column ? "^T" : string.Empty);
        #endregion

        #region Mutators
        public Vector<T> Transpose() => Kind == VectorKind.Row
            ? Vector.Column(Entries.ToArray())
            : Vector.Row(Entries.ToArray());
        #endregion

        #region Internal
        internal Vector(VectorKind kind, IEnumerable<T> entries)
        {
            Kind = kind;
            Entries = entries.ToArray();
            Length = Convert.ToUInt64(Entries.Count);
        }
        #endregion
    }

    public enum VectorPrototype
    {
        [Description("A vector with all zero values")]
        Zero,

        [Description("A vector with random values in the range (0, 1)")]
        Random
    }

}
