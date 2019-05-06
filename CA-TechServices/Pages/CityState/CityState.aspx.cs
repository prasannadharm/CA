using CA_TechService.Common.Generic;
using CA_TechService.Common.Transport.CityState;
using CA_TechService.Data.DataSource;
using CA_TechService.Data.DataSource.CityState;
using System;
using System.Collections.Generic;
using System.Web.Services;

namespace CA_TechServices.Pages.CityState
{
    public partial class CityState : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod]
        public static CityStateMasterEntity[] GetData() //Show the details of the data after insert in HTML Table
        {
            var details = new List<CityStateMasterEntity>();
            try
            {
                details = new CityStateMasterDAO().GetCityStateList();
            }
            catch (Exception ex)
            {
                // details.Add(new DbStatusEntity(ex.Message));
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
        public static CityStateMasterEntity[] EditData(int id)
        {
            var details = new List<CityStateMasterEntity>();
            try
            {
                details = new CityStateMasterDAO().EditCityState(id);
            }
            catch (Exception ex)
            {
                //details.Add(new DbStatusEntity(ex.Message));
            }
            return details.ToArray();
        }

        [WebMethod]
        public static DbStatusEntity[] UpdateData(CityStateMasterEntity obj, int id) //Update data in database  
        {
            var details = new List<DbStatusEntity>();
            try
            {
                details.Add(new CityStateMasterDAO().UpdateCityState(obj, id));
            }
            catch (Exception ex)
            {
                details.Clear();
                details.Add(new DbStatusEntity(ex.Message));
            }
            return details.ToArray();

        }

        [WebMethod]
        public static DbStatusEntity[] InsertData(CityStateMasterEntity obj)
        {
            var details = new List<DbStatusEntity>();
            try
            {
                details.Add(new CityStateMasterDAO().InsertCityState(obj));
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
                details.Add(new CityStateMasterDAO().DeleteCityState(id));
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