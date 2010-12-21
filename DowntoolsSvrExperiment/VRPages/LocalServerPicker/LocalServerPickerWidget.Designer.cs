namespace DowntoolsSvrExperiment.VRPages.LocalServerPicker
{
    partial class LocalServerPickerWidget
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LocalServerPickerWidget));
            this.m_Instances = new System.Windows.Forms.ComboBox();
            this.m_WindowsAuth = new System.Windows.Forms.RadioButton();
            this.m_SqlServerAuth = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.m_UserName = new System.Windows.Forms.TextBox();
            this.m_Password = new System.Windows.Forms.TextBox();
            this.m_UserNameLabel = new System.Windows.Forms.Label();
            this.m_PasswordLabel = new System.Windows.Forms.Label();
            this.m_WarningText = new System.Windows.Forms.Label();
            this.m_WarningIcon = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.m_WarningIcon)).BeginInit();
            this.SuspendLayout();
            // 
            // m_Instances
            // 
            this.m_Instances.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_Instances.FormattingEnabled = true;
            this.m_Instances.Location = new System.Drawing.Point(136, 100);
            this.m_Instances.Name = "m_Instances";
            this.m_Instances.Size = new System.Drawing.Size(179, 21);
            this.m_Instances.TabIndex = 0;
            // 
            // m_WindowsAuth
            // 
            this.m_WindowsAuth.AutoSize = true;
            this.m_WindowsAuth.Checked = true;
            this.m_WindowsAuth.Location = new System.Drawing.Point(136, 140);
            this.m_WindowsAuth.Name = "m_WindowsAuth";
            this.m_WindowsAuth.Size = new System.Drawing.Size(139, 17);
            this.m_WindowsAuth.TabIndex = 1;
            this.m_WindowsAuth.TabStop = true;
            this.m_WindowsAuth.Text = "Windows authentication";
            this.m_WindowsAuth.UseVisualStyleBackColor = true;
            // 
            // m_SqlServerAuth
            // 
            this.m_SqlServerAuth.AutoSize = true;
            this.m_SqlServerAuth.Location = new System.Drawing.Point(136, 164);
            this.m_SqlServerAuth.Name = "m_SqlServerAuth";
            this.m_SqlServerAuth.Size = new System.Drawing.Size(151, 17);
            this.m_SqlServerAuth.TabIndex = 2;
            this.m_SqlServerAuth.Text = "SQL Server Authentication";
            this.m_SqlServerAuth.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(79, 103);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "&Instance:";
            // 
            // m_UserName
            // 
            this.m_UserName.Enabled = false;
            this.m_UserName.Location = new System.Drawing.Point(136, 204);
            this.m_UserName.Name = "m_UserName";
            this.m_UserName.Size = new System.Drawing.Size(179, 20);
            this.m_UserName.TabIndex = 4;
            // 
            // m_Password
            // 
            this.m_Password.Enabled = false;
            this.m_Password.Location = new System.Drawing.Point(136, 232);
            this.m_Password.Name = "m_Password";
            this.m_Password.Size = new System.Drawing.Size(179, 20);
            this.m_Password.TabIndex = 5;
            // 
            // m_UserNameLabel
            // 
            this.m_UserNameLabel.AutoSize = true;
            this.m_UserNameLabel.Enabled = false;
            this.m_UserNameLabel.Location = new System.Drawing.Point(69, 207);
            this.m_UserNameLabel.Name = "m_UserNameLabel";
            this.m_UserNameLabel.Size = new System.Drawing.Size(61, 13);
            this.m_UserNameLabel.TabIndex = 6;
            this.m_UserNameLabel.Text = "&User name:";
            // 
            // m_PasswordLabel
            // 
            this.m_PasswordLabel.AutoSize = true;
            this.m_PasswordLabel.Enabled = false;
            this.m_PasswordLabel.Location = new System.Drawing.Point(74, 235);
            this.m_PasswordLabel.Name = "m_PasswordLabel";
            this.m_PasswordLabel.Size = new System.Drawing.Size(56, 13);
            this.m_PasswordLabel.TabIndex = 7;
            this.m_PasswordLabel.Text = "&Password:";
            // 
            // m_WarningText
            // 
            this.m_WarningText.Location = new System.Drawing.Point(72, 272);
            this.m_WarningText.Name = "m_WarningText";
            this.m_WarningText.Size = new System.Drawing.Size(240, 80);
            this.m_WarningText.TabIndex = 8;
            // 
            // m_WarningIcon
            // 
            this.m_WarningIcon.InitialImage = ((System.Drawing.Image)(resources.GetObject("m_WarningIcon.InitialImage")));
            this.m_WarningIcon.Location = new System.Drawing.Point(10, 272);
            this.m_WarningIcon.Name = "m_WarningIcon";
            this.m_WarningIcon.Size = new System.Drawing.Size(56, 49);
            this.m_WarningIcon.TabIndex = 9;
            this.m_WarningIcon.TabStop = false;
            // 
            // LocalServerPickerWidget
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.m_WarningIcon);
            this.Controls.Add(this.m_WarningText);
            this.Controls.Add(this.m_PasswordLabel);
            this.Controls.Add(this.m_UserNameLabel);
            this.Controls.Add(this.m_Password);
            this.Controls.Add(this.m_UserName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.m_SqlServerAuth);
            this.Controls.Add(this.m_WindowsAuth);
            this.Controls.Add(this.m_Instances);
            this.Name = "LocalServerPickerWidget";
            this.Size = new System.Drawing.Size(396, 362);
            ((System.ComponentModel.ISupportInitialize)(this.m_WarningIcon)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox m_Instances;
        private System.Windows.Forms.RadioButton m_WindowsAuth;
        private System.Windows.Forms.RadioButton m_SqlServerAuth;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox m_UserName;
        private System.Windows.Forms.TextBox m_Password;
        private System.Windows.Forms.Label m_UserNameLabel;
        private System.Windows.Forms.Label m_PasswordLabel;
        private System.Windows.Forms.Label m_WarningText;
        private System.Windows.Forms.PictureBox m_WarningIcon;
    }
}
