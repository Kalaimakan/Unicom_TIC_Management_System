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

        //Get Staffs
        public List<Staff> GetAllStaff()
        {
            List<Staff> staffs = new List<Staff>();
            using (var connection = Db_Config.getConnection())
            {
                string getAdminQuery = @"SELECT s.*, u.User_Name, u.Password, u.User_Email 
                                FROM Staffs s
                                JOIN Users u ON s.User_Id = u.User_Id";
                using (var getCommand = new SQLiteCommand(getAdminQuery, connection))
                {
                    using (SQLiteDataReader reader = getCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            staffs.Add(new Staff()
                            {
                                Staff_Id = Convert.ToInt32(reader["Staff_Id"]),
                                First_Name = reader["First_Name"].ToString(),
                                Last_Name = reader["Last_Name"].ToString(),
                                Email = reader["Email"].ToString(),
                                Date_Of_Birth = reader["Date_Of_Birth"].ToString(),
                                Gender = reader["Gender"].ToString(),
                                PhoneNumber = reader["PhoneNumber"].ToString(),
                                User_Name = reader["User_Name"].ToString(),
                                Password = reader["Password"].ToString(),
                                User_Email = reader["User_Email"].ToString(),
                                Salary = Convert.ToDouble(reader["Salary"])
                            });
                        }
                    }
                }
            }
            return staffs;
        }

        //Update Staff
        public bool UpdateStaff(Staff staff, User user)
        {
            using (var connection = Db_Config.getConnection())
            {
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        // Verify Staff_Id exists
                        if (staff.Staff_Id <= 0)
                        {
                            throw new Exception("Invalid Admin ID");
                        }

                        // Get User_Id for this staff
                        string getUserQuery = "SELECT User_Id FROM Staffs WHERE Staff_Id = @staffId";
                        int userId;

                        using (var getUserCmd = new SQLiteCommand(getUserQuery, connection, transaction))
                        {
                            getUserCmd.Parameters.AddWithValue("@staffId", staff.Staff_Id);
                            var result = getUserCmd.ExecuteScalar();
                            if (result == null)
                            {
                                throw new Exception("No matching Staff found");
                            }
                            userId = Convert.ToInt32(result);
                        }

                        // Update Staff table
                        string updateAdminQuery = @"UPDATE Staffs 
                                   SET First_Name = @firstName,
                                       Last_Name = @lastName,
                                       Email = @email,
                                       Gender = @gender,
                                       PhoneNumber = @phoneNumber,
                                       Salary = @salary
                                    WHERE Staff_Id = @staffId";

                        using (var staffCommand = new SQLiteCommand(updateAdminQuery, connection, transaction))
                        {
                            staffCommand.Parameters.AddWithValue("@firstName", staff.First_Name);
                            staffCommand.Parameters.AddWithValue("@lastName", staff.Last_Name);
                            staffCommand.Parameters.AddWithValue("@email", staff.Email);
                            staffCommand.Parameters.AddWithValue("@gender", staff.Gender);
                            staffCommand.Parameters.AddWithValue("@salary", staff.Salary);
                            staffCommand.Parameters.AddWithValue("@phoneNumber", staff.PhoneNumber);
                            staffCommand.Parameters.AddWithValue("@staffId", staff.Staff_Id);

                            int rowsAffected = staffCommand.ExecuteNonQuery();
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


        //Delete Staff
        public bool DeleteStaff(int staffId)
        {
            using (var connection = Db_Config.getConnection())
            {
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        // Verify Staff_Id exists
                        if (staffId <= 0)
                        {
                            throw new Exception("Invalid Staff ID");
                        }
                        // Get User_Id for this staff
                        string getUserQuery = "SELECT User_Id FROM Staffs WHERE Staff_Id = @staffId";
                        int userId;
                        using (var getUserCmd = new SQLiteCommand(getUserQuery, connection, transaction))
                        {
                            getUserCmd.Parameters.AddWithValue("@staffId", staffId);
                            var result = getUserCmd.ExecuteScalar();
                            if (result == null)
                            {
                                throw new Exception("No matching Staff found");
                            }
                            userId = Convert.ToInt32(result);
                        }
                        // Delete from Staffs table
                        string deleteStaffQuery = "DELETE FROM Staffs WHERE Staff_Id = @staffId";
                        using (var deleteStaffCmd = new SQLiteCommand(deleteStaffQuery, connection, transaction))
                        {
                            deleteStaffCmd.Parameters.AddWithValue("@staffId", staffId);
                            int rowsAffected = deleteStaffCmd.ExecuteNonQuery();
                            if (rowsAffected == 0)
                            {
                                throw new Exception("No staff record was deleted");
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
