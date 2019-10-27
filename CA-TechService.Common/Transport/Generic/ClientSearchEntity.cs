using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA_TechService.Common.Transport.Generic
{
    public class ClientSearchEntity
    {
        public Int64 C_ID { get; set; }
        public Int64 C_NO { get; set; }
        public string FILE_NO { get; set; }
        public string C_NAME { get; set; }
        public string PH_NO { get; set; }
        public string MOBILE_NO1 { get; set; }
        public string MOBILE_NO2 { get; set; }
        public string PAN { get; set; }
        public string AADHAAR { get; set; }
        public string GSTIN { get; set; }
        public string CLI_GRP_NAME { get; set; }        
    }
}
