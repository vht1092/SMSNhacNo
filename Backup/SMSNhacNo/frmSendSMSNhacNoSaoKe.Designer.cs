namespace SMSNhacNo
{
    partial class frmSendSMSNhacNoSaoKe
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
            this.lbVSMC = new System.Windows.Forms.Label();
            this.tboxStatementMonth = new System.Windows.Forms.TextBox();
            this.tboxCardBrn = new System.Windows.Forms.TextBox();
            this.lbStatementMonth = new System.Windows.Forms.Label();
            this.lbCardType = new System.Windows.Forms.Label();
            this.btn = new System.Windows.Forms.Button();
            this.lbSMSType = new System.Windows.Forms.Label();
            this.tboxSMSType = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(296, 65);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(147, 13);
            this.label1.TabIndex = 13;
            this.label1.Text = "(YYYYMM của tháng hiện tại)";
            // 
            // lbVSMC
            // 
            this.lbVSMC.AutoSize = true;
            this.lbVSMC.Location = new System.Drawing.Point(293, 34);
            this.lbVSMC.Name = "lbVSMC";
            this.lbVSMC.Size = new System.Drawing.Size(49, 13);
            this.lbVSMC.TabIndex = 12;
            this.lbVSMC.Text = "(VS, MC)";
            // 
            // tboxStatementMonth
            // 
            this.tboxStatementMonth.Location = new System.Drawing.Point(115, 62);
            this.tboxStatementMonth.Name = "tboxStatementMonth";
            this.tboxStatementMonth.Size = new System.Drawing.Size(172, 20);
            this.tboxStatementMonth.TabIndex = 11;
            // 
            // tboxCardBrn
            // 
            this.tboxCardBrn.Location = new System.Drawing.Point(115, 31);
            this.tboxCardBrn.Name = "tboxCardBrn";
            this.tboxCardBrn.Size = new System.Drawing.Size(172, 20);
            this.tboxCardBrn.TabIndex = 10;
            // 
            // lbStatementMonth
            // 
            this.lbStatementMonth.AutoSize = true;
            this.lbStatementMonth.Location = new System.Drawing.Point(7, 65);
            this.lbStatementMonth.Name = "lbStatementMonth";
            this.lbStatementMonth.Size = new System.Drawing.Size(88, 13);
            this.lbStatementMonth.TabIndex = 9;
            this.lbStatementMonth.Text = "Statement Month";
            // 
            // lbCardType
            // 
            this.lbCardType.AutoSize = true;
            this.lbCardType.Location = new System.Drawing.Point(7, 31);
            this.lbCardType.Name = "lbCardType";
            this.lbCardType.Size = new System.Drawing.Size(48, 13);
            this.lbCardType.TabIndex = 8;
            this.lbCardType.Text = "Card Brn";
            // 
            // btn
            // 
            this.btn.Location = new System.Drawing.Point(150, 140);
            this.btn.Name = "btn";
            this.btn.Size = new System.Drawing.Size(75, 23);
            this.btn.TabIndex = 7;
            this.btn.Text = "Send SMS";
            this.btn.UseVisualStyleBackColor = true;
            this.btn.Click += new System.EventHandler(this.btn_Click);
            // 
            // lbSMSType
            // 
            this.lbSMSType.AutoSize = true;
            this.lbSMSType.Location = new System.Drawing.Point(7, 103);
            this.lbSMSType.Name = "lbSMSType";
            this.lbSMSType.Size = new System.Drawing.Size(57, 13);
            this.lbSMSType.TabIndex = 14;
            this.lbSMSType.Text = "SMS Type";
            // 
            // tboxSMSType
            // 
            this.tboxSMSType.Location = new System.Drawing.Point(115, 100);
            this.tboxSMSType.Name = "tboxSMSType";
            this.tboxSMSType.ReadOnly = true;
            this.tboxSMSType.Size = new System.Drawing.Size(172, 20);
            this.tboxSMSType.TabIndex = 15;
            this.tboxSMSType.Text = "DEBT01";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Crimson;
            this.label2.Location = new System.Drawing.Point(80, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(279, 13);
            this.label2.TabIndex = 16;
            this.label2.Text = "Lấy thông tin từ bảng SMS NHAC NO gửi qua ben DB EB";
            // 
            // frmSendSMSNhacNoSaoKe
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(448, 169);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tboxSMSType);
            this.Controls.Add(this.lbSMSType);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lbVSMC);
            this.Controls.Add(this.tboxStatementMonth);
            this.Controls.Add(this.tboxCardBrn);
            this.Controls.Add(this.lbStatementMonth);
            this.Controls.Add(this.lbCardType);
            this.Controls.Add(this.btn);
            this.Name = "frmSendSMSNhacNoSaoKe";
            this.Text = "frmSendSMSNhacNoSaoKe";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbVSMC;
        private System.Windows.Forms.TextBox tboxStatementMonth;
        private System.Windows.Forms.TextBox tboxCardBrn;
        private System.Windows.Forms.Label lbStatementMonth;
        private System.Windows.Forms.Label lbCardType;
        private System.Windows.Forms.Button btn;
        private System.Windows.Forms.Label lbSMSType;
        private System.Windows.Forms.TextBox tboxSMSType;
        private System.Windows.Forms.Label label2;
    }
}