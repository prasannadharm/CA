using CA_TechService.Common.Generic;
using CA_TechService.Common.Transport.Bill;
using CA_TechService.Common.Transport.Generic;
using CA_TechService.Data.DataSource;
using CA_TechService.Data.DataSource.Bill;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CA_TechServices.Pages.Bill
{
    public partial class BillSettlement : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod]
        public static BSListingEntity[] GetData(string fromdate, string todate)
        {
            var details = new List<BSListingEntity>();
            try
            {
                details = new BSDAO().GetBSList(fromdate, todate);
            }
            catch (Exception ex)
            {
                // details.Add(new DbStatusEntity(ex.Message));
            }
            return details.ToArray();
        }

        [WebMethod]
        public static string[] GetLatestTrasnsactionNumber()
        {
            List<string> lstvalues = new List<string>();
            try
            {
                lstvalues = new GenericDAO().GetLatestTrasnsactionNumber("BS");
            }
            catch (Exception ex)
            {
                // details.Add(new DbStatusEntity(ex.Message));
            }
            return lstvalues.ToArray();
        }        

        [WebMethod]
        public static GenericIdNameEntity[] GetActivePaymodeList()
        {
            var details = new List<GenericIdNameEntity>();
            try
            {
                details = new GenericDAO().GetActivePaymodeList("BS");
            }
            catch (Exception ex)
            {
                // details.Add(new DbStatusEntity(ex.Message));
            }
            return details.ToArray();
        }
               

        [WebMethod]
        public static DbStatusEntity[] InsertData(BSParamEntity obj)
        {
            var details = new List<DbStatusEntity>();
            try
            {
                details.Add(new BSDAO().InsertBS(obj));
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
                details.Add(new BSDAO().DeleteBS(id));
            }
            catch (Exception ex)
            {
                details.Clear();
                details.Add(new DbStatusEntity(ex.Message));
            }
            return details.ToArray();
        }

        [WebMethod]
        public static Int64[] CheckVoidBSEnrty(Int64 id)
        {
            List<Int64> lstvalues = new List<Int64>();
            try
            {
                lstvalues = new BSDAO().CheckVoidBSEnrty(id);
            }
            catch (Exception ex)
            {
                // details.Add(new DbStatusEntity(ex.Message));
            }
            return lstvalues.ToArray();
        }

        [WebMethod]
        public static DbStatusEntity[] VoidData(long id)
        {
            var details = new List<DbStatusEntity>();
            try
            {
                details.Add(new BSDAO().VoidBSEntry(id));
            }
            catch (Exception ex)
            {
                details.Clear();
                details.Add(new DbStatusEntity(ex.Message));
            }
            return details.ToArray();
        }

        [WebMethod]
        public static BsEntity[] EditData(Int64 id)
        {
            var details = new List<BsEntity>();
            try
            {
                details = new BSDAO().EditBillSettlement(id);
            }
            catch (Exception ex)
            {
                // details.Add(new DbStatusEntity(ex.Message));
            }
            return details.ToArray();
        }

        [WebMethod]
        public static PendingBillClients[] GetPendingBillsCustomers()
        {
            var details = new List<PendingBillClients>();
            try
            {
                details = new GenericDAO().GetPendingBillsCustomers();
            }
            catch (Exception ex)
            {
                // details.Add(new DbStatusEntity(ex.Message));
            }
            return details.ToArray();
        }


        [WebMethod]
        public static PendingBillsByClient[] GetPendingBillsbyClientID(Int64 id)
        {
            var details = new List<PendingBillsByClient>();
            try
            {
                details = new GenericDAO().GetPendingBillsbyClientID(id);
            }
            catch (Exception ex)
            {
                // details.Add(new DbStatusEntity(ex.Message));
            }
            return details.ToArray();
        }
    }
}