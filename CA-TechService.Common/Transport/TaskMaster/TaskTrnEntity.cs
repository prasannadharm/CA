using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA_TechService.Common.Transport.TaskMaster
{
    public class TaskTrnEntity
    {

    }

    public class TaskTrnCreateListEntity
    {
        public Int64 T_ID { get; set; }
        public Int64 T_NO { get; set; }
        public string T_DATE { get; set; }
        public string T_NAME { get; set; }
        public Int64 C_NO { get; set; }        
        public string C_NAME { get; set; }
        public string FILE_NO { get; set; }
        public string SCH_ON { get; set; }
        public string CREATEDBY_NAME { get; set; }
        public bool VOID_STATUS { get; set; }        
    }

    public class TaskTrnPendingForInitializeEntity
    {
        public Int64 T_ID { get; set; }
        public string T_NAME { get; set; }
        public string T_DESC { get; set; }
        public Int32 PRIORITY { get; set; }
        public string RECURRING_TYPE { get; set; }
        public string TASK_SCH_DATE { get; set; }
        public Int64 C_ID { get; set; }
        public Int64 C_NO { get; set; }
        public string C_NAME { get; set; }
        public string FILE_NO { get; set; }
        public string PAN { get; set; }        
    }
}
