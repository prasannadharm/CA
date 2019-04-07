using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CA_TechService.Common.Transport.Login;
using CA_TechService.Common.Security.Encryption;
using System.Configuration;
using System.Data;

namespace CA_TechService.Data.DataSource.Login
{
    public class LoginDAO
    {
        public LoginEntity CheckLogin(LoginEntity objLogin)
        {
            string CS = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            SqlDataAdapter adapter;
            DataSet ds = new DataSet();
            using (SqlConnection con = new SqlConnection(CS))
            {
                objLogin.RESULT = 0;
                objLogin.MESSAGE = "Please valid User credentials.";

                SqlCommand cmd = new SqlCommand("USP_CheckLogin", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                cmd.Parameters.AddWithValue("@Email", objLogin.EMAIL);
                cmd.Parameters.AddWithValue("@Password", CryptographyHelper.Instance.Encrypt(objLogin.USER_PASSWORD));
                adapter = new SqlDataAdapter(cmd);
                adapter.Fill(ds);

                for (int i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
                {
                    if (ds.Tables[0].Rows[i]["RESULT"].ToString().Equals("1"))
                    {
                        objLogin.RESULT = 1;
                        objLogin.MESSAGE = ds.Tables[0].Rows[i]["MESSAGE"].ToString();
                        objLogin.USER_ID = Convert.ToInt32(ds.Tables[0].Rows[i]["USER_ID"].ToString());
                        objLogin.USER_NAME = ds.Tables[0].Rows[i]["USER_NAME"].ToString();
                        objLogin.USER_PASSWORD= ds.Tables[0].Rows[i]["USER_PASSWORD"].ToString();
                        objLogin.NAME = ds.Tables[0].Rows[i]["NAME"].ToString();
                        objLogin.EMAIL = ds.Tables[0].Rows[i]["EMAIL"].ToString();
                        objLogin.MOBILE_NO = ds.Tables[0].Rows[i]["MOBILE_NO"].ToString();
                        objLogin.ROLE_ID = ds.Tables[0].Rows[i]["ROLE_ID"] != DBNull.Value ? Convert.ToInt32(ds.Tables[0].Rows[i]["ROLE_ID"].ToString()) : 0;
                        objLogin.ACTIVE_STATUS = true;
                    }
                    else
                    {
                        objLogin.RESULT = 0;
                        objLogin.MESSAGE = ds.Tables[0].Rows[i]["MESSAGE"].ToString();
                    }                    
                }               
            }
            return objLogin;
        }
    }
}
