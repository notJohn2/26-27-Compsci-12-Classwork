// Keeping track of created objects
class Person
{
    private static int _numPeople = 0;
    private string _name;
    private int _id;

    public Person(string name)
    {
        this._name = name;
        this._id = Person._numPeople;
        Person._numPeople += 1;
        Console.WriteLine($"Created {this._name} with id {this._id}.");
        Console.WriteLine($"Number of people created: {Person._numPeople}");
    }

    // 2 Create a static getter method for _numPeople and call it to verify the number of people created.
    public static int GetNumPeople()
    {
        return _numPeople;
    }
    
}

class Program
{
    public static void Main(string[] args)
    {
        // 1. Copy this code and create a few Person objects to see what happens.
        // each time a person is created, their ID (total population - 1) as well as the total population is written to the console.
        Person p1 = new Person("Jeb");
        Person p2 = new Person("Val");
        Person p3 = new Person("Bill");
        Person p4 = new Person("Bob");

        // 2. Create a static getter method for _numPeople and call it to verify the number of people created.
        Console.WriteLine("Total people created: " + Person.GetNumPeople());  // Total people created: 4
    }
}
