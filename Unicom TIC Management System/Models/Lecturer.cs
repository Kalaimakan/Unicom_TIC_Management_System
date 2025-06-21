using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unicom_TIC_Management_System.Models
{
    class Lecturer
    {
        public int Lecturer_Id { get; set; }
        public string Employee_Id { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public string Date_of_Birth { get; set; }
        public double salary { get; set; }
        public int Subject_Id { get; set; }
        public string Subject_Name { get; set; }
    }
}
