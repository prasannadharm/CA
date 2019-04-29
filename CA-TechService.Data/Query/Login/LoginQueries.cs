#region Imports
#endregion
namespace CA_TechService.Data.Query.Login
{
    public  class LoginQueries
    {

        public static string CheckLogin
        {
            get { return "USP_CheckLogin"; }
        }

        public static string GetMenuForUser
        {
            get { return "USP_GetMenuForUser"; }
        }
    }
}
