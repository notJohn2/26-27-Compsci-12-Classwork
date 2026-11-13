public class Pizza {
    private string[] _toppings;
    public Pizza(string[] toppings) {
        this._toppings = toppings;
    }
    public void PrintToppings() {
        foreach (string t in this._toppings)
            Console.WriteLine(t);
    }
    public static Pizza CreateMeatlovers() {
        return new Pizza(new string[] {
            "cheese",
            "ground beef",
            "pepperoni",
            "bacon",
            "sausage"
        });
    }
    // Create a factory method for a cheese pizza. Modify the client code to make use of it.
    public static Pizza CreateCheese()
    {
        return new Pizza(new string[] {"cheese"});
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        Pizza cheese = new Pizza(new string[] {"cheese"});
        cheese.PrintToppings();

        Pizza meatLovers1 = Pizza.CreateMeatlovers();
        meatLovers1.PrintToppings();
        Pizza meatLovers2 = Pizza.CreateMeatlovers();
        meatLovers2.PrintToppings();

        // Create a factory method for a cheese pizza. Modify the client code to make use of it.
        Pizza cheese1 = Pizza.CreateCheese();
        cheese1.PrintToppings();
    }
}
