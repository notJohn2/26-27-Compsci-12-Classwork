class Container
{
    public string Brand;
    public string Color;
    private int _maxVolume;
    private int _volume;

    public Container(string brand, string color, int maxVolume)
    {
        this.Brand = brand;
        this.Color = color;
        this._maxVolume = maxVolume;
        this._volume = 0;
    }
    public virtual void Consume(int amount)
    {
        int newVolume = this._volume -= amount;
        if (newVolume < 0)
        {
            newVolume = 0;
        }
        this._volume = newVolume;
    }
    public virtual void Fill(int amount)
    {
        int newVolume = this._volume += amount;
        if (newVolume > this._maxVolume)
        {
            newVolume = this._maxVolume;
        }
        this._volume = newVolume;
        
    }
    public override string ToString()
    {
        return $"{this.Color} {this.Brand} - Filled volume: {this._volume}ml / {this._maxVolume}ml";
    }
}

class SealableContainer : Container
{
    private bool _isSealed;
    public SealableContainer(string brand, string color, int maxVolume) : base(brand, color, maxVolume)
    {
        this._isSealed = true;
    }
    public override void Consume(int amount)
    {
        if (this._isSealed == true)
        {
            throw new Exception("Cannot consume from a sealed container");
        }
        base.Consume(amount);
    }
    public override void Fill(int amount)
    {
        if (this._isSealed == true)
        {
            throw new Exception("Cannot fill up a sealed container");
        }
        base.Fill(amount);
    }
    public virtual void Open()
    {
        this._isSealed = false;
    }
    public virtual void Close()
    {
        this._isSealed = true;
    }
    public override string ToString()
    {
        return base.ToString() + $"    Sealed: {this._isSealed}";
    }
}

class LockableContainer : SealableContainer
{
    private bool _isLocked;

    public LockableContainer(string brand, string color, int maxVolume) : base(brand, color, maxVolume)
    {
        this._isLocked = false;
    }
    public override void Consume(int amount)
    {
        base.Consume(amount);
    }
    public override void Fill(int amount)
    {
        base.Fill(amount);
    }
    public override void Open()
    {
        if (this._isLocked)
        {
            throw new Exception("Cannot open locked container");
        }
        base.Open();
    }
    public override void Close()
    {
        // if i implemented only lock for closed containers, (_isLocked = true) would be impossible in the below context but whatever
        if (this._isLocked)
        {
            // IRL some locked containers can be locked first and closed, they just a click shut when closing after lock first
            throw new Exception("Cannot close locked container");
        }
        base.Close();
    }
    public void Lock()
    {
        // only lock closed containers would require getter for isSealed
        this._isLocked = true;
    }
    public void UnLock()
    {
        this._isLocked = false;
    }
    public override string ToString()
    {
        return base.ToString() + $"    Locked: {this._isLocked}";
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        // Testing Container
        Container bucket = new Container("Home Depot","Orange",20000);
        Console.WriteLine(bucket.ToString());   // 0ml / 20000ml
        bucket.Fill(15000);
        Console.WriteLine(bucket.ToString());   // 15000ml / 20000ml
        bucket.Fill(15000);
        Console.WriteLine(bucket.ToString());   // 20000ml / 20000ml
        bucket.Consume(17000);
        Console.WriteLine(bucket.ToString());   // 3000ml / 20000ml
        bucket.Consume(50000);
        Console.WriteLine(bucket.ToString());   // 0ml / 20000ml

        // Testing SealableContainer
        Console.WriteLine("===================================================================");
        SealableContainer microwave = new SealableContainer("Galanz", "Cyan", 14000);
        Console.WriteLine(microwave.ToString());    // 0 / 14000 ml, sealed
        try
        {
            microwave.Fill(5000);   // should throw error
        }
        catch
        {
            Console.WriteLine("oops, tried to fill a sealed container");
        }
        microwave.Open();
        Console.WriteLine("opened Galanz microwave");
        try
        {
            microwave.Fill(5000);   // should be successful
        }
        catch
        {
            Console.WriteLine("oops, tried to fill a sealed container");
        }
        Console.WriteLine(microwave.ToString());    // 5000 / 14000 ml, not sealed

        // Testing LockableContainer
        Console.WriteLine("===================================================================");
        LockableContainer vault = new LockableContainer("Vaultz","Black", 2800);
        Console.WriteLine(vault.ToString());    // 0 / 2800 ml, sealed, not locked
        vault.Open();
        vault.Fill(1000);
        Console.WriteLine(vault.ToString());    // 1000 / 2800 ml, not sealed, not locked
        vault.Lock();
        try
        {
            vault.Close();      // throws error - cant close locked container
        }
        catch
        {
            Console.WriteLine("oops, can't close the container, the lock is in the way");
        }
        Console.WriteLine(vault.ToString());    // 1000 / 2800 ml, not sealed, not locked
        vault.UnLock();
        Console.WriteLine("unlocked vault");
        vault.Close();
        Console.WriteLine(vault.ToString());    // 1000 / 2800 ml, sealed, not locked
        try
        {
            vault.Close();      // should be successful now
        }
        catch
        {
            Console.WriteLine("oops, can't close the container, the lock is in the way");
        }
        Console.WriteLine(vault.ToString());    // 1000 / 2800 ml, sealed, locked
    }
}
