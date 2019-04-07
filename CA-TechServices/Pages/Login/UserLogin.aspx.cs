using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CA_TechService.Data.DataSource.Login;
using CA_TechService.Common.Transport.Login;


namespace CA_TechServices.Pages.Login
{
    public partial class UserLogin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                Session["MENU"] = null;
                Session["USER_ID"] = null;
                Session["USER_DETAILS"] = null;
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            LoginEntity objlogin = new LoginEntity();
            objlogin.EMAIL = txtEmail.Text;
            objlogin.USER_PASSWORD = txtPassword.Text;
            objlogin = new LoginDAO().CheckLogin(objlogin);
            if (objlogin.RESULT == 1)
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
    }
}