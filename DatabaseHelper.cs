using System;
using MySql.Data.MySqlClient;

public class DatabaseHelper
{
    private static string connectionString = "Server=localhost;Database=GradingSystem;Uid=root;Pwd=;";

    public static void AddStudent(int id, string name, double quiz, double assignment, double exam)
    {
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            string query = "INSERT INTO Student (Student_ID, Student_Name, Quiz_Score, Assignment_Score, Exam_Score) VALUES (@id, @name, @quiz, @assignment, @exam)";
            MySqlCommand command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@id", id);
            command.Parameters.AddWithValue("@name", name);
            command.Parameters.AddWithValue("@quiz", quiz);
            command.Parameters.AddWithValue("@assignment", assignment);
            command.Parameters.AddWithValue("@exam", exam);

            connection.Open();
            command.ExecuteNonQuery();
        }
    }

    public static void AddGrade(int gradeId, int studentId, int courseId, float gradeScore, string gradeValue)
    {
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            string query = "INSERT INTO Grade (Grade_ID, Student_ID, Course_ID, Grade_Score, Grade_Value) VALUES (@gradeId, @studentId, @courseId, @gradeScore, @gradeValue)";
            MySqlCommand command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@gradeId", gradeId);
            command.Parameters.AddWithValue("@studentId", studentId);
            command.Parameters.AddWithValue("@courseId", courseId);
            command.Parameters.AddWithValue("@gradeScore", gradeScore);
            command.Parameters.AddWithValue("@gradeValue", gradeValue);

            connection.Open();
            command.ExecuteNonQuery();
        }
    }

    public static void EditStudent(int id, double quiz, double assignment, double exam)
    {
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            string query = "UPDATE Student SET Quiz_Score = @quiz, Assignment_Score = @assignment, Exam_Score = @exam WHERE Student_ID = @id";
            MySqlCommand command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@id", id);
            command.Parameters.AddWithValue("@quiz", quiz);
            command.Parameters.AddWithValue("@assignment", assignment);
            command.Parameters.AddWithValue("@exam", exam);

            connection.Open();
            command.ExecuteNonQuery();
        }
    }

    public static void DeleteStudent(int id)
    {
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            string query = "DELETE FROM Student WHERE Student_ID = @id";
            MySqlCommand command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@id", id);

            connection.Open();
            command.ExecuteNonQuery();
        }
    }

    public static void ComputeGrades()
    {
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            string query = "SELECT Student_ID, Quiz_Score, Assignment_Score, Exam_Score FROM Student";
            MySqlCommand command = new MySqlCommand(query, connection);

            connection.Open();
            MySqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                int id = Convert.ToInt32(reader["Student_ID"]);
                double quiz = Convert.ToDouble(reader["Quiz_Score"]);
                double assignment = Convert.ToDouble(reader["Assignment_Score"]);
                double exam = Convert.ToDouble(reader["Exam_Score"]);

                Student student = new Student
                {
                    ID = id,
                    QuizScore = quiz,
                    AssignmentScore = assignment,
                    ExamScore = exam
                };

                double finalGrade = student.ComputeFinalGrade();
                string gradeEquivalent = student.GenerateRemarks();

                Console.WriteLine($"Student ID: {id}, Final Percentage: {finalGrade}%, Grade: {gradeEquivalent}");
            }
        }
    }


    public static void ViewAllRecords()
    {
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            string query = "SELECT * FROM Student";
            MySqlCommand command = new MySqlCommand(query, connection);

            connection.Open();
            MySqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                Console.WriteLine($"ID: {reader["Student_ID"]}, Name: {reader["Student_Name"]}, Quiz: {reader["Quiz_Score"]}, Assignment: {reader["Assignment_Score"]}, Exam: {reader["Exam_Score"]}");
            }
        }
    }
}
