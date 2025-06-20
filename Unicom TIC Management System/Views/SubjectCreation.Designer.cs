namespace Unicom_TIC_Management_System.Views
{
    partial class SubjectCreation
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.labelSubjectName = new System.Windows.Forms.Label();
            this.labelCourseName = new System.Windows.Forms.Label();
            this.labelDepartmentName = new System.Windows.Forms.Label();
            this.textBoxSubjectName = new System.Windows.Forms.TextBox();
            this.comboBoxCourseName = new System.Windows.Forms.ComboBox();
            this.comboBoxDepartmentName = new System.Windows.Forms.ComboBox();
            this.buttonClear = new System.Windows.Forms.Button();
            this.buttonBack = new System.Windows.Forms.Button();
            this.buttonDelete = new System.Windows.Forms.Button();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.labelFillSubjectName = new System.Windows.Forms.Label();
            this.labelFillCourseName = new System.Windows.Forms.Label();
            this.labelFillDepartmentName = new System.Windows.Forms.Label();
            this.dataGridViewSubject = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewSubject)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(262, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(283, 32);
            this.label1.TabIndex = 0;
            this.label1.Text = "Subject Management";
            // 
            // labelSubjectName
            // 
            this.labelSubjectName.AutoSize = true;
            this.labelSubjectName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelSubjectName.Location = new System.Drawing.Point(87, 113);
            this.labelSubjectName.Name = "labelSubjectName";
            this.labelSubjectName.Size = new System.Drawing.Size(182, 22);
            this.labelSubjectName.TabIndex = 1;
            this.labelSubjectName.Text = "Subject Name           :";
            // 
            // labelCourseName
            // 
            this.labelCourseName.AutoSize = true;
            this.labelCourseName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelCourseName.Location = new System.Drawing.Point(87, 184);
            this.labelCourseName.Name = "labelCourseName";
            this.labelCourseName.Size = new System.Drawing.Size(180, 22);
            this.labelCourseName.TabIndex = 2;
            this.labelCourseName.Text = "Course Name           :";
            // 
            // labelDepartmentName
            // 
            this.labelDepartmentName.AutoSize = true;
            this.labelDepartmentName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelDepartmentName.Location = new System.Drawing.Point(87, 262);
            this.labelDepartmentName.Name = "labelDepartmentName";
            this.labelDepartmentName.Size = new System.Drawing.Size(180, 22);
            this.labelDepartmentName.TabIndex = 3;
            this.labelDepartmentName.Text = "Department Name    :";
            // 
            // textBoxSubjectName
            // 
            this.textBoxSubjectName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxSubjectName.Location = new System.Drawing.Point(336, 113);
            this.textBoxSubjectName.Name = "textBoxSubjectName";
            this.textBoxSubjectName.Size = new System.Drawing.Size(345, 28);
            this.textBoxSubjectName.TabIndex = 4;
            // 
            // comboBoxCourseName
            // 
            this.comboBoxCourseName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxCourseName.FormattingEnabled = true;
            this.comboBoxCourseName.Location = new System.Drawing.Point(336, 184);
            this.comboBoxCourseName.Name = "comboBoxCourseName";
            this.comboBoxCourseName.Size = new System.Drawing.Size(345, 30);
            this.comboBoxCourseName.TabIndex = 5;
            // 
            // comboBoxDepartmentName
            // 
            this.comboBoxDepartmentName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxDepartmentName.FormattingEnabled = true;
            this.comboBoxDepartmentName.Location = new System.Drawing.Point(336, 259);
            this.comboBoxDepartmentName.Name = "comboBoxDepartmentName";
            this.comboBoxDepartmentName.Size = new System.Drawing.Size(345, 30);
            this.comboBoxDepartmentName.TabIndex = 6;
            // 
            // buttonClear
            // 
            this.buttonClear.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.buttonClear.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonClear.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.buttonClear.Location = new System.Drawing.Point(421, 340);
            this.buttonClear.Margin = new System.Windows.Forms.Padding(2);
            this.buttonClear.Name = "buttonClear";
            this.buttonClear.Size = new System.Drawing.Size(95, 33);
            this.buttonClear.TabIndex = 28;
            this.buttonClear.Text = "Clear";
            this.buttonClear.UseVisualStyleBackColor = false;
            this.buttonClear.Click += new System.EventHandler(this.buttonClear_Click);
            // 
            // buttonBack
            // 
            this.buttonBack.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.buttonBack.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonBack.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.buttonBack.Location = new System.Drawing.Point(586, 340);
            this.buttonBack.Margin = new System.Windows.Forms.Padding(2);
            this.buttonBack.Name = "buttonBack";
            this.buttonBack.Size = new System.Drawing.Size(95, 33);
            this.buttonBack.TabIndex = 27;
            this.buttonBack.Text = "Back";
            this.buttonBack.UseVisualStyleBackColor = false;
            // 
            // buttonDelete
            // 
            this.buttonDelete.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.buttonDelete.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonDelete.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.buttonDelete.Location = new System.Drawing.Point(259, 340);
            this.buttonDelete.Margin = new System.Windows.Forms.Padding(2);
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.Size = new System.Drawing.Size(95, 33);
            this.buttonDelete.TabIndex = 26;
            this.buttonDelete.Text = "Delete";
            this.buttonDelete.UseVisualStyleBackColor = false;
            this.buttonDelete.Click += new System.EventHandler(this.buttonDelete_Click);
            // 
            // buttonAdd
            // 
            this.buttonAdd.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.buttonAdd.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonAdd.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.buttonAdd.Location = new System.Drawing.Point(91, 340);
            this.buttonAdd.Margin = new System.Windows.Forms.Padding(2);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(95, 33);
            this.buttonAdd.TabIndex = 25;
            this.buttonAdd.Text = "Add";
            this.buttonAdd.UseVisualStyleBackColor = false;
            this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
            // 
            // labelFillSubjectName
            // 
            this.labelFillSubjectName.AutoSize = true;
            this.labelFillSubjectName.ForeColor = System.Drawing.Color.Red;
            this.labelFillSubjectName.Location = new System.Drawing.Point(333, 149);
            this.labelFillSubjectName.Name = "labelFillSubjectName";
            this.labelFillSubjectName.Size = new System.Drawing.Size(0, 16);
            this.labelFillSubjectName.TabIndex = 29;
            // 
            // labelFillCourseName
            // 
            this.labelFillCourseName.AutoSize = true;
            this.labelFillCourseName.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelFillCourseName.ForeColor = System.Drawing.Color.Red;
            this.labelFillCourseName.Location = new System.Drawing.Point(333, 220);
            this.labelFillCourseName.Name = "labelFillCourseName";
            this.labelFillCourseName.Size = new System.Drawing.Size(0, 16);
            this.labelFillCourseName.TabIndex = 30;
            // 
            // labelFillDepartmentName
            // 
            this.labelFillDepartmentName.AutoSize = true;
            this.labelFillDepartmentName.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelFillDepartmentName.ForeColor = System.Drawing.Color.Red;
            this.labelFillDepartmentName.Location = new System.Drawing.Point(333, 296);
            this.labelFillDepartmentName.Name = "labelFillDepartmentName";
            this.labelFillDepartmentName.Size = new System.Drawing.Size(0, 16);
            this.labelFillDepartmentName.TabIndex = 31;
            // 
            // dataGridViewSubject
            // 
            this.dataGridViewSubject.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewSubject.Location = new System.Drawing.Point(91, 409);
            this.dataGridViewSubject.Name = "dataGridViewSubject";
            this.dataGridViewSubject.RowHeadersWidth = 51;
            this.dataGridViewSubject.RowTemplate.Height = 24;
            this.dataGridViewSubject.Size = new System.Drawing.Size(590, 259);
            this.dataGridViewSubject.TabIndex = 32;
            this.dataGridViewSubject.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridViewSubject_CellMouseClick);
            // 
            // SubjectCreation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(776, 694);
            this.Controls.Add(this.dataGridViewSubject);
            this.Controls.Add(this.labelFillDepartmentName);
            this.Controls.Add(this.labelFillCourseName);
            this.Controls.Add(this.labelFillSubjectName);
            this.Controls.Add(this.buttonClear);
            this.Controls.Add(this.buttonBack);
            this.Controls.Add(this.buttonDelete);
            this.Controls.Add(this.buttonAdd);
            this.Controls.Add(this.comboBoxDepartmentName);
            this.Controls.Add(this.comboBoxCourseName);
            this.Controls.Add(this.textBoxSubjectName);
            this.Controls.Add(this.labelDepartmentName);
            this.Controls.Add(this.labelCourseName);
            this.Controls.Add(this.labelSubjectName);
            this.Controls.Add(this.label1);
            this.Name = "SubjectCreation";
            this.Text = "SubjectCreation";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewSubject)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelSubjectName;
        private System.Windows.Forms.Label labelCourseName;
        private System.Windows.Forms.Label labelDepartmentName;
        private System.Windows.Forms.TextBox textBoxSubjectName;
        private System.Windows.Forms.ComboBox comboBoxCourseName;
        private System.Windows.Forms.ComboBox comboBoxDepartmentName;
        private System.Windows.Forms.Button buttonClear;
        private System.Windows.Forms.Button buttonBack;
        private System.Windows.Forms.Button buttonDelete;
        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.Label labelFillSubjectName;
        private System.Windows.Forms.Label labelFillCourseName;
        private System.Windows.Forms.Label labelFillDepartmentName;
        private System.Windows.Forms.DataGridView dataGridViewSubject;
    }
}