using CA_TechService.Common.Generic;
using CA_TechService.Common.Transport.ClientMaster;
using CA_TechService.Data.DataSource.ClientMaster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CA_TechServices.Pages.ClientMaster
{
    public partial class ClientGroup : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod]
        public static ClientGroupMasterEntity[] GetData() //Show the details of the data after insert in HTML Table
        {
            var details = new List<ClientGroupMasterEntity>();
            try
            {
                details = new ClientGroupMasterDAO().GetClientGroupList();
            }
            catch (Exception ex)
            {
                // details.Add(new DbStatusEntity(ex.Message));
            }
            return details.ToArray();
        }
       
        [WebMethod]
        public static ClientGroupMasterEntity[] EditData(int id)
        {
            var details = new List<ClientGroupMasterEntity>();
            try
            {
                details = new ClientGroupMasterDAO().EditClientGroup(id);
            }
            catch (Exception ex)
            {
                //details.Add(new DbStatusEntity(ex.Message));
            }
            return details.ToArray();
        }

        [WebMethod]
        public static DbStatusEntity[] UpdateData(ClientGroupMasterEntity obj, int id) //Update data in database  
        {
            var details = new List<DbStatusEntity>();
            try
            {
                details.Add(new ClientGroupMasterDAO().UpdateClientGroup(obj, id));
            }
            catch (Exception ex)
            {
                details.Clear();
                details.Add(new DbStatusEntity(ex.Message));
            }
            return details.ToArray();

        }

        [WebMethod]
        public static DbStatusEntity[] InsertData(ClientGroupMasterEntity obj)
        {
            var details = new List<DbStatusEntity>();
            try
            {
                details.Add(new ClientGroupMasterDAO().InsertClientGroup(obj));
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
                details.Add(new ClientGroupMasterDAO().DeleteClientGroup(id));
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