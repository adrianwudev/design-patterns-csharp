using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using MoreLinq;

namespace Singleton
{
    public interface IDatabase
    {
        int GetPopulation(string name);
    }

    public class SingletonDatabase : IDatabase
    {
        private Dictionary<string, int> capitals;
        private SingletonDatabase()
        {
            Console.WriteLine("Initializing database......");

            capitals = File.ReadAllLines("capitals.txt")
                .Batch(2)
                .ToDictionary(
                    list => list.ElementAt(0).Trim(),
                    list => int.Parse(list.ElementAt(1))
                    );
        }

        public int GetPopulation(string name)
        {
            return capitals[name];
        }

        private static Lazy<SingletonDatabase> instance =
            new Lazy<SingletonDatabase>(() => new SingletonDatabase()); // Lazy<> No Initialize until Invoke// instance.value line: 37

        public static SingletonDatabase Instance => instance.Value;
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            var db = SingletonDatabase.Instance;
            string City = "Tokyo";
            Console.WriteLine($"{City} has population {db.GetPopulation(City)}");
        }
    }
}
