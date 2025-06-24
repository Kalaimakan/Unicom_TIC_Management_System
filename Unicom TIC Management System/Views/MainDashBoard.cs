using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Unicom_TIC_Management_System.Views
{
    public partial class MainDashBoard : Form
    {
        public MainDashBoard()
        {
            InitializeComponent();
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
        private void buttonAdminManage_Click(object sender, EventArgs e)
        {
            panelCourseDepartmentButtons.Visible = false;
            panelAdminButtons.Visible = true;
            panelStaffButtons.Visible = false;
            panelLecturerButtons.Visible = false;
            panelStudentButtons.Visible = false;
            buttonRegisterAdmin.Visible = true;
            buttonViewAdmin.Visible = true;
            buttonUpdateAdmin.Visible = true;
            buttonDeleteAdmin.Visible = true;
        }

        private void buttonStaffManage_Click(object sender, EventArgs e)
        {
            panelCourseDepartmentButtons.Visible = false;
            panelAdminButtons.Visible = false;
            panelStaffButtons.Visible = true;
            panelLecturerButtons.Visible = false;
            panelStudentButtons.Visible = false;
            buttonRegisterStaff.Visible = true;
            buttonViewStaff.Visible = true;
            buttonUpdateStaff.Visible = true;
            buttonDeleteStaff.Visible = true;
        }
         
        private void buttonLecturerManage_Click(object sender, EventArgs e)
        {
            panelCourseDepartmentButtons.Visible = false;
            panelAdminButtons.Visible = false;
            panelStaffButtons.Visible = false;
            panelLecturerButtons.Visible = true;
            panelStudentButtons.Visible = false;
            buttonRegisterLecturer.Visible = true;
            buttonViewLecturer.Visible=true;
            buttonUpdateLecturer.Visible = true;
            buttonDeleteLecturer.Visible = true;
        }

        private void buttonStudentManage_Click(object sender, EventArgs e)
        {
            panelCourseDepartmentButtons.Visible = false;
            panelAdminButtons.Visible = false;
            panelStaffButtons.Visible = false;
            panelLecturerButtons.Visible = false;
            panelStudentButtons.Visible = true;
            buttonRegisterStudent.Visible = true;
            buttonViewStudent.Visible = true;
            buttonUpdateStudent.Visible = true;
            buttonDeleteStudent.Visible = true;

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

        }
    }
}
