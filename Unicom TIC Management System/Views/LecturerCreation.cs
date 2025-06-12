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
    public partial class LecturerCreation: Form
    {
        LecturerController lecturerController = new LecturerController();   
        Lecturer registerLecturer= new Lecturer();
        User registerUser= new User();
        public LecturerCreation()
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
            textBoxPhoneNumber.Clear();
            dateTimePickerDOB.ResetText();
            textBoxEmail.Clear();
            textBoxSalary.Clear();
            checkBoxMale.Checked = false;
            checkBoxFemale.Checked = false;
            checkBoxOther.Checked = false;
        }
        private void buttonRegister_Click(object sender, EventArgs e)
        {
            //Data Assainment
            registerLecturer.First_Name = textBoxFirstName.Text.Trim();
            registerLecturer.Last_Name = textBoxLastName.Text.Trim();
            registerUser.User_Name = textBoxUserName.Text.Trim();
            registerUser.Password = textBoxPassword.Text.Trim();
            registerLecturer.PhoneNumber = textBoxPhoneNumber.Text.Trim();
            registerLecturer.Email = textBoxEmail.Text.Trim();
            registerUser.User_Email = textBoxEmail.Text.Trim();
            registerLecturer.Date_of_Birth=dateTimePickerDOB.Value.ToString("yyyy-MM-dd");
            registerLecturer.salary=Convert.ToDouble(textBoxSalary.Text.Trim());
            registerUser.User_Role = "Lecturer";
            if (checkBoxMale.Checked)
                registerLecturer.Gender = "Male";
            else if (checkBoxFemale.Checked)
                registerLecturer.Gender = "Female";
            else
                registerLecturer.Gender = "Other";

            //Ask user to Confirm creation?
            DialogResult confirm = MessageBox.Show(
            $"Are you sure you want to register{registerLecturer.Last_Name} ?\n\nUsername: {registerUser.User_Name}\nEmail: {registerUser.User_Email}\nRole: Lecturer ",
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
