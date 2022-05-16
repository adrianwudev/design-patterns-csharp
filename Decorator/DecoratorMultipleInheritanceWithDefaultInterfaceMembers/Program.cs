using System;

namespace DecoratorMultipleInheritanceWithDefaultInterfaceMembers
{
    using static System.Console;
    public interface ICreature
    {
        int Age { get; set; }
    }

    public interface IBird : ICreature
    {
        void Fly()
        {
            if (Age >= 10)
                WriteLine("I am flying");
        }
    }

    public interface ILizzard : ICreature
    {
        void Crawl()
        {
            if (Age < 10)
                WriteLine("I am crawling");
        }
    }

    public class Organism { }

    public class Dragon : Organism, IBird, ILizzard
    {
        public int Age { get; set; }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            var d = new Dragon { Age = 5};

            if (d is IBird bird)
                bird.Fly();

            if (d is ILizzard lizzard)
                lizzard.Crawl();
        }
    }
}
