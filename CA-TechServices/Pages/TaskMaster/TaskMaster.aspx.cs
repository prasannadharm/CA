using System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CA_TechService.Common.Generic;
using CA_TechService.Common.Transport.TaskMaster;
using CA_TechService.Data.DataSource.TaskMaster;
using System.Web.Services;
using CA_TechService.Data.DataSource;
using CA_TechService.Common.Transport.Generic;

namespace CA_TechServices.Pages.TaskMaster
{
    public partial class TaskMaster : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        [WebMethod]
        public static TaskMasterMainEntity[] GetData()
        {
            var details = new List<TaskMasterMainEntity>();
            try
            {
                details = new TaskMasterDAO().GetTaskMasterList();
            }
            catch (Exception ex)
            {
                // details.Add(new DbStatusEntity(ex.Message));
            }
            return details.ToArray();
        }
        
        [WebMethod]
        public static TaskMasterSubEntity [] GetTaskMasterSatgesListById(Int64 id)
        {
            var details = new List<TaskMasterSubEntity>();
            try
            {
                details = new TaskMasterDAO().GetTaskMasterSatgesListById(id);
            }
            catch (Exception ex)
            {
                // details.Add(new DbStatusEntity(ex.Message));
            }
            return details.ToArray();
        }

        [WebMethod]
        public static TaskClientMappingEntity[] GetTaskMasterClientListById(Int64 id)
        {
            var details = new List<TaskClientMappingEntity>();
            try
            {
                details = new TaskMasterDAO().GetTaskMasterClientListById(id);
            }
            catch (Exception ex)
            {
                // details.Add(new DbStatusEntity(ex.Message));
            }
            return details.ToArray();
        }

        [WebMethod]
        public static DbStatusEntity[] DeleteData(int id)
        {
            var details = new List<DbStatusEntity>();
            try
            {
                details.Add(new TaskMasterDAO().DeleteTaskMaster(id));
            }
            catch (Exception ex)
            {
                details.Clear();
                details.Add(new DbStatusEntity(ex.Message));
            }
            return details.ToArray();
        }

        [WebMethod]
        public static TaskMasterEntity[] EditData(Int64 id)
        {
            var details = new List<TaskMasterEntity>();
            try
            {
                details = new TaskMasterDAO().EditTaskMaster(id);
            }
            catch (Exception ex)
            {
                // details.Add(new DbStatusEntity(ex.Message));
            }
            return details.ToArray();
        }

        [WebMethod]
        public static GenericIdNameEntity[] GetActiveUsers()
        {
            var details = new List<GenericIdNameEntity>();
            try
            {
                details = new GenericDAO().GetActiveUsersList();
            }
            catch (Exception ex)
            {
                // details.Add(new DbStatusEntity(ex.Message));
            }
            return details.ToArray();
        }

        [WebMethod]
        public static GenericIdNameEntity[] GetActiveTaskStages()
        {
            var details = new List<GenericIdNameEntity>();
            try
            {
                details = new GenericDAO().GetActiveTaskStagesList();
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
    }
}
