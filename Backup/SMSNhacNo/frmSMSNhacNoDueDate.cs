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
    public partial class frmSMSNhacNoDueDate : Form
    {
        private SMSNhacNoDueDateLogWritter _log = new SMSNhacNoDueDateLogWritter();
        public static string IDALERT = "MASTER_CARD_ALERT";
        public static string SMS_TYPE = "DEBT02";
        private static string SCBPhone = "";
        public static string MY_PHONE = "0963137131";

        public frmSMSNhacNoDueDate()
        {
            InitializeComponent();
            txbStatementMonth.Text = DateTime.Now.AddMonths(-1).ToString("yyyyMM"); ;
        }

        private void btnSendSMS_Click(object sender, EventArgs e)
        {
            String statementMonth = txbStatementMonth.Text;
            String dueDate = txbDueDate.Text;
            DataTable table = new DataTable();

            try
            {
                _log.WriteLog("----------------Begin Process-----------------");
                table.Rows.Clear();
                table = Get_SMS_Nhac_No_Due_Date(statementMonth, dueDate);
                if (table.Rows.Count > 0)
                {
                    DialogResult dialogResult = MessageBox.Show("So luong gui tin nhan SMS la " + table.Rows.Count, "Info", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        List<ObjectSMSNhacNoDueDate> listObjs = new List<ObjectSMSNhacNoDueDate>();
                        foreach (DataRow row in table.Rows)
                        {
                            ObjectSMSNhacNoDueDate obj = new ObjectSMSNhacNoDueDate();
                            obj.setSysDate(row.ItemArray[0].ToString());
                            obj.setHandPhone(row.ItemArray[1].ToString());
                            obj.setCardNo(row.ItemArray[2].ToString());
                            obj.setCardType(row.ItemArray[3].ToString());
                            obj.setCardBrand(row.ItemArray[4].ToString());
                            obj.setStatementMonth(row.ItemArray[5].ToString());
                            obj.setClosingBalance(row.ItemArray[6].ToString());
                            obj.setMinimumPayment(row.ItemArray[7].ToString());
                            obj.setDueDate(row.ItemArray[8].ToString());
                            obj.setMaskCard(row.ItemArray[9].ToString());
                            obj.setTotalBalanceIPP(row.ItemArray[10].ToString());
                            obj.setVip(row.ItemArray[11].ToString());
                            obj.setCifVip(row.ItemArray[12].ToString());
                            obj.setSumPaymentCw(row.ItemArray[13].ToString());

                            listObjs.Add(obj);
                        }

                        Insert_SMSMessage(listObjs);
                    }


                }
            }
            catch (Exception ex)
            {

                _log.WriteLog("Error RunService(), " + ex.Message);
            }
        }

        private DataTable Get_SMS_Nhac_No_Due_Date(string statementMonth, string dueDate)
        {
            DataTable table = new DataTable();
            try
            {
                table = DataAccess.GetSMSNhacNoDueDateDB(statementMonth, dueDate);
            }
            catch (Exception ex)
            {
                _log.WriteLog("Error Get_SMS_Nhac_No_Due_Date(), " + ex.Message);
            }
            return table;
        }

        private void Insert_SMSMessage(List<ObjectSMSNhacNoDueDate> listObjs)
        {

            if (listObjs != null && listObjs.Count > 0)
            {
                string message = "";
                int result = 0;
                int count = 0;

                foreach (ObjectSMSNhacNoDueDate obj in listObjs)
                {
                    result = 0;
                    message = CreateSMSMessage(obj.getCardBrand(), obj.getCardNo(), obj.getStatementMonth(),
                                              obj.getClosingBalance(), obj.getMinimumPayment(), obj.getDueDate(),
                                              obj.getTotalBalanceIPP(), obj.getVip(), obj.getCifVip(), obj.getSumPaymentCw());
                    if (string.IsNullOrEmpty(message) == false)
                    {
                        if (obj.getHandPhone() == "khong co")
                        {
                            result = DataAccess.InsertSMSMessateToEBankGW_2(IDALERT
                                                               , obj.getHandPhone()// MY_PHONE
                                                               , message
                                                               , 'Y'//Sent (Y se ko gui tin nhan)
                                                               , SMS_TYPE);
                        }
                        else
                        {
                            result = DataAccess.InsertSMSMessateToEBankGW_2(IDALERT
                                                                , obj.getHandPhone()// MY_PHONE
                                                                , message
                                                                , 'N'//hhhh Sent (N se gui tin nhan, Y se ko gui tin nhan)
                                                                , SMS_TYPE);
                        }
                        if (result == 1)
                        {
                            if (obj.getHandPhone() == "khong co")
                            {
                                count += DataAccess.InsertReminderPayment_2SMSToDW(SMS_TYPE, message
                                                                    , obj.getHandPhone()    //,long.Parse(obj.getTotalBalanceIPP())
                                                                    , DateTime.Parse(obj.getSysDate())
                                                                    , obj.getMaskCard()
                                                                    , obj.getCardBrand()
                                                                    , obj.getCardType()
                                                                    , obj.getStatementMonth()//    , long.Parse(obj.getStatementMonth())
                                                                    , obj.getClosingBalance()//    , long.Parse(obj.getClosingBalance())
                                                                    , obj.getMinimumPayment()//    , long.Parse(obj.getMinimumPayment())
                                                                    , obj.getDueDate()//    , int.Parse(obj.getDueDate())
                                                                    , "Y"
                                                                    );
                            }
                            else
                            {
                                count += DataAccess.InsertReminderPayment_2SMSToDW(SMS_TYPE, message
                                                                   , obj.getHandPhone()//    ,long.Parse(obj.getTotalBalanceIPP())
                                                                   , DateTime.Parse(obj.getSysDate())
                                                                   , obj.getMaskCard()
                                                                   , obj.getCardBrand()
                                                                   , obj.getCardType()
                                                                   , obj.getStatementMonth()//    , long.Parse(obj.getStatementMonth())
                                                                   , obj.getClosingBalance()//    , long.Parse(obj.getClosingBalance())
                                                                   , obj.getMinimumPayment()//    , long.Parse(obj.getMinimumPayment())
                                                                   , obj.getDueDate()//    , int.Parse(obj.getDueDate())
                                                                   , "N" //hhhh (N se gui tin nhan, Y se ko gui tin nhan)
                                                                   );
                            }
                        }
                    }
                }
                _log.WriteLog("So luong message da duoc Insert vao EbankGW thanh cong: " + count);
                return;
            }
            else
            {
                _log.WriteLog("List send sms is empty. Please check again");
                MessageBox.Show("List send sms is empty. Please check again", "Info");
            }

        }

        private string CreateSMSMessage(string brand, string pan, string settleDay, string closingBal
        , string miniPay, string dueDay, string tol_bal_ipp, string vip_card, string vip_cif, string sum_pays_cw)
        {
            try
            {
                pan = pan.Substring(12, 4);
                string settleDay_f = null;
                if (brand == "VISA")
                    settleDay_f = "15/" + settleDay.Substring(4, 2) + "/" + settleDay.Substring(2, 2);
                else
                    settleDay_f = "25/" + settleDay.Substring(4, 2) + "/" + settleDay.Substring(2, 2);

                //dueDay = dueDay.Substring(6, 2) + "/" + dueDay.Substring(4, 2) + "/" + dueDay.Substring(2, 2);
                dueDay = dueDay.Substring(6, 2) + "/" + dueDay.Substring(4, 2);
                if (vip_card == "Y" || vip_cif == "Y")
                    SCBPhone = "1800545438";
                else
                    SCBPhone = "19006538";
                double clsBal = 0;// double.Parse(closingBal); 
                double min = 0;// double.Parse(miniPay);
                //if (double.Parse(closingBal) < 0 || double.Parse(sum_pays_cw) < 0)
                if (double.Parse(closingBal) < 0)
                {
                    if (tol_bal_ipp != "")
                    {
                        //clsBal += double.Parse(tol_bal_ipp);
                        //min += double.Parse(tol_bal_ipp);
                        clsBal = double.Parse(tol_bal_ipp);
                        min = double.Parse(tol_bal_ipp);
                    }
                }
                else
                {
                    if (double.Parse(sum_pays_cw) < 0)// closing > 0 va sum_pays_cw < 0
                    {
                        clsBal = double.Parse(closingBal) + double.Parse(tol_bal_ipp);
                        min = double.Parse(tol_bal_ipp);
                    }
                    else
                    {
                        clsBal = double.Parse(closingBal) + double.Parse(tol_bal_ipp);
                        min = double.Parse(miniPay) + double.Parse(tol_bal_ipp);
                    }
                }
                string cloBal = string.Format("{0:#,##0.##}", clsBal);
                string miniPayment = string.Format("{0:#,##0.##}", min);

                //string smsMessage = "Cam on Quy khach da su dung the SCB " + brand + " x" + pan + ".Tong so tien TT: "
                //+ cloBal + "VND" + ".Vui long TT toi thieu: " + miniPayment + "VND" + " truoc " + dueDay + ".Chi tiet LH: " + SCBPhone;
                string smsMessage = "Cam on Quy khach su dung the " + pan + "\nDu no den " + settleDay_f + ": " + cloBal + "VND\nTT toi thieu: " + miniPayment + "VND" + "\nNgay den han " + dueDay + "\nVui long bo qua neu da TT\nLH " + SCBPhone;

                //if (smsMessage.Length > 160)
                //    smsMessage = smsMessage.Substring(0, 160);
                return smsMessage;
            }
            catch (Exception ex)
            {
                _log.WriteLog(ex.Message);
                return "";
            }
        }

        //private void TxbStatementMonth_TextChanged(object sender, EventArgs e)
        //{
        //    txbStatementMonth.Text = "201908";
        //}
    }
}
