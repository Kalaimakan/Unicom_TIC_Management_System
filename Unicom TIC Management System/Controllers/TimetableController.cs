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
    class TimetableController
    {
        public void AddTimetable(TimeTable timeTable)
        {
            using (var connection = Db_Config.getConnection())
            {
                string insertQuery = @"INSERT INTO Timetables (Date, Start_Time, End_Time, Subject_Name, Course_Name, Department_Name, Room) 
                                VALUES (@date, @startTime, @endTime, @subjectName, @courseName, @departmentName, @room);";
                using (var command = new SQLiteCommand(insertQuery, connection))
                {
                    command.Parameters.AddWithValue("@date", timeTable.Date); // Ensure date is in the correct format
                    command.Parameters.AddWithValue("@startTime", timeTable.Start_Time);
                    command.Parameters.AddWithValue("@endTime", timeTable.End_Time);
                    command.Parameters.AddWithValue("@subjectName", timeTable.Subject_Name);
                    command.Parameters.AddWithValue("@courseName", timeTable.Course_Name);
                    command.Parameters.AddWithValue("@departmentName", timeTable.Department_Name);
                    command.Parameters.AddWithValue("@room", timeTable.Room);
                    command.ExecuteNonQuery();
                }
            }
        }

        //get all timetable
        public List<TimeTable> GetAllTimetable()
        {
            List<TimeTable> timeTables = new List<TimeTable>();
            using (var connection = Db_Config.getConnection())
            {
                string selectQuery = "SELECT * FROM Timetables";
                using (var command = new SQLiteCommand(selectQuery, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            TimeTable timeTable = new TimeTable
                            {
                                Timetable_Id = reader.GetInt32(0),
                                Date = reader.GetString(1),
                                Start_Time = reader.GetString(2),
                                End_Time = reader.GetString(3),
                                Subject_Name = reader.GetString(4),
                                Course_Name = reader.GetString(5),
                                Department_Name = reader.GetString(6),
                                Room = reader.GetString(7)
                            };
                            timeTables.Add(timeTable);
                        }
                    }
                }
            }
            return timeTables;
        }
    }
}
