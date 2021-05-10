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
    public partial class frmInsertSMSNhacNoSaoKe : Form
    {
        private SMSNhacNoSaoKeLogWriter _log = new SMSNhacNoSaoKeLogWriter();

        public frmInsertSMSNhacNoSaoKe()
        {
            InitializeComponent();
        }

        private void btnInsetSMSSaoKe_Click(object sender, EventArgs e)
        {
            string cardBrn = tboxCardBrn.Text;
            string statementMonth = tboxStatementMonth.Text;

            DataTable table = new DataTable();
            try
            {
                _log.WriteLog("----------------Begin Process-----------------");
                table.Rows.Clear();
                table = GetSmsData(statementMonth, cardBrn);
                if (table.Rows.Count > 0)
                {
                    DialogResult dialogResult = MessageBox.Show("So luong gui tin nhan SMS la " + table.Rows.Count, "Info", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        List<ObjInsertSMSSaoKe> listObjs = new List<ObjInsertSMSSaoKe>();
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

                            if (totBalIpp == null || totBalIpp.Equals(""))
                                totBalIpp = "0";

                            ObjInsertSMSSaoKe obj = new ObjInsertSMSSaoKe(id, smsType, smsDetail, desMobile
                                , dateTime, insertTransDate, pan, cardBrnData, cardType, smsMonth, closingBal, dueDate
                                , minimumPayment, actType, totBalIpp, vip, cifVip, cardNo, loc, idStatement);
                            
                            listObjs.Add(obj);

                        }
                        Insert_SMSMessage(listObjs, cardBrn);
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

        private void Insert_SMSMessage(List<ObjInsertSMSSaoKe> listObjs, string crd_brn)
        {
            if (listObjs != null && listObjs.Count > 0)
            {
                int result = 0;
                int count = 0;
                foreach (ObjInsertSMSSaoKe obj in listObjs)
                {
                    result = 0;
                    string message = CreateSMSMessage(obj.getCardNo(), obj.getSmsMonth(), obj.getClosingBalance(), obj.getMinimumPayment()
                        , obj.getDueDate(), obj.getTotalBalIpp(), crd_brn, obj.getVip(), obj.getCifVip(), obj.getIdStatement());

                    if (string.IsNullOrEmpty(message) == false)
                    {
                        obj.setSmsDetail(message);

                        double ipp = 0;
                        if (obj.getTotalBalIpp() != "")
                            ipp = double.Parse(obj.getTotalBalIpp());

                        //closing balance >= 0 hoac closing balance <= -100,000 (du co hon 100k) hoac ipp > 0 
                        if (double.Parse(obj.getClosingBalance()) >= 0 || double.Parse(obj.getClosingBalance()) <= -100000 || ipp > 0)
                        {
                            if (obj.getDesMobile() == "khong co") //ko co so dien thoai
                            {
                                obj.setActionType("D"); //D = ko gui qua ebanking
                                result = DataAccess.Insert_SMS_Nhac_No_Sao_Ke(obj);
                            }
                            else
                            {
                                obj.setActionType("N"); //N = se gui qua ebanking
                                result = DataAccess.Insert_SMS_Nhac_No_Sao_Ke(obj);
                            }
                        }
                        else // 0 > closing > -100000 and don't have IPP, don't send
                        {
                            obj.setActionType("D"); //D = ko gui qua ebanking
                            result = DataAccess.Insert_SMS_Nhac_No_Sao_Ke(obj);
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

        private DataTable GetSmsData(string statementMonth, string cardBrn)
        {
            DataTable table = new DataTable();
            try
            {
                table = DataAccess.GetSmsData(statementMonth, cardBrn);
            }
            catch (Exception ex)
            {
                _log.WriteLog("Error Get_SMS_Nhac_No_Sao_Ke(), " + ex.Message);
            }
            return table;
        }

        private string CreateSMSMessage(string p_pan, string p_settleDay, string p_closingBal
                                    , string p_miniPay, string p_dueDay, string p_tol_bal_ipp
                                    , string p_crd_brn, string p_vip_card, string p_vip_cif, string p_ID_statement)
        {
            try
            {
                string SCBPhone = "";
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
