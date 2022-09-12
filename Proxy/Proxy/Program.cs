// See https://aka.ms/new-console-template for more information
Driver driver = new Driver(12);
CarProxy carProxy = new CarProxy(driver);
carProxy.Drive();

public interface ICar
{
    void Drive();
}

public class Car : ICar
{
    public void Drive()
    {
        Console.WriteLine("Car is been driven");
    }
}

public class Driver
{
    public Driver(int age)
    {
        Age = age;
    }
    public int Age { get; set; }
}

public class CarProxy : ICar
{
    private Driver driver;
    private Car car = new Car();
    public CarProxy(Driver _driver)
    {
        driver = _driver;
    }

    public void Drive()
    {
        if (driver.Age >= 16)
            car.Drive();
        else
            Console.WriteLine("Too Young");
    }
}