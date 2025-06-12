using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unicom_TIC_Management_System.Repositories
{
    class Db_Config
    {
        private static string connectionString = "Data Source=C:\\Users\\ASUS\\Desktop\\UMS project\\UnicomTICManagementSystem\\Unicom TIC Management System\\UnicomTICManagementSystem.db;Version=3;";

        public static SQLiteConnection getConnection()
        {
            SQLiteConnection connection = new SQLiteConnection(connectionString);
            connection.Open();
            return connection;
        }
    }
}
