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
        private Student selectedStudent = null;
        public EditStudent(string mode)
        {
            InitializeComponent();
            loadStudentData();
            clearField();
            if (mode == "Update")
            {
                labelheading.Text = "Update Students";
                buttonUpdate.Visible = true;
            }
            if (mode == "Delete")
            {
                labelheading.Text = "Delete Student";
                buttonDelete.Visible = true;
            }
        }

        public void loadStudentData()
        {
            List<Student> students = studentController.GetAllStudents();
            dataGridViewUpdate.DataSource = students;
            dataGridViewUpdate.Columns["Department_Id"].Visible = false;
            dataGridViewUpdate.Columns["Course_Id"].Visible = false;
            dataGridViewUpdate.Columns["User_Name"].Visible=false;
            dataGridViewUpdate.Columns["Password"].Visible=false;
            dataGridViewUpdate.Columns["User_Email"].Visible=false;
        }

        public void clearField()
        {
            textBoxFirstName.Clear();
            textBoxLastName.Clear();
            textBoxEmail.Clear();
            textBoxPhoneNumber.Clear();
            textBoxAddress.Clear();
            checkBoxMale.Checked = false;
            checkBoxFemale.Checked = false;
        }
        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            if (selectedStudent == null)
            {
                MessageBox.Show("Please select a Student to Update first.");
                return;
            }

            // Get new values from the form
            string newFirstName = textBoxFirstName.Text.Trim();
            string newLastName = textBoxLastName.Text.Trim();
            string newEmail = textBoxEmail.Text.Trim();
            string newPhone = textBoxPhoneNumber.Text.Trim();
            string newAddress = textBoxAddress.Text.Trim();
            string newGender = checkBoxMale.Checked ? "Male" : checkBoxFemale.Checked ? "Female" : "Other";

            // Check for empty fields
            if (string.IsNullOrWhiteSpace(newFirstName) || string.IsNullOrWhiteSpace(newLastName) ||
                string.IsNullOrWhiteSpace(newEmail) || string.IsNullOrWhiteSpace(newPhone) ||
                string.IsNullOrWhiteSpace(newAddress) || string.IsNullOrWhiteSpace(newGender))
            {
                MessageBox.Show("Please fill all required fields.");
                return;
            }

            // Check for changes
            if (selectedStudent.First_Name == newFirstName &&
                selectedStudent.Last_Name == newLastName &&
                selectedStudent.Email == newEmail &&
                selectedStudent.PhoneNumber == newPhone &&
                selectedStudent.Address == newAddress &&
                selectedStudent.Gender == newGender)
            {
                MessageBox.Show("No changes detected to update.");
                return;
            }

            // Assign new values
            student.First_Name = newFirstName;
            student.Last_Name = newLastName;
            student.Email = newEmail;
            student.PhoneNumber = newPhone;
            student.Address = newAddress;
            student.Gender = newGender;
            student.Student_Id = selectedStudent.Student_Id;

            // Confirm update
            if (MessageBox.Show($"Confirm update {student.Last_Name}?", "Confirm", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    bool success = studentController.UpdateAdmin(student, user);
                    if (success)
                    {
                        MessageBox.Show($"{student.Last_Name} updated successfully!");
                        loadStudentData();
                        clearField();
                        selectedStudent = null;
                    }
                    else
                    {
                        MessageBox.Show("Failed to update student.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error updating lecturer: {ex.Message}");
                }
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

        }

        private void dataGridViewUpdate_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridViewRow row = dataGridViewUpdate.Rows[e.RowIndex];

            textBoxFirstName.Text = row.Cells["First_Name"].Value.ToString();
            textBoxLastName.Text = row.Cells["Last_Name"].Value.ToString();
            textBoxEmail.Text = row.Cells["Email"].Value.ToString();
            textBoxPhoneNumber.Text = row.Cells["PhoneNumber"].Value.ToString();
            textBoxAddress.Text = row.Cells["Address"].Value.ToString();

            string gender = row.Cells["Gender"].Value.ToString();
            checkBoxMale.Checked = gender.Equals("Male", StringComparison.OrdinalIgnoreCase);
            checkBoxFemale.Checked = gender.Equals("Female", StringComparison.OrdinalIgnoreCase);

            student.Student_Id = Convert.ToInt32(row.Cells["Student_Id"].Value);

            selectedStudent = new Student
            {
                Student_Id = Convert.ToInt32(row.Cells["Student_Id"].Value),
                First_Name = textBoxFirstName.Text,
                Last_Name = textBoxLastName.Text,
                Email = textBoxEmail.Text,
                PhoneNumber = textBoxPhoneNumber.Text,
                Address = textBoxAddress.Text,
                Gender = gender
            };

        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
