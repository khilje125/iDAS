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
    public class BulkFeeInsertionTransaction
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
        public int InsertStudentMonthlyFeeInsertTransaction(DataTable studentList, ModelStudentFee objModelStudentFee,int FeeMonth,decimal AddedBy)
        {

            //Initialization
            conn = new SqlConnection(ConnectionString());
            strErrorMsg = string.Empty;
            intStatus = 0;
            int totalStudent = studentList.Rows.Count;
            int updatedTotalRecord = 0;
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
                    SqlParameter[] paramStudentFee = new SqlParameter[12];

                    paramStudentFee[0] = new SqlParameter("@StudentId", aStudent["StudentId"]);
                    paramStudentFee[1] = new SqlParameter("@ComputerCode", aStudent["ComputerCode"]);
                    paramStudentFee[2] = new SqlParameter("@FeeMonth", FeeMonth);
                    paramStudentFee[3] = new SqlParameter("@MonthlyFee", aStudent["MonthlyFee"]);
                    paramStudentFee[4] = new SqlParameter("@AdmissionFee", objModelStudentFee.AdmissionFee);
                    paramStudentFee[5] = new SqlParameter("@Admission", objModelStudentFee.Admission);
                    paramStudentFee[6] = new SqlParameter("@ReAdmissionFee", objModelStudentFee.ReAdmissionFee);
                    paramStudentFee[7] = new SqlParameter("@RegistrationFee", objModelStudentFee.RegistrationFee);
                    paramStudentFee[8] = new SqlParameter("@ComputerFee", objModelStudentFee.ComputerFee);
                    paramStudentFee[9] = new SqlParameter("@Fine", objModelStudentFee.Fine);
                    paramStudentFee[10] = new SqlParameter("@ExamFee", objModelStudentFee.ExamFee);
                    paramStudentFee[11] = new SqlParameter("@GeneratorFee", objModelStudentFee.GeneratorFee);
                    
                    intStatus = DALCommon.ExecuteTransaction("[sp_Admin_InsertStudentMonthlyFeeBulkStudentId]", cmd, paramStudentFee);
                    if (intStatus >= 1)
                    {
                        updatedTotalRecord = updatedTotalRecord + 1;
                    }
                    else {
                        DALUtility.ErrorLogBulkFeeInsertion(aStudent["StudentId"].ToString(), aStudent["MonthlyFee"].ToString(), "Bulk Fee noy Insert", AddedBy.ToString());
                    }
                }
                if (totalStudent == updatedTotalRecord)
                {
                    //Commit Transaction
                    sqlTrans.Commit();
                    intStatus = 1;
                }
                else if (intStatus == -1)
                {
                    strErrorMsg = "Error Occured while inserting record";
                }
                else if (intStatus == -2)
                {
                    strErrorMsg = "Fee already exists for this month";
                }

                else if (intStatus == -3)
                {
                    strErrorMsg = "Error occured while generation Serial Number index";
                }

                else if (intStatus == -4)
                {
                    strErrorMsg = "Student stucked off";
                }
                //If Error Field Is Not Empty
                if ((!string.IsNullOrEmpty(strErrorMsg)))
                {
                    DALUtility.ErrorLog(strErrorMsg, "BulkFeeInsertionTransaction.cs, InsertStudentMonthlyFeeInsertTransaction");
                }

            }
            catch (Exception ex)
            {
                sqlTrans.Rollback();
                DALUtility.ErrorLog(ex.Message, "BulkFeeInsertionTransaction.cs, InsertStudentMonthlyFeeInsertTransaction");
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