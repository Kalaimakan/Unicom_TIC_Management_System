using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Unicom_TIC_Management_System.Models;
using Unicom_TIC_Management_System.Repositories;

namespace Unicom_TIC_Management_System.Controllers
{
    class UserController : Validation
    {


        public (bool isValid, string errorMessage) validateUserData(User registerUser)
        {
            //validate User Name.
            var userNameValidate = validateName(registerUser.User_Name, "User Name");
            if (!userNameValidate.isValid)
                return userNameValidate;

            //validate Password.
            var passwordValidate = validatePassword(registerUser.Password);
            if (!passwordValidate.isValid)
                return passwordValidate;

            //validate Email.
            var emailValidate = validateEmail(registerUser.User_Email);
            if (!emailValidate.isValid)
                return emailValidate;

            return (true, string.Empty);
        }
        //Create User.
        public int createUser(User registerUser, SQLiteConnection connection, SQLiteTransaction transaction)
        {
            try
            {
                //Add Data to the User Table .
                string userQuary = @"INSERT INTO Users 
                                            (User_Name, Password, User_Email, User_Role) VALUES 
                                            (@UserName, @Password, @userEmail, @userRole);
                                             SELECT last_insert_rowid();";
                using (SQLiteCommand userCommand = new SQLiteCommand(userQuary, connection, transaction))
                {
                    userCommand.Parameters.AddWithValue("@UserName", registerUser.User_Name);
                    userCommand.Parameters.AddWithValue("@Password", registerUser.Password);
                    userCommand.Parameters.AddWithValue("@userEmail", registerUser.User_Email);
                    userCommand.Parameters.AddWithValue("@userRole", registerUser.User_Role);

                    //get the user Id.
                    int userId = Convert.ToInt32(userCommand.ExecuteScalar());
                    return userId;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error while creating the user: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        //Get User
        public List<User> getUser()
        {
            List<User> users = new List<User>();
            using (var connection = Db_Config.getConnection())
            {
                string getQuary = "SELECT * FROM Users";
                using (var command = new SQLiteCommand(getQuary, connection))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            users.Add(new User()
                            {
                                User_Id = reader.GetInt32(0),
                                User_Name = reader.GetString(1),
                                Password = reader.GetString(2),
                                User_Email = reader.GetString(3),
                                User_Role = reader.GetString(4)
                            });
                        }
                    }
                }
            }
            return users;
        }
    }
}