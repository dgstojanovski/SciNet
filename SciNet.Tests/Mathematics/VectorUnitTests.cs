using System;
using Xunit;
using SciNet.Mathematics;
using Xunit.Abstractions;
using static SciNet.Mathematics.Real;
using static SciNet.Mathematics.Complex;

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
            
            DumpVectorValue(vector);
            
            Assert.Equal(values.Length, vector.Length);
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
        public void Factory_Vector_Column_Positive_Double(params double[] values)
        {
            var vector = Vector.Column(values);
            Assert.Equal(values.Length, vector.Length);
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
        public void Factory_Vector_Column_Positive_Integer(params long[] values)
        {
            var vector = Vector.Column(values);
            
            DumpVectorValue(vector);
            
            Assert.Equal(values.Length, vector.Length);
            for (var i = 0; i < values.Length; i++)
            {
                Assert.True(values[i] == vector[i]);
            }
        }

        [Theory]
        [InlineData(1, Vector.Types.Row)]
        [InlineData(1, Vector.Types.Column)]
        [InlineData(2, Vector.Types.Row)]
        [InlineData(2, Vector.Types.Column)]
        [InlineData(short.MaxValue, Vector.Types.Row)]
        [InlineData(short.MaxValue, Vector.Types.Column)]
        public void Factory_Vector_Zero_Positive(int length, Vector.Types type)
        {
            var vector = Vector.Zero(length, type);
            
            DumpVectorValue(vector);
            
            Assert.Equal(length, vector.Length);
        }

        [Theory]
        [InlineData(int.MinValue, Vector.Types.Row)]
        [InlineData(int.MinValue, Vector.Types.Column)]
        [InlineData(-1, Vector.Types.Row)]
        [InlineData(-1, Vector.Types.Column)]
        [InlineData(0, Vector.Types.Row)]
        [InlineData(0, Vector.Types.Column)]
        public void Factory_Vector_Zero_Negative(int length, Vector.Types type)
        {
            Assert.Throws<ArgumentException>(() => Vector.Zero(length, type));
        }
        
        [Theory]
        [InlineData(1, Vector.Types.Row)]
        [InlineData(1, Vector.Types.Column)]
        [InlineData(2, Vector.Types.Row)]
        [InlineData(2, Vector.Types.Column)]
        [InlineData(short.MaxValue, Vector.Types.Row)]
        [InlineData(short.MaxValue, Vector.Types.Column)]
        public void Factory_Vector_Random_Positive(int length, Vector.Types type)
        {
            var vector = Vector.Random(length, type);
            
            DumpVectorValue(vector);
            
            Assert.Equal(length, vector.Length);
        }

        [Theory]
        [InlineData(int.MinValue, Vector.Types.Row)]
        [InlineData(int.MinValue, Vector.Types.Column)]
        [InlineData(-1, Vector.Types.Row)]
        [InlineData(-1, Vector.Types.Column)]
        [InlineData(0, Vector.Types.Row)]
        [InlineData(0, Vector.Types.Column)]
        public void Factory_Vector_Random_Negative(int length, Vector.Types type)
        {
            Assert.Throws<ArgumentException>(() => Vector.Random(length, type));
        }
        
        private readonly ITestOutputHelper _output;

        public VectorUnitTests(ITestOutputHelper output)
        {
            _output = output;
        }
        
        private void DumpVectorValue(VectorValue value)
        {
            _output.WriteLine($"Value: {value}");
            _output.WriteLine($"Length: {value.Length}");
            _output.WriteLine($"Transpose: {value.Transpose}");
            _output.WriteLine($"Type: {value.Type}");
        }
    }
}
