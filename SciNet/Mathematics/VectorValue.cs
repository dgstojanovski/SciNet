using System.Collections.Generic;
using System.Linq;
using SciNet.Core;

namespace SciNet.Mathematics
{
    [ValueType(typeof(VectorValue), "Immutable type representing vectors")]
    public readonly struct VectorValue
    {
        #region Properties
        [ValueProperty(typeof(VectorValue), "Describes whether this vector is a row or column vector")]
        public Vector.Types Type { get; }

        [ValueProperty(typeof(VectorValue), "The underlying decimal array representing this vector")]
        private IReadOnlyList<RealValue> Entries { get; }

        [ValueProperty(typeof(VectorValue), "The number of entries in this vector")]
        public int Length { get; }

        [ValueProperty(typeof(VectorValue), "The transpose of this vector, [a_1, a_2, ..., a_n]^T")]
        public VectorValue Transpose => Type == Vector.Types.Row
            ? Vector.Column(Entries.ToArray())
            : Vector.Row(Entries.ToArray());
        #endregion

        #region Operators
        public RealValue this[int i] => Entries[i];
        #endregion

        #region Overrides
        public override string ToString() => 
            string.Concat($"[{string.Join(", ", Entries)}]", Type == Vector.Types.Column ? "^T" : string.Empty);
        #endregion

        #region Constructors
        internal VectorValue(Vector.Types type, IEnumerable<RealValue> entries)
        {
            Type = type;
            Entries = entries.ToArray();
            Length = Entries.Count;
        }
        #endregion
    }
}