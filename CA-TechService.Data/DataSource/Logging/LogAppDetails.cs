using CA_TechService.Common.Transport.Logging;
using CA_TechService.Data.Query.Logging;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA_TechService.Data.DataSource.Logging
{
  public class LogAppDetails
    {
        public void ExceptionLogging(ExceptionInfo objExceptionInfo)
        {
            string CS = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                SqlCommand cmd = new SqlCommand(LoggingQueries.ExceptionLoggingQuery, con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                cmd.Parameters.AddWithValue("@APPLICATIONNAME",  objExceptionInfo.ApplicationName);
                cmd.Parameters.AddWithValue("@PROGRAMMENAME",    objExceptionInfo.ProgrammeName);
                cmd.Parameters.AddWithValue("@MACHINENAME",      objExceptionInfo.MachineName);
                cmd.Parameters.AddWithValue("@EXCEPTIONMESSAGE", objExceptionInfo.ExceptionMessage);
                cmd.Parameters.AddWithValue("@EXCEPTIONSORCE",   objExceptionInfo.ExceptionSource);
                cmd.Parameters.AddWithValue("@CUSTOMMESSAGE",    objExceptionInfo.CustomMessage);
                cmd.ExecuteNonQuery();
                
            }
        }
    }
}
