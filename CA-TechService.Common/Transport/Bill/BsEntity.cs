using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA_TechService.Common.Transport.Bill
{
    public class BsEntity
    {
        public BsEntity()
        {
            MAINARRAY = null;
            SUBARRAY = null;
        }

        public BSMainEntity[] MAINARRAY { get; set; }
        public BSsubEntity[] SUBARRAY { get; set; }
    }
        
    public class BSListingEntity
    {
        public Int64 BS_ID { get; set; }
        public Int64 BS_NO { get; set; }
        public string BS_DATE { get; set; }
        public Int64 C_NO { get; set; }
        public string FILE_NO { get; set; }
        public string C_NAME { get; set; }
        public string PAYMODE_NAME { get; set; }
        public double BS_AMT { get; set; }
        public string REMARKS { get; set; }
        public bool VOID_STATUS { get; set; }        
    }

    public class BSParamEntity
    {
        public Int64 BS_ID { get; set; }
        public string BS_DATE { get; set; }
        public Int64 C_ID { get; set; }
        public Int64 C_NO { get; set; }
        public string FILE_NO { get; set; }
        public string C_NAME { get; set; }
        public Int32 PAYMODE_ID { get; set; }
        public string PAYMODE_NAME { get; set; }
        public double BS_AMT { get; set; }
        public string REMARKS { get; set; }
        public BSsubEntity[] SUBARRAY { get; set; }      
    }
    
    public class BSMainEntity
    {
        public Int64 BS_ID { get; set; }
        public Int64 BS_NO { get; set; }
        public string BS_DATE { get; set; }
        public Int64 C_ID { get; set; }
        public Int64 C_NO { get; set; }
        public string FILE_NO { get; set; }
        public string C_NAME { get; set; }
        public Int32 PAYMODE_ID { get; set; }
        public string PAYMODE_NAME { get; set; }
        public double BS_AMT { get; set; }
        public string REMARKS { get; set; }
        public bool VOID_STATUS { get; set; }
        public string C_DETAILS { get; set; }
        public string GSTIN { get; set; }
        public string PAN { get; set; }
    }

    public class BSsubEntity
    {
        public Int64 SUB_BS_ID { get; set; }
        public Int64 BS_ID { get; set; }
        public int SL_NO { get; set; }
        public Int64 BILL_ID { get; set; }
        public Int64 BILL_NO { get; set; }
        public string BILL_DATE { get; set; }     
        public double BILL_AMT { get; set; }
        public double PAID_AMT { get; set; }
        public double BAL_AMT { get; set; }
        public double BS_AMT { get; set; }        
        public string GENID { get; set; }       
    }
}
