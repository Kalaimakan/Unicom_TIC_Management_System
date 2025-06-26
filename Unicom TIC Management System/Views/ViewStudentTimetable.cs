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

namespace Unicom_TIC_Management_System.Views
{
    public partial class ViewStudentTimetable: Form
    {
        TimetableController timetableController=new TimetableController();
        public ViewStudentTimetable()
        {
            InitializeComponent();
            loadTimetableData();
        }

        public void loadTimetableData()
        {
            var data = timetableController.GetAllTimetables();
            dataGridViewTimetableview.DataSource = data;
            dataGridViewTimetableview.RowHeadersVisible = false;
            dataGridViewTimetableview.Columns["Subject_Id"].Visible = false;
            dataGridViewTimetableview.Columns["Course_Id"].Visible = false;
            dataGridViewTimetableview.Columns["Department_Id"].Visible = false;
            dataGridViewTimetableview.Columns["Room_Id"].Visible = false;
        }
    }
}
