using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Unicom_TIC_Management_System.Controllers;
using Unicom_TIC_Management_System.Models;
using Unicom_TIC_Management_System.Repositories;

namespace Unicom_TIC_Management_System.Views
{
    public partial class EditAdmin : Form
    {
        UserController userController = new UserController();
        AdminController adminController = new AdminController();
        Admin admin = new Admin();
        User user = new User();
        private Admin selectedAdmin = null;
        public EditAdmin()
        {
            InitializeComponent();
            loadAdminData();
            clearField();
        }

        public void loadAdminData()
        {
            List<Admin> admins = adminController.GetAllAdmin();
            dataGridViewUpdate.DataSource = admins;
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
            checkBoxMale.Checked = false;
            checkBoxFemale.Checked = false;

        }
        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (selectedAdmin == null)
                {
                    MessageBox.Show("Please select an admin to Update.");
                    return;
                }

                // Get new values from form
                string newFirstName = textBoxFirstName.Text.Trim();
                string newLastName = textBoxLastName.Text.Trim();
                string newEmail = textBoxEmail.Text.Trim();
                string newPhone = textBoxPhoneNumber.Text.Trim();
                string newGender = checkBoxMale.Checked ? "Male" : checkBoxFemale.Checked ? "Female" : "Other";

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
                if (selectedAdmin.First_Name == newFirstName &&
                    selectedAdmin.Last_Name == newLastName &&
                    selectedAdmin.Email == newEmail &&
                    selectedAdmin.PhoneNumber == newPhone &&
                    selectedAdmin.Gender == newGender )
                {
                    MessageBox.Show("No changes detected to update.");
                    return;
                }

                admin.First_Name = newFirstName;
                admin.Last_Name = newLastName;
                admin.Email = newEmail;
                admin.PhoneNumber = newPhone;
                admin.Gender = newGender;



                // Confirm update
                if (MessageBox.Show($"Confirm update {admin.Last_Name}?",
                    "Confirm", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    bool success = adminController.UpdateAdmin(admin, user);
                    if (success)
                    {
                        MessageBox.Show("Admin updated successfully!");
                        loadAdminData();
                        clearField();
                    }
                    else
                    {
                        MessageBox.Show("Failed to update admin.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating admin: {ex.Message}");
            }
        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {


            DataGridViewRow row = dataGridViewUpdate.Rows[e.RowIndex];

            // Safely populate form fields
            textBoxFirstName.Text = row.Cells["First_Name"].Value.ToString();
            textBoxLastName.Text = row.Cells["Last_Name"].Value.ToString();
            textBoxEmail.Text = row.Cells["Email"].Value.ToString();
            textBoxPhoneNumber.Text = row.Cells["PhoneNumber"].Value.ToString();

            // Handle gender selection
            string gender = row.Cells["Gender"].Value.ToString();
            checkBoxMale.Checked = gender.Equals("Male", StringComparison.OrdinalIgnoreCase);
            checkBoxFemale.Checked = gender.Equals("Female", StringComparison.OrdinalIgnoreCase);

            // Store Admin_ID for update
            admin.Admin_Id = Convert.ToInt32(row.Cells["Admin_Id"].Value);

            selectedAdmin = new Admin()
            {
                Admin_Id = Convert.ToInt32(row.Cells["Admin_Id"].Value),
                First_Name = textBoxFirstName.Text,
                Last_Name = textBoxLastName.Text,
                Email = textBoxEmail.Text,
                PhoneNumber = textBoxPhoneNumber.Text,
                Gender = gender
            };

        }

        private void buttonTogglePassword_Click(object sender, EventArgs e)
        {
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridViewUpdate.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Please select an admin to delete", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int adminId = Convert.ToInt32(dataGridViewUpdate.SelectedRows[0].Cells["Admin_Id"].Value);
                string adminName = dataGridViewUpdate.SelectedRows[0].Cells["First_Name"].Value.ToString();

                var confirmResult = MessageBox.Show($"Are you sure you want to delete {adminName}?",
                                                  "Confirm Deletion",
                                                  MessageBoxButtons.YesNo,
                                                  MessageBoxIcon.Warning);

                if (confirmResult == DialogResult.Yes)
                {
                    adminController.DeleteAdmin(adminId);
                    loadAdminData();
                    clearField();
                    MessageBox.Show($"Delete {adminName} Successful", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting Admin: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}