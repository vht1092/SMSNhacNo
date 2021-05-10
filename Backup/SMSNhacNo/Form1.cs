using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SMSNhacNo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void sMSNhacNoSaoKeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmSMSNhacNoSaoKe f1 = new frmSMSNhacNoSaoKe();
            f1.ShowDialog();
        }

        private void sMSNhacNoDueDateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void insertNhacNoSaoKeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmInsertSMSNhacNoSaoKe f1 = new frmInsertSMSNhacNoSaoKe();
            f1.ShowDialog();
        }

        private void sendSMSNhacNoSaoKeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmSendSMSNhacNoSaoKe f1 = new frmSendSMSNhacNoSaoKe();
            f1.ShowDialog();
        }

        private void insertNhacNoDueDateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmInsertSMSDueDate f1 = new frmInsertSMSDueDate();
            f1.ShowDialog();
        }
    }
}
