using System;
using System.Linq;
using SciNet.Core;
using SciNet.Core.Attributes;

namespace SciNet.Mathematics
{
    [Definition(typeof(MatrixValue), "Rectangular matrix of complex values")]
    public class Matrix : IDefinition
    {
        [Factory(typeof(MatrixValue), "Create a matrix of zeroes of the specified width and height")]
        public static MatrixValue Zero(int width, int height)
        {
            if (width <= 0)
            {
                throw new ArgumentException("Width must be greater than zero", nameof(width));
            }
            
            if (height <= 0)
            {
                throw new ArgumentException("Height must be greater than zero", nameof(height));
            }

            return new MatrixValue(Enumerable.Repeat(Vector.Zero(width), height).ToArray());
        } 
    }
}