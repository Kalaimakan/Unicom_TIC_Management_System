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
    public partial class RoomCreation : Form
    {
        Room room = new Room();
        RoomController roomController = new RoomController();
        public RoomCreation()
        {
            InitializeComponent();
            clearFields();
            loadRooms();
        }
        public void clearFields()
        {
            textBoxRoomName.Clear();
            comboBoxRoomType.SelectedIndex = -1;
        }

        public void loadRooms()
        {
            dataGridViewRooms.DataSource = roomController.GetAllRooms();
            dataGridViewRooms.Columns["Room_Name"].HeaderText = "Room Name";
            dataGridViewRooms.Columns["Room_Type"].HeaderText = "Room Type";
        }
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            room.Room_Name = textBoxRoomName.Text.Trim();
            room.Room_Type = comboBoxRoomType.SelectedItem?.ToString().Trim();
            bool isValid = true;
            if (string.IsNullOrWhiteSpace(room.Room_Name))
            {
                labelFillRoomName.Visible = true;
                labelFillRoomName.Text = "Enter the Room Name.";
                isValid = false;
            }
            if (string.IsNullOrWhiteSpace(room.Room_Type))
            {
                labelFillRoomType.Visible = true;
                labelFillRoomType.Text = "Select the Room Type.";
                isValid = false;
            }
            if (!isValid)
            {
                return;
            }
            //comfirm message
            var confirmResult = MessageBox.Show($"Are you sure you want to add {room.Room_Name}?", "Confirm Add", MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.Yes)
            {
                try
                {
                    roomController.AddRoom(room);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error adding room: {ex.Message}");
                    return;
                }
            }
            else
            {
                MessageBox.Show("Room addition cancelled.");
                return;
            }

            MessageBox.Show("Room added successfully!");
            clearFields();
            loadRooms();
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (room.Room_Id == 0)
            {
                MessageBox.Show("Please select a room to delete.");
                return;
            }
            //comfirm message
            var confirmResult = MessageBox.Show($"Are you sure you want to delete {room.Room_Name}?", "Confirm Delete", MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.Yes)
            {
                try
                {
                    roomController.DeleteRoom(room.Room_Id);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting room: {ex.Message}");
                    return;
                }
            }
            else
            {
                MessageBox.Show("Room deletion cancelled.");
                return;
            }
            MessageBox.Show("Room deleted successfully!");
            clearFields();
            loadRooms();
        }

        private void dataGridViewRooms_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridViewRow row = dataGridViewRooms.Rows[e.RowIndex];
            textBoxRoomName.Text = row.Cells["Room_Name"].Value.ToString();
            comboBoxRoomType.SelectedItem = row.Cells["Room_Type"].Value.ToString();
            room.Room_Id = Convert.ToInt32(row.Cells["Room_Id"].Value);
        }
    }
}
