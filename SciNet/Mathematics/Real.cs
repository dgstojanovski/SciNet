using SciNet.Core;
using SciNet.Core.Attributes;

namespace SciNet.Mathematics
{
    [Definition(typeof(RealValue), "Functions for operating on real numbers")]
    public sealed class Real : IDefinition
    {
        [Factory(typeof(RealValue), "Creates a decimal real with the provided value")]
        public static RealValue Decimal(double value) => 
            new(value);
        
        [Factory(typeof(RealValue), "Creates an integer real with the provided value")]
        public static RealValue Integer(long value) => 
            new(value);

        [Factory(typeof(RealValue), "Creates a rational real with the provided numerator and denominator")]
        public static RealValue Rational(long numerator, long denominator) => 
            new(numerator, denominator);

        [Factory(typeof(RealValue), "An arbitrarily small decimal value")]
        public static RealValue Epsilon => 
            new(double.Epsilon);
        
        [Factory(typeof(RealValue), "The additive identity of the set of real numbers")]
        public static RealValue Zero => 
            new(0);
        
        [Factory(typeof(RealValue), "The multiplicative identity of the set of real numbers")]
        public static RealValue One => 
            new(1);

        [Factory(typeof(RealValue), "Gets the absolute value of the provided real number")]
        public static RealValue Absolute(RealValue value) =>
            Decimal(value.AbsoluteValue);
        
        [Factory(typeof(RealValue), "Gets the absolute value of the provided real number")]
        public static RealValue SquareRoot(RealValue value) =>
            value.SquareRoot;
        
        [Factory(typeof(RealValue), "Gets the absolute value of the provided real number")]
        public static RealValue Root(RealValue value, int root) =>
            value.Root(root);
        
        [Factory(typeof(RealValue), "Gets the square of the provided real number")]
        public static RealValue Square(RealValue value) =>
            value.Square;
        
        [Factory(typeof(RealValue), "Gets the specified power of the provided real number")]
        public static RealValue Power(RealValue value, int power) => 
            value.Power(power);
        
        [Factory(typeof(RealValue), "Gets the arc of the provided real number in radians")]
        public static RealValue ArcTangent(RealValue value) => 
            value.ArcTangent;
        
        private Real() { }
    }
}
