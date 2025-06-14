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
        public void createAdmin(User registerUser, Admin registerAdmin)
        {
            //try
            //{
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
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show($"An error occurred while registering the user: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
        }

    }
}
