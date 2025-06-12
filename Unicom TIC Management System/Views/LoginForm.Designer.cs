namespace Unicom_TIC_Management_System.Views
{
    partial class LoginForm
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
            this.labelIncorrectPassword = new System.Windows.Forms.Label();
            this.labelIncorrectUserName = new System.Windows.Forms.Label();
            this.buttonTogglePassword = new System.Windows.Forms.Button();
            this.buttonRegister = new System.Windows.Forms.Button();
            this.buttonLogin = new System.Windows.Forms.Button();
            this.textBoxLoginPassword = new System.Windows.Forms.TextBox();
            this.textBoxLoginUserName = new System.Windows.Forms.TextBox();
            this.labelLoginPassword = new System.Windows.Forms.Label();
            this.labelLoginUserName = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // labelIncorrectPassword
            // 
            this.labelIncorrectPassword.AutoSize = true;
            this.labelIncorrectPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelIncorrectPassword.ForeColor = System.Drawing.Color.Red;
            this.labelIncorrectPassword.Location = new System.Drawing.Point(311, 241);
            this.labelIncorrectPassword.Name = "labelIncorrectPassword";
            this.labelIncorrectPassword.Size = new System.Drawing.Size(46, 18);
            this.labelIncorrectPassword.TabIndex = 38;
            this.labelIncorrectPassword.Text = "label2";
            // 
            // labelIncorrectUserName
            // 
            this.labelIncorrectUserName.AutoSize = true;
            this.labelIncorrectUserName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelIncorrectUserName.ForeColor = System.Drawing.Color.Red;
            this.labelIncorrectUserName.Location = new System.Drawing.Point(311, 152);
            this.labelIncorrectUserName.Name = "labelIncorrectUserName";
            this.labelIncorrectUserName.Size = new System.Drawing.Size(19, 18);
            this.labelIncorrectUserName.TabIndex = 37;
            this.labelIncorrectUserName.Text = "hi";
            // 
            // buttonTogglePassword
            // 
            this.buttonTogglePassword.BackColor = System.Drawing.SystemColors.Control;
            this.buttonTogglePassword.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonTogglePassword.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.buttonTogglePassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonTogglePassword.ForeColor = System.Drawing.Color.Black;
            this.buttonTogglePassword.Location = new System.Drawing.Point(635, 191);
            this.buttonTogglePassword.Name = "buttonTogglePassword";
            this.buttonTogglePassword.Size = new System.Drawing.Size(34, 31);
            this.buttonTogglePassword.TabIndex = 36;
            this.buttonTogglePassword.Text = "👁️";
            this.buttonTogglePassword.UseVisualStyleBackColor = false;
            // 
            // buttonRegister
            // 
            this.buttonRegister.BackColor = System.Drawing.Color.DarkCyan;
            this.buttonRegister.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.buttonRegister.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonRegister.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonRegister.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.buttonRegister.Location = new System.Drawing.Point(137, 308);
            this.buttonRegister.Name = "buttonRegister";
            this.buttonRegister.Size = new System.Drawing.Size(213, 43);
            this.buttonRegister.TabIndex = 35;
            this.buttonRegister.Text = "Register";
            this.buttonRegister.UseVisualStyleBackColor = false;
            // 
            // buttonLogin
            // 
            this.buttonLogin.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.buttonLogin.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonLogin.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonLogin.ForeColor = System.Drawing.Color.White;
            this.buttonLogin.Location = new System.Drawing.Point(452, 306);
            this.buttonLogin.Name = "buttonLogin";
            this.buttonLogin.Size = new System.Drawing.Size(217, 45);
            this.buttonLogin.TabIndex = 34;
            this.buttonLogin.Text = "Log in";
            this.buttonLogin.UseVisualStyleBackColor = false;
            // 
            // textBoxLoginPassword
            // 
            this.textBoxLoginPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxLoginPassword.Location = new System.Drawing.Point(314, 191);
            this.textBoxLoginPassword.Name = "textBoxLoginPassword";
            this.textBoxLoginPassword.Size = new System.Drawing.Size(355, 30);
            this.textBoxLoginPassword.TabIndex = 33;
            // 
            // textBoxLoginUserName
            // 
            this.textBoxLoginUserName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxLoginUserName.Location = new System.Drawing.Point(314, 100);
            this.textBoxLoginUserName.Name = "textBoxLoginUserName";
            this.textBoxLoginUserName.Size = new System.Drawing.Size(355, 30);
            this.textBoxLoginUserName.TabIndex = 32;
            // 
            // labelLoginPassword
            // 
            this.labelLoginPassword.AutoSize = true;
            this.labelLoginPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelLoginPassword.Location = new System.Drawing.Point(134, 194);
            this.labelLoginPassword.Name = "labelLoginPassword";
            this.labelLoginPassword.Size = new System.Drawing.Size(149, 25);
            this.labelLoginPassword.TabIndex = 31;
            this.labelLoginPassword.Text = "Password         :";
            // 
            // labelLoginUserName
            // 
            this.labelLoginUserName.AutoSize = true;
            this.labelLoginUserName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelLoginUserName.Location = new System.Drawing.Point(132, 103);
            this.labelLoginUserName.Name = "labelLoginUserName";
            this.labelLoginUserName.Size = new System.Drawing.Size(151, 25);
            this.labelLoginUserName.TabIndex = 30;
            this.labelLoginUserName.Text = "User Name       :";
            // 
            // LoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.labelIncorrectPassword);
            this.Controls.Add(this.labelIncorrectUserName);
            this.Controls.Add(this.buttonTogglePassword);
            this.Controls.Add(this.buttonRegister);
            this.Controls.Add(this.buttonLogin);
            this.Controls.Add(this.textBoxLoginPassword);
            this.Controls.Add(this.textBoxLoginUserName);
            this.Controls.Add(this.labelLoginPassword);
            this.Controls.Add(this.labelLoginUserName);
            this.Name = "LoginForm";
            this.Text = "LoginForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelIncorrectPassword;
        private System.Windows.Forms.Label labelIncorrectUserName;
        private System.Windows.Forms.Button buttonTogglePassword;
        private System.Windows.Forms.Button buttonRegister;
        private System.Windows.Forms.Button buttonLogin;
        private System.Windows.Forms.TextBox textBoxLoginPassword;
        private System.Windows.Forms.TextBox textBoxLoginUserName;
        private System.Windows.Forms.Label labelLoginPassword;
        private System.Windows.Forms.Label labelLoginUserName;
    }
}