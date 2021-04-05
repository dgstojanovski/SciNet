using SciNet.Mathematics;
using Xunit;
using Xunit.Abstractions;
using static SciNet.Mathematics.Real;
using static SciNet.Mathematics.Complex;
using static SciNet.Mathematics.Matrix;

namespace SciNet.Tests.Mathematics
{
    public class MatrixUnitTests
    {
        private readonly ITestOutputHelper _output;

        public MatrixUnitTests(ITestOutputHelper output)
        {
            _output = output;
        }

        [Theory]
        [InlineData(1, 1)]
        [InlineData(1, 2)]
        [InlineData(2, 1)]
        [InlineData(2, 2)]
        public void Factory_ZeroMatrix_Positive(int width, int height)
        {
            var matrix = Zero(width, height);

            _output.WriteLine(matrix.ToInline());
            _output.WriteLine(matrix.ToJson(true));

            Assert.True(matrix.Width == width);
            Assert.True(matrix.Height == height);

            for (var row = 0; row < matrix.Height; row++)
            {
                for (var col = 0; col < matrix.Width; col++)
                {
                    if (matrix[row, col] != Real.Zero)
                        Assert.True(matrix[row, col] == Real.Zero);
                }
            }
        }
    }
}
