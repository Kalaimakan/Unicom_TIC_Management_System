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
    public partial class EditStaffs : Form
    {
        StaffController staffController = new StaffController();
        Staff staff = new Staff();
        User user = new User();
        private Staff selectedStaff = null;
        public EditStaffs()
        {
            InitializeComponent();
            loadAdminData();
        }

        public void loadAdminData()
        {
            List<Staff> staffs = staffController.GetAllStaff();
            dataGridViewUpdate.DataSource = staffs;
            dataGridViewUpdate.Columns["User_Name"].Visible = false;
            dataGridViewUpdate.Columns["Password"].Visible = false;
            dataGridViewUpdate.Columns["User_Email"].Visible = false;
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
        }
        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (selectedStaff == null)
                {
                    MessageBox.Show("Please select a staff member first.");
                    return;
                }

                // Get new values from form
                string newFirstName = textBoxFirstName.Text.Trim();
                string newLastName = textBoxLastName.Text.Trim();
                string newEmail = textBoxEmail.Text.Trim();
                string newPhone = textBoxPhoneNumber.Text.Trim();
                string newGender = checkBoxMale.Checked ? "Male" : checkBoxFemale.Checked ? "Female" : "Other";
                double newSalary = double.TryParse(textBoxSalary.Text.Trim(), out double salaryVal) ? salaryVal : 0;

                // Validate required fields
                if (string.IsNullOrWhiteSpace(newFirstName) ||
                    string.IsNullOrWhiteSpace(newLastName) ||
                    string.IsNullOrWhiteSpace(newEmail) ||
                    string.IsNullOrWhiteSpace(newPhone))
                {
                    MessageBox.Show("All fields are required.");
                    return;
                }

                // Check for changes
                if (selectedStaff.First_Name == newFirstName &&
                    selectedStaff.Last_Name == newLastName &&
                    selectedStaff.Email == newEmail &&
                    selectedStaff.PhoneNumber == newPhone &&
                    selectedStaff.Gender == newGender &&
                    selectedStaff.Salary == newSalary )
                {
                    MessageBox.Show("No changes detected to update.");
                    return;
                }

                staff.First_Name = newFirstName;
                staff.Last_Name = newLastName;
                staff.Email = newEmail;
                staff.PhoneNumber = newPhone;
                staff.Gender = newGender;
                staff.Salary = newSalary;
                

                // Confirm update
                if (MessageBox.Show($"Confirm update {staff.Last_Name}?",
                    "Confirm", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    bool success = staffController.UpdateStaff(staff, user);
                    if (success)
                    {
                        MessageBox.Show("Staff updated successfully!");
                        loadAdminData();
                        clearField();
                    }
                    else
                    {
                        MessageBox.Show("Failed to update staff.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating staff: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

            staff.Staff_Id = Convert.ToInt32(selectedRow.Cells["Staff_Id"].Value);

            selectedStaff = new Staff()
            {
                Staff_Id = Convert.ToInt32(selectedRow.Cells["Staff_Id"].Value),
                First_Name = textBoxFirstName.Text,
                Last_Name = textBoxLastName.Text,
                Email = textBoxEmail.Text,
                PhoneNumber = textBoxPhoneNumber.Text,
                Gender = gender,
                Salary = Convert.ToDouble(textBoxSalary.Text),
            };
        }

        private void buttonTogglePassword_Click(object sender, EventArgs e)
        {
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridViewUpdate.SelectedRows.Count == 0 ||
                    dataGridViewUpdate.SelectedRows[0].Cells["Staff_Id"]?.Value == null)
                {
                    MessageBox.Show("Please select a valid Staff to delete");
                    return;
                }
                int staffId = Convert.ToInt32(dataGridViewUpdate.SelectedRows[0].Cells["Staff_Id"].Value);
                if (MessageBox.Show($"Are you sure you want to delete Staff ID {staffId}?", "Confirm Delete", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    bool success = staffController.DeleteStaff(staffId);
                    if (success)
                    {
                        MessageBox.Show("Staff deleted successfully!");
                        loadAdminData(); // Refresh grid
                        clearField();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting staff: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}