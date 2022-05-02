using System;

namespace BridgeExercise
{
    public interface IRenderer
    {
        string WhatToRenderAs { get; }
    }

    public abstract class Shape
    {
        private IRenderer _renderer;

        protected Shape(IRenderer renderer)
        {
            _renderer = renderer ?? throw new ArgumentNullException(nameof(renderer));
        }

        public string Name { get; set; }

        public override string ToString()
        {
            return $"Drawing {Name} as {_renderer.WhatToRenderAs}";
        }
    }

    public class Triangle : Shape
    {
        public Triangle(IRenderer renderer) : base(renderer)
        {
            Name = "Triangle";
        }
    }

    public class Square : Shape
    {
        public Square(IRenderer renderer) : base(renderer)
        {
            Name = "Square";
        }
    }

    public class VectorRenderer : IRenderer
    {
        public string WhatToRenderAs
        {
            get
            {
                return "lines";
            }
        }
    }

    public class RasterRenderer : IRenderer
    {
        public string WhatToRenderAs
        {
            get
            {
                return "pixels";
            }
        }
    }

    // imagine VectorTriangle and RasterTriangle are here too
    internal class Program
    {
        static void Main(string[] args)
        {
            var Sq = new Square(new VectorRenderer());
            Console.WriteLine($"{Sq}");
        }
    }
}
