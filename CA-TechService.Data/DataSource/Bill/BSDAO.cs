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
    public class BSDAO
    {
        public List<BSListingEntity> GetBSList(string fromdate, string todate)
        {
            string CS = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            SqlDataAdapter adapter;
            DataSet ds = new DataSet();
            List<BSListingEntity> retlst = new List<BSListingEntity>();
            try
            {
                using (SqlConnection con = new SqlConnection(CS))
                {
                    SqlCommand cmd = new SqlCommand("USP_GetBillSettlementList", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@DateFrom", fromdate);
                    cmd.Parameters.AddWithValue("@DateTo", todate);
                    con.Open();
                    adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(ds);

                    for (int i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
                    {
                        BSListingEntity obj = new BSListingEntity();
                        obj.BS_ID = ds.Tables[0].Rows[i]["BS_ID"] == DBNull.Value ? 0 : Convert.ToInt64(ds.Tables[0].Rows[i]["BS_ID"]);
                        obj.BS_NO = ds.Tables[0].Rows[i]["BS_NO"] == DBNull.Value ? 0 : Convert.ToInt64(ds.Tables[0].Rows[i]["BS_NO"]);
                        obj.BS_DATE = ds.Tables[0].Rows[i]["BS_DATE"] == DBNull.Value ? "" : ds.Tables[0].Rows[i]["BS_DATE"].ToString();
                        obj.C_NO = ds.Tables[0].Rows[i]["C_NO"] == DBNull.Value ? 0 : Convert.ToInt64(ds.Tables[0].Rows[i]["C_NO"]);
                        obj.FILE_NO = ds.Tables[0].Rows[i]["FILE_NO"] == DBNull.Value ? "" : ds.Tables[0].Rows[i]["FILE_NO"].ToString();
                        obj.C_NAME = ds.Tables[0].Rows[i]["C_NAME"] == DBNull.Value ? "" : ds.Tables[0].Rows[i]["C_NAME"].ToString();
                        obj.REMARKS = ds.Tables[0].Rows[i]["REMARKS"] == DBNull.Value ? "" : ds.Tables[0].Rows[i]["REMARKS"].ToString();
                        obj.PAYMODE_NAME = ds.Tables[0].Rows[i]["PAYMODE_NAME"] == DBNull.Value ? "" : ds.Tables[0].Rows[i]["PAYMODE_NAME"].ToString();
                        obj.BS_AMT = ds.Tables[0].Rows[i]["BS_AMT"] == DBNull.Value ? 0 : Convert.ToDouble(ds.Tables[0].Rows[i]["BS_AMT"]);
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
        public DbStatusEntity DeleteBS(int id)
        {
            DbStatusEntity objreturn = new DbStatusEntity();
            string CS = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;

            try
            {
                using (SqlConnection con = new SqlConnection(CS))
                {
                    SqlCommand cmd = new SqlCommand("USP_DeleteBillSettlement", con);
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
        public DbStatusEntity InsertBS(BSParamEntity obj)
        {
            DbStatusEntity objreturn = new DbStatusEntity();
            string CS = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;

            try
            {
                using (SqlConnection con = new SqlConnection(CS))
                {
                    SqlCommand cmd = new SqlCommand("USP_InsertBillSettlement", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@C_ID", obj.C_ID);
                    cmd.Parameters.AddWithValue("@C_NO", obj.C_NO);
                    cmd.Parameters.AddWithValue("@FILE_NO", obj.FILE_NO);
                    cmd.Parameters.AddWithValue("@C_NAME", obj.C_NAME);
                    cmd.Parameters.AddWithValue("@BS_DATE", obj.BS_DATE);                    
                    cmd.Parameters.AddWithValue("@PAYMODE_ID", obj.PAYMODE_ID);
                    cmd.Parameters.AddWithValue("@PAYMODE_NAME", obj.PAYMODE_NAME);
                    cmd.Parameters.AddWithValue("@BS_AMT", obj.BS_AMT);
                    cmd.Parameters.AddWithValue("@REMARKS", obj.REMARKS);

                    DataTable dtsub = new DataTable();
                    dtsub.Columns.Add("SL_NO", typeof(int));
                    dtsub.Columns.Add("BILL_ID", typeof(int));
                    dtsub.Columns.Add("BILL_NO", typeof(int));
                    dtsub.Columns.Add("BILL_DATE", typeof(string));                    
                    dtsub.Columns.Add("BILL_AMT", typeof(Double));
                    dtsub.Columns.Add("PAID_AMT", typeof(Double));
                    dtsub.Columns.Add("BAL_AMT", typeof(Double));
                    dtsub.Columns.Add("BS_AMT", typeof(Double));

                    foreach (BSsubEntity objsub in obj.SUBARRAY)
                    {
                        DataRow dr = dtsub.NewRow();
                        dr["SL_NO"] = objsub.SL_NO;
                        dr["BILL_ID"] = objsub.BILL_ID;
                        dr["BILL_NO"] = objsub.BILL_NO;
                        dr["BILL_DATE"] = objsub.BILL_DATE;
                        dr["BILL_AMT"] = objsub.BILL_AMT;
                        dr["PAID_AMT"] = objsub.PAID_AMT;
                        dr["BAL_AMT"] = objsub.BAL_AMT;
                        dr["BS_AMT"] = objsub.BS_AMT;                       
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
        public List<Int64> CheckVoidBSEnrty(long id)
        {
            string CS = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            SqlDataAdapter adapter;
            DataSet ds = new DataSet();
            List<Int64> retvallst = new List<Int64>();
            try
            {
                using (SqlConnection con = new SqlConnection(CS))
                {
                    Int64 retval = 0;
                    SqlCommand cmd = new SqlCommand("USP_GetBillSettlementVoidDetailsbyID", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID", id);
                    con.Open();
                    adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(ds);
                    for (int i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
                    {
                        retval = ds.Tables[0].Rows[i]["BS_ID"] == DBNull.Value ? 0 : Convert.ToInt64(ds.Tables[0].Rows[i]["BS_ID"]);
                    }
                    retvallst.Add(retval);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return retvallst;
        }
        public DbStatusEntity VoidBSEntry(Int64 id)
        {
            DbStatusEntity objreturn = new DbStatusEntity();
            string CS = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            try
            {
                using (SqlConnection con = new SqlConnection(CS))
                {
                    SqlCommand cmd = new SqlCommand("USP_VoidBillSettlement", con);
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
        public List<BsEntity> EditBillSettlement(Int64 id)
        {
            string CS = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            SqlDataAdapter adapter;
            DataSet ds = new DataSet();
            List<BsEntity> retlst = new List<BsEntity>();
            BsEntity retval = new BsEntity();
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
                        List<BSMainEntity> objlst1 = new List<BSMainEntity>();
                        for (int i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
                        {
                            BSMainEntity obj1 = new BSMainEntity();
                            obj1.BS_ID = ds.Tables[0].Rows[i]["BS_ID"] == DBNull.Value ? 0 : Convert.ToInt64(ds.Tables[0].Rows[i]["BS_ID"]);
                            obj1.BS_NO = ds.Tables[0].Rows[i]["BS_NO"] == DBNull.Value ? 0 : Convert.ToInt64(ds.Tables[0].Rows[i]["BS_NO"]);
                            obj1.BS_DATE = ds.Tables[0].Rows[i]["BS_DATE"] == DBNull.Value ? "" : ds.Tables[0].Rows[i]["BS_DATE"].ToString();
                            obj1.C_ID = ds.Tables[0].Rows[i]["C_ID"] == DBNull.Value ? 0 : Convert.ToInt64(ds.Tables[0].Rows[i]["C_ID"]);
                            obj1.C_NO = ds.Tables[0].Rows[i]["C_NO"] == DBNull.Value ? 0 : Convert.ToInt64(ds.Tables[0].Rows[i]["C_NO"]);
                            obj1.FILE_NO = ds.Tables[0].Rows[i]["FILE_NO"] == DBNull.Value ? "" : ds.Tables[0].Rows[i]["FILE_NO"].ToString();
                            obj1.C_NAME = ds.Tables[0].Rows[i]["C_NAME"] == DBNull.Value ? "" : ds.Tables[0].Rows[i]["C_NAME"].ToString();
                            obj1.PAYMODE_ID = ds.Tables[0].Rows[i]["PAYMODE_ID"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["PAYMODE_ID"]);
                            obj1.PAYMODE_NAME = ds.Tables[0].Rows[i]["PAYMODE_NAME"] == DBNull.Value ? "" : ds.Tables[0].Rows[i]["PAYMODE_NAME"].ToString();
                            obj1.BS_AMT = ds.Tables[0].Rows[i]["BS_AMT"] == DBNull.Value ? 0 : Convert.ToDouble(ds.Tables[0].Rows[i]["BS_AMT"]);
                            obj1.REMARKS = ds.Tables[0].Rows[i]["REMARKS"] == DBNull.Value ? "" : ds.Tables[0].Rows[i]["REMARKS"].ToString();
                            obj1.VOID_STATUS = ds.Tables[0].Rows[i]["VOID_STATUS"] == DBNull.Value ? true : Convert.ToBoolean(ds.Tables[0].Rows[i]["VOID_STATUS"]);
                            obj1.C_DETAILS = ds.Tables[0].Rows[i]["C_DETAILS"] == DBNull.Value ? "" : ds.Tables[0].Rows[i]["C_DETAILS"].ToString();
                            obj1.PAN = ds.Tables[0].Rows[i]["PAN"] == DBNull.Value ? "" : ds.Tables[0].Rows[i]["PAN"].ToString();
                            obj1.GSTIN = ds.Tables[0].Rows[i]["GSTIN"] == DBNull.Value ? "" : ds.Tables[0].Rows[i]["GSTIN"].ToString();
                            objlst1.Add(obj1);
                        }
                        retval.MAINARRAY = objlst1.ToArray();

                        List<BSsubEntity> objlst2 = new List<BSsubEntity>();
                        for (int i = 0; i <= ds.Tables[1].Rows.Count - 1; i++)
                        {
                            BSsubEntity obj2 = new BSsubEntity();
                            obj2.SUB_BS_ID = ds.Tables[1].Rows[i]["SUB_BS_ID"] == DBNull.Value ? 0 : Convert.ToInt64(ds.Tables[1].Rows[i]["SUB_BS_ID"]);
                            obj2.BS_ID = ds.Tables[1].Rows[i]["BS_ID"] == DBNull.Value ? 0 : Convert.ToInt64(ds.Tables[1].Rows[i]["BS_ID"]);
                            obj2.SL_NO = ds.Tables[1].Rows[i]["SL_NO"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[1].Rows[i]["SL_NO"]);
                            obj2.BILL_ID = ds.Tables[1].Rows[i]["BILL_ID"] == DBNull.Value ? 0 : Convert.ToInt64(ds.Tables[1].Rows[i]["BILL_ID"]);
                            obj2.BILL_NO = ds.Tables[1].Rows[i]["BILL_NO"] == DBNull.Value ? 0 : Convert.ToInt64(ds.Tables[1].Rows[i]["BILL_NO"]);
                            obj2.BILL_DATE = ds.Tables[1].Rows[i]["BILL_DATE"] == DBNull.Value ? "" : ds.Tables[1].Rows[i]["BILL_DATE"].ToString();
                            obj2.BILL_AMT = ds.Tables[1].Rows[i]["BILL_AMT"] == DBNull.Value ? 0 : Convert.ToDouble(ds.Tables[1].Rows[i]["BILL_AMT"]);
                            obj2.PAID_AMT = ds.Tables[1].Rows[i]["PAID_AMT"] == DBNull.Value ? 0 : Convert.ToDouble(ds.Tables[1].Rows[i]["PAID_AMT"]);
                            obj2.BAL_AMT = ds.Tables[1].Rows[i]["BAL_AMT"] == DBNull.Value ? 0 : Convert.ToDouble(ds.Tables[1].Rows[i]["BAL_AMT"]);
                            obj2.BS_AMT = ds.Tables[1].Rows[i]["BS_AMT"] == DBNull.Value ? 0 : Convert.ToDouble(ds.Tables[1].Rows[i]["BS_AMT"]);                        

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
    }
}
