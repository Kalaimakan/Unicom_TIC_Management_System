using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Lifetime;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Unicom_TIC_Management_System.Controllers;
using Unicom_TIC_Management_System.Models;

namespace Unicom_TIC_Management_System.Views
{
    public partial class CourseCreatio : Form
    {
        BindingSource courseBindingSource = new BindingSource();
        Course addCourse = new Course();
        CourseController courseController = new CourseController();
        public CourseCreatio()
        {
            InitializeComponent();
            clearField();
            loadCourse();
            SetPlaceholders();
        }
        public void clearField()
        {
            textBoxAddCourse.Clear();
            comboBoxDepartment.ResetText();
            dateTimePickerStartDate.ResetText();

            labelFillCourse.Visible = false;
            labelFillDepartment.Visible = false;
            comboBoxDepartment.SelectedIndex = -1;


        }

        private void SetPlaceholders()
        {
            ApplyPlaceholder(textBoxAddCourse, "Enter the Course Name");
        }

        // apply placeholder to TextBox
        private void ApplyPlaceholder(TextBox textBox, string placeholder)
        {
            textBox.Text = placeholder;
            textBox.ForeColor = Color.Gray;

            textBox.Enter += (s, e) =>
            {
                if (textBox.Text == placeholder)
                {
                    textBox.Text = "";
                    textBox.ForeColor = Color.Black;

                }
            };

            textBox.Leave += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(textBox.Text))
                {
                    textBox.Text = placeholder;
                    textBox.ForeColor = Color.Gray;

                }
            };


        }
        public void loadCourse()
        {
            List<Course> courses = courseController.viewCourse();
            courseBindingSource.DataSource = courses;
            dataGridViewCourse.DataSource = courseBindingSource;
            dataGridViewCourse.ClearSelection();
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            // Validate the input fields
            var courseNameValidation = courseController.validateName(textBoxAddCourse.Text.Trim(), "Course Name");
            labelFillCourse.Text = courseNameValidation.errorMessage;
            labelFillCourse.Visible = !courseNameValidation.isValid;

            if (comboBoxDepartment.SelectedIndex < 0)
            {
                labelFillDepartment.Visible = true;
                labelFillDepartment.Text = "Please select a department.";
                return;
            }
            else
            {
                labelFillDepartment.Visible = false;
            }

            var startDateValidation = courseController.validateStartDate(dateTimePickerStartDate.Value);
            labelfillStartDate.Text = startDateValidation.errorMessage;
            labelfillStartDate.Visible = !startDateValidation.isValid;
            if (!courseNameValidation.isValid || !startDateValidation.isValid)
            {
                return;
            }
            addCourse.Course_Name = textBoxAddCourse.Text.Trim();
            addCourse.StartDate = dateTimePickerStartDate.Value.ToString();
            addCourse.Department = comboBoxDepartment.SelectedItem?.ToString();
            textBoxAddCourse.ForeColor = Color.Black;

            courseController.addCourse(addCourse);
            loadCourse();
            clearField();
        }
        public void getItemInField()
        {
            if (dataGridViewCourse.SelectedRows.Count > 0)
            {

                DataGridViewRow selectedRow = dataGridViewCourse.SelectedRows[0];
                textBoxAddCourse.ForeColor = Color.Black;
                comboBoxDepartment.ForeColor = Color.Black;

                textBoxAddCourse.Text = selectedRow.Cells["Course_Name"].Value?.ToString() ?? "";
                comboBoxDepartment.SelectedItem = selectedRow.Cells["Department"].Value?.ToString() ?? "";
                var startDateValue = selectedRow.Cells["StartDate"].Value;
                if (startDateValue != null && DateTime.TryParse(startDateValue.ToString(), out DateTime startDate))
                {
                    dateTimePickerStartDate.Value = startDate;
                }
                else
                {
                    dateTimePickerStartDate.Value = DateTime.Today;
                }
            }
        }
        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (dataGridViewCourse.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a course to delete.");
                return;
            }
            if (dataGridViewCourse.SelectedRows.Count > 0)
            {

                //Get the Delete Id
                int courseId = Convert.ToInt32(dataGridViewCourse.SelectedRows[0].Cells["Course_Id"].Value);

                //Ask confiromation to Delete
                DialogResult result = MessageBox.Show(
                                $"Are you sure you want to delete the {addCourse.Course_Name} course  ?",
                                "Confirm Delete",
                                MessageBoxButtons.YesNo
                                    );
                if (result == DialogResult.Yes)
                {
                    courseController.deleteCourse(courseId);
                    MessageBox.Show($"{addCourse.Course_Name} Course is Successfully Deleted .");
                    clearField();
                    loadCourse();

                }
                else if (result == DialogResult.No)
                {
                    MessageBox.Show("Course deletion was cancelled.");
                }
                else
                {
                    MessageBox.Show("Please Select the Course to Delete.");
                }
            }
        }

        private void dataGridViewCourse_SelectionChanged(object sender, EventArgs e)
        {
            getItemInField();
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            //if (textBoxAddCourse.Text != "Enter the Course Name" )
            clearField();
        }

        private void textBoxSearch_TextChanged(object sender, EventArgs e)
        {
            string searchText = textBoxSearch.Text.Trim().ToLower();

            List<Course> filteredCourses = courseController.viewCourse().Where(c => c.Course_Name.ToLower().StartsWith(searchText)).ToList();

            courseBindingSource.DataSource = filteredCourses;
        }
    }
}
