﻿namespace SMSNhacNo
{
    partial class frmSMSNhacNoSaoKe
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
            this.btn = new System.Windows.Forms.Button();
            this.lbCardType = new System.Windows.Forms.Label();
            this.lbStatementMonth = new System.Windows.Forms.Label();
            this.tboxCardBrn = new System.Windows.Forms.TextBox();
            this.tboxStatementMonth = new System.Windows.Forms.TextBox();
            this.lbVSMC = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btn
            // 
            this.btn.Location = new System.Drawing.Point(154, 79);
            this.btn.Name = "btn";
            this.btn.Size = new System.Drawing.Size(75, 23);
            this.btn.TabIndex = 0;
            this.btn.Text = "Send SMS";
            this.btn.UseVisualStyleBackColor = true;
            this.btn.Click += new System.EventHandler(this.btn_Click);
            // 
            // lbCardType
            // 
            this.lbCardType.AutoSize = true;
            this.lbCardType.Location = new System.Drawing.Point(3, 9);
            this.lbCardType.Name = "lbCardType";
            this.lbCardType.Size = new System.Drawing.Size(48, 13);
            this.lbCardType.TabIndex = 1;
            this.lbCardType.Text = "Card Brn";
            // 
            // lbStatementMonth
            // 
            this.lbStatementMonth.AutoSize = true;
            this.lbStatementMonth.Location = new System.Drawing.Point(3, 43);
            this.lbStatementMonth.Name = "lbStatementMonth";
            this.lbStatementMonth.Size = new System.Drawing.Size(88, 13);
            this.lbStatementMonth.TabIndex = 2;
            this.lbStatementMonth.Text = "Statement Month";
            // 
            // tboxCardBrn
            // 
            this.tboxCardBrn.Location = new System.Drawing.Point(111, 9);
            this.tboxCardBrn.Name = "tboxCardBrn";
            this.tboxCardBrn.Size = new System.Drawing.Size(172, 20);
            this.tboxCardBrn.TabIndex = 3;
            // 
            // tboxStatementMonth
            // 
            this.tboxStatementMonth.Location = new System.Drawing.Point(111, 40);
            this.tboxStatementMonth.Name = "tboxStatementMonth";
            this.tboxStatementMonth.Size = new System.Drawing.Size(172, 20);
            this.tboxStatementMonth.TabIndex = 4;
            // 
            // lbVSMC
            // 
            this.lbVSMC.AutoSize = true;
            this.lbVSMC.Location = new System.Drawing.Point(289, 12);
            this.lbVSMC.Name = "lbVSMC";
            this.lbVSMC.Size = new System.Drawing.Size(49, 13);
            this.lbVSMC.TabIndex = 5;
            this.lbVSMC.Text = "(VS, MC)";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(292, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "(YYYYMM)";
            // 
            // frmSMSNhacNoSaoKe
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(366, 110);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lbVSMC);
            this.Controls.Add(this.tboxStatementMonth);
            this.Controls.Add(this.tboxCardBrn);
            this.Controls.Add(this.lbStatementMonth);
            this.Controls.Add(this.lbCardType);
            this.Controls.Add(this.btn);
            this.Name = "frmSMSNhacNoSaoKe";
            this.Text = "frmSMSNhacNoSaoKe";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn;
        private System.Windows.Forms.Label lbCardType;
        private System.Windows.Forms.Label lbStatementMonth;
        private System.Windows.Forms.TextBox tboxCardBrn;
        private System.Windows.Forms.TextBox tboxStatementMonth;
        private System.Windows.Forms.Label lbVSMC;
        private System.Windows.Forms.Label label1;
    }
}