using System;
using SciNet.Mathematics.Numerical;

namespace SciNet.Tests
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine(Vector.Row(1, 2, 3));
            Console.WriteLine(Vector.Row(1, 2, 3.0));
            Console.WriteLine(Vector.Row(1, 2, 3.0m));
            Console.WriteLine(Vector.Row<decimal>(1, 2, 3));
            Console.WriteLine(Vector.Row(VectorPrototype.Zero, 5));
            Console.WriteLine(Vector.Row(VectorPrototype.Random, 5));

            Console.WriteLine(Vector.Column(1, 2, 3));
            Console.WriteLine(Vector.Column(1, 2, 3.0));
            Console.WriteLine(Vector.Column(1, 2, 3.0m));
            Console.WriteLine(Vector.Column<decimal>(1, 2, 3));
            Console.WriteLine(Vector.Column(VectorPrototype.Zero, 5));
            Console.WriteLine(Vector.Column(VectorPrototype.Random, 5));
        }
    }
}
