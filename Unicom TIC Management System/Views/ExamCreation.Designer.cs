namespace Unicom_TIC_Management_System.Views
{
    partial class ExamCreation
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ExamCreation));
            this.textBoxExamName = new System.Windows.Forms.TextBox();
            this.labelFirstName = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.dateTimePickerExamDate = new System.Windows.Forms.DateTimePicker();
            this.comboBoxExamType = new System.Windows.Forms.ComboBox();
            this.comboBoxCourse = new System.Windows.Forms.ComboBox();
            this.comboBoxSubject = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.buttonUpdate = new System.Windows.Forms.Button();
            this.buttonDelete = new System.Windows.Forms.Button();
            this.buttonBack = new System.Windows.Forms.Button();
            this.dataGridViewExams = new System.Windows.Forms.DataGridView();
            this.labelFillExamName = new System.Windows.Forms.Label();
            this.labelFillExamDate = new System.Windows.Forms.Label();
            this.labelFillExamType = new System.Windows.Forms.Label();
            this.labelFillSubject = new System.Windows.Forms.Label();
            this.labelFillCourse = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewExams)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // textBoxExamName
            // 
            this.textBoxExamName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxExamName.ForeColor = System.Drawing.Color.Black;
            this.textBoxExamName.Location = new System.Drawing.Point(272, 102);
            this.textBoxExamName.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBoxExamName.Name = "textBoxExamName";
            this.textBoxExamName.Size = new System.Drawing.Size(305, 28);
            this.textBoxExamName.TabIndex = 107;
            // 
            // labelFirstName
            // 
            this.labelFirstName.AutoSize = true;
            this.labelFirstName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelFirstName.Location = new System.Drawing.Point(72, 102);
            this.labelFirstName.Name = "labelFirstName";
            this.labelFirstName.Size = new System.Drawing.Size(162, 22);
            this.labelFirstName.TabIndex = 106;
            this.labelFirstName.Text = "Exam Name          :";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(206, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(219, 29);
            this.label1.TabIndex = 108;
            this.label1.Text = "Exam Management";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(72, 161);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(163, 22);
            this.label2.TabIndex = 109;
            this.label2.Text = "Exam Date            :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(72, 216);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(161, 22);
            this.label3.TabIndex = 110;
            this.label3.Text = "Exam Type           :";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(72, 321);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(158, 22);
            this.label4.TabIndex = 111;
            this.label4.Text = "Course                 :";
            // 
            // dateTimePickerExamDate
            // 
            this.dateTimePickerExamDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTimePickerExamDate.Location = new System.Drawing.Point(272, 159);
            this.dateTimePickerExamDate.Name = "dateTimePickerExamDate";
            this.dateTimePickerExamDate.Size = new System.Drawing.Size(305, 28);
            this.dateTimePickerExamDate.TabIndex = 112;
            // 
            // comboBoxExamType
            // 
            this.comboBoxExamType.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxExamType.FormattingEnabled = true;
            this.comboBoxExamType.Items.AddRange(new object[] {
            "Physical",
            "Online"});
            this.comboBoxExamType.Location = new System.Drawing.Point(272, 214);
            this.comboBoxExamType.Name = "comboBoxExamType";
            this.comboBoxExamType.Size = new System.Drawing.Size(305, 28);
            this.comboBoxExamType.TabIndex = 113;
            // 
            // comboBoxCourse
            // 
            this.comboBoxCourse.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxCourse.FormattingEnabled = true;
            this.comboBoxCourse.Location = new System.Drawing.Point(272, 320);
            this.comboBoxCourse.Name = "comboBoxCourse";
            this.comboBoxCourse.Size = new System.Drawing.Size(305, 28);
            this.comboBoxCourse.TabIndex = 114;
            // 
            // comboBoxSubject
            // 
            this.comboBoxSubject.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxSubject.FormattingEnabled = true;
            this.comboBoxSubject.Location = new System.Drawing.Point(272, 266);
            this.comboBoxSubject.Name = "comboBoxSubject";
            this.comboBoxSubject.Size = new System.Drawing.Size(305, 28);
            this.comboBoxSubject.TabIndex = 116;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(72, 267);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(160, 22);
            this.label5.TabIndex = 115;
            this.label5.Text = "Subject                 :";
            // 
            // buttonAdd
            // 
            this.buttonAdd.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.buttonAdd.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonAdd.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonAdd.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.buttonAdd.Location = new System.Drawing.Point(76, 378);
            this.buttonAdd.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(98, 38);
            this.buttonAdd.TabIndex = 117;
            this.buttonAdd.Text = "Add";
            this.buttonAdd.UseVisualStyleBackColor = false;
            this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
            // 
            // buttonUpdate
            // 
            this.buttonUpdate.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.buttonUpdate.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonUpdate.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonUpdate.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.buttonUpdate.Location = new System.Drawing.Point(211, 378);
            this.buttonUpdate.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonUpdate.Name = "buttonUpdate";
            this.buttonUpdate.Size = new System.Drawing.Size(98, 38);
            this.buttonUpdate.TabIndex = 118;
            this.buttonUpdate.Text = "Update";
            this.buttonUpdate.UseVisualStyleBackColor = false;
            this.buttonUpdate.Click += new System.EventHandler(this.buttonUpdate_Click);
            // 
            // buttonDelete
            // 
            this.buttonDelete.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.buttonDelete.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonDelete.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonDelete.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.buttonDelete.Location = new System.Drawing.Point(345, 378);
            this.buttonDelete.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.Size = new System.Drawing.Size(98, 38);
            this.buttonDelete.TabIndex = 119;
            this.buttonDelete.Text = "Delete";
            this.buttonDelete.UseVisualStyleBackColor = false;
            this.buttonDelete.Click += new System.EventHandler(this.buttonDelete_Click);
            // 
            // buttonBack
            // 
            this.buttonBack.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.buttonBack.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonBack.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonBack.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.buttonBack.Location = new System.Drawing.Point(479, 378);
            this.buttonBack.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonBack.Name = "buttonBack";
            this.buttonBack.Size = new System.Drawing.Size(98, 38);
            this.buttonBack.TabIndex = 120;
            this.buttonBack.Text = "Back";
            this.buttonBack.UseVisualStyleBackColor = false;
            // 
            // dataGridViewExams
            // 
            this.dataGridViewExams.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewExams.Location = new System.Drawing.Point(76, 440);
            this.dataGridViewExams.Name = "dataGridViewExams";
            this.dataGridViewExams.RowHeadersWidth = 51;
            this.dataGridViewExams.RowTemplate.Height = 24;
            this.dataGridViewExams.Size = new System.Drawing.Size(501, 218);
            this.dataGridViewExams.TabIndex = 121;
            this.dataGridViewExams.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridViewExams_CellMouseClick);
            // 
            // labelFillExamName
            // 
            this.labelFillExamName.AutoSize = true;
            this.labelFillExamName.ForeColor = System.Drawing.Color.Red;
            this.labelFillExamName.Location = new System.Drawing.Point(269, 132);
            this.labelFillExamName.Name = "labelFillExamName";
            this.labelFillExamName.Size = new System.Drawing.Size(0, 16);
            this.labelFillExamName.TabIndex = 122;
            // 
            // labelFillExamDate
            // 
            this.labelFillExamDate.AutoSize = true;
            this.labelFillExamDate.ForeColor = System.Drawing.Color.Red;
            this.labelFillExamDate.Location = new System.Drawing.Point(269, 190);
            this.labelFillExamDate.Name = "labelFillExamDate";
            this.labelFillExamDate.Size = new System.Drawing.Size(0, 16);
            this.labelFillExamDate.TabIndex = 123;
            // 
            // labelFillExamType
            // 
            this.labelFillExamType.AutoSize = true;
            this.labelFillExamType.ForeColor = System.Drawing.Color.Red;
            this.labelFillExamType.Location = new System.Drawing.Point(269, 245);
            this.labelFillExamType.Name = "labelFillExamType";
            this.labelFillExamType.Size = new System.Drawing.Size(0, 16);
            this.labelFillExamType.TabIndex = 124;
            // 
            // labelFillSubject
            // 
            this.labelFillSubject.AutoSize = true;
            this.labelFillSubject.ForeColor = System.Drawing.Color.Red;
            this.labelFillSubject.Location = new System.Drawing.Point(269, 297);
            this.labelFillSubject.Name = "labelFillSubject";
            this.labelFillSubject.Size = new System.Drawing.Size(0, 16);
            this.labelFillSubject.TabIndex = 125;
            // 
            // labelFillCourse
            // 
            this.labelFillCourse.AutoSize = true;
            this.labelFillCourse.ForeColor = System.Drawing.Color.Red;
            this.labelFillCourse.Location = new System.Drawing.Point(269, 351);
            this.labelFillCourse.Name = "labelFillCourse";
            this.labelFillCourse.Size = new System.Drawing.Size(0, 16);
            this.labelFillCourse.TabIndex = 126;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(12, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(127, 54);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 127;
            this.pictureBox1.TabStop = false;
            // 
            // ExamCreation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(650, 670);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.labelFillCourse);
            this.Controls.Add(this.labelFillSubject);
            this.Controls.Add(this.labelFillExamType);
            this.Controls.Add(this.labelFillExamDate);
            this.Controls.Add(this.labelFillExamName);
            this.Controls.Add(this.dataGridViewExams);
            this.Controls.Add(this.buttonBack);
            this.Controls.Add(this.buttonDelete);
            this.Controls.Add(this.buttonUpdate);
            this.Controls.Add(this.buttonAdd);
            this.Controls.Add(this.comboBoxSubject);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.comboBoxCourse);
            this.Controls.Add(this.comboBoxExamType);
            this.Controls.Add(this.dateTimePickerExamDate);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxExamName);
            this.Controls.Add(this.labelFirstName);
            this.Name = "ExamCreation";
            this.Text = "ExamCreation";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewExams)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxExamName;
        private System.Windows.Forms.Label labelFirstName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dateTimePickerExamDate;
        private System.Windows.Forms.ComboBox comboBoxExamType;
        private System.Windows.Forms.ComboBox comboBoxCourse;
        private System.Windows.Forms.ComboBox comboBoxSubject;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.Button buttonUpdate;
        private System.Windows.Forms.Button buttonDelete;
        private System.Windows.Forms.Button buttonBack;
        private System.Windows.Forms.DataGridView dataGridViewExams;
        private System.Windows.Forms.Label labelFillExamName;
        private System.Windows.Forms.Label labelFillExamDate;
        private System.Windows.Forms.Label labelFillExamType;
        private System.Windows.Forms.Label labelFillSubject;
        private System.Windows.Forms.Label labelFillCourse;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}