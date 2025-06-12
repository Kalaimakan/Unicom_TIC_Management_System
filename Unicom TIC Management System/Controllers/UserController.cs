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
    class UserController
    {
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
            catch(Exception ex)
            {
                MessageBox.Show($"Error while creating the user: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
            
        }
    }
}