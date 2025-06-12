using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Unicom_TIC_Management_System.Controllers;
using Unicom_TIC_Management_System.Models;

namespace Unicom_TIC_Management_System.Views
{
    public partial class AdminCreation : Form
    {
        AdminController adminController = new AdminController();
        User registerUser = new User();
        Admin registerAdmin = new Admin();
        public AdminCreation()
        {
            InitializeComponent();
            SetPlaceholders();
        }

        // Set placeholders for all text boxes
        private void SetPlaceholders()
        {
            ApplyPlaceholder(textBoxFirstName, "Enter the First Name");
            ApplyPlaceholder(textBoxLastName, "Enter the Last Name");
            ApplyPlaceholder(textBoxUserName, "Enter the User Name");
            //ApplyPlaceholder(textBoxPassword, "Enter the Password");
            ApplyPlaceholder(textBoxPhoneNumber, "Enter the Phone Number");
            ApplyPlaceholder(textBoxEmail, "Enter the Email");
        }

        // apply placeholder to TextBox
        private void ApplyPlaceholder(TextBox textBox, string placeholder)
        {
            textBox.Text = placeholder;
            textBox.ForeColor = Color.Gray;

            textBox.GotFocus += (s, e) =>
            {
                if (textBox.Text == placeholder)
                {
                    textBox.Text = "";
                    textBox.ForeColor = Color.Black;
                }
            };

            textBox.LostFocus += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(textBox.Text))
                {
                    textBox.Text = placeholder;
                    textBox.ForeColor = Color.Gray;
                }
            };
        }

        public void clearFormfield()
        {
            textBoxFirstName.Clear();
            textBoxLastName.Clear();
            textBoxUserName.Clear();
            textBoxPassword.Clear();
            textBoxEmail.Clear();
            textBoxPhoneNumber.Clear();
            dateTimePickerDOB.ResetText();
            checkBoxFemale.Checked = false;
            checkBoxMale.Checked = false;
            checkBoxOther.Checked = false;
        }
        private void buttonLogin_Click_1(object sender, EventArgs e)
        {
        }
        private void buttonRegister_Click_1(object sender, EventArgs e)
        {
            //Textbox Assain
            registerAdmin.First_Name = textBoxFirstName.Text.Trim();
            registerAdmin.Last_Name = textBoxLastName.Text.Trim();
            registerUser.User_Name = textBoxUserName.Text.Trim();
            registerUser.Password = textBoxPassword.Text.Trim();
            registerUser.User_Email = textBoxEmail.Text.Trim();
            registerUser.User_Role = "Admin";
            registerAdmin.Email = textBoxEmail.Text.Trim();
            registerAdmin.Date_Of_Birth = dateTimePickerDOB.Value.ToString("yyyy-MM-dd");
            registerAdmin.PhoneNumber = textBoxPhoneNumber.Text.Trim();
            //Gender Assain
            if (checkBoxMale.Checked)
                registerAdmin.Gender = "Male";
            else if (checkBoxFemale.Checked)
                registerAdmin.Gender = "Female";
            else
                registerAdmin.Gender = "Other";

            adminController.nameValidation(registerAdmin.First_Name, "First Name");


            //Ask user to Confirm creation?
            DialogResult confirm = MessageBox.Show(
            $"Are you sure you want to register{registerAdmin.Last_Name} ?\n\nUsername: {registerUser.User_Name}\nEmail: {registerUser.User_Email}\nRole: Admin ",
            "Confirm Registration",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question
            );
            if (confirm != DialogResult.Yes)
            {
                return;
            }
            adminController.createAdmin(registerUser, registerAdmin);
            clearFormfield();
        }


        private void buttonClear_Click(object sender, EventArgs e)
        {
            clearFormfield();
        }
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void buttonTogglePassword_Click_1(object sender, EventArgs e)
        {
            if (textBoxPassword.UseSystemPasswordChar)
            {
                textBoxPassword.UseSystemPasswordChar = false;
                buttonTogglePassword.Text = "🔒";
            }
            else
            {
                textBoxPassword.UseSystemPasswordChar = true;
                buttonTogglePassword.Text = "👁️";
            }
        }

    }
}