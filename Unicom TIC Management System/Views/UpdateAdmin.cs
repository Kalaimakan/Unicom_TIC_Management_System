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
    public partial class UpdateAdmin : Form
    {
        UserController userController = new UserController();
        AdminController adminController = new AdminController();
        Admin admin = new Admin();
        User user = new User();

        public UpdateAdmin()
        {
            InitializeComponent();
            loadAdminData();
            clearField();
        }

        public void loadAdminData()
        {
            List<Admin> admins = adminController.GetAllAdmin();
            dataGridViewUpdate.DataSource = admins;
        }

        public void clearField()
        {
            textBoxFirstName.Clear();
            textBoxLastName.Clear();
            textBoxEmail.Clear();
            textBoxPhoneNumber.Clear();
            textBoxUserName.Clear();
            textBoxPassword.Clear();
            checkBoxMale.Checked = false;
            checkBoxFemale.Checked = false;
            admin.Admin_Id = 0;
        }
        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            //try
            //{
            // Validate selection
            if (dataGridViewUpdate.SelectedRows.Count == 0 ||
                dataGridViewUpdate.SelectedRows[0].Cells["Admin_Id"]?.Value == null)
            {
                MessageBox.Show("Please select a valid admin to update");
                return;
            }

            // Get data from form
            admin.First_Name = textBoxFirstName.Text.Trim();
            admin.Last_Name = textBoxLastName.Text.Trim();
            admin.Email = textBoxEmail.Text.Trim();
            admin.PhoneNumber = textBoxPhoneNumber.Text.Trim();
            admin.Gender = checkBoxMale.Checked ? "Male" : checkBoxFemale.Checked ? "Female" : "Other";

            user.User_Name = textBoxUserName.Text.Trim();
            user.Password = textBoxPassword.Text.Trim();
            user.User_Email = textBoxEmail.Text.Trim();

            // Validate required fields
            if (string.IsNullOrWhiteSpace(admin.First_Name) ||
                string.IsNullOrWhiteSpace(user.User_Name) ||
                string.IsNullOrWhiteSpace(user.Password))
            {
                MessageBox.Show("First Name, Username and Password are required");
                return;
            }

            // Confirm update
            if (MessageBox.Show("Confirm update this admin?", "Confirm", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                bool success = adminController.UpdateAdmin(admin, user);
                if (success)
                {
                    MessageBox.Show("Admin updated successfully!");
                    loadAdminData(); // Refresh grid
                }
            }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show($"Error updating admin: {ex.Message}");
            //}
        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {


            DataGridViewRow row = dataGridViewUpdate.Rows[e.RowIndex];

            // Safely populate form fields
            textBoxFirstName.Text = row.Cells["First_Name"].Value.ToString();
            textBoxLastName.Text = row.Cells["Last_Name"].Value.ToString();
            textBoxEmail.Text = row.Cells["Email"].Value.ToString();
            textBoxPhoneNumber.Text = row.Cells["PhoneNumber"].Value.ToString();
            textBoxUserName.Text = row.Cells["User_Name"].Value.ToString();
            textBoxPassword.Text = row.Cells["Password"].Value.ToString();

            // Handle gender selection
            string gender = row.Cells["Gender"].Value.ToString();
            checkBoxMale.Checked = gender.Equals("Male", StringComparison.OrdinalIgnoreCase);
            checkBoxFemale.Checked = gender.Equals("Female", StringComparison.OrdinalIgnoreCase);

            // Store Admin_ID for update
            admin.Admin_Id = Convert.ToInt32(row.Cells["Admin_Id"].Value);
        }

        private void buttonTogglePassword_Click(object sender, EventArgs e)
        {
            if (textBoxPassword.Text == "Enter the Password" && textBoxPassword.ForeColor == Color.Gray)
            {
                return;
            }

            textBoxPassword.UseSystemPasswordChar = !textBoxPassword.UseSystemPasswordChar;
            buttonTogglePassword.Text = textBoxPassword.UseSystemPasswordChar ? "👁️" : "🔒";
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

                    MessageBox.Show("Delete functionality to be implemented", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting admin: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}