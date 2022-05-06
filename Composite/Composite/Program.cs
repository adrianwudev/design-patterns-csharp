using System;
using System.Collections.Generic;
using System.Text;

namespace Composite
{
    public class GraphicObject
    {
        public virtual string Name { get; set; } = "Group";
        public string Color;

        private Lazy<List<GraphicObject>> children = new Lazy<List<GraphicObject>>();
        public List<GraphicObject> Childern => children.Value;

        private void Print(StringBuilder sb, int depth)
        {
            sb.Append(new string('*', depth))
                .Append(string.IsNullOrWhiteSpace(Color) ? string.Empty : $"{Color} ")
                .AppendLine(Name);
            foreach(var child in Childern)
            {
                child.Print(sb, depth + 1);
            }
        }
        public override string ToString()
        {
            var sb = new StringBuilder();
            Print(sb, 0);
            return sb.ToString();
        }
    }

    public class Circle : GraphicObject
    {
        public override string Name => "Circle";
    }

    public class Square : GraphicObject
    {
        public override string Name => "Square";
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            var drawing = new GraphicObject { Name = "My Drawing" };
            drawing.Childern.Add(new Square { Color = "Red" });
            drawing.Childern.Add(new Circle { Color = "Yellow" });

            var group = new GraphicObject();
            group.Childern.Add(new Circle { Color = "Blue" });
            group.Childern.Add(new Square { Color = "Blue" });
            drawing.Childern.Add(group);

            Console.WriteLine(drawing);
        }
    }
}
