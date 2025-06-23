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

        public EditStaffs()
        {
            InitializeComponent();
            loadAdminData();
        }

        public void loadAdminData()
        {
            List<Staff> staffs = staffController.GetAllStaff();
            dataGridViewUpdate.DataSource = staffs;
        }
        public void clearField()
        {
            textBoxFirstName.Clear();
            textBoxLastName.Clear();
            textBoxEmail.Clear();
            textBoxPhoneNumber.Clear();
            textBoxUserName.Clear();
            textBoxPassword.Clear();
            textBoxSalary.Clear();
            checkBoxMale.Checked = false;
            checkBoxFemale.Checked = false;
        }
        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridViewUpdate.SelectedRows.Count == 0 ||
                dataGridViewUpdate.SelectedRows[0].Cells["Staff_Id"]?.Value == null)
                {
                    MessageBox.Show("Please select a valid Staff to update");
                    return;
                }
                staff.First_Name = textBoxFirstName.Text.Trim();
                staff.Last_Name = textBoxLastName.Text.Trim();
                staff.Email = textBoxEmail.Text.Trim();
                staff.PhoneNumber = textBoxPhoneNumber.Text.Trim();
                staff.Gender = checkBoxMale.Checked ? "Male" : checkBoxFemale.Checked ? "Female" : "Other";
                staff.Salary = Convert.ToDouble(textBoxSalary.Text.Trim());

                user.User_Name = textBoxUserName.Text.Trim();
                user.Password = textBoxPassword.Text.Trim();
                user.User_Email = textBoxEmail.Text.Trim();

                //validate fields
                if (string.IsNullOrEmpty(staff.First_Name) || string.IsNullOrEmpty(staff.Last_Name) ||
                    string.IsNullOrEmpty(staff.Email) || string.IsNullOrEmpty(staff.PhoneNumber) ||
                    string.IsNullOrEmpty(user.User_Name) || string.IsNullOrEmpty(user.Password) || string.IsNullOrWhiteSpace(Convert.ToString(staff.Salary)))
                {
                    MessageBox.Show("Please fill all required fields.");
                    return;
                }

                // Confirm update
                if (MessageBox.Show($"Confirm update {staff.Last_Name} ?", "Confirm", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    bool success = staffController.UpdateStaff(staff, user);
                    if (success)
                    {
                        MessageBox.Show("Staff updated successfully!");
                        loadAdminData(); // Refresh grid
                        clearField();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating staff: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridViewUpdate_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridViewRow selectedRow = dataGridViewUpdate.Rows[e.RowIndex];

            textBoxFirstName.Text = selectedRow.Cells["First_Name"].Value.ToString();
            textBoxLastName.Text = selectedRow.Cells["Last_Name"].Value.ToString();
            textBoxEmail.Text = selectedRow.Cells["Email"].Value.ToString();
            textBoxPhoneNumber.Text = selectedRow.Cells["PhoneNumber"].Value.ToString();
            textBoxUserName.Text = selectedRow.Cells["User_Name"].Value.ToString();
            textBoxPassword.Text = selectedRow.Cells["Password"].Value.ToString();
            textBoxSalary.Text= selectedRow.Cells["Salary"].Value.ToString();

            string gender = selectedRow.Cells["Gender"].Value.ToString();
            checkBoxMale.Checked = gender.Equals("Male", StringComparison.OrdinalIgnoreCase);
            checkBoxFemale.Checked = gender.Equals("Female", StringComparison.OrdinalIgnoreCase);

            staff.Staff_Id = Convert.ToInt32(selectedRow.Cells["Staff_Id"].Value);
        }

        private void buttonTogglePassword_Click(object sender, EventArgs e)
        {
            textBoxPassword.UseSystemPasswordChar = !textBoxPassword.UseSystemPasswordChar;
            buttonTogglePassword.Text = textBoxPassword.UseSystemPasswordChar ? "👁️" : "🔒";
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