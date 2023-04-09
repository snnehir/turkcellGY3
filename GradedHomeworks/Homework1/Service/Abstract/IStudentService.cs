public interface IStudentService: IBaseService<Student>
{
    List<Student> GetStudentsByFullName(string firstName, string lastName);
    List<Student> GetStudentsByName(string firstName);
}