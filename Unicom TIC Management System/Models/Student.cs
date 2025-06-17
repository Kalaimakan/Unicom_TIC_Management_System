using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unicom_TIC_Management_System.Models
{
    class Student
    {
        public int Student_Id { get; set; }
        public string Admission_No { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public string PhoneNumber { get; set; }
        public string Date_of_Birth { get; set; }
        public string Address { get; set; } 
        public string Entrolld_Course { get; set; }
        public int Course_Id { get; set; }  
    }
}
