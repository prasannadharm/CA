#region Imports
using System;
using CA_TechService.Data.DataSource.Login;
using CA_TechService.Common.Transport.Login;
using CA_TechServices.WebAppHelper;
#endregion

namespace CA_TechServices.Pages.Login
{
    public partial class UserLogin : System.Web.UI.Page
    {
        #region Page_Load
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                try
                {
                    Session.Abandon();
                    Session.Clear();
                    Session.RemoveAll();                   
                    Session["MENU"] = null;
                    Session["USER_ID"] = null;
                    Session["USER_DETAILS"] = null;
                }
                catch (Exception ex) { Helper.ExceptionHandling(ex,""); }
            }
        }
        #endregion

        #region btnLogin_Click
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            LoginEntity objlogin = new LoginEntity();
            objlogin.EMAIL = txtEmail.Text;
            objlogin.USER_PASSWORD = txtPassword.Text;
            try
            {
                objlogin = new LoginDAO().CheckLogin(objlogin);
                if (objlogin.RESULT.Equals(1))
                {
                    lblMessage.Text = objlogin.MESSAGE;
                    Session["USER_ID"] = objlogin.USER_ID;
                    Session["USER_DETAILS"] = objlogin;
                    Response.Redirect("../Dashboard/UserDashboard.aspx");

                }
                else
                {
                    Session["USER_ID"] = null;
                    Session["USER_DETAILS"] = null;
                    lblMessage.Text = objlogin.MESSAGE;
                    txtPassword.Focus();
                }
            }
            catch(Exception ex){ Helper.ExceptionHandling(ex,""); }
            
        }
        #endregion
    }
}