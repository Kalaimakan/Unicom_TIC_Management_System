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
    public partial class StaffCreation: Form
    {
        StaffController staffController = new StaffController();
        Staff registerStaff = new Staff();
        User registerUser = new User();
        public StaffCreation()
        {
            InitializeComponent();
            SetPlaceholders();
        }

        private void SetPlaceholders()
        {
            ApplyPlaceholder(textBoxFirstName, "Enter the First Name");
            ApplyPlaceholder(textBoxLastName, "Enter the Last Name");
            ApplyPlaceholder(textBoxUserName, "Enter the User Name");
            ApplyPlaceholder(textBoxPassword, "Enter the Password");
            ApplyPlaceholder(textBoxPhoneNumber, "Enter the Phone Number");
            ApplyPlaceholder(textBoxEmail, "Enter the Email");
            ApplyPlaceholder(textBoxSalary, "Enter the Salary");
        }

        private void ApplyPlaceholder(TextBox textBox, string placeholder)
        {
            textBox.Text = placeholder;
            textBox.ForeColor = Color.Gray;

            textBox.Enter += (s, e) =>
            {
                if (textBox.Text == placeholder)
                {
                    textBox.Text = "";
                    textBox.ForeColor = Color.Black;
                    if (textBox == textBoxPassword)
                    {
                        textBox.UseSystemPasswordChar = true;
                    }
                }
            };

            textBox.Leave += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(textBox.Text))
                {
                    textBox.Text = placeholder;
                    textBox.ForeColor = Color.Gray;
                    if (textBox == textBoxPassword)
                    {
                        textBox.UseSystemPasswordChar = false;
                    }
                }
            };

            // Special handling for password field
            if (textBox == textBoxPassword)
            {
                textBox.UseSystemPasswordChar = false;
            }
        }
        public void clearFields()
        {
            textBoxFirstName.Clear();
            textBoxLastName.Clear();
            textBoxUserName.Clear();
            textBoxPassword.Clear();
            textBoxPhoneNumber.Clear();
            dateTimePickerDOB.ResetText();
            textBoxEmail.Clear();
            textBoxSalary.Clear();
            checkBoxMale.Checked = false;
            checkBoxFemale.Checked = false;
            checkBoxOther.Checked = false;

            buttonTogglePassword.Text = "👁️";

            labelFillFirstName.Text = "";
            labelFillFirstName.Visible = false;
            labelFillLastName.Text = "";
            labelFillLastName.Visible = false;
            labelFillUserName.Text = "";
            labelFillUserName.Visible = false;
            labelFillPassword.Text = "";
            labelFillPassword.Visible = false;
            labelFillEmail.Text = "";
            labelFillEmail.Visible = false;
            labelFillPhoneNumber.Text = "";
            labelFillPhoneNumber.Visible = false;
            labelFillSalary.Text = "";
            labelFillSalary.Visible = false;
        }
        private void buttonRegister_Click(object sender, EventArgs e)
        {
            registerStaff.First_Name = textBoxFirstName.Text.Trim();
            registerStaff.Last_Name = textBoxLastName.Text.Trim();
            registerUser.User_Name = textBoxUserName.Text.Trim();
            registerUser.Password = textBoxPassword.Text.Trim();
            registerStaff.Date_Of_Birth=dateTimePickerDOB.Value.ToString("yyyy-MM-dd");
            registerUser.User_Email = textBoxEmail.Text.Trim();
            registerStaff.Email = textBoxEmail.Text.Trim();
            registerStaff.PhoneNumber = textBoxPhoneNumber.Text.Trim();
            registerStaff.Salary=Convert.ToDouble(textBoxSalary.Text.Trim());
            registerUser.User_Role = "Staff";

            if (checkBoxMale.Checked)
                registerStaff.Gender = "Male";
            else if (checkBoxFemale.Checked)
                registerStaff.Gender = "Female";
            else
                registerStaff.Gender = "Other";

            //Ask user to Confirm creation?
            DialogResult confirm = MessageBox.Show(
            $"Are you sure you want to register{registerStaff.Last_Name} ?\n\nUsername: {registerUser.User_Name}\nEmail: {registerUser.User_Email}\nRole: {registerUser.User_Role} ",
            "Confirm Registration",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question
            );
            if (confirm != DialogResult.Yes)
            {
                return;
            }
            staffController.createStaff(registerUser, registerStaff);
            clearFields();
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            clearFields();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
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
    }
}
