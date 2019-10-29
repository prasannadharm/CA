#region Imports
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using CA_TechService.Common.Transport.Bill;
using CA_TechService.Common.Generic;
#endregion
namespace CA_TechService.Data.DataSource.Bill
{
    public class BillDAO
    {
        public List<BillListingEntity> GetBillsList(string fromdate, string todate)
        {
            string CS = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            SqlDataAdapter adapter;
            DataSet ds = new DataSet();
            List<BillListingEntity> retlst = new List<BillListingEntity>();
            try
            {
                using (SqlConnection con = new SqlConnection(CS))
                {
                    SqlCommand cmd = new SqlCommand("USP_GetBillMainList", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@DateFrom", fromdate);
                    cmd.Parameters.AddWithValue("@DateTo", todate);
                    con.Open();
                    adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(ds);

                    for (int i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
                    {
                        BillListingEntity obj = new BillListingEntity();
                        obj.BILL_ID = ds.Tables[0].Rows[i]["BILL_ID"] == DBNull.Value ? 0 : Convert.ToInt64(ds.Tables[0].Rows[i]["BILL_ID"]);
                        obj.BILL_NO = ds.Tables[0].Rows[i]["BILL_NO"] == DBNull.Value ? 0 : Convert.ToInt64(ds.Tables[0].Rows[i]["BILL_NO"]);
                        obj.BILL_DATE = ds.Tables[0].Rows[i]["BILL_DATE"] == DBNull.Value ? "" : ds.Tables[0].Rows[i]["BILL_DATE"].ToString();
                        obj.C_NO = ds.Tables[0].Rows[i]["C_NO"] == DBNull.Value ? 0 : Convert.ToInt64(ds.Tables[0].Rows[i]["C_NO"]);
                        obj.FILE_NO = ds.Tables[0].Rows[i]["FILE_NO"] == DBNull.Value ? "" : ds.Tables[0].Rows[i]["FILE_NO"].ToString();
                        obj.C_NAME = ds.Tables[0].Rows[i]["C_NAME"] == DBNull.Value ? "" : ds.Tables[0].Rows[i]["C_NAME"].ToString();
                        obj.REMARKS = ds.Tables[0].Rows[i]["REMARKS"] == DBNull.Value ? "" : ds.Tables[0].Rows[i]["REMARKS"].ToString();
                        obj.PAYMODE_NAME = ds.Tables[0].Rows[i]["PAYMODE_NAME"] == DBNull.Value ? "" : ds.Tables[0].Rows[i]["PAYMODE_NAME"].ToString();
                        obj.NET_AMT = ds.Tables[0].Rows[i]["NET_AMT"] == DBNull.Value ? 0 : Convert.ToDouble(ds.Tables[0].Rows[i]["NET_AMT"]);
                        obj.VOID_STATUS = ds.Tables[0].Rows[i]["VOID_STATUS"] == DBNull.Value ? true : Convert.ToBoolean(ds.Tables[0].Rows[i]["VOID_STATUS"]);
                        retlst.Add(obj);                        
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return retlst;
        }

        public List<BillEntity> EditBill(Int64 id)
        {
            string CS = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            SqlDataAdapter adapter;
            DataSet ds = new DataSet();
            List<BillEntity> retlst = new List<BillEntity>();
            BillEntity retval = new BillEntity();
            try
            {
                using (SqlConnection con = new SqlConnection(CS))
                {
                    SqlCommand cmd = new SqlCommand("USP_GetBillDetailsbyID", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID", id);
                    con.Open();
                    adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(ds);
                    if (ds.Tables.Count >= 2)
                    {
                        List<BillMainEntity> objlst1 = new List<BillMainEntity>();
                        for (int i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
                        {
                            BillMainEntity obj1 = new BillMainEntity();
                            obj1.BILL_ID = ds.Tables[0].Rows[i]["BILL_ID"] == DBNull.Value ? 0 : Convert.ToInt64(ds.Tables[0].Rows[i]["BILL_ID"]);
                            obj1.BILL_NO = ds.Tables[0].Rows[i]["BILL_NO"] == DBNull.Value ? 0 : Convert.ToInt64(ds.Tables[0].Rows[i]["BILL_NO"]);
                            obj1.BILL_DATE = ds.Tables[0].Rows[i]["BILL_DATE"] == DBNull.Value ? "" : ds.Tables[0].Rows[i]["BILL_DATE"].ToString();
                            obj1.C_ID = ds.Tables[0].Rows[i]["C_ID"] == DBNull.Value ? 0 : Convert.ToInt64(ds.Tables[0].Rows[i]["C_ID"]);
                            obj1.C_NO = ds.Tables[0].Rows[i]["C_NO"] == DBNull.Value ? 0 : Convert.ToInt64(ds.Tables[0].Rows[i]["C_NO"]);
                            obj1.FILE_NO = ds.Tables[0].Rows[i]["FILE_NO"] == DBNull.Value ? "" : ds.Tables[0].Rows[i]["FILE_NO"].ToString();
                            obj1.C_NAME = ds.Tables[0].Rows[i]["C_NAME"] == DBNull.Value ? "" : ds.Tables[0].Rows[i]["C_NAME"].ToString();
                            obj1.PAYMODE_ID = ds.Tables[0].Rows[i]["PAYMODE_ID"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["PAYMODE_ID"]);
                            obj1.PAYMODE_NAME = ds.Tables[0].Rows[i]["PAYMODE_NAME"] == DBNull.Value ? "" : ds.Tables[0].Rows[i]["PAYMODE_NAME"].ToString();
                            obj1.TASK_ID = ds.Tables[0].Rows[i]["TASK_ID"] == DBNull.Value ? 0 : Convert.ToInt64(ds.Tables[0].Rows[i]["TASK_ID"]);
                            obj1.TASK_NO = ds.Tables[0].Rows[i]["TASK_NO"] == DBNull.Value ? 0 : Convert.ToInt64(ds.Tables[0].Rows[i]["TASK_NO"]);
                            obj1.GROSS_AMT = ds.Tables[0].Rows[i]["GROSS_AMT"] == DBNull.Value ? 0 : Convert.ToDouble(ds.Tables[0].Rows[i]["GROSS_AMT"]);
                            obj1.SGST_AMT = ds.Tables[0].Rows[i]["SGST_AMT"] == DBNull.Value ? 0 : Convert.ToDouble(ds.Tables[0].Rows[i]["SGST_AMT"]);
                            obj1.CGST_AMT = ds.Tables[0].Rows[i]["CGST_AMT"] == DBNull.Value ? 0 : Convert.ToDouble(ds.Tables[0].Rows[i]["CGST_AMT"]);
                            obj1.IGST_AMT = ds.Tables[0].Rows[i]["IGST_AMT"] == DBNull.Value ? 0 : Convert.ToDouble(ds.Tables[0].Rows[i]["IGST_AMT"]);
                            obj1.OTH_AMT = ds.Tables[0].Rows[i]["OTH_AMT"] == DBNull.Value ? 0 : Convert.ToDouble(ds.Tables[0].Rows[i]["OTH_AMT"]);
                            obj1.NET_AMT = ds.Tables[0].Rows[i]["NET_AMT"] == DBNull.Value ? 0 : Convert.ToDouble(ds.Tables[0].Rows[i]["NET_AMT"]);
                            obj1.BAL_AMT = ds.Tables[0].Rows[i]["BAL_AMT"] == DBNull.Value ? 0 : Convert.ToDouble(ds.Tables[0].Rows[i]["BAL_AMT"]);
                            obj1.DUE_DATE = ds.Tables[0].Rows[i]["DUE_DATE"] == DBNull.Value ? "" : ds.Tables[0].Rows[i]["DUE_DATE"].ToString();
                            obj1.REMARKS = ds.Tables[0].Rows[i]["REMARKS"] == DBNull.Value ? "" : ds.Tables[0].Rows[i]["REMARKS"].ToString();
                            obj1.VOID_STATUS = ds.Tables[0].Rows[i]["VOID_STATUS"] == DBNull.Value ? true : Convert.ToBoolean(ds.Tables[0].Rows[i]["VOID_STATUS"]);                                 
                            objlst1.Add(obj1);
                        }
                        retval.MAINARRAY = objlst1.ToArray();

                        List<BillSubEntity> objlst2 = new List<BillSubEntity>();
                        for (int i = 0; i <= ds.Tables[1].Rows.Count - 1; i++)
                        {
                            BillSubEntity obj2 = new BillSubEntity();
                            obj2.SUB_BILL_ID = ds.Tables[1].Rows[i]["SUB_BILL_ID"] == DBNull.Value ? 0 : Convert.ToInt64(ds.Tables[1].Rows[i]["SUB_BILL_ID"]);
                            obj2.BILL_ID = ds.Tables[1].Rows[i]["BILL_ID"] == DBNull.Value ? 0 : Convert.ToInt64(ds.Tables[1].Rows[i]["BILL_ID"]);
                            obj2.SL_NO = ds.Tables[1].Rows[i]["SL_NO"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[1].Rows[i]["SL_NO"]);
                            obj2.DESCP = ds.Tables[1].Rows[i]["DESCP"] == DBNull.Value ? "" : ds.Tables[1].Rows[i]["DESCP"].ToString();
                            obj2.GROSS_AMT = ds.Tables[1].Rows[i]["GROSS_AMT"] == DBNull.Value ? 0 : Convert.ToDouble(ds.Tables[1].Rows[i]["GROSS_AMT"]);
                            obj2.SGST_PER = ds.Tables[1].Rows[i]["SGST_PER"] == DBNull.Value ? 0 : Convert.ToDouble(ds.Tables[1].Rows[i]["SGST_PER"]);
                            obj2.SGST_AMT = ds.Tables[1].Rows[i]["SGST_AMT"] == DBNull.Value ? 0 : Convert.ToDouble(ds.Tables[1].Rows[i]["SGST_AMT"]);
                            obj2.CGST_PER = ds.Tables[1].Rows[i]["CGST_PER"] == DBNull.Value ? 0 : Convert.ToDouble(ds.Tables[1].Rows[i]["CGST_PER"]);
                            obj2.CGST_AMT = ds.Tables[1].Rows[i]["CGST_AMT"] == DBNull.Value ? 0 : Convert.ToDouble(ds.Tables[1].Rows[i]["CGST_AMT"]);
                            obj2.IGST_PER = ds.Tables[1].Rows[i]["IGST_PER"] == DBNull.Value ? 0 : Convert.ToDouble(ds.Tables[1].Rows[i]["IGST_PER"]);
                            obj2.IGST_AMT = ds.Tables[1].Rows[i]["IGST_AMT"] == DBNull.Value ? 0 : Convert.ToDouble(ds.Tables[1].Rows[i]["IGST_AMT"]);
                            obj2.NET_AMT = ds.Tables[1].Rows[i]["NET_AMT"] == DBNull.Value ? 0 : Convert.ToDouble(ds.Tables[1].Rows[i]["NET_AMT"]);
                            obj2.REMARKS = ds.Tables[1].Rows[i]["REMARKS"] == DBNull.Value ? "" : ds.Tables[1].Rows[i]["REMARKS"].ToString();
                            objlst2.Add(obj2);
                        }
                        retval.SUBARRAY = objlst2.ToArray();                                             
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            retlst.Add(retval);
            return retlst;
        }

        public DbStatusEntity DeleteBill(int id)
        {
            DbStatusEntity objreturn = new DbStatusEntity();
            string CS = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;

            try
            {
                using (SqlConnection con = new SqlConnection(CS))
                {
                    SqlCommand cmd = new SqlCommand("USP_DeleteBill", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID", id);

                    cmd.Parameters.Add("@RESULT", SqlDbType.Int);
                    cmd.Parameters["@RESULT"].Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@CNT", SqlDbType.Int);
                    cmd.Parameters["@CNT"].Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@MSG", SqlDbType.NVarChar, 500);
                    cmd.Parameters["@MSG"].Direction = ParameterDirection.Output;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    objreturn.RESULT = Convert.ToInt32(cmd.Parameters["@RESULT"].Value);
                    objreturn.CNT = Convert.ToInt32(cmd.Parameters["@CNT"].Value);
                    objreturn.MSG = Convert.ToString(cmd.Parameters["@MSG"].Value);
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return objreturn;
        }

        public DbStatusEntity InsertBill(BillParamEntity obj)
        {
            DbStatusEntity objreturn = new DbStatusEntity();
            string CS = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;

            try
            {
                using (SqlConnection con = new SqlConnection(CS))
                {
                    SqlCommand cmd = new SqlCommand("USP_InsertTaskMaster", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@C_ID", obj.C_ID);
                    cmd.Parameters.AddWithValue("@C_NO", obj.C_NO);
                    cmd.Parameters.AddWithValue("@FILE_NO", obj.FILE_NO);
                    cmd.Parameters.AddWithValue("@C_NAME", obj.C_NAME);
                    cmd.Parameters.AddWithValue("@BILL_DATE", obj.BILL_DATE);
                    cmd.Parameters.AddWithValue("@DUE_DATE", obj.DUE_DATE);
                    cmd.Parameters.AddWithValue("@PAYMODE_ID", obj.PAYMODE_ID);
                    cmd.Parameters.AddWithValue("@PAYMODE_NAME", obj.PAYMODE_NAME);
                    cmd.Parameters.AddWithValue("@TASK_ID", obj.TASK_ID);
                    cmd.Parameters.AddWithValue("@TASK_NO", obj.TASK_NO);
                    cmd.Parameters.AddWithValue("@GROSS_AMT", obj.GROSS_AMT);
                    cmd.Parameters.AddWithValue("@SGST_AMT", obj.SGST_AMT);
                    cmd.Parameters.AddWithValue("@CGST_AMT", obj.CGST_AMT);
                    cmd.Parameters.AddWithValue("@IGST_AMT", obj.IGST_AMT);
                    cmd.Parameters.AddWithValue("@OTH_AMT", obj.OTH_AMT);
                    cmd.Parameters.AddWithValue("@NET_AMT", obj.NET_AMT);
                    cmd.Parameters.AddWithValue("@REMARKS", obj.REMARKS);                 
    

                    DataTable dtsub = new DataTable();
                    dtsub.Columns.Add("SL_NO", typeof(int));
                    dtsub.Columns.Add("DESCP", typeof(string));
                    dtsub.Columns.Add("GROSS_AMT", typeof(Double));
                    dtsub.Columns.Add("SGST_PER", typeof(Double));
                    dtsub.Columns.Add("SGST_AMT", typeof(Double));
                    dtsub.Columns.Add("CGST_PER", typeof(Double));
                    dtsub.Columns.Add("CGST_AMT", typeof(Double));
                    dtsub.Columns.Add("IGST_PER", typeof(Double));
                    dtsub.Columns.Add("IGST_AMT", typeof(Double));
                    dtsub.Columns.Add("NET_AMT", typeof(Double));
                    dtsub.Columns.Add("REMARKS", typeof(Double));                 
	

                    foreach (BillSubEntity objsub in obj.SUBARRAY)
                    {
                        DataRow dr = dtsub.NewRow();                       
                        dr["SL_NO"] = objsub.SL_NO;
                        dr["DESCP"] = objsub.DESCP;
                        dr["GROSS_AMT"] = objsub.GROSS_AMT;
                        dr["SGST_PER"] = objsub.SGST_PER;
                        dr["SGST_AMT"] = objsub.SGST_AMT;
                        dr["CGST_PER"] = objsub.CGST_PER;
                        dr["CGST_AMT"] = objsub.CGST_AMT;
                        dr["IGST_PER"] = objsub.IGST_PER;
                        dr["IGST_AMT"] = objsub.IGST_AMT;
                        dr["NET_AMT"] = objsub.NET_AMT;
                        dr["REMARKS"] = objsub.REMARKS;
                        dtsub.Rows.Add(dr);
                    }

                    SqlParameter sqlParam = cmd.Parameters.AddWithValue("@TVP", dtsub);
                    sqlParam.SqlDbType = SqlDbType.Structured;                   

                    cmd.Parameters.Add("@RESULT", SqlDbType.Int);
                    cmd.Parameters["@RESULT"].Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@CNT", SqlDbType.Int);
                    cmd.Parameters["@CNT"].Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@MSG", SqlDbType.NVarChar, 500);
                    cmd.Parameters["@MSG"].Direction = ParameterDirection.Output;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    objreturn.RESULT = Convert.ToInt32(cmd.Parameters["@RESULT"].Value);
                    objreturn.CNT = Convert.ToInt32(cmd.Parameters["@CNT"].Value);
                    objreturn.MSG = Convert.ToString(cmd.Parameters["@MSG"].Value);
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return objreturn;
        }

        public DbStatusEntity UpdateBill(BillParamEntity obj, Int64 id)
        {
            DbStatusEntity objreturn = new DbStatusEntity();
            string CS = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;

            try
            {
                using (SqlConnection con = new SqlConnection(CS))
                {
                    SqlCommand cmd = new SqlCommand("USP_UpdateTaskMaster", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@BILL_ID", id);
                    cmd.Parameters.AddWithValue("@C_ID", obj.C_ID);
                    cmd.Parameters.AddWithValue("@C_NO", obj.C_NO);
                    cmd.Parameters.AddWithValue("@FILE_NO", obj.FILE_NO);
                    cmd.Parameters.AddWithValue("@C_NAME", obj.C_NAME);
                    cmd.Parameters.AddWithValue("@BILL_DATE", obj.BILL_DATE);
                    cmd.Parameters.AddWithValue("@DUE_DATE", obj.DUE_DATE);
                    cmd.Parameters.AddWithValue("@PAYMODE_ID", obj.PAYMODE_ID);
                    cmd.Parameters.AddWithValue("@PAYMODE_NAME", obj.PAYMODE_NAME);
                    cmd.Parameters.AddWithValue("@TASK_ID", obj.TASK_ID);
                    cmd.Parameters.AddWithValue("@TASK_NO", obj.TASK_NO);
                    cmd.Parameters.AddWithValue("@GROSS_AMT", obj.GROSS_AMT);
                    cmd.Parameters.AddWithValue("@SGST_AMT", obj.SGST_AMT);
                    cmd.Parameters.AddWithValue("@CGST_AMT", obj.CGST_AMT);
                    cmd.Parameters.AddWithValue("@IGST_AMT", obj.IGST_AMT);
                    cmd.Parameters.AddWithValue("@OTH_AMT", obj.OTH_AMT);
                    cmd.Parameters.AddWithValue("@NET_AMT", obj.NET_AMT);
                    cmd.Parameters.AddWithValue("@REMARKS", obj.REMARKS);


                    DataTable dtsub = new DataTable();
                    dtsub.Columns.Add("SL_NO", typeof(int));
                    dtsub.Columns.Add("DESCP", typeof(string));
                    dtsub.Columns.Add("GROSS_AMT", typeof(Double));
                    dtsub.Columns.Add("SGST_PER", typeof(Double));
                    dtsub.Columns.Add("SGST_AMT", typeof(Double));
                    dtsub.Columns.Add("CGST_PER", typeof(Double));
                    dtsub.Columns.Add("CGST_AMT", typeof(Double));
                    dtsub.Columns.Add("IGST_PER", typeof(Double));
                    dtsub.Columns.Add("IGST_AMT", typeof(Double));
                    dtsub.Columns.Add("NET_AMT", typeof(Double));
                    dtsub.Columns.Add("REMARKS", typeof(Double));


                    foreach (BillSubEntity objsub in obj.SUBARRAY)
                    {
                        DataRow dr = dtsub.NewRow();
                        dr["SL_NO"] = objsub.SL_NO;
                        dr["DESCP"] = objsub.DESCP;
                        dr["GROSS_AMT"] = objsub.GROSS_AMT;
                        dr["SGST_PER"] = objsub.SGST_PER;
                        dr["SGST_AMT"] = objsub.SGST_AMT;
                        dr["CGST_PER"] = objsub.CGST_PER;
                        dr["CGST_AMT"] = objsub.CGST_AMT;
                        dr["IGST_PER"] = objsub.IGST_PER;
                        dr["IGST_AMT"] = objsub.IGST_AMT;
                        dr["NET_AMT"] = objsub.NET_AMT;
                        dr["REMARKS"] = objsub.REMARKS;
                        dtsub.Rows.Add(dr);
                    }

                    SqlParameter sqlParam = cmd.Parameters.AddWithValue("@TVP", dtsub);
                    sqlParam.SqlDbType = SqlDbType.Structured;

                    cmd.Parameters.Add("@RESULT", SqlDbType.Int);
                    cmd.Parameters["@RESULT"].Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@CNT", SqlDbType.Int);
                    cmd.Parameters["@CNT"].Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@MSG", SqlDbType.NVarChar, 500);
                    cmd.Parameters["@MSG"].Direction = ParameterDirection.Output;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    objreturn.RESULT = Convert.ToInt32(cmd.Parameters["@RESULT"].Value);
                    objreturn.CNT = Convert.ToInt32(cmd.Parameters["@CNT"].Value);
                    objreturn.MSG = Convert.ToString(cmd.Parameters["@MSG"].Value);
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return objreturn;
        }

    }
}
