public class Classroom: BaseEntity
{
    private static int lastId = 100;
    public int Grade { get; set; }
    public string Branch { get; set; }
    public List<Student> Students { get; set; }
    public List<Homework> Homeworks { get; set; }
    public Teacher? Teacher { get; set; }
    public Classroom(int grade, string branch)
    {
        Id = lastId++;
        Grade = grade;
        Branch = branch;
        Students = new();
        Homeworks = new();
    }
    public override string ToString()
    {
        return $"{Id}\t{Grade}-{Branch}\t{Teacher?.GetTeacherInfo()}\t\t{Students.Count}";
    }

}