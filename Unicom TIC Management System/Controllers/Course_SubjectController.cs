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
    class Course_SubjectController
    {
        public void AddCourseSubject(Course addCourse, Subject subject, Department department)
        {
            using (var connection = Db_Config.getConnection())
            {
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        var subjectController = new SubjectController();
                        int subjectId = subjectController.AddSubject(subject, addCourse, department, connection, transaction);

                        string addCourseSubjectQuery = @"INSERT INTO Course_Subjects (Course_Id, Subject_Id) 
                                                 VALUES (@courseId, @subjectId)";
                        using (var command = new SQLiteCommand(addCourseSubjectQuery, connection, transaction))
                        {
                            command.Parameters.AddWithValue("@courseId", addCourse.Course_Id);
                            command.Parameters.AddWithValue("@subjectId", subjectId);
                            command.ExecuteNonQuery();
                        }

                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        Console.WriteLine("Error adding course-subject relation: " + ex.Message);
                        throw;
                    }
                }
            }
        }
    }
}
