ResponsiblePerson rPerson = new ResponsiblePerson(new Person { Age = 18 });
Console.WriteLine(rPerson.Drink());
Console.WriteLine(rPerson.Drive());

public class Person
{
    public int Age { get; set; }

    public string Drink()
    {
        return "drinking";
    }

    public string Drive()
    {
        return "driving";
    }

    public string DrinkAndDrive()
    {
        return "driving while drunk";
    }
}

public class ResponsiblePerson
{
    private readonly Person _person;
    public ResponsiblePerson(Person person)
    {
        // todo
        _person = person;
    }

    public int Age 
    { 
        get { return _person.Age; } 
        set { _person.Age = value; } 
    }

    public string Drink()
    {
        if (_person.Age >= 18)
            return _person.Drink();
        return "too young";
    }

    public string Drive()
    {
        if (_person.Age >= 16)
            return _person.Drive();
        return "too young";
    }

    public string DrinkAndDrive()
    {
        return "dead";
    }
}