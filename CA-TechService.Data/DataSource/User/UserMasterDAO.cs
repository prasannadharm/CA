using CA_TechService.Common.Generic;
using CA_TechService.Common.Security.Encryption;
using CA_TechService.Common.Transport.User;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA_TechService.Data.DataSource.User
{
    public class UserMasterDAO
    {
        public List<UserMasterEntity> GetUserList()
        {
            string CS = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            SqlDataAdapter adapter;
            DataSet ds = new DataSet();
            List<UserMasterEntity> retlst = new List<UserMasterEntity>();
            try
            {
                using (SqlConnection con = new SqlConnection(CS))
                {
                    SqlCommand cmd = new SqlCommand("USP_GetUsersMasterList", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(ds);

                    for (int i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
                    {
                        UserMasterEntity obj = new UserMasterEntity();
                        obj.USER_ID = Convert.ToInt32(ds.Tables[0].Rows[i]["USER_ID"].ToString());
                        obj.EMAIL = ds.Tables[0].Rows[i]["EMAIL"] == DBNull.Value ? "" : ds.Tables[0].Rows[i]["EMAIL"].ToString();
                        obj.NAME = ds.Tables[0].Rows[i]["NAME"] == DBNull.Value ? "" : ds.Tables[0].Rows[i]["NAME"].ToString();
                        obj.ROLE_NAME = ds.Tables[0].Rows[i]["ROLE_NAME"] == DBNull.Value ? "" : ds.Tables[0].Rows[i]["ROLE_NAME"].ToString();
                        obj.MOBILE_NO = ds.Tables[0].Rows[i]["MOBILE_NO"] == DBNull.Value ? "" : ds.Tables[0].Rows[i]["MOBILE_NO"].ToString();
                        obj.ACTIVE_STATUS = ds.Tables[0].Rows[i]["ACTIVE_STATUS"] == DBNull.Value ? true : Convert.ToBoolean(ds.Tables[0].Rows[i]["ACTIVE_STATUS"]);
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


        public List<UserMasterEntity> EditUserMaster(int id)
        {
            string CS = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            SqlDataAdapter adapter;
            DataSet ds = new DataSet();
            List<UserMasterEntity> retlst = new List<UserMasterEntity>();
            try
            {
                using (SqlConnection con = new SqlConnection(CS))
                {
                    SqlCommand cmd = new SqlCommand("USP_GetUserDetailsbyID", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID", id);
                    con.Open();
                    adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(ds);

                    for (int i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
                    {
                        UserMasterEntity obj = new UserMasterEntity();
                        obj.USER_ID = Convert.ToInt32(ds.Tables[0].Rows[i]["USER_ID"].ToString());
                        obj.EMAIL = ds.Tables[0].Rows[i]["EMAIL"] == DBNull.Value ? "" : ds.Tables[0].Rows[i]["EMAIL"].ToString();
                        obj.NAME = ds.Tables[0].Rows[i]["NAME"] == DBNull.Value ? "" : ds.Tables[0].Rows[i]["NAME"].ToString();
                        obj.USER_PASSWORD = ds.Tables[0].Rows[i]["USER_PASSWORD"] == DBNull.Value ? "" : ds.Tables[0].Rows[i]["USER_PASSWORD"].ToString();
                        obj.ROLE_ID = ds.Tables[0].Rows[i]["ROLE_ID"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["ROLE_ID"].ToString());
                        obj.ROLE_NAME = ds.Tables[0].Rows[i]["ROLE_NAME"] == DBNull.Value ? "" : ds.Tables[0].Rows[i]["ROLE_NAME"].ToString();
                        obj.MOBILE_NO = ds.Tables[0].Rows[i]["MOBILE_NO"] == DBNull.Value ? "" : ds.Tables[0].Rows[i]["MOBILE_NO"].ToString();
                        obj.ACTIVE_STATUS = ds.Tables[0].Rows[i]["ACTIVE_STATUS"] == DBNull.Value ? true : Convert.ToBoolean(ds.Tables[0].Rows[i]["ACTIVE_STATUS"]);
                        obj.USER_PASSWORD = CryptographyHelper.Instance.Decrypt(obj.USER_PASSWORD);
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


        public DbStatusEntity UpdateUserMaster(UserMasterEntity obj, int id)
        {
            DbStatusEntity objreturn = new DbStatusEntity();
            string CS = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            try
            {
                using (SqlConnection con = new SqlConnection(CS))
                {
                    SqlCommand cmd = new SqlCommand("USP_UpdateUsersMaster", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@USER_ID", id);
                    cmd.Parameters.AddWithValue("@USER_PASSWORD", CryptographyHelper.Instance.Encrypt(obj.USER_PASSWORD));
                    cmd.Parameters.AddWithValue("@NAME", obj.NAME);
                    cmd.Parameters.AddWithValue("@EMAIL", obj.EMAIL);
                    cmd.Parameters.AddWithValue("@MOBILE_NO", obj.MOBILE_NO);
                    cmd.Parameters.AddWithValue("@ACTIVE_STATUS", obj.ACTIVE_STATUS);
                    cmd.Parameters.AddWithValue("@ROLE_ID", obj.ROLE_ID);

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

        public DbStatusEntity InsertUserMaster(UserMasterEntity obj)
        {
            DbStatusEntity objreturn = new DbStatusEntity();
            string CS = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;

            try
            {
                using (SqlConnection con = new SqlConnection(CS))
                {
                    SqlCommand cmd = new SqlCommand("USP_InsertUsersMaster", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@USER_PASSWORD", CryptographyHelper.Instance.Encrypt(obj.USER_PASSWORD));
                    cmd.Parameters.AddWithValue("@NAME", obj.NAME);
                    cmd.Parameters.AddWithValue("@EMAIL", obj.EMAIL);
                    cmd.Parameters.AddWithValue("@MOBILE_NO", obj.MOBILE_NO);
                    cmd.Parameters.AddWithValue("@ACTIVE_STATUS", obj.ACTIVE_STATUS);
                    cmd.Parameters.AddWithValue("@ROLE_ID", obj.ROLE_ID);

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

        public DbStatusEntity DeleteUser(int id)
        {
            DbStatusEntity objreturn = new DbStatusEntity();
            string CS = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;

            try
            {
                using (SqlConnection con = new SqlConnection(CS))
                {
                    SqlCommand cmd = new SqlCommand("USP_DeleteUserMaster", con);
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
    }
}
