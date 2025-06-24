using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unicom_TIC_Management_System.Models;
using Unicom_TIC_Management_System.Repositories;

namespace Unicom_TIC_Management_System.Controllers
{
    class ExamController
    {
        //Create Exam
        public void CreateExam(Exam exam)
        {
            using (var connection = Db_Config.getConnection())
            {
                string quary = "INSERT INTO Exams (Exam_Name, Exam_Type, Exam_Date, Course_Id, Subject_Id) VALUES (@ExamName, @ExamType, @ExamDate, @CourseId, @SubjectId)";
                using (var command = new SQLiteCommand(quary, connection))
                {
                    command.Parameters.AddWithValue("@ExamName", exam.Exam_Name);
                    command.Parameters.AddWithValue("@ExamType", exam.Exam_Type);
                    command.Parameters.AddWithValue("@ExamDate", exam.Exam_Date);
                    command.Parameters.AddWithValue("@CourseId", exam.Course_Id);
                    command.Parameters.AddWithValue("@SubjectId", exam.Subject_Id);
                    command.ExecuteNonQuery();
                }
            }
        }

        // View Exam
        public List<Exam> ViewExams()
        {
            List<Exam> exams = new List<Exam>();
            using (var connection = Db_Config.getConnection())
            {
                string query = @"
            SELECT 
                e.Exam_Id,
                e.Exam_Name,
                e.Exam_Type,
                e.Exam_Date,
                s.Subject_Id,
                s.Subject_Name,
                c.Course_Id,
                c.Course_Name
            FROM Exams e
            JOIN Subjects s ON e.Subject_Id = s.Subject_Id
            JOIN Courses c ON e.Course_Id = c.Course_Id";
                using (var command = new SQLiteCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            exams.Add(new Exam
                            {
                                Exam_Id = Convert.ToInt32(reader["Exam_Id"]),
                                Exam_Name = reader["Exam_Name"].ToString(),
                                Exam_Type = reader["Exam_Type"].ToString(),
                                Exam_Date = reader["Exam_Date"].ToString(),
                                Subject_Id = Convert.ToInt32(reader["Subject_Id"]),
                                Course_Id = Convert.ToInt32(reader["Course_Id"]),
                                Subject_Name = reader["Subject_Name"].ToString(),
                                Course_Name = reader["Course_Name"].ToString()
                            });
                        }
                    }
                }
            }
            return exams;
        }

        // Update Exam
        public void UpdateExam(Exam exam)
        {
            using (var connection = Db_Config.getConnection())
            {
                string query = "UPDATE Exams SET Exam_Name = @ExamName, Exam_Type = @ExamType, Exam_Date = @ExamDate, Course_Id = @CourseId, Subject_Id = @SubjectId WHERE Exam_Id = @ExamId";
                using (var command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ExamName", exam.Exam_Name);
                    command.Parameters.AddWithValue("@ExamType", exam.Exam_Type);
                    command.Parameters.AddWithValue("@ExamDate", exam.Exam_Date);
                    command.Parameters.AddWithValue("@CourseId", exam.Course_Id);
                    command.Parameters.AddWithValue("@SubjectId", exam.Subject_Id);
                    command.Parameters.AddWithValue("@ExamId", exam.Exam_Id);
                    command.ExecuteNonQuery();
                }
            }
        }

        //Delete Exam
        public void DeleteExam(int examId)
        {
            using (var connection = Db_Config.getConnection())
            {
                string query = "DELETE FROM Exams WHERE Exam_Id = @ExamId";
                using (var command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ExamId", examId);
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
