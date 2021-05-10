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
    public partial class frmInsertSMSDueDate : Form
    {
        private SMSNhacNoDueDateLogWritter _log = new SMSNhacNoDueDateLogWritter();
        public static string IDALERT = "MASTER_CARD_ALERT";
        public static string SMS_TYPE = "DEBT02";

        public frmInsertSMSDueDate()
        {
            InitializeComponent();
            txbStatementMonth.Text = DateTime.Now.AddMonths(-1).ToString("yyyyMM"); ;
        }

        private void btnSendSMS_Click(object sender, EventArgs e)
        {
            string p_settl_month = txbStatementMonth.Text;
            string p_due_date = txbDueDate.Text;

            DataTable table = new DataTable();
            try
            {
                _log.WriteLog("----------------Begin Process-----------------");
                table.Rows.Clear();
                table = GetSmsDataDueDate(p_settl_month, p_due_date);
                if (table.Rows.Count > 0)
                {
                    DialogResult dialogResult = MessageBox.Show("So luong gui tin nhan SMS la " + table.Rows.Count, "Info", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        List<ObjInsertSMSDueDate> listObjs = new List<ObjInsertSMSDueDate>();
                        foreach (DataRow row in table.Rows)
                        {
                            string id = row.ItemArray[0].ToString();
                            string smsType = row.ItemArray[1].ToString();
                            string smsDetail = row.ItemArray[2].ToString(); //empty
                            string desMobile = row.ItemArray[3].ToString();
                            string dateTime = row.ItemArray[4].ToString();
                            string insertTransDate = row.ItemArray[5].ToString(); //empty
                            string pan = row.ItemArray[6].ToString();
                            string cardBrnData = row.ItemArray[7].ToString();
                            string cardType = row.ItemArray[8].ToString();
                            string smsMonth = row.ItemArray[9].ToString(); //YYYYMM
                            string closingBal = row.ItemArray[10].ToString();
                            string dueDate = row.ItemArray[11].ToString();
                            string minimumPayment = row.ItemArray[12].ToString();
                            string actType = row.ItemArray[13].ToString();//N: tao moi, Y: da gui qua eb, F: gui qua eb failed, D: ko gui
                            string totBalIpp = row.ItemArray[14].ToString();
                            string vip = row.ItemArray[15].ToString();
                            string cifVip = row.ItemArray[16].ToString();
                            string cardNo = row.ItemArray[17].ToString();
                            string loc = row.ItemArray[18].ToString();
                            string idStatement = row.ItemArray[19].ToString();

                            ObjInsertSMSDueDate obj = new ObjInsertSMSDueDate(id, smsType, smsDetail, desMobile
                                , dateTime, insertTransDate, pan, cardBrnData, cardType, smsMonth, closingBal, dueDate
                                , minimumPayment, actType, totBalIpp, vip, cifVip, cardNo, loc, idStatement);

                            listObjs.Add(obj);
                        }
                        Insert_SMSMessage(listObjs);
                    }
                    else if (dialogResult == DialogResult.No)
                    {
                        _log.WriteLog("User press button No ==> exit");
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("Data is empty", "Error");
                    _log.WriteLog("Data is empty");
                }
            }
            catch (Exception ex)
            {
                _log.WriteLog("Error RunService(), " + ex.Message);
            }
        }

        private DataTable GetSmsDataDueDate(string statementMonth, string dueDate)
        {
            DataTable table = new DataTable();
            try
            {
                table = DataAccess.GetSmsDataDueDate(statementMonth, dueDate);
            }
            catch (Exception ex)
            {
                _log.WriteLog("Error Get_SMS_Nhac_No_Due_Date(), " + ex.Message);
            }
            return table;
        }

        private void Insert_SMSMessage(List<ObjInsertSMSDueDate> listObjs)
        {
            if (listObjs != null && listObjs.Count > 0)
            {
                int result = 0;
                foreach (ObjInsertSMSDueDate obj in listObjs)
                {
                    result = 0;
                    string message = CreateSMSMessage(obj.getCardBrn(), obj.getCardNo(), obj.getSmsMonth(), obj.getClosingBalance(),
                        obj.getMinimumPayment(), obj.getDueDate(), obj.getVip(), obj.getCifVip());

                    if (string.IsNullOrEmpty(message) == false)
                    {
                        obj.setSmsDetail(message);
                        if (obj.getDesMobile() == "khong co") //ko co so dien thoai
                        {
                            obj.setActionType("D"); //D = ko gui qua ebanking
                            result = DataAccess.Insert_SMS_Nhac_No_Due_Date(obj);
                        }
                        else
                        {
                            obj.setActionType("N"); //N = se gui qua ebanking
                            result = DataAccess.Insert_SMS_Nhac_No_Due_Date(obj);
                        }
                    }
                    else
                    {
                        _log.WriteLog("LOC " + obj.getLoc() + " can not create message. Please check again");
                    }
                }

                MessageBox.Show("Doneeeeeeeeeeeee", "Info");
            }
            else
            {
                _log.WriteLog("List send sms is empty. Please check again");
                MessageBox.Show("List send sms is empty. Please check again", "Info");
            }
        }

        private string CreateSMSMessage(string brand, string pan, string settleDay, string totalClosingBal
            , string totalMiniPay, string dueDay, string vip_card, string vip_cif)
        {
            try
            {
                string SCBPhone = "";
                pan = pan.Substring(12, 4);
                string settleDay_f = "";
                if (brand.Equals("VS"))
                    settleDay_f = "15/" + settleDay.Substring(4, 2) + "/" + settleDay.Substring(2, 2);
                else
                    settleDay_f = "25/" + settleDay.Substring(4, 2) + "/" + settleDay.Substring(2, 2);

                dueDay = dueDay.Substring(6, 2) + "/" + dueDay.Substring(4, 2);
                if (vip_card == "Y" || vip_cif == "Y")
                    SCBPhone = "1800545438";
                else
                    SCBPhone = "19006538";

                double d_totalClsBal = double.Parse(totalClosingBal);
                double d_totalMin = double.Parse(totalMiniPay);

                string s_totalCloBal = string.Format("{0:#,##0.##}", d_totalClsBal);
                string s_totalMiniPay = string.Format("{0:#,##0.##}", d_totalMin);

                string smsMessage = "Cam on Quy khach su dung the " + pan + "\nDu no den " + settleDay_f + ": " + s_totalCloBal + "VND\nTT toi thieu: " + s_totalMiniPay + "VND" + "\nNgay den han " + dueDay + "\nVui long bo qua neu da TT\nLH " + SCBPhone;

                return smsMessage;
            }
            catch (Exception ex)
            {
                _log.WriteLog(ex.Message);
                return "";
            }
        }

    }
}
