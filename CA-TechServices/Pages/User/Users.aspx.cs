using CA_TechService.Common.Generic;
using CA_TechService.Common.Transport.Roles;
using CA_TechService.Common.Transport.User;
using CA_TechService.Data.DataSource;
using CA_TechService.Data.DataSource.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CA_TechServices.Pages.User
{
    public partial class Users : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod]
        public static UserMasterEntity[] GetData() //Show the details of the data after insert in HTML Table
        {
            var details = new List<UserMasterEntity>();
            try
            {
                details = new UserMasterDAO().GetUserList();
            }
            catch (Exception ex)
            {
                // details.Add(new DbStatusEntity(ex.Message));
            }
            return details.ToArray();
        }


        [WebMethod]
        public static RoleMasterEntity[] GetRoles() //Show the details of the data after insert in HTML Table
        {
            var details = new List<RoleMasterEntity>();
            try
            {
                details = new GenericDAO().GetActiveRoleList();
            }
            catch (Exception ex)
            {
                // details.Add(new DbStatusEntity(ex.Message));
            }
            return details.ToArray();
        }

        [WebMethod]
        public static UserMasterEntity[] EditData(int id)
        {
            var details = new List<UserMasterEntity>();
            try
            {
                details = new UserMasterDAO().EditUserMaster(id);
            }
            catch (Exception ex)
            {
                //details.Add(new DbStatusEntity(ex.Message));
            }
            return details.ToArray();
        }

        [WebMethod]
        public static DbStatusEntity[] UpdateData(UserMasterEntity obj, int id) //Update data in database  
        {
            var details = new List<DbStatusEntity>();
            try
            {
                details.Add(new UserMasterDAO().UpdateUserMaster(obj, id));
            }
            catch (Exception ex)
            {
                details.Clear();
                details.Add(new DbStatusEntity(ex.Message));
            }
            return details.ToArray();

        }

        [WebMethod]
        public static DbStatusEntity[] InsertData(UserMasterEntity obj)
        {
            var details = new List<DbStatusEntity>();
            try
            {
                details.Add(new UserMasterDAO().InsertUserMaster(obj));
            }
            catch (Exception ex)
            {
                details.Clear();
                details.Add(new DbStatusEntity(ex.Message));
            }
            return details.ToArray();
        }

        [WebMethod]
        public static DbStatusEntity[] DeleteData(int id)
        {
            var details = new List<DbStatusEntity>();
            try
            {
                details.Add(new UserMasterDAO().DeleteUser(id));
            }
            catch (Exception ex)
            {
                details.Clear();
                details.Add(new DbStatusEntity(ex.Message));
            }
            return details.ToArray();
        }
    }
}