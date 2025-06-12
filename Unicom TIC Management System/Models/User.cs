using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unicom_TIC_Management_System.Models
{
    class User
    {
        public int User_Id { get; set; }
        public string User_Name { get; set; }
        public string Password { get; set; }
        public string User_Email { get; set; }
        public string User_Role { get; set; }
        public DateTime User_CreatedAt { get; set; }
        public DateTime User_UpdatedAt { get; set; }
    }
}