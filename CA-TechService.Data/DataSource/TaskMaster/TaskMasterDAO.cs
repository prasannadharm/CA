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
                        retval.MainArray = objlst1.ToArray();

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
                        retval.SubArray = objlst2.ToArray();

                        List<TaskClientMappingEntity> objlst3 = new List<TaskClientMappingEntity>();
                        for (int i = 0; i <= ds.Tables[2].Rows.Count - 1; i++)
                        {
                            TaskClientMappingEntity obj3 = new TaskClientMappingEntity();                          
                            obj3.T_ID = ds.Tables[2].Rows[i]["T_ID"] == DBNull.Value ? 0 : Convert.ToInt64(ds.Tables[2].Rows[i]["T_ID"]);
                            obj3.C_ID = ds.Tables[2].Rows[i]["C_ID"] == DBNull.Value ? 0 : Convert.ToInt64(ds.Tables[2].Rows[i]["C_ID"]);
                            obj3.C_NO = ds.Tables[2].Rows[i]["C_NO"] == DBNull.Value ? 0 : Convert.ToInt64(ds.Tables[2].Rows[i]["C_NO"]);
                            obj3.C_NAME = ds.Tables[2].Rows[i]["C_NAME"] == DBNull.Value ? "" : ds.Tables[2].Rows[i]["C_NAME"].ToString();
                            obj3.FILE_NO = ds.Tables[2].Rows[i]["FILE_NO"] == DBNull.Value ? "" : ds.Tables[2].Rows[i]["FILE_NO"].ToString();
                            obj3.PAN = ds.Tables[2].Rows[i]["PAN"] == DBNull.Value ? "" : ds.Tables[2].Rows[i]["PAN"].ToString();
                            obj3.AADHAAR = ds.Tables[2].Rows[i]["AADHAAR"] == DBNull.Value ? "" : ds.Tables[2].Rows[i]["AADHAAR"].ToString();
                            obj3.GSTIN = ds.Tables[2].Rows[i]["GSTIN"] == DBNull.Value ? "" : ds.Tables[2].Rows[i]["GSTIN"].ToString();
                            obj3.PH_NO = ds.Tables[2].Rows[i]["PH_NO"] == DBNull.Value ? "" : ds.Tables[2].Rows[i]["PH_NO"].ToString();
                            obj3.MOBILE_NO1 = ds.Tables[2].Rows[i]["MOBILE_NO1"] == DBNull.Value ? "" : ds.Tables[2].Rows[i]["MOBILE_NO1"].ToString();
                            obj3.MOBILE_NO2 = ds.Tables[2].Rows[i]["MOBILE_NO2"] == DBNull.Value ? "" : ds.Tables[2].Rows[i]["MOBILE_NO2"].ToString();
                            obj3.CLI_GRP_NAME = ds.Tables[2].Rows[i]["CLI_GRP_NAME"] == DBNull.Value ? "" : ds.Tables[2].Rows[i]["CLI_GRP_NAME"].ToString();
                            objlst3.Add(obj3);                            
                        }
                        retval.ClientMapArray = objlst3.ToArray();

                        List<TaskClientCategoryMappingEntity> objlst4 = new List<TaskClientCategoryMappingEntity>();
                        for (int i = 0; i <= ds.Tables[3].Rows.Count - 1; i++)
                        {
                            TaskClientCategoryMappingEntity obj4 = new TaskClientCategoryMappingEntity();
                            obj4.T_ID = ds.Tables[3].Rows[i]["T_ID"] == DBNull.Value ? 0 : Convert.ToInt64(ds.Tables[3].Rows[i]["T_ID"]);
                            obj4.CLI_CAT_ID = ds.Tables[3].Rows[i]["CLI_CAT_ID"] == DBNull.Value ? 0 : Convert.ToInt64(ds.Tables[3].Rows[i]["CLI_CAT_ID"]);
                            objlst4.Add(obj4);
                        }
                        retval.ClientCategoryMapArray = objlst4.ToArray();
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
                        obj.C_NO = ds.Tables[0].Rows[i]["C_NO"] == DBNull.Value ? 0 : Convert.ToInt64(ds.Tables[0].Rows[i]["C_NO"]);
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

        public DbStatusEntity InsertTaskMaster(TaskMasterParamEntity obj)
        {
            DbStatusEntity objreturn = new DbStatusEntity();
            string CS = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;

            try
            {
                using (SqlConnection con = new SqlConnection(CS))
                {
                    SqlCommand cmd = new SqlCommand("USP_InsertTaskMaster", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@T_NAME", obj.T_NAME);
                    cmd.Parameters.AddWithValue("@T_DESC", obj.T_DESC);
                    cmd.Parameters.AddWithValue("@PRIORITY",obj.PRIORITY);
                    cmd.Parameters.AddWithValue("@RECURRING_TYPE", obj.RECURRING_TYPE);
                    cmd.Parameters.AddWithValue("@RECURRING_DAYS", obj.RECURRING_DAYS);
                    cmd.Parameters.AddWithValue("@RECURRING_START_DAY", obj.RECURRING_START_DAY);
                    cmd.Parameters.AddWithValue("@RECURRING_END_DATE", obj.RECURRING_END_DATE);
                    cmd.Parameters.AddWithValue("@ACTIVE_STATUS", obj.ACTIVE_STATUS);
                    cmd.Parameters.AddWithValue("@MAPPED_CLIENTS", obj.MAPPED_CLIENTS);
                    cmd.Parameters.AddWithValue("@MAPPED_CLI_CAT", obj.MAPPED_CLI_CAT);

                    DataTable dtsub = new DataTable();
                    dtsub.Columns.Add("TS_ID", typeof(int));
                    dtsub.Columns.Add("TS_NAME", typeof(string));
                    dtsub.Columns.Add("USER_ID", typeof(int));
                    dtsub.Columns.Add("SL_NO", typeof(int));

                    foreach(TaskMasterSubParamEntity objsub in obj.SUBARR)
                    {
                        DataRow dr = dtsub.NewRow();
                        dr["TS_ID"] = 0;
                        dr["TS_NAME"] = objsub.STAGE;
                        dr["USER_ID"] = objsub.USER_ID;
                        dr["SL_NO"] = objsub.SLNO;
                        dtsub.Rows.Add(dr);
                    }

                    SqlParameter sqlParam = cmd.Parameters.AddWithValue("@TVP", dtsub);
                    sqlParam.SqlDbType = SqlDbType.Structured;                   

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

        public DbStatusEntity UpdateTaskMaster(TaskMasterParamEntity obj, Int64 id)
        {
            DbStatusEntity objreturn = new DbStatusEntity();
            string CS = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;

            try
            {
                using (SqlConnection con = new SqlConnection(CS))
                {
                    SqlCommand cmd = new SqlCommand("USP_UpdateTaskMaster", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@T_ID", id);
                    cmd.Parameters.AddWithValue("@T_NAME", obj.T_NAME);
                    cmd.Parameters.AddWithValue("@T_DESC", obj.T_DESC);
                    cmd.Parameters.AddWithValue("@PRIORITY", obj.PRIORITY);
                    cmd.Parameters.AddWithValue("@RECURRING_TYPE", obj.RECURRING_TYPE);
                    cmd.Parameters.AddWithValue("@RECURRING_DAYS", obj.RECURRING_DAYS);
                    cmd.Parameters.AddWithValue("@RECURRING_START_DAY", obj.RECURRING_START_DAY);
                    cmd.Parameters.AddWithValue("@RECURRING_END_DATE", obj.RECURRING_END_DATE);
                    cmd.Parameters.AddWithValue("@ACTIVE_STATUS", obj.ACTIVE_STATUS);
                    cmd.Parameters.AddWithValue("@MAPPED_CLIENTS", obj.MAPPED_CLIENTS);
                    cmd.Parameters.AddWithValue("@MAPPED_CLI_CAT", obj.MAPPED_CLI_CAT);

                    DataTable dtsub = new DataTable();
                    dtsub.Columns.Add("TS_ID", typeof(int));
                    dtsub.Columns.Add("TS_NAME", typeof(string));
                    dtsub.Columns.Add("USER_ID", typeof(int));
                    dtsub.Columns.Add("SL_NO", typeof(int));

                    foreach (TaskMasterSubParamEntity objsub in obj.SUBARR)
                    {
                        DataRow dr = dtsub.NewRow();
                        dr["TS_ID"] = 0;
                        dr["TS_NAME"] = objsub.STAGE;
                        dr["USER_ID"] = objsub.USER_ID;
                        dr["SL_NO"] = objsub.SLNO;
                        dtsub.Rows.Add(dr);
                    }

                    SqlParameter sqlParam = cmd.Parameters.AddWithValue("@TVP", dtsub);
                    sqlParam.SqlDbType = SqlDbType.Structured;

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

    }
}

