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
    class Lecturer_StudentController
    {
        public void AddLecturertoStudent(Lecturer lecturer,Student student)
        {
            try
            {
                using (var connection = Db_Config.getConnection())
                {
                    string query = @"INSERT INTO Lecturer_Student(LecturerId, Subject_Name, StudentId) 
                                     VALUES(@lecturerId, @subjectName, @studentId)";
                    using (var command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@lecturerId", lecturer.Lecturer_Id);
                        command.Parameters.AddWithValue("@subjectName", lecturer.Subject_Name);
                        command.Parameters.AddWithValue("@studentId", student.Student_Id);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error adding lecturer to student: " + ex.Message);
            }
        }
    }
}
