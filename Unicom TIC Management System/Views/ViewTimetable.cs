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
    public partial class ViewTimetable: Form
    {
        TimetableController timetableController = new TimetableController();
        public ViewTimetable()
        {
            InitializeComponent();
            loadTimetableData();
        }
        public void loadTimetableData()
        {
            var data = timetableController.GetAllTimetables();
            dataGridViewTimetable.DataSource = data;
            dataGridViewTimetable.RowHeadersVisible = false;
            dataGridViewTimetable.Columns["Subject_Id"].Visible = false;
            dataGridViewTimetable.Columns["Course_Id"].Visible = false;
            dataGridViewTimetable.Columns["Department_Id"].Visible = false;
            dataGridViewTimetable.Columns["Room_Id"].Visible = false;
        }
    }
}
