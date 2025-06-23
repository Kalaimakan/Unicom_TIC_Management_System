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
    public partial class ViewStudents : Form
    {
        StudentController studentController = new StudentController();
        public ViewStudents()
        {
            InitializeComponent();
            loadStudentData();
        }

        public void loadStudentData()
        {
            List<Student> students = studentController.GetAllStudents();
            dataGridViewStudents.DataSource = students;
            dataGridViewStudents.RowHeadersVisible = false;
            dataGridViewStudents.Columns["Department_Id"].Visible = false;
            dataGridViewStudents.Columns["Course_Id"].Visible = false;
            dataGridViewStudents.Columns["User_Name"].Visible = false;
            dataGridViewStudents.Columns["Password"].Visible = false;
            dataGridViewStudents.Columns["User_Email"].Visible = false;
        }
    }
}
