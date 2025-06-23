using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Unicom_TIC_Management_System.Models;
using Unicom_TIC_Management_System.Repositories;

namespace Unicom_TIC_Management_System.Controllers
{
    class Lecturer_StudentController
    {
        public void AssignLecturerToStudent(Lecturer lecturer, Student student, Subject subject, SQLiteConnection connection, SQLiteTransaction transaction)
        {
            try
            {
                string assignQuery = @"INSERT INTO Lecturer_Students(
                         Lecturer_id, Subject_Name, Student_id) 
                         VALUES(@lecturerId, @subjectName, @studentId)";

                using (var assignCommand = new SQLiteCommand(assignQuery, connection, transaction))
                {
                    assignCommand.Parameters.AddWithValue("@lecturerId", lecturer.Lecturer_Id);
                    assignCommand.Parameters.AddWithValue("@subjectName", subject.Subject_Name);
                    assignCommand.Parameters.AddWithValue("@studentId", student.Student_Id);
                    int rowsAffected = assignCommand.ExecuteNonQuery();

                    if (rowsAffected == 0)
                    {
                        throw new Exception("Failed to insert into Lecturer_Students table");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error assigning lecturer to student: " + ex.Message);
                throw;
            }
        }
    }
}
