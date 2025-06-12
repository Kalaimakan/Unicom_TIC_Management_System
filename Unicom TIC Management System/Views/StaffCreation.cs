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
            clearFields();
        }

        public void clearFields()
        {
            textBoxFirstName.Clear();
            textBoxLastName.Clear();
            textBoxUserName.Clear();
            textBoxPassword.Clear();
            dateTimePickerDOB.ResetText();
            textBoxEmail.Clear();
            textBoxPhoneNumber.Clear();
            checkBoxMale.Checked = false;
            checkBoxFemale.Checked = false;
            checkBoxOther.Checked = false;
            textBoxSalary.Clear();
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
