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
    public partial class ClientCategory : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod]
        public static ClientCategoryMasterEntity[] GetData() //Show the details of the data after insert in HTML Table
        {
            var details = new List<ClientCategoryMasterEntity>();
            try
            {
                details = new ClientCategoryMasterDAO().GetClientCategoryList();
            }
            catch (Exception ex)
            {
                // details.Add(new DbStatusEntity(ex.Message));
            }
            return details.ToArray();
        }

        [WebMethod]
        public static ClientCategoryMasterEntity[] EditData(int id)
        {
            var details = new List<ClientCategoryMasterEntity>();
            try
            {
                details = new ClientCategoryMasterDAO().EditClientCategory(id);
            }
            catch (Exception ex)
            {
                //details.Add(new DbStatusEntity(ex.Message));
            }
            return details.ToArray();
        }

        [WebMethod]
        public static DbStatusEntity[] UpdateData(ClientCategoryMasterEntity obj, int id) //Update data in database  
        {
            var details = new List<DbStatusEntity>();
            try
            {
                details.Add(new ClientCategoryMasterDAO().UpdateClientCategory(obj, id));
            }
            catch (Exception ex)
            {
                details.Clear();
                details.Add(new DbStatusEntity(ex.Message));
            }
            return details.ToArray();

        }

        [WebMethod]
        public static DbStatusEntity[] InsertData(ClientCategoryMasterEntity obj)
        {
            var details = new List<DbStatusEntity>();
            try
            {
                details.Add(new ClientCategoryMasterDAO().InsertClientCategory(obj));
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
                details.Add(new ClientCategoryMasterDAO().DeleteClientCategory(id));
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