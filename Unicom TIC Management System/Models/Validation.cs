using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Unicom_TIC_Management_System.Controllers;
using Unicom_TIC_Management_System.Views;

namespace Unicom_TIC_Management_System.Models
{
    class Validation
    {
        // Name Validation.
        public (bool isValid, string errorMessage) validateName(string name, string fieldName)
        {
            //if it's Empty
            if (string.IsNullOrWhiteSpace(name) || name == $"Enter the {fieldName}")
            {
                return (false, $"{fieldName} is Required.");
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
                    return (false, $"{fieldName} can only contain letters, spaces, hyphens (-), and apostrophes (')");
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
                return (false, "Password is Required.");
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
                return (false, "Email is Required.");
            }

            string emailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            if (!Regex.IsMatch(email, emailPattern))
            {
                return (false, "Invalid email format.");
            }
            return (true, string.Empty);
        }

        // Phone Number Validation.
        public (bool isValid, string errorMessage) validatePhoneNumber(string phoneNumber)
        {
            //if It's Empty
            if (string.IsNullOrWhiteSpace(phoneNumber) || phoneNumber == "Enter the Phone Number")
            {
                return (false, "Phone Number is Required.");
            }

            if (phoneNumber.Length < 10)
            {
                return (false, "Phone Number must be at least 10 characters.");
            }
            return (true, string.Empty);
        }

        // Date of Birth Validation.
        public (bool isValid, string errorMessage) validateDateOfBirth(DateTime dateOfBirth)
        {
            //if It's Empty
            if (dateOfBirth == default(DateTime))
            {
                return (false, "Date of Birth is Required.");
            }
            //if the date in future
            if (dateOfBirth > DateTime.Now)
            {
                return (false, "Date of Birth cannot be in the future.");
            }

            return (true, string.Empty);
        }

        //Gender Validation.
        public static (bool isValid, string errorMessage) validateGender(bool isMaleChecked, bool isFemaleChecked, bool isOtherChecked)
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
    }

}