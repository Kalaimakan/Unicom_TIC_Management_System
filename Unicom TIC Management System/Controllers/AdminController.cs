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
    class AdminController : Validation
    {
        public void createAdmin(User registerUser, Admin registerAdmin)
        {
            try
            {
                using (var connection = Db_Config.getConnection())
                {
                    using (SQLiteTransaction transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            //first user table data.
                            string userQuary = @"INSERT INTO Users 
                                            (User_Name, Password, User_Email, User_Role) VALUES 
                                            (@UserName, @Password, @userEmail, @userRole);
                                             SELECT last_insert_rowid();";
                            using (SQLiteCommand userCommand = new SQLiteCommand(userQuary, connection))
                            {
                                userCommand.Parameters.AddWithValue("@UserName", registerUser.User_Name);
                                userCommand.Parameters.AddWithValue("@Password", registerUser.Password);
                                userCommand.Parameters.AddWithValue("@userEmail", registerUser.User_Email);
                                userCommand.Parameters.AddWithValue("@userRole", "Admin");

                                //get the user Id.
                                int userId = Convert.ToInt32(userCommand.ExecuteScalar());

                                //set the user Id to createAdmin
                                string adminQuary = @"INSERT INTO Admins
                                                (User_Id, First_Name, Last_Name, Date_Of_Birth, Email, Gender, PhoneNumber) VALUES 
                                                (@userId, @firstName, @lastName ,@dateOfBirth, @email, @gender ,@phoneNumber);";
                                using (SQLiteCommand adminCommand = new SQLiteCommand(adminQuary, connection))
                                {
                                    adminCommand.Parameters.AddWithValue("userId", userId);
                                    adminCommand.Parameters.AddWithValue("@firstName", registerAdmin.First_Name);
                                    adminCommand.Parameters.AddWithValue("@lastName", registerAdmin.Last_Name);
                                    adminCommand.Parameters.AddWithValue("@dateOfBirth", registerAdmin.Date_Of_Birth);
                                    adminCommand.Parameters.AddWithValue("@email", registerAdmin.Email);
                                    adminCommand.Parameters.AddWithValue("@gender", registerAdmin.Gender);
                                    adminCommand.Parameters.AddWithValue("@phoneNumber", registerAdmin.PhoneNumber);
                                    adminCommand.ExecuteNonQuery();



                                }
                            }
                            MessageBox.Show($"Mr/mrs.{registerAdmin.Last_Name} registered successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            transaction.Commit();
                        }
                        catch
                        {
                            MessageBox.Show("An error occurred while creating the admin. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            transaction.Rollback();
                            throw;
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while registering the user: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
