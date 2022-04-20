using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;

namespace PrototypeInheritance
{
    public static class ExtensionMethods
    {
        public static T DeepCopy<T>(this T self)
        {
            var stream = new MemoryStream();
            var formatter = new BinaryFormatter();
            formatter.Serialize(stream, self);
            stream.Seek(0, SeekOrigin.Begin);
            object copy = formatter.Deserialize(stream);
            stream.Close();
            return (T) copy;
        }

        public static T DeepCopyXml<T>(this T self)
        {
            using (var ms = new MemoryStream())
            {
                var s = new XmlSerializer(typeof(T));
                s.Serialize(ms, self);
                ms.Position = 0;
                return (T) s.Deserialize(ms);
            }
        }
    }

    [Serializable]
    public class Address
    {
        public string StreetName;
        public int HouseNumber;

        public Address()
        {

        }

        public Address(string streetName, int houseNumber)
        {
            StreetName = streetName;
            HouseNumber = houseNumber;
        }

        public void CopyTo(Address target)
        {
            target.StreetName = StreetName;
            target.HouseNumber = HouseNumber;
        }

        public override string ToString()
        {
            return $"{nameof(StreetName)}: {StreetName}, " + 
                   $"{nameof(HouseNumber)}: {HouseNumber}";
        }
    }

    [Serializable]
    public class Person
    {
        public string[] Names;
        public Address Address;

        public Person()
        {

        }

        public Person(string[] names, Address address)
        {
            Names = names;
            Address = address;
        }

        public void CopyTo(Person target)
        {
            target.Names = (string[]) Names.Clone();
            target.Address = Address.DeepCopy();
        }

        public override string ToString()
        {
            return $"{nameof(Names)}: {string.Join(",", Names)}, " +
                   $"{nameof(Address)}: {Address}";
        }
    }

    [Serializable]
    public class Employee : Person
    {
        public int Salary;
        public Employee()
        {

        }

        public Employee(string[] names, Address address,
            int salary) : base(names, address)
        {
            Salary = salary;
        }

        public override string ToString()
        {
            return $"{base.ToString()}, {nameof(Salary)}: {Salary}";
        }

        public void CopyTo(Employee target)
        {
            base.CopyTo(target);
            target.Salary = Salary;
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            var john = new Employee();
            john.Names = new[] { "John", "Doe"};
            john.Address = new Address
            {
                HouseNumber = 123,
                StreetName = "London Road",
            };
            john.Salary = 32100;

            var copy = john.DeepCopyXml();
            copy.Names[0] = "Celeste";
            copy.Names[1] = "Smith";
            copy.Address.StreetName = "Citizen Road";
            copy.Address.HouseNumber++;
            copy.Salary = 12300;

            Console.WriteLine(john);
            Console.WriteLine(copy);
        }
    }
}
