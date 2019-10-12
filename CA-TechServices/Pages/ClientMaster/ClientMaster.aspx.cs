using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CA_TechService.Common.Generic;
using CA_TechService.Common.Transport.ClientMaster;
using CA_TechService.Data.DataSource.ClientMaster;
using CA_TechService.Common.Transport.CityState;
using System.Web.Services;
using CA_TechService.Data.DataSource;

namespace CA_TechServices.Pages.ClientMaster
{
    public partial class ClientMaster : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod]
        public static ClientMasterEntity[] GetData(ClientMasterSearchEntity obj) //Show the details of the data after insert in HTML Table
        {
            var details = new List<ClientMasterEntity>();
            try
            {
                details = new ClientMasterDAO().GetClientList(obj);
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

        [WebMethod]
        public static StateMasterEntity[] GetStates() //Show the details of the data after insert in HTML Table
        {
            var details = new List<StateMasterEntity>();
            try
            {
                details = new GenericDAO().GetStateList();
            }
            catch (Exception ex)
            {
                // details.Add(new DbStatusEntity(ex.Message));
            }
            return details.ToArray();
        }

        [WebMethod]
        public static CityStateMasterEntity[] GetCityByState(string str) //Show the details of the data after insert in HTML Table
        {
            var details = new List<CityStateMasterEntity>();
            try
            {
                details = new GenericDAO().GetCityByState(str);
            }
            catch (Exception ex)
            {
                // details.Add(new DbStatusEntity(ex.Message));
            }
            return details.ToArray();
        }

        [WebMethod]
        public static ClientGroupMasterEntity[] GetActiveClientGroups() //Show the details of the data after insert in HTML Table
        {
            var details = new List<ClientGroupMasterEntity>();
            try
            {
                details = new GenericDAO().GetActiveClientGroups();
            }
            catch (Exception ex)
            {
                // details.Add(new DbStatusEntity(ex.Message));
            }
            return details.ToArray();
        }

        [WebMethod]
        public static ClientCategoryMasterEntity[] GetActiveClientCategories() //Show the details of the data after insert in HTML Table
        {
            var details = new List<ClientCategoryMasterEntity>();
            try
            {
                details = new GenericDAO().GetActiveClientCategories();
            }
            catch (Exception ex)
            {
                // details.Add(new DbStatusEntity(ex.Message));
            }
            return details.ToArray();
        }

        [WebMethod]
        public static ClientDocsEntity[] GetClientDocsData(long id) //Show the details of the data after insert in HTML Table
        {
            var details = new List<ClientDocsEntity>();
            try
            {
                details = new ClientMasterDAO().GetClientDocs(id);
            }
            catch (Exception ex)
            {
                // details.Add(new DbStatusEntity(ex.Message));
            }
            return details.ToArray();
        }


    }
}