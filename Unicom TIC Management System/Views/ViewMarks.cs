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
    public partial class ViewMarks: Form
    {
        MarkController markController=new MarkController();
        public ViewMarks()
        {
            InitializeComponent();
            loadMarksData();
        }

        public void loadMarksData()
        {
            List<Mark> marks = markController.GetMarks();
            dataGridViewMarks.DataSource = marks;
            //dataGridViewMarks.Columns["Subject_Id"].Visible = false;
            dataGridViewMarks.Columns["Marks_Value"].HeaderText = "Mark";
            dataGridViewMarks.Columns["Student_Id"].HeaderText = "Student ID";
            dataGridViewMarks.Columns["Exam_Id"].HeaderText = "Exam Id";
        }
    }
}
