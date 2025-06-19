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
    class DepartmentController
    {
        //Creaate Department.
        public void CreateDepartment(Department department)
        {
            using (var connection = Db_Config.getConnection())
            {
                using (SQLiteTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        string departmentQuary = "INSERT INTO Departments (Department_Name) VALUES (@departmentName)";
                        using (SQLiteCommand command = new SQLiteCommand(departmentQuary, connection))
                        {
                            command.Parameters.AddWithValue("@departmentName", department.Department_Name);
                            command.ExecuteNonQuery();
                        }
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        MessageBox.Show("Error creating department: " + ex.Message);
                    }
                }
            }
        }

        //Get All Departments.
        public List<Department> GetAllDepartment()
        {
            List<Department> departments = new List<Department>();
            using (var connection = Db_Config.getConnection())
            {
                string query = "SELECT * FROM Departments";
                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            departments.Add(new Department
                            {
                                Id = reader.GetInt32(0),
                                Department_Name = reader.GetString(1)
                            });
                        }
                    }
                }
            }
            return departments;
        }

        //Delete Department.
        public void DeleteDepartment(int Id)
        {
            using (var connection = Db_Config.getConnection())
            {
                string deleteQuery = "DELETE FROM Departments WHERE Id = @Id";
                using (SQLiteCommand command = new SQLiteCommand(deleteQuery, connection))
                {
                    command.Parameters.AddWithValue("@Id", Id);
                    //try
                    //{
                        command.ExecuteNonQuery();
                        MessageBox.Show("Department deleted successfully.");
                    //}
                    //catch (Exception ex)
                    //{
                    //    MessageBox.Show("Error deleting department: " + ex.Message);
                    //}
                }
            }
        }
    }
}
