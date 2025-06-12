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
    class LecturerController
    {
        public void createLecture(User registerUser, Lecturer registerLecturer)
        {
            try
            {
                using (var connection = Db_Config.getConnection())
                {
                    using (SQLiteTransaction transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            string lecturerQuary = @"INSERT INTO Lecturers(
                                               User_Id, First_Name, Last_Name, Email, PhoneNumber, Date_of_Birth, Gender, salary) VALUES (
                                               @userId, @firstName, @lastName, @email, @phoneNumber, @dateOfBirth, @gender, @salary );";
                            UserController userController = new UserController();
                            int userId = userController.createUser(registerUser, connection, transaction);
                            using (var lecturerCommand = new SQLiteCommand(lecturerQuary, connection))
                            {
                                lecturerCommand.Parameters.AddWithValue("@userId", userId);
                                lecturerCommand.Parameters.AddWithValue("@firstName", registerLecturer.First_Name);
                                lecturerCommand.Parameters.AddWithValue("@lastName", registerLecturer.Last_Name);
                                lecturerCommand.Parameters.AddWithValue("@email", registerLecturer.Email);
                                lecturerCommand.Parameters.AddWithValue("@phoneNumber", registerLecturer.PhoneNumber);
                                lecturerCommand.Parameters.AddWithValue("@dateOfBirth", registerLecturer.Date_of_Birth);
                                lecturerCommand.Parameters.AddWithValue("@gender", registerLecturer.Gender);
                                lecturerCommand.Parameters.AddWithValue("@salary", registerLecturer.salary);
                                lecturerCommand.ExecuteNonQuery();
                            }
                            transaction.Commit();
                            MessageBox.Show("Lecturer created successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            MessageBox.Show($"Error while creating the lecturer: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Database connection error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
