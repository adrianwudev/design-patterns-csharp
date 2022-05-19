using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static System.Console;

namespace DecoratorDetectingDecoratorCycles
{
    public abstract class Shape
    {
        public virtual string AsString() => string.Empty;
    }

    public class Circle : Shape
    {
        private float _radius;

        public Circle(float radius)
        {
            _radius = radius;
        }

        public void Resize(float facotr)
        {
            _radius *= facotr;
        }

        public override string AsString() => $"A circle with radius {_radius}";
    }

    public class Square : Shape
    {
        private float _side;

        public Square(float side)
        {
            _side = side;
        }

        public override string AsString() => $"A square with side {_side}";
    }

    public abstract class ShapeDecorator : Shape
    {
        protected internal readonly List<Type> types = new();
        protected internal Shape _shape;

        protected ShapeDecorator(Shape shape)
        {
            _shape = shape;
            if (_shape is ShapeDecorator sd)
                types.AddRange(sd.types);
        }
    }

    public abstract class ShapeDecorator<TSelf, TCyclePolicy> : ShapeDecorator
        where TCyclePolicy : ShapeDecoratorCyclePolicy, new()
    {
        protected readonly TCyclePolicy _policy = new();

        protected ShapeDecorator(Shape shape) : base(shape)
        {
            if (_policy.TypeAdditionAllowed(typeof(TSelf), types))
                types.Add(typeof(TSelf));
        }
    }

    public class ShapeDecoratorPolicy<T>
        : ShapeDecorator<T, ThrowOnCyclePolicy>
    {
        public ShapeDecoratorPolicy(Shape shape) : base(shape)
        {
        }
    }

    public class ColoredShape 
        : ShapeDecorator<ColoredShape, AbsorbCyclePolicy>
        //: ShapeDecoratorPolicy<ColoredShape>
    {
        private readonly string _color;

        public ColoredShape(Shape shape, string color) : base(shape)
        {
            _shape = shape ?? throw new ArgumentNullException(paramName: nameof(shape));
            _color = color ?? throw new ArgumentNullException(paramName: nameof(color));
        }

        public override string AsString()
        {
            var sb = new StringBuilder($"{_shape.AsString()}");
            // type initialized
            // types[0] -> current
            // types[1...] -> what it wraps
            if (_policy.ApplicationAllowed(types[0], types.Skip(1).ToList()))
                sb.Append($" has the color {_color}");

            return sb.ToString();
        }
    }

    public class TransparentShape : Shape
    {
        private Shape _shape;
        private float _transparency;

        public TransparentShape(Shape shape, float transparency)
        {
            _shape = shape ?? throw new ArgumentNullException(nameof(shape));
            _transparency = transparency;
        }

        public override string AsString() => $"{_shape.AsString()} has {_transparency * 100}% transparency";
    }

    public abstract class ShapeDecoratorCyclePolicy
    {
        public abstract bool TypeAdditionAllowed(Type type, IList<Type> allTypes);
        public abstract bool ApplicationAllowed(Type type, IList<Type> allTypes);
    }

    public class ThrowOnCyclePolicy : ShapeDecoratorCyclePolicy
    {
        private bool handler(Type type, IList<Type> allTypes)
        {
            if (allTypes.Contains(type))
                throw new InvalidOperationException(
                    $"Cycle detected! Type is already a {type.FullName}");
            return true;
        }

        public override bool TypeAdditionAllowed(Type type, IList<Type> allTypes)
        {
            return handler(type, allTypes);
        }

        public override bool ApplicationAllowed(Type type, IList<Type> allTypes)
        {
            return handler(type, allTypes);
        }
    }

    public class CyclesAllowedPolicy : ShapeDecoratorCyclePolicy
    {
        public override bool TypeAdditionAllowed(Type type, IList<Type> allTypes)
        {
            return true;
        }

        public override bool ApplicationAllowed(Type type, IList<Type> allTypes)
        {
            return true;
        }
    }

    public class AbsorbCyclePolicy : ShapeDecoratorCyclePolicy
    {
        public override bool TypeAdditionAllowed(Type type, IList<Type> allTypes)
        {
            return true;
        }

        public override bool ApplicationAllowed(Type type, IList<Type> allTypes)
        {
            return !allTypes.Contains(type);
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            var cirlce = new Circle(2);

            var colored1 = new ColoredShape(cirlce, "red");
            
            var colored2 = new ColoredShape(colored1, "blue");
            WriteLine(colored2.AsString());
        }
    }
}
