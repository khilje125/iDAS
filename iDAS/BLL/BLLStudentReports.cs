using iDAS.DAL;
using iDAS.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace iDAS.BLL
{
    public class BLLStudentReports
    {
        public List<ModelBankFeeChallanReport> GetStudentListReport()
        {
            List<ModelBankFeeChallanReport> lstModelBankFeeChallanReport = new List<ModelBankFeeChallanReport>();
            DataTable tblStudentList = new DataTable();
            SqlParameter[] param = new SqlParameter[2];

            //param[0] = new SqlParameter("@SearchCriteria", String.IsNullOrEmpty(SearchCriteria) ? "" : SearchCriteria);
            //param[1] = new SqlParameter("@SchoolAccountId", SchoolAccountId);

            tblStudentList = DALCommon.GetDataByStoredProcedure("[sp_Admin_GetStudentBankChallanReportBySchoolId]");
            if (tblStudentList.Rows.Count > 0)
            {
                foreach (DataRow aStudent in tblStudentList.Rows)
                {
                    ModelBankFeeChallanReport objModelBankFeeChallanReport = new ModelBankFeeChallanReport();
                    objModelBankFeeChallanReport.FeeId = Convert.ToInt32(aStudent["StudentId"]);
                    objModelBankFeeChallanReport.StudentId = Convert.ToInt32(aStudent["ComputerCode"]);
                    objModelBankFeeChallanReport.StudentName = Convert.ToString(aStudent["StudentName"]);
                    objModelBankFeeChallanReport.ClassName = Convert.ToString(aStudent["ClassName"]);
                    objModelBankFeeChallanReport.SectionName = Convert.ToString(aStudent["SectionName"]);
                    objModelBankFeeChallanReport.ComputerCode = Convert.ToInt32(aStudent["ComputerCode"]);
                    objModelBankFeeChallanReport.AdmissionFee = Convert.ToInt32(aStudent["AdmissionFee"]);
                    objModelBankFeeChallanReport.Admission = Convert.ToInt32(aStudent["Admission"]);
                    objModelBankFeeChallanReport.ReAdmissionFee = Convert.ToInt32(aStudent["ReAdmissionFee"]);
                    objModelBankFeeChallanReport.RegistrationFee = Convert.ToInt32(aStudent["RegistrationFee"]);
                    objModelBankFeeChallanReport.MonthlyFee = Convert.ToInt32(aStudent["MonthlyFee"]);
                    objModelBankFeeChallanReport.ComputerFee = Convert.ToInt32(aStudent["ComputerFee"]);
                    objModelBankFeeChallanReport.Fine = Convert.ToInt32(aStudent["Fine"]);
                    objModelBankFeeChallanReport.ExamFee = Convert.ToInt32(aStudent["ExamFee"]);
                    objModelBankFeeChallanReport.GeneratorFee = Convert.ToInt32(aStudent["GeneratorFee"]);
                    objModelBankFeeChallanReport.Total = Convert.ToInt32(aStudent["Total"]);
                    objModelBankFeeChallanReport.FeeMonth = Convert.ToString(aStudent["FeeMonth"]);

                    lstModelBankFeeChallanReport.Add(objModelBankFeeChallanReport);
                }
            }
            return lstModelBankFeeChallanReport;
        }


        public List<ModelStudentDynamicReport> GetStudentListForDynamicParameter(bool isStdId,bool isCompCode,bool isStudentName, bool isDOB)
        {
            List<ModelStudentDynamicReport> lstModelStudentDynamicReport = new List<ModelStudentDynamicReport>();
            DataTable tblStudentList = new DataTable();
            StringBuilder SelectedColumns = new StringBuilder();
            string strSelectedColumns = String.Empty;

            if (isStdId)
            {
                SelectedColumns.Append("StudentId,"); 
            }
            if (isCompCode)
            {
                SelectedColumns.Append("ComputerCode,");
            }
            if (isStudentName)
            {
                SelectedColumns.Append("StudentName,");
            }
            if (isDOB)
            {
                SelectedColumns.Append("DateOfBirth,");
            }

            if ((!string.IsNullOrEmpty(SelectedColumns.ToString())))
            {
                //Set Filter Expression
                strSelectedColumns = SelectedColumns.ToString().Remove(SelectedColumns.ToString().Length - 1, 1);

                //User Log
                //DALUtility.InsertUserLog("Search Manage Short Code. Search Criteria - " + searchCriteria, "Student/Search", Convert.ToInt32(Session[DALVariables.UserAccountId].ToString()));
            }

            string SearchCriteria = "";

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@SearchCriteria", String.IsNullOrEmpty(SearchCriteria) ? "" : SearchCriteria);
            param[1] = new SqlParameter("@SchoolAccountId", 40001);
            param[2] = new SqlParameter("@SColumns", strSelectedColumns);

            tblStudentList = DALCommon.GetDataUsingDataTable("[sp_Admin_GetStudentListForDynamicParameter]", param);
            if (tblStudentList.Rows.Count > 0)
            {
                bool isStudentIdColumn = tblStudentList.Columns.Contains("StudentId");
                bool isStudentNameColumn = tblStudentList.Columns.Contains("StudentName");
                bool isClassNameColumn = tblStudentList.Columns.Contains("ClassName");
                bool isClassDateOfBirthColumn = tblStudentList.Columns.Contains("DateOfBirth");

                foreach (DataRow aStudent in tblStudentList.Rows)
                {
                    ModelStudentDynamicReport objModelStudentDynamicReport = new ModelStudentDynamicReport();
                    if (isStudentIdColumn)
                    {
                        if (aStudent["StudentId"] != null)
                        {
                            objModelStudentDynamicReport.StudentId = Convert.ToInt32(aStudent["StudentId"].ToString());
                        }
                    }
                    if (isStudentNameColumn)
                    {
                        if (aStudent["StudentName"] != null)
                        {
                            objModelStudentDynamicReport.StudentName = aStudent["StudentName"].ToString();
                        }
                    }
                    if (isClassNameColumn)
                    {
                        if (aStudent["ClassName"] != null)
                        {
                            objModelStudentDynamicReport.ClassNameText = aStudent["ClassName"].ToString();
                        }
                    }
                    if (isClassDateOfBirthColumn)
                    {
                        if (aStudent["DateOfBirth"] != null)
                        {
                            objModelStudentDynamicReport.DateOfBirth = Convert.ToDateTime(aStudent["DateOfBirth"]);
                        }
                    }
                    
                    lstModelStudentDynamicReport.Add(objModelStudentDynamicReport);
                }
            }
            return lstModelStudentDynamicReport;
        }

        public DataTable GetStudentListForDynamicParameterTable(bool isStdId, bool isCompCode, bool isStudentName, bool isDOB)
        {
            DataTable tblStudentList = new DataTable();
            StringBuilder SelectedColumns = new StringBuilder();
            string strSelectedColumns = String.Empty;

            if (isStdId)
            {
                SelectedColumns.Append("StudentId,");
            }
            if (isCompCode)
            {
                SelectedColumns.Append("ComputerCode,");
            }
            if (isStudentName)
            {
                SelectedColumns.Append("StudentName,");
            }
            if (isDOB)
            {
                SelectedColumns.Append("DateOfBirth,");
            }

            if ((!string.IsNullOrEmpty(SelectedColumns.ToString())))
            {
                //Set Filter Expression
                strSelectedColumns = SelectedColumns.ToString().Remove(SelectedColumns.ToString().Length - 1, 1);

                //User Log
                //DALUtility.InsertUserLog("Search Manage Short Code. Search Criteria - " + searchCriteria, "Student/Search", Convert.ToInt32(Session[DALVariables.UserAccountId].ToString()));
            }

            string SearchCriteria = "";

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@SearchCriteria", String.IsNullOrEmpty(SearchCriteria) ? "" : SearchCriteria);
            param[1] = new SqlParameter("@SchoolAccountId", 40001);
            param[2] = new SqlParameter("@SColumns", strSelectedColumns);

            return DALCommon.GetDataUsingDataTable("[sp_Admin_GetStudentListForDynamicParameter]", param);
        }

        private DataTable MakeObjectDataTable(bool isStdId, bool isCompCode, bool isStudentName, bool isDOB, List<ModelStudentDynamicReport> lstModelStudentDynamicReport)
        {
            DataTable table = new DataTable();
            if (isStdId)
            {
                table.Columns.Add("StudentId", typeof(int));
                
            }
            if (isStudentName)
            {
                table.Columns.Add("StudentName", typeof(string));                
            }
            if (isCompCode)
            {
                table.Columns.Add("ClassName", typeof(string));
            }
            if (isDOB)
            {
                table.Columns.Add("DateOfBirth", typeof(DateTime));                
            }

            if (lstModelStudentDynamicReport.Count > 0)
            {
                foreach (ModelStudentDynamicReport item in lstModelStudentDynamicReport)
                {
                    // Here we add five DataRows.
            table.Rows.Add(25, "Indocin", "David", DateTime.Now);
            table.Rows.Add(50, "Enebrel", "Sam", DateTime.Now);
            table.Rows.Add(10, "Hydralazine", "Christoff", DateTime.Now);
            table.Rows.Add(21, "Combivent", "Janet", DateTime.Now);
            table.Rows.Add(100, "Dilantin", "Melanie", DateTime.Now);
                }
            }
            // Here we add five DataRows.
            table.Rows.Add(25, "Indocin", "David", DateTime.Now);
            table.Rows.Add(50, "Enebrel", "Sam", DateTime.Now);
            table.Rows.Add(10, "Hydralazine", "Christoff", DateTime.Now);
            table.Rows.Add(21, "Combivent", "Janet", DateTime.Now);
            table.Rows.Add(100, "Dilantin", "Melanie", DateTime.Now);
            return table;
        
        }

        // 
        public List<SelectListItem> GetReportListDropdown(double SchoolAccountId,int ReportCategory)
        {
            List<SelectListItem> lstReports = new List<SelectListItem>();
            lstReports.Add(new SelectListItem { Text = "--- Select Report Type --- ", Value = "0" });

            DataTable tblReportsList = new DataTable();
            SqlParameter[] param = new SqlParameter[2];

            param[0] = new SqlParameter("@SchoolAccountId", SchoolAccountId);
            param[1] = new SqlParameter("@ReportCategory", ReportCategory);

            tblReportsList = DALCommon.GetDataUsingDataTable("[sp_GetReportInfoBySchoolId]", param);
            if (tblReportsList.Rows.Count > 0)
            {
                foreach (DataRow aClass in tblReportsList.Rows)
                {
                    lstReports.Add(new SelectListItem { Text = Convert.ToString(aClass["ReportName"]), Value = Convert.ToString(aClass["ReportId"]) });
                }
            }
            return lstReports;
        }


        public List<ModelStudent> GetStudentListForReport(string SearchCriteria, int CampusTypeId, decimal SchoolAccountId)
        {
            List<ModelStudent> lstModelStudent = new List<ModelStudent>();
            DataTable tblStudentList = new DataTable();
            SqlParameter[] param = new SqlParameter[3];

            param[0] = new SqlParameter("@SearchCriteria", String.IsNullOrEmpty(SearchCriteria) ? "" : SearchCriteria);
            param[1] = new SqlParameter("@CampusTypeId", CampusTypeId);
            param[2] = new SqlParameter("@SchoolAccountId", SchoolAccountId);

            tblStudentList = DALCommon.GetDataUsingDataTable("[sp_Admin_GetStudentsForReportBySchoolId]", param);
            if (tblStudentList.Rows.Count > 0)
            {
                foreach (DataRow aStudent in tblStudentList.Rows)
                {
                    //StudentId,ComputerCode, RegNo, StudentName, FatherName,DateOfBirth,Status,
                    //ClassId,SectionId,MonthlyFee,Profession,P,Reference,Gender,DateOfAdmission,Address,HomeNumber,
                    //OfficeNumber,ClassName,SectionName
                    ModelStudent objModelStudent = new ModelStudent();
                    objModelStudent.StudentId = Convert.ToInt32(aStudent["StudentId"]);
                    objModelStudent.ComputerCode = Convert.ToInt32(aStudent["ComputerCode"]);
                    objModelStudent.RegNo = Convert.ToInt32(aStudent["RegNo"]);
                    objModelStudent.StudentName = Convert.ToString(aStudent["StudentName"]);
                    objModelStudent.FatherName = Convert.ToString(aStudent["FatherName"]);
                    objModelStudent.DateOfBirth = Convert.ToDateTime(aStudent["DateOfBirth"]);
                    objModelStudent.Status = Convert.ToInt32(aStudent["Status"]);
                    objModelStudent.MonthlyFee = Convert.ToInt32(aStudent["MonthlyFee"]);
                    objModelStudent.Profession = Convert.ToString(aStudent["Profession"]);
                    objModelStudent.Reference = Convert.ToString(aStudent["Reference"]);
                    objModelStudent.DateOfAdmission = Convert.ToDateTime(aStudent["DateOfAdmission"]);
                    objModelStudent.Address = Convert.ToString(aStudent["Address"]);
                    objModelStudent.HomeNumber = Convert.ToString(aStudent["HomeNumber"]);
                    objModelStudent.OfficeNumber = Convert.ToString(aStudent["OfficeNumber"]);
                    objModelStudent.MoblieNumber = Convert.ToString(aStudent["MoblieNumber"]);
                    objModelStudent.ClassNameText = Convert.ToString(aStudent["ClassName"]);
                    objModelStudent.SectionNameText = Convert.ToString(aStudent["SectionName"]);

                    // objModelStudent.ModelStudentFee = GetUnPaidFeeListByStudentId(objModelStudent.StudentId);
                    lstModelStudent.Add(objModelStudent);
                }
            }
            return lstModelStudent;
        }

        public List<ModelBankFeeChallanReport> GetFeeFollowUpStudentListForReport(string SearchCriteria, int UnPaidFeeMonths, int CampusTypeId, decimal SchoolAccountId)
        {
            List<ModelBankFeeChallanReport> lstModelBankFeeChallanReport = new List<ModelBankFeeChallanReport>();
            DataTable tblStudentList = new DataTable();
            SqlParameter[] param = new SqlParameter[4];

            param[0] = new SqlParameter("@SearchCriteria", String.IsNullOrEmpty(SearchCriteria) ? "" : SearchCriteria);
            param[1] = new SqlParameter("@FeeUnpaidMonth", UnPaidFeeMonths);
            param[2] = new SqlParameter("@CampusTypeId", CampusTypeId);
            param[3] = new SqlParameter("@SchoolAccountId", SchoolAccountId);

            tblStudentList = DALCommon.GetDataUsingDataTable("[sp_Admin_GetFeeFollowUpStudentsForReportBySchoolId]", param);
            if (tblStudentList.Rows.Count > 0)
            {
                foreach (DataRow aStudent in tblStudentList.Rows)
                {
                    //FeeId,SerialNo, AdmissionFee, Admission, ReAdmissionFee,RegistrationFee,MonthlyFee,
                    //ComputerFee,Fine,ExamFee,GeneratorFee,Total,FeePaidStatus,StudentId,ComputerCode,
                    //RegNo,StudentName,FatherName,ClassName,SectionName,FeeMonth,FeeMonthId
                    ModelBankFeeChallanReport objModelBankFeeChallanReport = new ModelBankFeeChallanReport();
                    objModelBankFeeChallanReport.FeeId = Convert.ToDouble(aStudent["FeeId"]);
                    objModelBankFeeChallanReport.StudentId = Convert.ToDouble(aStudent["StudentId"]);
                    objModelBankFeeChallanReport.StudentName = Convert.ToString(aStudent["StudentName"]);
                    objModelBankFeeChallanReport.FatherName = Convert.ToString(aStudent["FatherName"]);
                    objModelBankFeeChallanReport.ClassName = Convert.ToString(aStudent["ClassName"]);
                    objModelBankFeeChallanReport.SectionName = Convert.ToString(aStudent["SectionName"]);
                    objModelBankFeeChallanReport.ComputerCode = Convert.ToInt32(aStudent["ComputerCode"]);
                    objModelBankFeeChallanReport.AdmissionFee = Convert.ToInt32(aStudent["AdmissionFee"]);
                    objModelBankFeeChallanReport.Admission = Convert.ToInt32(aStudent["Admission"]);
                    objModelBankFeeChallanReport.ReAdmissionFee = Convert.ToInt32(aStudent["ReAdmissionFee"]);
                    objModelBankFeeChallanReport.RegistrationFee = Convert.ToInt32(aStudent["RegistrationFee"]);
                    objModelBankFeeChallanReport.MonthlyFee = Convert.ToInt32(aStudent["MonthlyFee"]);
                    objModelBankFeeChallanReport.ComputerFee = Convert.ToInt32(aStudent["ComputerFee"]);
                    objModelBankFeeChallanReport.Fine = Convert.ToInt32(aStudent["Fine"]);
                    objModelBankFeeChallanReport.ExamFee = Convert.ToInt32(aStudent["ExamFee"]);
                    objModelBankFeeChallanReport.GeneratorFee = Convert.ToInt32(aStudent["GeneratorFee"]);
                    objModelBankFeeChallanReport.Total = Convert.ToInt32(aStudent["Total"]);
                    objModelBankFeeChallanReport.FeeMonth = Convert.ToString(aStudent["FeeMonth"]);
                    objModelBankFeeChallanReport.FeeMonthId = Convert.ToInt32(aStudent["FeeMonthId"]);

                    // objModelStudent.ModelStudentFee = GetUnPaidFeeListByStudentId(objModelStudent.StudentId);
                    lstModelBankFeeChallanReport.Add(objModelBankFeeChallanReport);
                }
            }
            return lstModelBankFeeChallanReport;
        }

        public List<ModelStudentFeeFollowUpMonthCountReport> GetFeeFollowUpMonthCountForReportBySchoolId(string SearchCriteria, int UnPaidFeeMonths, int CampusTypeId, decimal SchoolAccountId)
        {
            List<ModelStudentFeeFollowUpMonthCountReport> lstFollowUpMonthCountReport = new List<ModelStudentFeeFollowUpMonthCountReport>();
            DataTable tblStudentList = new DataTable();
            SqlParameter[] param = new SqlParameter[4];

            param[0] = new SqlParameter("@SearchCriteria", String.IsNullOrEmpty(SearchCriteria) ? "" : SearchCriteria);
            param[1] = new SqlParameter("@FeeUnpaidMonth", UnPaidFeeMonths);
            param[2] = new SqlParameter("@CampusTypeId", CampusTypeId);
            param[3] = new SqlParameter("@SchoolAccountId", SchoolAccountId);

            tblStudentList = DALCommon.GetDataUsingDataTable("[sp_Admin_GetFeeFollowUpMonthCountForReportBySchoolId]", param);
            if (tblStudentList.Rows.Count > 0)
            {
                foreach (DataRow aStudent in tblStudentList.Rows)
                {
                    //UnPaidMonthsCount,StudentId,ComputerCode,StudentName,FatherName,SUM(Total)Total,ClassId,SectionId,ClassName,SectionName,HomeNumber,OfficeNumber,MoblieNumber
                    ModelStudentFeeFollowUpMonthCountReport objlstFollowUpMonthCountReport = new ModelStudentFeeFollowUpMonthCountReport();
                    objlstFollowUpMonthCountReport.UnPaidMonthCount = Convert.ToInt32(aStudent["UnPaidMonthsCount"]);
                    objlstFollowUpMonthCountReport.StudentId = Convert.ToDouble(aStudent["StudentId"]);
                    objlstFollowUpMonthCountReport.StudentName = Convert.ToString(aStudent["StudentName"]);
                    objlstFollowUpMonthCountReport.FatherName = Convert.ToString(aStudent["FatherName"]);
                    objlstFollowUpMonthCountReport.ClassName = Convert.ToString(aStudent["ClassName"]);
                    objlstFollowUpMonthCountReport.SectionName = Convert.ToString(aStudent["SectionName"]);
                    objlstFollowUpMonthCountReport.ComputerCode = Convert.ToInt32(aStudent["ComputerCode"]);
                    objlstFollowUpMonthCountReport.Total = Convert.ToInt32(aStudent["Total"]);
                    objlstFollowUpMonthCountReport.ClassId = Convert.ToInt32(aStudent["ClassId"]);
                    objlstFollowUpMonthCountReport.SectionId = Convert.ToInt32(aStudent["SectionId"]);
                    objlstFollowUpMonthCountReport.HomeNumber = Convert.ToString(aStudent["HomeNumber"]);
                    objlstFollowUpMonthCountReport.OfficeNumber = Convert.ToString(aStudent["OfficeNumber"]);
                    objlstFollowUpMonthCountReport.MoblieNumber = Convert.ToString(aStudent["MoblieNumber"]);
                    //objlstFollowUpMonthCountReport.OtherNumber = Convert.ToInt32(aStudent["SectionId"]);

                    lstFollowUpMonthCountReport.Add(objlstFollowUpMonthCountReport);
                }
            }
            return lstFollowUpMonthCountReport;
        }
    }
}