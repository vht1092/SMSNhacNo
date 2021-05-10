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
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void sMSThongBaoSaoKeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmSMSThongBaoSaoKe f1 = new frmSMSThongBaoSaoKe();
            f1.ShowDialog();
        }

        private void sMSNhacNoDueDateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmSMSNhacNoDueDate f2 = new frmSMSNhacNoDueDate();
            f2.ShowDialog();
        }

    }
}
