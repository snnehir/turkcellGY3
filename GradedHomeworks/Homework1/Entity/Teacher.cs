public class Teacher: BaseEntity
{
    private static int lastId = 500;
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public Classroom? Classroom { get; set; }
    public Teacher(string firstName, string lastName, Classroom classroom)
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

    public string GetTeacherInfo()
    {
        return $"{FirstName} {LastName} [{Id}]";
    }
}