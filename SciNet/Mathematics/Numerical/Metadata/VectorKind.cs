using System.ComponentModel;

namespace SciNet.Mathematics.Numerical.Metadata
{
    [Description("Represents the diffent possible kinds of vector, i.e. row and column")]
    public enum VectorKind
    {
        [Description("A row of values, as in [a_1, a_2, ... a_n]")]
        Row = 0x1,

        [Description("A column of values, as in [a_1, a_2, ... a_n]^T")]
        Column = 0x2
    }
}
