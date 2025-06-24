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
    public partial class MarkCreation : Form
    {
        Mark mark = new Mark();
        Exam exam = new Exam();
        ExamController examController = new ExamController();
        StudentController studentController = new StudentController();
        MarkController markController = new MarkController();
        public MarkCreation()
        {
            InitializeComponent();
            loadCombox();
            clearFields();
            loadMarksData();
        }
        public void loadCombox()
        {
            var subjects = examController.ViewExams();
            comboBoxSubject.DataSource = subjects;
            comboBoxSubject.DisplayMember = "Subject_Name";
            comboBoxSubject.ValueMember = "Subject_Id";

            var examName = examController.ViewExams();
            comboBoxExamName.DataSource = examName;
            comboBoxExamName.DisplayMember = "Exam_Name";
            comboBoxExamName.ValueMember = "Exam_Id";

            var student = studentController.GetAllStudents();
            comboBoxStudent.DataSource = student;
            comboBoxStudent.DisplayMember = "Last_Name";
            comboBoxStudent.ValueMember = "Student_Id";
        }
        public void clearFields()
        {
            comboBoxExamName.SelectedIndex = -1;
            comboBoxStudent.SelectedIndex = -1;
            comboBoxSubject.SelectedIndex = -1;
            textBoxMark.Clear();
        }

        public void loadMarksData()
        {
            List<Mark> marks = markController.GetMarks();
            dataGridView1.DataSource = marks;
            //dataGridView1.Columns["Subject_Id"].Visible = false;
            dataGridView1.Columns["Marks_Value"].HeaderText = "Mark";
            dataGridView1.Columns["Student_Id"].HeaderText = "Student ID";
            dataGridView1.Columns["Exam_Id"].HeaderText = "Exam Id";
        }
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            bool isValid = true;

            if (comboBoxStudent.SelectedIndex == -1)
            {
                labelFillStudent.Text = "Please select a student.";
                labelFillStudent.Visible = true;
                isValid = false;
            }
            else
            {
                labelFillStudent.Visible = false;
            }

            if (comboBoxExamName.SelectedIndex == -1)
            {
                labelFillExamName.Text = "Please select an exam.";
                labelFillExamName.Visible = true;
                isValid = false;
            }
            else
            {
                labelFillExamName.Visible = false;
            }

            if (comboBoxSubject.SelectedIndex == -1)
            {
                labelFillSubject.Text = "Please select a subject.";
                labelFillSubject.Visible = true;
                isValid = false;
            }
            else
            {
                labelFillSubject.Visible = false;
            }
            if (!int.TryParse(textBoxMark.Text.Trim(), out int markValue) || markValue < 0 || markValue > 100)
            {
                labelFillMark.Text = "Please enter a valid mark between 0 and 100.";
                labelFillMark.Visible = true;
                isValid = false;
            }
            else
            {
                labelFillMark.Visible = false;
            }

            if (!isValid)
                return;

            // Assign values to mark object
            mark.Student_Id = Convert.ToInt32(comboBoxStudent.SelectedValue);
            mark.Exam_Id = Convert.ToInt32(comboBoxExamName.SelectedValue);
            mark.Subject_Id = Convert.ToInt32(comboBoxSubject.SelectedValue);
            mark.Marks_Value = markValue;

            try
            {
                MarkController markController = new MarkController();
                markController.AddMark(mark);
                MessageBox.Show("Mark added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                textBoxMark.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to add mark: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            loadMarksData();
            loadCombox();
            clearFields();
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            if (mark.Mark_Id == 0)
            {
                MessageBox.Show("Please select a row to update from the table.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            bool isValid = true;

            if (comboBoxStudent.SelectedIndex == -1)
            {
                labelFillStudent.Text = "Please select a student.";
                labelFillStudent.Visible = true;
                isValid = false;
            }
            else
            {
                labelFillStudent.Visible = false;
            }

            if (comboBoxExamName.SelectedIndex == -1)
            {
                labelFillExamName.Text = "Please select an exam.";
                labelFillExamName.Visible = true;
                isValid = false;
            }
            else
            {
                labelFillExamName.Visible = false;
            }

            if (comboBoxSubject.SelectedIndex == -1)
            {
                labelFillSubject.Text = "Please select a subject.";
                labelFillSubject.Visible = true;
                isValid = false;
            }
            else
            {
                labelFillSubject.Visible = false;
            }

            if (!int.TryParse(textBoxMark.Text.Trim(), out int markValue) || markValue < 0 || markValue > 100)
            {
                labelFillMark.Text = "Please enter a valid mark between 0 and 100.";
                labelFillMark.Visible = true;
                isValid = false;
            }
            else
            {
                labelFillMark.Visible = false;
            }

            if (!isValid)
                return;

            // Update mark values
            mark.Student_Id = Convert.ToInt32(comboBoxStudent.SelectedValue);
            mark.Exam_Id = Convert.ToInt32(comboBoxExamName.SelectedValue);
            mark.Subject_Id = Convert.ToInt32(comboBoxSubject.SelectedValue);
            mark.Marks_Value = markValue;

            try
            {
                MarkController markController = new MarkController();
                markController.UpdateMark(mark);  // Ensure you implemented UpdateMark
                MessageBox.Show("Mark updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to update mark: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            loadCombox();  
            clearFields();  
            mark.Mark_Id = 0; 
            loadMarksData();

        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0) 
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                mark.Mark_Id = Convert.ToInt32(row.Cells["Mark_Id"].Value);

                comboBoxStudent.SelectedValue = Convert.ToInt32(row.Cells["Student_Id"].Value);
                comboBoxExamName.SelectedValue = Convert.ToInt32(row.Cells["Exam_Id"].Value);
                comboBoxSubject.SelectedValue = Convert.ToInt32(row.Cells["Subject_Id"].Value);
                textBoxMark.Text = row.Cells["Marks_Value"].Value.ToString();
            }
        }
    }
}
