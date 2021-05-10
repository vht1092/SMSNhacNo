namespace SMSNhacNo
{
    partial class frmMain
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
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
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
            this.sMSNhacNoDueDateToolStripMenuItem});
            this.sMSNhacNoToolStripMenuItem.Name = "sMSNhacNoToolStripMenuItem";
            this.sMSNhacNoToolStripMenuItem.Size = new System.Drawing.Size(92, 20);
            this.sMSNhacNoToolStripMenuItem.Text = "SMS Nhac No";
            // 
            // sMSNhacNoSaoKeToolStripMenuItem
            // 
            this.sMSNhacNoSaoKeToolStripMenuItem.Name = "sMSNhacNoSaoKeToolStripMenuItem";
            this.sMSNhacNoSaoKeToolStripMenuItem.Size = new System.Drawing.Size(198, 22);
            this.sMSNhacNoSaoKeToolStripMenuItem.Text = "SMS Thong Bao Sao Ke";
            this.sMSNhacNoSaoKeToolStripMenuItem.Click += new System.EventHandler(this.sMSThongBaoSaoKeToolStripMenuItem_Click);
            // 
            // sMSNhacNoDueDateToolStripMenuItem
            // 
            this.sMSNhacNoDueDateToolStripMenuItem.Name = "sMSNhacNoDueDateToolStripMenuItem";
            this.sMSNhacNoDueDateToolStripMenuItem.Size = new System.Drawing.Size(198, 22);
            this.sMSNhacNoDueDateToolStripMenuItem.Text = "SMS Nhac No Due Date";
            this.sMSNhacNoDueDateToolStripMenuItem.Click += new System.EventHandler(this.sMSNhacNoDueDateToolStripMenuItem_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(723, 261);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "frmMain";
            this.Text = "frmMain";
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
    }
}

