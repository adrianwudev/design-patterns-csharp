using System;
using static System.Console;

namespace DecoratorDynamicDecoratorComposition
{
    public interface IShape
    {
        string AsString();
    }

    public class Circle : IShape
    {
        private float _radius;

        public Circle(float radius)
        {
            _radius = radius;
        }

        public void Resize (float facotr)
        {
            _radius *= facotr;
        }

        public string AsString() => $"A circle with radius {_radius}";
    }

    public class Square : IShape
    {
        private float _side;

        public Square(float side)
        {
            _side = side;
        }

        public string AsString() => $"A square with side {_side}";
    }

    public class ColoredShape : IShape
    {
        private IShape _shape;
        private string _color;

        public ColoredShape(IShape shape, string color)
        {
            _shape = shape ?? throw new ArgumentNullException(paramName: nameof(shape));
            _color = color ?? throw new ArgumentNullException(paramName: nameof(color));
        }

        public string AsString() => $"{_shape.AsString()} has the color {_color}";
    }

    public class TransparentShape : IShape
    {
        private IShape _shape;
        private float _transparency;

        public TransparentShape(IShape shape, float transparency)
        {
            _shape = shape ?? throw new ArgumentNullException(nameof(shape));
            _transparency = transparency;
        }

        public string AsString() => $"{_shape.AsString()} has {_transparency * 100}% transparency";
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            var square = new Square(1.23f);
            WriteLine(square.AsString());

            var redSquare = new ColoredShape(square, "red");
            WriteLine(redSquare.AsString());

            var redHalfTransparentSquare = new TransparentShape(redSquare, 0.5f);
            WriteLine(redHalfTransparentSquare.AsString());
        }
    }
}
