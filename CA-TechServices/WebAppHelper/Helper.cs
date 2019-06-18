#region Imports
using CA_TechService.Common.Transport.Logging;
using CA_TechService.Data.DataSource.Logging;
using System;
#endregion
namespace CA_TechServices.WebAppHelper
{
    public class Helper 
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        
        #region ExceptionHandling
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
              FileLogging(exception);
            
        }

        public static void FileLogging(ExceptionInfo exception)
        {
            logger.Error("Application Name : {0} | Programme Name : {1} | Machine Name : {2} | Exception Message : {3} | Exception Source : {4} | Custom Message : {5}",
                exception.ApplicationName,
                exception.ProgrammeName,
                exception.MachineName,
                exception.ExceptionMessage,
                exception.ExceptionSource,
                exception.CustomMessage
                );
        }
        #endregion
    }
}