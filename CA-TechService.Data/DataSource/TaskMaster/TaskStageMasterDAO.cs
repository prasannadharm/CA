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
    public class TaskStageMasterDAO
    {
        #region GetTaskStageMasterList
        public List<TaskStageMasterEntity> GetTaskStageMasterList()
        {
            string CS = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            SqlDataAdapter adapter;
            DataSet ds = new DataSet();
            List<TaskStageMasterEntity> retlst = new List<TaskStageMasterEntity>();
            try
            {
                using (SqlConnection con = new SqlConnection(CS))
                {
                    SqlCommand cmd = new SqlCommand(TaskMasterQueries.GetTaskStageMasterList, con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(ds);

                    for (int i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
                    {
                        TaskStageMasterEntity obj = new TaskStageMasterEntity();
                        obj.TS_ID = Convert.ToInt32(ds.Tables[0].Rows[i]["TS_ID"].ToString());
                        obj.TS_NAME = ds.Tables[0].Rows[i]["TS_NAME"].ToString();
                        obj.INVOICE_TYPE = ds.Tables[0].Rows[i]["INVOICE_TYPE"] == DBNull.Value ? false :  Convert.ToBoolean(ds.Tables[0].Rows[i]["INVOICE_TYPE"]);
                        obj.ACTIVE_STATUS = ds.Tables[0].Rows[i]["ACTIVE_STATUS"] == DBNull.Value ? false : Convert.ToBoolean(ds.Tables[0].Rows[i]["ACTIVE_STATUS"]);
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
        #endregion

        #region EditTaskStageMaster
        public List<TaskStageMasterEntity> EditTaskStageMaster(int id)
        {
            string CS = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            SqlDataAdapter adapter;
            DataSet ds = new DataSet();
            List<TaskStageMasterEntity> retlst = new List<TaskStageMasterEntity>();
            try
            {
                using (SqlConnection con = new SqlConnection(CS))
                {
                    SqlCommand cmd = new SqlCommand(TaskMasterQueries.EditTaskStageMaster, con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID", id);
                    con.Open();
                    adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(ds);

                    for (int i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
                    {
                        TaskStageMasterEntity obj = new TaskStageMasterEntity();
                        obj.TS_ID = Convert.ToInt32(ds.Tables[0].Rows[i]["TS_ID"].ToString());
                        obj.TS_NAME = ds.Tables[0].Rows[i]["TS_NAME"].ToString();
                        obj.INVOICE_TYPE = ds.Tables[0].Rows[i]["INVOICE_TYPE"] == DBNull.Value ? false : Convert.ToBoolean(ds.Tables[0].Rows[i]["INVOICE_TYPE"]);
                        obj.ACTIVE_STATUS = ds.Tables[0].Rows[i]["ACTIVE_STATUS"] == DBNull.Value ? false : Convert.ToBoolean(ds.Tables[0].Rows[i]["ACTIVE_STATUS"]);
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
        #endregion

        public DbStatusEntity UpdateTaskStageMaster(TaskStageMasterEntity obj, int id)
        {
            DbStatusEntity objreturn = new DbStatusEntity();
            string CS = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            try
            {
                using (SqlConnection con = new SqlConnection(CS))
                {
                    SqlCommand cmd = new SqlCommand(TaskMasterQueries.UpdateTaskStageMaster, con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@TS_ID", id);
                    cmd.Parameters.AddWithValue("@TS_NAME", obj.TS_NAME);
                    cmd.Parameters.AddWithValue("@ACTIVE_STATUS", obj.ACTIVE_STATUS);
                    cmd.Parameters.AddWithValue("@INVOICE_TYPE", obj.INVOICE_TYPE);

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

        #region InsertTaskStageMaster
        public DbStatusEntity InsertTaskStageMaster(TaskStageMasterEntity obj)
        {
            DbStatusEntity objreturn = new DbStatusEntity();
            string CS = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;

            try
            {
                using (SqlConnection con = new SqlConnection(CS))
                {
                    SqlCommand cmd = new SqlCommand(TaskMasterQueries.InsertTaskStageMaster, con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@TS_NAME", obj.TS_NAME);
                    cmd.Parameters.AddWithValue("@ACTIVE_STATUS", obj.ACTIVE_STATUS);
                    cmd.Parameters.AddWithValue("@INVOICE_TYPE", obj.INVOICE_TYPE);

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
        #endregion

        #region DeleteTaskStageMaster
        public DbStatusEntity DeleteTaskStageMaster(int id)
        {
            DbStatusEntity objreturn = new DbStatusEntity();
            string CS = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;

            try
            {
                using (SqlConnection con = new SqlConnection(CS))
                {
                    SqlCommand cmd = new SqlCommand(TaskMasterQueries.DeleteTaskStageMaster, con);
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
        #endregion

    }
}
