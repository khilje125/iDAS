using iDAS.DAL;
using iDAS.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

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

    }
}