using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace SMSNhacNo
{
    class SMSNhacNoSaoKeLogWriter
    {
        public bool WriteLog(string LogMessage)
        {
            bool Status = false;
            string LogDirectory = Directory.GetCurrentDirectory() + "//Log//SMSNhacNoSaoKeLogWriter";//ConfigurationManager.AppSettings["LogDirectory"].ToString(); //"D://logTest";

            DateTime CurrentDateTime = DateTime.Now;
            string CurrentDateTimeString = CurrentDateTime.ToString();
            string logLine = BuildLogLine(CurrentDateTime, LogMessage);
            LogDirectory = LogDirectory + "Log_" + LogFileName(DateTime.Now) + ".txt";

            lock (typeof(SMSNhacNoSaoKeLogWriter))
            {
                StreamWriter oStreamWriter = null;
                try
                {
                    oStreamWriter = new StreamWriter(LogDirectory, true);
                    oStreamWriter.WriteLine(logLine);
                    Status = true;
                }
                catch
                {

                }
                finally
                {
                    if (oStreamWriter != null)
                    {
                        oStreamWriter.Close();
                    }
                }
            }
            return Status;
        }

        private string BuildLogLine(DateTime CurrentDateTime, string LogMessage)
        {
            StringBuilder loglineStringBuilder = new StringBuilder();
            loglineStringBuilder.Append(LogFileEntryDateTime(CurrentDateTime));
            loglineStringBuilder.Append(" \t");
            loglineStringBuilder.Append(LogMessage);
            return loglineStringBuilder.ToString();
        }


        public string LogFileEntryDateTime(DateTime CurrentDateTime)
        {
            return CurrentDateTime.ToString("dd-MM-yyyy HH:mm:ss");
        }


        private string LogFileName(DateTime CurrentDateTime)
        {
            return CurrentDateTime.ToString("dd_MM_yyyy");
        }
    }
}
