using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Unicom_TIC_Management_System.Models;
using Unicom_TIC_Management_System.Repositories;

namespace Unicom_TIC_Management_System.Controllers
{

    class LecturerController : Validation
    {
        public (bool isValid, string errorMessage) validateLecturerData(User validateUser, Lecturer validateLecturer)
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
        public void createLecture(User registerUser, Lecturer registerLecturer, Subject addSubject)
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
                                               User_Id, Employee_Id, First_Name, Last_Name, Email, PhoneNumber, Date_of_Birth, Gender, salary,subject_Id) VALUES (
                                               @userId, @employeeId, @firstName, @lastName, @email, @phoneNumber, @dateOfBirth, @gender, @salary,@subjectId );
                                                SELECT last_insert_rowid() ;";
                            UserController userController = new UserController();
                            int userId = userController.createUser(registerUser, connection, transaction);
                            string employeeId = Validation.autoGenerateLecturerId();
                            int lecturerId;
                            using (var lecturerCommand = new SQLiteCommand(lecturerQuary, connection))
                            {
                                lecturerCommand.Parameters.AddWithValue("@userId", userId);
                                lecturerCommand.Parameters.AddWithValue("@employeeId", employeeId);
                                lecturerCommand.Parameters.AddWithValue("@firstName", registerLecturer.First_Name);
                                lecturerCommand.Parameters.AddWithValue("@lastName", registerLecturer.Last_Name);
                                lecturerCommand.Parameters.AddWithValue("@email", registerLecturer.Email);
                                lecturerCommand.Parameters.AddWithValue("@phoneNumber", registerLecturer.PhoneNumber);
                                lecturerCommand.Parameters.AddWithValue("@dateOfBirth", registerLecturer.Date_of_Birth);
                                lecturerCommand.Parameters.AddWithValue("@gender", registerLecturer.Gender);
                                lecturerCommand.Parameters.AddWithValue("@salary", registerLecturer.salary);
                                lecturerCommand.Parameters.AddWithValue("@subjectId", addSubject.Subject_Id);
                                lecturerId = Convert.ToInt32(lecturerCommand.ExecuteScalar());
                                registerLecturer.Lecturer_Id = lecturerId;
                                registerLecturer.Subject_Id = addSubject.Subject_Id;
                                registerLecturer.Subject_Name = addSubject.Subject_Name;
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

        //get all lecturers
        public List<Lecturer> GetAllLecturer()
        {
            List<Lecturer> lecturers = new List<Lecturer>();
            using (var connection = Db_Config.getConnection())
            {
                string getLecturerQuary = "SELECT * FROM Lecturers;";
                using (var getCommand = new SQLiteCommand(getLecturerQuary, connection))
                {
                    using (SQLiteDataReader reader = getCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lecturers.Add(new Lecturer()
                            {
                                Lecturer_Id = Convert.ToInt32(reader["Lecturer_Id"]),
                                Employee_Id = reader["Employee_Id"].ToString(),
                                First_Name = reader["First_Name"].ToString(),
                                Last_Name = reader["Last_Name"].ToString(),
                                Email = reader["Email"].ToString(),
                                PhoneNumber = reader["PhoneNumber"].ToString(),
                                Date_of_Birth = reader["Date_of_Birth"].ToString(),
                                Gender = reader["Gender"].ToString(),
                                salary = Convert.ToDouble(reader["salary"]),
                                Subject_Id = Convert.ToInt32(reader["Subject_Id"])
                            });
                        }
                    }
                }
            }
            return lecturers;
        }

        //Update Lecturer
        public bool UpdateLecturer(Lecturer lecturer)
        {
            using (var connection = Db_Config.getConnection())
            {
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        // Update query - excluding Date_of_Birth
                        string updateLecturerQuery = @"UPDATE Lecturers 
                                        SET First_Name = @firstName,
                                            Last_Name = @lastName,
                                            Email = @email,
                                            PhoneNumber = @phoneNumber,
                                            Gender = @gender,
                                            Salary = @salary,
                                            Subject_Id = @subjectId
                                        WHERE Lecturer_Id = @lecturerId";

                        using (var updateCommand = new SQLiteCommand(updateLecturerQuery, connection, transaction))
                        {
                            updateCommand.Parameters.AddWithValue("@firstName", lecturer.First_Name);
                            updateCommand.Parameters.AddWithValue("@lastName", lecturer.Last_Name);
                            updateCommand.Parameters.AddWithValue("@email", lecturer.Email);
                            updateCommand.Parameters.AddWithValue("@phoneNumber", lecturer.PhoneNumber);
                            updateCommand.Parameters.AddWithValue("@gender", lecturer.Gender);
                            updateCommand.Parameters.AddWithValue("@salary", lecturer.salary);
                            updateCommand.Parameters.AddWithValue("@subjectId", lecturer.Subject_Id);
                            updateCommand.Parameters.AddWithValue("@lecturerId", lecturer.Lecturer_Id);

                            int rowsAffected = updateCommand.ExecuteNonQuery();
                            if (rowsAffected == 0)
                            {
                                throw new Exception("No lecturer records were updated");
                            }
                        }

                        transaction.Commit();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        MessageBox.Show($"Error updating lecturer: {ex.Message}", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
            }
        }

        //Delete lecturer
        public bool DeleteLecturer(int lecturerId)
        {
            using (var connection = Db_Config.getConnection())
            {
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        // Verify lecturerId exists
                        if (lecturerId <= 0)
                        {
                            throw new Exception("Invalid Lecturer ID");
                        }

                        string getUserQuery = "SELECT User_Id FROM Lecturers WHERE Lecturer_Id = @lecturerId";
                        int userId;

                        using (var getUserCmd = new SQLiteCommand(getUserQuery, connection, transaction))
                        {
                            getUserCmd.Parameters.AddWithValue("@lecturerId", lecturerId);
                            var result = getUserCmd.ExecuteScalar();
                            if (result == null)
                            {
                                throw new Exception("No matching Lecturer found");
                            }
                            userId = Convert.ToInt32(result);
                        }

                        //Delete from Lecturer_Student table
                        string deleteLecturerStudentQuary = "DELETE FROM Lecturer_Students WHERE Lecturer_Id= @lecturerId";
                        using (var deletecommand = new SQLiteCommand(deleteLecturerStudentQuary, connection, transaction))
                        {
                            deletecommand.Parameters.AddWithValue("@lecturerId", lecturerId);
                            deletecommand.ExecuteScalar();
                        }

                        //Delete from lecturer Table
                        string deleteLecturerQuary = "DELETE FROM Lecturers WHERE Lecturer_Id = @lecturerId";
                        using (var deletecommand = new SQLiteCommand(deleteLecturerQuary, connection, transaction))
                        {
                            deletecommand.Parameters.AddWithValue("@lecturerId", lecturerId);
                            int rowAffected = deletecommand.ExecuteNonQuery();
                            if (rowAffected == 0)
                            {
                                throw new Exception("No Lecturer record was deleted");
                            }
                        }

                        //Delete FRom User Table
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