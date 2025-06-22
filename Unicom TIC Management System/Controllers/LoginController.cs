using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unicom_TIC_Management_System.Models;
using Unicom_TIC_Management_System.Repositories;

namespace Unicom_TIC_Management_System.Controllers
{
    class LoginController
    {
        public List<User> getUser()
        {
            List<User> users = new List<User>();
            using (var connection = Db_Config.getConnection())
            {
                string getQuery = "SELECT * FROM Users";
                using (var command = new SQLiteCommand(getQuery, connection))
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

        //reset password
        public bool UpdatePassword(int userId , string newPassword)
        {
            using (var connection = Db_Config.getConnection())
            {
                string UpdatePasswordQuery = "UPDATE Users SET Password = @Password WHERE User_Id = @UserId";
                using (var command = new SQLiteCommand(UpdatePasswordQuery, connection))
                {
                    command.Parameters.AddWithValue("@Password", newPassword);
                    command.Parameters.AddWithValue("@UserId", userId);
                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0; 
                }
            }
        }
    }
}
