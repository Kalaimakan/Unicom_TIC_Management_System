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
    public partial class SubjectCreation : Form
    {
        Subject addSubject = new Subject();
        Course addCourse = new Course();
        CourseController courseController = new CourseController();
        Department addDepartment = new Department();
        DepartmentController departmentController = new DepartmentController();
        SubjectController subjectController = new SubjectController();
        Course_SubjectController courseSubjectController = new Course_SubjectController();
        public SubjectCreation()
        {
            InitializeComponent();
            SetPlaceholder();
            clearFields();
            LoadSubjects();
            LoadCourseCombo();
            LoadDepartmentCombo();
        }

        public void SetPlaceholder()
        {
            ApplyPlaceholder(textBoxSubjectName, "Enter Subject Name");
        }
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
        public void clearFields()
        {
            textBoxSubjectName.Text = "Enter Subject Name";
            comboBoxCourseName.ResetText();
            comboBoxDepartmentName.ResetText();
            comboBoxCourseName.SelectedIndex = -1;
            comboBoxDepartmentName.SelectedIndex = -1;
            labelFillSubjectName.Visible = false;
            labelFillCourseName.Visible = false;
            labelFillDepartmentName.Visible = false;
        }

        public void LoadSubjects()
        {
            try
            {
                List<Subject> subjects = subjectController.GetSubjects();
                dataGridViewSubject.DataSource = subjects;
                dataGridViewSubject.Columns["Subject_Id"].HeaderText = "Subject Id";
                dataGridViewSubject.Columns["Course_Id"].Visible = false;
                dataGridViewSubject.Columns["Department_Id"].Visible = false;
                dataGridViewSubject.Columns["Course_Name"].HeaderText = "Course";
                dataGridViewSubject.Columns["Department_Name"].HeaderText = "Department";
                dataGridViewSubject.Columns["Subject_Name"].HeaderText = "Subject";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading subjects: " + ex.Message);
            }
        }

        public void LoadCourseCombo()
        {
            List<Course> courses = courseController.viewCourse();
            comboBoxCourseName.DataSource = courses;
            comboBoxCourseName.DisplayMember = "Course_Name";
            comboBoxCourseName.ValueMember = "Course_Id";
        }

        public void LoadDepartmentCombo()
        {
            List<Department> departments = departmentController.GetAllDepartment();
            comboBoxDepartmentName.DataSource = departments;
            comboBoxDepartmentName.DisplayMember = "Department_Name";
            comboBoxDepartmentName.ValueMember = "Id";
        }
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            addSubject.Subject_Name = textBoxSubjectName.Text.Trim();
            if (string.IsNullOrWhiteSpace(addSubject.Subject_Name) || addSubject.Subject_Name == "Enter Subject Name")
            {
                labelFillSubjectName.Text = "Please enter a subject name";
                labelFillSubjectName.Visible = true;
                return;
            }
            else
            {
                labelFillSubjectName.Visible = false;
            }
            if (comboBoxCourseName.SelectedIndex == -1)
            {
                labelFillCourseName.Text = "Please select a course";
                labelFillCourseName.Visible = true;
                return;
            }
            else
            {
                labelFillCourseName.Visible = false;
            }
            if (comboBoxDepartmentName.SelectedIndex == -1)
            {
                labelFillCourseName.Text = "Please select a department";
                labelFillCourseName.Visible = true;
                return;
            }
            else
            {
                labelFillCourseName.Visible = false;
            }
            //Assain values ti subject object
            addDepartment = (Department)comboBoxDepartmentName.SelectedItem;
            addCourse = (Course)comboBoxCourseName.SelectedItem;

            try
            {
                courseSubjectController.AddCourseSubject(addCourse, addSubject, addDepartment);
                MessageBox.Show($"Subject {addSubject.Subject_Name} added successfully ");
                clearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding subject: " + ex.Message);
            }
            LoadSubjects();
            clearFields();
        }

        private void dataGridViewSubject_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (dataGridViewSubject.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dataGridViewSubject.SelectedRows[0];
                textBoxSubjectName.Text = row.Cells["Subject_Name"].Value?.ToString();
                textBoxSubjectName.ForeColor = Color.Black;

                // Set Course and Department combo boxes by matching values
                string courseName = row.Cells["Course_Name"].Value?.ToString();
                string deptName = row.Cells["Department_Name"].Value?.ToString();

                comboBoxCourseName.SelectedIndex = comboBoxCourseName.FindStringExact(courseName);
                comboBoxDepartmentName.SelectedIndex = comboBoxDepartmentName.FindStringExact(deptName);
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (dataGridViewSubject.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dataGridViewSubject.SelectedRows[0];
                int subjectId = Convert.ToInt32(row.Cells["Subject_Id"].Value);
                DialogResult result = MessageBox.Show(
                                    $"Are you sure you want to delete the {addSubject.Subject_Name} Subject ?",
                                    "Confirm Delete",
                                    MessageBoxButtons.YesNo
                                        );
                if (result == DialogResult.Yes)
                {
                    subjectController.DeleteSubject(subjectId);
                    MessageBox.Show($"{addSubject.Subject_Name} Subject is Successfully Deleted .");
                    clearFields();
                    LoadSubjects();

                }
                else if (result == DialogResult.No)
                {
                    MessageBox.Show("Course deletion was cancelled.");
                }
            }
            else
            {
                MessageBox.Show("Please select a subject to delete.");

            }
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            clearFields();
        }
    }
}