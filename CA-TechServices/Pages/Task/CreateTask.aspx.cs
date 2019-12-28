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


    }
}