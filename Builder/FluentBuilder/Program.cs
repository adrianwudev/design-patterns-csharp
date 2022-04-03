public class Person
{
    public string? Name { get; set; }
    public string? Position { get; set; }
    public DateTime DateOfBirth { get; set; }

    public class Builder : PersonBirthDateBuilder<Builder>
    {
        internal Builder() {}
    }

    public static Builder New => new Builder();

    public override string ToString()
    {
        return $"{nameof(Name)}: {Name}, {nameof(Position)}: {Position}";
    }
}

public abstract class PersonBuilder
{
    protected Person person = new Person();

    public Person Build()
    {
        return person;
    }
}

public class PersonInfoBuilder<SELF> : PersonBuilder
    where SELF : PersonInfoBuilder<SELF>
{
    public SELF Called(string name)
    {
        person.Name= name;
        return (SELF) this;
    }
}

public class PersonJobBuilder<SELF>
    : PersonInfoBuilder<PersonJobBuilder<SELF>>
    where SELF : PersonJobBuilder<SELF>
{
    public SELF WorkAsA(string position)
    {
        person.Position = position;
        return (SELF) this;
    }
}

public class PersonBirthDateBuilder<SELF>
    : PersonJobBuilder<PersonBirthDateBuilder<SELF>>
    where SELF : PersonBirthDateBuilder<SELF>
{
    public SELF Born(DateTime dateOfBirth)
    {
        person.DateOfBirth = dateOfBirth;
        return (SELF) this;
    }
}

internal class Program
{
    class SomeBuilder : PersonBirthDateBuilder<SomeBuilder>
    {

    }
    public static void Main(string[] args)
    {
       var me = Person.New
            .Called("Damian")
            .WorkAsA("Developer")
            .Born(DateTime.UtcNow)
            .Build();

        Console.WriteLine(me);
    }
}