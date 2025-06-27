using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Unicom_TIC_Management_System.Models;
using Unicom_TIC_Management_System.Repositories;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

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

        //Update User
        public void UpdateUser(User user, SQLiteConnection connection, SQLiteTransaction transaction)
        {
            string updateQuery = @"UPDATE Users 
                           SET User_Name = @UserName, 
                               Password = @Password, 
                               User_Email = @userEmail
                           WHERE User_Id = @UserId";

            using (SQLiteCommand command = new SQLiteCommand(updateQuery, connection, transaction))
            {
                command.Parameters.AddWithValue("@UserName", user.User_Name);
                command.Parameters.AddWithValue("@Password", user.Password);
                command.Parameters.AddWithValue("@userEmail", user.User_Email);
                command.Parameters.AddWithValue("@UserId", user.User_Id);

                int rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected == 0)
                {
                    throw new Exception("No user record was updated");
                }
            }
        }
        public void UpdateEmailUser(int userId, string email, SQLiteConnection connection, SQLiteTransaction transaction)
        {
            string updateUserEmailQuery = @"UPDATE Users 
                                            SET User_Email = @email
                                            WHERE User_Id = @userId";

            using (var updateUserCmd = new SQLiteCommand(updateUserEmailQuery, connection, transaction))
            {
                updateUserCmd.Parameters.AddWithValue("@email", email);
                updateUserCmd.Parameters.AddWithValue("@userId", userId);
                updateUserCmd.ExecuteNonQuery();
            }

        }

        //Delete User
        public void DeleteUser(int userId, SQLiteConnection connection, SQLiteTransaction transaction)
        {
            string deleteQuery = "DELETE FROM Users WHERE User_Id = @UserId";
            using (SQLiteCommand command = new SQLiteCommand(deleteQuery, connection, transaction))
            {
                command.Parameters.AddWithValue("@UserId", userId);
                int rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected == 0)
                {
                    throw new Exception("No user record was deleted");
                }
            }
        }
    }
}