using System;

class Program
{
    static void Main(string[] args)
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("=== College Grading System ===");
            Console.WriteLine("1. Add Student Record");
            Console.WriteLine("2. View Available Courses");
            Console.WriteLine("3. Add Course");
            Console.WriteLine("4. Assign Course to Student");
            Console.WriteLine("5. Add Detailed Grades for a Student");
            Console.WriteLine("6. View Grades for a Student");
            Console.WriteLine("7. Exit");
            Console.WriteLine("8. View All Students with Overall Grades");


            Console.Write("Enter your choice: ");
            int choice = int.Parse(Console.ReadLine() ?? "0");

            switch (choice)
            {
                case 1:
                    AddStudent();
                    break;
                case 2:
                    ViewCourses();
                    break;
                case 3:
                    AddCourse();
                    break;
                case 4:
                    AssignCourseToStudent();
                    break;
                case 5:
                    AddDetailedGradeForStudent();
                    break;
                case 6:
                    ViewStudentGrades();
                    break;
                case 7:
                    ViewAllStudentsWithGrades();
                    break;
                case 8:
                    return;
                default:
                    Console.WriteLine("Invalid choice. Press any key to continue...");
                    Console.ReadKey();
                    break;
            }
        }
    }

    static void AddStudent()
    {
        Console.Write("Enter Student ID: ");
        int id = int.Parse(Console.ReadLine());

        Console.Write("Enter Student Name: ");
        string name = Console.ReadLine();

        Console.Write("Enter Student Email (optional): ");
        string email = Console.ReadLine();

        DatabaseHelper.AddStudent(id, name, email);

        Console.WriteLine("Student added successfully! Press any key to continue...");
        Console.ReadKey();
    }

    static void ViewCourses()
    {
        Console.Clear();
        DatabaseHelper.ViewAvailableCourses();
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }

    static void AddCourse()
    {
        Console.Write("Enter Course Name: ");
        string courseName = Console.ReadLine();

        Console.Write("Enter Course Code: ");
        string courseCode = Console.ReadLine();

        DatabaseHelper.AddCourse(courseName, courseCode);
        Console.WriteLine("Course added successfully. Press any key to continue...");
        Console.ReadKey();
    }

    static void AssignCourseToStudent()
    {
        Console.Write("Enter Student ID: ");
        int studentId = int.Parse(Console.ReadLine());

        Console.Write("Enter Course ID: ");
        int courseId = int.Parse(Console.ReadLine());

        DatabaseHelper.AddStudentToCourse(studentId, courseId);
        Console.WriteLine("Course assigned to student successfully. Press any key to continue...");
        Console.ReadKey();
    }

    static void AddDetailedGradeForStudent()
    {
        Console.Write("Enter Student ID: ");
        int studentId = int.Parse(Console.ReadLine());

        Console.Write("Enter Course ID: ");
        int courseId = int.Parse(Console.ReadLine());

        Console.Write("Enter Quiz Score: ");
        double quizScore = double.Parse(Console.ReadLine());

        Console.Write("Enter Activity Score: ");
        double activityScore = double.Parse(Console.ReadLine());

        Console.Write("Enter Exam Score: ");
        double examScore = double.Parse(Console.ReadLine());

        DatabaseHelper.AddDetailedGrade(studentId, courseId, quizScore, activityScore, examScore);

        Console.WriteLine("Detailed grades added successfully. Press any key to continue...");
        Console.ReadKey();
    }

    static void ViewStudentGrades()
    {
        Console.Write("Enter Student ID: ");
        int studentId = int.Parse(Console.ReadLine());

        DatabaseHelper.ViewStudentGrades(studentId);
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }
    static void ViewAllStudentsWithGrades()
    {
        DatabaseHelper.ViewAllStudentsWithGrades();
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }

}