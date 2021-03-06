﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using CA_TechService.Common.Transport.ClientMaster;
using CA_TechService.Common.Generic;


namespace CA_TechService.Data.DataSource.ClientMaster
{
    public class ClientMasterDAO
    {
        public List<ClientMasterEntity> GetClientList(ClientMasterSearchEntity ob)
        {
            string CS = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            SqlDataAdapter adapter;
            DataSet ds = new DataSet();
            List<ClientMasterEntity> retlst = new List<ClientMasterEntity>();
            try
            {
                using (SqlConnection con = new SqlConnection(CS))
                {
                    SqlCommand cmd = new SqlCommand("USP_GetClientMasterList", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@C_NO", ob.C_NO);
                    cmd.Parameters.AddWithValue("@FILE_NO", ob.FILE_NO);
                    cmd.Parameters.AddWithValue("@C_NAME", ob.C_NAME);
                    cmd.Parameters.AddWithValue("@GENDER", ob.GENDER);
                    cmd.Parameters.AddWithValue("@CITY", ob.CITY);
                    cmd.Parameters.AddWithValue("@STATE", ob.STATE);
                    cmd.Parameters.AddWithValue("@PH_NO", ob.PH_NO);
                    cmd.Parameters.AddWithValue("@MOBILE_NO1", ob.MOBILE_NO1);
                    cmd.Parameters.AddWithValue("@MOBILE_NO2", ob.MOBILE_NO2);
                    cmd.Parameters.AddWithValue("@EMAIL_ID", ob.EMAIL_ID);                    
                    cmd.Parameters.AddWithValue("@PAN", ob.PAN);
                    cmd.Parameters.AddWithValue("@AADHAAR", ob.AADHAAR);
                    cmd.Parameters.AddWithValue("@GSTIN", ob.GSTIN);
                    cmd.Parameters.AddWithValue("@WARD", ob.WARD);
                    cmd.Parameters.AddWithValue("@RACK_NO", ob.RACK_NO);
                    cmd.Parameters.AddWithValue("@CLI_GRP_LST", ob.CLI_GRP_LST);
                    cmd.Parameters.AddWithValue("@CLI_CAT_LST", ob.CLI_CAT_LST);
                    cmd.Parameters.AddWithValue("@CNT", ob.CNT);
                    con.Open();
                    adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(ds);

                    for (int i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
                    {
                        ClientMasterEntity obj = new ClientMasterEntity();
                        obj.C_ID = ds.Tables[0].Rows[i]["C_ID"] == DBNull.Value ? 0 : Convert.ToInt64(ds.Tables[0].Rows[i]["C_ID"]);
                        obj.C_NO = ds.Tables[0].Rows[i]["C_NO"] == DBNull.Value ? 0 : Convert.ToInt64(ds.Tables[0].Rows[i]["C_NO"]);
                        obj.FILE_NO = ds.Tables[0].Rows[i]["FILE_NO"] != DBNull.Value ? ds.Tables[0].Rows[i]["FILE_NO"].ToString() : "";
                        obj.C_NAME = ds.Tables[0].Rows[i]["C_NAME"] != DBNull.Value ? ds.Tables[0].Rows[i]["C_NAME"].ToString() : "";
                        obj.MOBILE_NO1 = ds.Tables[0].Rows[i]["MOBILE_NO1"] != DBNull.Value ? ds.Tables[0].Rows[i]["MOBILE_NO1"].ToString() : "";
                        obj.PAN = ds.Tables[0].Rows[i]["PAN"] != DBNull.Value ? ds.Tables[0].Rows[i]["PAN"].ToString() : "";
                        obj.GSTIN = ds.Tables[0].Rows[i]["GSTIN"] != DBNull.Value ? ds.Tables[0].Rows[i]["GSTIN"].ToString() : "";
                        obj.ACTIVE_STATUS = Convert.ToBoolean(ds.Tables[0].Rows[i]["ACTIVE_STATUS"]);
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

        public List<ClientMasterEntity> EditClient(int id)
        {
            string CS = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            SqlDataAdapter adapter;
            DataSet ds = new DataSet();
            List<ClientMasterEntity> retlst = new List<ClientMasterEntity>();
            try
            {
                using (SqlConnection con = new SqlConnection(CS))
                {
                    SqlCommand cmd = new SqlCommand("USP_GetClientMasterDetailsbyID", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID", id);
                    con.Open();
                    adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(ds);

                    for (int i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
                    {
                        ClientMasterEntity obj = new ClientMasterEntity();
                        obj.C_ID = ds.Tables[0].Rows[i]["C_ID"] == DBNull.Value ? 0 : Convert.ToInt64(ds.Tables[0].Rows[i]["C_ID"]);
                        obj.C_NO = ds.Tables[0].Rows[i]["C_NO"] == DBNull.Value ? 0 : Convert.ToInt64(ds.Tables[0].Rows[i]["C_NO"]);
                        obj.FILE_NO = ds.Tables[0].Rows[i]["FILE_NO"] != DBNull.Value ? ds.Tables[0].Rows[i]["FILE_NO"].ToString() : "";
                        obj.C_NAME = ds.Tables[0].Rows[i]["C_NAME"] != DBNull.Value ? ds.Tables[0].Rows[i]["C_NAME"].ToString() : "";
                        obj.ALIAS = ds.Tables[0].Rows[i]["ALIAS"] != DBNull.Value ? ds.Tables[0].Rows[i]["ALIAS"].ToString() : "";
                        obj.FNAME = ds.Tables[0].Rows[i]["FNAME"] != DBNull.Value ? ds.Tables[0].Rows[i]["FNAME"].ToString() : "";
                        obj.HNAME = ds.Tables[0].Rows[i]["HNAME"] != DBNull.Value ? ds.Tables[0].Rows[i]["HNAME"].ToString() : "";
                        obj.GENDER = ds.Tables[0].Rows[i]["GENDER"] != DBNull.Value ? ds.Tables[0].Rows[i]["GENDER"].ToString() : "";
                        obj.SAME_AB = ds.Tables[0].Rows[i]["SAME_AB"] == DBNull.Value ? false : Convert.ToBoolean(ds.Tables[0].Rows[i]["SAME_AB"]);
                        obj.ADDR = ds.Tables[0].Rows[i]["ADDR"] != DBNull.Value ? ds.Tables[0].Rows[i]["ADDR"].ToString() : "";
                        obj.ADDR1 = ds.Tables[0].Rows[i]["ADDR1"] != DBNull.Value ? ds.Tables[0].Rows[i]["ADDR1"].ToString() : "";
                        obj.CITY = ds.Tables[0].Rows[i]["CITY"] != DBNull.Value ? ds.Tables[0].Rows[i]["CITY"].ToString() : "";
                        obj.CITY1 = ds.Tables[0].Rows[i]["CITY1"] != DBNull.Value ? ds.Tables[0].Rows[i]["CITY1"].ToString() : "";
                        obj.PIN = ds.Tables[0].Rows[i]["PIN"] != DBNull.Value ? ds.Tables[0].Rows[i]["PIN"].ToString() : "";
                        obj.PIN1 = ds.Tables[0].Rows[i]["PIN1"] != DBNull.Value ? ds.Tables[0].Rows[i]["PIN1"].ToString() : "";
                        obj.STATE = ds.Tables[0].Rows[i]["STATE"] != DBNull.Value ? ds.Tables[0].Rows[i]["STATE"].ToString() : "";
                        obj.STATE1 = ds.Tables[0].Rows[i]["STATE1"] != DBNull.Value ? ds.Tables[0].Rows[i]["STATE1"].ToString() : "";
                        obj.CNT_NAME = ds.Tables[0].Rows[i]["CNT_NAME"] != DBNull.Value ? ds.Tables[0].Rows[i]["CNT_NAME"].ToString() : "";
                        obj.PH_NO = ds.Tables[0].Rows[i]["PH_NO"] != DBNull.Value ? ds.Tables[0].Rows[i]["PH_NO"].ToString() : "";
                        obj.MOBILE_NO1 = ds.Tables[0].Rows[i]["MOBILE_NO1"] != DBNull.Value ? ds.Tables[0].Rows[i]["MOBILE_NO1"].ToString() : "";
                        obj.MOBILE_NO2 = ds.Tables[0].Rows[i]["MOBILE_NO2"] != DBNull.Value ? ds.Tables[0].Rows[i]["MOBILE_NO2"].ToString() : "";
                        obj.EMAIL_ID = ds.Tables[0].Rows[i]["EMAIL_ID"] != DBNull.Value ? ds.Tables[0].Rows[i]["EMAIL_ID"].ToString() : "";
                        obj.PAN = ds.Tables[0].Rows[i]["PAN"] != DBNull.Value ? ds.Tables[0].Rows[i]["PAN"].ToString() : "";
                        obj.AADHAAR = ds.Tables[0].Rows[i]["AADHAAR"] != DBNull.Value ? ds.Tables[0].Rows[i]["AADHAAR"].ToString() : "";
                        obj.GSTIN = ds.Tables[0].Rows[i]["GSTIN"] != DBNull.Value ? ds.Tables[0].Rows[i]["GSTIN"].ToString() : "";
                        obj.WARD = ds.Tables[0].Rows[i]["WARD"] != DBNull.Value ? ds.Tables[0].Rows[i]["WARD"].ToString() : "";
                        obj.RACK_NO = ds.Tables[0].Rows[i]["RACK_NO"] != DBNull.Value ? ds.Tables[0].Rows[i]["RACK_NO"].ToString() : "";
                        obj.ALERT_MSG = ds.Tables[0].Rows[i]["ALERT_MSG"] != DBNull.Value ? ds.Tables[0].Rows[i]["ALERT_MSG"].ToString() : "";
                        obj.CLI_GRP_NAME = ds.Tables[0].Rows[i]["CLI_GRP_NAME"] != DBNull.Value ? ds.Tables[0].Rows[i]["CLI_GRP_NAME"].ToString() : "";
                        obj.CLI_GRP_ID = ds.Tables[0].Rows[i]["CLI_GRP_ID"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["CLI_GRP_ID"].ToString());
                        obj.DOB = ds.Tables[0].Rows[i]["DOB"] == DBNull.Value ? "" : Convert.ToString(ds.Tables[0].Rows[i]["DOB"]);
                        obj.ACTIVE_STATUS = Convert.ToBoolean(ds.Tables[0].Rows[i]["ACTIVE_STATUS"]);

                        List<ClientCategoryMapping> objmaplst = new List<ClientCategoryMapping>();
                        string catids = "";
                        for (int j = 0; j <= ds.Tables[1].Rows.Count - 1; j++)
                        {
                            ClientCategoryMapping objnew = new ClientCategoryMapping();
                            objnew.CLI_CAT_ID = ds.Tables[1].Rows[j]["CLI_CAT_ID"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[1].Rows[j]["CLI_CAT_ID"]);
                            objnew.C_ID = ds.Tables[1].Rows[j]["C_ID"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[1].Rows[j]["C_ID"]);
                            objnew.CLI_CAT_NAME = ds.Tables[1].Rows[j]["CLI_CAT_NAME"] == DBNull.Value ? "" : ds.Tables[1].Rows[j]["CLI_CAT_NAME"].ToString();
                            objmaplst.Add(objnew);
                            catids = catids + objnew.CLI_CAT_ID.ToString() + ",";
                        }
                        catids = catids.TrimEnd(',');
                        obj.ClientCategoryStringList = catids;
                        obj.ClientCategoryList = objmaplst;
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


        public DbStatusEntity UpdateClient(ClientMasterEntity obj, int id)
        {
            DbStatusEntity objreturn = new DbStatusEntity();
            string CS = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            try
            {
                using (SqlConnection con = new SqlConnection(CS))
                {
                    SqlCommand cmd = new SqlCommand("USP_UpdateClientMaster", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@C_ID", id);
                    cmd.Parameters.AddWithValue("@FILE_NO", obj.FILE_NO);
                    cmd.Parameters.AddWithValue("@C_NAME", obj.C_NAME);
                    cmd.Parameters.AddWithValue("@ALIAS", obj.ALIAS);
                    cmd.Parameters.AddWithValue("@FNAME", obj.FNAME);
                    cmd.Parameters.AddWithValue("@HNAME", obj.HNAME);
                    cmd.Parameters.AddWithValue("@GENDER", obj.GENDER);
                    cmd.Parameters.AddWithValue("@SAME_AB", obj.SAME_AB);
                    cmd.Parameters.AddWithValue("@ADDR", obj.ADDR);
                    cmd.Parameters.AddWithValue("@ADDR1", obj.ADDR1);
                    cmd.Parameters.AddWithValue("@CITY", obj.CITY);
                    cmd.Parameters.AddWithValue("@CITY1", obj.CITY1);
                    cmd.Parameters.AddWithValue("@PIN", obj.PIN);
                    cmd.Parameters.AddWithValue("@PIN1", obj.PIN1);
                    cmd.Parameters.AddWithValue("@STATE", obj.STATE);
                    cmd.Parameters.AddWithValue("@STATE1", obj.STATE1);
                    cmd.Parameters.AddWithValue("@CNT_NAME", obj.CNT_NAME);
                    cmd.Parameters.AddWithValue("@PH_NO", obj.PH_NO);
                    cmd.Parameters.AddWithValue("@MOBILE_NO1", obj.MOBILE_NO1);
                    cmd.Parameters.AddWithValue("@MOBILE_NO2", obj.MOBILE_NO2);
                    cmd.Parameters.AddWithValue("@EMAIL_ID", obj.EMAIL_ID);
                    cmd.Parameters.AddWithValue("@DOB", obj.DOB);
                    cmd.Parameters.AddWithValue("@PAN", obj.PAN);
                    cmd.Parameters.AddWithValue("@AADHAAR", obj.AADHAAR);
                    cmd.Parameters.AddWithValue("@GSTIN", obj.GSTIN);
                    cmd.Parameters.AddWithValue("@WARD", obj.WARD);
                    cmd.Parameters.AddWithValue("@RACK_NO", obj.RACK_NO);
                    cmd.Parameters.AddWithValue("@CLI_GRP_ID", obj.CLI_GRP_ID);
                    cmd.Parameters.AddWithValue("@Alert_Msg", obj.ALERT_MSG);
                    cmd.Parameters.AddWithValue("@ClientCategoryStringList", obj.ClientCategoryStringList);                    
                    cmd.Parameters.AddWithValue("@ACTIVE_STATUS", obj.ACTIVE_STATUS);

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

        public DbStatusEntity InsertClient(ClientMasterEntity obj)
        {
            DbStatusEntity objreturn = new DbStatusEntity();
            string CS = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;

            try
            {
                using (SqlConnection con = new SqlConnection(CS))
                {
                    SqlCommand cmd = new SqlCommand("USP_InsertClientMaster", con);
                    cmd.CommandType = CommandType.StoredProcedure;                   
                    cmd.Parameters.AddWithValue("@FILE_NO", obj.FILE_NO);
                    cmd.Parameters.AddWithValue("@C_NAME", obj.C_NAME);
                    cmd.Parameters.AddWithValue("@ALIAS", obj.ALIAS);
                    cmd.Parameters.AddWithValue("@FNAME", obj.FNAME);
                    cmd.Parameters.AddWithValue("@HNAME", obj.HNAME);
                    cmd.Parameters.AddWithValue("@GENDER", obj.GENDER);
                    cmd.Parameters.AddWithValue("@SAME_AB", obj.SAME_AB);
                    cmd.Parameters.AddWithValue("@ADDR", obj.ADDR);
                    cmd.Parameters.AddWithValue("@ADDR1", obj.ADDR1);
                    cmd.Parameters.AddWithValue("@CITY", obj.CITY);
                    cmd.Parameters.AddWithValue("@CITY1", obj.CITY1);
                    cmd.Parameters.AddWithValue("@PIN", obj.PIN);
                    cmd.Parameters.AddWithValue("@PIN1", obj.PIN1);
                    cmd.Parameters.AddWithValue("@STATE", obj.STATE);
                    cmd.Parameters.AddWithValue("@STATE1", obj.STATE1);
                    cmd.Parameters.AddWithValue("@CNT_NAME", obj.CNT_NAME);
                    cmd.Parameters.AddWithValue("@PH_NO", obj.PH_NO);
                    cmd.Parameters.AddWithValue("@MOBILE_NO1", obj.MOBILE_NO1);
                    cmd.Parameters.AddWithValue("@MOBILE_NO2", obj.MOBILE_NO2);
                    cmd.Parameters.AddWithValue("@EMAIL_ID", obj.EMAIL_ID);
                    cmd.Parameters.AddWithValue("@DOB", obj.DOB);
                    cmd.Parameters.AddWithValue("@PAN", obj.PAN);
                    cmd.Parameters.AddWithValue("@AADHAAR", obj.AADHAAR);
                    cmd.Parameters.AddWithValue("@GSTIN", obj.GSTIN);
                    cmd.Parameters.AddWithValue("@WARD", obj.WARD);
                    cmd.Parameters.AddWithValue("@RACK_NO", obj.RACK_NO);
                    cmd.Parameters.AddWithValue("@CLI_GRP_ID", obj.CLI_GRP_ID);
                    cmd.Parameters.AddWithValue("@Alert_Msg", obj.ALERT_MSG);
                    cmd.Parameters.AddWithValue("@ClientCategoryStringList", obj.ClientCategoryStringList);
                    cmd.Parameters.AddWithValue("@ACTIVE_STATUS", obj.ACTIVE_STATUS);

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

        public DbStatusEntity DeleteClient(int id)
        {
            DbStatusEntity objreturn = new DbStatusEntity();
            string CS = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;

            try
            {
                using (SqlConnection con = new SqlConnection(CS))
                {
                    SqlCommand cmd = new SqlCommand("USP_DeleteClientMaster", con);
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

        public List<ClientDocsEntity> GetClientDocs(long id)
        {
            string CS = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            SqlDataAdapter adapter;
            DataSet ds = new DataSet();
            List<ClientDocsEntity> retlst = new List<ClientDocsEntity>();
            try
            {
                using (SqlConnection con = new SqlConnection(CS))
                {
                    SqlCommand cmd = new SqlCommand("USP_GetClientDocsbyID", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID", id);
                    con.Open();
                    adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(ds);

                    for (int i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
                    {
                        ClientDocsEntity obj = new ClientDocsEntity();
                        obj.CLIENT_ID = ds.Tables[0].Rows[i]["CLIENT_ID"] == DBNull.Value ? 0 : Convert.ToInt64(ds.Tables[0].Rows[i]["CLIENT_ID"].ToString());
                        obj.ORG_FILE_NAME = ds.Tables[0].Rows[i]["ORG_FILE_NAME"] == DBNull.Value ? "" : ds.Tables[0].Rows[i]["ORG_FILE_NAME"].ToString();
                        obj.PHY_FILE_NAME = ds.Tables[0].Rows[i]["PHY_FILE_NAME"] == DBNull.Value ? "" : ds.Tables[0].Rows[i]["PHY_FILE_NAME"].ToString();
                        obj.REMARKS = ds.Tables[0].Rows[i]["REMARKS"] == DBNull.Value ? "" : Convert.ToString(ds.Tables[0].Rows[i]["REMARKS"]);                        
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

        public DbStatusEntity InsertClientDocs(ClientDocsEntity obj)
        {
            DbStatusEntity objreturn = new DbStatusEntity();
            string CS = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            try
            {
                using (SqlConnection con = new SqlConnection(CS))
                {
                    SqlCommand cmd = new SqlCommand("USP_InsertClientDocs", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@CLIENT_ID", obj.CLIENT_ID);
                    cmd.Parameters.AddWithValue("@ORG_FILE_NAME", obj.ORG_FILE_NAME);
                    cmd.Parameters.AddWithValue("@PHY_FILE_NAME", obj.PHY_FILE_NAME);
                    cmd.Parameters.AddWithValue("@REMARKS", obj.REMARKS);

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

        public DbStatusEntity DeleteClientDocs(long id, string phy_file_name)
        {
            DbStatusEntity objreturn = new DbStatusEntity();
            string CS = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            try
            {
                using (SqlConnection con = new SqlConnection(CS))
                {
                    SqlCommand cmd = new SqlCommand("USP_DeleteClientDocs", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID", id);
                    cmd.Parameters.AddWithValue("@PHY_FILE_NAME", phy_file_name);

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

        public DbStatusEntity DeleteClientCredentials(long id)
        {
            DbStatusEntity objreturn = new DbStatusEntity();
            string CS = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            try
            {
                using (SqlConnection con = new SqlConnection(CS))
                {
                    SqlCommand cmd = new SqlCommand("USP_DeleteClientCredentials", con);
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

        public DbStatusEntity InsertClientCredentials(ClientCredentialsEntity obj)
        {
            DbStatusEntity objreturn = new DbStatusEntity();
            string CS = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            try
            {
                using (SqlConnection con = new SqlConnection(CS))
                {
                    SqlCommand cmd = new SqlCommand("USP_InsertClientCredentials", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@CLIENT_ID", obj.CLIENT_ID);
                    cmd.Parameters.AddWithValue("@SITE_NAME", obj.SITE_NAME);
                    cmd.Parameters.AddWithValue("@URL", obj.URL);
                    cmd.Parameters.AddWithValue("@UNAME", obj.UNAME);
                    cmd.Parameters.AddWithValue("@UPASS", obj.UPASS);
                    cmd.Parameters.AddWithValue("@REMARKS", obj.REMARKS);

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

        public DbStatusEntity UpdateClientCredentials(ClientCredentialsEntity obj)
        {
            DbStatusEntity objreturn = new DbStatusEntity();
            string CS = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            try
            {
                using (SqlConnection con = new SqlConnection(CS))
                {
                    SqlCommand cmd = new SqlCommand("USP_UpdateClientCredentials", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID", obj.ID);
                    cmd.Parameters.AddWithValue("@CLIENT_ID", obj.CLIENT_ID);
                    cmd.Parameters.AddWithValue("@SITE_NAME", obj.SITE_NAME);
                    cmd.Parameters.AddWithValue("@URL", obj.URL);
                    cmd.Parameters.AddWithValue("@UNAME", obj.UNAME);
                    cmd.Parameters.AddWithValue("@UPASS", obj.UPASS);
                    cmd.Parameters.AddWithValue("@REMARKS", obj.REMARKS);

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

        public List<ClientCredentialsEntity> GetClientCredentialsByClientID(long id)
        {
            string CS = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            SqlDataAdapter adapter;
            DataSet ds = new DataSet();
            List<ClientCredentialsEntity> retlst = new List<ClientCredentialsEntity>();
            try
            {
                using (SqlConnection con = new SqlConnection(CS))
                {
                    SqlCommand cmd = new SqlCommand("USP_GetClientCredentialsbyClientID", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID", id);
                    con.Open();
                    adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(ds);

                    for (int i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
                    {
                        ClientCredentialsEntity obj = new ClientCredentialsEntity();
                        obj.ID = ds.Tables[0].Rows[i]["ID"] == DBNull.Value ? 0 : Convert.ToInt64(ds.Tables[0].Rows[i]["ID"].ToString());
                        obj.CLIENT_ID = ds.Tables[0].Rows[i]["CLIENT_ID"] == DBNull.Value ? 0 : Convert.ToInt64(ds.Tables[0].Rows[i]["CLIENT_ID"].ToString());
                        obj.SITE_NAME = ds.Tables[0].Rows[i]["SITE_NAME"] == DBNull.Value ? "" : ds.Tables[0].Rows[i]["SITE_NAME"].ToString();
                        obj.URL = ds.Tables[0].Rows[i]["URL"] == DBNull.Value ? "" : ds.Tables[0].Rows[i]["URL"].ToString();
                        obj.UNAME = ds.Tables[0].Rows[i]["UNAME"] == DBNull.Value ? "" : ds.Tables[0].Rows[i]["UNAME"].ToString();
                        obj.UPASS = ds.Tables[0].Rows[i]["UPASS"] == DBNull.Value ? "" : ds.Tables[0].Rows[i]["UPASS"].ToString();
                        obj.REMARKS = ds.Tables[0].Rows[i]["REMARKS"] == DBNull.Value ? "" : Convert.ToString(ds.Tables[0].Rows[i]["REMARKS"]);
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

        public List<ClientCredentialsEntity> GetClientCredentialsByID(long id)
        {
            string CS = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            SqlDataAdapter adapter;
            DataSet ds = new DataSet();
            List<ClientCredentialsEntity> retlst = new List<ClientCredentialsEntity>();
            try
            {
                using (SqlConnection con = new SqlConnection(CS))
                {
                    SqlCommand cmd = new SqlCommand("USP_GetClientCredentialsbyID", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID", id);
                    con.Open();
                    adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(ds);

                    for (int i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
                    {
                        ClientCredentialsEntity obj = new ClientCredentialsEntity();
                        obj.ID = ds.Tables[0].Rows[i]["ID"] == DBNull.Value ? 0 : Convert.ToInt64(ds.Tables[0].Rows[i]["ID"].ToString());
                        obj.CLIENT_ID = ds.Tables[0].Rows[i]["CLIENT_ID"] == DBNull.Value ? 0 : Convert.ToInt64(ds.Tables[0].Rows[i]["CLIENT_ID"].ToString());
                        obj.SITE_NAME = ds.Tables[0].Rows[i]["SITE_NAME"] == DBNull.Value ? "" : ds.Tables[0].Rows[i]["SITE_NAME"].ToString();
                        obj.URL = ds.Tables[0].Rows[i]["URL"] == DBNull.Value ? "" : ds.Tables[0].Rows[i]["URL"].ToString();
                        obj.UNAME = ds.Tables[0].Rows[i]["UNAME"] == DBNull.Value ? "" : ds.Tables[0].Rows[i]["UNAME"].ToString();
                        obj.UPASS = ds.Tables[0].Rows[i]["UPASS"] == DBNull.Value ? "" : ds.Tables[0].Rows[i]["UPASS"].ToString();
                        obj.REMARKS = ds.Tables[0].Rows[i]["REMARKS"] == DBNull.Value ? "" : Convert.ToString(ds.Tables[0].Rows[i]["REMARKS"]);
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
