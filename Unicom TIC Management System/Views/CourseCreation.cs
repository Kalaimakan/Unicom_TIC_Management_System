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
    public partial class CourseCreation: Form
    {
        CourseController courseController = new CourseController();
        Course addCourse = new Course();
        public CourseCreation()
        {
            InitializeComponent();
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            addCourse.Course_Name = textBoxAddCourse.Text.Trim();
            addCourse.StartDate = dateTimePickerStartDate.Value.ToString();

            courseController.addCourse(addCourse);
        }

        private void dataGridViewCourse_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }
    }
}
