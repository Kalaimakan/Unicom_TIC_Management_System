using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Unicom_TIC_Management_System.Repositories
{
    class Migration
    {
        public static void createTable()
        {
            using (var connection = Db_Config.getConnection())
            using (var transaction = connection.BeginTransaction())
            {
                try
                {
                    var commend = connection.CreateCommand();
                    commend.Transaction = transaction;

                    //Enable forgin keys.
                    commend.CommandText = "PRAGMA foreign_keys = ON;";
                    commend.ExecuteNonQuery();

                    //create User Table.
                    commend.CommandText = @"CREATE TABLE IF NOT EXISTS Users(
                                    User_Id INTEGER PRIMARY KEY AUTOINCREMENT,
                                    User_Name TEXT NOT NULL UNIQUE,
                                    Password TEXT NOT NULL,
                                    User_Email TEXT NOT NULL UNIQUE,
                                    User_Role TEXT NOT NULL,
                                    User_CreatedAt TEXT NOT NULL DEFAULT CURRENT_TIMESTAMP,
                                    User_UpdatedAt TEXT NOT NULL DEFAULT CURRENT_TIMESTAMP
                                    )";
                    commend.ExecuteNonQuery();

                    //Create Department Table.
                    commend.CommandText = @"CREATE TABLE IF NOT EXISTS Departments(
                                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                                    Department_Name TEXT NOT NULL UNIQUE
                                    )";
                    commend.ExecuteNonQuery();

                    //Create Course Table.
                    commend.CommandText = @"CREATE TABLE IF NOT EXISTS Courses(
                                    Course_Id INTEGER PRIMARY KEY AUTOINCREMENT,
                                    Course_Name TEXT NOT NULL UNIQUE,
                                    StartDate TEXT NOT NULL,
                                    Department_Id INTEGER,
                                    FOREIGN KEY (Department_Id) REFERENCES Departments(Department_Id)
                                        ON DELETE SET NULL
                                        ON UPDATE CASCADE
                                    )";
                    commend.ExecuteNonQuery();

                    //Create Room Table.
                    commend.CommandText = @"CREATE TABLE IF NOT EXISTS Rooms(
                                    Room_Id INTEGER PRIMARY KEY AUTOINCREMENT,
                                    Room_Name TEXT NOT NULL,
                                    Room_Type TEXT NOT NULL
                                    )";
                    commend.ExecuteNonQuery();

                    //Create Subject Table.
                    commend.CommandText = @"CREATE TABLE IF NOT EXISTS Subjects(
                                    Course_Id INTEGER,
                                    Subject_Id INTEGER PRIMARY KEY AUTOINCREMENT,
                                    Subject_Name TEXT NOT NULL,
                                    Department_Id INTEGER,
                                    FOREIGN KEY (Department_Id) REFERENCES Departments(Department_Id)
                                        ON DELETE SET NULL
                                        ON UPDATE CASCADE,
                                    FOREIGN KEY (Course_Id) REFERENCES Courses(Course_Id)
                                        ON DELETE SET NULL
                                        ON UPDATE CASCADE
                                    )";
                    commend.ExecuteNonQuery();

                    //Create Admin Table.
                    commend.CommandText = @"CREATE TABLE IF NOT EXISTS Admins(
                                    User_Id INTEGER,
                                    Admin_Id INTEGER PRIMARY KEY AUTOINCREMENT,
                                    First_Name TEXT NOT NULL,
                                    Last_Name TEXT NOT NULL,
                                    Email TEXT NOT NULL UNIQUE,
                                    Date_of_Birth TEXT NOT NULL ,
                                    Gender TEXT NOT NULL ,
                                    PhoneNumber TEXT NOT NULL UNIQUE,
                                    FOREIGN KEY (User_Id) REFERENCES Users(User_Id)
                                        ON DELETE SET NULL
                                        ON UPDATE CASCADE
                                    )";
                    commend.ExecuteNonQuery();

                    //Create Staff Table.
                    commend.CommandText = @"CREATE TABLE IF NOT EXISTS Staffs(
                                    User_Id INTEGER,
                                    Staff_Id INTEGER PRIMARY KEY AUTOINCREMENT,
                                    First_Name TEXT NOT NULL,
                                    Last_Name TEXT NOT NULL,
                                    Email NOT NULL UNIQUE,
                                    PhoneNumber NOT NULL UNIQUE,
                                    Date_of_Birth TEXT NOT NULL ,
                                    Gender NOT NULL,
                                    Salary NOT NULL,
                                    FOREIGN KEY (User_Id) REFERENCES Users(User_Id)
                                        ON DELETE SET NULL
                                        ON UPDATE CASCADE
                                    )";
                    commend.ExecuteNonQuery();

                    //create student table.
                    commend.CommandText = @"CREATE TABLE IF NOT EXISTS Students(
                                    User_Id INTEGER,
                                    Student_Id INTEGER PRIMARY KEY AUTOINCREMENT,
                                    Admission_No TEXT NOT NULL UNIQUE,
                                    First_Name TEXT NOT NULL,
                                    Last_Name TEXT NOT NULL,
                                    Email TEXT NOT NULL UNIQUE,
                                    Gender TEXT NOT NULL ,
                                    PhoneNumber TEXT NOT NULL UNIQUE,
                                    Date_of_Birth TEXT NOT NULL ,
                                    Address TEXT NOT NULL,
                                    Entrolled_Course TEXT NOT NULL,
                                    Course_Id INTEGER,
                                    Department_Id INTEGER,
                                    FOREIGN KEY (Department_Id) REFERENCES Departments(Department_Id)
                                        ON DELETE SET NULL
                                        ON UPDATE CASCADE,
                                    FOREIGN KEY (Course_Id) REFERENCES Courses(Course_Id)
                                        ON DELETE SET NULL
                                        ON UPDATE CASCADE,
                                    FOREIGN KEY (User_Id) REFERENCES Users(User_Id)
                                        ON DELETE SET NULL
                                        ON UPDATE CASCADE
                                    )";
                    commend.ExecuteNonQuery();

                    //Create Lecturer Table.
                    commend.CommandText = @"CREATE TABLE IF NOT EXISTS Lecturers(
                                    User_Id INTEGER,
                                    Lecturer_Id INTEGER PRIMARY KEY AUTOINCREMENT,
                                    Employee_Id TEXT NOT NULL UNIQUE,
                                    First_Name TEXT NOT NULL,
                                    Last_Name TEXT NOT NULL,
                                    Email TEXT NOT NULL UNIQUE,
                                    PhoneNumber TEXT NOT NULL UNIQUE,
                                    Date_of_Birth TEXT NOT NULL ,
                                    Gender NOT NULL,
                                    salary TEXT NOT NULL ,
                                    Subject_Id INTEGER,
                                    FOREIGN KEY (Subject_Id) REFERENCES Subjects(Subject_Id)
                                        ON DELETE SET NULL
                                        ON UPDATE CASCADE,  
                                    FOREIGN KEY (User_Id) REFERENCES Users(User_Id)
                                        ON DELETE SET NULL
                                        ON UPDATE CASCADE
                                    )";
                    commend.ExecuteNonQuery();

                    //Create Exam Table.
                    commend.CommandText = @"CREATE TABLE IF NOT EXISTS Exams(
                                    Exam_Id INTEGER PRIMARY KEY AUTOINCREMENT,
                                    Name TEXT NOT NULL, 
                                    Subject_Id INTEGER,
                                    FOREIGN KEY (Subject_Id) REFERENCES Subjects(Subject_Id)
                                        ON DELETE SET NULL
                                        ON UPDATE CASCADE
                                    )";
                    commend.ExecuteNonQuery();

                    //Create Marks table.
                    commend.CommandText = @"CREATE TABLE IF NOT EXISTS Marks(
                                    Mark_Id INTEGER PRIMARY KEY AUTOINCREMENT,
                                    Marks_Value INTEGER NOT NULL,     
                                    Student_Id INTEGER,
                                    Exam_Id INTEGER,
                                    FOREIGN KEY (Student_Id) REFERENCES Students(Student_Id)
                                        ON DELETE SET NULL
                                        ON UPDATE CASCADE,
                                    FOREIGN KEY (Exam_Id) REFERENCES Exams(Exam_Id)
                                        ON DELETE SET NULL
                                        ON UPDATE CASCADE
                                    )";
                    commend.ExecuteNonQuery();

                    //Create Timetable Table.
                    commend.CommandText = @"CREATE TABLE IF NOT EXISTS Timetables(
                                    Timetable_Id INTEGER PRIMARY KEY AUTOINCREMENT,
                                    Date TEXT NOT NULL,
                                    Start_Time NOT NULL,
                                    End_Time NOT NULL,
                                    Subject_Id INTEGER,
                                    Course_Id INTEGER,
                                    Department_Id INTEGER,
                                    Room_Id INTEGER,
                                    FOREIGN KEY (Subject_Id) REFERENCES Subjects(Subject_Id)
                                        ON DELETE SET NULL
                                        ON UPDATE CASCADE                                    
                                    FOREIGN KEY (Course_Id) REFERENCES Courses(Course_Id)
                                        ON DELETE SET NULL
                                        ON UPDATE CASCADE,
                                    FOREIGN KEY (Department_Id) REFERENCES Departments(Id)
                                        ON DELETE SET NULL
                                        ON UPDATE CASCADE,
                                    FOREIGN KEY (Room_Id) REFERENCES Rooms(Room_Id)
                                        ON DELETE SET NULL
                                        ON UPDATE CASCADE
                                    )";
                    commend.ExecuteNonQuery();

                    //Create Lecturer_Students Table.
                    commend.CommandText = @"CREATE TABLE IF NOT EXISTS Lecturer_Students(
                                    Lecturer_Student_Id INTEGER PRIMARY KEY AUTOINCREMENT,
                                    Lecturer_Id INTEGER,
                                    Subject_Name TEXT,
                                    Student_Id INTEGER,
                                    FOREIGN KEY (Lecturer_Id) REFERENCES Lecturers(Lecturer_Id)
                                        ON DELETE SET NULL
                                        ON UPDATE CASCADE,
                                    FOREIGN KEY (Student_Id) REFERENCES Students(Student_Id)
                                        ON DELETE SET NULL
                                        ON UPDATE CASCADE
                                    )";
                    commend.ExecuteNonQuery();

                    //Create Subject_Students Table.
                    commend.CommandText = @"CREATE TABLE IF NOT EXISTS Subject_Students(
                                    Subject_Student_Id INTEGER PRIMARY KEY AUTOINCREMENT,
                                    Student_Id INTEGER,
                                    Subject_Name TEXT,
                                    Subject_Id INTEGER,
                                    FOREIGN KEY (Subject_Id) REFERENCES Subjects(Subject_Id)
                                        ON DELETE SET NULL
                                        ON UPDATE CASCADE,
                                    FOREIGN KEY (Student_Id) REFERENCES Students(Student_Id)
                                        ON DELETE SET NULL
                                        ON UPDATE CASCADE
                                    )";
                    commend.ExecuteNonQuery();

                    //Create Course_Subjects Table.
                    commend.CommandText = @"CREATE TABLE IF NOT EXISTS Course_Subjects(
                                    Course_Subject_Id INTEGER PRIMARY KEY AUTOINCREMENT,
                                    Course_Id INTEGER,
                                    Subject_Id INTEGER,
                                    FOREIGN KEY (Course_Id) REFERENCES Courses(Course_Id)
                                        ON DELETE SET NULL
                                        ON UPDATE CASCADE,
                                    FOREIGN KEY (Subject_Id) REFERENCES Subjects(Subject_Id)
                                        ON DELETE SET NULL
                                        ON UPDATE CASCADE
                                    )";
                    commend.ExecuteNonQuery();
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    MessageBox.Show($"Error creating tables: {ex.Message}");
                    throw;
                }

            }
        }
    }
}
