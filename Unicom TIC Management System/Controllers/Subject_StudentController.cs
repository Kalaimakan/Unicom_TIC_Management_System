using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unicom_TIC_Management_System.Models;

namespace Unicom_TIC_Management_System.Controllers
{
    class Subject_StudentController
    {
        public void AssignSubjectsToStudent(Subject subject, Student student, SQLiteConnection connection, SQLiteTransaction transaction)
        {

            string insertSubjectStudentQuery = @"INSERT INTO Subject_Students (Subject_Id, Subject_Name, Student_Id)
                                                     VALUES (@subjectId, @subjectName, @studentId)";
            using (var insertCmd = new SQLiteCommand(insertSubjectStudentQuery, connection, transaction))
            {
                insertCmd.Parameters.AddWithValue("@subjectId", subject.Subject_Id);
                insertCmd.Parameters.AddWithValue("@subjectName", subject.Subject_Name);
                insertCmd.Parameters.AddWithValue("@studentId", student.Student_Id);
                insertCmd.ExecuteNonQuery();
            }
        }
    }
}
