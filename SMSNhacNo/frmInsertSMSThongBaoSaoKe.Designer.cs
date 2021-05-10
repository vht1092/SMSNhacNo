namespace SMSNhacNo
{
    partial class frmInsertSMSThongBaoSaoKe
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
            this.btnInsetSMSSaoKe = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.btnExportToExcel = new System.Windows.Forms.Button();
            this.lblLoading = new System.Windows.Forms.Label();
            this.chkMCW = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(302, 100);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 13);
            this.label1.TabIndex = 12;
            this.label1.Text = "(YYYYMM)";
            // 
            // lbVSMC
            // 
            this.lbVSMC.AutoSize = true;
            this.lbVSMC.Location = new System.Drawing.Point(299, 69);
            this.lbVSMC.Name = "lbVSMC";
            this.lbVSMC.Size = new System.Drawing.Size(49, 13);
            this.lbVSMC.TabIndex = 11;
            this.lbVSMC.Text = "(VS, MC)";
            // 
            // tboxStatementMonth
            // 
            this.tboxStatementMonth.Location = new System.Drawing.Point(121, 97);
            this.tboxStatementMonth.Name = "tboxStatementMonth";
            this.tboxStatementMonth.Size = new System.Drawing.Size(172, 20);
            this.tboxStatementMonth.TabIndex = 10;
            // 
            // tboxCardBrn
            // 
            this.tboxCardBrn.Location = new System.Drawing.Point(121, 66);
            this.tboxCardBrn.Name = "tboxCardBrn";
            this.tboxCardBrn.Size = new System.Drawing.Size(172, 20);
            this.tboxCardBrn.TabIndex = 9;
            // 
            // lbStatementMonth
            // 
            this.lbStatementMonth.AutoSize = true;
            this.lbStatementMonth.Location = new System.Drawing.Point(13, 100);
            this.lbStatementMonth.Name = "lbStatementMonth";
            this.lbStatementMonth.Size = new System.Drawing.Size(88, 13);
            this.lbStatementMonth.TabIndex = 8;
            this.lbStatementMonth.Text = "Statement Month";
            // 
            // lbCardType
            // 
            this.lbCardType.AutoSize = true;
            this.lbCardType.Location = new System.Drawing.Point(13, 66);
            this.lbCardType.Name = "lbCardType";
            this.lbCardType.Size = new System.Drawing.Size(48, 13);
            this.lbCardType.TabIndex = 7;
            this.lbCardType.Text = "Card Brn";
            // 
            // btnInsetSMSSaoKe
            // 
            this.btnInsetSMSSaoKe.Location = new System.Drawing.Point(104, 140);
            this.btnInsetSMSSaoKe.Name = "btnInsetSMSSaoKe";
            this.btnInsetSMSSaoKe.Size = new System.Drawing.Size(104, 23);
            this.btnInsetSMSSaoKe.TabIndex = 13;
            this.btnInsetSMSSaoKe.Text = "Process Insert";
            this.btnInsetSMSSaoKe.UseVisualStyleBackColor = true;
            this.btnInsetSMSSaoKe.Click += new System.EventHandler(this.btnInsetSMSSaoKe_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Crimson;
            this.label2.Location = new System.Drawing.Point(12, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(464, 13);
            this.label2.TabIndex = 14;
            this.label2.Text = "(Call Procedure GET_SMS_NHAC_NO_SAO_KE để Insert dữ liệu xuống bảng SMS_NHAC_NO)";
            // 
            // btnExportToExcel
            // 
            this.btnExportToExcel.Location = new System.Drawing.Point(228, 140);
            this.btnExportToExcel.Name = "btnExportToExcel";
            this.btnExportToExcel.Size = new System.Drawing.Size(104, 23);
            this.btnExportToExcel.TabIndex = 15;
            this.btnExportToExcel.Text = "Export to Excel";
            this.btnExportToExcel.UseVisualStyleBackColor = true;
            this.btnExportToExcel.Click += new System.EventHandler(this.BtnExportToExcel_Click);
            // 
            // lblLoading
            // 
            this.lblLoading.AutoSize = true;
            this.lblLoading.Location = new System.Drawing.Point(338, 145);
            this.lblLoading.Name = "lblLoading";
            this.lblLoading.Size = new System.Drawing.Size(54, 13);
            this.lblLoading.TabIndex = 12;
            this.lblLoading.Text = "Loading...";
            this.lblLoading.Visible = false;
            // 
            // chkMCW
            // 
            this.chkMCW.AutoSize = true;
            this.chkMCW.Location = new System.Drawing.Point(121, 43);
            this.chkMCW.Name = "chkMCW";
            this.chkMCW.Size = new System.Drawing.Size(73, 17);
            this.chkMCW.TabIndex = 21;
            this.chkMCW.Text = "MC World";
            this.chkMCW.UseVisualStyleBackColor = true;
            // 
            // frmInsertSMSThongBaoSaoKe
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(473, 173);
            this.Controls.Add(this.chkMCW);
            this.Controls.Add(this.btnExportToExcel);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnInsetSMSSaoKe);
            this.Controls.Add(this.lblLoading);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lbVSMC);
            this.Controls.Add(this.tboxStatementMonth);
            this.Controls.Add(this.tboxCardBrn);
            this.Controls.Add(this.lbStatementMonth);
            this.Controls.Add(this.lbCardType);
            this.Name = "frmInsertSMSThongBaoSaoKe";
            this.Text = "Insert SMS Thong Bao Sao Ke";
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
        private System.Windows.Forms.Button btnInsetSMSSaoKe;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnExportToExcel;
        private System.Windows.Forms.Label lblLoading;
        private System.Windows.Forms.CheckBox chkMCW;
    }
}