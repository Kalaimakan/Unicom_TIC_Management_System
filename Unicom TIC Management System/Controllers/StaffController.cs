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
    class StaffController : Validation
    {
        public (bool isValid, string errorMessage) validateStaffData(User validateUser, Lecturer validateLecturer)
        {
            //First Name validate
            var firstNameValidate = validateName(validateLecturer.First_Name, "First Name");
            if (!firstNameValidate.isValid)
            {
                return firstNameValidate;
            }
            //Last Name validate
            var lastNameValidate = validateName(validateLecturer.Last_Name, "Last Name");
            if (!lastNameValidate.isValid)
            {
                return lastNameValidate;
            }
            //User Name validate
            var userNameValidate = validateName(validateUser.User_Name, "User Name");
            if (!userNameValidate.isValid)
            {
                return userNameValidate;
            }
            //Password validate
            var passwordValidate = validatePassword(validateUser.Password);
            if (!passwordValidate.isValid)
            {
                return passwordValidate;
            }
            //DOB validate
            if (!DateTime.TryParse(validateLecturer.Date_of_Birth, out DateTime DOB))
            {
                return (false, "Invalid Date of Birth format.");
            }
            var dateOfBirthValidate = validateDateOfBirth(DOB);
            if (!dateOfBirthValidate.isValid)
            {
                return dateOfBirthValidate;
            }
            //Email validate
            var emailValidate = validateEmail(validateUser.User_Email);
            if (!emailValidate.isValid)
            {
                return emailValidate;
            }
            //Phone Number validate
            var phoneNumberValidate = validatePhoneNumber(validateLecturer.PhoneNumber);
            if (!phoneNumberValidate.isValid)
            {
                return phoneNumberValidate;
            }
            //Gender validate.
            var genderValidate = validateGender(false, false, false);
            if (!genderValidate.isValid)
            {
                return genderValidate;
            }
            //Salary validate
            var salaryValidate = validateSalary(Convert.ToString(validateLecturer.salary));
            if (!salaryValidate.isValid)
            {
                return salaryValidate;
            }
            return (true, string.Empty);
        }
        public void createStaff(User registerUser, Staff registerStaff)
        {
            try
            {
                using (var connection = Db_Config.getConnection())
                {
                    using (var transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            string staffQuery = @"INSERT INTO Staffs 
                                            (User_Id, First_Name, Last_Name, Email, PhoneNumber, Date_Of_Birth, Gender, Salary ) VALUES 
                                            (@UserId, @firstName, @lastName, @email, @phoneNumber, @dateOfBirth, @gender, @salary );";
                            UserController userController = new UserController();
                            int userId = userController.createUser(registerUser, connection, transaction);
                            using (var staffCommand = new SQLiteCommand(staffQuery, connection))
                            {
                                staffCommand.Parameters.AddWithValue("@UserId", userId);
                                staffCommand.Parameters.AddWithValue("@firstName", registerStaff.First_Name);
                                staffCommand.Parameters.AddWithValue("@lastName", registerStaff.Last_Name);
                                staffCommand.Parameters.AddWithValue("@dateOfBirth", registerStaff.Date_Of_Birth);
                                staffCommand.Parameters.AddWithValue("@email", registerStaff.Email);
                                staffCommand.Parameters.AddWithValue("@gender", registerStaff.Gender);
                                staffCommand.Parameters.AddWithValue("@phoneNumber", registerStaff.PhoneNumber);
                                staffCommand.Parameters.AddWithValue("@salary", registerStaff.Salary);
                                staffCommand.ExecuteNonQuery();
                            }
                            transaction.Commit();
                            MessageBox.Show("Staff created successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        catch (Exception ex)
                        {
                            // Rollback the transaction in case of an error
                            transaction.Rollback();
                            MessageBox.Show($"Error while creating the staff: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error while Registering the User : {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
