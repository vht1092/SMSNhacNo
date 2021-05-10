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
    public partial class frmSendSMSThongBaoSaoKe : Form
    {
        public static string SMS_TYPE = "DEBT01";
        public static string IDALERT = "MASTER_CARD_ALERT";
        private SMSNhacNoSaoKeLogWriter _log = new SMSNhacNoSaoKeLogWriter();

        public frmSendSMSThongBaoSaoKe()
        {
            InitializeComponent();
        }

        private void btn_Click(object sender, EventArgs e)
        {
            string f_cardBrn = tboxCardBrn.Text;
            string f_statementMonth = tboxStatementMonth.Text;
            string f_smsType = tboxSMSType.Text;
            _log.WriteLog("f_cardBrn: " + f_cardBrn + ", f_statementMonth: " + f_statementMonth + ", f_smsType: " + f_smsType);
            List<ObjInsertSMSSaoKe> listObjs = DataAccess.getListSMSNhacNoSaoKe(f_cardBrn, f_statementMonth, f_smsType);

            if (listObjs != null && listObjs.Count > 0)
            {
                DialogResult dialogResult = MessageBox.Show("Số lượng gởi SMS sao kê là: " + listObjs.Count, "Info", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (dialogResult == DialogResult.Yes)
                {
                    int rowNum = 0;
                    lblLoading.Text = "Loading...";
                    lblLoading.Visible = true;
                    foreach (ObjInsertSMSSaoKe obj in listObjs)
                    {
                        rowNum++;
                        //call function insert DB GW
                        int result = DataAccess.InsertSMSMessateToEBankGW_2(IDALERT
                                                                        , obj.getDesMobile()//classDataAccess.MYPHONE
                                                                        , obj.getSmsDetail()
                                                                        , 'N'// hhhh /N: tao moi, Y: da gui qua eb, F: gui qua eb failed, D: ko gui
                                                                        , SMS_TYPE);
                        //call function update table SMS_NHAC_NO  
                        if (result == 1)
                        {
                            DataAccess.UpdateStatusSMSNhacNoSaoKe(obj.getId(), obj.getCardBrn(), obj.getSmsMonth(), obj.getSmsType(), "Y");
                            _log.WriteLog("id: " + obj.getId() + ", card brn: " + obj.getCardBrn() + ", Month: " + obj.getSmsMonth() + ", Loc: " + obj.getLoc() + " is successful");
                        }
                        else
                        {
                            DataAccess.UpdateStatusSMSNhacNoSaoKe(obj.getId(), obj.getCardBrn(), obj.getSmsMonth(), obj.getSmsType(), "F");
                            _log.WriteLog("id: " + obj.getId() + ", card brn: " + obj.getCardBrn() + ", Month: " + obj.getSmsMonth() + ", Loc: " + obj.getLoc() + " is failed");
                        }
                        lblLoading.Text = "Loading " + (rowNum * 100 / listObjs.Count) + "%...";
                    }
                    lblLoading.Text = "Send SMS completed.";
                    lblLoading.Visible = false;
                    MessageBox.Show("DONE.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (dialogResult == DialogResult.No)
                {
                    _log.WriteLog("User press button No ==> exit");
                    MessageBox.Show("User press button No ==> exit", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            else
            {
                MessageBox.Show("List SMS Nhac No Sao Ke is null. Please check again", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _log.WriteLog("List SMS Nhac No Sao Ke is null. Please check again");
            }


            //MessageBox.Show("Data: " + f_cardBrn + ", " + f_statementMonth + ", " + f_smsType);

        }
    }
}
