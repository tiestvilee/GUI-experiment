namespace DowntoolsSvrExperiment.WizardControl
{
    partial class WizardViewWidget
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
            this.m_Page = new System.Windows.Forms.Panel();
            this.m_PageList = new System.Windows.Forms.FlowLayoutPanel();
            this.m_TopBorderPanel = new System.Windows.Forms.Panel();
            this.m_ButtonPanel = new System.Windows.Forms.Panel();
            this.m_BottomBorderPanel = new System.Windows.Forms.Panel();
            this.m_PreviousButton = new System.Windows.Forms.Button();
            this.m_NextButton = new System.Windows.Forms.Button();
            this.m_CancelButton = new System.Windows.Forms.Button();
            this.m_PageName = new System.Windows.Forms.Label();
            this.m_ButtonPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_Page
            // 
            this.m_Page.BackColor = System.Drawing.SystemColors.Control;
            this.m_Page.Location = new System.Drawing.Point(148, 52);
            this.m_Page.Name = "m_Page";
            this.m_Page.Size = new System.Drawing.Size(332, 344);
            this.m_Page.TabIndex = 0;
            // 
            // m_PageList
            // 
            this.m_PageList.BackColor = System.Drawing.Color.Transparent;
            this.m_PageList.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.m_PageList.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_PageList.ForeColor = System.Drawing.SystemColors.GrayText;
            this.m_PageList.Location = new System.Drawing.Point(0, 52);
            this.m_PageList.Name = "m_PageList";
            this.m_PageList.Size = new System.Drawing.Size(148, 344);
            this.m_PageList.TabIndex = 1;
            // 
            // m_TopBorderPanel
            // 
            this.m_TopBorderPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_TopBorderPanel.BackColor = System.Drawing.Color.Black;
            this.m_TopBorderPanel.Location = new System.Drawing.Point(0, 52);
            this.m_TopBorderPanel.Name = "m_TopBorderPanel";
            this.m_TopBorderPanel.Size = new System.Drawing.Size(480, 1);
            this.m_TopBorderPanel.TabIndex = 2;
            // 
            // m_ButtonPanel
            // 
            this.m_ButtonPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_ButtonPanel.BackColor = System.Drawing.SystemColors.Control;
            this.m_ButtonPanel.Controls.Add(this.m_BottomBorderPanel);
            this.m_ButtonPanel.Controls.Add(this.m_PreviousButton);
            this.m_ButtonPanel.Controls.Add(this.m_NextButton);
            this.m_ButtonPanel.Controls.Add(this.m_CancelButton);
            this.m_ButtonPanel.Location = new System.Drawing.Point(0, 396);
            this.m_ButtonPanel.Name = "m_ButtonPanel";
            this.m_ButtonPanel.Size = new System.Drawing.Size(480, 40);
            this.m_ButtonPanel.TabIndex = 3;
            // 
            // m_BottomBorderPanel
            // 
            this.m_BottomBorderPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_BottomBorderPanel.BackColor = System.Drawing.Color.Black;
            this.m_BottomBorderPanel.Location = new System.Drawing.Point(0, 0);
            this.m_BottomBorderPanel.Name = "m_BottomBorderPanel";
            this.m_BottomBorderPanel.Size = new System.Drawing.Size(480, 1);
            this.m_BottomBorderPanel.TabIndex = 3;
            // 
            // m_PreviousButton
            // 
            this.m_PreviousButton.Location = new System.Drawing.Point(224, 8);
            this.m_PreviousButton.Name = "m_PreviousButton";
            this.m_PreviousButton.Size = new System.Drawing.Size(76, 24);
            this.m_PreviousButton.TabIndex = 2;
            this.m_PreviousButton.Text = "Previous";
            this.m_PreviousButton.UseVisualStyleBackColor = true;
            // 
            // m_NextButton
            // 
            this.m_NextButton.Location = new System.Drawing.Point(308, 8);
            this.m_NextButton.Name = "m_NextButton";
            this.m_NextButton.Size = new System.Drawing.Size(76, 24);
            this.m_NextButton.TabIndex = 1;
            this.m_NextButton.Text = "Next";
            this.m_NextButton.UseVisualStyleBackColor = true;
            // 
            // m_CancelButton
            // 
            this.m_CancelButton.Location = new System.Drawing.Point(392, 8);
            this.m_CancelButton.Name = "m_CancelButton";
            this.m_CancelButton.Size = new System.Drawing.Size(76, 24);
            this.m_CancelButton.TabIndex = 0;
            this.m_CancelButton.Text = "Cancel";
            this.m_CancelButton.UseVisualStyleBackColor = true;
            // 
            // m_PageName
            // 
            this.m_PageName.BackColor = System.Drawing.Color.Transparent;
            this.m_PageName.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_PageName.Location = new System.Drawing.Point(40, 0);
            this.m_PageName.Name = "m_PageName";
            this.m_PageName.Size = new System.Drawing.Size(439, 52);
            this.m_PageName.TabIndex = 4;
            this.m_PageName.Text = "label1";
            this.m_PageName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // WizardViewWidget
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.m_TopBorderPanel);
            this.Controls.Add(this.m_PageName);
            this.Controls.Add(this.m_ButtonPanel);
            this.Controls.Add(this.m_PageList);
            this.Controls.Add(this.m_Page);
            this.Name = "WizardViewWidget";
            this.Size = new System.Drawing.Size(480, 436);
            this.m_ButtonPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel m_Page;
        private System.Windows.Forms.Panel m_TopBorderPanel;
        private System.Windows.Forms.Panel m_ButtonPanel;
        private System.Windows.Forms.Label m_PageName;
        private System.Windows.Forms.Button m_PreviousButton;
        private System.Windows.Forms.Button m_NextButton;
        private System.Windows.Forms.Button m_CancelButton;
        private System.Windows.Forms.Panel m_BottomBorderPanel;
        private System.Windows.Forms.FlowLayoutPanel m_PageList;
    }
}
