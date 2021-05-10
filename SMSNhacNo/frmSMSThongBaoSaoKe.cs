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
    public partial class frmSMSThongBaoSaoKe : Form
    {
        public static string SMS_TYPE = "DEBT01";
        public static string IDALERT = "MASTER_CARD_ALERT";
        private static string SCBPhone = "";
        private SMSNhacNoSaoKeLogWriter _log = new SMSNhacNoSaoKeLogWriter();

        public frmSMSThongBaoSaoKe()
        {
            InitializeComponent();
        }

        private void btn_Click(object sender, EventArgs e)
        {
            string cardBrn = tboxCardBrn.Text;
            string statementMonth = tboxStatementMonth.Text;

            DataTable table = new DataTable();

            try
            {
                _log.WriteLog("----------------Begin Process-----------------");
                table.Rows.Clear();
                table = Get_SMS_Nhac_No_Sao_Ke(statementMonth, cardBrn);
                if (table.Rows.Count > 0)
                {
                    DialogResult dialogResult = MessageBox.Show("So luong gui tin nhan SMS la " + table.Rows.Count, "Info", MessageBoxButtons.YesNo);
                    
                    if (dialogResult == DialogResult.Yes)
                    {
                        List<ObjectSMSNhacNoSaoKe> listObjs = new List<ObjectSMSNhacNoSaoKe>();
                        foreach (DataRow row in table.Rows)
                        {
                            string sdate = row.ItemArray[0].ToString();
                            string sHandPhone = row.ItemArray[1].ToString();
                            string sCardNo = row.ItemArray[2].ToString();
                            string sCardType = row.ItemArray[3].ToString();//Standard, Gold, . . . .
                            string sCardBrn = row.ItemArray[4].ToString(); //VS, MC
                            string sStateMonth = row.ItemArray[5].ToString();
                            string sClosingBalance = row.ItemArray[6].ToString();
                            string sMinimumPayment = row.ItemArray[7].ToString();
                            string sDueDate = row.ItemArray[8].ToString();
                            string sCardMask = row.ItemArray[9].ToString();
                            string sTotBalIPP = row.ItemArray[10].ToString();
                            string sVip = row.ItemArray[11].ToString();
                            string sCifVip = row.ItemArray[12].ToString();
                            string sIdStatement = row.ItemArray[13].ToString();

                            ObjectSMSNhacNoSaoKe obj = new ObjectSMSNhacNoSaoKe(sdate, sHandPhone, sCardNo, sCardType, sCardBrn
                                , sStateMonth, sClosingBalance, sMinimumPayment, sDueDate, sCardMask, sTotBalIPP, sVip, sCifVip, sIdStatement);

                            listObjs.Add(obj);
                        }
                        _log.WriteLog("So luong gui tin nhan SMS nhac no sao ke ky " + statementMonth + " la: " + listObjs.Count);

                        Insert_SMSMessage(listObjs, cardBrn);

                        MessageBox.Show("Hoàn tất gửi tin nhắn sao kê.","Info", MessageBoxButtons.OK);
                    }
                    else if (dialogResult == DialogResult.No)
                    {
                        _log.WriteLog("User press button No ==> exit");
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                _log.WriteLog("Error RunService(), " + ex.Message);
            }
        }

        private DataTable Get_SMS_Nhac_No_Sao_Ke(string statementMonth, string cardBrn) 
        {
            DataTable table = new DataTable();
            try
            {
                table = DataAccess.GetSMSNhacNoSaoKeDB(statementMonth, cardBrn);
            }
            catch (Exception ex)
            {
                _log.WriteLog("Error Get_SMS_Nhac_No_Sao_Ke(), " + ex.Message);
            }
            return table;
        }

        private void Insert_SMSMessage(List<ObjectSMSNhacNoSaoKe> listObjs, string crd_brn)
        {
            if (listObjs != null && listObjs.Count > 0)
            {
                int result = 0;
                int count = 0;  
                foreach (ObjectSMSNhacNoSaoKe obj in listObjs)
                {
                    result = 0;
                    string message = CreateSMSMessage(obj.getCardNo(), obj.getStateMonth(), obj.getClosingBalance(), obj.getMinimumPayment()
                        , obj.getDueDate(), obj.getTotBalIPP(), crd_brn, obj.getVip(), obj.getCifVip(), obj.getIdStatement());
                    
                    if (string.IsNullOrEmpty(message) == false)
                    {
                        double ipp = 0;
                        if (obj.getTotBalIPP() != "")
                            ipp = double.Parse(obj.getTotBalIPP());

                        //closing balance >= 0 hoac closing balance <= -100,000 (du co hon 100k) hoac ipp > 0 
                        if (double.Parse(obj.getClosingBalance()) >= 0 || double.Parse(obj.getClosingBalance()) <= -100000 || ipp > 0)
                        {
                            if (obj.getHandPhone() == "khong co") //ko co so dien thoai
                            {
                                result = DataAccess.InsertSMSMessateToEBankGW_2(IDALERT
                                                                    , obj.getHandPhone()//classDataAccess.MYPHONE
                                                                    , message
                                                                    , 'Y'// Y: se ko gui tin nhan, D: ko gui, N: gui, E:Error
                                                                    , SMS_TYPE);
                            }
                            else
                            {
                                result = DataAccess.InsertSMSMessateToEBankGW_2(IDALERT
                                                                    , obj.getHandPhone()//classDataAccess.MYPHONE
                                                                    , message
                                                                    , 'N'// hhhh Y: se ko gui tin nhan, D: ko gui, N: gui, E:Error
                                                                    , SMS_TYPE);
                            }
                        }
                        else // 0 > closing > -100000 and don't have IPP, don't send
                        {
                            count += DataAccess.InsertReminderPayment_1SMSToDW(SMS_TYPE, message
                                                                   , obj.getHandPhone()//    ,long.Parse(row.ItemArray[10].ToString())
                                                                   , DateTime.Parse(obj.getSDate())
                                                                   , obj.getCardMask()
                                                                   , obj.getCardBrn()
                                                                   , obj.getCardType()
                                                                   , obj.getStateMonth()//    , long.Parse(row.ItemArray[5].ToString())
                                                                   , obj.getClosingBalance()//    , long.Parse(row.ItemArray[6].ToString())
                                                                   , double.Parse(obj.getMinimumPayment())//    , long.Parse(row.ItemArray[7].ToString())
                                                                   , obj.getDueDate()//    , int.Parse(row.ItemArray[8].ToString())
                                                                   , "Y");
                        }

                        if (result == 1)
                        {
                            if (obj.getHandPhone() == "khong co")
                            {
                                count += DataAccess.InsertReminderPayment_1SMSToDW(SMS_TYPE, message
                                                                    , obj.getHandPhone()//    ,long.Parse(row.ItemArray[10].ToString())
                                                                    , DateTime.Parse(obj.getSDate())
                                                                    , obj.getCardMask()
                                                                    , obj.getCardBrn()
                                                                    , obj.getCardType()
                                                                    , obj.getStateMonth()//    , long.Parse(row.ItemArray[5].ToString())
                                                                    , obj.getClosingBalance()//    , long.Parse(row.ItemArray[6].ToString())
                                                                    , double.Parse(obj.getMinimumPayment())//    , long.Parse(row.ItemArray[7].ToString())
                                                                    , obj.getDueDate()//    , int.Parse(row.ItemArray[8].ToString())
                                                                    , "Y");
                            }
                            else
                            {
                                count += DataAccess.InsertReminderPayment_1SMSToDW(SMS_TYPE, message
                                                                    , obj.getHandPhone()//    ,long.Parse(row.ItemArray[10].ToString())
                                                                    , DateTime.Parse(obj.getSDate())
                                                                    , obj.getCardMask()
                                                                    , obj.getCardBrn()
                                                                    , obj.getCardType()
                                                                    , obj.getStateMonth()//    , long.Parse(row.ItemArray[5].ToString())
                                                                    , obj.getClosingBalance()//    , long.Parse(row.ItemArray[6].ToString())
                                                                    , double.Parse(obj.getMinimumPayment())//    , long.Parse(row.ItemArray[7].ToString())
                                                                    , obj.getDueDate()//    , int.Parse(row.ItemArray[8].ToString())
                                                                    , "N"//hhhh Y: se ko gui tin nhan, D: ko gui, N: gui, E:Error
                                                                    );
                            }
                        }
                        else
                        {
                            _log.WriteLog("Insert " + obj.getCardNo() + " failed.");
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

        private string CreateSMSMessage(string p_pan, string p_settleDay, string p_closingBal
                                    , string p_miniPay, string p_dueDay, string p_tol_bal_ipp
                                    , string p_crd_brn, string p_vip_card, string p_vip_cif, string p_ID_statement)
        {
            try
            {
                string last4Digits = p_pan.Substring(12, 4); //cat 4 so duoi the
                string settleDay = p_settleDay.Substring(4, 2) + "/" + p_settleDay.Substring(2, 2);
                string dueDay = p_dueDay.Substring(6, 2) + "/" + p_dueDay.Substring(4, 2);

                double clsBal = double.Parse(p_closingBal);
                double mini = double.Parse(p_miniPay);

                //Neu KH co du co card va co du no IPP thi lay du no IPP lam closingball va mini
                if (double.Parse(p_closingBal) < 0 && p_tol_bal_ipp != "")
                {
                    if (double.Parse(p_tol_bal_ipp) > 0)
                    {
                        clsBal = double.Parse(p_tol_bal_ipp);
                        mini = double.Parse(p_tol_bal_ipp);
                    }
                }

                //Neu KH co du no card va co du no IPP thi lay du no IPP cong closingbal va mini
                if (double.Parse(p_closingBal) >= 0 && p_tol_bal_ipp != "")
                {
                    clsBal += double.Parse(p_tol_bal_ipp);
                    mini += double.Parse(p_tol_bal_ipp);
                }

                string sCloBal = string.Format("{0:#,##0.##}", clsBal);
                string sMiniPayment = string.Format("{0:#,##0.##}", mini);

                string smsMessage = "";

                if (p_vip_card == "Y" || p_vip_cif == "Y")
                    SCBPhone = "1800545438";
                else
                    SCBPhone = "19006538";

                if (clsBal < 0)
                {
                    double clsBal_1 = -1 * clsBal;
                    string cloBal_2 = string.Format("{0:#,##0.##}", clsBal_1);
                    if (p_crd_brn == "VS")
                        smsMessage = "Cam on Quy khach da su dung the SCB " + last4Digits + "\nSo DU CO trong the den 15/" + settleDay + ":" + cloBal_2 + "VND";
                    else //MC
                        smsMessage = "Cam on Quy khach da su dung the SCB " + last4Digits + "\nSo DU CO trong the den 25/" + settleDay + ":" + cloBal_2 + "VND";

                }
                else
                {
                    if (clsBal == 0)
                    {
                        if (p_crd_brn == "VS")
                            smsMessage = "Cam on Quy khach da su dung va thanh toan the SCB " + last4Digits + "\nDu no den 15/" + settleDay + ": 0VND.";
                        else //MC
                            smsMessage = "Cam on Quy khach da su dung va thanh toan the SCB " + last4Digits + "\nDu no den 25/" + settleDay + ": 0VND.";
                    }
                    else //clsBal > 0
                    {
                        if (p_crd_brn == "VS")
                            smsMessage = "Sao ke the " + last4Digits + "\nDu no den 15/" + settleDay + ": " + sCloBal + "VND\nTT toi thieu "
                                                + sMiniPayment + "VND\nHan TT " + dueDay;
                        else //MC
                            smsMessage = "Sao ke the " + last4Digits + "\nDu no den 25/" + settleDay + ": " + sCloBal + "VND\nTT toi thieu "
                                                + sMiniPayment + "VND\nHan TT " + dueDay;
                    }
                }

                if (p_ID_statement != null && !p_ID_statement.Trim().Equals(""))
                    smsMessage = smsMessage + "\nChi tiet: https://card.scb.com.vn/skt/skt.html?id=" + p_ID_statement;
                

                return smsMessage;

            }
            catch (Exception ex)
            {
                _log.WriteLog("Error CreateSMSMessage(), " + ex.Message);
                return "";
            }
        }
    }
}
/*
0:  private string sdate; 
1:  private string handPhone;
2:  private string cardNo;
3:  private string cardType;
4:  private string cardBrn;
5:  private string stateMonth;
6:  private string closingBalance;
7:  private string minimumPayment;
8:  private string dueDate;
9:  private string cardMask;
10: private string totBalIPP;
11: private string vip;
12: private string cifVip;
13: private string idStatement;
*/