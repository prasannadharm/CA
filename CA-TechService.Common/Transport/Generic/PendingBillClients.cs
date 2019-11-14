using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA_TechService.Common.Transport.Generic
{
    public class PendingBillClients
    {
        public Int64 C_ID { get; set; }
        public Int64 C_NO { get; set; }
        public string FILE_NO { get; set; }
        public string C_NAME { get; set; }        
        public string C_DETAILS { get; set; }
        public double NET_AMT { get; set; }
        public double BAL_AMT { get; set; }
        public Int32 BILL_COUNT { get; set; }      
    }

    public class PendingBillsByClient
    {
        public Int64 BILL_ID { get; set; }
        public Int64 BILL_NO { get; set; }
        public string BILL_DATE { get; set; }
        public double BILL_AMT { get; set; }
        public double PAID_AMT { get; set; }
        public double BAL_AMT { get; set; }
        public double BS_AMT { get; set; }       

    }
}
