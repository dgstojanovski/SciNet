using System;
using Xunit;
using Xunit.Abstractions;
using static SciNet.Mathematics.Vector;

namespace SciNet.Tests.Mathematics
{
    public class VectorUnitTests
    {
        private readonly ITestOutputHelper _output;

        public VectorUnitTests(ITestOutputHelper output)
        {
            _output = output;
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
        public void Factory_Vector_Row_Positive_Double(params double[] values)
        {
            var vector = Row(values);
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
            var vector = Row(values);
            
             _output.WriteLine(vector.ToJson(true));;
            
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
            var vector = Column(values);
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
            var vector = Column(values);
            
             _output.WriteLine(vector.ToJson(true));;
            
            Assert.Equal(values.Length, vector.Length);
            for (var i = 0; i < values.Length; i++)
            {
                Assert.True(values[i] == vector[i]);
            }
        }

        [Theory]
        [InlineData(1, Types.Row)]
        [InlineData(1, Types.Column)]
        [InlineData(2, Types.Row)]
        [InlineData(2, Types.Column)]
        [InlineData(short.MaxValue, Types.Row)]
        [InlineData(short.MaxValue, Types.Column)]
        public void Factory_Vector_Zero_Positive(int length, Types type)
        {
            var vector = Zero(length, type);
            
             _output.WriteLine(vector.ToJson(true));;
            
            Assert.Equal(length, vector.Length);
        }

        [Theory]
        [InlineData(int.MinValue, Types.Row)]
        [InlineData(int.MinValue, Types.Column)]
        [InlineData(-1, Types.Row)]
        [InlineData(-1, Types.Column)]
        [InlineData(0, Types.Row)]
        [InlineData(0, Types.Column)]
        public void Factory_Vector_Zero_Negative(int length, Types type)
        {
            Assert.Throws<ArgumentException>(() => Zero(length, type));
        }
        
        [Theory]
        [InlineData(1, Types.Row)]
        [InlineData(1, Types.Column)]
        [InlineData(2, Types.Row)]
        [InlineData(2, Types.Column)]
        [InlineData(short.MaxValue, Types.Row)]
        [InlineData(short.MaxValue, Types.Column)]
        public void Factory_Vector_Random_Positive(int length, Types type)
        {
            var vector = Random(length, type);
            
             _output.WriteLine(vector.ToJson(true));;
            
            Assert.Equal(length, vector.Length);
        }

        [Theory]
        [InlineData(int.MinValue, Types.Row)]
        [InlineData(int.MinValue, Types.Column)]
        [InlineData(-1, Types.Row)]
        [InlineData(-1, Types.Column)]
        [InlineData(0, Types.Row)]
        [InlineData(0, Types.Column)]
        public void Factory_Vector_Random_Negative(int length, Types type)
        {
            Assert.Throws<ArgumentException>(() => Random(length, type));
        }
    }
}
