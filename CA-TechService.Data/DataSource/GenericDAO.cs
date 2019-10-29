using CA_TechService.Common.Transport.CityState;
using CA_TechService.Common.Transport.ClientMaster;
using CA_TechService.Common.Transport.Generic;
using CA_TechService.Common.Transport.Roles;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA_TechService.Data.DataSource
{
    public class GenericDAO
    {
        public List<StateMasterEntity> GetStateList()
        {
            string CS = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            SqlDataAdapter adapter;
            DataSet ds = new DataSet();
            List<StateMasterEntity> retlst = new List<StateMasterEntity>();
            try
            {
                using (SqlConnection con = new SqlConnection(CS))
                {
                    SqlCommand cmd = new SqlCommand("USP_GetActiveStateList", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(ds);

                    for (int i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
                    {
                        StateMasterEntity obj = new StateMasterEntity();
                        obj.ID = Convert.ToInt32(ds.Tables[0].Rows[i]["ID"].ToString());
                        obj.NAME = ds.Tables[0].Rows[i]["NAME"].ToString();
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
        public List<CityStateMasterEntity> GetCityByState(string strstate)
        {
            string CS = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            SqlDataAdapter adapter;
            DataSet ds = new DataSet();
            List<CityStateMasterEntity> retlst = new List<CityStateMasterEntity>();
            try
            {
                using (SqlConnection con = new SqlConnection(CS))
                {
                    SqlCommand cmd = new SqlCommand("USP_GetCityByState", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@STATE", strstate);
                    con.Open();
                    adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(ds);

                    for (int i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
                    {
                        CityStateMasterEntity obj = new CityStateMasterEntity();
                        obj.ID = Convert.ToInt32(ds.Tables[0].Rows[i]["ID"].ToString());
                        obj.CITY = ds.Tables[0].Rows[i]["CITY"].ToString();
                        obj.STATE = ds.Tables[0].Rows[i]["STATE"].ToString();
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
        public List<RoleMasterEntity> GetActiveRoleList()
        {
            string CS = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            SqlDataAdapter adapter;
            DataSet ds = new DataSet();
            List<RoleMasterEntity> retlst = new List<RoleMasterEntity>();
            try
            {
                using (SqlConnection con = new SqlConnection(CS))
                {
                    SqlCommand cmd = new SqlCommand("USP_GetActiveRoleList", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(ds);

                    for (int i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
                    {
                        if (ds.Tables[0].Rows[i]["ROLE_NAME"] != DBNull.Value)
                        {
                            RoleMasterEntity obj = new RoleMasterEntity();
                            obj.ROLE_ID = Convert.ToInt32(ds.Tables[0].Rows[i]["ROLE_ID"].ToString());
                            obj.ROLE_NAME = ds.Tables[0].Rows[i]["ROLE_NAME"].ToString();
                            retlst.Add(obj);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return retlst;
        }
        
        public List<CityStateMasterEntity> GetAllCities()
        {
            string CS = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            SqlDataAdapter adapter;
            DataSet ds = new DataSet();
            List<CityStateMasterEntity> retlst = new List<CityStateMasterEntity>();
            try
            {
                using (SqlConnection con = new SqlConnection(CS))
                {
                    SqlCommand cmd = new SqlCommand("USP_GetActiveCityList", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(ds);

                    for (int i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
                    {
                        CityStateMasterEntity obj = new CityStateMasterEntity();
                        obj.ID = Convert.ToInt32(ds.Tables[0].Rows[i]["ID"].ToString());
                        obj.CITY = ds.Tables[0].Rows[i]["CITY"].ToString();
                        obj.STATE = ds.Tables[0].Rows[i]["STATE"].ToString();
                        obj.ACTIVE_STATUS = true;
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

        public List<ClientCategoryMasterEntity> GetActiveClientCategories()
        {
            string CS = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            SqlDataAdapter adapter;
            DataSet ds = new DataSet();
            List<ClientCategoryMasterEntity> retlst = new List<ClientCategoryMasterEntity>();
            try
            {
                using (SqlConnection con = new SqlConnection(CS))
                {
                    SqlCommand cmd = new SqlCommand("USP_GetActiveClientCateGoryList", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(ds);

                    for (int i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
                    {
                        ClientCategoryMasterEntity obj = new ClientCategoryMasterEntity();
                        obj.CLI_CAT_ID = Convert.ToInt32(ds.Tables[0].Rows[i]["CLI_CAT_ID"].ToString());
                        obj.CLI_CAT_NAME = ds.Tables[0].Rows[i]["CLI_CAT_NAME"].ToString();
                        obj.ACTIVE_STATUS = true;
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

        public List<ClientGroupMasterEntity> GetActiveClientGroups()
        {
            string CS = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            SqlDataAdapter adapter;
            DataSet ds = new DataSet();
            List<ClientGroupMasterEntity> retlst = new List<ClientGroupMasterEntity>();
            try
            {
                using (SqlConnection con = new SqlConnection(CS))
                {
                    SqlCommand cmd = new SqlCommand("USP_GetActiveClientGroupList", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(ds);

                    for (int i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
                    {
                        ClientGroupMasterEntity obj = new ClientGroupMasterEntity();
                        obj.CLI_GRP_ID = Convert.ToInt32(ds.Tables[0].Rows[i]["CLI_GRP_ID"].ToString());
                        obj.CLI_GRP_NAME = ds.Tables[0].Rows[i]["CLI_GRP_NAME"].ToString();
                        obj.ACTIVE_STATUS = true;
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

        public List<GenericIdNameEntity> GetActiveTaskStagesList()
        {
            string CS = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            SqlDataAdapter adapter;
            DataSet ds = new DataSet();
            List<GenericIdNameEntity> retlst = new List<GenericIdNameEntity>();
            try
            {
                using (SqlConnection con = new SqlConnection(CS))
                {
                    SqlCommand cmd = new SqlCommand("USP_GetActiveTaskStagesList", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(ds);

                    for (int i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
                    {
                        GenericIdNameEntity obj = new GenericIdNameEntity();
                        obj.ID = ds.Tables[0].Rows[i]["ID"] == DBNull.Value ? 0 : Convert.ToInt64(ds.Tables[0].Rows[i]["ID"]);
                        obj.NAME = ds.Tables[0].Rows[i]["NAME"] == DBNull.Value ? "" : ds.Tables[0].Rows[i]["NAME"].ToString();                       
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

        public List<GenericIdNameEntity> GetActiveUsersList()
        {
            string CS = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            SqlDataAdapter adapter;
            DataSet ds = new DataSet();
            List<GenericIdNameEntity> retlst = new List<GenericIdNameEntity>();
            try
            {
                using (SqlConnection con = new SqlConnection(CS))
                {
                    SqlCommand cmd = new SqlCommand("USP_GetActiveUsersList", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(ds);

                    for (int i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
                    {
                        GenericIdNameEntity obj = new GenericIdNameEntity();
                        obj.ID = ds.Tables[0].Rows[i]["ID"] == DBNull.Value ? 0 : Convert.ToInt64(ds.Tables[0].Rows[i]["ID"]);
                        obj.NAME = ds.Tables[0].Rows[i]["NAME"] == DBNull.Value ? "" : ds.Tables[0].Rows[i]["NAME"].ToString();
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

        public List<ClientSearchEntity> GetClientSearchList(string filterby, string filtertext)
        {
            string CS = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            SqlDataAdapter adapter;
            DataSet ds = new DataSet();
            List<ClientSearchEntity> retlst = new List<ClientSearchEntity>();
            try
            {
                using (SqlConnection con = new SqlConnection(CS))
                {
                    SqlCommand cmd = new SqlCommand("USP_GetClientSearchList", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@FILTERBY", filterby);
                    cmd.Parameters.AddWithValue("@FILTERTEXT", filtertext);
                    con.Open();
                    adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(ds);

                    for (int i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
                    {
                        ClientSearchEntity obj = new ClientSearchEntity();
                        obj.C_ID = ds.Tables[0].Rows[i]["C_ID"] == DBNull.Value ? 0 : Convert.ToInt64(ds.Tables[0].Rows[i]["C_ID"]);
                        obj.C_NO = ds.Tables[0].Rows[i]["C_NO"] == DBNull.Value ? 0 : Convert.ToInt64(ds.Tables[0].Rows[i]["C_NO"]);
                        obj.C_NAME = ds.Tables[0].Rows[i]["C_NAME"] == DBNull.Value ? "" : ds.Tables[0].Rows[i]["C_NAME"].ToString();
                        obj.FILE_NO = ds.Tables[0].Rows[i]["FILE_NO"] == DBNull.Value ? "" : ds.Tables[0].Rows[i]["FILE_NO"].ToString();
                        obj.PAN = ds.Tables[0].Rows[i]["PAN"] == DBNull.Value ? "" : ds.Tables[0].Rows[i]["PAN"].ToString();
                        obj.AADHAAR = ds.Tables[0].Rows[i]["AADHAAR"] == DBNull.Value ? "" : ds.Tables[0].Rows[i]["AADHAAR"].ToString();
                        obj.GSTIN = ds.Tables[0].Rows[i]["GSTIN"] == DBNull.Value ? "" : ds.Tables[0].Rows[i]["GSTIN"].ToString();
                        obj.CLI_GRP_NAME = ds.Tables[0].Rows[i]["CLI_GRP_NAME"] == DBNull.Value ? "" : ds.Tables[0].Rows[i]["CLI_GRP_NAME"].ToString();                        

                        obj.PH_NO = ds.Tables[0].Rows[i]["PH_NO"] == DBNull.Value ? "" : ds.Tables[0].Rows[i]["PH_NO"].ToString();
                        obj.MOBILE_NO1 = ds.Tables[0].Rows[i]["MOBILE_NO1"] == DBNull.Value ? "" : ds.Tables[0].Rows[i]["MOBILE_NO1"].ToString();
                        obj.MOBILE_NO2 = ds.Tables[0].Rows[i]["MOBILE_NO2"] == DBNull.Value ? "" : ds.Tables[0].Rows[i]["MOBILE_NO2"].ToString();
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
        public List<string> GetCurrentDate()
        {
            string CS = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            SqlDataAdapter adapter;
            DataSet ds = new DataSet();
            List<string> lstvalues = new List<string>();
            try
            {
                using (SqlConnection con = new SqlConnection(CS))
                {
                    SqlCommand cmd = new SqlCommand("USP_GetCurrentDate", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(ds);
                    for (int i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
                    {
                        lstvalues.Add(ds.Tables[0].Rows[i]["DATE"].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lstvalues;
        }
        public List<string> GetLatestTrasnsactionNumber(string str)
        {
            string CS = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            SqlDataAdapter adapter;
            DataSet ds = new DataSet();
            List<string> lstvalues = new List<string>();
            try
            {
                using (SqlConnection con = new SqlConnection(CS))
                {
                    SqlCommand cmd = new SqlCommand("USP_GetTransNo", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@STR", str);
                    con.Open();
                    adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(ds);
                    for (int i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
                    {
                        lstvalues.Add(string.Format("{0}-{1}", ds.Tables[0].Rows[i]["NUMBER"].ToString(), ds.Tables[0].Rows[i]["DATE"].ToString()));
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lstvalues;
        }
        public List<GenericIdNameEntity> GetActivePaymodeList()
        {
            string CS = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            SqlDataAdapter adapter;
            DataSet ds = new DataSet();
            List<GenericIdNameEntity> retlst = new List<GenericIdNameEntity>();
            try
            {
                using (SqlConnection con = new SqlConnection(CS))
                {
                    SqlCommand cmd = new SqlCommand("USP_GetActivePaymodeList", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(ds);

                    for (int i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
                    {
                        GenericIdNameEntity obj = new GenericIdNameEntity();
                        obj.ID = ds.Tables[0].Rows[i]["ID"] == DBNull.Value ? 0 : Convert.ToInt64(ds.Tables[0].Rows[i]["ID"]);
                        obj.NAME = ds.Tables[0].Rows[i]["NAME"] == DBNull.Value ? "" : ds.Tables[0].Rows[i]["NAME"].ToString();
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
