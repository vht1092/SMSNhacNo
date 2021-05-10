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
    public partial class frmSendSMSNhacNoDueDate : Form
    {
        private SMSNhacNoDueDateLogWritter _log = new SMSNhacNoDueDateLogWritter();
        public static string IDALERT = "MASTER_CARD_ALERT";
        public static string SMS_TYPE = "DEBT02";

        public frmSendSMSNhacNoDueDate()
        {
            InitializeComponent();
            txbStatementMonth.Text = DateTime.Now.AddMonths(-1).ToString("yyyyMM");
            //DateTime dueDateDefaultValue = new DateTime(DateTime.Now.Year, DateTime.Now.Month - 1, 25).AddDays(15);
            //txbDueDate.Text = dueDateDefaultValue.ToString("yyyyMMdd");
        }

        private void btnSendSMS_Click(object sender, EventArgs e)
        {
            String f_statementMonth = txbStatementMonth.Text;
            String f_dueDate = txbDueDate.Text;
            String f_smsType = tboxSMSType.Text;
            _log.WriteLog("f_statementMonth: " + f_statementMonth + ", f_dueDate: " + f_dueDate + ", f_smsType: " + f_smsType);
            List<ObjInsertSMSDueDate> listObjs = DataAccess.getListSMSNhacNoDueDate(f_dueDate, f_statementMonth, f_smsType);

            if (listObjs != null && listObjs.Count > 0)
            {
                DialogResult dialogResult = MessageBox.Show("Số lượng gởi SMS nhắc nợ là: " + listObjs.Count, "Info", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (dialogResult == DialogResult.Yes)
                {
                    int rowNum = 0;
                    lblLoading.Text = "Loading...";
                    lblLoading.Visible = true;
                    foreach (ObjInsertSMSDueDate obj in listObjs)
                    {
                        rowNum++;
                        int result = DataAccess.InsertSMSMessateToEBankGW_2(IDALERT
                                                                        , obj.getDesMobile()//classDataAccess.MYPHONE
                                                                        , obj.getSmsDetail()
                                                                        , 'N'// hhhh /N: tao moi, Y: da gui qua eb, F: gui qua eb failed, D: ko gui
                                                                        , SMS_TYPE);
                        //call function update table SMS_NHAC_NO  
                        if (result == 1)
                        {
                            DataAccess.UpdateStatusSMSNhacNoDueDate(obj.getId(), obj.getDueDate(), obj.getSmsMonth(), obj.getSmsType(), "Y");
                            _log.WriteLog("id: " + obj.getId() + ", due Date: " + obj.getDueDate() + ", Month: " + obj.getSmsMonth() + ", Loc: " + obj.getLoc() + " is successful");
                        }
                        else
                        {
                            DataAccess.UpdateStatusSMSNhacNoDueDate(obj.getId(), obj.getDueDate(), obj.getSmsMonth(), obj.getSmsType(), "F");
                            _log.WriteLog("id: " + obj.getId() + ", due Date: " + obj.getDueDate() + ", Month: " + obj.getSmsMonth() + ", Loc: " + obj.getLoc() + " is failed");
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
                MessageBox.Show("List SMS Nhac No Due Date is null. Please check again", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _log.WriteLog("List SMS Nhac No Due Date is null. Please check again");
            }
        }
    }

}
