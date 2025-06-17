using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Unicom_TIC_Management_System.Models;
using Unicom_TIC_Management_System.Repositories;

namespace Unicom_TIC_Management_System.Controllers
{
    class CourseController : Validation
    {
        public (bool isValid, string errorMessage) ValidateCourse(Course course)
        {
            var courseNameValidate = validateCourseName(course.Course_Name, "Course");
            if (!courseNameValidate.isValid)
                return courseNameValidate;

            if (!DateTime.TryParse(course.StartDate, out DateTime startDate))
            {
                return (false, "Invalid Start Date format.");
            }
            var startDateValidate = validateStartDate(startDate);
            if (!startDateValidate.isValid)
                return startDateValidate;

            return (true, string.Empty);
        }
        //Add Course.
        public void addCourse(Course addCourse)
        {
            using (var connection = Db_Config.getConnection())
            {
                try
                {
                    using (SQLiteTransaction transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            string addcourseQuary = @"INSERT INTO Courses (
                                                   Course_Name, StartDate, Department) VALUES (
                                                   @courseName, @startDate, @deparment);";
                            using (var courseCommand = new SQLiteCommand(addcourseQuary, connection))
                            {
                                courseCommand.Parameters.AddWithValue("@courseName", addCourse.Course_Name);
                                courseCommand.Parameters.AddWithValue("@startDate", addCourse.StartDate);
                                courseCommand.Parameters.AddWithValue("@deparment", addCourse.Department);
                                courseCommand.ExecuteNonQuery();
                            }
                            MessageBox.Show($"{addCourse.Course_Name} Added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            transaction.Commit();
                        }
                        catch (SQLiteException ex)
                        {
                            transaction.Rollback();
                            MessageBox.Show($"Error while adding course: {ex.Message}");
                            return;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred while registering the user: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;

                }
            }
        }

        //View Course.
        public List<Course> viewCourse()
        {
            List<Course> courses = new List<Course>();
            using (var connection = Db_Config.getConnection())
            {
                string viewCourseQuery = @"SELECT * FROM Courses";

                using (var cmd = new SQLiteCommand(viewCourseQuery, connection))
                {
                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            courses.Add(new Course
                            {
                                Course_Id = reader.GetInt32(0),
                                Course_Name = reader.GetString(1),
                                Department = reader.GetString(2),
                                StartDate = reader.GetString(3)
                            });
                        }
                    }
                }
            }
            return courses;
        }

        //Delete Course.
        public void deleteCourse(int courseId)
        {
            using (var connection = Db_Config.getConnection())
            {
                string deleteQuary = "DELETE FROM Courses WHERE Course_Id = @courseId";
                using (var deleteCommand=new SQLiteCommand(deleteQuary,connection))
                {
                    deleteCommand.Parameters.AddWithValue("@courseId", courseId);
                    deleteCommand.ExecuteNonQuery();
                }
            }
        }
    }
}
