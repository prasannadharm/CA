using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CA_TechService.Common.Generic;
using System.Web.Services;
using CA_TechService.Data.DataSource;
using CA_TechService.Common.Transport.Generic;
using CA_TechService.Common.Transport.Bill;
using CA_TechService.Data.DataSource.Bill;

namespace CA_TechServices.Pages.Bill
{
    public partial class Bill : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod]
        public static BillListingEntity[] GetData(string fromdate, string todate)
        {
            var details = new List<BillListingEntity>();
            try
            {
                details = new BillDAO().GetBillsList(fromdate, todate);
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
                lstvalues = new GenericDAO().GetLatestTrasnsactionNumber("BILL");
            }
            catch (Exception ex)
            {
                // details.Add(new DbStatusEntity(ex.Message));
            }
            return lstvalues.ToArray();
        }

        [WebMethod]
        public static BillEntity[] EditData(Int64 id)
        {
            var details = new List<BillEntity>();
            try
            {
                details = new BillDAO().EditBill(id);
            }
            catch (Exception ex)
            {
                // details.Add(new DbStatusEntity(ex.Message));
            }
            return details.ToArray();
        }
        
        [WebMethod]
        public static GenericIdNameEntity[] GetActivePaymodeList()
        {
            var details = new List<GenericIdNameEntity>();
            try
            {
                details = new GenericDAO().GetActivePaymodeList();
            }
            catch (Exception ex)
            {
                // details.Add(new DbStatusEntity(ex.Message));
            }
            return details.ToArray();
        }

        [WebMethod]
        public static ClientSearchEntity[] GetClientSearchList(string filterby, string filtertext)
        {
            var details = new List<ClientSearchEntity>();
            try
            {
                details = new GenericDAO().GetClientSearchList(filterby, filtertext);
            }
            catch (Exception ex)
            {
                // details.Add(new DbStatusEntity(ex.Message));
            }
            return details.ToArray();
        }

        [WebMethod]
        public static DbStatusEntity[] UpdateData(BillParamEntity obj, Int64 id) //Update data in database  
        {
            var details = new List<DbStatusEntity>();
            try
            {
                details.Add(new BillDAO().UpdateBill(obj, id));
            }
            catch (Exception ex)
            {
                details.Clear();
                details.Add(new DbStatusEntity(ex.Message));
            }
            return details.ToArray();

        }

        [WebMethod]
        public static DbStatusEntity[] InsertData(BillParamEntity obj)
        {
            var details = new List<DbStatusEntity>();
            try
            {
                details.Add(new BillDAO().InsertBill(obj));
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
                details.Add(new BillDAO().DeleteBill(id));
            }
            catch (Exception ex)
            {
                details.Clear();
                details.Add(new DbStatusEntity(ex.Message));
            }
            return details.ToArray();
        }

        [WebMethod]
        public static Int64[] CheckVoidBillEnrty(Int64 id)
        {
            List<Int64> lstvalues = new List<Int64>();
            try
            {
                lstvalues = new BillDAO().CheckVoidBillEnrty(id);
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
                details.Add(new BillDAO().VoidBillEntry(id));
            }
            catch (Exception ex)
            {
                details.Clear();
                details.Add(new DbStatusEntity(ex.Message));
            }
            return details.ToArray();
        }

        [WebMethod]
        public static Int64[] CheckBillSettledEnrty(Int64 id)
        {
            List<Int64> lstvalues = new List<Int64>();
            try
            {
                lstvalues = new BillDAO().CheckBillSettledEnrty(id);
            }
            catch (Exception ex)
            {
                // details.Add(new DbStatusEntity(ex.Message));
            }
            return lstvalues.ToArray();
        }
    }
}