using System;
using System.Linq;
using System.Collections.Generic;
using Xunit;
using SciNet.Mathematics;

namespace SciNet.Tests.Mathematics
{
    public class VectorUnitTests
    {
        [Theory]
        [InlineData(1)]
        [InlineData(1.0)]
        [InlineData(1, 2)]
        [InlineData(1.0, 2)]
        [InlineData(2, 1.0)]
        [InlineData(1.0, 2.0)]
        [InlineData(short.MaxValue)]
        public void Factory_Vector_Row_Positive(params object[] values)
        {
            var vector = Vector.Row(values);
            Assert.Equal(values.Length, vector.Length);
            Assert.Equal(values.Length, vector.Entries.Count);
            Assert.Collection(vector.Entries, e => Assert.Equal(Array.IndexOf(values, e), Array.IndexOf(vector.Entries.ToArray(), e)));
        }

        [Theory]
        [InlineData(VectorPrototype.Zero, 1)]
        [InlineData(VectorPrototype.Zero, 2)]
        [InlineData(VectorPrototype.Zero, short.MaxValue)]
        public void Factory_Vector_Row_Prototype_Positive(VectorPrototype prototype, int length)
        {
            var vector = Vector.Row(prototype, length);
            Assert.Equal(length, vector.Length);
            Assert.Equal(length, vector.Entries.Count);
            Assert.True(vector.Entries.All(e => e == 0));
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        [InlineData(short.MinValue)]
        public void Factory_Vector_Row_Prototype_Negative(int length)
        {
            Assert.Throws<ArgumentException>(() => Vector.Row(VectorPrototype.Zero, length));
        }
    }
}
