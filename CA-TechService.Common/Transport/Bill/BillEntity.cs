using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA_TechService.Common.Transport.Bill
{
    public class BillEntity
    {
        public BillEntity()
        {
            MAINARRAY = null;
            SUBARRAY = null;
        }

        public BillMainEntity[] MAINARRAY { get; set; }
        public BillSubEntity[] SUBARRAY { get; set; }
    }

    public class BillMainEntity
    {
        public Int64 BILL_ID { get; set; }
        public Int64 BILL_NO { get; set; }
        public string BILL_DATE { get; set; }
        public Int64 C_ID { get; set; }
        public Int64 C_NO { get; set; }
        public string FILE_NO { get; set; }
        public string C_NAME { get; set; }
        public Int32 PAYMODE_ID { get; set; }
        public string PAYMODE_NAME { get; set; }
        public Int64 TASK_ID { get; set; }
        public Int64 TASK_NO { get; set; }
        public double GROSS_AMT { get; set; }
        public double SGST_AMT { get; set; }
        public double CGST_AMT { get; set; }
        public double IGST_AMT { get; set; }
        public double OTH_AMT { get; set; }
        public double NET_AMT { get; set; }
        public double BAL_AMT { get; set; }
        public string DUE_DATE { get; set; }
        public string REMARKS { get; set; }
        public bool VOID_STATUS { get; set; }
    }

    public class BillSubEntity
    {
        public Int64 SUB_BILL_ID { get; set; }
        public Int64 BILL_ID { get; set; }
        public int SL_NO { get; set; }
        public string DESCP { get; set; }
        public double GROSS_AMT { get; set; }
        public double SGST_PER { get; set; }
        public double SGST_AMT { get; set; }
        public double CGST_PER { get; set; }
        public double CGST_AMT { get; set; }
        public double IGST_PER { get; set; }
        public double IGST_AMT { get; set; }        
        public double NET_AMT { get; set; }
        public string REMARKS { get; set; }
        public string GENID { get; set; }
    }

    public class BillListingEntity
    {
        public Int64 BILL_ID { get; set; }
        public Int64 BILL_NO { get; set; }
        public string BILL_DATE { get; set; }
        public Int64 C_NO { get; set; }
        public string FILE_NO { get; set; }
        public string C_NAME { get; set; }
        public string PAYMODE_NAME { get; set; }
        public double NET_AMT { get; set; }
        public string REMARKS { get; set; }
        public bool VOID_STATUS { get; set; }        
    }

    public class BillParamEntity
    {
        public Int64 BILL_ID { get; set; }
        public Int64 BILL_NO { get; set; }
        public string BILL_DATE { get; set; }
        public Int64 C_ID { get; set; }
        public Int64 C_NO { get; set; }
        public string FILE_NO { get; set; }
        public string C_NAME { get; set; }
        public Int32 PAYMODE_ID { get; set; }
        public string PAYMODE_NAME { get; set; }
        public Int64 TASK_ID { get; set; }
        public Int64 TASK_NO { get; set; }
        public double GROSS_AMT { get; set; }
        public double SGST_AMT { get; set; }
        public double CGST_AMT { get; set; }
        public double IGST_AMT { get; set; }
        public double OTH_AMT { get; set; }
        public double NET_AMT { get; set; }
        public double BAL_AMT { get; set; }
        public string DUE_DATE { get; set; }
        public string REMARKS { get; set; }
        public BillSubEntity[] SUBARRAY { get; set; }
    }
}
