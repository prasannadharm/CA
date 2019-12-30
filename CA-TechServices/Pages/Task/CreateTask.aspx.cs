using System;
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
using CA_TechService.Common.Transport.Task;
using CA_TechService.Data.DataSource.Task;
using CA_TechService.Common.Transport.ClientMaster;

namespace CA_TechServices.Pages.Task
{
    public partial class CreateTask : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod]
        public static TaskTrnCreateListEntity[] GetData(string fromdate, string todate)
        {
            var details = new List<TaskTrnCreateListEntity>();
            try
            {
                details = new TaskTrnCreateTaskDAO().GetTaskTrnCreatedList(fromdate, todate);
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
                lstvalues = new GenericDAO().GetLatestTrasnsactionNumber("TASK");
            }
            catch (Exception ex)
            {
                // details.Add(new DbStatusEntity(ex.Message));
            }
            return lstvalues.ToArray();
        }

        [WebMethod]
        public static string[] GetTaskSchDates(Int64 id)
        {
            List<string> lstvalues = new List<string>();
            try
            {
                lstvalues = new TaskTrnCreateTaskDAO().GetTaskSchDateForTask(id);
            }
            catch (Exception ex)
            {
                // details.Add(new DbStatusEntity(ex.Message));
            }
            return lstvalues.ToArray();
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
        public static GenericIdNameEntity[] GetActiveTasksList()
        {
            var details = new List<GenericIdNameEntity>();
            try
            {
                details = new GenericDAO().GetActiveTasksList();
            }
            catch (Exception ex)
            {
                // details.Add(new DbStatusEntity(ex.Message));
            }
            return details.ToArray();
        }

        [WebMethod]
        public static GenericIdNameEntity[] GetActiveClientListforDropdown()
        {
            var details = new List<GenericIdNameEntity>();
            try
            {
                details = new GenericDAO().GetActiveClientListforDropdown();
            }
            catch (Exception ex)
            {
                // details.Add(new DbStatusEntity(ex.Message));
            }
            return details.ToArray();
        }

        [WebMethod]
        public static TaskTrnPendingForInitializeEntity[] GetPendingTaskData(string TIDSTR, string CIDSTR, string CLICATIDSTR)
        {
            var details = new List<TaskTrnPendingForInitializeEntity>();
            try
            {
                details = new TaskTrnCreateTaskDAO().GetTaskTrnPendingTaskForInitailization(TIDSTR, CIDSTR, CLICATIDSTR);
            }
            catch (Exception ex)
            {
                // details.Add(new DbStatusEntity(ex.Message)); 
            }
            return details.ToArray();
        }

        [WebMethod]
        public static DbStatusEntity[] InsertTaskAbortData(Int64 T_ID, Int64 C_ID, string SCH_ON)
        {
            var details = new List<DbStatusEntity>();
            try
            {
                HttpContext context = HttpContext.Current;
                details.Add(new TaskTrnCreateTaskDAO().InsertTaskAbortTrn(T_ID, C_ID, SCH_ON, Convert.ToInt64(context.Session["USER_ID"].ToString())));
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