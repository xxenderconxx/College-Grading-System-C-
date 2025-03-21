using System;
using MySql.Data.MySqlClient;

public class DatabaseHelper
{
    private static string connectionString = "Server=localhost;Database=GradingSystem;Uid=root;Pwd=;";

    // Add a new student
    public static void AddStudent(int id, string name, string email)
    {
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            string query = "INSERT INTO Student (Student_ID, Student_Name, Student_Email) VALUES (@id, @name, @Email)";
            MySqlCommand command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@id", id);
            command.Parameters.AddWithValue("@name", name);
            command.Parameters.AddWithValue("@Email", email);

            connection.Open();
            command.ExecuteNonQuery();
        }
    }

    // Add a new course
    public static void AddCourse(string courseName, string courseCode)
    {
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            string query = "INSERT INTO Course (Course_Name, Course_Code) VALUES (@courseName, @courseCode)";
            MySqlCommand command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@courseName", courseName);
            command.Parameters.AddWithValue("@courseCode", courseCode);

            connection.Open();
            command.ExecuteNonQuery();
        }
    }

    // View all available courses
    public static void ViewAvailableCourses()
    {
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            string query = "SELECT * FROM Course";
            MySqlCommand command = new MySqlCommand(query, connection);

            connection.Open();
            MySqlDataReader reader = command.ExecuteReader();

            Console.WriteLine("Available Courses:");
            while (reader.Read())
            {
                Console.WriteLine($"ID: {reader["Course_ID"]}, Name: {reader["Course_Name"]}, Code: {reader["Course_Code"]}");
            }
        }
    }

    // Assign a student to a course
    public static void AddStudentToCourse(int studentId, int courseId)
    {
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            string query = "INSERT INTO Grade (Student_ID, Course_ID) VALUES (@studentId, @courseId)";
            MySqlCommand command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@studentId", studentId);
            command.Parameters.AddWithValue("@courseId", courseId);

            connection.Open();
            command.ExecuteNonQuery();
        }
    }

    // Add grades for a student in a specific course
    public static void AddDetailedGrade(int studentId, int courseId, double quizScore, double activityScore, double examScore)
    {
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            string query = @"
                UPDATE Grade 
                SET Quiz_Score = @quizScore, Activity_Score = @activityScore, Exam_Score = @examScore, 
                    Grade_Score = @finalScore, Grade_Value = @gradeValue
                WHERE Student_ID = @studentId AND Course_ID = @courseId";

            // Calculate the final grade (e.g., 30% Quiz, 30% Activity, 40% Exam)
            double finalScore = (quizScore * 0.3) + (activityScore * 0.3) + (examScore * 0.4);
            string gradeValue = GenerateGradeValue(finalScore);

            MySqlCommand command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@studentId", studentId);
            command.Parameters.AddWithValue("@courseId", courseId);
            command.Parameters.AddWithValue("@quizScore", quizScore);
            command.Parameters.AddWithValue("@activityScore", activityScore);
            command.Parameters.AddWithValue("@examScore", examScore);
            command.Parameters.AddWithValue("@finalScore", finalScore);
            command.Parameters.AddWithValue("@gradeValue", gradeValue);

            connection.Open();
            command.ExecuteNonQuery();
        }
    }

    private static string GenerateGradeValue(double finalScore)
    {
        if (finalScore >= 96) return "1.0 (Excellent)";
        if (finalScore >= 91) return "1.25 (Very Good)";
        if (finalScore >= 86) return "1.5 (Very Good)";
        if (finalScore >= 81) return "1.75 (Good)";
        if (finalScore >= 76) return "2.0 (Good)";
        if (finalScore >= 71) return "2.25 (Satisfactory)";
        if (finalScore >= 66) return "2.5 (Satisfactory)";
        if (finalScore >= 61) return "2.75 (Passing)";
        if (finalScore >= 51) return "3.0 (Passing)";
        return "5.0 (Failing)";
    }

    // View grades for a student in all courses
    public static void ViewStudentGrades(int studentId)
    {
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            string query = @"
                SELECT c.Course_Name, c.Course_Code, g.Quiz_Score, g.Activity_Score, g.Exam_Score, g.Grade_Score, g.Grade_Value
                FROM Grade g
                INNER JOIN Course c ON g.Course_ID = c.Course_ID
                WHERE g.Student_ID = @studentId";

            MySqlCommand command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@studentId", studentId);

            connection.Open();
            MySqlDataReader reader = command.ExecuteReader();

            double totalScore = 0;
            int courseCount = 0;

            Console.WriteLine($"Grades for Student ID: {studentId}");
            Console.WriteLine("-------------------------------------------------");

            while (reader.Read())
            {
                string courseName = reader["Course_Name"].ToString();
                string courseCode = reader["Course_Code"].ToString();
                double quizScore = Convert.ToDouble(reader["Quiz_Score"]);
                double activityScore = Convert.ToDouble(reader["Activity_Score"]);
                double examScore = Convert.ToDouble(reader["Exam_Score"]);
                double finalScore = Convert.ToDouble(reader["Grade_Score"]);
                string gradeValue = reader["Grade_Value"].ToString();

                Console.WriteLine($"Course: {courseName} ({courseCode})");
                Console.WriteLine($" - Quiz Score: {quizScore:F2}");
                Console.WriteLine($" - Activity Score: {activityScore:F2}");
                Console.WriteLine($" - Exam Score: {examScore:F2}");
                Console.WriteLine($" - Final Score: {finalScore:F2}");
                Console.WriteLine($" - Grade Value: {gradeValue}");
                Console.WriteLine("-------------------------------------------------");

                totalScore += finalScore;
                courseCount++;
            }

            if (courseCount > 0)
            {
                double overallAverage = totalScore / courseCount;
                Console.WriteLine($"Overall Average Across Assigned Courses: {overallAverage:F2}");
            }
            else
            {
                Console.WriteLine("No courses assigned yet.");
            }
        }
    }

    public static void ViewAllStudentsWithGrades()
    {
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            string query = @"
                SELECT s.Student_ID, s.Student_Name, 
                    AVG(g.Grade_Score) AS Overall_Average
                FROM Student s
                LEFT JOIN Grade g ON s.Student_ID = g.Student_ID
                GROUP BY s.Student_ID, s.Student_Name";

            MySqlCommand command = new MySqlCommand(query, connection);

            connection.Open();
            MySqlDataReader reader = command.ExecuteReader();

            Console.WriteLine("All Students with Overall Grades:");
            Console.WriteLine("---------------------------------------------------");

            while (reader.Read())
            {
                int studentId = Convert.ToInt32(reader["Student_ID"]);
                string studentName = reader["Student_Name"].ToString();
                double overallAverage = reader["Overall_Average"] != DBNull.Value
                                        ? Convert.ToDouble(reader["Overall_Average"])
                                        : 0;

                // Convert Overall Average to Grade Value
                string gradeValue = GenerateGradeValue(overallAverage);

                Console.WriteLine($"ID: {studentId}, Name: {studentName}, Overall Grade Value: {gradeValue}");
            }

            Console.WriteLine("---------------------------------------------------");
        }
    }
}
