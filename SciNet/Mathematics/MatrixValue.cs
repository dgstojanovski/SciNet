using System;
using System.Linq;
using System.Text.Encodings.Web;
using System.Text.Json;
using MetaNet.Core;
using MetaNet.Core.Attributes;

namespace SciNet.Mathematics
{
    [Value(typeof(MatrixValue), "Immutable matrix value")]
    public readonly struct MatrixValue : IValue
    {
        #region Properties
        [Property(typeof(MatrixValue), "Number of columns in this matrix")]
        public int Width { get; }
        
        [Property(typeof(MatrixValue), "Number of rows in this matrix")]
        public int Height { get; }
        
        private VectorValue[] Rows { get; }
        #endregion Properties

        #region Constructors

        internal MatrixValue(params VectorValue[] rows)
        {
            var lengths = rows.Select(r => r.Length).Distinct().ToArray();
            if (lengths.Length > 1)
            {
                throw new ArgumentException("All rows must have the same length", nameof(rows));
            }

            Width = lengths.Single();
            Height = rows.Length;
            Rows = rows;
        }

        #endregion Constructors

        #region Implementations
        public string ToJson(bool pretty = false)
        {
            return JsonSerializer
                .Serialize(new
                {
                    Width,
                    Height,
                    Rows = Rows.Select(r => r.ToInline()).ToArray()
                }, new JsonSerializerOptions
                {
                    WriteIndented = pretty,
                    Encoder = pretty ? JavaScriptEncoder.UnsafeRelaxedJsonEscaping : JavaScriptEncoder.Default
                });
        }

        public string ToInline()
        {
            return $"[{string.Join(", ", Rows.Select(r => r.ToInline()))}]";
        }
        #endregion Implementations

        #region Operators

        public ComplexValue this[int row, int column] => Rows[row][column];

        #endregion Operators
    }
}