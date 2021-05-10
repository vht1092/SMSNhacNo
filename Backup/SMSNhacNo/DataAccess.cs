using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OracleClient;


namespace SMSNhacNo
{
    class DataAccess
    {
        private static SMSNhacNoSaoKeLogWriter _logSMSNhacNoSaoKe = new SMSNhacNoSaoKeLogWriter();
        private static SMSNhacNoDueDateLogWritter _logSMSNhacNoDueDate = new SMSNhacNoDueDateLogWritter();

        public static DataTable GetSMSNhacNoSaoKeDB(string statementMonth, string cardBrn)
        {
            DataTable data = new DataTable();
            OracleConnection conn = null;
            try
            {
                conn = OracleDBConnection.OpenConnectionDB("CW_IM_STAN");
                //OracleCommand cmd = new OracleCommand("Get_Reminder_Payment_1_IPP", connection);
                //////OracleCommand cmd = new OracleCommand("Get_Reminder_Payment_1_IPP_E", connection); //loai bo loc               
                OracleCommand cmd = new OracleCommand("Get_Reminder_Payment_1_IPP_T", conn);

                cmd.CommandType = CommandType.StoredProcedure;

                OracleParameter settl_month = new OracleParameter("settl_month", statementMonth);
                settl_month.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(settl_month);

                OracleParameter crd_brn = new OracleParameter("p_crd_brn", cardBrn);
                crd_brn.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(crd_brn);

                OracleParameter results = new OracleParameter("sys_cursor", OracleType.Cursor);
                results.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(results);

                OracleDataAdapter da = new OracleDataAdapter(cmd);

                da.Fill(data);
                if (conn != null)
                    conn.Close();
                return data;
            }
            catch (Exception ex)
            {
                _logSMSNhacNoSaoKe.WriteLog("Error GetReminderPayment1() at DataAccess, " + ex.Message);
                if (conn != null)
                    conn.Close();
                data.Clear();
                return data;
            }
        }

        public static int InsertSMSMessateToEBankGW_2(string idAlert, string mobile, string message,
                                           char msgstat, string smsType)
        {
            OracleConnection connection = null;
            try
            {

                connection = OracleDBConnection.OpenConnectionDB("EBANK_GW");
                OracleCommand cmd = new OracleCommand("SMS_SCB.PROC_INS_MASTERCARD_KM", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                OracleParameter idAlert_p = new OracleParameter("id_alert", idAlert);
                idAlert_p.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(idAlert_p);

                OracleParameter mobile_p = new OracleParameter("mobile", mobile);
                mobile_p.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(mobile_p);

                OracleParameter message_p = new OracleParameter("message", message);
                message_p.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(message_p);

                OracleParameter msgstat_p = new OracleParameter("msgstat", msgstat);
                msgstat_p.Direction = ParameterDirection.Input;
                msgstat_p.OracleType = OracleType.Char;
                cmd.Parameters.Add(msgstat_p);

                OracleParameter mc_sms_type_p = new OracleParameter("mc_sms_type", smsType);
                mc_sms_type_p.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(mc_sms_type_p);

                OracleString rowID;
                int insertedRow = 0;

                insertedRow = cmd.ExecuteOracleNonQuery(out rowID);
                if (connection != null)
                    connection.Close();
                return insertedRow;
            }
            catch (Exception ex)
            {
                _logSMSNhacNoSaoKe.WriteLog("Error InsertSMSMessateToEBankGW_2() at DataAccess, " + ex.Message);
                if (connection != null)
                    connection.Close();
                return 0;
            }
        }

        public static int InsertReminderPayment_1SMSToDW(
                                            string sms_type,
                                            string sms_detail,
                                            string dest_mobile,//long dest_mobile,
                                            DateTime get_trans_datetime,
                                            string pan,
                                            string card_brn,
                                            string card_type,
                                            string sms_date,//long sms_date,
                                            //string sms_time,//long sms_time,
                                            string closing_balance,
                                            double minimum_payment,//long paymented_amt,
                                            string due_date,
                                            string sms_stat)
        {
            OracleConnection connection = null;
            try
            {
                connection = OracleDBConnection.OpenConnectionDB("CW_DW");
                
                OracleCommand cmd = new OracleCommand("fpt.Insert_Reminder_Payment_1_2", connection);
                //OracleCommand cmd = new OracleCommand("fpt.Insert_Reminder_Payment_1", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                OracleParameter sms_type_p = new OracleParameter("sms_type_p", sms_type);
                sms_type_p.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(sms_type_p);

                OracleParameter sms_detail_p = new OracleParameter("sms_detail_p", sms_detail);
                sms_detail_p.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(sms_detail_p);

                OracleParameter dest_mobile_p = new OracleParameter("dest_mobile_p", dest_mobile);
                dest_mobile_p.Direction = ParameterDirection.Input;
                //dest_mobile_p.OracleType = OracleType.Number;
                cmd.Parameters.Add(dest_mobile_p);

                OracleParameter get_trans_datetime_p = new OracleParameter("get_trans_datetime_p", get_trans_datetime);
                get_trans_datetime_p.Direction = ParameterDirection.Input;
                //get_trans_datetime_p.OracleType = OracleType.DateTime;
                cmd.Parameters.Add(get_trans_datetime_p);

                OracleParameter pan_p = new OracleParameter("pan_p", pan);
                pan_p.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(pan_p);

                OracleParameter card_brn_p = new OracleParameter("card_brn_p", card_brn);
                card_brn_p.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(card_brn_p);

                OracleParameter card_type_p = new OracleParameter("card_type_p", card_type);
                card_type_p.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(card_type_p);

                OracleParameter sms_date_p = new OracleParameter("sms_date_p", sms_date);
                sms_date_p.Direction = ParameterDirection.Input;
                //sms_date_p.OracleType = OracleType.Number;
                cmd.Parameters.Add(sms_date_p);

                OracleParameter closing_balance_p = new OracleParameter("closing_balance_p", closing_balance);
                closing_balance_p.Direction = ParameterDirection.Input;
                //sms_date_p.OracleType = OracleType.Number;
                cmd.Parameters.Add(closing_balance_p);

                OracleParameter minimum_payment_p = new OracleParameter("minimum_payment_p", minimum_payment);
                minimum_payment_p.Direction = ParameterDirection.Input;
                //sms_date_p.OracleType = OracleType.Number;
                cmd.Parameters.Add(minimum_payment_p);

                OracleParameter due_date_p = new OracleParameter("due_date_p", due_date);
                due_date_p.Direction = ParameterDirection.Input;
                //sms_date_p.OracleType = OracleType.Number;
                cmd.Parameters.Add(due_date_p);

                OracleParameter sms_stat_p = new OracleParameter("sms_stat_p", sms_stat);
                sms_stat_p.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(sms_stat_p);

                OracleString rowID;
                int insertedRow = 0;

                insertedRow = cmd.ExecuteOracleNonQuery(out rowID);
                if (connection != null)
                    connection.Close();
                return insertedRow;
            }
            catch (Exception ex)
            {
                _logSMSNhacNoSaoKe.WriteLog("Error InsertReminderPayment_1SMSToDW()at DataAccess" + ex.Message);
                if (connection != null)
                    connection.Close();
                return 0;
            }
        }

        public static DataTable GetSMSNhacNoDueDateDB(string statementMonth, string dueDate)
        {
            DataTable data = new DataTable();
            OracleConnection conn = null;
            try
            {
                conn = OracleDBConnection.OpenConnectionDB("CW_IM_STAN");
                //OracleCommand cmd = new OracleCommand("Get_Reminder_Payment_2_IPP", conn);
                //OracleCommand cmd = new OracleCommand("Get_Reminder_Payment_2_IPP_Err", conn);                
                OracleCommand cmd = new OracleCommand("Get_Reminder_Payment_2_IPP_T", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                OracleParameter settl_month = new OracleParameter("settl_month", statementMonth);
                settl_month.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(settl_month);

                OracleParameter due_dt = new OracleParameter("due_dt", dueDate);
                due_dt.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(due_dt);

                OracleParameter results = new OracleParameter("sys_cursor", OracleType.Cursor);
                results.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(results);

                OracleDataAdapter da = new OracleDataAdapter(cmd);

                da.Fill(data);
                if (conn != null)
                    conn.Close();
                return data;
            }
            catch (Exception ex)
            {
                _logSMSNhacNoDueDate.WriteLog("Error GetSMSNhacNoDueDateDB() at DataAccess, " + ex.Message);
                if (conn != null)
                    conn.Close();
                data.Clear();
                return data;
            }

        }

        public static int InsertReminderPayment_2SMSToDW(
                                            string sms_type,
                                            string sms_detail,
                                            string dest_mobile,//long dest_mobile,
                                            DateTime get_trans_datetime,
                                            string pan,
                                            string card_brn,
                                            string card_type,
                                            string sms_date,//long sms_date,
                                            string closing_balance,
                                            string minimum_payment,//long paymented_amt,
                                            string due_date,
                                            string sms_stat
                                            )
        {
            OracleConnection conn = null;
            try
            {
                conn = OracleDBConnection.OpenConnectionDB("CW_DW");
                OracleCommand cmd = new OracleCommand("fpt.Insert_Reminder_Payment_1_2", conn);
                //OracleCommand cmd = new OracleCommand("fpt.Insert_Reminder_Payment_1", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                OracleParameter sms_type_p = new OracleParameter("sms_type_p", sms_type);
                sms_type_p.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(sms_type_p);

                OracleParameter sms_detail_p = new OracleParameter("sms_detail_p", sms_detail);
                sms_detail_p.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(sms_detail_p);

                OracleParameter dest_mobile_p = new OracleParameter("dest_mobile_p", dest_mobile);
                dest_mobile_p.Direction = ParameterDirection.Input;
                //dest_mobile_p.OracleType = OracleType.Number;
                cmd.Parameters.Add(dest_mobile_p);

                OracleParameter get_trans_datetime_p = new OracleParameter("get_trans_datetime_p", get_trans_datetime);
                get_trans_datetime_p.Direction = ParameterDirection.Input;
                //get_trans_datetime_p.OracleType = OracleType.DateTime;
                cmd.Parameters.Add(get_trans_datetime_p);

                OracleParameter pan_p = new OracleParameter("pan_p", pan);
                pan_p.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(pan_p);

                OracleParameter card_brn_p = new OracleParameter("card_brn_p", card_brn);
                card_brn_p.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(card_brn_p);

                OracleParameter card_type_p = new OracleParameter("card_type_p", card_type);
                card_type_p.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(card_type_p);

                OracleParameter sms_date_p = new OracleParameter("sms_date_p", sms_date);
                sms_date_p.Direction = ParameterDirection.Input;
                //sms_date_p.OracleType = OracleType.Number;
                cmd.Parameters.Add(sms_date_p);

                OracleParameter closing_balance_p = new OracleParameter("closing_balance_p", closing_balance);
                closing_balance_p.Direction = ParameterDirection.Input;
                //sms_date_p.OracleType = OracleType.Number;
                cmd.Parameters.Add(closing_balance_p);

                OracleParameter minimum_payment_p = new OracleParameter("minimum_payment_p", minimum_payment);
                minimum_payment_p.Direction = ParameterDirection.Input;
                //sms_date_p.OracleType = OracleType.Number;
                cmd.Parameters.Add(minimum_payment_p);

                OracleParameter due_date_p = new OracleParameter("due_date_p", due_date);
                due_date_p.Direction = ParameterDirection.Input;
                //sms_date_p.OracleType = OracleType.Number;
                cmd.Parameters.Add(due_date_p);

                OracleParameter sms_stat_p = new OracleParameter("sms_stat_p", sms_stat);
                sms_stat_p.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(sms_stat_p);

                OracleString rowID;
                int insertedRow = 0;

                insertedRow = cmd.ExecuteOracleNonQuery(out rowID);
                if (conn != null)
                    conn.Close();
                return insertedRow;
            }
            catch (Exception ex)
            {
                _logSMSNhacNoDueDate.WriteLog("Error InsertReminderPayment_2SMSToDW() at DataAccess, " + ex.Message);
                if (conn != null)
                    conn.Close();
                return 0;
            }
        }

        public static DataTable GetSmsData(string statementMonth, string cardBrn)
        {
            DataTable data = new DataTable();
            OracleConnection conn = null;
            try
            {
                conn = OracleDBConnection.OpenConnectionDB("CW_IM_STAN");
                OracleCommand cmd = new OracleCommand("GET_SMS_NHAC_NO_SAO_KE", conn);

                cmd.CommandType = CommandType.StoredProcedure;

                OracleParameter settl_month = new OracleParameter("p_settl_month", statementMonth);
                settl_month.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(settl_month);

                OracleParameter crd_brn = new OracleParameter("p_card_brn", cardBrn);
                crd_brn.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(crd_brn);

                OracleParameter results = new OracleParameter("sys_cursor", OracleType.Cursor);
                results.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(results);

                OracleDataAdapter da = new OracleDataAdapter(cmd);

                da.Fill(data);
                if (conn != null)
                    conn.Close();
                return data;
            }
            catch (Exception ex)
            {
                _logSMSNhacNoSaoKe.WriteLog("Error GET_SMS_NHAC_NO_SAO_KE at DataAccess, " + ex.Message);
                if (conn != null)
                    conn.Close();
                data.Clear();
                return data;
            }
        }

        public static int Insert_SMS_Nhac_No_Sao_Ke(ObjInsertSMSSaoKe obj)
        {
            OracleConnection conn = new OracleConnection();
            try
            {
                conn = OracleDBConnection.OpenConnectionDB("CW_DW");
                string str = "Insert into SMS_NHAC_NO VALUES " + "('"+obj.getId()+"','"+obj.getSmsType()+"','"+obj.getSmsDetail()+"','"+obj.getDesMobile()+"',"+obj.getDateTime()+","+obj.getInsertTransDateTime() + ",'"+obj.getPan()+"','"+obj.getCardBrn()+"','"+obj.getCardType()+"',"+obj.getSmsMonth()+","+obj.getClosingBalance()+","+obj.getDueDate()+","+obj.getMinimumPayment() + ",'"+obj.getActionType()+"',"+obj.getTotalBalIpp()+",'"+obj.getVip()+"','"+obj.getCifVip()+"','"+obj.getCardNo()+"',"+obj.getLoc()+",'"+obj.getIdStatement()+"'"+")";
                OracleCommand cmd = new OracleCommand(str, conn);
                cmd.CommandType = CommandType.Text;
                int rowsUpdated = cmd.ExecuteNonQuery();
                if (rowsUpdated == 0)
                    _logSMSNhacNoSaoKe.WriteLog("Insert record table SMS_NHAC_NO failed because " + rowsUpdated);
                else
                    _logSMSNhacNoSaoKe.WriteLog("Insert record table SMS_NHAC_NO");

                conn.Close();
                return rowsUpdated;
            }
            catch (Exception ex)
            {
                _logSMSNhacNoSaoKe.WriteLog("Insert record DB SMS_NHAC_NO failed because " + ex.Message);
                if (conn != null)
                    conn.Close();
                return -1;
            }
        }

        public static List<ObjInsertSMSSaoKe> getListSMSNhacNoSaoKe(string p_cardBrn, string p_stateMonth, string p_smsType)
        {
            List<ObjInsertSMSSaoKe> listObjs = new List<ObjInsertSMSSaoKe>();
            OracleConnection conn = new OracleConnection();
            try
            {
                conn = OracleDBConnection.OpenConnectionDB("CW_DW");
                string str = @"select ID, SMS_TYPE, SMS_DETAIL, DEST_MOBILE, GET_TRANS_DATETIME
                                      , INSERT_TRANS_DATETIME, PAN, CARD_BRN, CARD_TYPE, SMS_MONTH
                                      , CLOSING_BALANCE, DUE_DATE, MINIMUM_PAYMENT, ACTION_TYPE
                                      , TOL_BAL_IPP, VIP, CIF_VIP, CARD_NO, LOC, ID_STATEMENT
                               from SMS_NHAC_NO 
                               where ACTION_TYPE = 'N' 
                                     and SMS_MONTH = " + p_stateMonth + @"
                                     and CARD_BRN = '" + p_cardBrn + @"'
                                     and SMS_TYPE = '" + p_smsType + "'";

                OracleCommand cmd = new OracleCommand(str, conn);
                cmd.CommandType = CommandType.Text;
                OracleDataReader objReader = cmd.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        string id = "";
                        if (objReader["ID"] != null)
                            id = objReader["ID"].ToString().Trim();

                        string smsType = "";
                        if (objReader["SMS_TYPE"] != null)
                            smsType = objReader["SMS_TYPE"].ToString().Trim();

                        string smsDetail = "";
                        if (objReader["SMS_DETAIL"] != null)
                            smsDetail = objReader["SMS_DETAIL"].ToString().Trim();

                        string destMobile = "";
                        if (objReader["DEST_MOBILE"] != null)
                            destMobile = objReader["DEST_MOBILE"].ToString().Trim();

                        string dateTime = "";
                        if (objReader["GET_TRANS_DATETIME"] != null)
                            dateTime = objReader["GET_TRANS_DATETIME"].ToString().Trim();

                        string insertTransDate = "";
                        if (objReader["INSERT_TRANS_DATETIME"] != null)
                            insertTransDate = objReader["INSERT_TRANS_DATETIME"].ToString().Trim();

                        string pan = "";
                        if (objReader["PAN"] != null)
                            pan = objReader["PAN"].ToString().Trim();

                        string cardBrn = "";
                        if (objReader["CARD_BRN"] != null)
                            cardBrn = objReader["CARD_BRN"].ToString().Trim();

                        string cardType = "";
                        if (objReader["CARD_TYPE"] != null)
                            cardType = objReader["CARD_TYPE"].ToString().Trim();

                        string smsMonth = "";
                        if (objReader["SMS_MONTH"] != null)
                            smsMonth = objReader["SMS_MONTH"].ToString().Trim();

                        string closingBal = "";
                        if (objReader["CLOSING_BALANCE"] != null)
                            closingBal = objReader["CLOSING_BALANCE"].ToString().Trim();

                        string dueDate = "";
                        if (objReader["DUE_DATE"] != null)
                            dueDate = objReader["DUE_DATE"].ToString().Trim();

                        string minPayment = "";
                        if (objReader["MINIMUM_PAYMENT"] != null)
                            minPayment = objReader["MINIMUM_PAYMENT"].ToString().Trim();

                        string actionType = "";
                        if (objReader["ACTION_TYPE"] != null)
                            actionType = objReader["ACTION_TYPE"].ToString().Trim();

                        string tolBallIpp = "";
                        if (objReader["TOL_BAL_IPP"] != null)
                            tolBallIpp = objReader["TOL_BAL_IPP"].ToString().Trim();

                        string vip = "";
                        if (objReader["VIP"] != null)
                            vip = objReader["VIP"].ToString().Trim();

                        string cifVip = "";
                        if (objReader["CIF_VIP"] != null)
                            cifVip = objReader["CIF_VIP"].ToString().Trim();

                        string cardNo = "";
                        if (objReader["CARD_NO"] != null)
                            cardNo = objReader["CARD_NO"].ToString().Trim();

                        string loc = "";
                        if (objReader["LOC"] != null)
                            loc = objReader["LOC"].ToString().Trim();

                        string idStatement = "";
                        if (objReader["ID_STATEMENT"] != null)
                            idStatement = objReader["ID_STATEMENT"].ToString().Trim();

                        ObjInsertSMSSaoKe obj = new ObjInsertSMSSaoKe(id, smsType, smsDetail, destMobile,
                            dateTime, insertTransDate, pan, cardBrn, cardType, smsMonth, closingBal, dueDate,
                            minPayment, actionType, tolBallIpp, vip, cifVip, cardNo, loc, idStatement);

                        listObjs.Add(obj);
                    }
                }
                conn.Close();
            }
            catch (Exception ex)
            {
                _logSMSNhacNoSaoKe.WriteLog("Get data DB SMS_NHAC_NO failed because " + ex.Message);
                if (conn != null)
                    conn.Close();
                return null;
            }

            return listObjs;
        }

        public static void UpdateStatusSMSNhacNoSaoKe(string id, string cardBrn, string stateMonth, string smsType, string status)
        {
            OracleConnection conn = new OracleConnection(); // C#
            try
            {
                conn = OracleDBConnection.OpenConnectionDB("CW_DW");
                string str = @"update SMS_NHAC_NO set ACTION_TYPE = '" + status + @"', INSERT_TRANS_DATETIME = TO_CHAR(SYSDATE, 'YYYYMMDDHH24MISS')
                               where id = '" + id + @"' 
                                     and CARD_BRN = '" + cardBrn + @"'
                                     and SMS_MONTH = " + stateMonth + @" 
                                     and SMS_TYPE = '" + smsType + "'";

                OracleCommand cmd = new OracleCommand(str, conn);
                cmd.CommandType = CommandType.Text;
                int rowsUpdated = cmd.ExecuteNonQuery();
                if (rowsUpdated == 0)
                    _logSMSNhacNoSaoKe.WriteLog("UpdateStatusSMSNhacNoSaoKe: Update record DB SMS_NHAC_NO failed because " + rowsUpdated);
                else
                    _logSMSNhacNoSaoKe.WriteLog("UpdateStatusSMSNhacNoSaoKe: Update stattus record DB SMS_NHAC_NO");

                conn.Close();
            }
            catch (Exception ex)
            {
                _logSMSNhacNoSaoKe.WriteLog("UpdateStatusSMSNhacNoSaoKe: Update record DB SMS_NHAC_NO failed because " + ex.Message);
                if (conn != null)
                    conn.Close();
            }
        }

        public static DataTable GetSmsDataDueDate(string settlMonth, string dueDate)
        {
            DataTable data = new DataTable();
            OracleConnection conn = null;
            try
            {
                conn = OracleDBConnection.OpenConnectionDB("CW_IM_STAN");
                OracleCommand cmd = new OracleCommand("GET_SMS_NHAC_NO_DUE_DATE", conn);

                cmd.CommandType = CommandType.StoredProcedure;

                OracleParameter settl_month = new OracleParameter("p_settl_month", settlMonth);
                settl_month.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(settl_month);

                OracleParameter due_date = new OracleParameter("p_due_date", dueDate);
                due_date.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(due_date);

                OracleParameter results = new OracleParameter("sys_cursor", OracleType.Cursor);
                results.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(results);

                OracleDataAdapter da = new OracleDataAdapter(cmd);

                da.Fill(data);
                if (conn != null)
                    conn.Close();
                return data;
            }
            catch (Exception ex)
            {
                _logSMSNhacNoSaoKe.WriteLog("Error GET_SMS_NHAC_NO_DUE_DATE at DataAccess, " + ex.Message);
                if (conn != null)
                    conn.Close();
                data.Clear();
                return data;
            }
        }

        public static int Insert_SMS_Nhac_No_Due_Date(ObjInsertSMSDueDate obj)
        {
            OracleConnection conn = new OracleConnection();
            try
            {
                conn = OracleDBConnection.OpenConnectionDB("CW_DW");
                string str = "Insert into SMS_NHAC_NO VALUES " + "('" + obj.getId() + "','" + obj.getSmsType() + "','" + obj.getSmsDetail() + "','" + obj.getDesMobile() + "'," + obj.getDateTime() + "," + obj.getInsertTransDateTime() + ",'" + obj.getPan() + "','" + obj.getCardBrn() + "','" + obj.getCardType() + "'," + obj.getSmsMonth() + "," + obj.getClosingBalance() + "," + obj.getDueDate() + "," + obj.getMinimumPayment() + ",'" + obj.getActionType() + "'," + obj.getTotalBalIpp() + ",'" + obj.getVip() + "','" + obj.getCifVip() + "','" + obj.getCardNo() + "'," + obj.getLoc() + ",'" + obj.getIdStatement() + "'" + ")";
                OracleCommand cmd = new OracleCommand(str, conn);
                cmd.CommandType = CommandType.Text;
                int rowsUpdated = cmd.ExecuteNonQuery();
                if (rowsUpdated == 0)
                    _logSMSNhacNoSaoKe.WriteLog("Insert record table SMS_NHAC_NO failed because " + rowsUpdated);
                else
                    _logSMSNhacNoSaoKe.WriteLog("Insert record table SMS_NHAC_NO");

                conn.Close();
                return rowsUpdated;
            }
            catch (Exception ex)
            {
                _logSMSNhacNoSaoKe.WriteLog("Insert record DB SMS_NHAC_NO failed because " + ex.Message);
                if (conn != null)
                    conn.Close();
                return -1;
            }
        }

    }
}
