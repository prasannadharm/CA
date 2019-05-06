using CA_TechService.Common.Generic;
using CA_TechService.Common.Transport.CityState;
using CA_TechService.Common.Transport.Company;
using CA_TechService.Data.DataSource;
using CA_TechService.Data.DataSource.Company;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CA_TechServices.Pages.Company
{
    public partial class Company : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod]
        public static CompanyMasterEntity[] GetData() //Show the details of the data after insert in HTML Table
        {
            var details = new List<CompanyMasterEntity>();
            try
            {
                details = new CompanyMasterDAO().GetCompanyMasterList();
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
        public static DbStatusEntity[] UpdateData(CompanyMasterEntity obj) //Update data in database  
        {
            var details = new List<DbStatusEntity>();
            try
            {
                details.Add(new CompanyMasterDAO().UpdateCompany(obj));
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