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
    public partial class frmInsertSMSDueDate : Form
    {
        private SMSNhacNoDueDateLogWritter _log = new SMSNhacNoDueDateLogWritter();
        public static string IDALERT = "MASTER_CARD_ALERT";
        public static string SMS_TYPE = "DEBT02";

        public frmInsertSMSDueDate()
        {
            InitializeComponent();
            //txbStatementMonth.Text = DateTime.Now.AddMonths(-1).ToString("yyyyMM");
            //txbStatementMonth.Text = DateTime.Now.ToString("yyyyMM");
            //DateTime dueDateDefaultValue = new DateTime(DateTime.Now.Year, DateTime.Now.Month - 1, 25).AddDays(15);
            //txbDueDate.Text = dueDateDefaultValue.ToString("yyyyMMdd");
        }

        private void btnInsertSMS_Click(object sender, EventArgs e)
        {
            string p_settl_month = txbStatementMonth.Text;
            string p_due_date = txbDueDate.Text;

            DataTable table = new DataTable();
            try
            {
                _log.WriteLog("----------------Begin Process-----------------");
                lblLoading.Text = "Loading...";
                lblLoading.Visible = true;
                table.Rows.Clear();
                table = GetSmsDataDueDate(p_settl_month, p_due_date);
                int rowNum = 0;
                if (table.Rows.Count > 0)
                {
                    DialogResult dialogResult = MessageBox.Show("So luong tin nhan SMS import la " + table.Rows.Count, "Info", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (dialogResult == DialogResult.Yes)
                    {
                        DataAccess.update_Data_Notify_Send_SMS_DueDate(p_settl_month, p_due_date);

                        List<ObjInsertSMSDueDate> listObjs = new List<ObjInsertSMSDueDate>();
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

                            ObjInsertSMSDueDate obj = new ObjInsertSMSDueDate(id, smsType, smsDetail, desMobile
                                , dateTime, insertTransDate, pan, cardBrnData, cardType, smsMonth, closingBal, dueDate
                                , minimumPayment, actType, totBalIpp, vip, cifVip, cardNo, loc, idStatement, statementAmt);

                            listObjs.Add(obj);
                            lblLoading.Text = "Loading " + (rowNum * 50 / listObjs.Count) + "%...";
                        }
                        Insert_SMSMessage(listObjs);
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
                else
                {
                    MessageBox.Show("Data is empty", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    _log.WriteLog("Data is empty");
                }
            }
            catch (Exception ex)
            {
                _log.WriteLog("Error RunService(), " + ex.Message);
                MessageBox.Show("Error RunService(), " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private DataTable GetSmsDataDueDate(string statementMonth, string dueDate)
        {
            DataTable table = new DataTable();
            try
            {
                if (chkMCW.Checked)
                    table = DataAccess.GetSmsDataDueDateMCW(statementMonth, dueDate);
                else
                    table = DataAccess.GetSmsDataDueDate(statementMonth, dueDate);
                
                 
            }
            catch (Exception ex)
            {
                if (chkMCW.Checked)
                {
                    _log.WriteLog("Error Get_SMS_Nhac_No_Due_Date_68(), " + ex.Message);
                    MessageBox.Show("Error Get_SMS_Nhac_No_Due_Date_68(), " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                } else
                {
                    _log.WriteLog("Error Get_SMS_Nhac_No_Due_Date(), " + ex.Message);
                    MessageBox.Show("Error Get_SMS_Nhac_No_Due_Date(), " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
   
            }
            return table;
        }

        private void Insert_SMSMessage(List<ObjInsertSMSDueDate> listObjs)
        {
            int rowNum = 0;
            if (listObjs != null && listObjs.Count > 0)
            {
                int result = 0;
                foreach (ObjInsertSMSDueDate obj in listObjs)
                {
                    rowNum++;
                    result = 0;
                    string message = CreateSMSMessage(obj.getCardBrn(), obj.getCardType(), obj.getCardNo(), obj.getSmsMonth(), obj.getClosingBalance(),  obj.getStatementAmt(),
                        obj.getMinimumPayment(), obj.getDueDate(), obj.getVip(), obj.getCifVip());

                    if (string.IsNullOrEmpty(message) == false)
                    {

                        obj.setSmsDetail(message);
                        if (obj.getDesMobile() == "khong co" || obj.getActionType().Equals("D")) //ko co so dien thoai
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
                    lblLoading.Text = "Loading " + (50 + (rowNum * 49 / listObjs.Count)) + "%...";
                }

                MessageBox.Show("DONE.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                _log.WriteLog("List import sms is empty. Please check again");
                MessageBox.Show("List import sms is empty. Please check again", "Info", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private string CreateSMSMessage(string brand, string cardType, string pan, string settleDay, string totalClosingBal, string statementAmt
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
                double d_statementAmt = double.Parse(string.IsNullOrEmpty(statementAmt) ? "0" : statementAmt);
                double d_totalMin = double.Parse(totalMiniPay);

                string s_totalCloBal = string.Format("{0:#,##0.##}", d_totalClsBal);
                string s_statementAmt = string.Format("{0:#,##0.##}", d_statementAmt);
                string s_totalMiniPay = string.Format("{0:#,##0.##}", d_totalMin);

                string smsMessage = "";

                if(brand.Equals("MC") && cardType.Equals("W"))
                    smsMessage = "Sao ke the " + pan + " den " + settleDay_f + "\nTT toan bo: " + s_statementAmt + "VND\nTT toi thieu: " + s_totalMiniPay + "VND" + "\nHan TT " + dueDay + "\nVui long bo qua neu da TT\nLH " + SCBPhone;
                else
                    smsMessage = "Sao ke the " + pan + " den " + settleDay_f + "\nTT toan bo: " + s_totalCloBal + "VND\nTT toi thieu: " + s_totalMiniPay + "VND" + "\nHan TT " + dueDay + "\nVui long bo qua neu da TT\nLH " + SCBPhone;

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
                string p_settl_month = txbStatementMonth.Text;
                string p_due_date = txbDueDate.Text;
                _log.WriteLog("due Date: " + p_due_date + ", statementMonth: " + p_settl_month + ", SMS_TYPE: " + SMS_TYPE);
                if (string.IsNullOrEmpty(p_settl_month) || string.IsNullOrEmpty(p_due_date))
                {
                    MessageBox.Show("Invalid condition!!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                lblLoading.Text = "Loading 10%.... ";
                lblLoading.Visible = true;
                DataTable dataTable = DataAccess.getListSMSExportNhacNoDueDate(p_due_date, p_settl_month, SMS_TYPE);
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
                        String fileNameExport = "EXPORT_NHAC_NO_DUE_DATE_" + p_due_date + ".xlsx";
                     
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
                        Microsoft.Office.Interop.Excel.Worksheet worksheets = (Microsoft.Office.Interop.Excel.Worksheet)xlApp.ActiveWorkbook.Worksheets[2];

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

        private void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        ///-------------------------------
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
                lblLoading.Text = "Loading " + ((i + 1) * 100 / table.Rows.Count) + "%...";
            }
            /*
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
            foreach (ObjInsertSMSDueDate obj in listObjs)
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
            String fileNameExport = "EXPORT_NHAC_NO_DUE_DATE_" + p_due_date + ".xls";

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
            MessageBox.Show("List export to excel is empty, please check again !", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            _log.WriteLog("List export to excel is empty, please check again !");
        }
        lblLoading.Visible = false;
    }
    */
    }

}
