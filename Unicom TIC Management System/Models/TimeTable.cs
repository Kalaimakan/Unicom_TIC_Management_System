using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unicom_TIC_Management_System.Models
{
    class TimeTable
    {
        public int Timetable_Id { get; set; }
        public string Date {  get; set; }
        public string Start_Time { get; set; }
        public string End_Time { get; set; }
        public int Subject_Id { get; set; }
        public int Course_Id { get; set; }
        public int Department_Id { get; set; }
        public int Room_Id {  get; set; }
        public string Subject_Name { get; set; }
        public string Course_Name { get; set; }
        public string Department_Name { get; set; }
        public string Room_Name { get; set; }

    }
}
