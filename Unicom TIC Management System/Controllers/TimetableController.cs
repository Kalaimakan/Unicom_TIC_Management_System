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
            try
            {
                using (var connection = Db_Config.getConnection())
                {
                    const string insertQuery = @"INSERT INTO Timetables 
                        (Date, Start_Time, End_Time, Subject_Id, Course_Id, Department_Id, Room_Id) 
                        VALUES (@date, @startTime, @endTime, @subjectId, @courseId, @departmentId, @roomId)";

                    using (var command = new SQLiteCommand(insertQuery, connection))
                    {
                        command.Parameters.AddWithValue("@date", timeTable.Date);
                        command.Parameters.AddWithValue("@startTime", timeTable.Start_Time);
                        command.Parameters.AddWithValue("@endTime", timeTable.End_Time);
                        command.Parameters.AddWithValue("@subjectId", timeTable.Subject_Id);
                        command.Parameters.AddWithValue("@courseId", timeTable.Course_Id);
                        command.Parameters.AddWithValue("@departmentId", timeTable.Department_Id);
                        command.Parameters.AddWithValue("@roomId", timeTable.Room_Id);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to add timetable: " + ex.Message);
            }
        }

        public List<TimeTable> GetAllTimetables()
        {
            var timeTables = new List<TimeTable>();

            try
            {
                using (var connection = Db_Config.getConnection())
                {
                    const string selectQuery = @"SELECT t.Timetable_Id, t.Date, t.Start_Time, t.End_Time,
                                t.Subject_Id, t.Course_Id, t.Department_Id, t.Room_Id,
                                s.Subject_Name, c.Course_Name, d.Department_Name, r.Room_Name
                                FROM Timetables t
                                LEFT JOIN Subjects s ON t.Subject_Id = s.Subject_Id
                                LEFT JOIN Courses c ON t.Course_Id = c.Course_Id
                                LEFT JOIN Departments d ON t.Department_Id = d.Id
                                LEFT JOIN Rooms r ON t.Room_Id = r.Room_Id
                                ORDER BY t.Date, t.Start_Time";

                    using (var command = new SQLiteCommand(selectQuery, connection))
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            try
                            {
                                var timeTable = new TimeTable
                                {
                                    Timetable_Id = Convert.ToInt32(reader["Timetable_Id"]),
                                    Date = reader["Date"].ToString(),
                                    Start_Time = reader["Start_Time"].ToString(),
                                    End_Time = reader["End_Time"].ToString(),
                                    Subject_Id = Convert.ToInt32(reader["Subject_Id"]),
                                    Course_Id = Convert.ToInt32(reader["Course_Id"]),
                                    Department_Id = Convert.ToInt32(reader["Department_Id"]),
                                    Room_Id = Convert.ToInt32(reader["Room_Id"]),
                                    Subject_Name = reader["Subject_Name"].ToString(),
                                    Course_Name = reader["Course_Name"].ToString(),
                                    Department_Name = reader["Department_Name"].ToString(),
                                    Room_Name = reader["Room_Name"].ToString()
                                };
                                timeTables.Add(timeTable);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Error reading timetable record: {ex.Message}");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to load timetables: " + ex.Message);
            }

            return timeTables;
        }

        public void UpdateTimetable(TimeTable timeTable)
        {
            try
            {
                using (var connection = Db_Config.getConnection())
                {
                    const string updateQuery = @"UPDATE Timetables 
                            SET Date = @date, Start_Time = @startTime, End_Time = @endTime, 
                                Subject_Id = @subjectId, Course_Id = @courseId, 
                                Department_Id = @departmentId, Room_Id = @roomId
                            WHERE Timetable_Id = @timetableId";

                    using (var command = new SQLiteCommand(updateQuery, connection))
                    {
                        command.Parameters.AddWithValue("@date", timeTable.Date);
                        command.Parameters.AddWithValue("@startTime", timeTable.Start_Time);
                        command.Parameters.AddWithValue("@endTime", timeTable.End_Time);
                        command.Parameters.AddWithValue("@subjectId", timeTable.Subject_Id);
                        command.Parameters.AddWithValue("@courseId", timeTable.Course_Id);
                        command.Parameters.AddWithValue("@departmentId", timeTable.Department_Id);
                        command.Parameters.AddWithValue("@roomId", timeTable.Room_Id);
                        command.Parameters.AddWithValue("@timetableId", timeTable.Timetable_Id);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to update timetable: " + ex.Message);
            }
        }

        //Delete a timetable by ID
        public void DeleteTimetable(int timetableId)
        {
            try
            {
                using (var connection = Db_Config.getConnection())
                {
                    const string deleteQuery = @"DELETE FROM Timetables WHERE Timetable_Id = @timetableId";
                    using (var command = new SQLiteCommand(deleteQuery, connection))
                    {
                        command.Parameters.AddWithValue("@timetableId", timetableId);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to delete timetable: " + ex.Message);
            }
        }
    }
}