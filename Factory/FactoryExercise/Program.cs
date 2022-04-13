using System;
using System.Collections.Generic;

namespace FactoryExercise
{
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    internal interface IPersonFactory
    {
        void CreatePerson(string Name);
        void listPerson();
    }

    internal class PersonFactory : IPersonFactory
    {
        private List<Person> _list = new List<Person>();

        public PersonFactory(List<Person> list)
        {
            _list = list;
        }

        public void CreatePerson(string Name)
        {
            _list.Add(new Person { Id = _list.Count, Name = Name });
        }

        public void listPerson()
        {
            foreach (var person in _list)
            {
                Console.WriteLine($"Id: {person.Id}, Name: {person.Name}");
            }
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            List<Person> list = new List<Person>();
            IPersonFactory factory = new PersonFactory(list);
            factory.CreatePerson("John");
            factory.CreatePerson("Carlos");
            factory.CreatePerson("Mariana");

            IPersonFactory factory2 = new PersonFactory(list);
            factory2.CreatePerson("Celeste");
            factory2.listPerson();
        }
    }
}
