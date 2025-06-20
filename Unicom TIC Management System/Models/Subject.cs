using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unicom_TIC_Management_System.Models
{
    class Subject
    {
        public int Subject_Id { get; set; }
        public string Subject_Name { get; set; }
        public int Course_Id { get; set; }
        public string Course_Name { get; set; }
        public int Department_Id { get; set; }
        public string Department_Name { get; set; }
    }
}
