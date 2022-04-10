using System;

namespace Factory
{
    public enum CoordinateSystem
    {
        Cartesian,
        Polar
    }

    public class Point
    {
        private double x, y;
        private Point(double x, double y)
        {
            this.x = x;
            this.y = y;
        }

        public static Point Origin2 = new Point(0, 0);

        public override string ToString()
        {
            return $"{nameof(x)}: {x}, {nameof(y)}: {y}";
        }

        public static class Factory
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
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            var CartesianPoint = Point.Factory.NewCartesianPoint(1.0, 1.0);
            Console.WriteLine("CartesianPoint");
            Console.WriteLine(CartesianPoint);

            var PolarPoint = Point.Factory.NewPolarPoint(1.0, 1.0);
            Console.WriteLine("\nPolarPoint");
            Console.WriteLine(PolarPoint);

            var OriginPoint = Point.Origin2;
            Console.WriteLine("\nOriginPoint");
            Console.WriteLine(OriginPoint);
        }
    }
}
