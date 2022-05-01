using System;

namespace AdapterExercise
{
    public class Square
    {
        public int Side;
    }

    public interface IRectangle
    {
        int Width { get; }
        int Height { get; }
    }

    public static class ExtensionMethods
    {
        public static int Area(this IRectangle rc)
        {
            return rc.Width * rc.Height;
        }
    }

    public class SquareToRectangleAdapter : IRectangle
    {
        private readonly Square _square;
        public SquareToRectangleAdapter(Square square)
        {
            // todo
            _square = square;
        }

        public int Width => _square.Side;
        public int Height => _square.Side;
        // todo

    }
}
