using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unicom_TIC_Management_System.Models
{
    class Exam
    {
        public int Exam_Id { get; set; }
        public string Exam_Name { get; set; }
        public string Exam_Type { get; set; }
        public string Exam_Date { get; set; }
        public int Course_Id { get; set; }
        public string Course_Name { get; set; }
        public int Subject_Id { get; set; }
        public string Subject_Name { get; set; }
    }
}
