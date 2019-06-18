namespace CA_TechService.Common.Transport.Roles
{
    public class RoleSelectedMenuEntity
    {
        public int    ROLE_ID     { get; set; }
        public int    MENU_ID     { get; set; }
        public string MENU_NAME   { get; set; }
        public bool   IsReadOnly {get; set;}
    }
}
