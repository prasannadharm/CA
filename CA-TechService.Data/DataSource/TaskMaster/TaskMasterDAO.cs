#region Imports
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using CA_TechService.Common.Transport.TaskMaster;
using CA_TechService.Data.Query.TaskMaster;
using CA_TechService.Common.Generic;
#endregion
namespace CA_TechService.Data.DataSource.TaskMaster
{
    public class TaskMasterDAO
    {
        public List<TaskMasterMainEntity> GetTaskMasterList()
        {
            string CS = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            SqlDataAdapter adapter;
            DataSet ds = new DataSet();
            List<TaskMasterMainEntity> retlst = new List<TaskMasterMainEntity>();
            try
            {
                using (SqlConnection con = new SqlConnection(CS))
                {
                    SqlCommand cmd = new SqlCommand("USP_GetTaskMasterList", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(ds);

                    for (int i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
                    {
                        TaskMasterMainEntity obj = new TaskMasterMainEntity();
                        obj.T_ID = ds.Tables[0].Rows[i]["T_ID"] == DBNull.Value ? 0 : Convert.ToInt64(ds.Tables[0].Rows[i]["T_ID"]);
                        obj.T_NAME = ds.Tables[0].Rows[i]["T_NAME"] == DBNull.Value ? "" : ds.Tables[0].Rows[i]["T_NAME"].ToString();
                        obj.T_DESC = ds.Tables[0].Rows[i]["T_DESC"] == DBNull.Value ? "" : ds.Tables[0].Rows[i]["T_DESC"].ToString();
                        obj.RECURRING = ds.Tables[0].Rows[i]["RECURRING"] == DBNull.Value ? true: Convert.ToBoolean(ds.Tables[0].Rows[i]["RECURRING"]);
                        obj.RECURRING_TYPE = ds.Tables[0].Rows[i]["RECURRING_TYPE"] == DBNull.Value ? "" : ds.Tables[0].Rows[i]["RECURRING_TYPE"].ToString();
                        obj.PRIORITY = ds.Tables[0].Rows[i]["PRIORITY"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["PRIORITY"]);
                        obj.ACTIVE_STATUS = ds.Tables[0].Rows[i]["ACTIVE_STATUS"] == DBNull.Value ? true: Convert.ToBoolean(ds.Tables[0].Rows[i]["ACTIVE_STATUS"]);
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

        public List<TaskMasterEntity> EditTaskMaster(Int64 id)
        {
            string CS = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            SqlDataAdapter adapter;
            DataSet ds = new DataSet();
            List<TaskMasterEntity> retlst = new List<TaskMasterEntity>();
            TaskMasterEntity retval = new TaskMasterEntity();
            try
            {
                using (SqlConnection con = new SqlConnection(CS))
                {
                    SqlCommand cmd = new SqlCommand("USP_GetTaskMasterDetailsbyID", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID", id);
                    con.Open();
                    adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(ds);
                    if (ds.Tables.Count >= 3)
                    {
                        List<TaskMasterMainEntity> objlst1 = new List<TaskMasterMainEntity>();
                        for (int i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
                        {
                            TaskMasterMainEntity obj1 = new TaskMasterMainEntity();
                            obj1.T_ID = ds.Tables[0].Rows[i]["T_ID"] == DBNull.Value ? 0 : Convert.ToInt64(ds.Tables[0].Rows[i]["T_ID"]);
                            obj1.T_NAME = ds.Tables[0].Rows[i]["T_NAME"] == DBNull.Value ? "" : ds.Tables[0].Rows[i]["T_NAME"].ToString();
                            obj1.T_DESC = ds.Tables[0].Rows[i]["T_DESC"] == DBNull.Value ? "" : ds.Tables[0].Rows[i]["T_DESC"].ToString();
                            obj1.PRIORITY = ds.Tables[0].Rows[i]["PRIORITY"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["PRIORITY"]);
                            obj1.RECURRING = ds.Tables[0].Rows[i]["RECURRING"] == DBNull.Value ? true : Convert.ToBoolean(ds.Tables[0].Rows[i]["RECURRING"]);
                            obj1.RECURRING_TYPE = ds.Tables[0].Rows[i]["RECURRING_TYPE"] == DBNull.Value ? "" : ds.Tables[0].Rows[i]["RECURRING_TYPE"].ToString();                           
                            obj1.RECURRING_DAYS = ds.Tables[0].Rows[i]["RECURRING_DAYS"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["RECURRING_DAYS"]);
                            obj1.RECURRING_START_DAY = ds.Tables[0].Rows[i]["RECURRING_START_DAY"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["RECURRING_START_DAY"]);
                            obj1.RECURRING_END_DATE = ds.Tables[0].Rows[i]["RECURRING_END_DATE"] == DBNull.Value ? "" : Convert.ToString(ds.Tables[0].Rows[i]["RECURRING_END_DATE"]);
                            obj1.ACTIVE_STATUS = ds.Tables[0].Rows[i]["ACTIVE_STATUS"] == DBNull.Value ? true : Convert.ToBoolean(ds.Tables[0].Rows[i]["ACTIVE_STATUS"]);
                            objlst1.Add(obj1);                           
                        }
                        retval.MainList = objlst1;

                        List<TaskMasterSubEntity> objlst2 = new List<TaskMasterSubEntity>();
                        for (int i = 0; i <= ds.Tables[1].Rows.Count - 1; i++)
                        {
                            TaskMasterSubEntity obj2 = new TaskMasterSubEntity();
                            obj2.ID = ds.Tables[1].Rows[i]["ID"] == DBNull.Value ? 0 : Convert.ToInt64(ds.Tables[1].Rows[i]["ID"]);
                            obj2.T_ID = ds.Tables[1].Rows[i]["T_ID"] == DBNull.Value ? 0 : Convert.ToInt64(ds.Tables[1].Rows[i]["T_ID"]);
                            obj2.TS_ID = ds.Tables[1].Rows[i]["TS_ID"] == DBNull.Value ? 0 : Convert.ToInt64(ds.Tables[1].Rows[i]["TS_ID"]);
                            obj2.TS_NAME = ds.Tables[1].Rows[i]["TS_NAME"] == DBNull.Value ? "" : ds.Tables[1].Rows[i]["TS_NAME"].ToString();
                            obj2.USER_ID = ds.Tables[1].Rows[i]["USER_ID"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[1].Rows[i]["USER_ID"]);
                            obj2.SL_NO = ds.Tables[1].Rows[i]["SL_NO"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[1].Rows[i]["SL_NO"]);
                            obj2.NAME = ds.Tables[1].Rows[i]["NAME"] == DBNull.Value ? "" : ds.Tables[1].Rows[i]["NAME"].ToString();
                            objlst2.Add(obj2);                            
                        }
                        retval.SubList = objlst2;

                        List<TaskClientMappingEntity> objlst3 = new List<TaskClientMappingEntity>();
                        for (int i = 0; i <= ds.Tables[2].Rows.Count - 1; i++)
                        {
                            TaskClientMappingEntity obj3 = new TaskClientMappingEntity();                          
                            obj3.T_ID = ds.Tables[2].Rows[i]["T_ID"] == DBNull.Value ? 0 : Convert.ToInt64(ds.Tables[2].Rows[i]["T_ID"]);
                            obj3.C_ID = ds.Tables[2].Rows[i]["C_ID"] == DBNull.Value ? 0 : Convert.ToInt64(ds.Tables[2].Rows[i]["C_ID"]);
                            obj3.C_NAME = ds.Tables[2].Rows[i]["C_NAME"] == DBNull.Value ? "" : ds.Tables[2].Rows[i]["C_NAME"].ToString();
                            obj3.FILE_NO = ds.Tables[2].Rows[i]["FILE_NO"] == DBNull.Value ? "" : ds.Tables[2].Rows[i]["FILE_NO"].ToString();
                            obj3.PAN = ds.Tables[2].Rows[i]["PAN"] == DBNull.Value ? "" : ds.Tables[2].Rows[i]["PAN"].ToString();
                            objlst3.Add(obj3);                            
                        }
                        retval.SubList = objlst2;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            retlst.Add(retval);
            return retlst;
        }

        public DbStatusEntity DeleteTaskMaster(int id)
        {
            DbStatusEntity objreturn = new DbStatusEntity();
            string CS = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;

            try
            {
                using (SqlConnection con = new SqlConnection(CS))
                {
                    SqlCommand cmd = new SqlCommand("USP_DeleteTaskMaster", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID", id);

                    cmd.Parameters.Add("@RESULT", SqlDbType.Int);
                    cmd.Parameters["@RESULT"].Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@CNT", SqlDbType.Int);
                    cmd.Parameters["@CNT"].Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@MSG", SqlDbType.NVarChar, 500);
                    cmd.Parameters["@MSG"].Direction = ParameterDirection.Output;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    objreturn.RESULT = Convert.ToInt32(cmd.Parameters["@RESULT"].Value);
                    objreturn.CNT = Convert.ToInt32(cmd.Parameters["@CNT"].Value);
                    objreturn.MSG = Convert.ToString(cmd.Parameters["@MSG"].Value);
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return objreturn;
        }

        public List<TaskClientMappingEntity> GetTaskMasterClientListById(Int64 id)
        {
            string CS = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            SqlDataAdapter adapter;
            DataSet ds = new DataSet();
            List<TaskClientMappingEntity> retlst = new List<TaskClientMappingEntity>();
            try
            {
                using (SqlConnection con = new SqlConnection(CS))
                {
                    SqlCommand cmd = new SqlCommand("USP_GetTaskMasterClientsDetailsbyID", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID", id);
                    con.Open();
                    adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(ds);

                    for (int i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
                    {
                        TaskClientMappingEntity obj = new TaskClientMappingEntity();
                        obj.T_ID = ds.Tables[0].Rows[i]["T_ID"] == DBNull.Value ? 0 : Convert.ToInt64(ds.Tables[0].Rows[i]["T_ID"]);
                        obj.C_ID = ds.Tables[0].Rows[i]["C_ID"] == DBNull.Value ? 0 : Convert.ToInt64(ds.Tables[0].Rows[i]["C_ID"]);
                        obj.C_NAME = ds.Tables[0].Rows[i]["C_NAME"] == DBNull.Value ? "" : ds.Tables[0].Rows[i]["C_NAME"].ToString();
                        obj.FILE_NO = ds.Tables[0].Rows[i]["C_NAME"] == DBNull.Value ? "" : ds.Tables[0].Rows[i]["FILE_NO"].ToString();
                        obj.PAN = ds.Tables[0].Rows[i]["C_NAME"] == DBNull.Value ? "" : ds.Tables[0].Rows[i]["PAN"].ToString();
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

        public List<TaskMasterSubEntity> GetTaskMasterSatgesListById(Int64 id)
        {
            string CS = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            SqlDataAdapter adapter;
            DataSet ds = new DataSet();
            List<TaskMasterSubEntity> retlst = new List<TaskMasterSubEntity>();
            try
            {
                using (SqlConnection con = new SqlConnection(CS))
                {
                    SqlCommand cmd = new SqlCommand("USP_GetTaskMasterStagesDetailsbyID", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID", id);
                    con.Open();
                    adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(ds);

                    for (int i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
                    {
                        TaskMasterSubEntity obj = new TaskMasterSubEntity();
                        obj.ID = ds.Tables[0].Rows[i]["ID"] == DBNull.Value ? 0 : Convert.ToInt64(ds.Tables[0].Rows[i]["ID"]);
                        obj.T_ID = ds.Tables[0].Rows[i]["T_ID"] == DBNull.Value ? 0 : Convert.ToInt64(ds.Tables[0].Rows[i]["T_ID"]);
                        obj.TS_ID = ds.Tables[0].Rows[i]["TS_ID"] == DBNull.Value ? 0 : Convert.ToInt64(ds.Tables[0].Rows[i]["TS_ID"]);
                        obj.TS_NAME = ds.Tables[0].Rows[i]["TS_NAME"] == DBNull.Value ? "" : ds.Tables[0].Rows[i]["TS_NAME"].ToString();
                        obj.USER_ID = ds.Tables[0].Rows[i]["USER_ID"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["USER_ID"]);
                        obj.NAME = ds.Tables[0].Rows[i]["NAME"] == DBNull.Value ? "" : ds.Tables[0].Rows[i]["NAME"].ToString();
                        obj.SL_NO = ds.Tables[0].Rows[i]["SL_NO"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["SL_NO"]);
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
