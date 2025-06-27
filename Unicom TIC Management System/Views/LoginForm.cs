using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using Unicom_TIC_Management_System.Controllers;
using Unicom_TIC_Management_System.Models;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace Unicom_TIC_Management_System.Views
{
    public partial class LoginForm : Form
    {
        User loginUser = new User();
        UserController loginController = new UserController();
        public LoginForm()
        {
            InitializeComponent();
            SetPlaceholders();
            clearFormField();
            buttonRegister.Visible = true;

        }

        private void SetPlaceholders()
        {
            ApplyPlaceholder(textBoxLoginUserName, "Enter your User Name or Email");
            ApplyPlaceholder(textBoxLoginPassword, "Enter your Password");
        }

        // apply placeholder to TextBox
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
                    if (textBox == textBoxLoginPassword)
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
                    if (textBox == textBoxLoginPassword)
                    {
                        textBox.UseSystemPasswordChar = false;
                    }
                }
            };

            if (textBox == textBoxLoginPassword)
            {
                textBox.UseSystemPasswordChar = false;
            }
        }
        public void clearFormField()
        {
            labelIncorrectUserName.Text = string.Empty;
            labelIncorrectPassword.Text = string.Empty;
            
        }
        private void ShowAuthenticationError(string input)
        {
            var users = loginController.getUser();
            bool userExists = users.Any(user =>
                user.User_Name == input || user.User_Email == input);

            if (!userExists)
            {
                labelIncorrectUserName.Visible = true;
                labelIncorrectUserName.Text = "Incorrect Username or Email.";
            }
            else
            {
                labelIncorrectPassword.Visible = true;
                labelIncorrectPassword.Text = "Incorrect Password.";
            }
        }

        private User AuthenticateUser(string input, string password)
        {
            var users = loginController.getUser();
            return users.FirstOrDefault(user =>
                (user.User_Name == input || user.User_Email == input) &&
                user.Password == password);
            
        }
        private void buttonLogin_Click(object sender, EventArgs e)
        {
            string usernameOrEmail = textBoxLoginUserName.Text;
            string password = textBoxLoginPassword.Text;
            bool isValid = false;

            if (string.IsNullOrWhiteSpace(textBoxLoginUserName.Text) || textBoxLoginUserName.Text == "Enter your User Name or Email")
            {
                labelIncorrectUserName.Visible = true;
                labelIncorrectUserName.Text = "Username or Email is required.";
                isValid = false; 
            }
            else
            {
                labelIncorrectUserName.Visible = false;
            }

            if (string.IsNullOrWhiteSpace(textBoxLoginPassword.Text) || textBoxLoginPassword.Text== "Enter your Password")
            {
                labelIncorrectUserName.Visible = true;
                labelIncorrectPassword.Text = "Password is required.";
                isValid = false;
            }
            else
            {
                labelIncorrectUserName.Visible=false;
            }

            if (isValid)
            {
                return;
            }
            var matchedUser = AuthenticateUser(usernameOrEmail, password);
            if (matchedUser != null)
            {
             
                MessageBox.Show("Login successful!");
                new MainDashBoard(matchedUser.User_Role,matchedUser.User_Id,matchedUser.User_Name,matchedUser.Password).ShowDialog();
                textBoxLoginPassword.Clear();
                textBoxLoginUserName.Clear();
            }
            else
            {
                ShowAuthenticationError(usernameOrEmail);
            }

        }

        private void buttonTogglePassword_Click(object sender, EventArgs e)
        {
            if (textBoxLoginPassword.Text == "Enter the Password" && textBoxLoginPassword.ForeColor == Color.Gray)
            {
                return;
            }

            textBoxLoginPassword.UseSystemPasswordChar = !textBoxLoginPassword.UseSystemPasswordChar;
            buttonTogglePassword.Text = textBoxLoginPassword.UseSystemPasswordChar ? "👁️" : "🔒";
        }

        private void buttonForgotPassword_Click(object sender, EventArgs e)
        {
            ForgetPassword forgetPasswordForm = new ForgetPassword();
            forgetPasswordForm.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            new AdminCreation().ShowDialog();
        }
    }
}
