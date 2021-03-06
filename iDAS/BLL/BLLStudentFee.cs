﻿using iDAS.Controllers;
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

        public DataTable getFeeMonthsByYearDP()
        {

            DataTable adataSetTable = new DataTable();

            adataSetTable = DALCommon.GetDataByStoredProcedure("[sp_Admin_GetFeeMothByYear]");
            return adataSetTable;


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




        public  decimal InsertStudentMonthlyAdvanceFeeByStudentId(decimal StudentId, int Feemonth, int AdmissionFee, int ReAdmissionFee,
         int RegistrationFee,  int ComputerFee, int Fine, int ExamFee, int GenatorFee, string FeeDate,int SchoolAccountId)
          {
            try
            {
                SqlParameter[] paramStudentFee = new SqlParameter[11];
                paramStudentFee[0] = new SqlParameter("@StudentId", Convert.ToDecimal(StudentId));
                paramStudentFee[1] = new SqlParameter("@FeeMonth",Feemonth);
                paramStudentFee[2] = new SqlParameter("@AdmissionFee", AdmissionFee);
               
                paramStudentFee[3] = new SqlParameter("@ReAdmissionFee", ReAdmissionFee);
                paramStudentFee[4] = new SqlParameter("@RegistrationFee", RegistrationFee);
                paramStudentFee[5] = new SqlParameter("@ComputerFee", ComputerFee);
                paramStudentFee[6] = new SqlParameter("@Fine", Fine);
                paramStudentFee[7] = new SqlParameter("@ExamFee", ExamFee);
                paramStudentFee[8] = new SqlParameter("@GeneratorFee", GenatorFee);
                paramStudentFee[9] = new SqlParameter("@FeeDate",Convert.ToString(FeeDate));
                paramStudentFee[10] = new SqlParameter("@SchoolAccountId", SchoolAccountId);
                return DALCommon.ExecuteNonQueryReturnIdentity("[sp_Admin_InsertStudentMonthlyFeeByStudentId]", paramStudentFee);

            }
            catch (Exception)
            {

                throw;
            }
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

<<<<<<< HEAD
        public int StudentBulkMonthlyFeeInsertion(decimal StudentID)
        {
            ModelStudent objModelStudent = new ModelStudent();
            DataTable tblStudentList= new DataTable();
            tblStudentList = DALCommon.GetDataByStoredProcedure("ABC");
            if (tblStudentList.Rows.Count > 0)
            {
                
            }
            return 0;
        }

=======
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
>>>>>>> c466499924de2141b8ce9fba88b9349e6d130ab6
    }
}