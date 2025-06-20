using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Unicom_TIC_Management_System.Repositories;
using Unicom_TIC_Management_System.Views;

namespace Unicom_TIC_Management_System
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Migration.createTable();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new AdminCreation());
            //Application.Run(new StudentCreation());
            //Application.Run(new StaffCreation());
            //Application.Run(new LecturerCreation());
            Application.Run(new CourseCreatio());
            //Application.Run(new LoginForm());
            //Application.Run(new DepartmentCeation());
            Application.Run(new SubjectCreation());
        }
    }
}
