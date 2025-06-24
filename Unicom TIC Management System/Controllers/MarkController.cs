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
    class MarkController
    {
        public void AddMark(Mark mark)
        {
            using (var connection = Db_Config.getConnection())
            {
                string query = "INSERT INTO Marks (Student_Id, Exam_Id, Marks_Value ,Subject_Id) VALUES (@StudentId, @ExamId, @MarkValue, @subjectId)";
                using (var command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@StudentId", mark.Student_Id);
                    command.Parameters.AddWithValue("@ExamId", mark.Exam_Id);
                    command.Parameters.AddWithValue("@MarkValue", mark.Marks_Value);
                    command.Parameters.AddWithValue("@subjectId", mark.Subject_Id);
                    command.ExecuteNonQuery();
                }
            }
        }

        //get all marks
        public List<Mark> GetMarks()
        {
            List<Mark> marks = new List<Mark>();
            using (var connection = Db_Config.getConnection())
            {
                string query = "SELECT * FROM Marks";
                using (var command = new SQLiteCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            marks.Add( new Mark()
                            {
                                Mark_Id = reader.GetInt32(0),
                                Marks_Value = reader.GetInt32(1),
                                Student_Id = reader.GetInt32(2),
                                Exam_Id = reader.GetInt32(3),
                                Subject_Id = reader.GetInt32(4)
                            });
                        }
                    }
                }
                return marks;
            }
        }

        //Update a mark
        public void UpdateMark(Mark mark)
        {
            using (var connection = Db_Config.getConnection())
            {
                string query = "UPDATE Marks SET Student_Id = @StudentId, Exam_Id = @ExamId, Subject_Id = @SubjectId, Marks_Value = @MarkValue WHERE Mark_Id = @MarkId";
                using (var command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@StudentId", mark.Student_Id);
                    command.Parameters.AddWithValue("@ExamId", mark.Exam_Id);
                    command.Parameters.AddWithValue("@SubjectId", mark.Subject_Id);
                    command.Parameters.AddWithValue("@MarkValue", mark.Marks_Value);
                    command.Parameters.AddWithValue("@MarkId", mark.Mark_Id);

                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected == 0)
                    {
                        throw new Exception("Update failed. No matching record found.");
                    }
                }
            }
        }
    }
}
