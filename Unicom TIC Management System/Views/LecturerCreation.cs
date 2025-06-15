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
    public partial class LecturerCreation : Form
    {
        LecturerController lecturerController = new LecturerController();
        Lecturer registerLecturer = new Lecturer();
        User registerUser = new User();
        public LecturerCreation()
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
            //Data Assainment
            registerLecturer.First_Name = textBoxFirstName.Text.Trim();
            registerLecturer.Employee_Id = Validation.autoGenerateLecturerId();
            registerLecturer.Last_Name = textBoxLastName.Text.Trim();
            registerUser.User_Name = textBoxUserName.Text.Trim();
            registerUser.Password = textBoxPassword.Text.Trim();
            registerLecturer.PhoneNumber = textBoxPhoneNumber.Text.Trim();
            registerLecturer.Email = textBoxEmail.Text.Trim();
            registerUser.User_Email = textBoxEmail.Text.Trim();
            registerLecturer.Date_of_Birth = dateTimePickerDOB.Value.ToString("yyyy-MM-dd");
            registerUser.User_Role = "Lecturer";
            if (checkBoxMale.Checked)
                registerLecturer.Gender = "Male";
            else if (checkBoxFemale.Checked)
                registerLecturer.Gender = "Female";
            else
                registerLecturer.Gender = "Other";

            //validate the form field.
            var firstnameValidate = lecturerController.validateName(registerLecturer.First_Name, "First Name");
            labelFillFirstName.Text = firstnameValidate.errorMessage;
            labelFillFirstName.Visible = !firstnameValidate.isValid;

            var lastnameValidate = lecturerController.validateName(registerLecturer.Last_Name, "Last Name");
            labelFillLastName.Text = lastnameValidate.errorMessage;
            labelFillLastName.Visible = !lastnameValidate.isValid;

            var usernameValidate = lecturerController.validateName(registerUser.User_Name, "User Name");
            labelFillUserName.Text = usernameValidate.errorMessage;
            labelFillUserName.Visible = !usernameValidate.isValid;

            var passwordValidate = lecturerController.validatePassword(registerUser.Password);
            labelFillPassword.Text = passwordValidate.errorMessage;
            labelFillPassword.Visible = !passwordValidate.isValid;

            var emailValidate = lecturerController.validateEmail(registerUser.User_Email);
            labelFillEmail.Text = emailValidate.errorMessage;
            labelFillEmail.Visible = !emailValidate.isValid;

            var phoneNumberValidate = lecturerController.validatePhoneNumber(registerLecturer.PhoneNumber);
            labelFillPhoneNumber.Text = phoneNumberValidate.errorMessage;
            labelFillPhoneNumber.Visible = !phoneNumberValidate.isValid;

            var dateOfBirthValidate = lecturerController.validateDateOfBirth(dateTimePickerDOB.Value);
            labelFillDOB.Text = dateOfBirthValidate.errorMessage;
            labelFillDOB.Visible = !dateOfBirthValidate.isValid;

            var genderValidate = lecturerController.validateGender(checkBoxMale.Checked, checkBoxFemale.Checked, checkBoxOther.Checked);
            labelFillGender.Text = genderValidate.errorMessage;
            labelFillGender.Visible = !genderValidate.isValid;

            var salaryValidate = lecturerController.validateSalary(textBoxSalary.Text.Trim());
            labelFillSalary.Text = salaryValidate.errorMessage;
            labelFillSalary.Visible = !salaryValidate.isValid;

            if (!salaryValidate.isValid)
            {
                return;
            }
            registerLecturer.salary = double.Parse(textBoxSalary.Text.Trim());

            //Check if all validations passed
            if (!firstnameValidate.isValid || !lastnameValidate.isValid || !usernameValidate.isValid || !passwordValidate.isValid || !emailValidate.isValid || !phoneNumberValidate.isValid || !dateOfBirthValidate.isValid || !genderValidate.isValid)
            {
                return;
            }

            //Ask user to Confirm creation?
            DialogResult confirm = MessageBox.Show(
            $"Are you sure you want to register{registerLecturer.Last_Name} ?\n\nLecturer Id : {registerLecturer.Employee_Id}\nUsername : {registerUser.User_Name}\nEmail : {registerUser.User_Email}\nRole : Lecturer ",
            "Confirm Registration",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question
            );
            if (confirm != DialogResult.Yes)
            {
                return;
            }

            lecturerController.createLecture(registerUser, registerLecturer);
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
