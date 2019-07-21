#region Imports
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#endregion
namespace CA_TechService.Data.Query.ClientMaster
{
  public class ClientMasterQueries
    {
        public static string GetClientCategoryList { get { return "USP_GetClientCategoryList"; } }

        public static string InsertClientCategoryMaster { get { return "USP_InsertClientCategoryMaster"; } }

        public static string UpdateClientCategoryMaster { get { return "USP_UpdateClientCategoryMaster"; } }

        public static string DeleteClientCategoryMaster { get { return "USP_DeleteClientCategoryMaster"; } }

        public static string GetClientCategoryDetailsbyID { get { return "USP_GetClientCategoryDetailsbyID"; } }
    }
}
 