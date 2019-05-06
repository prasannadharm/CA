using CA_TechService.Common.Generic;
using CA_TechService.Common.Security.Encryption;
using CA_TechService.Common.Transport.User;
using CA_TechService.Data.DataSource.EmailServer;
using CA_TechService.Data.DataSource.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CA_TechServices.Pages.Login
{
    public partial class ForgotPassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod]
        public static DbStatusEntity[] ForgotPasword(ForgotPasswordEntity email)
        {
            var details = new List<DbStatusEntity>();
            SendEmail objsendemail = new SendEmail();
            try
            {
                DbStatusEntity objret = new ForgotPasswordDAO().GetFogotPassword(email.EMAIL);
                if (objret.RESULT == 1)
                {
                    string password = CryptographyHelper.Instance.Decrypt(objret.MSG);
                    string strsubject = "Password retrival from CA website.";
                    string strbody = "Hi,<br><br>Your Email : <b>" + email.EMAIL + "</b><br>Your passowrd : <b>" + password + "</b><br><br>Team CA Website";
                    if (objsendemail.SendMail(email.EMAIL, strsubject, strbody) == false)
                    {
                        objret.RESULT = 0;
                        objret.MSG = "Mail sending failue";
                    }
                    else
                    {
                        objret.MSG = "Login details email sent to " + email.EMAIL;
                    }
                }
                details.Add(objret);
            }
            catch (Exception ex)
            {
                details.Add(new DbStatusEntity(ex.Message));
            }
            return details.ToArray();
        }
    }
}