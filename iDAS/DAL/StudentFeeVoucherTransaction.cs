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
    public class StudentFeeVoucherTransaction
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

        #region "Insert Student Monthly Bulk Fee Insert Transaction"
        public int InsertUnPaidMonthlyFeeVoucherTransaction(string VoucherDueDate, DataTable studentList, decimal AddedBy)
        {

            //Initialization
            conn = new SqlConnection(ConnectionString());
            strErrorMsg = string.Empty;
            intStatus = 0;
            int totalStudent = studentList.Rows.Count;
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
                foreach (DataRow aStudent in studentList.Rows)
                {
                    double voucherId = 0;
                    SqlParameter[] paramStudentFee = new SqlParameter[3];
                    paramStudentFee[0] = new SqlParameter("@StudentId", aStudent["StudentId"]);
                    paramStudentFee[1] = new SqlParameter("@VoucherDueDate", VoucherDueDate);
                    paramStudentFee[2] = new SqlParameter("@AddedBy", AddedBy);

                    voucherId = DALCommon.ExecuteTransactionReturnIdentity("[sp_Admin_InsertStudentFeeVoucher]", cmd, paramStudentFee);
                    if (voucherId >= 1)
                    {
                        DataTable tblUnPaidFeeMonth = new DataTable();
                        SqlParameter[] paramUnPaidFeeMonth = new SqlParameter[3];
                        paramUnPaidFeeMonth[0] = new SqlParameter("@VoucherId", voucherId);
                        paramUnPaidFeeMonth[1] = new SqlParameter("@StudentId ", aStudent["StudentId"]);
                        paramUnPaidFeeMonth[2] = new SqlParameter("@AddedBy ", AddedBy);
                        //Bulk Monthly Fee Voucher Creating
                        ResultStatus = DALCommon.ExecuteTransaction("[sp_Admin_InsertFeeVoucherMonthsDataByVoucherID]", cmd, paramUnPaidFeeMonth);

                        if (ResultStatus > 0)
                        {
                            updatedTotalRecord = updatedTotalRecord + 1;
                            
                        }
                        else
                        {
                            intStatus = -2;
                            strErrorMsg = "Error Occured while inserting record";
                            DALUtility.ErrorLog("Error occured inserting Monthly Fee Voucher record", "StudentFeeVoucherTransaction.cs, InsertUnPaidMonthlyFeeVoucherTransaction");
                        }
                    }
                    else {
                        intStatus = -3;
                        strErrorMsg = "Error Occured while inserting record";
                        DALUtility.ErrorLog("Error occured generating VoucherId", "StudentFeeVoucherTransaction.cs, InsertUnPaidMonthlyFeeVoucherTransaction");
                    }
                }
                //If Error Field Is Not Empty
                if ((!string.IsNullOrEmpty(strErrorMsg)))
                {
                    DALUtility.ErrorLog(strErrorMsg, "BulkFeeInsertionTransaction.cs, InsertStudentMonthlyFeeInsertTransaction");
                }
                if (updatedTotalRecord == totalStudent)
                {
                    sqlTrans.Commit();
                    intStatus = 1; 
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