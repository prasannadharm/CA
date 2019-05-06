using CA_TechService.Common.Transport.Logging;
using CA_TechService.Data.DataSource.Logging;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CA_TechServices.WebAppHelper
{
    public class Helper 
    {
        public static void ExceptionHandling(Exception ex,string customMessage)
        {
            bool logToDB = Constants.isDbLogging;
            LogAppDetails dataSource = new LogAppDetails();
            ExceptionInfo exception = new ExceptionInfo
            {
                ApplicationName  = Constants.ApplicationName, 
                ProgrammeName    = ex.TargetSite.ToString(),
                MachineName      = Environment.MachineName, 
                ExceptionMessage = ex.Message,
                ExceptionSource  = ex.Source,
                CustomMessage    = customMessage
            };
            if(logToDB)
            dataSource.ExceptionLogging(exception);
            else
            {
                //to do , implement Nlogging
            }
        }
    }
}