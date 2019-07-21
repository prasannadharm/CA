#region Imports
using System;
using System.Collections.Generic;
using CA_TechService.Common.Generic;
using CA_TechService.Common.Transport.ClientMaster;
using CA_TechService.Data.DataSource.ClientMaster;
using System.Web.Services;
using CA_TechService.Common.Transport.TaskMaster;
using CA_TechService.Data.DataSource.TaskMaster;
#endregion
namespace CA_TechServices.Pages.TaskMaster
{
    public partial class TaskStageMaster : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        [WebMethod]
        public static TaskStageMasterEntity[] GetData() //Show the details of the data after insert in HTML Table
        {
            var details = new List<TaskStageMasterEntity>();
            try
            {
                details = new TaskStageMasterDAO().GetTaskStageMasterList();
            }
            catch (Exception ex)
            {
                // details.Add(new DbStatusEntity(ex.Message));
            }
            return details.ToArray();
        }

        [WebMethod]
        public static TaskStageMasterEntity[] EditData(int id)
        {
            var details = new List<TaskStageMasterEntity>();
            try
            {
                details = new TaskStageMasterDAO().EditTaskStageMaster(id);
            }
            catch (Exception ex)
            {
                //details.Add(new DbStatusEntity(ex.Message));
            }
            return details.ToArray();
        }

        [WebMethod]
        public static DbStatusEntity[] UpdateData(TaskStageMasterEntity obj, int id) //Update data in database  
        {
            var details = new List<DbStatusEntity>();
            try
            {
                details.Add(new TaskStageMasterDAO().UpdateTaskStageMaster(obj, id));
            }
            catch (Exception ex)
            {
                details.Clear();
                details.Add(new DbStatusEntity(ex.Message));
            }
            return details.ToArray();

        }

        [WebMethod]
        public static DbStatusEntity[] InsertData(TaskStageMasterEntity obj)
        {
            var details = new List<DbStatusEntity>();
            try
            {
                details.Add(new TaskStageMasterDAO().InsertTaskStageMaster(obj));
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
                details.Add(new TaskStageMasterDAO().DeleteTaskStageMaster(id));
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