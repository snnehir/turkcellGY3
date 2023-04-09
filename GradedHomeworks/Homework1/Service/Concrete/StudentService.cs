using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;

public class StudentService : IStudentService
{
    List<Student> students;
    public StudentService(List<Student> students)
    {
        this.students = students;
    }
    public StudentService()
    {
        students = new();
    }
    public void Add(Student entity) => students.Add(entity);

    public Student? GetById(int Id) => students.FirstOrDefault(s => s.Id == Id);
    public List<Student> GetAll() => students;
    public void Remove(int Id)
    {
        Student? student = GetById(Id);
        if (student == null)
            Console.WriteLine("Student not found.");
        else
        {
            students.Remove(student);
            Console.WriteLine("Student removed.");
        }
    }
    public void Update(Student entity)
    {
        Student? student = GetById(entity.Id);
        if (student == null)
            Console.WriteLine("Student not found.");
        else
        {
            int index = students.IndexOf(student);
            students[index] = entity;
            Console.WriteLine("Student info updated.");
        }
    }

    public List<Student> GetStudentsByFullName(string firstName, string lastName) => students.Where(s => s.FirstName == firstName && s.LastName == lastName).ToList();

    public List<Student> GetStudentsByName(string firstName) => students.Where(s => s.FirstName == firstName).ToList();
}