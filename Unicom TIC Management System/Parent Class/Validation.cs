using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Unicom_TIC_Management_System.Controllers;
using Unicom_TIC_Management_System.Repositories;
using Unicom_TIC_Management_System.Views;

namespace Unicom_TIC_Management_System.Models
{
    class Validation
    {
        private static int lecturerAdmissionNo = 100;

        public static string autoGenerateStudentId()
        {
            string lastId = null;
            string newStudentId = "UT10000";

            using (var connection = Db_Config.getConnection())
            {
                string query = "SELECT Admission_No FROM Students WHERE Admission_No LIKE 'UT%' ORDER BY Admission_No DESC LIMIT 1";
                using (var cmd = new SQLiteCommand(query, connection))
                {
                    object result = cmd.ExecuteScalar();
                    if (result != null)
                    {
                        lastId = result.ToString(); 
                        int lastNumber = int.Parse(lastId.Substring(2));
                        newStudentId = "UT" + (lastNumber + 1);
                    }
                    else
                    {
                        newStudentId = "UT10001";
                    }
                }
            }

            return newStudentId;
        }
        public static string autoGenerateLecturerId()
        {
            string lastId = null;
            string newLecturerId = "LT100";

            using (var connection = Db_Config.getConnection())
            {
                string query = "SELECT Employee_Id FROM Lecturers WHERE Employee_Id LIKE 'LT%' ORDER BY Employee_Id DESC LIMIT 1";
                using (var cmd = new SQLiteCommand(query, connection))
                {
                    object result = cmd.ExecuteScalar();
                    if (result != null)
                    {
                        lastId = result.ToString();
                        int lastNumber = int.Parse(lastId.Substring(2));
                        newLecturerId = "LT" + (lastNumber + 1);
                    }
                    else
                    {
                        newLecturerId = "LT101";
                    }
                }
            }

            return newLecturerId;
        }

        // Name Validation.
        public (bool isValid, string errorMessage) validateName(string name, string fieldName)
        {
            //if it's Empty
            if (string.IsNullOrWhiteSpace(name) || name == $"Enter the {fieldName}")
            {
                return (false, $"{fieldName} is required.");
            }

            if (fieldName == "User Name")
            {
                if (name.Length < 4)
                {
                    return (false, "Username must be at least 4 characters.");
                }

                if (name.Length > 20)
                {
                    return (false, "Username cannot be long  20 characters.");
                }

                string userNamePattern = @"^[a-zA-Z][a-zA-Z0-9_\.]+$";
                if (!Regex.IsMatch(name, userNamePattern))
                {
                    return (false, "Username Cantain invalid Characters.");
                }
            }
            else
            {
                //if its less lenth.
                if (name.Length < 2)
                {
                    return (false, $"{fieldName} must be at least 2 characters.");
                }

                //first name or last name not cantain numbers or simbols
                string namePattern = @"^[a-zA-Z\s\-']+$";
                if (!Regex.IsMatch(name, namePattern))
                {
                    return (false, $"{fieldName} Cantain Invalid Characters.");
                }
            }
            return (true, string.Empty);
        }

        //Password Validation.
        public (bool isValid, string errorMessage) validatePassword(string password)
        {
            //if It's Empty
            if (string.IsNullOrWhiteSpace(password) || password == "Enter the Password")
            {
                return (false, "Password is required.");
            }
            //if its less lenth.
            if (password.Length < 8)
            {
                return (false, "Password must be at least 8 characters.");
            }
            //if its more lenth.
            if (password.Length > 20)
            {
                return (false, "Password cannot be long 20 characters.");
            }
            //if it not contain numbers.
            if (!Regex.IsMatch(password, @"\d"))
            {
                return (false, "Password must contain at least one number.");
            }
            //if it does not contain letters.
            if (!Regex.IsMatch(password, @"[a-zA-Z]"))
            {
                return (false, "Password must contain at least one letter.");
            }
            return (true, string.Empty);
        }

        // Email Validation.
        public (bool isValid, string errorMessage) validateEmail(string email)
        {
            //if It's Empty
            if (string.IsNullOrWhiteSpace(email) || email == "Enter the Email")
            {
                return (false, "Email is required.");
            }

            string emailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            if (!Regex.IsMatch(email, emailPattern))
            {
                return (false, "Invalid Email format.");
            }
            return (true, string.Empty);
        }

        // Phone Number Validation.
        public (bool isValid, string errorMessage) validatePhoneNumber(string phoneNumber)
        {
            //if It's Empty
            if (string.IsNullOrWhiteSpace(phoneNumber) || phoneNumber == "Enter the Phone Number")
            {
                return (false, "Phone Number is required.");
            }

            if (!phoneNumber.All(Char.IsDigit))
            {
                return (false, "Enter Numbers Only.");
            }

            if (phoneNumber.Length < 10 || phoneNumber.Length > 10)
            {
                return (false, "Phone Number must be 10 Numbers.");
            }
            return (true, string.Empty);
        }

        // Date of Birth Validation.
        public (bool isValid, string errorMessage) validateDateOfBirth(DateTime dateOfBirth)
        {
            //if It's Empty
            if (dateOfBirth == default(DateTime))
            {
                return (false, "Date of Birth is required.");
            }

            //if the date in future
            if (dateOfBirth >= DateTime.Today)
            {
                return (false, "You can't predict a human born this date in the future.");
            }
            return (true, string.Empty);
        }

        //Gender Validation.
        public (bool isValid, string errorMessage) validateGender(bool isMaleChecked, bool isFemaleChecked, bool isOtherChecked)
        {
            int checkedCount = 0;
            if (isMaleChecked)
                checkedCount++;
            if (isFemaleChecked)
                checkedCount++;
            if (isOtherChecked)
                checkedCount++;

            if (checkedCount == 0)
            {
                return (false, "Gender selection is required.");
            }
            else if (checkedCount > 1)
            {
                return (false, "Please select only one gender option.");
            }

            return (true, string.Empty);
        }

        //Salary Validation.
        public (bool isValid, string errorMessage) validateSalary(string salary)
        {
            //if it's Empty.
            if (string.IsNullOrWhiteSpace(salary) || salary == "Enter the Salary")
            {
                return (false, "salary is required.");
            }
            //if it not numbers
            if (!double.TryParse(salary, out double Salary))
            {
                return (false, "salary must be a valid number.");
            }

            //if it less than 0
            if (Salary < 0)
            {
                return (false, "Salary must be positive Nuumber");
            }
            return (true, string.Empty);
        }

        //Address Validation
        public (bool isValid, string errorMessage) validateAddress(string address)
        {
            if (string.IsNullOrWhiteSpace(address))
            {
                return (false, "Address is required.");
            }

            if (address.Length > 30)
            {
                return (false, "Address is toooo long.");
            }
            return (true, string.Empty);
        }

        //Course Name Validation
        public (bool isValid, string errorMessage) validateCourseName(string courseName , string fieldName)
        {
            //if it's Empty
            if (string.IsNullOrWhiteSpace(courseName) || courseName == "Enter the Course Name")
            {
                return (false, $"{fieldName} is required.");
            }
            //if its less lenth.
            if (courseName.Length < 5)
            {
                return (false, $"{fieldName} must be at least 2 characters.");
            }
            //if its more lenth.
            if (courseName.Length > 30)
            {
                return (false, $"{fieldName} cannot be long 30 characters.");
            }
            return (true, string.Empty);
        }

        //Start Date Validation
        public (bool isValid, string errorMessage) validateStartDate(DateTime startDate)
        {
            //if it's Empty
            if (startDate == default(DateTime))
            {
                return (false, "Start Date is required.");
            }
            //if the date in future
            if (startDate <= DateTime.Today)
            {
                return (false, "Start Date cannot be in the past.");
            }
            return (true, string.Empty);
        }

        


    }

}