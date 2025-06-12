using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unicom_TIC_Management_System.Controllers;
using Unicom_TIC_Management_System.Views;

namespace Unicom_TIC_Management_System.Models
{
    class Validation 
    {
        public string nameValidation(string name, string fieldName)
        {
            if (name == "Enter the User Name" || string.IsNullOrWhiteSpace(name))
            {
                return $"{fieldName} is Required.";
            }
            else
            {
                return $"{fieldName} be at least 3 characters long and cannot be empty.";
            }
        }
    }
}
