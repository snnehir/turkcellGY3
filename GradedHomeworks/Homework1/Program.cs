TeacherService teacherService = new();
StudentService studentService = new();
ClassroomService classroomService = new();

DisplayMenu();
Console.Write("> Enter your choice (Press any other key to quit): ");
string userInput = Console.ReadLine() ?? string.Empty;
while (IsTerminationInput(userInput))
{
    int choice = int.Parse(userInput);
    switch (choice)
    {
        case 1:
            try
            {
                Console.Write("Enter Grade: ");
                int grade = int.Parse(Console.ReadLine());
                Console.Write("Enter Branch: ");
                string branch = Console.ReadLine().InputValidate();
                Classroom classroom = new(grade, branch);
                classroomService.Add(classroom);
                Console.WriteLine($"Classroom with id {classroom.Id} is added.");
            }
            catch (Exception exception)
            {
                Console.WriteLine("[1] Error: " + exception.Message);
            }
            break;
        case 2:
            try
            {
                if (classroomService.IsClassroomListEmpty())
                    throw new Exception("There is no classroom in the system!");
                Console.Write("Enter Teacher's First Name: ");
                string firstName = Console.ReadLine().InputValidate();
                Console.Write("Enter Teacher's Last Name : ");
                string lastName = Console.ReadLine().InputValidate();
                Console.Write("Enter Classroom Id: ");
                int classroomId = int.Parse(Console.ReadLine());
                Classroom? classroom = classroomService.GetById(classroomId);
                if (classroom == null)
                    throw new Exception("Classroom not found.");
                Teacher teacher = new(firstName, lastName, classroom);
                teacherService.Add(teacher);
                classroomService.AssignTeacher(classroom, teacher);
                Console.WriteLine("New teacher is added and assigned to the classroom.");
            }
            catch (Exception exception)
            {
                Console.WriteLine("[2] Error: " + exception.Message);
            }
            break;
        case 3:
            try
            {
                if (classroomService.IsClassroomListEmpty())
                    throw new Exception("There is no classroom in the system!");
                Console.Write("Enter Student's First Name: ");
                string firstName2 = Console.ReadLine().InputValidate();
                Console.Write("Enter Student's Last Name : ");
                string lastName2 = Console.ReadLine().InputValidate();
                Console.Write("Enter Student's Classroom Id: ");
                int classroomId = int.Parse(Console.ReadLine());
                Classroom? classroom = classroomService.GetById(classroomId);
                if (classroom == null)
                    throw new Exception("Classroom not found.");
                Student student = new(firstName2, lastName2, classroom);
                studentService.Add(student);
                classroomService.AddStudent(classroom, student);
                Console.WriteLine("New student is added to the classroom.");
            }
            catch (Exception exception)
            {
                Console.WriteLine("[3] Error: " + exception.Message);
            }
            break;
        case 4:
            try
            {
                Console.Write("Enter Teacher's First Name: ");
                string firstName3 = Console.ReadLine().InputValidate();
                Console.Write("Enter Teacher's Last Name: ");
                string lastName3 = Console.ReadLine().InputValidate();
                List<Teacher> teachers = teacherService.GetTeachersByFullName(firstName3, lastName3);
                if (teachers.Count == 0)
                {
                    Console.WriteLine("Teacher not found.");
                    break;
                }
                Console.WriteLine("Id\tFull Name\tClassroom");
                foreach (Teacher teacher in teachers)
                {
                    Console.WriteLine(teacher);
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine("[4] Error: " + exception.Message);
            }
            
            break;
        case 5:
            try
            {
                Console.Write("Enter Student's First Name: ");
                string firstName4 = Console.ReadLine().InputValidate();
                Console.Write("Enter Student's Last Name: ");
                string lastName4 = Console.ReadLine().InputValidate();
                List<Student> students = studentService.GetStudentsByFullName(firstName4, lastName4);
                if (students.Count == 0)
                    Console.WriteLine("Student not found.");
                Console.WriteLine("Id\tFull Name\tClassroom".ConstructTableHeader());
                foreach (Student student in students)
                {
                    Console.WriteLine(student);
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine("[5] Error: " + exception.Message);
            }
            break;

        case 6:
            try
            {
                Console.Write("Enter First Name: ");
                string firstName6 = Console.ReadLine().InputValidate();
                Console.Write("Enter Last Name: ");
                string lastName6 = Console.ReadLine().InputValidate();
                Console.Write("Enter Student's Classroom Id: ");
                int classroomId2 = int.Parse(Console.ReadLine());
                List<Student> students = classroomService.SearchStudentByFullName(classroomId2, firstName6, lastName6);
                if (students.Count == 0)
                {
                    Console.WriteLine("Student not found in this classroom.");
                    break;
                }
                Console.WriteLine("Id\tFull Name\tClassroom".ConstructTableHeader());
                foreach (Student student in students)
                {
                    Console.WriteLine(student);
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine("[6] Error: " + exception.Message);
            }
            break;
        case 7:
            List<Teacher> teachers2 = teacherService.GetAll();
            if(teachers2.Count == 0)
            {
                Console.WriteLine("No teacher found.");
                break;
            }
            Console.WriteLine("Id\tFull Name\tClassroom".ConstructTableHeader());
            foreach (Teacher teacher in teachers2)
            {
                Console.WriteLine(teacher);
            }
            break;
        case 8:
            List<Student> students2 = studentService.GetAll();
            if(students2.Count == 0)
            {
                Console.WriteLine("No student found.");
            }
            Console.WriteLine("Id\tFull Name\tClassroom".ConstructTableHeader());
            foreach (Student student in students2)
            {
                Console.WriteLine(student);
            }
            break;
        case 9:
            try
            {
                Console.Write("Enter Classroom Id: ");
                int classRoomId3 = int.Parse(Console.ReadLine());
                List<Student> students3 = classroomService.GetStudentsOfClassroom(classRoomId3);
                if (students3.Count == 0)
                {
                    Console.WriteLine("No student found in this classroom.");
                    break;
                }
                Console.WriteLine("Id\tFull Name\tClassroom".ConstructTableHeader());
                foreach (Student student in students3)
                {
                    Console.WriteLine(student);
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine("[9] Error: ", exception.Message);
            }
            
            break;
        case 10:
            List<Classroom> classrooms = classroomService.GetAll();
            if (classrooms.Count == 0)
            {
                Console.WriteLine("No classroom found.");
                break;
            }
            Console.WriteLine("Id\tName\tTeacher\t\tEnrolled Students".ConstructTableHeader());
            foreach (Classroom classroom in classrooms)
            {
                Console.WriteLine(classroom);
            }
            break;
        default:
            break;
    }

    Console.Write("> Enter your choice (Press any other key to quit): ");
    userInput = Console.ReadLine() ?? string.Empty;
}

void DisplayMenu()
{
    Console.WriteLine("Welcome to school management system!");
    Console.WriteLine("Menu:");
    Console.WriteLine("1- Add Classroom");
    Console.WriteLine("2- Add a New Teacher");
    Console.WriteLine("3- Add a New Student");
    Console.WriteLine("4- Search Teacher(s) by Full Name");
    Console.WriteLine("5- Search Student(s) by Full Name");
    Console.WriteLine("6- Search Student(s) in a Classroom by Full Name");
    Console.WriteLine("7- Get All Teachers");
    Console.WriteLine("8- Get All Students");
    Console.WriteLine("9- Get All Students in a Classroom");
    Console.WriteLine("10- Get All Classrooms");
    
}

bool IsTerminationInput(string input)
{
    int choice;
    if (int.TryParse(input, out choice))
    {
        return choice > 0 && choice < 11;
    }
    return false;
}
