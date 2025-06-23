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
    public partial class Timetable_Creation : Form
    {
        TimeTable timeTable = new TimeTable();
        Subject subject = new Subject();
        Course course = new Course();
        Department department = new Department();
        Room room = new Room();
        RoomController roomController = new RoomController();
        CourseController courseController = new CourseController();
        DepartmentController departmentController = new DepartmentController();
        SubjectController subjectController = new SubjectController();
        TimetableController timetableController = new TimetableController();
        public Timetable_Creation()
        {
            InitializeComponent();
            loadTimetableData();
            dateTimePickerStartDate.Format = DateTimePickerFormat.Custom;
            dateTimePickerStartDate.CustomFormat = "hh:mm";
            dateTimePickerStartDate.ShowUpDown = true;
            dateTimePickerEndDate.Format = DateTimePickerFormat.Custom;
            dateTimePickerEndDate.CustomFormat = "hh:mm";
            dateTimePickerEndDate.ShowUpDown = true;
            clearFields();
            loadSubjectCombo();
            loadDepartmentCombo();
            loadRoomCombo();
            loadCourseCombo();

        }

        public void loadSubjectCombo()
        {
            var subjects = subjectController.GetSubjects();
            comboBoxSubject.DataSource = subjects;
            comboBoxSubject.DisplayMember = "Subject_Name";
            comboBoxSubject.ValueMember = "Subject_Id";
            comboBoxSubject.SelectedIndex = -1;
        }
        public void loadCourseCombo()
        {
            var Course = courseController.viewCourse();
            comboBoxCourse.DataSource = Course;
            comboBoxCourse.DisplayMember = "Course_Name";
            comboBoxCourse.ValueMember = "Course_Id";
            comboBoxCourse.SelectedIndex = -1;
        }

        public void loadDepartmentCombo()
        {
            var departments = departmentController.GetAllDepartment();
            comboBoxDepartment.DataSource = departments;
            comboBoxDepartment.DisplayMember = "Department_Name";
            comboBoxDepartment.ValueMember = "Id";
            comboBoxDepartment.SelectedIndex = -1;
        }

        public void loadRoomCombo()
        {
            var rooms = roomController.GetAllRooms();
            comboBoxRoom.DataSource = rooms;
            comboBoxRoom.DisplayMember = "Room_Name";
            comboBoxRoom.ValueMember = "Room_Id";
            comboBoxRoom.SelectedIndex = -1;
        }
        public void clearFields()
        {
            dateTimePickerDate.Value = DateTime.Today;
            dateTimePickerStartDate.Value = DateTime.Now;
            dateTimePickerEndDate.Value = DateTime.Now;
            comboBoxCourse.SelectedIndex = -1;
            comboBoxSubject.SelectedIndex = -1;
            comboBoxDepartment.SelectedIndex = -1;
            comboBoxRoom.SelectedIndex = -1;
        }
        public void loadTimetableData()
        {
            var data = timetableController.GetAllTimetables();
            dataGridViewTimetableview.DataSource = data;
            dataGridViewTimetableview.RowHeadersVisible = false;
            dataGridViewTimetableview.Columns["Subject_Id"].Visible = false;
            dataGridViewTimetableview.Columns["Course_Id"].Visible = false;
            dataGridViewTimetableview.Columns["Department_Id"].Visible = false;
            dataGridViewTimetableview.Columns["Room_Id"].Visible = false;
        }
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            bool isValid = true;
            if (dateTimePickerDate.Value.Date == DateTime.Now)
            {
                labelFillDate.Text = "Please select a date.";
                labelFillDate.Visible = true;
                isValid = false;
            }
            else
            {
                labelFillDate.Visible = false;
            }
            if (dateTimePickerStartDate.Value.Date == DateTime.Now)
            {
                labelFillStartTime.Text = "Please select a start time.";
                labelFillStartTime.Visible = true;
                isValid = false;
            }
            else
            {
                labelFillStartTime.Visible = false;
            }
            if (dateTimePickerEndDate.Value.Date == DateTime.Now)
            {
                labelFillEndTime.Text = "Please select an end time.";
                labelFillEndTime.Visible = true;
                isValid = false;
            }
            else
            {
                labelFillEndTime.Visible = false;
            }
            if (comboBoxCourse.SelectedIndex < 0)
            {
                labelFillCourse.Text = "Please select a course.";
                labelFillCourse.Visible = true;
                isValid = false;
            }
            else
            {
                labelFillCourse.Visible = false;
            }
            if (comboBoxSubject.SelectedIndex < 0)
            {
                labelFillSubject.Text = "Please select a subject.";
                labelFillSubject.Visible = true;
                isValid = false;
            }
            else
            {
                labelFillSubject.Visible = false;
            }
            if (comboBoxDepartment.SelectedIndex < 0)
            {
                labelFillDepartment.Text = "Please select a department.";
                labelFillDepartment.Visible = true;
                isValid = false;
            }
            else
            {
                labelFillDepartment.Visible = false;
            }
            if (!isValid)
            {
                return;
            }
            timeTable.Date = dateTimePickerDate.Value.ToString("yyyy-MM-dd");
            timeTable.Start_Time = dateTimePickerStartDate.Value.ToString();
            timeTable.End_Time = dateTimePickerEndDate.Value.ToString();
            subject = (Subject)comboBoxSubject.SelectedItem;
            course = (Course)comboBoxCourse.SelectedItem;
            department = (Department)comboBoxDepartment.SelectedItem;
            room = (Room)comboBoxRoom.SelectedItem;

            //confirmation message
            DialogResult confirm = MessageBox.Show(
                $"Do you want to add timetable for {subject.Subject_Name}?",
                "Confirm Add",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
             );
            if (confirm == DialogResult.Yes)
            {
                timeTable.Subject_Id = subject.Subject_Id;
                timeTable.Course_Id = course.Course_Id;
                timeTable.Department_Id = department.Id;
                timeTable.Room_Id = room.Room_Id;
                timetableController.AddTimetable(timeTable);
                MessageBox.Show("Timetable added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Timetable addition cancelled.", "Cancelled", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            clearFields();
            loadTimetableData();
        }

        private void dataGridViewTimetableview_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                // Clear previous selection
                dataGridViewTimetableview.ClearSelection();

                // Select the entire row
                dataGridViewTimetableview.Rows[e.RowIndex].Selected = true;

                DataGridViewRow row = dataGridViewTimetableview.Rows[e.RowIndex];
                dateTimePickerDate.Value = Convert.ToDateTime(row.Cells["Date"].Value);
                dateTimePickerStartDate.Value = Convert.ToDateTime(row.Cells["Start_Time"].Value);
                dateTimePickerEndDate.Value = Convert.ToDateTime(row.Cells["End_Time"].Value);

                // Set selected items in combo boxes
                comboBoxCourse.SelectedValue = row.Cells["Course_Id"].Value;
                comboBoxSubject.SelectedValue = row.Cells["Subject_Id"].Value;
                comboBoxDepartment.SelectedValue = row.Cells["Department_Id"].Value;
                comboBoxRoom.SelectedValue = row.Cells["Room_Id"].Value;
            }
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            if (dataGridViewTimetableview.CurrentRow == null)
            {
                MessageBox.Show("Please select a timetable to update.", "No row selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            // Get selected timetable ID
            foreach (DataGridViewColumn col in dataGridViewTimetableview.Columns)
            {
                Console.WriteLine(col.Name);
            }
            int timetableId = Convert.ToInt32(dataGridViewTimetableview.SelectedRows[0].Cells["Timetable_Id"].Value);

            // Create updated timetable object
            TimeTable updatedTimeTable = new TimeTable
            {
                Timetable_Id = timetableId,
                Date = dateTimePickerDate.Value.ToString("yyyy-MM-dd"),
                Start_Time = dateTimePickerStartDate.Value.ToString(),
                End_Time = dateTimePickerEndDate.Value.ToString(),
                Subject_Id = ((Subject)comboBoxSubject.SelectedItem).Subject_Id,
                Course_Id = ((Course)comboBoxCourse.SelectedItem).Course_Id,
                Department_Id = ((Department)comboBoxDepartment.SelectedItem).Id,
                Room_Id = ((Room)comboBoxRoom.SelectedItem).Room_Id
            };

            // Confirm update
            DialogResult result = MessageBox.Show("Are you sure you want to update this timetable?", "Confirm Update", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                timetableController.UpdateTimetable(updatedTimeTable);
                MessageBox.Show("Timetable updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                clearFields();
                loadTimetableData();
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (dataGridViewTimetableview.CurrentRow == null)
            {
                MessageBox.Show("Please select a timetable to Delete.", "No row selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            // Get the selected timetable ID
            int timetableId = Convert.ToInt32(dataGridViewTimetableview.CurrentRow.Cells["Timetable_Id"].Value);

            // Get subject name for confirmation message
            string subjectName = dataGridViewTimetableview.CurrentRow.Cells["Subject_Name"].Value.ToString();

            // Confirmation dialog
            DialogResult result = MessageBox.Show(
                $"Are you sure you want to delete the timetable for {subjectName}?",
                "Confirm Deletion",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                try
                {
                    timetableController.DeleteTimetable(timetableId);
                    loadTimetableData();
                    clearFields();
                    MessageBox.Show("Timetable deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting timetable: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }
    }
}
