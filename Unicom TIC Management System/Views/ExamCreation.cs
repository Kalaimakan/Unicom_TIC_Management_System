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
    public partial class ExamCreation : Form
    {
        private int selectedExamId = -1;
        Exam exam = new Exam();
        Course addcourse = new Course();
        Subject addsubject = new Subject();
        ExamController examController = new ExamController();
        SubjectController subjectController = new SubjectController();
        CourseController courseController = new CourseController();
        public ExamCreation()
        {
            InitializeComponent();
            clearFields();
            loadExamData();
            loadComboBox();
        }

        public void clearFields()
        {
            textBoxExamName.Clear();
            comboBoxExamType.SelectedIndex = -1;
            dateTimePickerExamDate.Value = DateTime.Now;
            comboBoxCourse.SelectedIndex = -1;
            comboBoxSubject.SelectedIndex = -1;
            labelFillExamName.Visible = false;
            labelFillExamType.Visible = false;
            labelFillExamDate.Visible = false;
            labelFillCourse.Visible = false;
            labelFillSubject.Visible = false;
        }

        public void loadComboBox()
        {
            // Load courses into the comboBoxCourse
            var course = courseController.viewCourse();
            comboBoxCourse.DataSource = course;
            comboBoxCourse.DisplayMember = "Course_Name"; 
            comboBoxCourse.ValueMember = "Course_Id"; 
            // Load subjects into the comboBoxSubject
            var subject= subjectController.GetSubjects();
            comboBoxSubject.DataSource = subject;
            comboBoxSubject.DisplayMember = "Subject_Name"; 
            comboBoxSubject.ValueMember = "Subject_Id"; 
        }

        public void loadExamData()
        {
            var exams=examController.ViewExams();
            dataGridViewExams.DataSource = exams;
            dataGridViewExams.Columns["Subject_Id"].Visible = false; // Hide the Exam_Id column
            dataGridViewExams.Columns["Course_Id"].Visible = false; // Hide the Course_Id column
        }
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            bool isValid = true;
            exam.Exam_Name = textBoxExamName.Text.Trim();
            exam.Exam_Type = comboBoxExamType.SelectedItem?.ToString();
            exam.Exam_Date = dateTimePickerExamDate.Value.ToString("yyyy-MM-dd");
            exam.Course_Id = Convert.ToInt32(comboBoxCourse.SelectedValue);
            exam.Subject_Id = Convert.ToInt32(comboBoxSubject.SelectedValue);

            if (string.IsNullOrEmpty(exam.Exam_Name))
            {
                labelFillExamName.Text = "Exam Name is required.";
                labelFillExamName.Visible = true;
                isValid = false;
            }
            else
            {
                labelFillExamName.Visible = false;
            }
            if (exam.Exam_Type == null)
            {
                labelFillExamType.Text = "Exam Type is required.";
                labelFillExamType.Visible = true;
                isValid = false;
            }
            else
            {
                labelFillExamType.Visible = false;
            }
            if (string.IsNullOrEmpty(exam.Exam_Date))
            {
                labelFillExamDate.Text = "Exam Date is required.";
                labelFillExamDate.Visible = true;
                isValid = false;
            }
            else
            {
                labelFillExamDate.Visible = false;
            }
            if (exam.Course_Id <= 0)
            {
                labelFillCourse.Text = "Course is required.";
                labelFillCourse.Visible = true;
                isValid = false;
            }
            else
            {
                labelFillCourse.Visible = false;
            }
            if (exam.Subject_Id <= 0)
            {
                labelFillSubject.Text = "Subject is required.";
                labelFillSubject.Visible = true;
                isValid = false;
            }
            else
            {
                labelFillSubject.Visible = false;
            }
            if (isValid)
            {
                try
                {
                    examController.CreateExam(exam);
                    MessageBox.Show("Exam created successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred while creating the exam: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                clearFields();
                loadExamData();
            }

        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            if (selectedExamId == -1)
            {
                MessageBox.Show("Please select an exam to update.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            exam.Exam_Id = selectedExamId;
            exam.Exam_Name = textBoxExamName.Text.Trim();
            exam.Exam_Type = comboBoxExamType.SelectedItem?.ToString();
            exam.Exam_Date = dateTimePickerExamDate.Value.ToString("yyyy-MM-dd");
            exam.Course_Id = Convert.ToInt32(comboBoxCourse.SelectedValue);
            exam.Subject_Id = Convert.ToInt32(comboBoxSubject.SelectedValue);

            bool isValid = true;

            if (string.IsNullOrEmpty(exam.Exam_Name))
            {
                labelFillExamName.Text = "Exam Name is required.";
                labelFillExamName.Visible = true;
                isValid = false;
            }
            else labelFillExamName.Visible = false;

            if (exam.Exam_Type == null)
            {
                labelFillExamType.Text = "Exam Type is required.";
                labelFillExamType.Visible = true;
                isValid = false;
            }
            else labelFillExamType.Visible = false;

            if (string.IsNullOrEmpty(exam.Exam_Date))
            {
                labelFillExamDate.Text = "Exam Date is required.";
                labelFillExamDate.Visible = true;
                isValid = false;
            }
            else labelFillExamDate.Visible = false;

            if (exam.Course_Id <= 0)
            {
                labelFillCourse.Text = "Course is required.";
                labelFillCourse.Visible = true;
                isValid = false;
            }
            else labelFillCourse.Visible = false;

            if (exam.Subject_Id <= 0)
            {
                labelFillSubject.Text = "Subject is required.";
                labelFillSubject.Visible = true;
                isValid = false;
            }
            else labelFillSubject.Visible = false;

            if (isValid)
            {
                try
                {
                    examController.UpdateExam(exam);
                    MessageBox.Show("Exam updated successfully.", "Updated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    clearFields();
                    loadExamData();
                    selectedExamId = -1;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred while updating the exam: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void dataGridViewExams_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {

            if (e.RowIndex >= 0)
            {
                DataGridViewRow selectedRow = dataGridViewExams.Rows[e.RowIndex];
                selectedExamId = Convert.ToInt32(selectedRow.Cells["Exam_Id"].Value);

                textBoxExamName.Text = selectedRow.Cells["Exam_Name"].Value?.ToString();
                comboBoxExamType.SelectedItem = selectedRow.Cells["Exam_Type"].Value?.ToString();

                if (DateTime.TryParse(selectedRow.Cells["Exam_Date"].Value?.ToString(), out DateTime examDate))
                {
                    dateTimePickerExamDate.Value = examDate;
                }

                comboBoxCourse.SelectedValue = Convert.ToInt32(selectedRow.Cells["Course_Id"].Value);
                comboBoxSubject.SelectedValue = Convert.ToInt32(selectedRow.Cells["Subject_Id"].Value);
            }

        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (selectedExamId == -1)
            {
                MessageBox.Show("Please select an exam to delete.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var confirmResult = MessageBox.Show("Are you sure you want to delete this exam?",
                                                "Confirm Delete",
                                                MessageBoxButtons.YesNo,
                                                MessageBoxIcon.Question);

            if (confirmResult == DialogResult.Yes)
            {
                try
                {
                    examController.DeleteExam(selectedExamId);
                    MessageBox.Show("Exam deleted successfully.", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    clearFields();
                    loadExamData();
                    selectedExamId = -1;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred while deleting the exam: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
