using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA_TechService.Common.Transport.TaskMaster
{
    public class TaskMasterEntity
    {
        public TaskMasterEntity()
        {
            MainList = new List<TaskMasterMainEntity>();
            SubList = new List<TaskMasterSubEntity>();
            ClientMapList = new List<TaskClientMappingEntity>();
        }
        public List<TaskMasterMainEntity> MainList { get; set; }
        public List<TaskMasterSubEntity> SubList { get; set; }
        public List<TaskClientMappingEntity> ClientMapList { get; set; }
    }

    public class TaskMasterMainEntity
    {
        public Int64 T_ID { get; set; }
        public string T_NAME { get; set; }
        public string T_DESC { get; set; }
        public Int32 PRIORITY { get; set; }
        public bool RECURRING { get; set; }
        public string RECURRING_TYPE { get; set; }
        public Int32 RECURRING_DAYS { get; set; }
        public Int32 RECURRING_START_DAY { get; set; }
        public string RECURRING_END_DATE { get; set; }
        public bool ACTIVE_STATUS { get; set; }
    }

    public class TaskMasterSubEntity
    {
        public Int64 ID { get; set; }
        public Int64 T_ID { get; set; }
        public Int64 TS_ID { get; set; }
        public string TS_NAME { get; set; }
        public int USER_ID { get; set; }
        public string NAME { get; set; }
        public int SL_NO { get; set; }
    }

    public class TaskClientMappingEntity
    {
        public Int64 T_ID { get; set; }
        public Int64 C_ID { get; set; }
        public string C_NAME { get; set; }
        public string FILE_NO { get; set; }
        public string PAN { get; set; }        
    }

}
