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

namespace Unicom_TIC_Management_System.Views
{
    public partial class ChangeUserNamePassword: Form
    {
        private MainDashBoard mainDash;
        User changeUser = new User();
        UserController userController = new UserController();
        MainDashBoard mainDashBoard = new MainDashBoard();
        public ChangeUserNamePassword()
        {
            InitializeComponent();
        }
        private int userId;
        public ChangeUserNamePassword(MainDashBoard dashboard)
        {
            InitializeComponent();
            this.mainDash = dashboard;
        }
        public ChangeUserNamePassword(int userId)
        {
            InitializeComponent();
            this.userId = userId;
        }
        
        private void buttonChangePassword_Click(object sender, EventArgs e)
        {
            changeUser.User_Id = userId;
            changeUser.User_Name = textBoxChangeUserName.Text;
            changeUser.Password = textBoxChangeNewPassword.Text;
            if (string.IsNullOrWhiteSpace(textBoxChangeUserName.Text))
            {
                labelFillUserName.Text = "Please fill your new username.";
                labelFillUserName.Visible = true;
            }
            else
            {
                labelFillUserName.Visible = false;
            }
            if (string.IsNullOrWhiteSpace(textBoxChangeNewPassword.Text))
            {
                labelFillNewPassword.Text = "Please fill your new password.";
                labelFillNewPassword.Visible = true;
            }
            else
            {
                labelFillNewPassword.Visible = false;
            }
            if (string.IsNullOrWhiteSpace(textBoxChangeConfirmPassword.Text))
            {
                labelFillConfirmPassword.Text = "Please fill your confirm password.";
                labelFillConfirmPassword.Visible = true;
            }
            else
            {
                labelFillConfirmPassword.Visible = false;
            }
            if (textBoxChangeNewPassword.Text != textBoxChangeConfirmPassword.Text)
            {
                labelFillConfirmPassword.Visible = true;
                labelFillConfirmPassword.Text = "New Password and Confirm Password do not match.";
            }
            else
            {
                labelFillConfirmPassword.Visible = false;
                userController.UpdateUser(changeUser);
                mainDash.RefreshUserInfo(changeUser);
                MessageBox.Show("Username and Password changed successfully!");
                this.Close();
            }
        }
    }
}
