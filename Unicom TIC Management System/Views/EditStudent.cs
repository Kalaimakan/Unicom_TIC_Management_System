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
    public partial class EditStudent : Form
    {
        StudentController studentController = new StudentController();
        Student student = new Student();
        User user = new User();
        public EditStudent()
        {
            InitializeComponent();
            loadStudentData();
            clearField();
        }

        public void loadStudentData()
        {
            List<Student> students = studentController.GetAllStudents();
            dataGridViewUpdate.DataSource = students;
            dataGridViewUpdate.Columns["Department_Id"].Visible = false;
            dataGridViewUpdate.Columns["Course_Id"].Visible = false;
        }

        public void clearField()
        {
            textBoxFirstName.Clear();
            textBoxLastName.Clear();
            textBoxEmail.Clear();
            textBoxPhoneNumber.Clear();
            textBoxUserName.Clear();
            textBoxPassword.Clear();
            textBoxAddress.Clear();
            checkBoxMale.Checked = false;
            checkBoxFemale.Checked = false;
        }
        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                // Validate selection
                if (dataGridViewUpdate.SelectedRows.Count == 0 ||
                dataGridViewUpdate.SelectedRows[0].Cells["Student_Id"]?.Value == null)
                {
                    MessageBox.Show("Please select a Student to update");
                    return;
                }
                //get data from form
                student.First_Name = textBoxFirstName.Text.Trim();
                student.Last_Name = textBoxLastName.Text.Trim();
                student.User_Name = textBoxUserName.Text.Trim();
                student.Password = textBoxPassword.Text.Trim();
                student.Email = textBoxEmail.Text.Trim();
                student.PhoneNumber = textBoxPhoneNumber.Text.Trim();
                student.Address = textBoxAddress.Text.Trim();
                student.Gender = checkBoxMale.Checked ? "Male" : checkBoxFemale.Checked ? "Female" : "Other";

                user.User_Name = textBoxUserName.Text.Trim();
                user.Password = textBoxPassword.Text.Trim();
                user.User_Email = textBoxEmail.Text.Trim();

                //validate data
                if (string.IsNullOrEmpty(student.First_Name) || string.IsNullOrEmpty(student.Last_Name) ||
                    string.IsNullOrEmpty(student.User_Name) || string.IsNullOrEmpty(student.Password) ||
                    string.IsNullOrEmpty(student.Email) || string.IsNullOrEmpty(student.PhoneNumber) ||
                    string.IsNullOrEmpty(student.Address))
                {
                    MessageBox.Show("Please fill all fields");
                    return;
                }
                // Confirm update
                if (MessageBox.Show($"Confirm update {student.Last_Name} ?", "Confirm", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    bool success = studentController.UpdateAdmin(student, user);
                    if (success)
                    {
                        MessageBox.Show($"{student.Last_Name} updated successfully!");
                        loadStudentData();
                        clearField();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating Student: {ex.Message}");
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridViewUpdate.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Please select an Student to delete", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int studentId = Convert.ToInt32(dataGridViewUpdate.SelectedRows[0].Cells["Student_Id"].Value);
                string studentName = dataGridViewUpdate.SelectedRows[0].Cells["Last_Name"].Value.ToString();

                var confirmResult = MessageBox.Show($"Are you sure you want to delete {studentName}?",
                                                  "Confirm Deletion",
                                                  MessageBoxButtons.YesNo,
                                                  MessageBoxIcon.Warning);

                if (confirmResult == DialogResult.Yes)
                {
                    studentController.DeleteStudent(studentId);

                    MessageBox.Show($"Delete {studentName} Successful", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting Student : {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            loadStudentData();
            clearField();
        }

        private void buttonTogglePassword_Click(object sender, EventArgs e)
        {
            textBoxPassword.UseSystemPasswordChar = !textBoxPassword.UseSystemPasswordChar;
            buttonTogglePassword.Text = textBoxPassword.UseSystemPasswordChar ? "👁️" : "🔒";
        }

        private void dataGridViewUpdate_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridViewRow row = dataGridViewUpdate.Rows[e.RowIndex];

            textBoxFirstName.Text = row.Cells["First_Name"].Value.ToString();
            textBoxLastName.Text = row.Cells["Last_Name"].Value.ToString();
            textBoxUserName.Text = row.Cells["User_Name"].Value.ToString();
            textBoxPassword.Text = row.Cells["Password"].Value.ToString();
            textBoxEmail.Text = row.Cells["Email"].Value.ToString();
            textBoxPhoneNumber.Text = row.Cells["PhoneNumber"].Value.ToString();
            textBoxAddress.Text = row.Cells["Address"].Value.ToString();

            string gender = row.Cells["Gender"].Value.ToString();
            checkBoxMale.Checked = gender.Equals("Male", StringComparison.OrdinalIgnoreCase);
            checkBoxFemale.Checked = gender.Equals("Female", StringComparison.OrdinalIgnoreCase);

            student.Student_Id = Convert.ToInt32(row.Cells["Student_Id"].Value);

        }
    }
    }
