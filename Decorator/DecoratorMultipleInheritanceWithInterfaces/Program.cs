using System;

namespace DecoratorMultipleInheritanceWithInterfaces
{
    public interface IBird
    {
        int Weight { get; set; }

        void Fly();
    }

    public interface ILizzard
    {
        int Weight { get; set; }

        void Crawl();
    }

    public class Bird : IBird
    {
        public int Weight { get; set; }

        public void Fly()
        {
            Console.WriteLine($"Soaring in the sky with weight {Weight}");
        }
    }

    public class Lizzard : ILizzard
    {
        public int Weight { get; set; }

        public void Crawl()
        {
            Console.WriteLine($"Crawling in the dirt with weight {Weight}");
        }
    }

    public class Dragon : IBird, ILizzard
    {
        private Bird _bird = new Bird();
        private Lizzard _lizzard = new Lizzard();
        private int _weight;

        public void Fly()
        {
            _bird.Fly();
        }
        public void Crawl()
        {
            _lizzard.Crawl();
        }

        public int Weight 
        {
            get { return _weight; }
            set
            { 
                _weight = value;
                _bird.Weight = value;
                _lizzard.Weight = value;
            }
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            var d = new Dragon();
            d.Weight = 45;
            d.Fly();
            d.Crawl();
        }
    }
}
