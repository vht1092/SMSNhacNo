namespace SMSNhacNo
{
    partial class formMain
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.sMSNhacNoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sMSNhacNoSaoKeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sMSNhacNoDueDateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sendSMSNhacNoSaoKeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sendSMSNhacNoDueDateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sMSImportDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importThongBaoSaoKeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importNhacNoDueDateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sMSImportDataToolStripMenuItem,
            this.sMSNhacNoToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(723, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // sMSNhacNoToolStripMenuItem
            // 
            this.sMSNhacNoToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sMSNhacNoSaoKeToolStripMenuItem,
            this.sMSNhacNoDueDateToolStripMenuItem,
            this.sendSMSNhacNoSaoKeToolStripMenuItem,
            this.sendSMSNhacNoDueDateToolStripMenuItem});
            this.sMSNhacNoToolStripMenuItem.Name = "sMSNhacNoToolStripMenuItem";
            this.sMSNhacNoToolStripMenuItem.Size = new System.Drawing.Size(92, 20);
            this.sMSNhacNoToolStripMenuItem.Text = "SMS Nhac No";
            // 
            // sMSNhacNoSaoKeToolStripMenuItem
            // 
            this.sMSNhacNoSaoKeToolStripMenuItem.Enabled = false;
            this.sMSNhacNoSaoKeToolStripMenuItem.Name = "sMSNhacNoSaoKeToolStripMenuItem";
            this.sMSNhacNoSaoKeToolStripMenuItem.Size = new System.Drawing.Size(227, 22);
            this.sMSNhacNoSaoKeToolStripMenuItem.Text = "SMS Nhac No Sao Ke";
            this.sMSNhacNoSaoKeToolStripMenuItem.Visible = false;
            this.sMSNhacNoSaoKeToolStripMenuItem.Click += new System.EventHandler(this.sMSNhacNoSaoKeToolStripMenuItem_Click);
            // 
            // sMSNhacNoDueDateToolStripMenuItem
            // 
            this.sMSNhacNoDueDateToolStripMenuItem.Enabled = false;
            this.sMSNhacNoDueDateToolStripMenuItem.Name = "sMSNhacNoDueDateToolStripMenuItem";
            this.sMSNhacNoDueDateToolStripMenuItem.Size = new System.Drawing.Size(227, 22);
            this.sMSNhacNoDueDateToolStripMenuItem.Text = "SMS Nhac No Due Date";
            this.sMSNhacNoDueDateToolStripMenuItem.Visible = false;
            this.sMSNhacNoDueDateToolStripMenuItem.Click += new System.EventHandler(this.sMSNhacNoDueDateToolStripMenuItem_Click);
            // 
            // sendSMSNhacNoSaoKeToolStripMenuItem
            // 
            this.sendSMSNhacNoSaoKeToolStripMenuItem.Name = "sendSMSNhacNoSaoKeToolStripMenuItem";
            this.sendSMSNhacNoSaoKeToolStripMenuItem.Size = new System.Drawing.Size(227, 22);
            this.sendSMSNhacNoSaoKeToolStripMenuItem.Text = "Send SMS Thong Bao Sao Ke";
            this.sendSMSNhacNoSaoKeToolStripMenuItem.Click += new System.EventHandler(this.sendSMSNhacNoSaoKeToolStripMenuItem_Click);
            // 
            // sendSMSNhacNoDueDateToolStripMenuItem
            // 
            this.sendSMSNhacNoDueDateToolStripMenuItem.Name = "sendSMSNhacNoDueDateToolStripMenuItem";
            this.sendSMSNhacNoDueDateToolStripMenuItem.Size = new System.Drawing.Size(227, 22);
            this.sendSMSNhacNoDueDateToolStripMenuItem.Text = "Send SMS Nhac No Due Date";
            this.sendSMSNhacNoDueDateToolStripMenuItem.Click += new System.EventHandler(this.SendSMSNhacNoDueDateToolStripMenuItem_Click);
            // 
            // sMSImportDataToolStripMenuItem
            // 
            this.sMSImportDataToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.importThongBaoSaoKeToolStripMenuItem,
            this.importNhacNoDueDateToolStripMenuItem});
            this.sMSImportDataToolStripMenuItem.Name = "sMSImportDataToolStripMenuItem";
            this.sMSImportDataToolStripMenuItem.Size = new System.Drawing.Size(108, 20);
            this.sMSImportDataToolStripMenuItem.Text = "SMS Import Data";
            // 
            // importThongBaoSaoKeToolStripMenuItem
            // 
            this.importThongBaoSaoKeToolStripMenuItem.Name = "importThongBaoSaoKeToolStripMenuItem";
            this.importThongBaoSaoKeToolStripMenuItem.Size = new System.Drawing.Size(211, 22);
            this.importThongBaoSaoKeToolStripMenuItem.Text = "Import Thông báo Sao Kê";
            this.importThongBaoSaoKeToolStripMenuItem.Click += new System.EventHandler(this.ImportThongBaoSaoKeToolStripMenuItem_Click);
            // 
            // importNhacNoDueDateToolStripMenuItem
            // 
            this.importNhacNoDueDateToolStripMenuItem.Name = "importNhacNoDueDateToolStripMenuItem";
            this.importNhacNoDueDateToolStripMenuItem.Size = new System.Drawing.Size(211, 22);
            this.importNhacNoDueDateToolStripMenuItem.Text = "Import Nhắc Nợ Due Date";
            this.importNhacNoDueDateToolStripMenuItem.Click += new System.EventHandler(this.ImportNhacNoDueDateToolStripMenuItem_Click);
            // 
            // formMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(723, 261);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "formMain";
            this.Text = "SMS NHAC NO";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem sMSNhacNoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sMSNhacNoSaoKeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sMSNhacNoDueDateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sendSMSNhacNoSaoKeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sendSMSNhacNoDueDateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sMSImportDataToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importThongBaoSaoKeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importNhacNoDueDateToolStripMenuItem;
    }
}

