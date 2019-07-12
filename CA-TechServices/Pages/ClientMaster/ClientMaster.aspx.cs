using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CA_TechService.Common.Generic;
using CA_TechService.Common.Transport.ClientMaster;
using CA_TechService.Data.DataSource.ClientMaster;
using System.Web.Services;

namespace CA_TechServices.Pages.ClientMaster
{
    public partial class ClientMaster : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod]
        public static ClientMasterEntity[] GetData() //Show the details of the data after insert in HTML Table
        {
            var details = new List<ClientMasterEntity>();
            try
            {
                details = new ClientMasterDAO().GetClientList();
            }
            catch (Exception ex)
            {
                // details.Add(new DbStatusEntity(ex.Message));
            }
            return details.ToArray();
        }

        [WebMethod]
        public static ClientMasterEntity[] EditData(int id)
        {
            var details = new List<ClientMasterEntity>();
            try
            {
                details = new ClientMasterDAO().EditClient(id);
            }
            catch (Exception ex)
            {
                //details.Add(new DbStatusEntity(ex.Message));
            }
            return details.ToArray();
        }

        [WebMethod]
        public static DbStatusEntity[] UpdateData(ClientMasterEntity obj, int id) //Update data in database  
        {
            var details = new List<DbStatusEntity>();
            try
            {
                details.Add(new ClientMasterDAO().UpdateClient(obj, id));
            }
            catch (Exception ex)
            {
                details.Clear();
                details.Add(new DbStatusEntity(ex.Message));
            }
            return details.ToArray();

        }

        [WebMethod]
        public static DbStatusEntity[] InsertData(ClientMasterEntity obj)
        {
            var details = new List<DbStatusEntity>();
            try
            {
                details.Add(new ClientMasterDAO().InsertClient(obj));
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
                details.Add(new ClientMasterDAO().DeleteClient(id));
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