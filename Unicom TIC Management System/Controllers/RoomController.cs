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
    class RoomController
    {
        //Create Room
        public void AddRoom(Room room)
        {
            using (var connection =Db_Config.getConnection())
            {
                string addQuery = "INSERT INTO Rooms (Room_Name, Room_Type) VALUES (@Room_Name, @Room_Type)";
                using (var command = new SQLiteCommand(addQuery, connection))
                {
                    command.Parameters.AddWithValue("@Room_Name", room.Room_Name);
                    command.Parameters.AddWithValue("@Room_Type", room.Room_Type);
                    command.ExecuteNonQuery();
                }
            }
        }

        //view all rooms
        public List<Room> GetAllRooms()
        {
            List<Room> rooms = new List<Room>();
            using (var connection = Db_Config.getConnection())
            {
                string selectQuery = "SELECT * FROM Rooms";
                using (var command = new SQLiteCommand(selectQuery, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            rooms.Add(new Room
                            {
                                Room_Id = reader.GetInt32(0),
                                Room_Name = reader.GetString(1),
                                Room_Type = reader.GetString(2)
                            });
                        }
                    }
                }
            }
            return rooms;
        }

        //delete room
        public void DeleteRoom(int roomId)
        {
            using (var connection = Db_Config.getConnection())
            {
                string deleteQuery = "DELETE FROM Rooms WHERE Room_Id = @Room_Id";
                using (var command = new SQLiteCommand(deleteQuery, connection))
                {
                    command.Parameters.AddWithValue("@Room_Id", roomId);
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
