using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using System.Data.OracleClient;
using TUONGHD;
using System.Windows.Forms;

namespace SMSNhacNo
{
    class OracleDBConnection
    {
        static private string _key = "HanVien";
        public OracleDBConnection()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public static OracleConnection OpenConnection(string DBName)
        {
            string stConnect = System.Configuration.ConfigurationManager.AppSettings[DBName].ToString();

            stConnect = EnCode.DeCodeToString(stConnect, _key);

            //string fcc_test = "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=192.168.49.5)(PORT=1521))(CONNECT_DATA=(SID=fptcv)));User ID=VIENTH;Password=Vien123";
            //string fcc_live = "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=192.168.42.12)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=fcscb)));User ID=VIENTH;Password=Vien123";
            //string CW_DW = "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=192.168.46.14)(PORT=3020))(CONNECT_DATA=(SID=DW)));User ID=ccps;Password=cwccpsdw";

            //string s = EnCode.EnCodeToString(a, _key);

            OracleConnection sqlConnect;
            try
            {
                sqlConnect = new OracleConnection(stConnect);
                sqlConnect.Open();
                return sqlConnect;
            }
            catch
            {
                return null;
            }
        }

        public static OracleConnection OpenConnectionDB(string DBName)
        {
            string stConnect = System.Configuration.ConfigurationManager.AppSettings[DBName].ToString();


            OracleConnection sqlConnect;
            try
            {
                sqlConnect = new OracleConnection(stConnect);
                sqlConnect.Open();
                return sqlConnect;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static bool CloseConnection(OracleConnection sqlConnect)
        {
            try
            {
                sqlConnect.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
