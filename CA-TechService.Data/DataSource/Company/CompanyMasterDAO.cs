using CA_TechService.Common.Generic;
using CA_TechService.Common.Transport.Company;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA_TechService.Data.DataSource.Company
{
    public class CompanyMasterDAO
    {
        public List<CompanyMasterEntity> GetCompanyMasterList()
        {
            string CS = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            SqlDataAdapter adapter;
            DataSet ds = new DataSet();
            List<CompanyMasterEntity> retlst = new List<CompanyMasterEntity>();
            try
            {
                using (SqlConnection con = new SqlConnection(CS))
                {
                    SqlCommand cmd = new SqlCommand("USP_GetCompanyMasterList", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(ds);

                    for (int i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
                    {
                        CompanyMasterEntity obj = new CompanyMasterEntity();
                        obj.COMPANY_NAME = ds.Tables[0].Rows[i]["COMPANY_NAME"] == DBNull.Value ? "" : ds.Tables[0].Rows[i]["COMPANY_NAME"].ToString();
                        obj.ADDRESS = ds.Tables[0].Rows[i]["ADDRESS"] == DBNull.Value ? "" : ds.Tables[0].Rows[i]["ADDRESS"].ToString();
                        obj.CITY = ds.Tables[0].Rows[i]["CITY"] == DBNull.Value ? "" : ds.Tables[0].Rows[i]["CITY"].ToString();
                        obj.STATE = ds.Tables[0].Rows[i]["STATE"] == DBNull.Value ? "" : ds.Tables[0].Rows[i]["STATE"].ToString();
                        obj.PIN_NO = ds.Tables[0].Rows[i]["PIN_NO"] == DBNull.Value ? "" : ds.Tables[0].Rows[i]["PIN_NO"].ToString();
                        obj.GSTIN = ds.Tables[0].Rows[i]["GSTIN"] == DBNull.Value ? "" : ds.Tables[0].Rows[i]["GSTIN"].ToString();
                        obj.EMAIL = ds.Tables[0].Rows[i]["EMAIL"] == DBNull.Value ? "" : ds.Tables[0].Rows[i]["EMAIL"].ToString();
                        obj.TELEPHONE = ds.Tables[0].Rows[i]["TELEPHONE"] == DBNull.Value ? "" : ds.Tables[0].Rows[i]["TELEPHONE"].ToString();
                        obj.FAX = ds.Tables[0].Rows[i]["FAX"] == DBNull.Value ? "" : ds.Tables[0].Rows[i]["FAX"].ToString();
                        obj.WEB = ds.Tables[0].Rows[i]["WEB"] == DBNull.Value ? "" : ds.Tables[0].Rows[i]["WEB"].ToString();
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


        public DbStatusEntity UpdateCompany(CompanyMasterEntity obj)
        {
            DbStatusEntity objreturn = new DbStatusEntity();
            string CS = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            try
            {
                using (SqlConnection con = new SqlConnection(CS))
                {
                    SqlCommand cmd = new SqlCommand("USP_UpdateCompanyMaster", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@COMPANY_NAME", obj.COMPANY_NAME);
                    cmd.Parameters.AddWithValue("@ADDRESS", obj.ADDRESS);
                    cmd.Parameters.AddWithValue("@CITY", obj.CITY);
                    cmd.Parameters.AddWithValue("@STATE", obj.STATE);
                    cmd.Parameters.AddWithValue("@PIN_NO", obj.PIN_NO);
                    cmd.Parameters.AddWithValue("@TELEPHONE", obj.TELEPHONE);
                    cmd.Parameters.AddWithValue("@FAX", obj.FAX);
                    cmd.Parameters.AddWithValue("@EMAIL", obj.EMAIL);
                    cmd.Parameters.AddWithValue("@WEB", obj.WEB);
                    cmd.Parameters.AddWithValue("@GSTIN", obj.GSTIN);

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
