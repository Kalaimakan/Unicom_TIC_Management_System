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
    class CourseController
    {
        public void addCourse(Course addCourse)
        {
            using (var connection =Db_Config.getConnection())
            {
                try
                {
                    using (SQLiteTransaction transaction = connection.BeginTransaction())
                    {
                        string addcourseQuary = @"INSERT INTO Courses (
                                            Course_Name, StartDate) VALUES (
                                            @courseName, @startDate);";
                        using (var courseCommand = new SQLiteCommand(addcourseQuary, connection))
                        {
                            courseCommand.Parameters.AddWithValue("@courseName", addCourse.Course_Name);
                            courseCommand.Parameters.AddWithValue("@startDate", addCourse.StartDate);
                            courseCommand.ExecuteNonQuery();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error while adding course {ex.Message}"  );
                }

            }
        }

        public void viewCourse(DataGridView dataGridView)
        {
            using (var connection =Db_Config.getConnection())
            {
                string viewCourseQuery = @"SELECT * FROM Courses";

                using (var cmd = new SQLiteCommand(viewCourseQuery, connection))
                {
                    using (SQLiteDataReader reader =cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int courseId=reader.GetInt32(0);
                            string courseName = reader.GetString(1);
                            string startDate = reader.GetString(2);
                            Course course = new Course
                            {
                                Course_Id = courseId,
                                Course_Name = courseName,
                                StartDate = startDate
                            };
                        }
                    }
                }



            }
        }
    }
}
