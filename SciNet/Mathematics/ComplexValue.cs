using System.Text.Encodings.Web;
using System.Text.Json;
using SciNet.Core;
using SciNet.Core.Attributes;
using static SciNet.Mathematics.Real;

namespace SciNet.Mathematics
{
    [Value(typeof(ComplexValue), "Complex value with both real and imaginary parts")]
    public readonly struct ComplexValue : IValue
    {
        #region Properties

        [Property(typeof(ComplexValue), "The real part of this complex value")]
        public RealValue RealPart { get; }

        [Property(typeof(ComplexValue), "The imaginary part of this complex value")]
        public RealValue ImaginaryPart { get; }

        [Property(typeof(ComplexValue), "Returns true if this complex value is also a real value")]
        public bool IsReal => ImaginaryPart == Zero;

        [Property(typeof(ComplexValue), "The magnitude this complex value represented as a cartesian vector")]
        public RealValue Magnitude => IsReal
            ? RealPart
            : SquareRoot(RealPart.Square + ImaginaryPart.Square);

        [Property(typeof(ComplexValue), "The angle this complex value represented as a cartesian vector")]
        public RealValue Argument => IsReal
            ? Zero
            : ArcTangent(ImaginaryPart / RealPart);

        #endregion

        #region Constructors

        internal ComplexValue(RealValue realPart, RealValue imaginaryPart)
        {
            RealPart = realPart;
            ImaginaryPart = imaginaryPart;
        }

        #endregion Constructors

        #region Implementations

        public string ToJson(bool pretty = false)
        {
            return JsonSerializer
                .Serialize(new
                {
                    Magnitude = Magnitude.ToInline(),
                    Argument = Argument.ToString(),
                    RealPart = RealPart.ToInline(),
                    ImaginaryPart = RealPart.ToInline()
                }, new JsonSerializerOptions
                {
                    WriteIndented = pretty,
                    Encoder = pretty ? JavaScriptEncoder.UnsafeRelaxedJsonEscaping : JavaScriptEncoder.Default
                });
        }

        public string ToInline()
        {
            return IsReal
                ? RealPart.ToString()
                : ImaginaryPart.Value > 0
                    ? $"({RealPart} + {(ImaginaryPart.IsInteger && (long) ImaginaryPart.AbsoluteValue == 1 ? string.Empty : ImaginaryPart.AbsoluteValue)}i)"
                    : $"({RealPart} - {(ImaginaryPart.IsInteger && (long) ImaginaryPart.AbsoluteValue == 1 ? string.Empty : ImaginaryPart.AbsoluteValue)}i)";
        }

        #endregion Implementations

        #region Overrides

        public override string ToString()
        {
            return ToInline();
        }

        #endregion Overrides
    }
}