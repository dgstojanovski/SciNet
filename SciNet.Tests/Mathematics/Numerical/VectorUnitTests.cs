using SciNet.Mathematics.Numerical;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SciNet.Tests.Mathematics.Numerical
{
    public class VectorUnitTests
    {
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(short.MaxValue)]
        public void Factory_Vector_Row_Prototype_Zero_Positive(int length)
        {
            var vector = Vector.Row(VectorPrototype.Zero, length);
            Assert.Equal(length, vector.Length);
            Assert.Equal(length, vector.Entries.Count);
            Assert.True(vector.Entries.All(e => e == 0));
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        [InlineData(short.MinValue)]
        public void Factory_Vector_Row_Prototype_Zero_Negative(int length)
        {
            Assert.Throws<ArgumentException>(() => Vector.Row(VectorPrototype.Zero, length));
        }
    }
}
