using System;

namespace Factory
{
    public enum CoordinateSystem
    {
        Cartesian,
        Polar
    }

    public static class PointFactory
    {
        public static Point NewCartesianPoint(double x, double y)
        {
            return new Point(x, y);
        }

        public static Point NewPolarPoint(double rho, double theta)
        {
            return new Point(rho * Math.Cos(theta), rho * Math.Sin(theta));
        }
    }

    public class Point
    {
        private double x, y;
        public Point(double x, double y)
        {
            this.x = x;
            this.y = y;
        }

        public override string ToString()
        {
            return $"{nameof(x)}: {x}, {nameof(y)}: {y}";
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            var CartesianPoint = PointFactory.NewCartesianPoint(1.0, 1.0);
            Console.WriteLine("CartesianPoint");
            Console.WriteLine(CartesianPoint);

            var PolarPoint = PointFactory.NewPolarPoint(1.0, 1.0);
            Console.WriteLine("\nPolarPoint");
            Console.WriteLine(PolarPoint);
        }
    }
}
