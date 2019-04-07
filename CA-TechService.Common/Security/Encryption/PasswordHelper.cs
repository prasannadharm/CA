#region ProgramInformation
/*------------------------------------------------------------------------------------------------------------------------------
	Module & SubModule Name	: Common
    Package Name            : TFO.COMMON.Security
	Description	            : Helper class for generating Random Password.
    Author(s)               : Satchidananda
    Date Created        	: Nov 11, 2010
------------------------------------------------------------------------------------------------------------------------------*/
#endregion

#region Imports
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
#endregion

namespace CA_TechService.Common.Security.Encryption
{
    public class PasswordHelper
    {
        #region
        //added by michael for carefore
        public static string GeneratePassword(int length)
        {
            string Pool = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz~!@#$%^*_+}{:?/*.-|;0123456789";
            string Password;
            Random rand = new Random();
            while (true)
            {
                Password = string.Empty;
                for (int iCount = 0; iCount < length; iCount++)
                {
                    Password += Pool.Substring(rand.Next(Pool.Length), 1);
                }

                int iMatch = 0;
                Regex objPatterns;

                objPatterns = new Regex("[0-9]");
                if (objPatterns.IsMatch(Password))
                    iMatch += 1;

                objPatterns = new Regex("[a-z]");
                if (objPatterns.IsMatch(Password))
                    iMatch += 1;

                objPatterns = new Regex("[A-Z]");
                if (objPatterns.IsMatch(Password))
                    iMatch += 1;

                objPatterns = new Regex("[\\W_]");
                if (objPatterns.IsMatch(Password))
                    iMatch += 1;

                if ((iMatch >= 3) && (Password.Length >= length))
                    break;
            }
            return Password;
        } 
        #endregion
    }
}
