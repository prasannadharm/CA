
namespace CA_TechService.Data.Query.TaskMaster
{
    public class TaskMasterQueries
    {
        public static string GetTaskStageMasterList { get { return "USP_GetTaskStageMasterList"; } }

        public static string InsertTaskStageMaster { get { return "USP_InsertTaskStageMaster"; } }

        public static string UpdateTaskStageMaster { get { return "USP_UpdateTaskStageMaster"; } }

        public static string DeleteTaskStageMaster { get { return "USP_DeleteTaskStageMaster"; } }

        public static string EditTaskStageMaster { get { return "USP_GetTaskStageMasterDetailsbyID"; } }
    }
}
