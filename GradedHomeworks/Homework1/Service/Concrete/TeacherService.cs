public class TeacherService : ITeacherService
{
    List<Teacher> teachers;
    public TeacherService(List<Teacher> teachers)
    {
        this.teachers = teachers;
    }
    public TeacherService()
    {
        teachers = new();
    }
    public void Add(Teacher entity)
    {
        teachers.Add(entity);
        Console.WriteLine("Teacher is added: " + entity.Id);
    }
    public Teacher? GetById(int Id)
    {
        return teachers.FirstOrDefault(x => x.Id == Id);
    }

    public List<Teacher> GetAll() => teachers;

    public void Remove(int Id)
    {
        Teacher? teacher = GetById(Id);
        if (teacher == null)
            Console.WriteLine("Teacher not found.");
        else
        {
            teachers.Remove(teacher);
            Console.WriteLine("Teacher removed.");
        }
    }

    public void Update(Teacher entity)
    {
        Teacher? teacher = GetById(entity.Id);
        if (teacher == null)
            Console.WriteLine("Teacher not found.");
        else
        {
            int index = teachers.IndexOf(teacher);
            teachers[index] = entity;
            Console.WriteLine("Teacher info updated.");
        }
    }

    public List<Teacher> GetTeachersByFullName(string firstName, string lastName) => teachers.Where(t => t.FirstName == firstName && t.LastName == lastName).ToList();

}