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
    public partial class ViewStaffs: Form
    {
        StaffController staffController = new StaffController();
        public ViewStaffs()
        {
            InitializeComponent();
            loadStaffData();
        }

        public void loadStaffData()
        {
            List<Staff> staffs = staffController.GetAllStaff();
            if (staffs != null && staffs.Count > 0)
            {
                dataGridViewStaffs.DataSource = staffs;
                dataGridViewStaffs.RowHeadersVisible = false;
                dataGridViewStaffs.Columns["User_Name"].Visible = false;
                dataGridViewStaffs.Columns["Password"].Visible = false;
                dataGridViewStaffs.Columns["User_Email"].Visible = false;
            }
            

        }
    }
}
