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
    public partial class StudentCreation : Form
    {
        Course addCourse = new Course();
        Department addDepartment = new Department();
        CourseController CourseController = new CourseController();
        StudentController studentController = new StudentController();
        Student registerStudent = new Student();
        User registerUser = new User();
        public StudentCreation()
        {
            InitializeComponent();
            SetPlaceholders();
            LoadCourse();

        }

        private void SetPlaceholders()
        {
            ApplyPlaceholder(textBoxFirstName, "Enter the First Name");
            ApplyPlaceholder(textBoxLastName, "Enter the Last Name");
            ApplyPlaceholder(textBoxUserName, "Enter the User Name");
            ApplyPlaceholder(textBoxPassword, "Enter the Password");
            ApplyPlaceholder(textBoxPhoneNumber, "Enter the Phone Number");
            ApplyPlaceholder(textBoxEmail, "Enter the Email");
            ApplyPlaceholder(textBoxAddress, "Enter the Address");
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

        //public void loadCourse()
        //{
        //    comboBoxCourse.Items.Clear();
        //    var courses=CourseController.viewCourse();
        //    foreach (var course in courses)
        //    {
        //        comboBoxCourse.Items.Add(course.Course_Name);
        //    }
        //    comboBoxCourse.SelectedIndex = -1;
        //}

        public void LoadCourse()
        {
            List<Course> courses = CourseController.viewCourse();
            comboBoxCourse.DataSource=courses;
            comboBoxCourse.DisplayMember = "Course_Name";
            comboBoxCourse.ValueMember = "Course_Id";
            comboBoxCourse.SelectedIndex = -1;
        }
        public void clearFields()
        {
            textBoxFirstName.Clear();
            textBoxLastName.Clear();
            textBoxUserName.Clear();
            textBoxPassword.Clear();
            textBoxPhoneNumber.Clear();
            textBoxAddress.Clear();
            dateTimePickerDOB.ResetText();
            textBoxEmail.Clear();
            comboBoxCourse.SelectedIndex = -1;
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
            labelFillAddress.Text = "";
            labelFillAddress.Visible = false;
            labelFillCourse.Text = "";
            labelFillCourse.Visible = false;

        }
        private void buttonRegister_Click(object sender, EventArgs e)
        {
            //Assain value to the table Property
            registerStudent.Admission_No = Validation.autoGenerateStudentId();
            registerStudent.First_Name = textBoxFirstName.Text.Trim();
            registerStudent.Last_Name = textBoxLastName.Text.Trim();
            registerUser.User_Name = textBoxUserName.Text.Trim();
            registerUser.Password = textBoxPassword.Text.Trim();
            registerStudent.Date_of_Birth = dateTimePickerDOB.Value.ToString("yyyy-MM-dd");
            registerStudent.Email = textBoxEmail.Text.Trim();
            registerUser.User_Email = textBoxEmail.Text.Trim();
            registerUser.User_Role = "Student";
            registerStudent.Address = textBoxAddress.Text.Trim();
            registerStudent.PhoneNumber = textBoxPhoneNumber.Text.Trim();
            registerStudent.Entrolld_Course = comboBoxCourse.SelectedItem?.ToString();
            //Assin gender
            if (checkBoxMale.Checked)
                registerStudent.Gender = "Male";
            else if (checkBoxFemale.Checked)
                registerStudent.Gender = "Female";
            else
                registerStudent.Gender = "Other";

            //Validate the form fields
            var firstNameValidation = studentController.validateName(registerStudent.First_Name, "First Name");
            labelFillFirstName.Text = firstNameValidation.errorMessage;
            labelFillFirstName.Visible = !firstNameValidation.isValid;

            var lastNameValidation = studentController.validateName(registerStudent.Last_Name, "Last Name");
            labelFillLastName.Text = lastNameValidation.errorMessage;
            labelFillLastName.Visible = !lastNameValidation.isValid;

            var userNameValidation = studentController.validateName(registerUser.User_Name, "User Name");
            labelFillUserName.Text = userNameValidation.errorMessage;
            labelFillUserName.Visible = !userNameValidation.isValid;

            var passwordValidation = studentController.validatePassword(registerUser.Password);
            labelFillPassword.Text = passwordValidation.errorMessage;
            labelFillPassword.Visible = !passwordValidation.isValid;

            var emailValidation = studentController.validateEmail(registerUser.User_Email);
            labelFillEmail.Text = emailValidation.errorMessage;
            labelFillEmail.Visible = !emailValidation.isValid;

            var phoneNumberValidation = studentController.validatePhoneNumber(registerStudent.PhoneNumber);
            labelFillPhoneNumber.Text = phoneNumberValidation.errorMessage;
            labelFillPhoneNumber.Visible = !phoneNumberValidation.isValid;

            var dateOfBirthValidation = studentController.validateDateOfBirth(dateTimePickerDOB.Value);
            labelFillDOB.Text = dateOfBirthValidation.errorMessage;
            labelFillDOB.Visible = !dateOfBirthValidation.isValid;

            var genderValidation = studentController.validateGender(checkBoxMale.Checked, checkBoxFemale.Checked, checkBoxOther.Checked);
            labelFillGender.Text = genderValidation.errorMessage;
            labelFillGender.Visible = !genderValidation.isValid;

            var addressValidation = studentController.validateAddress(registerStudent.Address);
            labelFillAddress.Text = addressValidation.errorMessage;
            labelFillAddress.Visible = !addressValidation.isValid;

            if (comboBoxCourse.SelectedItem != null)
            {
                addCourse = (Course)comboBoxCourse.SelectedItem;
                registerStudent.Entrolld_Course = addCourse.Course_Name;
                registerStudent.Course_Id = addCourse.Course_Id;
            }
            else
            {
                labelFillCourse.Visible = true;
                labelFillCourse.Text = "Select the Course";
                return;
            }

            if (!firstNameValidation.isValid || !lastNameValidation.isValid || !userNameValidation.isValid || !passwordValidation.isValid || !emailValidation.isValid || !phoneNumberValidation.isValid || !dateOfBirthValidation.isValid || !genderValidation.isValid || !addressValidation.isValid)
            {
                return;
            }


            

            //Confirmation Dialog
            DialogResult confirm = MessageBox.Show(
            $"Are you sure you want to register {registerStudent.Last_Name} ?\n\nStudent Id : {registerStudent.Admission_No}\nUsername : {registerUser.User_Name}\nEmail : {registerUser.User_Email}\nRole : Student\nSelected Course : {addCourse.Course_Name} ",
            "Confirm Registration",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question
            );
            if (confirm != DialogResult.Yes)
            {
                return;
            }
            studentController.createStudent(registerStudent, registerUser, addCourse, addDepartment);
            clearFields();
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            if (textBoxFirstName.Text != "Enter the First Name" || textBoxLastName.Text != "Enter the Last Name" || textBoxUserName.Text != "Enter the User Name" || textBoxPassword.Text != "Enter the Password" || textBoxPhoneNumber.Text != "Enter the Phone Number" || textBoxEmail.Text != "Enter the Email" || textBoxAddress.Text != "Enter the Address")
            {
                clearFields();
            }
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
