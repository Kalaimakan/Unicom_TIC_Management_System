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
            var lastNameValidate = validateName(validateLecturer.Last_Name,"Last Name");
            if (!lastNameValidate.isValid)
            {
                return lastNameValidate;
            }
            //User Name validate
            var userNameValidate = validateName(validateUser.User_Name,"User Name");
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
            var genderValidate = validateGender(false,false,false);
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
            return (true,string.Empty);
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
                                               @userId, @employeeId, @firstName, @lastName, @email, @phoneNumber, @dateOfBirth, @gender, @salary,@subjectId );";
                            UserController userController = new UserController();
                            int userId = userController.createUser(registerUser, connection, transaction);
                            string employeeId = Validation.autoGenerateLecturerId();
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
                                lecturerCommand.Parameters.AddWithValue("@subjectId",addSubject.Subject_Id);
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

        public List<Lecturer> GetLecturersByCourseId(int courseId, SQLiteConnection connection)
        {
            List<Lecturer> lecturers = new List<Lecturer>();
            string query = @"SELECT l.Lecturer_Id, l.Subject_Id, l.First_Name, l.Last_Name
                        FROM Lecturers l
                        JOIN Subjects s ON s.Subject_Id = l.Subject_Id
                        WHERE s.Course_Id = @courseId";

            using (var command = new SQLiteCommand(query, connection))
            {
                command.Parameters.AddWithValue("@courseId", courseId);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        lecturers.Add(new Lecturer
                        {
                            Lecturer_Id = Convert.ToInt32(reader["Lecturer_Id"]),
                            Subject_Id = Convert.ToInt32(reader["Subject_Id"]),
                            First_Name = reader["First_Name"].ToString(),
                            Last_Name = reader["Last_Name"].ToString()
                        });
                    }
                }
            }
            return lecturers;
        }

    }
}
