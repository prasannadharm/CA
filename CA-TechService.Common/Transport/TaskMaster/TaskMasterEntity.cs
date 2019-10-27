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
            MainArray = null;
            SubArray = null;
            ClientMapArray = null;
            ClientCategoryMapArray = null;
        }
        public TaskMasterMainEntity[] MainArray { get; set; }
        public TaskMasterSubEntity[] SubArray { get; set; }
        public TaskClientMappingEntity[] ClientMapArray { get; set; }
        public TaskClientCategoryMappingEntity[] ClientCategoryMapArray { get; set; }
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
        public Int64 C_NO { get; set; }
        public string C_NAME { get; set; }
        public string FILE_NO { get; set; }
        public string PAN { get; set; }
        public string AADHAAR { get; set; }
        public string GSTIN { get; set; }
        public string PH_NO { get; set; }
        public string MOBILE_NO1 { get; set; }
        public string MOBILE_NO2 { get; set; }
        public string CLI_GRP_NAME { get; set; }
    }

    public class TaskClientCategoryMappingEntity
    {
        public Int64 T_ID { get; set; }
        public Int64 CLI_CAT_ID { get; set; }       
    }

    public class TaskMasterParamEntity
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
        public string MAPPED_CLIENTS { get; set; }
        public string MAPPED_CLI_CAT { get; set; }
        public TaskMasterSubParamEntity[] SUBARR { get; set; }
    }

    public class TaskMasterSubParamEntity
    {
        public Int32 SLNO { get; set; }
        public Int32 USER_ID { get; set; }
        public string USER { get; set; }
        public string STAGE { get; set; }
        public string GENID { get; set; }
    }

}
