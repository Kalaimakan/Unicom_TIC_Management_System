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
        public (bool isValid, string errorMessage) validateAdminData(User registerUser, Admin registerAdmin)
        {
            //validate First Name.
            var firstNameValidate = validateName(registerAdmin.First_Name, "First Name");
            if (!firstNameValidate.isValid)
                return firstNameValidate;

            //validate Last Name.
            var lastNameValidate = validateName(registerAdmin.Last_Name, "Last Name");
            if (!lastNameValidate.isValid)
                return lastNameValidate;

            //validate Email.
            var emailValidate = validateEmail(registerUser.User_Email);
            if (!emailValidate.isValid)
                return emailValidate;

            //validate Phone Number.
            var phoneNumberValidate = validatePhoneNumber(registerAdmin.PhoneNumber);
            if (!phoneNumberValidate.isValid)
                return phoneNumberValidate;

            //validate Date of Birth.
            if (!DateTime.TryParse(registerAdmin.Date_Of_Birth, out DateTime DOB))
            {
                return (false, "Invalid Date of Birth format.");
            }
            var dateOfBirthValidate = validateDateOfBirth(DOB);
            if (!dateOfBirthValidate.isValid)
                return dateOfBirthValidate;

            //validate Gender.
            var genderValidate = validateGender(false, false, false);
            if (!genderValidate.isValid)
                return genderValidate;



            return (true, string.Empty);
        }

        //Create Admin.
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
                            //set the user Id to createAdmin
                            UserController userController = new UserController();
                            int userId = userController.createUser(registerUser, connection, transaction);
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

        //View Admin
        //public List<Admin> GetAllAdmin()
        //{
        //    List<Admin> admins = new List<Admin>();
        //    using (var connection = Db_Config.getConnection())
        //    {
        //        string getAdminQuery = "SELECT * FROM Admins";
        //        using (var getCommand = new SQLiteCommand(getAdminQuery, connection))
        //        {
        //            using (SQLiteDataReader reader = getCommand.ExecuteReader())
        //            {
        //                while (reader.Read())
        //                {
        //                    admins.Add(new Admin()
        //                    {
        //                        Admin_Id = Convert.ToInt32(reader["Admin_Id"]),
        //                        First_Name = reader["First_Name"].ToString(),
        //                        Last_Name = reader["Last_Name"].ToString(),
        //                        Date_Of_Birth = reader["Date_Of_Birth"].ToString(),
        //                        Email = reader["Email"].ToString(),
        //                        Gender = reader["Gender"].ToString(),
        //                        PhoneNumber = reader["PhoneNumber"].ToString()
        //                    });
        //                }
        //            }
        //        }
        //    }
        //    return admins;
        //}

        public List<Admin> GetAllAdmin()
        {
            List<Admin> admins = new List<Admin>();
            using (var connection = Db_Config.getConnection())
            {
                string getAdminQuery = @"SELECT a.*, u.User_Name, u.Password, u.User_Email 
                                FROM Admins a
                                JOIN Users u ON a.User_Id = u.User_Id";
                using (var getCommand = new SQLiteCommand(getAdminQuery, connection))
                {
                    using (SQLiteDataReader reader = getCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            admins.Add(new Admin()
                            {
                                Admin_Id = Convert.ToInt32(reader["Admin_Id"]),
                                First_Name = reader["First_Name"].ToString(),
                                Last_Name = reader["Last_Name"].ToString(),
                                Email = reader["Email"].ToString(),
                                Date_Of_Birth = reader["Date_Of_Birth"].ToString(),
                                Gender = reader["Gender"].ToString(),
                                PhoneNumber = reader["PhoneNumber"].ToString(),
                                User_Name = reader["User_Name"].ToString(),  
                                Password = reader["Password"].ToString(),     
                                User_Email = reader["User_Email"].ToString() 
                            });
                        }
                    }
                }
            }
            return admins;
        }

        //Update Admin
        public bool UpdateAdmin(Admin admin, User user)
        {
            using (var connection = Db_Config.getConnection())
            {
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        // Verify Admin_Id exists
                        if (admin.Admin_Id <= 0)
                        {
                            throw new Exception("Invalid Admin ID");
                        }

                        // Get User_Id for this admin
                        string getUserQuery = "SELECT User_Id FROM Admins WHERE Admin_Id = @adminId";
                        int userId;

                        using (var getUserCmd = new SQLiteCommand(getUserQuery, connection, transaction))
                        {
                            getUserCmd.Parameters.AddWithValue("@adminId", admin.Admin_Id);
                            var result = getUserCmd.ExecuteScalar();
                            if (result == null)
                            {
                                throw new Exception("No matching admin found");
                            }
                            userId = Convert.ToInt32(result);
                        }

                        // Update Admin table
                        string updateAdminQuery = @"UPDATE Admins 
                                   SET First_Name = @firstName,
                                       Last_Name = @lastName,
                                       Email = @email,
                                       Gender = @gender,
                                       PhoneNumber = @phoneNumber
                                    WHERE Admin_Id = @adminId";

                        using (var adminCommand = new SQLiteCommand(updateAdminQuery, connection, transaction))
                        {
                            adminCommand.Parameters.AddWithValue("@firstName", admin.First_Name);
                            adminCommand.Parameters.AddWithValue("@lastName", admin.Last_Name);
                            adminCommand.Parameters.AddWithValue("@email", admin.Email);
                            adminCommand.Parameters.AddWithValue("@gender", admin.Gender);
                            adminCommand.Parameters.AddWithValue("@phoneNumber", admin.PhoneNumber);
                            adminCommand.Parameters.AddWithValue("@adminId", admin.Admin_Id);

                            int rowsAffected = adminCommand.ExecuteNonQuery();
                            if (rowsAffected == 0)
                            {
                                throw new Exception("No admin record was updated");
                            }
                        }

                        // Update User table
                        user.User_Id = userId;
                        UserController userController = new UserController();
                        userController.UpdateUser(user, connection, transaction);

                        transaction.Commit();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw new Exception($"Update failed: {ex.Message}");
                    }
                }
            }
        }

        //delete Admin
        public bool DeleteAdmin(int adminId)
        {
            using (var connection = Db_Config.getConnection())
            {
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        // Verify Admin_Id exists
                        if (adminId <= 0)
                        {
                            throw new Exception("Invalid Admin ID");
                        }
                        // Get User_Id for this admin
                        string getUserQuery = "SELECT User_Id FROM Admins WHERE Admin_Id = @adminId";
                        int userId;
                        using (var getUserCmd = new SQLiteCommand(getUserQuery, connection, transaction))
                        {
                            getUserCmd.Parameters.AddWithValue("@adminId", adminId);
                            var result = getUserCmd.ExecuteScalar();
                            if (result == null)
                            {
                                throw new Exception("No matching admin found");
                            }
                            userId = Convert.ToInt32(result);
                        }
                        // Delete from Admins table
                        string deleteAdminQuery = "DELETE FROM Admins WHERE Admin_Id = @adminId";
                        using (var deleteAdminCmd = new SQLiteCommand(deleteAdminQuery, connection, transaction))
                        {
                            deleteAdminCmd.Parameters.AddWithValue("@adminId", adminId);
                            int rowsAffected = deleteAdminCmd.ExecuteNonQuery();
                            if (rowsAffected == 0)
                            {
                                throw new Exception("No admin record was deleted");
                            }
                        }
                        // Delete from Users table
                        UserController userController = new UserController();
                        userController.DeleteUser(userId, connection, transaction);
                        transaction.Commit();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw new Exception($"Delete failed: {ex.Message}");
                    }
                }
            }
        }
    }
}
