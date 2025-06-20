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
    public partial class DepartmentCeation : Form
    {
        DepartmentController departmentController = new DepartmentController();
        Department addDepartment = new Department();
        public DepartmentCeation()
        {
            InitializeComponent();
            LoadDepartmentData();
            SetPlaceholders();
        }

        public void SetPlaceholders()
        {
            ApplyPlaceholder(textBoxAddDepartment, "Enter the Department Name");
        }
        // apply placeholder to TextBox
        private void ApplyPlaceholder(TextBox textBox, string placeholder)
        {
            textBox.Text = placeholder;
            textBox.ForeColor = Color.Gray;
            textBox.Enter += (s, e) =>
            {
                if (textBox.Text == placeholder)
                {
                    textBox.Text = "";
                    textBox.ForeColor = Color.Black;
                }
            };
            textBox.Leave += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(textBox.Text))
                {
                    textBox.Text = placeholder;
                    textBox.ForeColor = Color.Gray;
                }
            };
        }
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            addDepartment.Department_Name = textBoxAddDepartment.Text.Trim();

            // Validate input
            if (string.IsNullOrWhiteSpace(addDepartment.Department_Name) || addDepartment.Department_Name == "Enter the Department Name")
            {
                labelFillDepartment.Text = "Please enter a department name.";
                labelFillDepartment.Visible = true;
                return;
            }

            try
            {
                departmentController.CreateDepartment(addDepartment);
                MessageBox.Show($"{addDepartment.Department_Name} Department created successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error creating department: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            ClearFields();
            LoadDepartmentData();
        }
        public void LoadDepartmentData()
        {
            var departments = departmentController.GetAllDepartment();
            dataGridViewDepartment.AutoGenerateColumns = true;
            dataGridViewDepartment.DataSource = departments;

            dataGridViewDepartment.Columns["Department_Name"].HeaderText = "Department Name";
            dataGridViewDepartment.ClearSelection();
        }
        public void ClearFields()
        {
            textBoxAddDepartment.Clear();
            labelFillDepartment.Visible = false;
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (dataGridViewDepartment.SelectedRows.Count == 0)
            {
                labelFillDepartment.Text = "Please select a department to delete.";
                labelFillDepartment.Visible = true;
            }
            if (dataGridViewDepartment.SelectedRows.Count > 0)
            {
                var confirm = MessageBox.Show($"Are you sure you want to delete {addDepartment.Department_Name} ?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (confirm == DialogResult.Yes)
                {
                    departmentController.DeleteDepartment(addDepartment.Id);
                    ClearFields();
                    LoadDepartmentData();
                }
                else if (confirm == DialogResult.No)
                {
                    MessageBox.Show($"Deletation Cancaled.", "Cancel Message", MessageBoxButtons.OK);
                }
            }
            SetPlaceholders();
        }

        private void dataGridViewDepartment_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridViewDepartment.Rows[e.RowIndex];
                addDepartment.Id = Convert.ToInt32(row.Cells["Id"].Value);
                addDepartment.Department_Name = row.Cells["Department_Name"].Value.ToString();
                textBoxAddDepartment.Text = addDepartment.Department_Name;
            }
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            ClearFields();
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
