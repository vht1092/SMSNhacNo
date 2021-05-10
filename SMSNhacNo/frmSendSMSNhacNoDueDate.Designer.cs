namespace SMSNhacNo
{
    partial class frmSendSMSNhacNoDueDate
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
            this.tboxSMSType = new System.Windows.Forms.TextBox();
            this.lbSMSType = new System.Windows.Forms.Label();
            this.lblLoading = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lbStatementMonth
            // 
            this.lbStatementMonth.AutoSize = true;
            this.lbStatementMonth.Location = new System.Drawing.Point(12, 23);
            this.lbStatementMonth.Name = "lbStatementMonth";
            this.lbStatementMonth.Size = new System.Drawing.Size(53, 13);
            this.lbStatementMonth.TabIndex = 3;
            this.lbStatementMonth.Text = "Due Date";
            // 
            // txbDueDate
            // 
            this.txbDueDate.Location = new System.Drawing.Point(105, 20);
            this.txbDueDate.Name = "txbDueDate";
            this.txbDueDate.Size = new System.Drawing.Size(182, 20);
            this.txbDueDate.TabIndex = 4;
            // 
            // lbDueDateormatNote
            // 
            this.lbDueDateormatNote.AutoSize = true;
            this.lbDueDateormatNote.Location = new System.Drawing.Point(297, 23);
            this.lbDueDateormatNote.Name = "lbDueDateormatNote";
            this.lbDueDateormatNote.Size = new System.Drawing.Size(75, 13);
            this.lbDueDateormatNote.TabIndex = 5;
            this.lbDueDateormatNote.Text = "(YYYYMMDD)";
            // 
            // btnSendSMS
            // 
            this.btnSendSMS.Location = new System.Drawing.Point(163, 128);
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
            this.lblStatementMonth.Location = new System.Drawing.Point(12, 56);
            this.lblStatementMonth.Name = "lblStatementMonth";
            this.lblStatementMonth.Size = new System.Drawing.Size(91, 13);
            this.lblStatementMonth.TabIndex = 7;
            this.lblStatementMonth.Text = "Statement Month ";
            // 
            // lbStatementMonthNote
            // 
            this.lbStatementMonthNote.AutoSize = true;
            this.lbStatementMonthNote.Location = new System.Drawing.Point(297, 56);
            this.lbStatementMonthNote.Name = "lbStatementMonthNote";
            this.lbStatementMonthNote.Size = new System.Drawing.Size(140, 13);
            this.lbStatementMonthNote.TabIndex = 8;
            this.lbStatementMonthNote.Text = "(YYYYMM) = Now - 1 month";
            // 
            // txbStatementMonth
            // 
            this.txbStatementMonth.Location = new System.Drawing.Point(105, 53);
            this.txbStatementMonth.Name = "txbStatementMonth";
            this.txbStatementMonth.Size = new System.Drawing.Size(182, 20);
            this.txbStatementMonth.TabIndex = 9;
            // 
            // tboxSMSType
            // 
            this.tboxSMSType.Location = new System.Drawing.Point(105, 85);
            this.tboxSMSType.Name = "tboxSMSType";
            this.tboxSMSType.ReadOnly = true;
            this.tboxSMSType.Size = new System.Drawing.Size(182, 20);
            this.tboxSMSType.TabIndex = 17;
            this.tboxSMSType.Text = "DEBT02";
            // 
            // lbSMSType
            // 
            this.lbSMSType.AutoSize = true;
            this.lbSMSType.Location = new System.Drawing.Point(12, 88);
            this.lbSMSType.Name = "lbSMSType";
            this.lbSMSType.Size = new System.Drawing.Size(57, 13);
            this.lbSMSType.TabIndex = 16;
            this.lbSMSType.Text = "SMS Type";
            // 
            // lblLoading
            // 
            this.lblLoading.AutoSize = true;
            this.lblLoading.Location = new System.Drawing.Point(244, 133);
            this.lblLoading.Name = "lblLoading";
            this.lblLoading.Size = new System.Drawing.Size(54, 13);
            this.lblLoading.TabIndex = 18;
            this.lblLoading.Text = "Loading...";
            this.lblLoading.Visible = false;
            // 
            // frmSendSMSNhacNoDueDate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(447, 181);
            this.Controls.Add(this.lblLoading);
            this.Controls.Add(this.tboxSMSType);
            this.Controls.Add(this.lbSMSType);
            this.Controls.Add(this.txbStatementMonth);
            this.Controls.Add(this.lbStatementMonthNote);
            this.Controls.Add(this.lblStatementMonth);
            this.Controls.Add(this.btnSendSMS);
            this.Controls.Add(this.lbDueDateormatNote);
            this.Controls.Add(this.txbDueDate);
            this.Controls.Add(this.lbStatementMonth);
            this.Name = "frmSendSMSNhacNoDueDate";
            this.Text = "Send SMS Nhac No Due Date";
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
        private System.Windows.Forms.TextBox tboxSMSType;
        private System.Windows.Forms.Label lbSMSType;
        private System.Windows.Forms.Label lblLoading;
    }
}