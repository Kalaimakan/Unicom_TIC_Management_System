﻿using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
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
            return (true, string.Empty);
        }
        public void createStudent(Student registerStudent, User registerUser, Course course, Department department)
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
                                                User_Id, Admission_No, First_Name, Last_Name, Email, Gender, PhoneNumber, Date_of_Birth, Address, Entrolled_Course, Course_Id,Department_Id)VALUES(
                                                @userId, @admissionNo, @firstName, @lastName, @email, @gender, @phoneNumber, @dateofBirth, @address, @entrolledCourse, @courseId, @departmentId);
                                              SELECT last_insert_rowid();
                                                ";


                            UserController userController = new UserController();
                            int userId = userController.createUser(registerUser, connection, transaction);
                            string admissionNumber = Validation.autoGenerateStudentId();
                            int studentId;
                            using (SQLiteCommand studentCommand = new SQLiteCommand(studentQuary, connection))
                            {
                                studentCommand.Parameters.AddWithValue("@userId", userId);
                                studentCommand.Parameters.AddWithValue("@admissionNo", admissionNumber);
                                studentCommand.Parameters.AddWithValue("@firstName", registerStudent.First_Name);
                                studentCommand.Parameters.AddWithValue("@lastName", registerStudent.Last_Name);
                                studentCommand.Parameters.AddWithValue("@email", registerStudent.Email);
                                studentCommand.Parameters.AddWithValue("@gender", registerStudent.Gender);
                                studentCommand.Parameters.AddWithValue("@phoneNumber", registerStudent.PhoneNumber);
                                studentCommand.Parameters.AddWithValue("@dateofBirth", registerStudent.Date_of_Birth);
                                studentCommand.Parameters.AddWithValue("@address", registerStudent.Address);
                                studentCommand.Parameters.AddWithValue("@entrolledCourse", course.Course_Name);
                                studentCommand.Parameters.AddWithValue("@courseId", course.Course_Id);
                                studentCommand.Parameters.AddWithValue("@departmentId", course.Department_Id);
                                studentId = Convert.ToInt32(studentCommand.ExecuteScalar());
                                registerStudent.Student_Id = studentId;
                            }

                            SubjectController subjectController = new SubjectController();
                            List<Subject> subjects = subjectController.GetSubjectsByCourseId(course.Course_Id, connection, transaction);
                            Subject_StudentController subjectStudentController = new Subject_StudentController();
                            Lecturer_StudentController lecturerStudentController = new Lecturer_StudentController();
                            foreach (var subject in subjects)
                            {
                                subjectStudentController.AssignSubjectsToStudent(subject, registerStudent, connection, transaction);

                                // Get lecturers for this subject
                                List<Lecturer> subjectLecturers = GetLecturersBySubjectId(subject.Subject_Id, connection, transaction);

                                // Assign each lecturer to the student for this subject
                                foreach (var lecturer in subjectLecturers)
                                {
                                    lecturerStudentController.AssignLecturerToStudent(lecturer, registerStudent, subject, connection, transaction);
                                }
                            }

                            MessageBox.Show($"Mr/mrs.{registerStudent.Last_Name} registered successfully! and Entrolled in {course.Course_Name} with {subjects.Count} Subjects", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        // Get Students
        public List<Student> GetAllStudents()
        {
            List<Student> students = new List<Student>();
            using (var connection = Db_Config.getConnection())
            {
                string getStudentQuery = "SELECT * FROM Students";
                using (var getCommand = new SQLiteCommand(getStudentQuery, connection))
                {
                    using (SQLiteDataReader reader = getCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            students.Add(new Student()
                            {
                                Student_Id = Convert.ToInt32(reader["Student_Id"]),
                                Admission_No = reader["Admission_No"].ToString(),
                                First_Name = reader["First_Name"].ToString(),
                                Last_Name = reader["Last_Name"].ToString(),
                                Email = reader["Email"].ToString(),
                                Gender = reader["Gender"].ToString(),
                                PhoneNumber = reader["PhoneNumber"].ToString(),
                                Date_of_Birth = reader["Date_Of_Birth"].ToString(),
                                Address = reader["Address"].ToString(),
                                Entrolld_Course = reader["Entrolled_Course"].ToString(),
                                Course_Id = Convert.ToInt32(reader["Course_Id"]),
                                Department_Id = Convert.ToInt32(reader["Department_Id"]),
                            });
                        }
                    }
                }
            }
            return students;
        }

        //Get lecturers by subject ID
        private List<Lecturer> GetLecturersBySubjectId(int subjectId, SQLiteConnection connection, SQLiteTransaction transaction)
        {
            List<Lecturer> lecturers = new List<Lecturer>();
            string query = "SELECT * FROM Lecturers WHERE Subject_Id = @subjectId";

            using (var command = new SQLiteCommand(query, connection, transaction))
            {
                command.Parameters.AddWithValue("@subjectId", subjectId);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        lecturers.Add(new Lecturer()
                        {
                            Lecturer_Id = Convert.ToInt32(reader["Lecturer_Id"]),
                            Employee_Id = reader["Employee_Id"].ToString(),
                            First_Name = reader["First_Name"].ToString(),
                            Last_Name = reader["Last_Name"].ToString(),
                            Subject_Id = Convert.ToInt32(reader["Subject_Id"]),
                            Subject_Name = "" // You might want to populate this too
                        });
                    }
                }
            }
            return lecturers;
        }

        //update student
        public bool UpdateAdmin(Student student, User user)
        {
            using (var connection = Db_Config.getConnection())
            {
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        // Verify Admin_Id exists
                        if (student.Student_Id <= 0)
                        {
                            throw new Exception("Invalid student ID");
                        }

                        // Get User_Id for this student
                        string getUserQuery = "SELECT User_Id FROM Students WHERE Student_Id = @studentId";
                        int userId;

                        using (var getUserCmd = new SQLiteCommand(getUserQuery, connection, transaction))
                        {
                            getUserCmd.Parameters.AddWithValue("@studentId", student.Student_Id);
                            var result = getUserCmd.ExecuteScalar();
                            if (result == null)
                            {
                                throw new Exception("No matching Student found");
                            }
                            userId = Convert.ToInt32(result);
                        }

                        // Update Student table
                        string updateStudentQuery = @"UPDATE Students 
                                   SET First_Name = @firstName,
                                       Last_Name = @lastName,
                                       Email = @email,
                                       Gender = @gender,
                                       PhoneNumber = @phoneNumber,
                                       Address= @address,
                                       Course_Id = @courseId,
                                       Department_Id = @departmentId   
                                      WHERE Student_Id = @studentId";

                        using (var studentCommand = new SQLiteCommand(updateStudentQuery, connection, transaction))
                        {
                            studentCommand.Parameters.AddWithValue("@firstName", student.First_Name);
                            studentCommand.Parameters.AddWithValue("@lastName", student.Last_Name);
                            studentCommand.Parameters.AddWithValue("@email", student.Email);
                            studentCommand.Parameters.AddWithValue("@gender", student.Gender);
                            studentCommand.Parameters.AddWithValue("@phoneNumber", student.PhoneNumber);
                            studentCommand.Parameters.AddWithValue("@address", student.Address);
                            studentCommand.Parameters.AddWithValue("@courseId", student.Course_Id);
                            studentCommand.Parameters.AddWithValue("@departmentId", student.Department_Id);
                            studentCommand.Parameters.AddWithValue("@studentId", student.Student_Id);

                            int rowsAffected = studentCommand.ExecuteNonQuery();
                            if (rowsAffected == 0)
                            {
                                throw new Exception("No Student record was updated");
                            }
                        }
                        UserController userController = new UserController();
                        userController.UpdateEmailUser(userId, student.Email, connection, transaction);
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

        // Delete Student
        public bool DeleteStudent(int studentId)
        {
            using (var connection = Db_Config.getConnection())
            {
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        // Verify Student_Id exists
                        if (studentId <= 0)
                        {
                            throw new Exception("Invalid student ID");
                        }

                        // FIRST get the User_Id before deleting anything
                        string getUserQuery = "SELECT User_Id FROM Students WHERE Student_Id = @studentId";
                        int userId;

                        using (var getUserCmd = new SQLiteCommand(getUserQuery, connection, transaction))
                        {
                            getUserCmd.Parameters.AddWithValue("@studentId", studentId);
                            var result = getUserCmd.ExecuteScalar();
                            if (result == null)
                            {
                                throw new Exception("No matching Student found");
                            }
                            userId = Convert.ToInt32(result);
                        }

                        // Now delete from dependent tables
                        string deleteSubjectStudentQuery = "DELETE FROM Subject_Students WHERE Student_Id = @studentId";
                        using (var deleteCommand = new SQLiteCommand(deleteSubjectStudentQuery, connection, transaction))
                        {
                            deleteCommand.Parameters.AddWithValue("@studentId", studentId);
                            deleteCommand.ExecuteNonQuery();
                        }

                        string deleteLecturerStudentQuery = "DELETE FROM Lecturer_Students WHERE Student_Id = @studentId";
                        using (var deleteLecturerCommand = new SQLiteCommand(deleteLecturerStudentQuery, connection, transaction))
                        {
                            deleteLecturerCommand.Parameters.AddWithValue("@studentId", studentId);
                            deleteLecturerCommand.ExecuteNonQuery();
                        }

                        // Finally delete from Students table
                        string deleteStudentQuery = "DELETE FROM Students WHERE Student_Id = @studentId";
                        using (var deleteCommand = new SQLiteCommand(deleteStudentQuery, connection, transaction))
                        {
                            deleteCommand.Parameters.AddWithValue("@studentId", studentId);
                            int rowsAffected = deleteCommand.ExecuteNonQuery();
                            if (rowsAffected == 0)
                            {
                                throw new Exception("No Student record was deleted");
                            }
                        }

                        // Delete from Users table using the userId we got earlier
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