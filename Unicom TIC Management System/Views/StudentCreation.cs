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
    public partial class StudentCreation: Form
    {
        StudentController studentController = new StudentController();
        Student registerStudent =new Student();
        User registerUser=new User();
        public StudentCreation()
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
            registerStudent.First_Name = textBoxFirstName.Text.Trim();
            registerStudent.Last_Name = textBoxLastName.Text.Trim();
            registerUser.User_Name = textBoxUserName.Text.Trim();
            registerUser.Password = textBoxPassword.Text.Trim();
            registerStudent.Date_of_Birth = dateTimePickerDOB.Value.ToString("yyyy-MM-dd");
            registerStudent.Email= textBoxEmail.Text.Trim();
            registerUser.User_Email = textBoxEmail.Text.Trim();
            registerUser.User_Role = "Student";
            registerStudent.Address = textBoxAddress.Text.Trim();
            registerStudent.PhoneNumber = textBoxPhoneNumber.Text.Trim();
            registerStudent.Entrolld_Course=comboBoxCourse.SelectedItem?.ToString();
            //Assin gender
            if (checkBoxMale.Checked)
                registerStudent.Gender = "Male";
            else if (checkBoxFemale.Checked)
                registerStudent.Gender = "Female";
            else
                registerStudent.Gender = "Other";



            //Confirmation Dialog
            DialogResult confirm = MessageBox.Show(
            $"Are you sure you want to register{registerStudent.Last_Name} ?\n\nUsername: {registerUser.User_Name}\nEmail: {registerUser.User_Email}\nRole: Student\nSelected Course :{registerStudent.Entrolld_Course} ",
            "Confirm Registration",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question
            );
            if (confirm != DialogResult.Yes)
            {
                return;
            }
            studentController.createStudent(registerStudent, registerUser);
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
            }
            else
            {
                textBoxPassword.UseSystemPasswordChar = true;
            }
        }
    }
}
