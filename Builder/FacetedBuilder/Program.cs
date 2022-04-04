using System;

namespace FacetedBuilder
{
    class Program
    {
        public class Person
        {
            // Address
            public string StreetAddress, Postcode, City;
            // Employment
            public string CompanyName, Position;
            public int AnnualIncome;
            public override string ToString()
            {
                return $"{nameof(StreetAddress)}: {StreetAddress}, {nameof(Postcode)}: {Postcode}, {nameof(City)}: {City}," +
                    $" \n{nameof(CompanyName)}: {CompanyName}, {nameof(Position)}: {Position}, {nameof(AnnualIncome)}: {AnnualIncome}";
            }
        }

        public class PersonBuilder // Facade
        {
            // The object we're going to build
            protected Person person = new Person();

            public PersonAddressBuilder Lives => new PersonAddressBuilder(person);
            public PersonJobBuilder Works => new PersonJobBuilder(person);

            public static implicit operator Person(PersonBuilder pb)
            {
                return pb.person;
            }
        }

        public class PersonJobBuilder : PersonBuilder
        {
            public PersonJobBuilder(Person person)
            {
                this.person = person;
            }

            public PersonJobBuilder At(string companyName)
            {
                person.CompanyName = companyName;
                return this;
            }

            public PersonJobBuilder AsA(string position)
            {
                person.Position = position;
                return this;
            }

            public PersonJobBuilder Earning(int annualIncome)
            {
                person.AnnualIncome = annualIncome;
                return this;
            }
        }

        public class PersonAddressBuilder : PersonBuilder
        {
            // might not work with a value type!
            public PersonAddressBuilder(Person person)
            {
                this.person = person;
            }

            public PersonAddressBuilder At(string streetAddress)
            {
                person.StreetAddress = streetAddress;
                return this;
            }

            public PersonAddressBuilder WithPostcode(string postcode)
            {
                person.Postcode = postcode;
                return this;
            }

            public PersonAddressBuilder In(string city)
            {
                person.City = city;
                return this;
            }
        }
        static void Main(string[] args)
        {
            var pb = new PersonBuilder();
            Person person = pb
                .Lives
                    .At("155 Taipei Road")
                    .In("Taipei")
                    .WithPostcode("123")
                .Works
                    .At("Amazon")
                    .AsA("Developer")
                    .Earning(1500000);

            Console.WriteLine(person);
        }
    }
}
