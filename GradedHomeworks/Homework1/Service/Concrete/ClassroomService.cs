public class ClassroomService : IClassroomService
{
    private List<Classroom> _classrooms;
    public ClassroomService()
    {
        _classrooms = new();
    }

    public void Add(Classroom entity)
    {
        if (GetClassroomByGradeAndBranch(entity.Grade, entity.Branch) != null)
            throw new Exception("Classroom with same grade and branch already exists!");
        _classrooms.Add(entity);
    }
    public Classroom? GetById(int Id) => _classrooms.FirstOrDefault(x => x.Id == Id);
    public List<Classroom> GetAll() => _classrooms;
    public void Remove(int Id)
    {
        Classroom? classroom = _classrooms.FirstOrDefault(x => x.Id == Id);
        if (classroom == null)
            throw new Exception("Classroom not found.");
        else
        {
            foreach (Student student in classroom.Students)
            {
                student.Classroom = null;
            }
            if(classroom.Teacher != null)
            {
                classroom.Teacher.Classroom = null;
            }
            _classrooms.Remove(classroom);
        }
    }
    public void Update(Classroom entity)
    {
        Classroom? classroom = GetById(entity.Id);
        if (classroom == null)
            throw new Exception("Classroom not found.");
        int index = _classrooms.IndexOf(classroom);
        _classrooms[index] = entity;
    }
    public void AddStudent(Classroom classroom, Student student) => classroom.Students.Add(student);
    public void AssignTeacher(Classroom classroom, Teacher teacher) => classroom.Teacher = teacher;
    public Classroom? GetClassroomByGradeAndBranch(int grade, string branch) => _classrooms.FirstOrDefault(c => c.Grade == grade && c.Branch.Equals(branch));
    public void RemoveStudent(int classroomId, int studentId)
    {
        Classroom? classroom = _classrooms.FirstOrDefault(x => x.Id == classroomId);
        if (classroom == null)
            throw new Exception("Classroom not found.");
        Student? student = classroom.Students.FirstOrDefault(s => s.Id == studentId);
        if (student == null)
            throw new Exception("Student not found in this classroom.");
        student.Classroom = null;
        classroom.Students.Remove(student);
    }
    public List<Student> SearchStudentByFullName(int classroomId, string firstName, string lastName)
    {
        Classroom? classroom = _classrooms.FirstOrDefault(x => x.Id == classroomId);
        if (classroom == null)
            throw new Exception("Classroom not found.");
        return classroom.Students.Where(x => x.FirstName == firstName && x.LastName == lastName).ToList();
    }

    public List<Student> SearchStudentByName(int classroomId, string firstName)
    {
        Classroom? classroom = _classrooms.FirstOrDefault(x => x.Id == classroomId);
        if (classroom == null)
            throw new Exception("Classroom not found.");
        return classroom.Students.Where(x => x.FirstName == firstName).ToList();
    }

    public Student? SearchStudentById(int classroomId, int studentId)
    {
        Classroom? classroom = _classrooms.FirstOrDefault(x => x.Id == classroomId);
        if (classroom == null)
            throw new Exception("Classroom not found.");
        return classroom.Students.FirstOrDefault(s => s.Id == studentId);
    }
    public List<Classroom> GetClassroomsByGrade(int grade) => _classrooms.Where(x => x.Grade == grade).ToList();
    public List<Student> GetStudentsOfClassroom(int classroomId)
    {
        Classroom? classroom = _classrooms.FirstOrDefault(x => x.Id == classroomId);
        if (classroom == null)
            throw new Exception("Classroom not found.");
        return classroom.Students;
    }
    public bool IsClassroomListEmpty() => _classrooms.Count == 0;

    public void AddHomework(Classroom classroom, Homework homework) => classroom.Homeworks.Add(homework);
}