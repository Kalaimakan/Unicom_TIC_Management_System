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
    public partial class ViewAdmin : Form
    {
        AdminController adminController = new AdminController();
        public ViewAdmin()
        {
            InitializeComponent();
            loadAdminData();
        }

        public void loadAdminData()
        {
            List<Admin> admins = adminController.GetAllAdmin();
            if (admins != null && admins.Count > 0)
            {
                dataGridViewAdmin.DataSource = admins;
                dataGridViewAdmin.RowHeadersVisible = false;
                dataGridViewAdmin.Columns["User_Name"].Visible = false;
                dataGridViewAdmin.Columns["Password"].Visible = false;
                dataGridViewAdmin.Columns["User_Email"].Visible = false;

            }
            

        }
    }
}
