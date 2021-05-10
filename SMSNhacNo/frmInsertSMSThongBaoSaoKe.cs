using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
//using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace SMSNhacNo
{
    public partial class frmInsertSMSThongBaoSaoKe : Form
    {
        public static string SMS_TYPE = "DEBT01";
        private SMSNhacNoSaoKeLogWriter _log = new SMSNhacNoSaoKeLogWriter();

        public frmInsertSMSThongBaoSaoKe()
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
                lblLoading.Text = "Loading...";
                lblLoading.Visible = true;
                table.Rows.Clear();
                table = GetSmsData(statementMonth, cardBrn);
                int rowNum = 0;
                string cyccDay = "";
                if (tboxCardBrn.Text == "VS")
                    cyccDay = "55";
                else
                    if (chkMCW.Checked == true)
                        cyccDay = "68";
                    else
                        cyccDay = "45";

                if (table.Rows.Count > 0)
                {
                    DialogResult dialogResult = MessageBox.Show("So luong tin nhan SMS import la " + table.Rows.Count, "Info", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (dialogResult == DialogResult.Yes)
                    {
                        DataAccess.update_Data_Notify_Send_SMS_Statement(statementMonth, cardBrn, cyccDay);

                        List<ObjInsertSMSSaoKe> listObjs = new List<ObjInsertSMSSaoKe>();
                        foreach (DataRow row in table.Rows)
                        {
                            rowNum++;
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
                            string statementAmt = row.ItemArray[20].ToString();

                            if (totBalIpp == null || totBalIpp.Equals(""))
                                totBalIpp = "0";

                            ObjInsertSMSSaoKe obj = new ObjInsertSMSSaoKe(id, smsType, smsDetail, desMobile
                                , dateTime, insertTransDate, pan, cardBrnData, cardType, smsMonth, closingBal, dueDate
                                , minimumPayment, actType, totBalIpp, vip, cifVip, cardNo, loc, idStatement,statementAmt);

                            listObjs.Add(obj);
                            lblLoading.Text = "Loading " + (rowNum * 50 / listObjs.Count) + "%...";
                        }
                        Insert_SMSMessage(listObjs, cardBrn);

                        lblLoading.Text = "Import data completed.";
                        lblLoading.Visible = false;
                    }
                    else if (dialogResult == DialogResult.No)
                    {
                        _log.WriteLog("User press button No ==> exit");
                        MessageBox.Show("User press button No ==> exit", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                _log.WriteLog("Error RunService(), " + ex.Message);
                MessageBox.Show("Error RunService(), " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Insert_SMSMessage(List<ObjInsertSMSSaoKe> listObjs, string crd_brn)
        {
            int rowNum = 0;
            if (listObjs != null && listObjs.Count > 0)
            {
                int result = 0;
                int count = 0;
                foreach (ObjInsertSMSSaoKe obj in listObjs)
                {
                    rowNum++;
                    result = 0;
                    string message = CreateSMSMessage(obj.getCardNo(), obj.getSmsMonth(), obj.getClosingBalance(),obj.getStatementAmt(), obj.getMinimumPayment()
                        , obj.getDueDate(), obj.getTotalBalIpp(), crd_brn, obj.getCardType(), obj.getVip(), obj.getCifVip(), obj.getIdStatement());

                    if (string.IsNullOrEmpty(message) == false)
                    {
                        obj.setSmsDetail(message);

                        double ipp = 0;
                        if (obj.getTotalBalIpp() != "")
                            ipp = double.Parse(obj.getTotalBalIpp());

                        //closing balance >= 0 hoac closing balance <= -100,000 (du co hon 100k) hoac ipp > 0 
                        Boolean isIdStatement = obj.getIdStatement() != null && !obj.getIdStatement().Equals("") ? true : false;
                        if (double.Parse(obj.getClosingBalance()) >= 0 || double.Parse(obj.getClosingBalance()) <= -100000 ||  ipp > 0 || isIdStatement || obj.getCardType()=="B")
                        {
                            

                            if (obj.getDesMobile() == "khong co" || obj.getActionType() == "D") //ko co so dien thoai
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
                    lblLoading.Text = "Loading " + (50 + (rowNum * 49 / listObjs.Count)) + "%...";
                }
                MessageBox.Show("DONE.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                _log.WriteLog("List import sms is empty. Please check again");
                MessageBox.Show("List import sms is empty. Please check again", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private DataTable GetSmsData(string statementMonth, string cardBrn)
        {
            DataTable table = new DataTable();
            try
            {
                if (chkMCW.Checked)
                    table = DataAccess.GetSmsDataMCW(statementMonth, cardBrn);
                else
                    table = DataAccess.GetSmsData(statementMonth, cardBrn);
            }
            catch (Exception ex)
            {
                if (chkMCW.Checked)
                {
                    _log.WriteLog("Error Get_SMS_Nhac_No_Sao_Ke_68(), " + ex.Message);
                    MessageBox.Show("Error Get_SMS_Nhac_No_Sao_Ke_68(), " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                } else
                {
                    _log.WriteLog("Error Get_SMS_Nhac_No_Sao_Ke(), " + ex.Message);
                    MessageBox.Show("Error Get_SMS_Nhac_No_Sao_Ke(), " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                    
            }
            return table;
        }

        private string CreateSMSMessage(string p_pan, string p_settleDay, string p_closingBal, string p_statementAmt
                                    , string p_miniPay, string p_dueDay, string p_tol_bal_ipp
                                    , string p_crd_brn, string P_crd_type, string p_vip_card, string p_vip_cif, string p_ID_statement)
        {
            try
            {
                string SCBPhone = "";
                string last4Digits = p_pan.Substring(12, 4); //cat 4 so duoi the
                string settleDay = p_settleDay.Substring(4, 2) + "/" + p_settleDay.Substring(2, 2);
                string dueDay = p_dueDay.Substring(6, 2) + "/" + p_dueDay.Substring(4, 2);

                double clsBal = double.Parse(p_closingBal);
                double mini = double.Parse(p_miniPay);

                double statementAmt = double.Parse(string.IsNullOrEmpty(p_statementAmt) ? "0" : p_statementAmt);

                /*
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
                */
                string sCloBal = string.Format("{0:#,##0.##}", clsBal);
                string sMiniPayment = string.Format("{0:#,##0.##}", mini);
                string sStatementAmt = string.Format("{0:#,##0.##}", statementAmt);

                string smsMessage = "";

                if (p_vip_card == "Y" || p_vip_cif == "Y")
                    SCBPhone = "1800545438";
                else
                    SCBPhone = "19006538";

                if(p_crd_brn.Equals("MC") && P_crd_type.Equals("W"))
                {
                    if (statementAmt < 0)
                    {
                        double statementAmt_1 = -1 * statementAmt;
                        string statementAmt_2 = string.Format("{0:#,##0.##}", statementAmt_1);
                        smsMessage = "Cam on Quy khach da su dung the SCB " + last4Digits + "\nSo DU CO trong the den 25/" + settleDay + ":" + statementAmt_2 + "VND";

                    }
                    else
                    {
                        if (statementAmt == 0)
                        {
                            if (clsBal > 0 && p_ID_statement != null && !p_ID_statement.Trim().Equals(""))
                                smsMessage = "Sao ke the " + last4Digits + "\nDu no den 25/" + settleDay + ": " + sCloBal + "VND";
                            else
                                smsMessage = "Cam on Quy khach da su dung va thanh toan the SCB " + last4Digits + "\nDu no den 25/" + settleDay + ": 0VND.";
                        }
                        else //statementAmt > 0
                        {
                            smsMessage = "Sao ke the " + last4Digits + " den 25/" + settleDay + "\nTT toan bo: " + sStatementAmt + "VND\nTT toi thieu "
                                                    + sMiniPayment + "VND\nHan TT " + dueDay;
                            
                        }
                    }
                }
                else
                {
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
                                smsMessage = "Sao ke the " + last4Digits + " den 15/" + settleDay + "\nTT toan bo: " + sCloBal + "VND\nTT toi thieu "
                                                    + sMiniPayment + "VND\nHan TT " + dueDay;
                            else //MC
                                smsMessage = "Sao ke the " + last4Digits + " den 25/" + settleDay + "\nTT toan bo: " + sCloBal + "VND\nTT toi thieu "
                                                    + sMiniPayment + "VND\nHan TT " + dueDay;
                        }
                    }
                }

                if (p_ID_statement != null && !p_ID_statement.Trim().Equals("")) 
                    smsMessage = smsMessage + "\nChi tiet: https://card.scb.com.vn/skt/skt.html?id=" + p_ID_statement;

                //smsMessage = smsMessage.PadRight(160, ' ') + "\nTin nhan nay thay cho tin nhan sao ke ngay 08/01/2020. \nVui long bo qua vi su bat tien nay";

                return smsMessage;

            }
            catch (Exception ex)
            {
                _log.WriteLog("Error CreateSMSMessage(), " + ex.Message);
                MessageBox.Show("Error CreateSMSMessage(), " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return "";
            }
        }

        private void BtnExportToExcel_Click(object sender, EventArgs e)
        {
            /*
            try
            {
                string cardBrn = tboxCardBrn.Text;
                string statementMonth = tboxStatementMonth.Text;
                _log.WriteLog("cardBrn: " + cardBrn + ", statementMonth: " + statementMonth + ", SMS_TYPE: " + SMS_TYPE);
                if (string.IsNullOrEmpty(cardBrn) || string.IsNullOrEmpty(statementMonth))
                {
                    MessageBox.Show("Invalid condition!!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                lblLoading.Text = "Loading 10%.... ";
                lblLoading.Visible = true;
                DataTable dataTable = DataAccess.getListSMSExportThongBaoSaoKe(cardBrn, statementMonth, SMS_TYPE);
                lblLoading.Text = "Loading 40%... ";
                if (dataTable.Rows.Count > 0)
                {
                    using (Syncfusion.XlsIO.ExcelEngine excelEngine = new Syncfusion.XlsIO.ExcelEngine())
                    {
                        Syncfusion.XlsIO.IApplication application = excelEngine.Excel;
                        application.DefaultVersion = Syncfusion.XlsIO.ExcelVersion.Excel2016;
                        Syncfusion.XlsIO.IWorkbook workbook = application.Workbooks.Create(1);
                        Syncfusion.XlsIO.IWorksheet worksheet = workbook.Worksheets[0];

                        worksheet.ImportDataTable(dataTable, true, 1, 1, true);
                        lblLoading.Text = "Loading 60%... ";
                        worksheet.UsedRange.AutofitColumns();
                        lblLoading.Text = "Loading 80%... ";
                        String pathExport = System.Configuration.ConfigurationManager.AppSettings["PATH_EXPORT"].ToString();
                        if (!System.IO.Directory.Exists(pathExport))
                            System.IO.Directory.CreateDirectory(pathExport);
                        String fileNameExport = "EXPORT_THONG_BAO_SAO_KE_" + cardBrn + "_" + statementMonth + ".xlsx";

                        workbook.SaveAs(pathExport + "\\" + fileNameExport);

                        //--------------------------------------
                        Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();

                        if (xlApp == null)
                        {
                            MessageBox.Show("Excel is not properly installed!!");
                            return;
                        }
                        xlApp.DisplayAlerts = false;
                        Microsoft.Office.Interop.Excel.Workbook xlWorkBook = xlApp.Workbooks.Open(pathExport + "\\" + fileNameExport, 0, false, 5, "", "", false, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "", true, false, 0, true, false, false);
                        Microsoft.Office.Interop.Excel.Worksheet worksheets = (Microsoft.Office.Interop.Excel.Worksheet)xlWorkBook.Worksheets[2];//(Microsoft.Office.Interop.Excel.Worksheet)xlApp.ActiveWorkbook.Worksheets[2];
                        worksheets.Delete();
                        xlApp.DisplayAlerts = true;
                        lblLoading.Text = "Loading 99%... ";
                        xlWorkBook.Save();
                        xlWorkBook.Close();

                        System.Runtime.InteropServices.Marshal.ReleaseComObject(worksheets);
                        System.Runtime.InteropServices.Marshal.ReleaseComObject(xlWorkBook);
                        System.Runtime.InteropServices.Marshal.ReleaseComObject(xlApp);
                        lblLoading.Text = "Export completed."; 
                        MessageBox.Show("Excel file created , you can find the file " + pathExport + "\\" + fileNameExport, "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        lblLoading.Visible = false;
                    }
                }
                else
                {
                    MessageBox.Show("List export to excel is empty, please check again !", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    _log.WriteLog("List export to excel is empty, please check again !");
                }
            }
            catch (Exception ex)
            {
                _log.WriteLog("Error BtnExportToExcel(), " + ex.Message);
                MessageBox.Show("Error BtnExportToExcel(), " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            */
        }
        /*
        if (table.Rows.Count > 0)
        {
            Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();

            if (xlApp == null)
            {
                MessageBox.Show("Excel is not properly installed!!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Microsoft.Office.Interop.Excel.Workbook xlWorkBook;
            Microsoft.Office.Interop.Excel.Worksheet xlWorkSheet;
            object misValue = System.Reflection.Missing.Value;

            xlWorkBook = xlApp.Workbooks.Add(misValue);
            xlWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);

            int colNum = 0;
            foreach (DataColumn col in table.Columns)
            {
                colNum++;
                xlWorkSheet.Cells[1, colNum] = col.ColumnName;
            }

            for (int i = 0; i < table.Rows.Count; i++)
            {
                for (int j = 0; j < table.Columns.Count; j++)
                {
                    xlWorkSheet.Cells[i + 2, j + 1] = table.Rows[i][j].ToString();
                }
                lblLoading.Text = "Loading " + ((i+1) * 100 / table.Rows.Count) + "%...";
            }

            /*
            int rowNum = 1;
            xlWorkSheet.Cells[rowNum, 1] = "ID";
            xlWorkSheet.Cells[rowNum, 2] = "SMS_TYPE";
            xlWorkSheet.Cells[rowNum, 3] = "SMS_DETAIL";
            xlWorkSheet.Cells[rowNum, 4] = "DEST_MOBILE";
            xlWorkSheet.Cells[rowNum, 5] = "GET_TRANS_DATETIME";
            xlWorkSheet.Cells[rowNum, 6] = "INSERT_TRANS_DATETIME";
            xlWorkSheet.Cells[rowNum, 7] = "PAN";
            xlWorkSheet.Cells[rowNum, 8] = "CARD_BRN";
            xlWorkSheet.Cells[rowNum, 9] = "CARD_TYPE";
            xlWorkSheet.Cells[rowNum, 10] = "SMS_MONTH";
            xlWorkSheet.Cells[rowNum, 11] = "CLOSING_BALANCE";
            xlWorkSheet.Cells[rowNum, 12] = "DUE_DATE";
            xlWorkSheet.Cells[rowNum, 13] = "MINIMUM_PAYMENT";
            xlWorkSheet.Cells[rowNum, 14] = "ACTION_TYPE";
            xlWorkSheet.Cells[rowNum, 15] = "TOL_BAL_IPP";
            xlWorkSheet.Cells[rowNum, 16] = "VIP";
            xlWorkSheet.Cells[rowNum, 17] = "CIF_VIP";
            xlWorkSheet.Cells[rowNum, 18] = "CARD_NO";
            xlWorkSheet.Cells[rowNum, 19] = "LOC";
            xlWorkSheet.Cells[rowNum, 20] = "ID_STATEMENT";


            foreach (ObjInsertSMSSaoKe obj in listObjs)
            {
                rowNum++;
                xlWorkSheet.Cells[rowNum, 1] = obj.getId();
                xlWorkSheet.Cells[rowNum, 2] = obj.getSmsType();
                xlWorkSheet.Cells[rowNum, 3] = obj.getSmsDetail();
                xlWorkSheet.Cells[rowNum, 4] = obj.getDesMobile();
                xlWorkSheet.Cells[rowNum, 5] = obj.getDateTime();
                xlWorkSheet.Cells[rowNum, 6] = obj.getInsertTransDateTime();
                xlWorkSheet.Cells[rowNum, 7] = obj.getPan();
                xlWorkSheet.Cells[rowNum, 8] = obj.getCardBrn();
                xlWorkSheet.Cells[rowNum, 9] = obj.getCardType();
                xlWorkSheet.Cells[rowNum, 10] = obj.getSmsMonth();
                xlWorkSheet.Cells[rowNum, 11] = obj.getClosingBalance();
                xlWorkSheet.Cells[rowNum, 12] = obj.getDueDate();
                xlWorkSheet.Cells[rowNum, 13] = obj.getMinimumPayment();
                xlWorkSheet.Cells[rowNum, 14] = obj.getActionType();
                xlWorkSheet.Cells[rowNum, 15] = obj.getTotalBalIpp();
                xlWorkSheet.Cells[rowNum, 16] = obj.getVip();
                xlWorkSheet.Cells[rowNum, 17] = obj.getCifVip();
                xlWorkSheet.Cells[rowNum, 18] = obj.getCardNo();
                xlWorkSheet.Cells[rowNum, 19] = obj.getLoc();
                xlWorkSheet.Cells[rowNum, 20] = obj.getIdStatement();

                lblLoading.Text = "Loading " + rowNum * 100 / listObjs.Count + "%...";
            }


            String pathExport = System.Configuration.ConfigurationManager.AppSettings["PATH_EXPORT"].ToString();
            if (!System.IO.Directory.Exists(pathExport))
                System.IO.Directory.CreateDirectory(pathExport);
            String fileNameExport = "EXPORT_THONG_BAO_SAO_KE_" + cardBrn + "_" + statementMonth + ".xls";

            xlWorkBook.SaveAs(pathExport + "\\" + fileNameExport, Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
            xlWorkBook.Close(true, misValue, misValue);
            xlApp.Quit();

            System.Runtime.InteropServices.Marshal.ReleaseComObject(xlWorkSheet);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(xlWorkBook);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(xlApp);
            lblLoading.Text = "Export excel file completed.";
            MessageBox.Show("Excel file created , you can find the file " + pathExport + "\\" + fileNameExport, "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);


        }
        else
        {
            MessageBox.Show("List export to excel is empty, please check again !","Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            _log.WriteLog("List export to excel is empty, please check again !");
        }
        lblLoading.Visible = false;
    }
*/
    }
}
