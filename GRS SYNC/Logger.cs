using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Configuration;
using System.Windows.Forms;


namespace GRS_SYNC
{
    public class Logger
    {
        #region Write Log
        public static void Log(string msg, bool isException)
        {
            StreamWriter fileWriter = null;
            try
            {
                DirectoryInfo di = new DirectoryInfo(ErrorFilePath);
                if (!di.Exists)
                {
                    di.Create();
                }
                if (!File.Exists(ErrorFileName))
                {
                    fileWriter = new StreamWriter(ErrorFileName);
                    if (string.IsNullOrEmpty(msg))
                    {
                        fileWriter.WriteLine("-------------------------------------------------");
                    }
                    else
                    {
                        if (isException)
                            fileWriter.WriteLine("Exception :- " + DateTime.Now + " :: " + msg);
                        else
                            fileWriter.WriteLine("Message   :- " + DateTime.Now + " :: " + msg);
                    }
                }
                else
                {
                    fileWriter = File.AppendText(ErrorFileName);
                    if (string.IsNullOrEmpty(msg))
                    {
                        fileWriter.WriteLine("-------------------------------------------------");
                    }
                    else
                    {
                        if (isException)
                            fileWriter.WriteLine("Exception :- " + DateTime.Now + " :: " + msg);
                        else
                            fileWriter.WriteLine("Message   :- " + DateTime.Now + " :: " + msg);
                    }
                }
            }
            catch (Exception)
            {
            }
            finally
            {
                if (fileWriter != null)
                    fileWriter.Close();
            }
        }
        #endregion

        #region Error File Name
        private static string ErrorFileName
        {
            get
            {
                return ErrorFilePath + "\\" + ConfigurationManager.AppSettings["ErrorFileName"] + GetDateString;
            }
        }
        #endregion

        #region Error File Path
        private static string ErrorFilePath
        {
            get
            {
                return Application.StartupPath + "\\LOG"; //ConfigurationManager.AppSettings["ErrorFilePath"];
            }
        }
        #endregion

        #region Get Date String
        private static string GetDateString
        {
            get
            {
                return "_" + DateTime.Today.ToString("dd-MM-yyyy") + FileExtension;
            }
        }
        #endregion

        #region File Extension
        private static string FileExtension
        {
            get
            {
                return ".txt";
            }
        }
        #endregion
    }
}
