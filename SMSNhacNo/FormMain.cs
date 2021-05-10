using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
//using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SMSNhacNo
{
    public partial class formMain : Form
    {
        public formMain()
        {
            InitializeComponent();
        }

        private void sMSNhacNoSaoKeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmSMSNhacNoSaoKe f1 = new frmSMSNhacNoSaoKe();
            f1.ShowDialog();
        }

        private void insertNhacNoSaoKeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void sendSMSNhacNoSaoKeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmSendSMSThongBaoSaoKe f1 = new frmSendSMSThongBaoSaoKe();
            f1.ShowDialog();
        }

        private void insertNhacNoDueDateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void SendSMSNhacNoDueDateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmSendSMSNhacNoDueDate f1 = new frmSendSMSNhacNoDueDate();
            f1.ShowDialog();
        }

        private void ImportThongBaoSaoKeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmInsertSMSThongBaoSaoKe f1 = new frmInsertSMSThongBaoSaoKe();
            f1.ShowDialog();
        }

        private void ImportNhacNoDueDateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmInsertSMSDueDate f1 = new frmInsertSMSDueDate();
            f1.ShowDialog();
        }

        private void sMSNhacNoDueDateToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
