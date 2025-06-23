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
    public partial class TimetableManagement: Form
    {
        TimeTable timeTable = new TimeTable();
        Subject subject = new Subject();
        Course course = new Course();
        Department department = new Department();
        CourseController courseController = new CourseController();
        DepartmentController departmentController = new DepartmentController();
        SubjectController subjectController = new SubjectController();  

        public TimetableManagement()
        {
            InitializeComponent();
            loadCourseCombo();
            loadDepartmentCombo();
            dateTimePickerStartDate.Format = DateTimePickerFormat.Custom;
            dateTimePickerStartDate.CustomFormat = "hh:mm";
            dateTimePickerStartDate.ShowUpDown = true;
            dateTimePickerEndDate.Format = DateTimePickerFormat.Custom;
            dateTimePickerEndDate.CustomFormat = "hh:mm";
            dateTimePickerEndDate.ShowUpDown = true;
        }

        public void loadSubjectCombo()
        {
            var subjects = subjectController.GetSubjects();
            comboBoxSubject.DataSource = subjects;
            comboBoxSubject.DisplayMember = "Subject_Name";
            comboBoxSubject.ValueMember = "Subject_Id";
            comboBoxSubject.SelectedIndex = -1;
        }
        public void loadCourseCombo()
        {
            var Course= courseController.viewCourse();
            comboBoxCourse.DataSource = Course;
            comboBoxCourse.DisplayMember = "Course_Name";
            comboBoxCourse.ValueMember = "Course_Id";
            comboBoxCourse.SelectedIndex = -1;
        }

        public void loadDepartmentCombo()
        {
            var departments = departmentController.GetAllDepartment();
            comboBoxDepartment.DataSource = departments;
            comboBoxDepartment.DisplayMember = "Department_Name";
            comboBoxDepartment.ValueMember = "Id";
            comboBoxDepartment.SelectedIndex = -1;
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            if (!dateTimePickerDate.Checked)
            {
                labelFillDate.Text = "Please select a date.";
                labelFillDate.Visible = true;
            }
            else
            {
                labelFillDate.Visible = false;
            }
            if (!dateTimePickerStartDate.Checked)
            {
                labelFillStartTime.Text = "Please select a start time.";
                labelFillStartTime.Visible = true;
            }
            else
            {
                labelFillStartTime.Visible = false;
            }
            if (!dateTimePickerEndDate.Checked)
            {
                labelFillEndTime.Text = "Please select an end time.";
                labelFillEndTime.Visible = true;
            }
            else
            {
                labelFillEndTime.Visible = false;
            }
            if (comboBoxCourse.SelectedIndex < 0)
            {
                labelFillCourse.Text = "Please select a course.";
                labelFillCourse.Visible = true;
            }
            else
            {
                labelFillCourse.Visible = false;
            }
            if (comboBoxSubject.SelectedIndex<0)
            {
               labelFillSubject.Text = "Please select a subject.";
                labelFillSubject.Visible = true;
            }
            else
            {
                labelFillSubject.Visible = false;
            }
            if (comboBoxDepartment.SelectedIndex < 0)
            {
                labelFillDepartment.Text = "Please select a department.";
                labelFillDepartment.Visible = true;
            }
            else
            {
                labelFillDepartment.Visible = false;
            }

            timeTable.Date = dateTimePickerDate.Value.ToString("yyyy-MM-dd");

            

            timeTable.Start_Time = dateTimePickerStartDate.Value.ToString();
            timeTable.End_Time = dateTimePickerEndDate.Value.ToString();
            subject = (Subject)comboBoxSubject.SelectedItem;
            course = (Course)comboBoxCourse.SelectedItem;
            department = (Department)comboBoxDepartment.SelectedItem;


        }
    }
}
