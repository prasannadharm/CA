using CA_TechService.Common.Generic;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace CA_TechService.Data.DataSource.EmailServer
{
    public class EmailServerDAO
    {
        public EmailEntity GetEmailServerDetails()
        {
            string CS = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            SqlDataAdapter adapter;
            DataSet ds = new DataSet();
            EmailEntity retobj = new EmailEntity();
            try
            {
                using (SqlConnection con = new SqlConnection(CS))
                {
                    SqlCommand cmd = new SqlCommand("USP_GetEmailServerDetails", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(ds);

                    for (int i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
                    {
                        EmailEntity obj = new EmailEntity();
                        obj.Port = ds.Tables[0].Rows[i]["Port"] == DBNull.Value ? 587 : Convert.ToInt32(ds.Tables[0].Rows[i]["Port"].ToString());
                        obj.Host = ds.Tables[0].Rows[i]["Host"] == DBNull.Value ? "smtp.gmail.com" : ds.Tables[0].Rows[i]["Host"].ToString();
                        obj.UserName = ds.Tables[0].Rows[i]["UserName"] == DBNull.Value ? "" : ds.Tables[0].Rows[i]["UserName"].ToString();
                        obj.UserPassword = ds.Tables[0].Rows[i]["UserPassword"] == DBNull.Value ? "" : ds.Tables[0].Rows[i]["UserPassword"].ToString();
                        obj.EnableSsl = Convert.ToBoolean(ds.Tables[0].Rows[i]["EnableSsl"]);
                        retobj = obj;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return retobj;
        }
    }
}
