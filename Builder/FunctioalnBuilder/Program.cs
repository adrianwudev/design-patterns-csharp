using System;
using System.Collections.Generic;

namespace FunctionalBuilder
{
    public class Person
    {
        public string Name, Position;

        public override string ToString()
        {
            return $"{Name} is a {Position}";
        }
    }

    public sealed class PersonBuilder
    {
        public readonly List<Action<Person>> Actions
            = new List<Action<Person>>();

        public PersonBuilder Called(string name)
        {
            Actions.Add(p => { p.Name = name; });
            return this;
        }

        public Person Build()
        {
            var p = new Person();
            Actions.ForEach(a => a(p));
            return p;
        }

    }

    public static class PersonBuilderExtensions
    {
        public static PersonBuilder WorkAsA
            (this PersonBuilder builder, string position)
        {
            builder.Actions.Add(p =>
            {
                p.Position = position;
            });
            return builder;
        }
    }
    internal class Program
    {

        static void Main(string[] args)
        {
            var pb = new PersonBuilder();
            var person = pb.Called("Damian")
                .WorkAsA("Developer")
                .Build();

            Console.WriteLine(person);
        }
    }
}
