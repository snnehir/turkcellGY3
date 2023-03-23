Human human = new Human("Bob");
Human computerEngineer = new ComputerEngineer("Simon", "Go");

// Computer engineer list must contain only computer engineers. (Every human is not a computer engineer.)
// However, at compile time, the following line of code won't give any warnings:
Human[] computerEngineerArray = new Human[2];
computerEngineerArray[0] = human;
computerEngineerArray[1] = computerEngineer; // runtime error

// Generic lists are not covariant!
// The following line of code will give error at compile time. (better for us, developers)
List<Human> computerEngineerListErr = new List<ComputerEngineer>();

List<ComputerEngineer> computerEngineerList = new List<ComputerEngineer>();
// However, even we know that every computer engineers are humans, the following line will also give error at compile time.
List<Human> humanList = computerEngineerList;

// Example for covariance & controvariance

// We can do above operation by using IEnumerable because it is covariant. (It uses "out" keyword.)
// - Microsoft Definition Covariance:
//   An object that is instantiated with a more derived type argument is assigned to an object instantiated with a less derived type argument.
// Human enumerable contains list of ComputerEngineer (more derived)
IEnumerable<Human> humansEnumerable = computerEngineerList;

// If we want to sort list of humans or subclasses of human based on same property (i.e. Name), IComparable can be implemented in Human class.
// The IComparable interface is controvariant. (It uses "in" keyword.) 
// - Microsoft Definition Controvariance:
//   An object that is instantiated with a less derived type argument is assigned to an object instantiated with a more derived type argument.
// computerEngineerList is sorted by using implemented Human (less derived) compare method
computerEngineerList.Sort();


class Human : IComparable<Human>
{
    public string Name { get; set; }
    public Human(string Name)
    {
        this.Name = Name;
    }
    public void Greet()
    {
        Console.WriteLine($"Hello human, my name is {Name}!");
    }

    public int CompareTo(Human? obj)
    {
        return this.Name.CompareTo(obj?.Name);
    }
}

class ComputerEngineer : Human
{
    public string FavoriteProgrammingLanguage { get; set; }
    public ComputerEngineer(string Name) : base(Name)
    {
    }
    public ComputerEngineer(string Name, string FavoriteProgrammingLanguage) : base(Name)
    {
        this.FavoriteProgrammingLanguage = FavoriteProgrammingLanguage;
    }

}

interface IProducer<out T>      // covariant
{
    T Produce();
}

interface IConsumer<in T>       // contravariant
{
    void Consume(T obj);
}