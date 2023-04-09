public interface ITeacherService: IBaseService<Teacher>
{
    List<Teacher> GetTeachersByFullName(string firstName, string lastName);
}