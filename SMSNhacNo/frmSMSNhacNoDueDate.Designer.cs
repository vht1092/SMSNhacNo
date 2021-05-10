namespace SMSNhacNo
{
    partial class frmSMSNhacNoDueDate
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
            this.lbStatementMonth = new System.Windows.Forms.Label();
            this.txbDueDate = new System.Windows.Forms.TextBox();
            this.lbDueDateormatNote = new System.Windows.Forms.Label();
            this.btnSendSMS = new System.Windows.Forms.Button();
            this.lblStatementMonth = new System.Windows.Forms.Label();
            this.lbStatementMonthNote = new System.Windows.Forms.Label();
            this.txbStatementMonth = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // lbStatementMonth
            // 
            this.lbStatementMonth.AutoSize = true;
            this.lbStatementMonth.Location = new System.Drawing.Point(12, 13);
            this.lbStatementMonth.Name = "lbStatementMonth";
            this.lbStatementMonth.Size = new System.Drawing.Size(53, 13);
            this.lbStatementMonth.TabIndex = 3;
            this.lbStatementMonth.Text = "Due Date";
            // 
            // txbDueDate
            // 
            this.txbDueDate.Location = new System.Drawing.Point(105, 10);
            this.txbDueDate.Name = "txbDueDate";
            this.txbDueDate.Size = new System.Drawing.Size(182, 20);
            this.txbDueDate.TabIndex = 4;
            // 
            // lbDueDateormatNote
            // 
            this.lbDueDateormatNote.AutoSize = true;
            this.lbDueDateormatNote.Location = new System.Drawing.Point(297, 13);
            this.lbDueDateormatNote.Name = "lbDueDateormatNote";
            this.lbDueDateormatNote.Size = new System.Drawing.Size(75, 13);
            this.lbDueDateormatNote.TabIndex = 5;
            this.lbDueDateormatNote.Text = "(YYYYMMDD)";
            // 
            // btnSendSMS
            // 
            this.btnSendSMS.Location = new System.Drawing.Point(163, 87);
            this.btnSendSMS.Name = "btnSendSMS";
            this.btnSendSMS.Size = new System.Drawing.Size(75, 23);
            this.btnSendSMS.TabIndex = 6;
            this.btnSendSMS.Text = "Send SMS";
            this.btnSendSMS.UseVisualStyleBackColor = true;
            this.btnSendSMS.Click += new System.EventHandler(this.btnSendSMS_Click);
            // 
            // lblStatementMonth
            // 
            this.lblStatementMonth.AutoSize = true;
            this.lblStatementMonth.Location = new System.Drawing.Point(12, 46);
            this.lblStatementMonth.Name = "lblStatementMonth";
            this.lblStatementMonth.Size = new System.Drawing.Size(91, 13);
            this.lblStatementMonth.TabIndex = 7;
            this.lblStatementMonth.Text = "Statement Month ";
            // 
            // lbStatementMonthNote
            // 
            this.lbStatementMonthNote.AutoSize = true;
            this.lbStatementMonthNote.Location = new System.Drawing.Point(297, 46);
            this.lbStatementMonthNote.Name = "lbStatementMonthNote";
            this.lbStatementMonthNote.Size = new System.Drawing.Size(140, 13);
            this.lbStatementMonthNote.TabIndex = 8;
            this.lbStatementMonthNote.Text = "(YYYYMM) = Now - 1 month";
            // 
            // txbStatementMonth
            // 
            this.txbStatementMonth.Location = new System.Drawing.Point(105, 43);
            this.txbStatementMonth.Name = "txbStatementMonth";
            this.txbStatementMonth.Size = new System.Drawing.Size(182, 20);
            this.txbStatementMonth.TabIndex = 9;
            // 
            // frmSMSNhacNoDueDate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(447, 136);
            this.Controls.Add(this.txbStatementMonth);
            this.Controls.Add(this.lbStatementMonthNote);
            this.Controls.Add(this.lblStatementMonth);
            this.Controls.Add(this.btnSendSMS);
            this.Controls.Add(this.lbDueDateormatNote);
            this.Controls.Add(this.txbDueDate);
            this.Controls.Add(this.lbStatementMonth);
            this.Name = "frmSMSNhacNoDueDate";
            this.Text = "frmSMSNhacNoDueDate";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbStatementMonth;
        private System.Windows.Forms.TextBox txbDueDate;
        private System.Windows.Forms.Label lbDueDateormatNote;
        private System.Windows.Forms.Button btnSendSMS;
        private System.Windows.Forms.Label lblStatementMonth;
        private System.Windows.Forms.Label lbStatementMonthNote;
        private System.Windows.Forms.TextBox txbStatementMonth;
    }
}