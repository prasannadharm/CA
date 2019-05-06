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
    public class ChangePasswordDAO
    {
        public DbStatusEntity ChangePassword(ChangePasswordEntity obj)
        {
            DbStatusEntity objreturn = new DbStatusEntity();
            string CS = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            try
            {
                using (SqlConnection con = new SqlConnection(CS))
                {
                    SqlCommand cmd = new SqlCommand("USP_UpdateNewPassword", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@USER_ID", obj.USER_ID);
                    cmd.Parameters.AddWithValue("@OLD_PASSWORD", CryptographyHelper.Instance.Encrypt(obj.OLD_PASSWORD));
                    cmd.Parameters.AddWithValue("@NEW_PASSWORD", CryptographyHelper.Instance.Encrypt(obj.NEW_PASSWORD));

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
