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
    public partial class EditLecturer : Form
    {
        Lecturer lecturer = new Lecturer();
        LecturerController lecturerController = new LecturerController();
        private Lecturer selectedLecturer = null;

        public EditLecturer()
        {
            InitializeComponent();
            loadLecturerData();
            clearField();
        }

        public void loadLecturerData()
        {
            // Assuming you have a method to get all lecturers
            List<Lecturer> lecturers = lecturerController.GetAllLecturer();
            dataGridViewUpdate.DataSource = lecturers;
            dataGridViewUpdate.Columns["Subject_Id"].Visible = false;
            dataGridViewUpdate.Columns["Subject_Name"].Visible = false;
            dataGridViewUpdate.Columns["Employee_Id"].Visible = false;
        }

        public void clearField()
        {
            textBoxFirstName.Clear();
            textBoxLastName.Clear();
            textBoxEmail.Clear();
            textBoxPhoneNumber.Clear();
            textBoxSalary.Clear();
            checkBoxMale.Checked = false;
            checkBoxFemale.Checked = false;
            checkBoxOther.Checked = false;
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            if (selectedLecturer == null)
            {
                MessageBox.Show("Please select a lecturer first.");
                return;
            }

            // Get new values from the form
            string newFirstName = textBoxFirstName.Text.Trim();
            string newLastName = textBoxLastName.Text.Trim();
            string newEmail = textBoxEmail.Text.Trim();
            string newPhone = textBoxPhoneNumber.Text.Trim();
            string newGender = checkBoxMale.Checked ? "Male" : checkBoxFemale.Checked ? "Female" : "Other";
            double newSalary = double.TryParse(textBoxSalary.Text.Trim(), out double salaryVal) ? salaryVal : 0;

            // Check for empty fields
            if (string.IsNullOrWhiteSpace(newFirstName) || string.IsNullOrWhiteSpace(newLastName) ||
                string.IsNullOrWhiteSpace(newEmail) || string.IsNullOrWhiteSpace(newPhone) ||
                string.IsNullOrWhiteSpace(newGender) || newSalary == 0)
            {
                MessageBox.Show("Please fill all required fields.");
                return;
            }

            // Check for changes
            if (selectedLecturer.First_Name == newFirstName &&
                selectedLecturer.Last_Name == newLastName &&
                selectedLecturer.Email == newEmail &&
                selectedLecturer.PhoneNumber == newPhone &&
                selectedLecturer.Gender == newGender &&
                selectedLecturer.salary == newSalary)
            {
                MessageBox.Show("No changes detected to update.");
                return;
            }

            // Assign new values
            lecturer.First_Name = newFirstName;
            lecturer.Last_Name = newLastName;
            lecturer.Email = newEmail;
            lecturer.PhoneNumber = newPhone;
            lecturer.Gender = newGender;
            lecturer.salary = newSalary;

            if (MessageBox.Show($"Confirm update {lecturer.Last_Name} ?", "Confirm", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    bool success = lecturerController.UpdateLecturer(lecturer);
                    if (success)
                    {
                        MessageBox.Show("Lecturer updated successfully!");
                        loadLecturerData();
                        clearField();
                        selectedLecturer = null;
                    }
                    else
                    {
                        MessageBox.Show("Failed to update lecturer.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error updating lecturer: {ex.Message}");
                }
            }
        }
        private void dataGridViewUpdate_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridViewRow selectedRow = dataGridViewUpdate.Rows[e.RowIndex];


            textBoxFirstName.Text = selectedRow.Cells["First_Name"].Value.ToString();
            textBoxLastName.Text = selectedRow.Cells["Last_Name"].Value.ToString();
            textBoxEmail.Text = selectedRow.Cells["Email"].Value.ToString();
            textBoxPhoneNumber.Text = selectedRow.Cells["PhoneNumber"].Value.ToString();
            textBoxSalary.Text = selectedRow.Cells["Salary"].Value.ToString();

            string gender = selectedRow.Cells["Gender"].Value.ToString();
            checkBoxMale.Checked = gender.Equals("Male", StringComparison.OrdinalIgnoreCase);
            checkBoxFemale.Checked = gender.Equals("Female", StringComparison.OrdinalIgnoreCase);

            lecturer.Lecturer_Id = Convert.ToInt32(selectedRow.Cells["Lecturer_Id"].Value);

            selectedLecturer = new Lecturer
            {
                Lecturer_Id = lecturer.Lecturer_Id,
                First_Name = textBoxFirstName.Text,
                Last_Name = textBoxLastName.Text,
                Email = textBoxEmail.Text,
                PhoneNumber = textBoxPhoneNumber.Text,
                salary = double.Parse(textBoxSalary.Text),
                Gender = gender
            };
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridViewUpdate.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Please select an Lecturer to delete", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int lecturerId = Convert.ToInt32(dataGridViewUpdate.SelectedRows[0].Cells["Lecturer_Id"].Value);
                string lecturerName = dataGridViewUpdate.SelectedRows[0].Cells["Last_Name"].Value.ToString();

                var confirmResult = MessageBox.Show($"Are you sure you want to delete {lecturerName}?",
                                                  "Confirm Deletion",
                                                  MessageBoxButtons.YesNo,
                                                  MessageBoxIcon.Warning);

                if (confirmResult == DialogResult.Yes)
                {
                    lecturerController.DeleteLecturer(lecturerId);
                    loadLecturerData();
                    clearField();
                    MessageBox.Show($"Delete {lecturerName} Successful", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting Lecturer: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
