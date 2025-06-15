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
    class StudentController : Validation
    {
        public (bool isValid, string errorMessage) validateStudentData(User validateUser, Student validateStudent)
        {
            //First Name validate
            var firstNameValidate = validateName(validateStudent.First_Name, "First Name");
            if (!firstNameValidate.isValid)
            {
                return firstNameValidate;
            }
            //Last Name validate
            var lastNameValidate = validateName(validateStudent.Last_Name, "Last Name");
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
            if (!DateTime.TryParse(validateStudent.Date_of_Birth, out DateTime DOB))
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
            var phoneNumberValidate = validatePhoneNumber(validateStudent.PhoneNumber);
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
            //Address validate.
            var addressValidate = validateAddress(validateStudent.Address);
            if (!addressValidate.isValid)
            {
                return addressValidate;
            }
            return (true,string.Empty);
        }
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
                            string studentQuary = @"INSERT INTO Students(
                                                User_Id, Admission_No, First_Name, Last_Name, Email, Gender, PhoneNumber, Date_of_Birth, Address, Entrolled_Course)VALUES(
                                                @userId, @admissionNo, @firstName, @lastName, @email, @gender, @phoneNumber, @dateofBirth, @address, @entrolledCourse
                                              );";
                            

                            UserController userController = new UserController();
                            int userId = userController.createUser(registerUser, connection, transaction);
                            string admissionNumber = Validation.autoGenerateStudentId();
                            using (SQLiteCommand studentCommand = new SQLiteCommand(studentQuary, connection))
                            {
                                studentCommand.Parameters.AddWithValue("@userId", userId);
                                studentCommand.Parameters.AddWithValue("@admissionNo",admissionNumber);
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
                            //string getIdQuery = "SELECT Admission_No FROM Students ORDER BY Admission_No DESC LIMIT 1";
                            //using (SQLiteCommand getIdCommand = new SQLiteCommand(getIdQuery, connection))
                            //{
                            //    using (SQLiteDataReader reader = getIdCommand.ExecuteReader())
                            //    {
                            //        if (reader.Read())
                            //        {
                            //            registerStudent.Admission_No = reader["Admission_No"].ToString();
                            //        }
                            //        else
                            //        {
                            //            MessageBox.Show("Failed to retrieve the last inserted student ID.");
                            //        }
                            //    }
                            //}
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
