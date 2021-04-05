using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Text.Json;
using SciNet.Core;
using SciNet.Core.Attributes;

namespace SciNet.Mathematics
{
    [Value(typeof(VectorValue), "Immutable type representing vectors")]
    public readonly struct VectorValue : IValue
    {
        #region Properties

        [Property(typeof(VectorValue), "Describes whether this vector is a row or column vector")]
        public Vector.Types Type { get; }

        [Property(typeof(VectorValue), "The underlying decimal array representing this vector")]
        private IReadOnlyList<RealValue> Entries { get; }

        [Property(typeof(VectorValue), "The number of entries in this vector")]
        public int Length { get; }

        [Property(typeof(VectorValue), "The transpose of this vector, [a_1, a_2, ..., a_n]^T")]
        public VectorValue Transpose => Type == Vector.Types.Row
            ? Vector.Column(Entries.ToArray())
            : Vector.Row(Entries.ToArray());

        #endregion

        #region Constructors

        internal VectorValue(Vector.Types type, IEnumerable<RealValue> entries)
        {
            Type = type;
            Entries = entries.ToArray();
            Length = Entries.Count;
        }

        #endregion

        #region Implementations

        public string ToJson(bool pretty = false)
        {
            return JsonSerializer.Serialize(new
            {
                Length,
                Type = Type.ToString(),
                Entries = Entries.Select(e => e.ToInline()).ToArray()
            }, new JsonSerializerOptions
            {
                WriteIndented = pretty,
                Encoder = pretty ? JavaScriptEncoder.UnsafeRelaxedJsonEscaping : JavaScriptEncoder.Default
            });
        }

        public string ToInline()
        {
            return string.Concat($"[{string.Join(", ", Entries)}]", Type == Vector.Types.Column ? "^T" : string.Empty);
        }

        #endregion Implementations

        #region Overrides

        public override string ToString()
        {
            return ToInline();
        }

        #endregion Overrides

        #region Operators

        public RealValue this[int i] => Entries[i];

        #endregion Operators
    }
}