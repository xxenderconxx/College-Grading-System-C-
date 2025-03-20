public class Student
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public double QuizScore { get; set; }
        public double AssignmentScore { get; set; }
        public double ExamScore { get; set; }

        public double ComputeFinalGrade()
    {
        // Weighted average: Quiz (30%), Assignment (30%), Exam (40%)
        double finalPercentage = (QuizScore * 0.3) + (AssignmentScore * 0.3) + (ExamScore * 0.4);

        // Convert percentage to 2 decimal places
        return Math.Round(finalPercentage, 2);
    }

    public string GenerateRemarks()
    {
        double percentage = ComputeFinalGrade();
        string grade;

        // Convert percentage to 1.0–5.0 scale
        if (percentage >= 96) grade = "1.0 (Excellent)";
        else if (percentage >= 91) grade = "1.25 (Very Good)";
        else if (percentage >= 86) grade = "1.5 (Very Good)";
        else if (percentage >= 81) grade = "1.75 (Good)";
        else if (percentage >= 76) grade = "2.0 (Good)";
        else if (percentage >= 71) grade = "2.25 (Satisfactory)";
        else if (percentage >= 66) grade = "2.5 (Satisfactory)";
        else if (percentage >= 61) grade = "2.75 (Passing)";
        else if (percentage >= 51) grade = "3.0 (Passing)";
        else grade = "5.0 (Failing)";

        return grade;
    }

}
