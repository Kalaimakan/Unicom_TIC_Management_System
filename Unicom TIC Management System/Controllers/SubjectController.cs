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
    class SubjectController
    {
        public int AddSubject(Subject addsubject, Course addCourse, Department addDepartment, SQLiteConnection connection, SQLiteTransaction transaction)
        {
            string insertQuery = @"INSERT INTO subjects (Course_Id, Subject_Name, Department_Id) 
                            VALUES (@courseId, @subjectName, @departmentId);";

            using (var command = new SQLiteCommand(insertQuery, connection, transaction))
            {
                command.Parameters.AddWithValue("@courseId", addCourse.Course_Id);
                command.Parameters.AddWithValue("@subjectName", addsubject.Subject_Name);
                command.Parameters.AddWithValue("@departmentId", addDepartment.Id);
                command.ExecuteNonQuery();
            }

            int subjectId;
            using (var command = new SQLiteCommand("SELECT last_insert_rowid()", connection, transaction))
            {
                subjectId = Convert.ToInt32(command.ExecuteScalar());
            }

            return subjectId;
        }

        public List<Subject> GetSubjects()
        {
            List<Subject> subjects = new List<Subject>();
            using (var connection = Db_Config.getConnection())
            {
                string query = @"SELECT s.Subject_Id,s.Subject_Name,c.Course_Id,c.Course_Name,d.Id,d.Department_Name 
                                FROM subjects s
                                JOIN Courses c ON s.Course_Id=c.Course_Id
                                JOIN Departments d ON s.Department_Id=d.Id";
                using (var command = new SQLiteCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            subjects.Add(new Subject
                            {
                                Subject_Id = reader.GetInt32(0),
                                Subject_Name = reader.GetString(1),
                                Course_Id = reader.GetInt32(2),
                                Course_Name = reader.GetString(3),
                                Department_Id = reader.GetInt32(4),
                                Department_Name = reader.GetString(5)
                            });
                        }
                    }
                }
            }
            return subjects;
        }

        //Delete subject by ID
        public void DeleteSubject(int subjectId)
        {
            using (var connection = Db_Config.getConnection())
            {
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        string deleteQuery = @"DELETE FROM subjects WHERE Subject_Id = @subjectId";
                        using (var command = new SQLiteCommand(deleteQuery, connection, transaction))
                        {
                            command.Parameters.AddWithValue("@subjectId", subjectId);
                            command.ExecuteNonQuery();
                        }
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        Console.WriteLine("Error deleting subject: " + ex.Message);
                        throw;
                    }
                }
            }
        }
    }
}
