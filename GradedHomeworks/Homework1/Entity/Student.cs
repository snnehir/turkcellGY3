using System.Xml.Linq;

public class Student: BaseEntity
{
    private static int lastId = 1000;
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public Classroom? Classroom { get; set; }

    public Student(string firstName, string lastName, Classroom classroom)
    {
        Id = lastId++;
        FirstName = firstName;
        LastName = lastName;
        Classroom = classroom;
    }
    public override string ToString()
    {
        return $"{Id}\t{FirstName} {LastName}\t\t{Classroom?.Grade} - {Classroom?.Branch}";
    }
}