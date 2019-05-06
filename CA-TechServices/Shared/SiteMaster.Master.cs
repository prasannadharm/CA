#region Imports
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using CA_TechService.Common.Transport.Rules;
using CA_TechService.Data.DataSource.Login;
using CA_TechService.Common.Transport.Login;
#endregion
namespace CA_TechServices.Shared
{
    public partial class SiteMaster : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["USER_ID"] == null)
            {
                Response.Redirect("~/Pages/Login/UserLogin.aspx");
            }
            if (!this.IsPostBack)
            {                
                //Code to Build menu
                GetUserMenuFromDB();
                List<UserMenuEntity> objMenuList = this.GetUserMenuDataFromList(0);
                PopulateMenu(objMenuList, 0, null);
                if (Session["USER_DETAILS"] != null)
                {
                    lblLoginName.Text = ((LoginEntity)Session["USER_DETAILS"]).EMAIL;
                }
            }
        }

        #region Menu Loading Code
        private void GetUserMenuFromDB()
        {
            Session["MENU"] = null;
            if (Session["USER_ID"] != null)
            {
                List<UserMenuEntity> objlst = new LoginDAO().GetMenuForUser(Convert.ToInt32(Session["USER_ID"]));
                Session["MENU"] = objlst;
            }
        }

        private List<UserMenuEntity> GetUserMenuDataFromList(int parentMenuId)
        {
            List<UserMenuEntity> objMenuList = Session["MENU"] != null ? (List<UserMenuEntity>)Session["MENU"] : new List<UserMenuEntity>();
            return objMenuList.Where(p =>p.PARENT_MENU_ID==parentMenuId).ToList();            
        }

        private void PopulateMenu(List<UserMenuEntity> objlst, int parentMenuId, MenuItem parentMenuItem)
        {
            string currentPage = Path.GetFileName(Request.Url.AbsolutePath);
            foreach (UserMenuEntity row in objlst)
            {
                MenuItem menuItem = new MenuItem
                {
                    Value = row.MENU_ID.ToString(),
                    Text = row.TITLE.ToString(),
                    NavigateUrl = row.URL.ToString(),
                    Selected = row.URL.ToString().EndsWith(currentPage, StringComparison.CurrentCultureIgnoreCase)
                };
                if (parentMenuId == 0)
                {
                    Menu1.Items.Add(menuItem);
                    List<UserMenuEntity> objMenuList = this.GetUserMenuDataFromList(int.Parse(menuItem.Value));
                    PopulateMenu(objMenuList, int.Parse(menuItem.Value), menuItem);
                }
                else
                {
                    parentMenuItem.ChildItems.Add(menuItem);
                }
            }
        }
        #endregion
    }
}