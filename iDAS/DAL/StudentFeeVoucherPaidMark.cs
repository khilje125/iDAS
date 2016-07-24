using iDAS.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace iDAS.DAL
{
    public class StudentFeeVoucherPaidMark
    {
        #region "Declare Variables"
        private SqlTransaction sqlTrans = null;
        private SqlCommand cmd = null;
        private int intStatus = 0;
        private SqlConnection conn = null;
        #endregion
        private string strErrorMsg = string.Empty;

        #region "Define Connection String"
        private string ConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["DASAdminDbCon"].ConnectionString;
        }
        #endregion

        #region "Insert Student Monthly Fee Paid Mark Bulk By Fee ID "
        public int InsertFeeVoucherFeePaidTransaction(string VoucherPaidDate, double FeeVoucherId, DataTable FeeIdList, double AddedBy)
        {

            //Initialization
            conn = new SqlConnection(ConnectionString());
            strErrorMsg = string.Empty;
            intStatus = 0;
            int totalFeeId = FeeIdList.Rows.Count;
            int updatedTotalRecord = 0;
            int ResultStatus = 0;
            

            try
            {
                //Open Connection
                if ((conn.State == System.Data.ConnectionState.Closed))
                {
                    conn.Open();
                }

                //Begin Transaction
                sqlTrans = conn.BeginTransaction();

                //Initialize Command
                cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.Transaction = sqlTrans;

                //Update Transaction Response
                foreach (DataRow aFeeId in FeeIdList.Rows)
                {
                    int ResultRecord = 0;
                    SqlParameter[] paramStudentFee = new SqlParameter[3];
                    paramStudentFee[0] = new SqlParameter("@FeeId", aFeeId["FeeId"]);
                    paramStudentFee[1] = new SqlParameter("@FeeDate", VoucherPaidDate);
                    paramStudentFee[2] = new SqlParameter("@AddedBy", AddedBy);

                    ResultRecord = DALCommon.ExecuteTransaction("[sp_Admin_UpdateStudentFeePaidMarkedByFeeId]", cmd, paramStudentFee);

                    if (ResultRecord > 0)
                    {
                        updatedTotalRecord = updatedTotalRecord + 1;

                    }
                    else
                    {
                        intStatus = -2;
                        strErrorMsg = "Error occured while marking fee month paid with fee Id";
                      }
                }

                if (updatedTotalRecord == totalFeeId)
                {
                    SqlParameter[] paramFeeVoucher = new SqlParameter[1];
                    paramFeeVoucher[0] = new SqlParameter("@VoucherId", FeeVoucherId);
                    //Mark voucher paid
                    ResultStatus = DALCommon.ExecuteTransaction("[sp_Admin_UpdateStudentFeeVoucherPaidMarkedByVoucherId]", cmd, paramFeeVoucher);
                    
                    if (ResultStatus > 0)
                    {
                        sqlTrans.Commit();
                        intStatus = 1;
                    }
                }
                else
                {
                    intStatus = -3;
                    strErrorMsg = "Error Occured while marking fee voucher paid";
                 }

                //If Error Field Is Not Empty
                if ((!string.IsNullOrEmpty(strErrorMsg)))
                {
                    DALUtility.ErrorLog(strErrorMsg, "BulkFeeInsertionTransaction.cs, InsertStudentMonthlyFeeInsertTransaction, InsertFeeVoucherFeePaidTransaction");
                }
                
            }

            catch (Exception ex)
            {
                sqlTrans.Rollback();
                DALUtility.ErrorLog(ex.Message, "StudentFeeVoucherTransaction.cs, InsertUnPaidMonthlyFeeVoucherTransaction");
            }
            finally
            {
                if ((conn.State == System.Data.ConnectionState.Open))
                {
                    conn.Close();
                }
                cmd = null;
                sqlTrans = null;
            }

            return intStatus;
        }
        #endregion
    }
}