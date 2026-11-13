// Refactoring pre-existing code
// This code has two classes that have striking similarities (though important differences).
// Abstract the similarities into a base-class (or parent-class) and have Player and Enemy both inherit from it.
public class Character
{
    public int Health;
    public int Strength;
    public string Name;
    public Character(string name, int health, int strength)
    {
        this.Name = name;
        this.Health = health;
        this.Strength = strength;
    }
    public virtual void Attack(Character other)
    {
        int damage = this.Strength;
        other.TakeDamage(damage);
    }
    public virtual void TakeDamage(int damage)
    {
        this.Health -= damage;
    }
    public virtual bool IsDefeated()
    {
        return this.Health <= 0;
    }
}
public class Player : Character
{
    private float _dodgeChance;
    private float _armor;
    
    public Player(string name, int health, int strength) : base(name, health, strength)
    {
        this._dodgeChance = 0.1f;
        this._armor = 50f;
    }
    public Player(string name, int health, int strength, float dodgeChance, float armor) : base(name, health, strength)
    {
        this._dodgeChance = dodgeChance;
        this._armor = armor;
    }

    public override void Attack(Character other)
    {
        int damage = 15 + base.Strength * 50;
        other.TakeDamage(damage);
    }

    public override void TakeDamage(int damage)
    {
        int netDamage;
        int mitigation;
        
        Random rnd = new Random();
        int dodgeRoll = rnd.Next(0, 100);
        
        if (dodgeRoll < this._dodgeChance * 100)
        {
            netDamage = 0;
        }
        else
        {
            mitigation = damage / (int) (this._armor * 0.5);
            netDamage = damage - mitigation;
        }
        base.TakeDamage(netDamage);
    }

    public override bool IsDefeated()
    {
        return base.IsDefeated();
    }
}

public class Enemy : Character
{
    public Enemy(string name, int health, int strength) : base(name, health, strength) {}

    public override void Attack(Character other) 
    {
        int damage = 5 + (int) (base.Strength * 0.3);
        other.TakeDamage(damage);
    }
    
    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
    }

    public override bool IsDefeated()
    {
        return base.IsDefeated();
    }
}

public class Program {
    public static void Main(string[] args) {
        Player player = new Player("Mr. Gallo", 100, 999);
        Enemy enemy = new Enemy("Goblin", 100, 50);

        enemy.Attack(player);
        Console.WriteLine(player.Health);
        Console.WriteLine(player.IsDefeated());

        player.Attack(enemy);
        Console.WriteLine(enemy.Health);
        Console.WriteLine(enemy.IsDefeated());

    }
}
