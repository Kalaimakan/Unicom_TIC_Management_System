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
    class StudentController
    {
        public void createStudent(Student registerStudent, User registerUser)
        {
            try
            {
                using (var connection = Db_Config.getConnection())
                {
                    using (SQLiteTransaction transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            UserController userController = new UserController();
                            int userId = userController.createUser(registerUser, connection, transaction);
                            string studentQuary = @"INSERT INTO Students(
                                                User_Id, First_Name, Last_Name, Email, Gender, PhoneNumber, Date_of_Birth, Address, Entrolled_Course)VALUES(
                                                @userId, @firstName, @lastName, @email, @gender, @phoneNumber, @dateofBirth, @address, @entrolledCourse
                                              );";
                            using (SQLiteCommand studentCommand = new SQLiteCommand(studentQuary, connection))
                            {
                                studentCommand.Parameters.AddWithValue("@userId", userId);
                                studentCommand.Parameters.AddWithValue("@firstName", registerStudent.First_Name);
                                studentCommand.Parameters.AddWithValue("@lastName", registerStudent.Last_Name);
                                studentCommand.Parameters.AddWithValue("@email", registerStudent.Email);
                                studentCommand.Parameters.AddWithValue("@gender", registerStudent.Gender);
                                studentCommand.Parameters.AddWithValue("@phoneNumber", registerStudent.PhoneNumber);
                                studentCommand.Parameters.AddWithValue("@dateofBirth", registerStudent.Date_of_Birth);
                                studentCommand.Parameters.AddWithValue("@address", registerStudent.Address);
                                studentCommand.Parameters.AddWithValue("@entrolledCourse", registerStudent.Entrolld_Course);
                                studentCommand.ExecuteNonQuery();
                            }
                            MessageBox.Show($"Mr/mrs.{registerStudent.Last_Name} registered successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            transaction.Commit();
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            MessageBox.Show($"Error while creating the student: {ex.Message}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error occurred while registering the Student: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }
    }
}
