using System;

namespace SingletonMonostate
{
    public class CEO
    {
        private static string name;
        private static int age;

        public string Name
        {
            get => name;
            set => name = value;
        }

        public int Age
        {
            get => age;
            set => age = value;
        }

        public override string ToString()
        {
            return $"{nameof(Name)}: {Name}, {nameof(Age)}: {Age}";
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            var CEO = new CEO();
            CEO.Name = "John";
            CEO.Age = 45;
            Console.WriteLine(CEO);

            var CEO2 = new CEO();
            CEO.Age = 32;
            Console.WriteLine(CEO2);
        }
    }
}
