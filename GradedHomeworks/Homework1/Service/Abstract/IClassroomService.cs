public interface IClassroomService: IBaseService<Classroom>
{
    Classroom? GetClassroomByGradeAndBranch(int grade, string branch);
    void AddStudent(Classroom classroom, Student student);
    void AssignTeacher(Classroom classroom, Teacher teacher);
    Student? SearchStudentById(int classroomId, int studentId);
    List<Student> SearchStudentByFullName(int classroomId, string firstName, string lastName);
    List<Student> SearchStudentByName(int classroomId, string firstName);
    List<Student> GetStudentsOfClassroom(int classroomId);
    void RemoveStudent(int classroomId, int studentId);
    List<Classroom> GetClassroomsByGrade(int grade);
    void AddHomework(Classroom classroom, Homework homework);
}