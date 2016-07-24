using iDAS.DAL;
using iDAS.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace iDAS.BLL
{
    public class BLLStudentFee
    {
        public List<ModelStudent> GetStudentListReport(string SearchCriteria, decimal SchoolAccountId)
        {
            List<ModelStudent> lstModelStudent = new List<ModelStudent>();
            DataTable tblStudentList = new DataTable();
            SqlParameter[] param = new SqlParameter[2];

            param[0] = new SqlParameter("@SearchCriteria", String.IsNullOrEmpty(SearchCriteria) ? "" : SearchCriteria);
            param[1] = new SqlParameter("@SchoolAccountId", SchoolAccountId);

            tblStudentList = DALCommon.GetDataUsingDataTable("[sp_Admin_GetStudentListBySchoolId]", param);
            if (tblStudentList.Rows.Count > 0)
            {
                foreach (DataRow aStudent in tblStudentList.Rows)
                {
                    ModelStudent objModelStudent = new ModelStudent();
                    objModelStudent.StudentId = Convert.ToInt32(aStudent["StudentId"]);
                    objModelStudent.ComputerCode = Convert.ToInt32(aStudent["ComputerCode"]);
                    objModelStudent.RegNo = Convert.ToInt32(aStudent["RegNo"]);
                    objModelStudent.StudentName = Convert.ToString(aStudent["StudentName"]);
                    objModelStudent.FatherName = Convert.ToString(aStudent["FatherName"]);
                    objModelStudent.MonthlyFee = Convert.ToInt32(aStudent["MonthlyFee"]);
                    objModelStudent.Sex = Convert.ToInt32(aStudent["Gender"]);
                    objModelStudent.DateOfBirth = Convert.ToDateTime(aStudent["DateOfBirth"]);
                    objModelStudent.Status = Convert.ToInt32(aStudent["Status"]);
                    objModelStudent.StatusText = Convert.ToString(aStudent["StatusText"]);
                    objModelStudent.ClassNameText = Convert.ToString(aStudent["ClassName"]);
                    objModelStudent.SectionNameText = Convert.ToString(aStudent["SectionName"]);

                    objModelStudent.Address = Convert.ToString(aStudent["Address"]);
                    objModelStudent.HomeNumber = Convert.ToString(aStudent["HomeNumber"]);
                    objModelStudent.OfficeNumber = Convert.ToString(aStudent["OfficeNumber"]);
                    objModelStudent.MoblieNumber = Convert.ToString(aStudent["MoblieNumber"]);
                    lstModelStudent.Add(objModelStudent);
                }
            }
            return lstModelStudent;
        }

        public DataTable GetStudentDataTableForReport(string SearchCriteria, decimal SchoolAccountId)
        {
            List<ModelStudent> lstModelStudent = new List<ModelStudent>();
            DataTable tblStudentList = new DataTable();
            SqlParameter[] param = new SqlParameter[2];

            param[0] = new SqlParameter("@SearchCriteria", String.IsNullOrEmpty(SearchCriteria) ? "" : SearchCriteria);
            param[1] = new SqlParameter("@SchoolAccountId", SchoolAccountId);

            return DALCommon.GetDataUsingDataTable("[sp_Admin_GetStudentListBySchoolId]", param);
        }

        public ModelStudent GetStudentFeeInfoById(decimal StudentID)
        {
            ModelStudent objModelStudent = new ModelStudent();
            DataTable tblStudentInfo = new DataTable();
            SqlParameter[] param = new SqlParameter[1];

            param[0] = new SqlParameter("@StudentId", StudentID);

            tblStudentInfo = DALCommon.GetDataUsingDataTable("[sp_Admin_GetStudentInfoByStudentId]", param);
            if (tblStudentInfo.Rows.Count > 0)
            {
                objModelStudent.StudentId = Convert.ToInt32(tblStudentInfo.Rows[0]["StudentId"].ToString());
                objModelStudent.ComputerCode = Convert.ToInt32(tblStudentInfo.Rows[0]["ComputerCode"].ToString());
                objModelStudent.RegNo = Convert.ToInt32(tblStudentInfo.Rows[0]["RegNo"].ToString());
                objModelStudent.StudentName = Convert.ToString(tblStudentInfo.Rows[0]["StudentName"].ToString());
                objModelStudent.FatherName = Convert.ToString(tblStudentInfo.Rows[0]["FatherName"].ToString());
                objModelStudent.MonthlyFee = Convert.ToInt32(tblStudentInfo.Rows[0]["MonthlyFee"].ToString());
                objModelStudent.StudentClass.ClassName = Convert.ToString(tblStudentInfo.Rows[0]["ClassName"].ToString());
                objModelStudent.StudentSection.SectionName = Convert.ToString(tblStudentInfo.Rows[0]["SectionName"].ToString());

                objModelStudent.Status = Convert.ToInt32(tblStudentInfo.Rows[0]["Status"].ToString());
                objModelStudent.ClassId = Convert.ToInt32(tblStudentInfo.Rows[0]["ClassId"].ToString());
                objModelStudent.SectionId = Convert.ToInt32(tblStudentInfo.Rows[0]["SectionId"].ToString());

                DataTable tblStudentFeeInfo = new DataTable();
                SqlParameter[] paramStudentFee = new SqlParameter[1];

                paramStudentFee[0] = new SqlParameter("@StudentId", StudentID);

                tblStudentFeeInfo = DALCommon.GetDataUsingDataTable("[sp_Admin_GetStudentMonthlyFeeByStudentId]", paramStudentFee);
                if (tblStudentFeeInfo.Rows.Count > 0)
                {
                    foreach (DataRow studentFeeRow in tblStudentFeeInfo.Rows)
                    {
                        ModelStudentFee objModelStudentFee = new ModelStudentFee();
                        objModelStudentFee.FeeId = Convert.ToDouble(studentFeeRow["FeeId"]);
                        objModelStudentFee.StudentId = Convert.ToDouble(studentFeeRow["StudentId"]);
                        objModelStudentFee.ComputerCode = Convert.ToInt32(studentFeeRow["ComputerCode"]);
                        objModelStudentFee.SerialNo = Convert.ToInt32(studentFeeRow["SerialNo"]);
                        if (!DBNull.Value.Equals(studentFeeRow["FeeDate"]))
                        {
                            objModelStudentFee.FeeDate = Convert.ToDateTime(studentFeeRow["FeeDate"].ToString());
                        }
                        else
                        {
                            objModelStudentFee.FeeDate = DateTime.Now;
                        }

                        objModelStudentFee.AdmissionFee = DALUtility.isNumericDBNULL(studentFeeRow["AdmissionFee"].ToString());
                        objModelStudentFee.Admission = DALUtility.isNumericDBNULL(studentFeeRow["Admission"].ToString());
                        objModelStudentFee.ReAdmissionFee = DALUtility.isNumericDBNULL(studentFeeRow["ReAdmissionFee"].ToString());
                        objModelStudentFee.RegistrationFee = DALUtility.isNumericDBNULL(studentFeeRow["RegistrationFee"].ToString());
                        objModelStudentFee.MonthlyFee = DALUtility.isNumericDBNULL(studentFeeRow["MonthlyFee"].ToString());
                        objModelStudentFee.ComputerFee = DALUtility.isNumericDBNULL(studentFeeRow["ComputerFee"].ToString());
                        objModelStudentFee.Fine = DALUtility.isNumericDBNULL(studentFeeRow["Fine"].ToString());
                        objModelStudentFee.ExamFee = DALUtility.isNumericDBNULL(studentFeeRow["ExamFee"].ToString());
                        objModelStudentFee.GeneratorFee = DALUtility.isNumericDBNULL(studentFeeRow["GeneratorFee"].ToString());
                        objModelStudentFee.FeeMonth = Convert.ToString(studentFeeRow["FeeMonth"]);
                        objModelStudentFee.Total = DALUtility.isNumericDBNULL(studentFeeRow["Total"].ToString());
                        objModelStudentFee.FeePaidStatus = Convert.ToInt32(studentFeeRow["FeePaidStatus"]);

                        objModelStudent.ModelStudentFee.Add(objModelStudentFee);
                    }
                }
            }
            return objModelStudent;
        }

        public ModelStudentFee GetStudentSinngleFeeInfoByFeeId(double FeeId)
        {
            ModelStudentFee objModelStudentFee = new ModelStudentFee();
            DataTable tblStudentFeeInfo = new DataTable();
            SqlParameter[] param = new SqlParameter[1];

            param[0] = new SqlParameter("@FeeId", FeeId);
            tblStudentFeeInfo = DALCommon.GetDataUsingDataTable("[sp_Admin_GetStudentSinngleFeeInfoByFeeId]", param);

            if (tblStudentFeeInfo.Rows.Count > 0)
            {
                objModelStudentFee.FeeId = Convert.ToDouble(tblStudentFeeInfo.Rows[0]["FeeId"]);
                objModelStudentFee.StudentId = Convert.ToInt32(tblStudentFeeInfo.Rows[0]["StudentId"]);
                objModelStudentFee.ComputerCode = Convert.ToInt32(tblStudentFeeInfo.Rows[0]["ComputerCode"]);
                objModelStudentFee.SerialNo = Convert.ToInt32(tblStudentFeeInfo.Rows[0]["SerialNo"]);
                objModelStudentFee.AdmissionFee = DALUtility.isNumericDBNULL(tblStudentFeeInfo.Rows[0]["AdmissionFee"].ToString());
                objModelStudentFee.Admission = DALUtility.isNumericDBNULL(tblStudentFeeInfo.Rows[0]["Admission"].ToString());
                objModelStudentFee.ReAdmissionFee = DALUtility.isNumericDBNULL(tblStudentFeeInfo.Rows[0]["ReAdmissionFee"].ToString());
                objModelStudentFee.RegistrationFee = DALUtility.isNumericDBNULL(tblStudentFeeInfo.Rows[0]["RegistrationFee"].ToString());
                objModelStudentFee.MonthlyFee = DALUtility.isNumericDBNULL(tblStudentFeeInfo.Rows[0]["MonthlyFee"].ToString());
                objModelStudentFee.ComputerFee = DALUtility.isNumericDBNULL(tblStudentFeeInfo.Rows[0]["ComputerFee"].ToString());
                objModelStudentFee.Fine = DALUtility.isNumericDBNULL(tblStudentFeeInfo.Rows[0]["Fine"].ToString());
                objModelStudentFee.ExamFee = DALUtility.isNumericDBNULL(tblStudentFeeInfo.Rows[0]["ExamFee"].ToString());
                objModelStudentFee.GeneratorFee = DALUtility.isNumericDBNULL(tblStudentFeeInfo.Rows[0]["GeneratorFee"].ToString());
                objModelStudentFee.FeeMonth = Convert.ToString(tblStudentFeeInfo.Rows[0]["FeeMonth"]);
                objModelStudentFee.Total = DALUtility.isNumericDBNULL(tblStudentFeeInfo.Rows[0]["Total"].ToString());
                objModelStudentFee.FeePaidStatus = Convert.ToInt32(tblStudentFeeInfo.Rows[0]["FeePaidStatus"].ToString());
            }
            return objModelStudentFee;
        }

        public int UpdateStudentSinngleFeeInfoByFeeId(ModelStudentFee objModelStudentFee)
        {
            SqlParameter[] param = new SqlParameter[10];

            param[0] = new SqlParameter("@FeeId", objModelStudentFee.FeeId);
            param[1] = new SqlParameter("@AdmissionFee", objModelStudentFee.AdmissionFee);
            param[2] = new SqlParameter("@Admission", objModelStudentFee.Admission);
            param[3] = new SqlParameter("@ReAdmissionFee", objModelStudentFee.ReAdmissionFee);
            param[4] = new SqlParameter("@RegistrationFee", objModelStudentFee.RegistrationFee);
            param[5] = new SqlParameter("@MonthlyFee", objModelStudentFee.MonthlyFee);
            param[6] = new SqlParameter("@ComputerFee", objModelStudentFee.ComputerFee);
            param[7] = new SqlParameter("@Fine", objModelStudentFee.Fine);
            param[8] = new SqlParameter("@ExamFee", objModelStudentFee.ExamFee);
            param[9] = new SqlParameter("@GeneratorFee", objModelStudentFee.GeneratorFee);

            return DALCommon.ExecuteNonQuery("[sp_Admin_UpdateStudentSinngleFeeInfoByFeeId]", param);
        }

        public List<ModelStudentFee> GetMonthlyFeeListByStudentID(decimal StudentID)
        {
            List<ModelStudentFee> objlstModelStudentFee = new List<ModelStudentFee>();
            DataTable tblStudentFeeInfo = new DataTable();
            SqlParameter[] paramStudentFee = new SqlParameter[1];

            paramStudentFee[0] = new SqlParameter("@StudentId", StudentID);

            tblStudentFeeInfo = DALCommon.GetDataUsingDataTable("[sp_Admin_GetStudentMonthlyFeeByStudentId]", paramStudentFee);
            if (tblStudentFeeInfo.Rows.Count > 0)
            {
                foreach (DataRow studentFeeRow in tblStudentFeeInfo.Rows)
                {
                    ModelStudentFee objModelStudentFee = new ModelStudentFee();
                    objModelStudentFee.FeeId = Convert.ToDouble(studentFeeRow["FeeId"]);
                    objModelStudentFee.StudentId = Convert.ToInt32(studentFeeRow["StudentId"]);
                    objModelStudentFee.ComputerCode = Convert.ToInt32(studentFeeRow["ComputerCode"]);
                    objModelStudentFee.SerialNo = Convert.ToInt32(studentFeeRow["SerialNo"]);
                    if (!DBNull.Value.Equals(studentFeeRow["FeeDate"]))
                    {
                        objModelStudentFee.FeeDate = Convert.ToDateTime(studentFeeRow["FeeDate"].ToString());
                    }
                    else
                    {
                        objModelStudentFee.FeeDate = DateTime.Now;
                    }

                    objModelStudentFee.AdmissionFee = DALUtility.isNumericDBNULL(studentFeeRow["AdmissionFee"].ToString());
                    objModelStudentFee.Admission = DALUtility.isNumericDBNULL(studentFeeRow["Admission"].ToString());
                    objModelStudentFee.ReAdmissionFee = DALUtility.isNumericDBNULL(studentFeeRow["ReAdmissionFee"].ToString());
                    objModelStudentFee.RegistrationFee = DALUtility.isNumericDBNULL(studentFeeRow["RegistrationFee"].ToString());
                    objModelStudentFee.MonthlyFee = DALUtility.isNumericDBNULL(studentFeeRow["MonthlyFee"].ToString());
                    objModelStudentFee.ComputerFee = DALUtility.isNumericDBNULL(studentFeeRow["ComputerFee"].ToString());
                    objModelStudentFee.Fine = DALUtility.isNumericDBNULL(studentFeeRow["Fine"].ToString());
                    objModelStudentFee.ExamFee = DALUtility.isNumericDBNULL(studentFeeRow["ExamFee"].ToString());
                    objModelStudentFee.GeneratorFee = DALUtility.isNumericDBNULL(studentFeeRow["GeneratorFee"].ToString());
                    objModelStudentFee.FeeMonth = Convert.ToString(studentFeeRow["FeeMonth"]);
                    objModelStudentFee.Total = DALUtility.isNumericDBNULL(studentFeeRow["Total"].ToString());
                    objModelStudentFee.FeePaidStatus = Convert.ToInt32(studentFeeRow["FeePaidStatus"]);

                    objlstModelStudentFee.Add(objModelStudentFee);
                }
            }
            return objlstModelStudentFee;
        }

        public int StudentBulkMonthlyFeeInsertion(decimal StudentID)
        {
            ModelStudent objModelStudent = new ModelStudent();
            DataTable tblStudentList = new DataTable();
            tblStudentList = DALCommon.GetDataByStoredProcedure("ABC");
            if (tblStudentList.Rows.Count > 0)
            {

            }
            return 0;
        }

        //for bulk Fee Insertion

        public int InsertBulkStudentMonthlyFee(ModelStudentFee objModelStudentFee, string SearchCriteria, int FeeMonth)
        {
            ModelStudent objModelStudent = new ModelStudent();
            DataTable tblStudents = new DataTable();
            int _result = 0;
            decimal _UserAccountId = Convert.ToDecimal(HttpContext.Current.Session[DAL.DALVariables.UserAccountId].ToString());
            decimal _SchoolAccountId = Convert.ToDecimal(HttpContext.Current.Session[DAL.DALVariables.SchoolAccountId].ToString());

            SqlParameter[] param = new SqlParameter[2];

            param[0] = new SqlParameter("@SearchCriteria", SearchCriteria);
            param[1] = new SqlParameter("@SchoolAccountId", _SchoolAccountId);

            tblStudents = DALCommon.GetDataUsingDataTable("[sp_Admin_GetStudentListForBulkFeeInsertion]", param);

            BulkFeeInsertionTransaction objBulkFeeInsertionTransaction = new BulkFeeInsertionTransaction();
            if (tblStudents.Rows.Count > 0)
            {

                _result = objBulkFeeInsertionTransaction.InsertStudentMonthlyFeeInsertTransaction(tblStudents, objModelStudentFee, FeeMonth, _UserAccountId);
            }
            return _result;
        }

        //BankChallan Student Info with Nested Monthly Unpaid student list

        public List<ModelStudent> GetUnPaidFeeStudentListReport(string SearchCriteria, decimal SchoolAccountId)
        {
            List<ModelStudent> lstModelStudent = new List<ModelStudent>();
            DataTable tblStudentList = new DataTable();
            SqlParameter[] param = new SqlParameter[2];

            param[0] = new SqlParameter("@SearchCriteria", String.IsNullOrEmpty(SearchCriteria) ? "" : SearchCriteria);
            param[1] = new SqlParameter("@SchoolAccountId", SchoolAccountId);

            tblStudentList = DALCommon.GetDataUsingDataTable("[sp_Admin_GetFeeUnPainStudentsBySchoolId]", param);
            if (tblStudentList.Rows.Count > 0)
            {
                foreach (DataRow aStudent in tblStudentList.Rows)
                {
                    //StudentId,ComputerCode, RegNo, StudentName, FatherName,DateOfBirth,Status,ClassName,SectionName
                    ModelStudent objModelStudent = new ModelStudent();
                    objModelStudent.StudentId = Convert.ToInt32(aStudent["StudentId"]);
                    objModelStudent.ComputerCode = Convert.ToInt32(aStudent["ComputerCode"]);
                    objModelStudent.RegNo = Convert.ToInt32(aStudent["RegNo"]);
                    objModelStudent.StudentName = Convert.ToString(aStudent["StudentName"]);
                    objModelStudent.FatherName = Convert.ToString(aStudent["FatherName"]);
                    objModelStudent.DateOfBirth = Convert.ToDateTime(aStudent["DateOfBirth"]);
                    objModelStudent.Status = Convert.ToInt32(aStudent["Status"]);
                    objModelStudent.ClassNameText = Convert.ToString(aStudent["ClassName"]);
                    objModelStudent.SectionNameText = Convert.ToString(aStudent["SectionName"]);

                   // objModelStudent.ModelStudentFee = GetUnPaidFeeListByStudentId(objModelStudent.StudentId);
                    lstModelStudent.Add(objModelStudent);
                }
            }
            return lstModelStudent;
        }

        public List<ModelStudentFee> GetUnPaidFeeListByStudentId(string SearchCriteria, decimal SchoolAccountId)
        {
            List<ModelStudentFee> lstModelStudentFee = new List<ModelStudentFee>();

            DataTable tblUnPaidFee = new DataTable();
            SqlParameter[] param = new SqlParameter[2];

            param[0] = new SqlParameter("@SearchCriteria", String.IsNullOrEmpty(SearchCriteria) ? "" : SearchCriteria);
            param[1] = new SqlParameter("@SchoolAccountId", SchoolAccountId);

            tblUnPaidFee = DALCommon.GetDataUsingDataTable("[sp_Admin_GetUnPaidFeeInfoByStudentId]", param);
            if (tblUnPaidFee.Rows.Count > 0)
            {
                foreach (DataRow aStudent in tblUnPaidFee.Rows)
                {
                    //StudentId, AdmissionFee, Admission, ReAdmissionFee, RegistrationFee, 
                    //MonthlyFee, ComputerFee, Fine, ExamFee, GeneratorFee, FeeMonth, ISNULL(Total,0)Total, FeePaidStatus
                    ModelStudentFee objModelStudentFee = new ModelStudentFee();
                    objModelStudentFee.StudentId = Convert.ToInt32(aStudent["StudentId"]);
                    objModelStudentFee.AdmissionFee = Convert.ToInt32(aStudent["AdmissionFee"]);
                    objModelStudentFee.Admission = Convert.ToInt32(aStudent["Admission"]);
                    objModelStudentFee.ReAdmissionFee = Convert.ToInt32(aStudent["ReAdmissionFee"]);
                    objModelStudentFee.RegistrationFee = Convert.ToInt32(aStudent["RegistrationFee"]);
                    objModelStudentFee.MonthlyFee = Convert.ToInt32(aStudent["MonthlyFee"]);
                    objModelStudentFee.ComputerFee = Convert.ToInt32(aStudent["ComputerFee"]);
                    objModelStudentFee.Fine = Convert.ToInt32(aStudent["Fine"]);
                    objModelStudentFee.ExamFee = Convert.ToInt32(aStudent["ExamFee"]);
                    objModelStudentFee.GeneratorFee = Convert.ToInt32(aStudent["GeneratorFee"]);
                    objModelStudentFee.FeeMonth = Convert.ToString(aStudent["FeeMonth"]);
                    objModelStudentFee.Total = Convert.ToInt32(aStudent["Total"]);
                    objModelStudentFee.FeePaidStatus = Convert.ToInt32(aStudent["FeePaidStatus"]);
                    
                    lstModelStudentFee.Add(objModelStudentFee);
                }

            }
            return lstModelStudentFee;

        }

        //Generate Student Fee Voucher
        public int GenerateMonthlyFeeVoucher(string SearchCriteria, decimal SchoolAccountId, string VoucherDueDate,decimal AddedBy)
        {
            DataTable tblFeeUnPaidStudentList = new DataTable();
            SqlParameter[] param = new SqlParameter[2];
            int _transResult = 0;
            param[0] = new SqlParameter("@SearchCriteria", String.IsNullOrEmpty(SearchCriteria) ? "" : SearchCriteria);
            param[1] = new SqlParameter("@SchoolAccountId", SchoolAccountId);

            tblFeeUnPaidStudentList = DALCommon.GetDataUsingDataTable("[sp_Admin_GetFeeUnPaidStudentsBySchoolId]", param);
            if (tblFeeUnPaidStudentList.Rows.Count > 0)
            {
                StudentFeeVoucherTransaction objStudentFeeVoucherTransaction = new StudentFeeVoucherTransaction();
                _transResult = objStudentFeeVoucherTransaction.InsertUnPaidMonthlyFeeVoucherTransaction(VoucherDueDate, tblFeeUnPaidStudentList, AddedBy);
            }
            return _transResult;
        }

        //BankChallan Student Info with Nested Monthly Unpaid student list With Voucher

        public List<ModelStudentBankChallan> GetUnPaidFeeStudentListWithVoucher(string SearchCriteria, string VoucherDueDate, double SchoolAccountId)
        {
            List<ModelStudentBankChallan> lstModelStudentBankChallan = new List<ModelStudentBankChallan>();
            DataTable tblStudentList = new DataTable();
            SqlParameter[] param = new SqlParameter[3];
           
            param[0] = new SqlParameter("@SearchCriteria", String.IsNullOrEmpty(SearchCriteria) ? "" : SearchCriteria);
            param[1] = new SqlParameter("@VoucherDueDate", VoucherDueDate);
            param[2] = new SqlParameter("@SchoolAccountId", SchoolAccountId);

            tblStudentList = DALCommon.GetDataUsingDataTable("[sp_Admin_GetFeeUnPaidStudentsByVoucherDueDate]", param);
            if (tblStudentList.Rows.Count > 0)
            {
                foreach (DataRow aStudent in tblStudentList.Rows)
                {
                    //StudentId,ComputerCode, RegNo, StudentName, FatherName,DateOfBirth,Status,ClassName,SectionName
                    ModelStudentBankChallan objModelStudentBankChallan = new ModelStudentBankChallan();
                    objModelStudentBankChallan.VoucherId = Convert.ToDouble(aStudent["VoucherId"]);
                    objModelStudentBankChallan.StudentId = Convert.ToDouble(aStudent["StudentId"]);
                    objModelStudentBankChallan.ComputerCode = Convert.ToInt32(aStudent["ComputerCode"]);
                    objModelStudentBankChallan.RegNo = Convert.ToInt32(aStudent["RegNo"]);
                    objModelStudentBankChallan.StudentName = Convert.ToString(aStudent["StudentName"]);
                    objModelStudentBankChallan.FatherName = Convert.ToString(aStudent["FatherName"]);
                    objModelStudentBankChallan.DateOfBirth = Convert.ToDateTime(aStudent["DateOfBirth"]);
                    objModelStudentBankChallan.Status = Convert.ToInt32(aStudent["Status"]);
                    objModelStudentBankChallan.ClassNameText = Convert.ToString(aStudent["ClassName"]);
                    objModelStudentBankChallan.SectionNameText = Convert.ToString(aStudent["SectionName"]);
                    objModelStudentBankChallan.VoucherDueDate = Convert.ToDateTime(aStudent["VoucherDueDate"]);
                    // objModelStudent.ModelStudentFee = GetUnPaidFeeListByStudentId(objModelStudent.StudentId);
                    lstModelStudentBankChallan.Add(objModelStudentBankChallan);
                }
            }
            return lstModelStudentBankChallan;
        }

        public List<ModelStudentFee> GetUnPaidFeeMonthsByDuaDateVoucher(string VoucherDueDate, double SchoolAccountId)
        {
            List<ModelStudentFee> lstModelStudentFee = new List<ModelStudentFee>();

            DataTable tblUnPaidFee = new DataTable();
            SqlParameter[] param = new SqlParameter[2];
            //param[0] = new SqlParameter("@SearchCriteria", String.IsNullOrEmpty(SearchCriteria) ? "" : SearchCriteria);
            param[0] = new SqlParameter("@VoucherDueDate", VoucherDueDate);
            param[1] = new SqlParameter("@SchoolAccountId", SchoolAccountId);

            tblUnPaidFee = DALCommon.GetDataUsingDataTable("[sp_Admin_GetUnPaidFeeMonthsByDuaDateVoucher]", param);
            if (tblUnPaidFee.Rows.Count > 0)
            {
                foreach (DataRow aStudent in tblUnPaidFee.Rows)
                {
                    //VoucherId,FeeId,StudentId, AdmissionFee, Admission, ReAdmissionFee, RegistrationFee, 
                    //MonthlyFee, ComputerFee, Fine, ExamFee, GeneratorFee, FeeMonth,FeeMonthText, ISNULL(Total,0)Total, FeePaidStatus
                    ModelStudentFee objModelStudentFee = new ModelStudentFee();

                    objModelStudentFee.VoucherId = Convert.ToDouble(aStudent["VoucherId"]);
                    objModelStudentFee.FeeId = Convert.ToDouble(aStudent["FeeId"]);
                    objModelStudentFee.StudentId = Convert.ToDouble(aStudent["StudentId"]);
                    objModelStudentFee.AdmissionFee = Convert.ToInt32(aStudent["AdmissionFee"]);
                    objModelStudentFee.Admission = Convert.ToInt32(aStudent["Admission"]);
                    objModelStudentFee.ReAdmissionFee = Convert.ToInt32(aStudent["ReAdmissionFee"]);
                    objModelStudentFee.RegistrationFee = Convert.ToInt32(aStudent["RegistrationFee"]);
                    objModelStudentFee.MonthlyFee = Convert.ToInt32(aStudent["MonthlyFee"]);
                    objModelStudentFee.ComputerFee = Convert.ToInt32(aStudent["ComputerFee"]);
                    objModelStudentFee.Fine = Convert.ToInt32(aStudent["Fine"]);
                    objModelStudentFee.ExamFee = Convert.ToInt32(aStudent["ExamFee"]);
                    objModelStudentFee.GeneratorFee = Convert.ToInt32(aStudent["GeneratorFee"]);
                    objModelStudentFee.FeeMonth = Convert.ToString(aStudent["FeeMonth"]);
                    objModelStudentFee.FeeMonthText = Convert.ToString(aStudent["FeeMonthText"]);
                    objModelStudentFee.Total = Convert.ToInt32(aStudent["Total"]);
                    objModelStudentFee.FeePaidStatus = Convert.ToInt32(aStudent["FeePaidStatus"]);

                    lstModelStudentFee.Add(objModelStudentFee);
                }

            }
            return lstModelStudentFee;

        }

        public int FeeVoucherFeePaidEntry(string FeeVoucherFeePaidDate, double FeeVoucherId,double AddedBy)
        {
            DataTable tblFeeData = new DataTable();
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@VoucherId", FeeVoucherId);

            tblFeeData = DALCommon.GetDataUsingDataTable("[sp_Admin_GetFeeIdDataByVoucherID]", param);

            StudentFeeVoucherPaidMark objStudentFeeVoucherPaidMark = new StudentFeeVoucherPaidMark();
            return objStudentFeeVoucherPaidMark.InsertFeeVoucherFeePaidTransaction(FeeVoucherFeePaidDate, FeeVoucherId, tblFeeData, AddedBy);
        }
    }
}