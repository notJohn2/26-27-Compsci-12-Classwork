// 2. 
//  a) Create a Character class (for an RPG game) with attributes:
//      health
//      mana
//      strength
//      intellect
//
// b) Create factory methods for the following classes with the following base-stats:
//      Warrior: 10 health, 0 mana, 25 strength, 5 intellect
//      Priest: 5 health, 20 mana, 5 strength, 10 intellect
//      Mage: 5 health, 15 mana, 3 strength, 20 intellect
//
// c)   Write some client code that will use at least one of the factory methods and print out some of the object's attributes.

class Character
{
    private int _health;
    private int _mana;
    private int _strength;
    private int _intellect;

    public Character(int health, int mana, int strength, int intellect)
    {
        this._health = health;
        this._mana = mana;
        this._strength = strength;
        this._intellect = intellect;
    }
    public static Character CreateWarrior()
    {
        return new Character(10, 0, 25, 5);
    }
    public static Character CreateMage()
    {
        return new Character(5, 20, 5, 10);
    }
    public static Character CreatePriest()
    {
        return new Character(5, 15, 3, 20);
    }
    public override string ToString()
    {
        return base.ToString() + $"(health={this._health}, mana={this._mana}, strength={this._strength}, intellect={this._intellect})";
    }
}

class Program
{
    public static void Main(string[] args)
    {
        Character warrior = Character.CreateWarrior();
        Console.WriteLine(warrior.ToString());

        Character warrior1 = Character.CreateWarrior();
        Console.WriteLine(warrior1.ToString());

        Character priest = Character.CreatePriest();
        Console.WriteLine(priest.ToString());

        Character priest1 = Character.CreatePriest();
        Console.WriteLine(priest1.ToString());

        Character mage = Character.CreateMage();
        Console.WriteLine(mage.ToString());
        
        Character mage1 = Character.CreateMage();
        Console.WriteLine(mage1.ToString());
    }
}
