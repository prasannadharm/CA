#region Imports
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#endregion
namespace CA_TechService.Common.Transport.ClientMaster
{
    public class ClientMasterEntity
    {
        public ClientMasterEntity()
        {
            ClientCategoryList = new List<ClientCategoryMapping>();
        }
                
        public Int64  C_ID { get; set; }
        public Int64 C_NO { get; set; }
        public string FILE_NO { get; set; }
        public string C_NAME { get; set; }
        public string ALIAS { get; set; }
        public string FNAME { get; set; }
        public string HNAME { get; set; }
        public string GENDER { get; set; }
        public bool SAME_AB { get; set; }
        public string ADDR { get; set; }
        public string ADDR1 { get; set; }
        public string CITY { get; set; }
        public string CITY1 { get; set; }
        public string PIN { get; set; }
        public string PIN1 { get; set; }
        public string STATE { get; set; }
        public string STATE1 { get; set; }
        public string CNT_NAME { get; set; }
        public string PH_NO { get; set; }
        public string MOBILE_NO1 { get; set; }
        public string MOBILE_NO2 { get; set; }
        public string EMAIL_ID { get; set; }
        public string DOB { get; set; }
        public string PAN { get; set; }
        public string AADHAAR { get; set; }
        public string GSTIN { get; set; }
        public string WARD { get; set; }
        public string RACK_NO { get; set; }
        public int CLI_GRP_ID { get; set; }
        public string CLI_GRP_NAME { get; set; }
        public bool ACTIVE_STATUS { get; set; }
        public string ALERT_MSG { get; set; }
        public List<ClientCategoryMapping> ClientCategoryList { get; set; }
        public string ClientCategoryStringList { get; set; }
    }

    public class ClientMasterSearchEntity
    {
        public Int32 CNT { get; set; }
        public string C_NO { get; set; }
        public string FILE_NO { get; set; }
        public string C_NAME { get; set; }
        public string GENDER { get; set; }
        public string CITY { get; set; }
        public string STATE { get; set; }
        public string PH_NO { get; set; }
        public string MOBILE_NO1 { get; set; }
        public string MOBILE_NO2 { get; set; }
        public string EMAIL_ID { get; set; }
        public string PAN { get; set; }
        public string AADHAAR { get; set; }
        public string GSTIN { get; set; }
        public string WARD { get; set; }
        public string RACK_NO { get; set; }
        public string CLI_GRP_LST { get; set; }
        public string CLI_CAT_LST { get; set; }        
    }

    public class ClientCategoryMapping
    {
        public Int64 C_ID { get; set; }
        public int CLI_CAT_ID { get; set; }
        public string CLI_CAT_NAME { get; set; }
    }

    public class ClientDocsEntity
    {
        public long CLIENT_ID { get; set; }
        public string ORG_FILE_NAME { get; set; }
        public string PHY_FILE_NAME { get; set; }
        public string REMARKS { get; set; }        
    }

    public class ClientCredentialsEntity
    {
        public long ID { get; set; }
        public long CLIENT_ID { get; set; }
        public string SITE_NAME { get; set; }
        public string URL { get; set; }
        public string UNAME { get; set; }
        public string UPASS { get; set; }
        public string REMARKS { get; set; }
    }
}
