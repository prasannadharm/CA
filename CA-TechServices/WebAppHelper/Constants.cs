using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace CA_TechServices.WebAppHelper
{
    public class Constants
    {
        public static bool isDbLogging
        {
            get { return Convert.ToBoolean(ConfigurationManager.AppSettings["isDbLogging"].ToString()); }
        }
        public static string ApplicationName
        {
            get { return ConfigurationManager.AppSettings["ApplicationName"].ToString(); }
        }
    }
}