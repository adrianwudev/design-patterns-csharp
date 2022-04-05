using System;

namespace FactoryMethod
{
    public class Point
    {
        public static Point NewCartesianPoint(double x, double y)
        {
            return new Point(x, y);
        }

        public static Point NewPolarPoint(double rho, double theta)
        {
            return new Point(rho * Math.Cos(theta), rho * Math.Sin(theta));
        }

        private double x, y;
        private Point(double x, double y)
        {
            this.x = x;
            this.y = y;
        }

        public override string ToString()
        {
            return $"{nameof(x)}: {x}, {nameof(y)}: {y}";
        }
    }
    public enum CoordinateSystem
    {
        Cartesian,
        Polar
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            var CartesianPoint = Point.NewCartesianPoint(1.0, 1.0);
            Console.WriteLine("CartesianPoint");
            Console.WriteLine(CartesianPoint);

            var PolarPoint = Point.NewPolarPoint(1.0, 1.0);
            Console.WriteLine("\nPolarPoint");
            Console.WriteLine(PolarPoint);
        }
    }
}
