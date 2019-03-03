using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Net;
using System.IO;

namespace GRS_SYNC
{
    class Program
    {
        [DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        const int SW_HIDE = 0;
        const int SW_SHOW = 5;

        static double GENENTRY, COMPGENENTRY, SCHENTRYBOOKED, SCHENTRYARRIVED, FOODBOOKED, FOODSERVED, TOTGENENTRY, TOTCOMPGENENTRY;
        static double SWLKISSCOUNT, SWLKPENCOUNT, SWLKRECCOUNT, SWLKRENT, SWCLISSCOUNT, SWCLPENCOUNT, SWCLRECCOUNT, SWCLRENT;
        static double GIFTSHOPTOTAL, GIFTSHOPKIOSKTOTAL, RESTURANTTOTAL, RESTURANT1TOTAL;
        static double TOTCOLL, TOTCOMPSCHENTRY, TOTSCHENTRY, FIVEDVRENTRY;
        static string IPSTR;
        static void Main(string[] args)
        {
            try
            {
                var handle = GetConsoleWindow();

                // Hide
                ShowWindow(handle, SW_HIDE);

                GENENTRY = COMPGENENTRY = SCHENTRYBOOKED = SCHENTRYARRIVED = FOODBOOKED = FOODSERVED = TOTGENENTRY = TOTCOMPGENENTRY=0;
                SWLKISSCOUNT = SWLKPENCOUNT = SWLKRECCOUNT = SWLKRENT = SWCLISSCOUNT = SWCLPENCOUNT = SWCLRECCOUNT = SWCLRENT = 0;
                GIFTSHOPTOTAL = GIFTSHOPKIOSKTOTAL = RESTURANTTOTAL = RESTURANT1TOTAL = 0;
                TOTCOLL = TOTCOMPSCHENTRY = TOTSCHENTRY = FIVEDVRENTRY = 0;
                IPSTR = "";
               
                DataTable dtrx1 = new DataTable();
                DataTable dtrx2 = new DataTable();
                DataTable dtrst1 = new DataTable();
                DataTable dtrst2 = new DataTable();

                Console.WriteLine("Fetching Ticketing data Reception");
                Logger.Log("Fecthing Ticketing data Reception", false);
                GetReceptionData();

                Console.WriteLine("Fetching Rental data Swimwear");
                Logger.Log("Fetching Rental data Swimwear", false);
                GetSwimwearData();

                Console.WriteLine("Fetching Billing Data From GiftShop");
                Logger.Log("Fetching Billing Data From GiftShop", false);
                dtrx1 = GetGiftShopBillDatatoSync(0);
                Logger.Log("Fetching Billing Data From GiftShop Kiosk", false);
                Console.WriteLine("Fetching Billing Data From GiftShop Kiosk");
                dtrx2 = GetGiftShopBillDatatoSync(1);

                Console.WriteLine("Fetching Billing Data From Resturant");
                Logger.Log("Fetching Billing Data From Resturant", false);
                dtrst1 = GetResturantBillDatatoSync(0);

                Logger.Log("Fetching Billing Data From Resturant1", false);
                Console.WriteLine("Fetching Billing Data From Resturant1");
                dtrst2 = GetResturantBillDatatoSync(1);

                Logger.Log("Fetching IP", false);
                Console.WriteLine("Fetching IP");
                IPSTR = GetIP();

                Console.WriteLine("Syncing Data to Cloud......");
                Logger.Log("Syncing Data to Cloud", false);
                SyncDataToServer(dtrx1, dtrx2, dtrst1, dtrst2);
                Console.WriteLine("Sync Completed.....");
                Logger.Log("Sync Completed", false);

            }
            catch(Exception ex)
            {
                Logger.Log("Error on Main function : " + ex.Message, true);      
            }
            //MessageBox.Show("Sync Completed");
        }

        public static string GetIP()
        {
            string s = "";
            DataTable dt = new DataTable();
            dt.Columns.Add("IPADDRESS", typeof(string));
            dt.Columns.Add("SQLSERVER", typeof(string));
            dt.Columns.Add("UNAME", typeof(string));
            dt.Columns.Add("UPASS", typeof(string));
            try
            {
                WebClient client = new WebClient();
                // Add a user agent header in case the requested URI contains a query.
                client.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR1.0.3705;)");
                string baseurl = "http://checkip.dyndns.org/";
                Stream data = client.OpenRead(baseurl);
                StreamReader reader = new StreamReader(data);
                s = reader.ReadToEnd();
                data.Close();
                reader.Close();
                s = s.Replace("<html><head><title>Current IP Check</title></head><body>", "").Replace("</body></html>", "").ToString();
                s.Replace("Current IP Address:", "").Trim();
            }
            catch (Exception ex)
            {
                // MessageBox.Show(ex.Message.ToString(), "getLocalIPAddress");
                s = "";
            }           
            return s;
        }


        public static void GetReceptionData()
        {
            DataSet DsReturn = new DataSet();
            try
            {
                GENENTRY = 0;
                SCHENTRYBOOKED = 0;
                SCHENTRYARRIVED = 0;
                FOODBOOKED = 0;
                FOODSERVED = 0;
                COMPGENENTRY = 0;
                TOTGENENTRY = 0;
                TOTCOMPGENENTRY = 0;
                TOTCOLL = TOTCOMPSCHENTRY = TOTSCHENTRY = FIVEDVRENTRY = 0;
                string ConnectionString = "";
                ConnectionString = ConfigurationManager.ConnectionStrings["RECP_DB"].ConnectionString;

                using (SqlConnection con = new SqlConnection(ConnectionString))
                {
                    string str = "";
                    if (ConfigurationManager.AppSettings["Dated"].ToString().Trim() == "")
                        str = "Select SUM(A_QTY+C_QTY+SRC_QTY), 1 NOS From bill_main where void_status=0 and BILL_DATE=DATEADD(dd, 0, DATEDIFF(dd, 0, GETDATE())) " +
                              "UNION SELECT SUM(QTY), 2 NOS FROM INT_CREATOR WHERE VOID_STATUS=0 AND L_TIME<>'' AND L_TIME<>'-' AND A_DATE=DATEADD(dd, 0, DATEDIFF(dd, 0, GETDATE())) " + 
                              "UNION SELECT SUM(QTY), 3 NOS FROM INT_CREATOR WHERE VOID_STATUS=0 AND A_DATE=DATEADD(dd, 0, DATEDIFF(dd, 0, GETDATE())) " +
                              "UNION SELECT SUM(QTY), 4 NOS FROM INT_CREATOR WHERE VOID_STATUS=0 AND A_DATE=DATEADD(dd, 0, DATEDIFF(dd, 0, GETDATE())) AND SCLED_ID IN (select distinct(M.SCLED_ID) from INT_CREATOR M INNER JOIN INT_SUB S ON M.INT_ID=S.INT_ID INNER JOIN COUPON C ON C.C_ID=S.C_ID WHERE C.LUNCH_STATUS=1 AND M.VOID_STATUS=0 and S.FETCH_STATUS=1 AND M.A_DATE=DATEADD(dd, 0, DATEDIFF(dd, 0, GETDATE()))) " +
                              "UNION Select SUM(SFQTY), 5 NOS From bill_main where void_status=0 and BILL_DATE=DATEADD(dd, 0, DATEDIFF(dd, 0, GETDATE())) " +
                              "UNION Select SUM(A_QTY+C_QTY+SRC_QTY), 6 NOS From bill_main where void_status=0 and PCK_NAME NOT IN ('COLLEGE', 'SCHOOL1', 'SCHOOL2') and BILL_DATE=DATEADD(dd, 0, DATEDIFF(dd, 0, GETDATE())) " +
                              "UNION Select SUM(SFQTY), 7 NOS From bill_main where void_status=0 and PCK_NAME NOT IN ('COLLEGE', 'SCHOOL1', 'SCHOOL2') and BILL_DATE=DATEADD(dd, 0, DATEDIFF(dd, 0, GETDATE())) " +
                              "UNION Select SUM(VR_QTY), 8 NOS From bill_main where void_status=0 and BILL_DATE=DATEADD(dd, 0, DATEDIFF(dd, 0, GETDATE())) " +
                              "UNION Select SUM(NET_AMT), 9 NOS From bill_main where void_status=0 and BILL_DATE=DATEADD(dd, 0, DATEDIFF(dd, 0, GETDATE())) " +
                              "UNION Select SUM(A_QTY+C_QTY+SRC_QTY), 10 NOS From bill_main where void_status=0 and PCK_NAME IN ('COLLEGE', 'SCHOOL1', 'SCHOOL2') and BILL_DATE=DATEADD(dd, 0, DATEDIFF(dd, 0, GETDATE())) " +
                              "UNION Select SUM(SFQTY), 11 NOS From bill_main where void_status=0 and PCK_NAME IN ('COLLEGE', 'SCHOOL1', 'SCHOOL2') and BILL_DATE=DATEADD(dd, 0, DATEDIFF(dd, 0, GETDATE())) ORDER BY NOS";
                    else
                        str = "Select SUM(A_QTY+C_QTY+SRC_QTY), 1 NOS From bill_main where void_status=0 and BILL_DATE='" + ConfigurationManager.AppSettings["Dated"].ToString().Trim() + "' " +
                              "UNION SELECT SUM(QTY), 2 NOS FROM INT_CREATOR WHERE VOID_STATUS=0 AND L_TIME<>'' AND L_TIME<>'-' AND A_DATE='" + ConfigurationManager.AppSettings["Dated"].ToString().Trim() + "' " + 
                              "UNION SELECT SUM(QTY), 3 NOS FROM INT_CREATOR WHERE VOID_STATUS=0 AND A_DATE='" + ConfigurationManager.AppSettings["Dated"].ToString().Trim() + "' " +
                              "UNION SELECT SUM(QTY), 4 NOS FROM INT_CREATOR WHERE VOID_STATUS=0 AND A_DATE='" + ConfigurationManager.AppSettings["Dated"].ToString().Trim() + "' AND SCLED_ID IN (select distinct(M.SCLED_ID) from INT_CREATOR M INNER JOIN INT_SUB S ON M.INT_ID=S.INT_ID INNER JOIN COUPON C ON C.C_ID=S.C_ID WHERE C.LUNCH_STATUS=1 AND M.VOID_STATUS=0 and S.FETCH_STATUS=1 AND M.A_DATE='" + ConfigurationManager.AppSettings["Dated"].ToString().Trim() + "') " +
                              "UNION Select SUM(SFQTY), 5 NOS From bill_main where void_status=0 and BILL_DATE='" + ConfigurationManager.AppSettings["Dated"].ToString().Trim() + "' " +
                              "UNION Select SUM(A_QTY+C_QTY+SRC_QTY), 6 NOS From bill_main where void_status=0 and PCK_NAME NOT IN ('COLLEGE', 'SCHOOL1', 'SCHOOL2') and BILL_DATE='" + ConfigurationManager.AppSettings["Dated"].ToString().Trim() + "' " +
                              "UNION Select SUM(SFQTY), 7 NOS From bill_main where void_status=0 and PCK_NAME NOT IN ('COLLEGE', 'SCHOOL1', 'SCHOOL2') and BILL_DATE='" + ConfigurationManager.AppSettings["Dated"].ToString().Trim() + "' " +
                              "UNION Select SUM(VR_QTY), 8 NOS From bill_main where void_status=0 and BILL_DATE='" + ConfigurationManager.AppSettings["Dated"].ToString().Trim() + "' " +
                              "UNION Select SUM(NET_AMT), 9 NOS From bill_main where void_status=0 and BILL_DATE='" + ConfigurationManager.AppSettings["Dated"].ToString().Trim() + "' " +
                              "UNION Select SUM(A_QTY+C_QTY+SRC_QTY), 10 NOS From bill_main where void_status=0 and PCK_NAME IN ('COLLEGE', 'SCHOOL1', 'SCHOOL2') and BILL_DATE='" + ConfigurationManager.AppSettings["Dated"].ToString().Trim() + "' " +
                              "UNION Select SUM(SFQTY), 11 NOS From bill_main where void_status=0 and PCK_NAME IN ('COLLEGE', 'SCHOOL1', 'SCHOOL2') and BILL_DATE='" + ConfigurationManager.AppSettings["Dated"].ToString().Trim() + "' ORDER BY NOS";
                    
                    SqlCommand cmd = new SqlCommand(str, con);
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandTimeout = 180;
                    con.Open();
                    SqlDataAdapter dadapter = new SqlDataAdapter(cmd);
                    dadapter.Fill(DsReturn);
                    if (DsReturn != null && DsReturn.Tables.Count > 0 && DsReturn.Tables[0].Rows.Count > 0)
                    {
                        TOTGENENTRY = (DsReturn.Tables[0].Rows[0][0] != DBNull.Value ? Convert.ToDouble(DsReturn.Tables[0].Rows[0][0]) : 0);
                    }

                    if (DsReturn != null && DsReturn.Tables.Count > 0 && DsReturn.Tables[0].Rows.Count > 1)
                    {
                        SCHENTRYARRIVED = (DsReturn.Tables[0].Rows[1][0] != DBNull.Value ? Convert.ToDouble(DsReturn.Tables[0].Rows[1][0]) : 0);
                    }

                    if (DsReturn != null && DsReturn.Tables.Count > 0 && DsReturn.Tables[0].Rows.Count > 2)
                    {
                        FOODBOOKED = SCHENTRYBOOKED = (DsReturn.Tables[0].Rows[2][0] != DBNull.Value ? Convert.ToDouble(DsReturn.Tables[0].Rows[2][0]) : 0);
                    }

                    if (DsReturn != null && DsReturn.Tables.Count > 0 && DsReturn.Tables[0].Rows.Count > 3)
                    {
                        FOODSERVED = (DsReturn.Tables[0].Rows[3][0] != DBNull.Value ? Convert.ToDouble(DsReturn.Tables[0].Rows[3][0]) : 0);
                    }

                    if (DsReturn != null && DsReturn.Tables.Count > 0 && DsReturn.Tables[0].Rows.Count > 4)
                    {
                        TOTCOMPGENENTRY = (DsReturn.Tables[0].Rows[4][0] != DBNull.Value ? Convert.ToDouble(DsReturn.Tables[0].Rows[4][0]) : 0);
                    }

                    if (DsReturn != null && DsReturn.Tables.Count > 0 && DsReturn.Tables[0].Rows.Count > 5)
                    {
                        GENENTRY = (DsReturn.Tables[0].Rows[5][0] != DBNull.Value ? Convert.ToDouble(DsReturn.Tables[0].Rows[5][0]) : 0);
                    }

                    if (DsReturn != null && DsReturn.Tables.Count > 0 && DsReturn.Tables[0].Rows.Count > 6)
                    {
                        COMPGENENTRY = (DsReturn.Tables[0].Rows[6][0] != DBNull.Value ? Convert.ToDouble(DsReturn.Tables[0].Rows[6][0]) : 0);
                    }


                    if (DsReturn != null && DsReturn.Tables.Count > 0 && DsReturn.Tables[0].Rows.Count > 7)
                    {
                        FIVEDVRENTRY = (DsReturn.Tables[0].Rows[7][0] != DBNull.Value ? Convert.ToDouble(DsReturn.Tables[0].Rows[7][0]) : 0);
                    }

                    if (DsReturn != null && DsReturn.Tables.Count > 0 && DsReturn.Tables[0].Rows.Count > 8)
                    {
                        TOTCOLL = (DsReturn.Tables[0].Rows[8][0] != DBNull.Value ? Convert.ToDouble(DsReturn.Tables[0].Rows[8][0]) : 0);
                    }

                    if (DsReturn != null && DsReturn.Tables.Count > 0 && DsReturn.Tables[0].Rows.Count > 9)
                    {
                        TOTSCHENTRY = (DsReturn.Tables[0].Rows[9][0] != DBNull.Value ? Convert.ToDouble(DsReturn.Tables[0].Rows[9][0]) : 0);
                    }

                    if (DsReturn != null && DsReturn.Tables.Count > 0 && DsReturn.Tables[0].Rows.Count > 10)
                    {
                        TOTCOMPSCHENTRY = (DsReturn.Tables[0].Rows[10][0] != DBNull.Value ? Convert.ToDouble(DsReturn.Tables[0].Rows[10][0]) : 0);
                    }

                    con.Close();
                }
            }
            catch (Exception ex)
            {
                Logger.Log("Error on while taking values from database (Reception) :" + ex.Message, true);
            }
        }

        public static void GetSwimwearData()
        {
            SWLKISSCOUNT=0;
            SWLKPENCOUNT=0; 
            SWLKRECCOUNT=0;
            SWLKRENT=0;
            SWCLISSCOUNT=0;
            SWCLPENCOUNT=0;
            SWCLRECCOUNT=0;
            SWCLRENT = 0;
            DataSet DsReturn = new DataSet();
            try
            {                
                string ConnectionString = "";
                ConnectionString = ConfigurationManager.ConnectionStrings["SW_DB"].ConnectionString;

                using (SqlConnection con = new SqlConnection(ConnectionString))
                {
                    string str = "";
                    if (ConfigurationManager.AppSettings["Dated"].ToString().Trim() == "")
                        str = "SELECT SUM(S.QTY), 1 AS NOS From ISS_MAIN M INNER JOIN ISS_SUB S ON M.ISS_ID=S.ISS_ID WHERE M.TYPE='Issues' AND M.VOID_STATUS=0 AND S.LT=1 and S.ITEM_NAME<>'LICENSE FEE' AND S.ITEM_NAME <>'P-LOCKERS' AND M.ISS_DATE=DATEADD(dd, 0, DATEDIFF(dd, 0, GETDATE())) " +
                              "UNION SELECT SUM(S.QTY), 2 AS NOS From ISS_MAIN M INNER JOIN ISS_SUB S ON M.ISS_ID=S.ISS_ID WHERE M.TYPE='Issues' AND M.VOID_STATUS=0 AND S.LT=0 and S.ITEM_NAME<>'LICENSE FEE' AND S.ITEM_NAME <>'P-LOCKERS' AND M.ISS_DATE=DATEADD(dd, 0, DATEDIFF(dd, 0, GETDATE())) " +
                              "UNION SELECT SUM(S.QTY), 3 AS NOS From ISS_MAIN M INNER JOIN ISS_SUB S ON M.ISS_ID=S.ISS_ID WHERE M.TYPE='Issues' AND M.VOID_STATUS=0 AND S.LT=1 AND S.RECEIPT_STATUS=1 and S.ITEM_NAME<>'LICENSE FEE' AND S.ITEM_NAME <>'P-LOCKERS' AND M.ISS_DATE=DATEADD(dd, 0, DATEDIFF(dd, 0, GETDATE())) " +
                              "UNION SELECT SUM(S.QTY), 4 AS NOS From ISS_MAIN M INNER JOIN ISS_SUB S ON M.ISS_ID=S.ISS_ID WHERE M.TYPE='Issues' AND M.VOID_STATUS=0 AND S.LT=0 AND S.RECEIPT_STATUS=1 and S.ITEM_NAME<>'LICENSE FEE' AND S.ITEM_NAME <>'P-LOCKERS' AND M.ISS_DATE=DATEADD(dd, 0, DATEDIFF(dd, 0, GETDATE())) " +
                              "UNION SELECT SUM(ISnull(S.Rent_Amt,0)), 5 AS NOS From ISS_MAIN M INNER JOIN ISS_SUB S ON M.ISS_ID=S.ISS_ID WHERE M.TYPE='Issues' AND M.VOID_STATUS=0 AND S.LT=0 AND S.ITEM_NAME<>'LICENSE FEE' AND S.ITEM_NAME <>'P-LOCKERS' AND M.ISS_DATE=DATEADD(dd, 0, DATEDIFF(dd, 0, GETDATE())) " +
                              "UNION SELECT SUM(ISNUll(S.ForeFit_Amt,0)), 6 AS NOS From ISS_MAIN M INNER JOIN ISS_SUB S ON M.ISS_ID=S.ISS_ID WHERE M.TYPE='Receipts' AND M.VOID_STATUS=0 AND S.LT=0 AND S.ITEM_NAME<>'LICENSE FEE' AND S.ITEM_NAME <>'P-LOCKERS' AND M.ISS_DATE=DATEADD(dd, 0, DATEDIFF(dd, 0, GETDATE())) " +
                              "UNION SELECT SUM(ISnull(S.Rent_Amt,0)), 7 AS NOS From ISS_MAIN M INNER JOIN ISS_SUB S ON M.ISS_ID=S.ISS_ID WHERE M.TYPE='Issues' AND M.VOID_STATUS=0 AND S.LT=1 AND S.ITEM_NAME<>'LICENSE FEE' AND S.ITEM_NAME <>'P-LOCKERS' AND M.ISS_DATE=DATEADD(dd, 0, DATEDIFF(dd, 0, GETDATE())) " +
                              "UNION SELECT SUM(ISNUll(S.ForeFit_Amt,0)), 8 AS NOS From ISS_MAIN M INNER JOIN ISS_SUB S ON M.ISS_ID=S.ISS_ID WHERE M.TYPE='Receipts' AND M.VOID_STATUS=0 AND S.LT=1 AND S.ITEM_NAME<>'LICENSE FEE' AND S.ITEM_NAME <>'P-LOCKERS' AND M.ISS_DATE=DATEADD(dd, 0, DATEDIFF(dd, 0, GETDATE())) ORDER BY NOS";
                    else
                        str = "SELECT SUM(S.QTY), 1 AS NOS From ISS_MAIN M INNER JOIN ISS_SUB S ON M.ISS_ID=S.ISS_ID WHERE M.TYPE='Issues' AND M.VOID_STATUS=0 AND S.LT=1 and S.ITEM_NAME<>'LICENSE FEE' AND S.ITEM_NAME <>'P-LOCKERS' AND M.ISS_DATE='" + ConfigurationManager.AppSettings["Dated"].ToString().Trim() + "' " +
                              "UNION SELECT SUM(S.QTY), 2 AS NOS From ISS_MAIN M INNER JOIN ISS_SUB S ON M.ISS_ID=S.ISS_ID WHERE M.TYPE='Issues' AND M.VOID_STATUS=0 AND S.LT=0 and S.ITEM_NAME<>'LICENSE FEE' AND S.ITEM_NAME <>'P-LOCKERS' AND M.ISS_DATE='" + ConfigurationManager.AppSettings["Dated"].ToString().Trim() + "' " +
                              "UNION SELECT SUM(S.QTY), 3 AS NOS From ISS_MAIN M INNER JOIN ISS_SUB S ON M.ISS_ID=S.ISS_ID WHERE M.TYPE='Issues' AND M.VOID_STATUS=0 AND S.LT=1 AND S.RECEIPT_STATUS=1 and S.ITEM_NAME<>'LICENSE FEE' AND S.ITEM_NAME <>'P-LOCKERS' AND M.ISS_DATE='" + ConfigurationManager.AppSettings["Dated"].ToString().Trim() + "' " +
                              "UNION SELECT SUM(S.QTY), 4 AS NOS From ISS_MAIN M INNER JOIN ISS_SUB S ON M.ISS_ID=S.ISS_ID WHERE M.TYPE='Issues' AND M.VOID_STATUS=0 AND S.LT=0 AND S.RECEIPT_STATUS=1 and S.ITEM_NAME<>'LICENSE FEE' AND S.ITEM_NAME <>'P-LOCKERS' AND M.ISS_DATE='" + ConfigurationManager.AppSettings["Dated"].ToString().Trim() + "' " +
                              "UNION SELECT SUM(ISnull(S.Rent_Amt,0)), 5 AS NOS From ISS_MAIN M INNER JOIN ISS_SUB S ON M.ISS_ID=S.ISS_ID WHERE M.TYPE='Issues' AND M.VOID_STATUS=0 AND S.LT=0 AND S.ITEM_NAME<>'LICENSE FEE' AND S.ITEM_NAME <>'P-LOCKERS' AND M.ISS_DATE='" + ConfigurationManager.AppSettings["Dated"].ToString().Trim() + "' " +
                              "UNION SELECT SUM(ISNUll(S.ForeFit_Amt,0)), 6 AS NOS From ISS_MAIN M INNER JOIN ISS_SUB S ON M.ISS_ID=S.ISS_ID WHERE M.TYPE='Receipts' AND M.VOID_STATUS=0 AND S.LT=0 AND S.ITEM_NAME<>'LICENSE FEE' AND S.ITEM_NAME <>'P-LOCKERS' AND M.ISS_DATE='" + ConfigurationManager.AppSettings["Dated"].ToString().Trim() + "' " +
                              "UNION SELECT SUM(ISnull(S.Rent_Amt,0)), 7 AS NOS From ISS_MAIN M INNER JOIN ISS_SUB S ON M.ISS_ID=S.ISS_ID WHERE M.TYPE='Issues' AND M.VOID_STATUS=0 AND S.LT=1 AND S.ITEM_NAME<>'LICENSE FEE' AND S.ITEM_NAME <>'P-LOCKERS' AND M.ISS_DATE='" + ConfigurationManager.AppSettings["Dated"].ToString().Trim() + "' " +
                              "UNION SELECT SUM(ISNUll(S.ForeFit_Amt,0)), 8 AS NOS From ISS_MAIN M INNER JOIN ISS_SUB S ON M.ISS_ID=S.ISS_ID WHERE M.TYPE='Receipts' AND M.VOID_STATUS=0 AND S.LT=1 AND S.ITEM_NAME<>'LICENSE FEE' AND S.ITEM_NAME <>'P-LOCKERS' AND M.ISS_DATE='" + ConfigurationManager.AppSettings["Dated"].ToString().Trim() + "' ORDER BY NOS";
                    
                    SqlCommand cmd = new SqlCommand(str, con);
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandTimeout = 180;
                    con.Open();
                    SqlDataAdapter dadapter = new SqlDataAdapter(cmd);
                    dadapter.Fill(DsReturn);
                    if (DsReturn != null && DsReturn.Tables.Count > 0 && DsReturn.Tables[0].Rows.Count > 0)
                    {
                        SWLKISSCOUNT = (DsReturn.Tables[0].Rows[0][0] != DBNull.Value ? Convert.ToDouble(DsReturn.Tables[0].Rows[0][0]) : 0);
                    }

                    if (DsReturn != null && DsReturn.Tables.Count > 0 && DsReturn.Tables[0].Rows.Count > 1)
                    {
                        SWCLISSCOUNT = (DsReturn.Tables[0].Rows[1][0] != DBNull.Value ? Convert.ToDouble(DsReturn.Tables[0].Rows[1][0]) : 0);
                    }

                    if (DsReturn != null && DsReturn.Tables.Count > 0 && DsReturn.Tables[0].Rows.Count > 2)
                    {
                        SWLKRECCOUNT = (DsReturn.Tables[0].Rows[2][0] != DBNull.Value ? Convert.ToDouble(DsReturn.Tables[0].Rows[2][0]) : 0);
                    }

                    if (DsReturn != null && DsReturn.Tables.Count > 0 && DsReturn.Tables[0].Rows.Count > 3)
                    {
                        SWCLRECCOUNT = (DsReturn.Tables[0].Rows[3][0] != DBNull.Value ? Convert.ToDouble(DsReturn.Tables[0].Rows[3][0]) : 0);
                    }

                    if (DsReturn != null && DsReturn.Tables.Count > 0 && DsReturn.Tables[0].Rows.Count > 4)
                    {
                        SWCLRENT = (DsReturn.Tables[0].Rows[4][0] != DBNull.Value ? Convert.ToDouble(DsReturn.Tables[0].Rows[4][0]) : 0);
                    }

                    if (DsReturn != null && DsReturn.Tables.Count > 0 && DsReturn.Tables[0].Rows.Count > 5)
                    {
                        SWCLRENT = SWCLRENT + (DsReturn.Tables[0].Rows[5][0] != DBNull.Value ? Convert.ToDouble(DsReturn.Tables[0].Rows[5][0]) : 0);
                    }

                    if (DsReturn != null && DsReturn.Tables.Count > 0 && DsReturn.Tables[0].Rows.Count > 6)
                    {
                        SWLKRENT = (DsReturn.Tables[0].Rows[6][0] != DBNull.Value ? Convert.ToDouble(DsReturn.Tables[0].Rows[6][0]) : 0);
                    }

                    if (DsReturn != null && DsReturn.Tables.Count > 0 && DsReturn.Tables[0].Rows.Count > 7)
                    {
                        SWLKRENT = SWLKRENT + (DsReturn.Tables[0].Rows[7][0] != DBNull.Value ? Convert.ToDouble(DsReturn.Tables[0].Rows[7][0]) : 0);
                    }

                    SWLKPENCOUNT = SWLKISSCOUNT - SWLKRECCOUNT;
                    SWCLPENCOUNT = SWCLISSCOUNT - SWCLRECCOUNT;
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                Logger.Log("Error on while taking values from database (Reception) :" + ex.Message, true);
            }
        }        
        
        public static DataTable GetSampleData()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("COMPANY_ID", typeof(int));
            dt.Columns.Add("NAME", typeof(string));
            dt.Columns.Add("QTY", typeof(double));
            dt.Columns.Add("AMT", typeof(double));          
            return dt;
        }
               
        public static DataTable GetGiftShopBillDatatoSync(int db)
        {
            DataSet DsReturn = new DataSet();
            try
            {
                string ConnectionString = "";                
                
                if (db == 0)
                {
                    ConnectionString = ConfigurationManager.ConnectionStrings["RX_DB"].ConnectionString;
                    GIFTSHOPTOTAL = GetBillTotal(db, "rx");
                }
                else
                {
                    ConnectionString = ConfigurationManager.ConnectionStrings["RX1_DB"].ConnectionString;
                    GIFTSHOPKIOSKTOTAL = GetBillTotal(db, "rx");
                }

                using (SqlConnection con = new SqlConnection(ConnectionString))
                {
                    string str = "";
                    if (db == 0)
                    {
                        if (ConfigurationManager.AppSettings["Dated"].ToString().Trim() == "")
                            str = "Select 1 as COMPANY_ID, IM.GROUP_NAME AS NAME, ISNULL(SUM(BS.QTY),0) QTY, Round(SUM(BS.QTY*(BS.SALE_RATE+BS.LST_TOT_AMT_SALE)),2) AMT From BILL_SUB BS INNER JOIN BILL_MAIN BM ON BM.BILL_ID=BS.BILL_ID INNER JOIN ITEM_MASTER IM ON BS.ITEM_ID=IM.ITEM_ID WHERE BM.VOID_STATUS=0 and BM.BILL_DATE=DATEADD(dd, 0, DATEDIFF(dd, 0, GETDATE())) GROUP BY IM.GROUP_NAME ORDER BY IM.GROUP_NAME";
                        else
                            str = "Select 1 as COMPANY_ID, IM.GROUP_NAME AS NAME, ISNULL(SUM(BS.QTY),0) QTY, Round(SUM(BS.QTY*(BS.SALE_RATE+BS.LST_TOT_AMT_SALE)),2) AMT From BILL_SUB BS INNER JOIN BILL_MAIN BM ON BM.BILL_ID=BS.BILL_ID INNER JOIN ITEM_MASTER IM ON BS.ITEM_ID=IM.ITEM_ID WHERE BM.VOID_STATUS=0 and BM.BILL_DATE='" + ConfigurationManager.AppSettings["Dated"].ToString().Trim() + "' GROUP BY IM.GROUP_NAME ORDER BY IM.GROUP_NAME";
                    }
                    else
                    {
                        if (ConfigurationManager.AppSettings["Dated"].ToString().Trim() == "")
                            str = "Select 2 as COMPANY_ID, IM.GROUP_NAME AS NAME, ISNULL(SUM(BS.QTY),0) QTY, Round(SUM(BS.QTY*(BS.SALE_RATE+BS.LST_TOT_AMT_SALE)),2) AMT From BILL_SUB BS INNER JOIN BILL_MAIN BM ON BM.BILL_ID=BS.BILL_ID INNER JOIN ITEM_MASTER IM ON BS.ITEM_ID=IM.ITEM_ID WHERE BM.VOID_STATUS=0 and BM.BILL_DATE=DATEADD(dd, 0, DATEDIFF(dd, 0, GETDATE())) GROUP BY IM.GROUP_NAME ORDER BY IM.GROUP_NAME";
                        else
                            str = "Select 2 as COMPANY_ID, IM.GROUP_NAME AS NAME, ISNULL(SUM(BS.QTY),0) QTY, Round(SUM(BS.QTY*(BS.SALE_RATE+BS.LST_TOT_AMT_SALE)),2) AMT From BILL_SUB BS INNER JOIN BILL_MAIN BM ON BM.BILL_ID=BS.BILL_ID INNER JOIN ITEM_MASTER IM ON BS.ITEM_ID=IM.ITEM_ID WHERE BM.VOID_STATUS=0 and BM.BILL_DATE='" + ConfigurationManager.AppSettings["Dated"].ToString().Trim() + "' GROUP BY IM.GROUP_NAME ORDER BY IM.GROUP_NAME";
                    } 
                    SqlCommand cmd = new SqlCommand(str, con);
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandTimeout = 180;
                    con.Open();
                    SqlDataAdapter dadapter = new SqlDataAdapter(cmd);
                    dadapter.Fill(DsReturn);
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                Logger.Log("Error on while taking values from database (GiftShop "+ db.ToString() +") :" + ex.Message, true);
                return GetSampleData();
            }
            return DsReturn.Tables[0];
        }

        public static double GetBillTotal(int db, string pck)
        {
            double retval = 0;
            DataSet DsReturn = new DataSet();
            try
            {
                string ConnectionString = "";
                if (db == 0 && pck == "rx")
                    ConnectionString = ConfigurationManager.ConnectionStrings["RX_DB"].ConnectionString;
                else if (db == 1 && pck == "rx")
                    ConnectionString = ConfigurationManager.ConnectionStrings["RX1_DB"].ConnectionString;
                else if (db == 0 && pck == "rst")
                    ConnectionString = ConfigurationManager.ConnectionStrings["RST_DB"].ConnectionString;
                else if (db == 1 && pck == "rst")
                    ConnectionString = ConfigurationManager.ConnectionStrings["RST1_DB"].ConnectionString;

                using (SqlConnection con = new SqlConnection(ConnectionString))
                {
                    string str = "";
                    if (ConfigurationManager.AppSettings["Dated"].ToString().Trim() == "")
                        str = "SELECT SUM(BILL_AMT) FROM BILL_MAIN WHERE VOID_STATUS=0 and BILL_DATE=DATEADD(dd, 0, DATEDIFF(dd, 0, GETDATE()))";
                    else
                        str = "SELECT SUM(BILL_AMT) FROM BILL_MAIN WHERE VOID_STATUS=0 and BILL_DATE='" + ConfigurationManager.AppSettings["Dated"].ToString().Trim() + "'";

                    SqlCommand cmd = new SqlCommand(str, con);
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandTimeout = 180;
                    con.Open();
                    SqlDataAdapter dadapter = new SqlDataAdapter(cmd);
                    dadapter.Fill(DsReturn);
                    if (DsReturn != null && DsReturn.Tables.Count > 0 && DsReturn.Tables[0].Rows.Count > 0)
                    {
                        retval = (DsReturn.Tables[0].Rows[0][0] != DBNull.Value ? Convert.ToDouble(DsReturn.Tables[0].Rows[0][0]) : 0);
                    }
                    con.Close();
                }
            }
            catch(Exception ex)
            {
                Logger.Log("Error on while taking total values from database ("+ pck + " " + db.ToString() + ") :" + ex.Message, true);
            }
            return retval;
        }
        
        public static DataTable GetResturantBillDatatoSync(int db)
        {
            DataSet DsReturn = new DataSet();
            try
            {
                string ConnectionString = "";
                if (db == 0)
                {
                    ConnectionString = ConfigurationManager.ConnectionStrings["RST_DB"].ConnectionString;
                    RESTURANTTOTAL = GetBillTotal(db, "rst");
                }
                else
                {
                    ConnectionString = ConfigurationManager.ConnectionStrings["RST1_DB"].ConnectionString;
                    RESTURANT1TOTAL = GetBillTotal(db, "rst");
                }

                using (SqlConnection con = new SqlConnection(ConnectionString))
                {
                    string str = "";
                    if (db == 0)
                    {
                        if (ConfigurationManager.AppSettings["Dated"].ToString().Trim() == "")
                            str = "Select 1 as COMPANY_ID, BS.ITEM_NAME AS NAME, ISNULL(SUM(BS.QTY),0) QTY, Round(SUM(BS.QTY*(BS.SALE_RATE+BS.LST_TOT_AMT_SALE)),2) AMT From BILL_SUB BS INNER JOIN BILL_MAIN BM ON BM.BILL_ID=BS.BILL_ID INNER JOIN ITEM_MASTER IM ON BS.ITEM_ID=IM.ITEM_ID WHERE BM.VOID_STATUS=0 and IM.GN_3='O' AND BM.BILL_DATE=DATEADD(dd, 0, DATEDIFF(dd, 0, GETDATE())) GROUP BY BS.ITEM_NAME ORDER BY BS.ITEM_NAME";
                        else
                            str = "Select 1 as COMPANY_ID, BS.ITEM_NAME AS NAME, ISNULL(SUM(BS.QTY),0) QTY, Round(SUM(BS.QTY*(BS.SALE_RATE+BS.LST_TOT_AMT_SALE)),2) AMT From BILL_SUB BS INNER JOIN BILL_MAIN BM ON BM.BILL_ID=BS.BILL_ID INNER JOIN ITEM_MASTER IM ON BS.ITEM_ID=IM.ITEM_ID WHERE BM.VOID_STATUS=0 and IM.GN_3='O' AND BM.BILL_DATE='" + ConfigurationManager.AppSettings["Dated"].ToString().Trim() + "' GROUP BY BS.ITEM_NAME ORDER BY BS.ITEM_NAME";
                    }
                    else
                    {
                        if (ConfigurationManager.AppSettings["Dated"].ToString().Trim() == "")
                            str = "Select 2 as COMPANY_ID, BS.ITEM_NAME AS NAME, ISNULL(SUM(BS.QTY),0) QTY, Round(SUM(BS.QTY*(BS.SALE_RATE+BS.LST_TOT_AMT_SALE)),2) AMT From BILL_SUB BS INNER JOIN BILL_MAIN BM ON BM.BILL_ID=BS.BILL_ID INNER JOIN ITEM_MASTER IM ON BS.ITEM_ID=IM.ITEM_ID WHERE BM.VOID_STATUS=0 and IM.GN_3='O' AND BM.BILL_DATE=DATEADD(dd, 0, DATEDIFF(dd, 0, GETDATE())) GROUP BY BS.ITEM_NAME ORDER BY BS.ITEM_NAME";
                        else
                            str = "Select 2 as COMPANY_ID, BS.ITEM_NAME AS NAME, ISNULL(SUM(BS.QTY),0) QTY, Round(SUM(BS.QTY*(BS.SALE_RATE+BS.LST_TOT_AMT_SALE)),2) AMT From BILL_SUB BS INNER JOIN BILL_MAIN BM ON BM.BILL_ID=BS.BILL_ID INNER JOIN ITEM_MASTER IM ON BS.ITEM_ID=IM.ITEM_ID WHERE BM.VOID_STATUS=0 and IM.GN_3='O' AND BM.BILL_DATE='" + ConfigurationManager.AppSettings["Dated"].ToString().Trim() + "' GROUP BY BS.ITEM_NAME ORDER BY BS.ITEM_NAME";
                    }
                    SqlCommand cmd = new SqlCommand(str, con);
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandTimeout = 180;
                    con.Open();
                    SqlDataAdapter dadapter = new SqlDataAdapter(cmd);
                    dadapter.Fill(DsReturn);
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                Logger.Log("Error on while taking values from database (Resturant " + db.ToString() + "):" + ex.Message, true);
                return GetSampleData();
            }
            return DsReturn.Tables[0];
        }

        public static void SyncDataToServer(DataTable dtrx1, DataTable dtrx2, DataTable dtrst1, DataTable dtrst2)
        {
            try
            {
                string ConnectionString = ConfigurationManager.ConnectionStrings["COULD_SQL_DB"].ConnectionString;
                using (SqlConnection con = new SqlConnection(ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("SYNCDB", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandTimeout = 0;

                    cmd.Parameters.Add("@GENENTRY", SqlDbType.Money);
                    cmd.Parameters["@GENENTRY"].Value = GENENTRY;

                    cmd.Parameters.Add("@SCHENTRYBOOKED", SqlDbType.Money);
                    cmd.Parameters["@SCHENTRYBOOKED"].Value = SCHENTRYBOOKED;

                    cmd.Parameters.Add("@SCHENTRYARRIVED", SqlDbType.Money);
                    cmd.Parameters["@SCHENTRYARRIVED"].Value = SCHENTRYARRIVED;

                    cmd.Parameters.Add("@FOODBOOKED", SqlDbType.Money);
                    cmd.Parameters["@FOODBOOKED"].Value = FOODBOOKED;

                    cmd.Parameters.Add("@FOODSERVED", SqlDbType.Money);
                    cmd.Parameters["@FOODSERVED"].Value = FOODSERVED;

                    cmd.Parameters.Add("@SWLKISSCOUNT", SqlDbType.Money);
                    cmd.Parameters["@SWLKISSCOUNT"].Value = SWLKISSCOUNT;

                    cmd.Parameters.Add("@SWLKPENCOUNT", SqlDbType.Money);
                    cmd.Parameters["@SWLKPENCOUNT"].Value = SWLKPENCOUNT;

                    cmd.Parameters.Add("@SWLKRECCOUNT", SqlDbType.Money);
                    cmd.Parameters["@SWLKRECCOUNT"].Value = SWLKRECCOUNT;

                    cmd.Parameters.Add("@SWLKRENT", SqlDbType.Money);
                    cmd.Parameters["@SWLKRENT"].Value = SWLKRENT;

                    cmd.Parameters.Add("@SWCLISSCOUNT", SqlDbType.Money);
                    cmd.Parameters["@SWCLISSCOUNT"].Value = SWCLISSCOUNT;
                    
                    cmd.Parameters.Add("@SWCLPENCOUNT", SqlDbType.Money);
                    cmd.Parameters["@SWCLPENCOUNT"].Value = SWCLPENCOUNT;

                    cmd.Parameters.Add("@SWCLRECCOUNT", SqlDbType.Money);
                    cmd.Parameters["@SWCLRECCOUNT"].Value = SWCLRECCOUNT;

                    cmd.Parameters.Add("@SWCLRENT", SqlDbType.Money);
                    cmd.Parameters["@SWCLRENT"].Value = SWCLRENT;

                    cmd.Parameters.Add("@GIFTSHOPTOTAL", SqlDbType.Money);
                    cmd.Parameters["@GIFTSHOPTOTAL"].Value = GIFTSHOPTOTAL;

                    cmd.Parameters.Add("@GIFTSHOPKIOSKTOTAL", SqlDbType.Money);
                    cmd.Parameters["@GIFTSHOPKIOSKTOTAL"].Value = GIFTSHOPKIOSKTOTAL;

                    cmd.Parameters.Add("@RESTURANTTOTAL", SqlDbType.Money);
                    cmd.Parameters["@RESTURANTTOTAL"].Value = RESTURANTTOTAL;

                    cmd.Parameters.Add("@RESTURANT1TOTAL", SqlDbType.Money);
                    cmd.Parameters["@RESTURANT1TOTAL"].Value = RESTURANT1TOTAL;
                    
                    cmd.Parameters.Add("@RX1", SqlDbType.Structured, dtrx1.Rows.Count);
                    cmd.Parameters["@RX1"].Value = dtrx1;
                    cmd.Parameters.Add("@RX2", SqlDbType.Structured, dtrx2.Rows.Count);
                    cmd.Parameters["@RX2"].Value = dtrx2;
                    cmd.Parameters.Add("@RST1", SqlDbType.Structured, dtrst1.Rows.Count);
                    cmd.Parameters["@RST1"].Value = dtrst1;
                    cmd.Parameters.Add("@RST2", SqlDbType.Structured, dtrst2.Rows.Count);
                    cmd.Parameters["@RST2"].Value = dtrst2;

                    cmd.Parameters.Add("@COMPGENENTRY", SqlDbType.Money);
                    cmd.Parameters["@COMPGENENTRY"].Value = COMPGENENTRY;

                    cmd.Parameters.Add("@TOTGENENTRY", SqlDbType.Money);
                    cmd.Parameters["@TOTGENENTRY"].Value = TOTGENENTRY;

                    cmd.Parameters.Add("@TOTCOMPGENENTRY", SqlDbType.Money);
                    cmd.Parameters["@TOTCOMPGENENTRY"].Value = TOTCOMPGENENTRY;

                    cmd.Parameters.Add("@FIVEDVRENTRY", SqlDbType.Money);
                    cmd.Parameters["@FIVEDVRENTRY"].Value = FIVEDVRENTRY;

                    cmd.Parameters.Add("@TOTSCHENTRY", SqlDbType.Money);
                    cmd.Parameters["@TOTSCHENTRY"].Value = TOTSCHENTRY;

                    cmd.Parameters.Add("@TOTCOMPSCHENTRY", SqlDbType.Money);
                    cmd.Parameters["@TOTCOMPSCHENTRY"].Value = TOTCOMPSCHENTRY;

                    cmd.Parameters.Add("@IPSTR", SqlDbType.NVarChar);
                    cmd.Parameters["@IPSTR"].Value = IPSTR;

                    cmd.Parameters.Add("@TOTCOLL", SqlDbType.Money);
                    cmd.Parameters["@TOTCOLL"].Value = TOTCOLL;

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                Logger.Log("Error on while Syncing Data to Cloud : " + ex.Message, true);               
            }

            return;
        }
               
    }
}
