#region Imports
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using CA_TechService.Common.Transport.Task;
using CA_TechService.Common.Generic;
#endregion
namespace CA_TechService.Data.DataSource.Task
{
    public class TaskTrnCreateTaskDAO
    {
        public List<TaskTrnCreateListEntity> GetTaskTrnCreatedList(string fromdate, string todate)
        {
            string CS = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            SqlDataAdapter adapter;
            DataSet ds = new DataSet();
            List<TaskTrnCreateListEntity> retlst = new List<TaskTrnCreateListEntity>();
            try
            {
                using (SqlConnection con = new SqlConnection(CS))
                {
                    SqlCommand cmd = new SqlCommand("USP_GetTaskTrnCreatedList", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@DateFrom", fromdate);
                    cmd.Parameters.AddWithValue("@DateTo", todate);
                    con.Open();
                    adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(ds);

                    for (int i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
                    {
                        TaskTrnCreateListEntity obj = new TaskTrnCreateListEntity();
                        obj.T_ID = ds.Tables[0].Rows[i]["T_ID"] == DBNull.Value ? 0 : Convert.ToInt64(ds.Tables[0].Rows[i]["T_ID"]);
                        obj.T_NO = ds.Tables[0].Rows[i]["T_NO"] == DBNull.Value ? 0 : Convert.ToInt64(ds.Tables[0].Rows[i]["T_NO"]);
                        obj.T_DATE = ds.Tables[0].Rows[i]["T_DATE"] == DBNull.Value ? "" : ds.Tables[0].Rows[i]["T_DATE"].ToString();
                        obj.T_NAME = ds.Tables[0].Rows[i]["T_NAME"] == DBNull.Value ? "" : ds.Tables[0].Rows[i]["T_NAME"].ToString();
                        obj.C_NO = ds.Tables[0].Rows[i]["C_NO"] == DBNull.Value ? 0 : Convert.ToInt64(ds.Tables[0].Rows[i]["C_NO"]);
                        obj.C_NAME = ds.Tables[0].Rows[i]["C_NAME"] == DBNull.Value ? "" : ds.Tables[0].Rows[i]["C_NAME"].ToString();
                        obj.FILE_NO = ds.Tables[0].Rows[i]["FILE_NO"] == DBNull.Value ? "" : ds.Tables[0].Rows[i]["FILE_NO"].ToString();
                        obj.SCH_ON = ds.Tables[0].Rows[i]["SCH_ON"] == DBNull.Value ? "" : ds.Tables[0].Rows[i]["SCH_ON"].ToString();
                        obj.CREATEDBY_NAME = ds.Tables[0].Rows[i]["CREATEDBY_NAME"] == DBNull.Value ? "" : ds.Tables[0].Rows[i]["CREATEDBY_NAME"].ToString();
                        obj.VOID_STATUS = ds.Tables[0].Rows[i]["VOID_STATUS"] == DBNull.Value ? true : Convert.ToBoolean(ds.Tables[0].Rows[i]["VOID_STATUS"]);
                        retlst.Add(obj);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return retlst;
        }

        public List<TaskTrnPendingForInitializeEntity> GetTaskTrnPendingTaskForInitailization(string TIDSTR, string CIDSTR, string CLICATIDSTR)
        {
            string CS = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            SqlDataAdapter adapter;
            DataSet ds = new DataSet();
            List<TaskTrnPendingForInitializeEntity> retlst = new List<TaskTrnPendingForInitializeEntity>();
            try
            {
                using (SqlConnection con = new SqlConnection(CS))
                {
                    SqlCommand cmd = new SqlCommand("USP_GetPendingTaskForInitialization", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 0;
                    cmd.Parameters.AddWithValue("@TIDSTR", TIDSTR);
                    cmd.Parameters.AddWithValue("@CIDSTR", CIDSTR);
                    cmd.Parameters.AddWithValue("@CLICATIDSTR", CLICATIDSTR);
                    con.Open();                    
                    adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(ds);

                    for (int i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
                    {
                        TaskTrnPendingForInitializeEntity obj = new TaskTrnPendingForInitializeEntity();
                        obj.T_ID = ds.Tables[0].Rows[i]["T_ID"] == DBNull.Value ? 0 : Convert.ToInt64(ds.Tables[0].Rows[i]["T_ID"]);
                        obj.T_NAME = ds.Tables[0].Rows[i]["T_NAME"] == DBNull.Value ? "" : ds.Tables[0].Rows[i]["T_NAME"].ToString();
                        obj.T_DESC = ds.Tables[0].Rows[i]["T_DESC"] == DBNull.Value ? "" : ds.Tables[0].Rows[i]["T_DESC"].ToString();
                        obj.PRIORITY = ds.Tables[0].Rows[i]["PRIORITY"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["PRIORITY"]);
                        obj.RECURRING_TYPE = ds.Tables[0].Rows[i]["RECURRING_TYPE"] == DBNull.Value ? "" : ds.Tables[0].Rows[i]["RECURRING_TYPE"].ToString();
                        obj.TASK_SCH_DATE = ds.Tables[0].Rows[i]["TASK_SCH_DATE"] == DBNull.Value ? "" : ds.Tables[0].Rows[i]["TASK_SCH_DATE"].ToString();
                        obj.C_ID = ds.Tables[0].Rows[i]["C_ID"] == DBNull.Value ? 0 : Convert.ToInt64(ds.Tables[0].Rows[i]["C_ID"]);
                        obj.C_NO = ds.Tables[0].Rows[i]["C_NO"] == DBNull.Value ? 0 : Convert.ToInt64(ds.Tables[0].Rows[i]["C_NO"]);
                        obj.C_NAME = ds.Tables[0].Rows[i]["C_NAME"] == DBNull.Value ? "" : ds.Tables[0].Rows[i]["C_NAME"].ToString();
                        obj.FILE_NO = ds.Tables[0].Rows[i]["FILE_NO"] == DBNull.Value ? "" : ds.Tables[0].Rows[i]["FILE_NO"].ToString();
                        obj.PAN = ds.Tables[0].Rows[i]["PAN"] == DBNull.Value ? "" : ds.Tables[0].Rows[i]["PAN"].ToString();
                        retlst.Add(obj);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return retlst;
        }

        public List<string> GetTaskSchDateForTask(Int64 id)
        {
            string CS = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            SqlDataAdapter adapter;
            DataSet ds = new DataSet();
            List<string> retlst = new List<string>();
            try
            {
                using (SqlConnection con = new SqlConnection(CS))
                {
                    SqlCommand cmd = new SqlCommand("USP_GetTaskSchOnForTaskbyID", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@T_ID", id);
                    con.Open();
                    adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(ds);

                    for (int i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
                    {
                        string obj = "";
                        obj = ds.Tables[0].Rows[i]["TASK_SCH_DATE"] == DBNull.Value ? "" : ds.Tables[0].Rows[i]["TASK_SCH_DATE"].ToString();
                        retlst.Add(obj);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return retlst;
        }

    }
}
