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
        public void AssignLecturerToStudents(Lecturer lecturer, SQLiteConnection connection, SQLiteTransaction transaction)
        {
            try
            {
                // First, get all students who are studying the subject this lecturer teaches
                string getStudentsQuery = @"SELECT s.Student_Id, sub.Subject_Name 
                                   FROM Students s
                                   JOIN Subject_Students ss ON s.Student_Id = ss.Student_Id
                                   JOIN Subjects sub ON ss.Subject_Id = sub.Subject_Id
                                   WHERE sub.Subject_Id = @subjectId";

                List<Student> students = new List<Student>();

                using (var command = new SQLiteCommand(getStudentsQuery, connection, transaction))
                {
                    command.Parameters.AddWithValue("@subjectId", lecturer.Subject_Id);

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            students.Add(new Student
                            {
                                Student_Id = Convert.ToInt32(reader["Student_Id"]),
                            });
                        }
                    }
                }

                // Now assign the lecturer to each student for this subject
                string assignQuery = @"INSERT INTO Lecturer_Students(
                             Lecturer_id, Subject_Name, Student_id) 
                             VALUES(@lecturerId, @subjectName, @studentId)";

                foreach (var student in students)
                {
                    using (var assignCommand = new SQLiteCommand(assignQuery, connection, transaction))
                    {
                        assignCommand.Parameters.AddWithValue("@lecturerId", lecturer.Lecturer_Id);
                        assignCommand.Parameters.AddWithValue("@subjectName", lecturer.Subject_Name);
                        assignCommand.Parameters.AddWithValue("@studentId", student.Student_Id);
                        int rowsAffected = assignCommand.ExecuteNonQuery();

                        if (rowsAffected == 0)
                        {
                            throw new Exception("Failed to insert into Lecturer_Students table");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error assigning lecturer to students: " + ex.Message);
                throw;
            }
        }
    }
}
