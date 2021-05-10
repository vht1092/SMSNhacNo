namespace SMSNhacNo
{
    partial class frmInsertSMSDueDate
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
            this.txbStatementMonth = new System.Windows.Forms.TextBox();
            this.lbStatementMonthNote = new System.Windows.Forms.Label();
            this.lblStatementMonth = new System.Windows.Forms.Label();
            this.btnSendSMS = new System.Windows.Forms.Button();
            this.lbDueDateormatNote = new System.Windows.Forms.Label();
            this.txbDueDate = new System.Windows.Forms.TextBox();
            this.lbStatementMonth = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txbStatementMonth
            // 
            this.txbStatementMonth.Location = new System.Drawing.Point(101, 66);
            this.txbStatementMonth.Name = "txbStatementMonth";
            this.txbStatementMonth.ReadOnly = true;
            this.txbStatementMonth.Size = new System.Drawing.Size(182, 20);
            this.txbStatementMonth.TabIndex = 16;
            // 
            // lbStatementMonthNote
            // 
            this.lbStatementMonthNote.AutoSize = true;
            this.lbStatementMonthNote.Location = new System.Drawing.Point(293, 69);
            this.lbStatementMonthNote.Name = "lbStatementMonthNote";
            this.lbStatementMonthNote.Size = new System.Drawing.Size(140, 13);
            this.lbStatementMonthNote.TabIndex = 15;
            this.lbStatementMonthNote.Text = "(YYYYMM) = Now - 1 month";
            // 
            // lblStatementMonth
            // 
            this.lblStatementMonth.AutoSize = true;
            this.lblStatementMonth.Location = new System.Drawing.Point(8, 69);
            this.lblStatementMonth.Name = "lblStatementMonth";
            this.lblStatementMonth.Size = new System.Drawing.Size(91, 13);
            this.lblStatementMonth.TabIndex = 14;
            this.lblStatementMonth.Text = "Statement Month ";
            // 
            // btnSendSMS
            // 
            this.btnSendSMS.Location = new System.Drawing.Point(159, 110);
            this.btnSendSMS.Name = "btnSendSMS";
            this.btnSendSMS.Size = new System.Drawing.Size(75, 23);
            this.btnSendSMS.TabIndex = 13;
            this.btnSendSMS.Text = "Send SMS";
            this.btnSendSMS.UseVisualStyleBackColor = true;
            this.btnSendSMS.Click += new System.EventHandler(this.btnSendSMS_Click);
            // 
            // lbDueDateormatNote
            // 
            this.lbDueDateormatNote.AutoSize = true;
            this.lbDueDateormatNote.Location = new System.Drawing.Point(293, 36);
            this.lbDueDateormatNote.Name = "lbDueDateormatNote";
            this.lbDueDateormatNote.Size = new System.Drawing.Size(75, 13);
            this.lbDueDateormatNote.TabIndex = 12;
            this.lbDueDateormatNote.Text = "(YYYYMMDD)";
            // 
            // txbDueDate
            // 
            this.txbDueDate.Location = new System.Drawing.Point(101, 33);
            this.txbDueDate.Name = "txbDueDate";
            this.txbDueDate.Size = new System.Drawing.Size(182, 20);
            this.txbDueDate.TabIndex = 11;
            // 
            // lbStatementMonth
            // 
            this.lbStatementMonth.AutoSize = true;
            this.lbStatementMonth.Location = new System.Drawing.Point(8, 36);
            this.lbStatementMonth.Name = "lbStatementMonth";
            this.lbStatementMonth.Size = new System.Drawing.Size(53, 13);
            this.lbStatementMonth.TabIndex = 10;
            this.lbStatementMonth.Text = "Due Date";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Crimson;
            this.label2.Location = new System.Drawing.Point(8, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(480, 13);
            this.label2.TabIndex = 17;
            this.label2.Text = "(Call Procedure GET_SMS_NHAC_NO_DUE_DATE để Insert dữ liệu xuống bảng SMS_NHAC_NO" +
                ")";
            // 
            // frmInsertSMSDueDate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(487, 149);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txbStatementMonth);
            this.Controls.Add(this.lbStatementMonthNote);
            this.Controls.Add(this.lblStatementMonth);
            this.Controls.Add(this.btnSendSMS);
            this.Controls.Add(this.lbDueDateormatNote);
            this.Controls.Add(this.txbDueDate);
            this.Controls.Add(this.lbStatementMonth);
            this.Name = "frmInsertSMSDueDate";
            this.Text = "frmInsertSMSDueDate";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txbStatementMonth;
        private System.Windows.Forms.Label lbStatementMonthNote;
        private System.Windows.Forms.Label lblStatementMonth;
        private System.Windows.Forms.Button btnSendSMS;
        private System.Windows.Forms.Label lbDueDateormatNote;
        private System.Windows.Forms.TextBox txbDueDate;
        private System.Windows.Forms.Label lbStatementMonth;
        private System.Windows.Forms.Label label2;
    }
}