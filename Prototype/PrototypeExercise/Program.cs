using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace PrototypeExercise
{
    public static class ExtensionMethods
    {
        public static T DeepCopy<T>(this T self)
        {
            using (var stream = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(stream, self);
                stream.Position = 0;
                object copy = formatter.Deserialize(stream);
                return (T) copy;
            }
        }
    }

    [Serializable]
    public class Point
    {
        public int X, Y;
        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }
    }

    [Serializable]
    public class Line
    {
        public Point Start, End;

        public Line(Point start, Point end)
        {
            Start = start;
            End = end;
        }

        public override string ToString()
        {
            return $"From {Start.X}, {Start.Y} To {End.X}, {End.Y}";
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            Line Original = new Line(new Point(0, 0), new Point(5, 5));
            Console.WriteLine(Original);

            Line Copy = Original.DeepCopy();
            Copy.Start.X = 15;
            Copy.Start.Y = 15;
            Copy.End = new Point(50, 50);
            Console.WriteLine(Copy);
        }
    }
}
