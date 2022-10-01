
public class Creature
{
    public string Name;
    public int Attack, Defense;

    public Creature (string name, int attack, int defense)
    {
        Name = name ?? throw new ArgumentNullException(paramName: nameof(name));
        Attack = attack;
        Defense = defense;
    }

    public override string ToString()
    {
        return $"{nameof(Name)}: {Name}, {nameof(Attack)}: {Attack}, {nameof(Defense)}: {Defense}";
    }
}

public class CreatureModifier
{
    protected Creature creature;
    protected CreatureModifier next;

    public CreatureModifier(Creature creature)
    {
        this.creature = creature ?? throw new ArgumentNullException(paramName: nameof(creature));
    }

    public void Add(CreatureModifier cm)
    {
        if (next != null) next.Add(cm);
        else next = cm;
    }

    public virtual void Handle() => next?.Handle();
}

public class NoBonusesModifier : CreatureModifier
{
    public NoBonusesModifier(Creature creature) : base(creature)
    {

    }

    public override void Handle()
    {
        //
    }
}

public class DobuleAttackModifier : CreatureModifier
{
    public DobuleAttackModifier (Creature creature): base(creature)
    {

    }

    public override void Handle()
    {
        Console.Write($"Dobuling {creature.Name}'s attack");
        creature.Attack *= 2;
        base.Handle();
    }
}

public class DoubleDefenseModifier : CreatureModifier
{
    public DoubleDefenseModifier (Creature creature) : base(creature)
    {

    }

    public override void Handle()
    {
        Console.WriteLine($"Dobuling {creature.Name}'s defense");
        creature.Defense *= 2;
        base.Handle();
    }
}

public class ChainOfResponsibility
{
    static void Main(string[] args)
    {
        var goblin = new Creature("Goblin", 2, 2);
        Console.WriteLine(goblin);

        var root = new CreatureModifier(goblin);

        root.Add(new NoBonusesModifier(goblin));

        Console.WriteLine("Let's double goblin's attack...");
        root.Add(new DobuleAttackModifier(goblin));

        Console.WriteLine("Let's double goblin's defense...");
        root.Add(new DoubleDefenseModifier(goblin));

        // eventually...
        root.Handle();
        Console.WriteLine(goblin);
    }
}