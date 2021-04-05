using System;
using System.Linq;
using Xunit;
using SciNet.Mathematics;

namespace SciNet.Tests.Mathematics
{
    public class VectorUnitTests
    {
        [Theory]
        [InlineData(-2.0, -1.0)]
        [InlineData(-1.0, -2.0)]
        [InlineData(-2.0)]
        [InlineData(-1.0)]
        [InlineData(0.0)]
        [InlineData(1.0)]
        [InlineData(2.0)]
        [InlineData(1.0, 2.0)]
        [InlineData(2.0, 2.0)]
        public void Factory_Vector_Row_Positive_Double(params double[] values)
        {
            var vector = Vector.Row(values);
            Assert.Equal(values.Length, vector.Length);
            Assert.Equal(values.Length, vector.Entries.Count);
            for (var i = 0; i < values.Length; i++)
            {
                Assert.True(values[i] == vector[i]);
            }
        }
        
        [Theory]
        [InlineData(-2.0, -1.0)]
        [InlineData(-1.0, -2.0)]
        [InlineData(-2.0)]
        [InlineData(-1.0)]
        [InlineData(0.0)]
        [InlineData(1.0)]
        [InlineData(2.0)]
        [InlineData(1.0, 2.0)]
        [InlineData(2.0, 2.0)]
        public void Factory_Vector_Row_Positive_Decimal(params double[] values)
        {
            var vector = Vector.Row(values.Select(Convert.ToDouble).ToArray());
            Assert.Equal(values.Length, vector.Length);
            Assert.Equal(values.Length, vector.Entries.Count);
            for (var i = 0; i < values.Length; i++)
            {
                Assert.True(values[i] == vector[i]);
            }
        }
        
        [Theory]
        [InlineData(-2L, -1L)]
        [InlineData(-1L, -2L)]
        [InlineData(-2L)]
        [InlineData(-1L)]
        [InlineData(0L)]
        [InlineData(1L)]
        [InlineData(2L)]
        [InlineData(1L, 2L)]
        [InlineData(2L, 2L)]
        public void Factory_Vector_Row_Positive_Integer(params long[] values)
        {
            var vector = Vector.Row(values);
            Assert.Equal(values.Length, vector.Length);
            Assert.Equal(values.Length, vector.Entries.Count);
            for (var i = 0; i < values.Length; i++)
            {
                Assert.True(values[i] == vector[i]);
            }
        }

        [Theory]
        [InlineData(VectorPrototype.Zero, 1)]
        [InlineData(VectorPrototype.Zero, 2)]
        [InlineData(VectorPrototype.Zero, short.MaxValue)]
        public void Factory_Vector_Row_Prototype_Zero_Positive(VectorPrototype prototype, int length)
        {
            var vector = Vector.Row(prototype, length);
            Assert.Equal(length, vector.Length);
            Assert.Equal(length, vector.Entries.Count);
            Assert.True(vector.Entries.All(e => e == Real.Zero));
        }

        [Theory]
        [InlineData(VectorPrototype.Zero, -1)]
        [InlineData(VectorPrototype.Zero, 0)]
        [InlineData(VectorPrototype.Zero, short.MinValue)]
        public void Factory_Vector_Row_Prototype_Zero_Negative(VectorPrototype prototype, int length)
        {
            Assert.Throws<ArgumentException>(() => Vector.Row(prototype, length));
        }
        
        [Theory]
        [InlineData(VectorPrototype.Random, 1)]
        [InlineData(VectorPrototype.Random, 2)]
        [InlineData(VectorPrototype.Random, short.MaxValue)]
        public void Factory_Vector_Row_Prototype_Random_Positive(VectorPrototype prototype, int length)
        {
            var vector = Vector.Row(prototype, length);
            Assert.Equal(length, vector.Length);
            Assert.Equal(length, vector.Entries.Count);
            Assert.True(vector.Entries.All(e => e.Value > 0.0 && e.Value < 1.0));
        }
        
        [Theory]
        [InlineData(VectorPrototype.Random, -1)]
        [InlineData(VectorPrototype.Random, 0)]
        [InlineData(VectorPrototype.Random, short.MinValue)]
        public void Factory_Vector_Row_Prototype_Random_Negative(VectorPrototype prototype, int length)
        {
            Assert.Throws<ArgumentException>(() => Vector.Row(prototype, length));
        }
    }
}
