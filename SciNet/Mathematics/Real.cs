using SciNet.Core;

namespace SciNet.Mathematics
{
    [Factory(typeof(RealValue), "Functions for operating on real numbers")]
    public static class Real
    {
        #region Factories
        public static RealValue Decimal(double value) => new(value);
        
        public static RealValue Integer(long value) => new(value);

        public static RealValue Rational(long numerator, long denominator) => new(numerator, denominator);
        #endregion

        #region Values
        [SpecialValue(typeof(RealValue), "An arbitrarily small decimal value")]
        public static RealValue Epsilon => new(double.Epsilon);
        
        [SpecialValue(typeof(RealValue), "The additive identity of the set of real numbers")]
        public static RealValue Zero => new(0);
        
        [SpecialValue(typeof(RealValue), "The multiplicative identity of the set of real numbers")]
        public static RealValue One => new(1);
        #endregion

        #region Transformations
        public static RealValue Absolute(RealValue value) =>
            Decimal(value.AbsoluteValue);
        public static RealValue SquareRoot(RealValue value) =>
            value.SquareRoot;
        
        public static RealValue Root(RealValue value, int root) =>
            value.Root(root);
        
        public static RealValue Square(RealValue value) =>
            value.Square;
        
        public static RealValue Power(RealValue value, int power) => 
            value.Power(power);
        
        public static RealValue ArcTangent(RealValue value) => value.ArcTangent;
        #endregion
    }
}
