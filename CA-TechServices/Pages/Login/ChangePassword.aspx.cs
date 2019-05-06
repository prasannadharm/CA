using CA_TechService.Common.Generic;
using CA_TechService.Common.Transport.User;
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
    public partial class ChangePassword : System.Web.UI.Page
    {
        public static int userid = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["USER_ID"] == null)
            {
                Response.Redirect("~/Pages/UserLogin.aspx");
            }
            else
            {
                userid = Convert.ToInt32(Session["USER_ID"]);
            }
            if (!this.IsPostBack)
            {

            }
        }

        [WebMethod]
        public static DbStatusEntity[] UpdateData(ChangePasswordEntity obj)
        {
            var details = new List<DbStatusEntity>();
            try
            {
                obj.USER_ID = userid;
                details.Add(new ChangePasswordDAO().ChangePassword(obj));
            }
            catch (Exception ex)
            {
                //details.Add(new DbStatusEntity(ex.Message));
            }
            return details.ToArray();
        }
    }
}