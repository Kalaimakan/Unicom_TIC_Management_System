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
    public partial class ForgetPassword : Form
    {
        LoginController loginController = new LoginController();
        public ForgetPassword()
        {
            InitializeComponent();
        }

        private void buttonChangePassword_Click(object sender, EventArgs e)
        {
            string username = textBoxResetUserName.Text.Trim();
            string email = textBoxResetEmail.Text.Trim();
            string newPassword = textBoxResetNewPassword.Text.Trim();
            string confirmPassword = textBoxResetConfirmPassword.Text.Trim();

            if (string.IsNullOrWhiteSpace(username))
            {
                labelFillUserName.Visible = true;
                labelFillUserName.Text = "Please fill your username.";
                return;
            }

            if (string.IsNullOrWhiteSpace(email))
            {
                labelFillEmail.Visible = true;
                labelFillEmail.Text = "Please fill your email.";
                return;
            }

            if (string.IsNullOrWhiteSpace(newPassword))
            {
                labelFillNewPassword.Visible = true;
                labelFillNewPassword.Text = "Please fill your new password.";
                return;
            }

            if (string.IsNullOrWhiteSpace(confirmPassword))
            {
                labelFillConfirmPassword.Visible = true;
                labelFillConfirmPassword.Text = "Please fill your confirm password.";
                return;
            }

            var users = loginController.getUser();
            var userToUpdate = users.FirstOrDefault(u => u.User_Name == username && u.User_Email == email);

            if (userToUpdate != null)
            {
                if (newPassword == confirmPassword)
                {
                    bool updateSuccess = loginController.UpdatePassword(userToUpdate.User_Id, newPassword);
                    if (updateSuccess)
                    {
                        MessageBox.Show("Password updated successfully.");
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Failed to update password. Try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    labelFillConfirmPassword.Visible = true;
                    labelFillConfirmPassword.Text = "Passwords do not match.";
                }
            }
            else
            {
                labelFillEmail.Visible = true;
                labelFillEmail.Text = "Username or Email is incorrect.";
            }
        }

        private void buttonBackToLogin_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}