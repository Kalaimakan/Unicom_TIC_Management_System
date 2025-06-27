using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Unicom_TIC_Management_System.Controllers;
using Unicom_TIC_Management_System.Models;

namespace Unicom_TIC_Management_System.Views
{
    public partial class MainDashBoard : Form
    {
        UserController userController = new UserController();
        public MainDashBoard()
        {
            InitializeComponent();
        }
        private int userId;
        private string UserRole;
        private string UserName;
        private string Password;
        public MainDashBoard(string userRole,int id, string userName, string password)
        {
            InitializeComponent();
            this.UserRole = userRole;
            this.UserName = userName;
            this.Password = password;
            this.userId = id;
        }
        public void RefreshUserInfo(User updatedUser)
        {
            labelShowUserName.Text = updatedUser.User_Name;
            labelShowPassword.Text = updatedUser.Password;
        }
        public void LoadForm(object formObj)
        {
            if (this.panelMainPanal.Controls.Count > 0)
            {
                this.panelMainPanal.Controls.RemoveAt(0);
            }

            Form form = formObj as Form;
            form.TopLevel = false;
            form.Dock = DockStyle.Fill;
            this.panelMainPanal.Controls.Add(form);
            this.panelMainPanal.Tag = form;
            form.Show();
        }
        private void MainDashBoard_Load(object sender, EventArgs e)
        {
            if (UserRole=="Admin")
            {
                labelRoleBasedAccess.Text = $"{UserRole} DashBoard";
                labelWelcome.Text = $"Welcome to Unicom Management System {UserName}!";
            }
            if (UserRole == "Staff" || UserRole == "Lecturer" || UserRole == "Student")
            {
                buttonRoom.Visible = false ;
                buttonAdminManage.Visible = false;
                buttonStaffManage.Visible = false;
                buttonRegisterStudent.Visible = false;
                buttonUpdateStudent.Visible = false;
                buttonDeleteStudent.Visible = false;
                buttonEducationManage.Visible = false;
                buttonLecturerManage.Visible = false;
                buttonTimetable.Visible = false;
                labelRoleBasedAccess.Text = $"{UserRole} DashBoard";
                labelWelcome.Text = $"Welcome to Unicom Management System {UserName}!";
            }
            if (UserRole == "Lecturer" || UserRole == "Student")
            {
                buttonRoomManage.Visible = false;
                labelRoleBasedAccess.Text = $"{UserRole} DashBoard";
                labelWelcome.Text = $"Welcome to Unicom Management System {UserName}!";
            }
            if (UserRole == "Student")
            {
                buttonStudentManage.Visible = false;
                buttonExam.Visible = false;
                labelRoleBasedAccess.Text = $"{UserRole} DashBoard";
                labelWelcome.Text = $"Welcome to Unicom Management System {UserName}!";
            }
            User user = userController.getUserById(userId);
            labelShowUserName.Text = user.User_Name;
            labelShowPassword.Text = user.Password;
        }
        private void LoadButtons(string place)
        {
            buttonRoom.Visible = false;
            panelTimetableButtons.Visible = false;
            panelExamMarkButtons.Visible = false;
            panelCourseDepartmentButtons.Visible = false;
            panelAdminButtons.Visible = false;
            panelStudentButtons.Visible = false;
            panelStaffButtons.Visible = false;
            panelLecturerButtons.Visible = false;
            if (place == "admin")
            {
                panelAdminButtons.Visible = true;
                buttonRegisterAdmin.Visible = true;
                buttonViewAdmin.Visible = true;
                buttonUpdateAdmin.Visible = true;
                buttonDeleteAdmin.Visible = true;
            }
            if (place == "staff")
            {
                panelStaffButtons.Visible = true;
                buttonRegisterStaff.Visible = true;
                buttonViewStaff.Visible = true;
                buttonUpdateStaff.Visible = true;
                buttonDeleteStaff.Visible = true;
            }
            if (place == "Lecturer")
            {
                panelLecturerButtons.Visible = true;
                buttonRegisterLecturer.Visible = true;
                buttonViewLecturer.Visible = true;
                buttonUpdateLecturer.Visible = true;
                buttonDeleteLecturer.Visible = true;

            }
            if (place == "Student")
            {
                panelStudentButtons.Visible = true;
                buttonRegisterStudent.Visible = true;
                buttonViewStudent.Visible = true;
                buttonUpdateStudent.Visible = true;
                buttonDeleteStudent.Visible = true;
            }
            if (place == "Course")
            {
                if (UserRole != "Student")
                    panelCourseDepartmentButtons.Visible = true;
                buttonCourse.Visible = true;
                buttonDepartment.Visible = true;
                buttonSubject.Visible = true;
            }
            if (place == "Exam")
            {
                panelExamMarkButtons.Visible = true;
                if (UserRole != "Student")
                    buttonExam.Visible = true;
                buttonMark.Visible = true;
                buttonViewMark.Visible = true;
            }
            if (place == "Room")
            {
                if (UserRole != "Student")
                    buttonRoomManage.Visible = true;
                    buttonRoom.Visible = true;

            }
            if (place == "Timetable")
            {
                panelTimetableButtons.Visible = true;
                buttonViewTimetable.Visible = true;
                if (UserRole != "Student")
                    buttonTimetable.Visible = true;
            }
        }
        private void buttonAdminManage_Click(object sender, EventArgs e)
        {
            LoadButtons("admin");
        }

        private void buttonStaffManage_Click(object sender, EventArgs e)
        {
            LoadButtons("staff");
        }

        private void buttonLecturerManage_Click(object sender, EventArgs e)
        {
            LoadButtons("Lecturer");
        }

        private void buttonStudentManage_Click(object sender, EventArgs e)
        {
            LoadButtons("Student");

        }
        private void buttonRegisterAdmin_Click(object sender, EventArgs e)
        {
            LoadForm(new AdminCreation());
        }

        private void buttonViewAdmin_Click_1(object sender, EventArgs e)
        {
            LoadForm(new ViewAdmin());
        }

        private void buttonUpdateAdmin_Click_1(object sender, EventArgs e)
        {
            LoadForm(new EditAdmin("Update"));
        }

        private void buttonDeleteAdmin_Click_1(object sender, EventArgs e)
        {
            LoadForm(new EditAdmin("Delete"));
        }

        private void buttonRegisterStaff_Click_1(object sender, EventArgs e)
        {
            LoadForm(new StaffCreation());
        }

        private void buttonViewStaff_Click_1(object sender, EventArgs e)
        {
            LoadForm(new ViewStaffs());
        }

        private void buttonUpdateStaff_Click_1(object sender, EventArgs e)
        {
            LoadForm(new EditStaffs("Update"));
        }

        private void buttonDeleteStaff_Click_1(object sender, EventArgs e)
        {
            LoadForm(new EditStaffs("Delete"));
        }

        private void buttonRegisterLecturer_Click_1(object sender, EventArgs e)
        {
            LoadForm(new LecturerCreation());
        }

        private void buttonViewLecturer_Click_1(object sender, EventArgs e)
        {
            LoadForm(new ViewLecturer());
        }

        private void buttonUpdateLecturer_Click_1(object sender, EventArgs e)
        {
            LoadForm(new EditLecturer("Update"));
        }

        private void buttonDeleteLecturer_Click_1(object sender, EventArgs e)
        {
            LoadForm(new EditLecturer("Delete"));
        }

        private void buttonRegisterStudent_Click_1(object sender, EventArgs e)
        {
            LoadForm(new StudentCreation());
        }

        private void buttonViewStudent_Click_1(object sender, EventArgs e)
        {
            LoadForm(new ViewStudents());
        }

        private void buttonUpdateStudent_Click_1(object sender, EventArgs e)
        {
            LoadForm(new EditStudent("Update"));

        }

        private void buttonDeleteStudent_Click_1(object sender, EventArgs e)
        {
            LoadForm(new EditStudent("Delete"));
        }

        private void buttonCourse_Click_1(object sender, EventArgs e)
        {
            LoadForm(new CourseCreatio());
        }

        private void buttonDepartment_Click(object sender, EventArgs e)
        {
            LoadForm(new DepartmentCeation());
        }

        private void buttonCourseDepartment_Click(object sender, EventArgs e)
        {
            LoadButtons("Course");

        }

        private void buttonExam_Click(object sender, EventArgs e)
        {
            LoadForm(new ExamCreation());
        }

        private void buttonMark_Click(object sender, EventArgs e)
        {
            LoadForm(new MarkCreation());
        }

        private void buttonExamMarks_Click(object sender, EventArgs e)
        {
            LoadButtons("Exam");

        }

        private void buttonTimetable_Click(object sender, EventArgs e)
        {
            LoadForm(new Timetable_Creation());

        }

        private void buttonViewTimetable_Click(object sender, EventArgs e)
        {
            LoadForm(new ViewTimetable());

        }

        private void buttonRoom_Click(object sender, EventArgs e)
        {
            LoadForm(new RoomCreation());

        }

        private void buttonRoomManage_Click(object sender, EventArgs e)
        {
            LoadButtons("Room");

        }

        private void buttonTimetableManage_Click(object sender, EventArgs e)
        {
            LoadButtons("Timetable");

        }

        private void buttonSubject_Click(object sender, EventArgs e)
        {
            LoadForm(new SubjectCreation());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();

        }

        private void buttonViewMark_Click(object sender, EventArgs e)
        {
            LoadForm(new ViewMarks());
        }

        private void buttonChange_Click(object sender, EventArgs e)
        {
            LoadForm(new ChangeUserNamePassword(this, userId));
        }
    }
}
