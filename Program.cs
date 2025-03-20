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
            Console.WriteLine("2. Edit Student Record");
            Console.WriteLine("3. Delete Student Record");
            Console.WriteLine("4. Compute Final Grades");
            Console.WriteLine("5. View All Records");
            Console.WriteLine("6. Exit");

            Console.Write("Enter your choice: ");
            int choice = int.Parse(Console.ReadLine() ?? "0");

            switch (choice)
            {
                case 1:
                    AddStudent();
                    break;
                case 2:
                    EditStudent();
                    break;
                case 3:
                    DeleteStudent();
                    break;
                case 4:
                    ComputeGrades();
                    break;
                case 5:
                    ViewAllRecords();
                    break;
                case 6:
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

        Console.Write("Enter Quiz Score: ");
        double quiz = double.Parse(Console.ReadLine());

        Console.Write("Enter Assignment Score: ");
        double assignment = double.Parse(Console.ReadLine());

        Console.Write("Enter Exam Score: ");
        double exam = double.Parse(Console.ReadLine());

        DatabaseHelper.AddStudent(id, name, quiz, assignment, exam);

        Console.WriteLine("Student record added successfully. Press any key to continue...");
        Console.ReadKey();
    }

    static void EditStudent()
    {
        Console.Write("Enter Student ID to Edit: ");
        int id = int.Parse(Console.ReadLine());

        Console.Write("Enter New Quiz Score: ");
        double quiz = double.Parse(Console.ReadLine());

        Console.Write("Enter New Assignment Score: ");
        double assignment = double.Parse(Console.ReadLine());

        Console.Write("Enter New Exam Score: ");
        double exam = double.Parse(Console.ReadLine());

        DatabaseHelper.EditStudent(id, quiz, assignment, exam);
        Console.WriteLine("Student record updated successfully. Press any key to continue...");
        Console.ReadKey();
    }

    static void DeleteStudent()
    {
        Console.Write("Enter Student ID to Delete: ");
        int id = int.Parse(Console.ReadLine());

        DatabaseHelper.DeleteStudent(id);
        Console.WriteLine("Student record deleted successfully. Press any key to continue...");
        Console.ReadKey();
    }

    static void ComputeGrades()
    {
        DatabaseHelper.ComputeGrades();
        Console.WriteLine("Final grades computed successfully. Press any key to continue...");
        Console.ReadKey();
    }

    static void ViewAllRecords()
    {
        DatabaseHelper.ViewAllRecords();
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }
}
