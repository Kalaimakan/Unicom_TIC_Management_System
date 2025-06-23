using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Unicom_TIC_Management_System.Controllers;
using Unicom_TIC_Management_System.Models;

namespace Unicom_TIC_Management_System.Views
{
    public partial class ViewLecturer: Form
    {
        LecturerController lecturerController = new LecturerController();
        public ViewLecturer()
        {
            InitializeComponent();
            loadLecturerData();
        }
        public void loadLecturerData()
        {
            // Assuming you have a method to get all lecturers
            List<Lecturer> lecturers = lecturerController.GetAllLecturer();
            dataGridViewLecturers.DataSource = lecturers;
            dataGridViewLecturers.Columns["Subject_Id"].Visible = false; 
            dataGridViewLecturers.Columns["Subject_Name"].Visible = false; 
            dataGridViewLecturers.RowHeadersVisible = false;
        }
    }
}
