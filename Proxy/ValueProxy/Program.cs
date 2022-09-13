// See https://aka.ms/new-console-template for more information
using System.Diagnostics;

Console.WriteLine("Hello, World!");
Console.WriteLine(
    10f * 5.Percent()
    );

Console.WriteLine(
    2.Percent() + 3.Percent() //5%
    );


[DebuggerDisplay("{value*100.0f}%")]
public struct Percentage : IEquatable<Percentage>
{
    private readonly float value;

    internal Percentage(float value)
    {
        this.value = value;
    }

    public static float operator *(float f, Percentage p)
    {
        return f * p.value;
    }

    public static Percentage operator +(Percentage a, Percentage b)
    {
        return new Percentage(a.value + b.value);
    }

    public static bool operator ==(Percentage left, Percentage right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(Percentage left, Percentage right)
    {
        return !(left == right);
    }

    public override string ToString()
    {
        return $"{value * 100}%";
    }

    public override bool Equals(object? obj)
    {
        return obj is Percentage percentage && Equals(percentage);
    }

    public bool Equals(Percentage other)
    {
        return value == other.value;
    }

    public override int GetHashCode()
    {
        return value.GetHashCode();
    }
}

public static class PercentageExtensions
{
    public static Percentage Percent(this float value)
    {
        return new Percentage(value / 100.0f);
    }

    public static Percentage Percent(this int value)
    {
        return new Percentage(value / 100.0f);
    }
}