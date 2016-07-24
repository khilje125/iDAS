using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using iDAS.BLL;
using iDAS.DAL;
using iDAS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace iDAS.ASPXReport
{
    public partial class StudentFeeReport : System.Web.UI.Page
    {
        ReportDocument crystalReportDocument = new ReportDocument();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    if (Session[DALVariables.SchoolAccountId] == null)
                    {
                        Response.Redirect("User/Login");
                    }
                }
            }
            catch (Exception ex)
            {

                DALUtility.ErrorLog(ex.Message, "StudentReport.aspx.cs, Page_Load");
            }
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            try
            {
                crystalReportDocument = new ReportDocument();

                string strRptPath = Server.MapPath("~/") + "Reports//" + DALCommonFormData.GetReportFileNamePath(ReportTypeID);
                crystalReportDocument.Load(strRptPath);

                ModelReportInfo lstModelReportInfo = new ModelReportInfo();
                lstModelReportInfo = getStudentBankChallanStudentInfo(ReportTypeID);
                ParameterFields paramFields = new ParameterFields();
                ParameterField paramField;
                ParameterDiscreteValue paramDiscreteValue;

                if (!String.IsNullOrEmpty(lstModelReportInfo.SchoolTitleForReport))
                {
                    paramField = new ParameterField();
                    paramField.Name = "SchoolNameTitle";
                    paramDiscreteValue = new ParameterDiscreteValue();
                    paramDiscreteValue.Value = lstModelReportInfo.SchoolTitleForReport;
                    paramField.CurrentValues.Add(paramDiscreteValue);
                    //Add the paramField to paramFields
                    paramFields.Add(paramField);
                }

                if (!String.IsNullOrEmpty(lstModelReportInfo.SchoolAddressForReport))
                {
                    paramField = new ParameterField();
                    paramField.Name = "SchoolAddress";
                    paramDiscreteValue = new ParameterDiscreteValue();
                    paramDiscreteValue.Value = lstModelReportInfo.SchoolAddressForReport;
                    paramField.CurrentValues.Add(paramDiscreteValue);
                    //Add the paramField to paramFields
                    paramFields.Add(paramField);
                }

                paramField = new ParameterField();
                paramField.Name = "ReportTitle";
                paramDiscreteValue = new ParameterDiscreteValue();
                paramDiscreteValue.Value = ReportTitle;
                paramField.CurrentValues.Add(paramDiscreteValue);
                //Add the paramField to paramFields
                paramFields.Add(paramField);

                if (!IsPostBack)
                {
                    rptStudentFeeFollowUp.ParameterFieldInfo = paramFields;
                    if (ReportTypeID == 4)
                    {
                    crystalReportDocument.SetDataSource(getStudentFeeFollowUpReportData());
                    }
                    else{
                    crystalReportDocument.SetDataSource(getStudentUnpaidFeeInfoBySearch());
                    }
                    //crystalReport.SetDataSource(dsStudentClass);
                    rptStudentFeeFollowUp.Dispose();
                    rptStudentFeeFollowUp.ReportSource = crystalReportDocument;
                }
                else
                {
                    rptStudentFeeFollowUp.ParameterFieldInfo = paramFields;
                    if (ReportTypeID == 4)
                    {
                        crystalReportDocument.SetDataSource(getStudentFeeFollowUpReportData());
                    }
                    else
                    {
                        crystalReportDocument.SetDataSource(getStudentUnpaidFeeInfoBySearch());
                    }
                    //getStudentBankChallanFeeList()
                    rptStudentFeeFollowUp.Dispose();
                    rptStudentFeeFollowUp.ReportSource = crystalReportDocument;
                }
            }
            catch (Exception ex)
            {

                DALUtility.ErrorLog(ex.Message, "StudentFeeReport.aspx.cs, Page_PreInit");
            }
        }
        //Report Title
        public string ReportTitle
        {
            get
            {
                if ((Request.QueryString["Title"] != null))
                {
                    return Convert.ToString(EncryptDecrypt.Decrypt(Request.QueryString["Title"]));
                }

                return String.Empty;
            }
        }

        //Campus Type ID
        public int CampusId
        {
            get
            {
                if ((Request.QueryString["CampId"] != null))
                {
                    return Convert.ToInt32(EncryptDecrypt.Decrypt(Request.QueryString["CampId"]));
                }

                return 0;
            }
        }
        public int ReportTypeID
        {
            get
            {
                if ((Request.QueryString["Report"] != null))
                {
                    return Convert.ToInt32(EncryptDecrypt.Decrypt(Request.QueryString["Report"]));
                }

                return 0;
            }
        }

        public int UnPaidMonths
        {
            get
            {
                if ((Request.QueryString["Month"] != null))
                {
                    return Convert.ToInt32(EncryptDecrypt.Decrypt(Request.QueryString["Month"]));
                }

                return 0;
            }
        }

        public string SearchCriteriaParam
        {
            get
            {
                if ((Request.QueryString["Search"] != null))
                {
                    return Convert.ToString(EncryptDecrypt.Decrypt(Request.QueryString["Search"]));
                }

                return String.Empty;
            }
        }

        private ModelReportInfo getStudentBankChallanStudentInfo(int reportID)
        {
            ModelReportInfo lstModelReportInfo = new ModelReportInfo();
            lstModelReportInfo = DALCommonFormData.GetReportInfoModel(reportID);
            return lstModelReportInfo;
        }

        private List<ModelBankFeeChallanReport> getStudentUnpaidFeeInfoBySearch()
        {

            BLLStudentReports objBLLStudentReports = new BLLStudentReports();
            List<ModelBankFeeChallanReport> lstModelBankFeeChallanReportt = new List<ModelBankFeeChallanReport>();
            //Testing
            //string SearchCriteria = "Students.ClassId = 1 AND Students.SectionId = 1";

            lstModelBankFeeChallanReportt = objBLLStudentReports.GetFeeFollowUpStudentListForReport(SearchCriteriaParam,UnPaidMonths, CampusId, Convert.ToInt32(Session[iDAS.DAL.DALVariables.SchoolAccountId].ToString()));
            return lstModelBankFeeChallanReportt;
        }
        private List<ModelStudentFeeFollowUpMonthCountReport> getStudentFeeFollowUpReportData()
        {

            BLLStudentReports objBLLStudentReports = new BLLStudentReports();
            List<ModelStudentFeeFollowUpMonthCountReport> lstFollowUpMonthCountReport = new List<ModelStudentFeeFollowUpMonthCountReport>();
            //Testing
            //string SearchCriteria = "Students.ClassId = 1 AND Students.SectionId = 1";

            lstFollowUpMonthCountReport = objBLLStudentReports.GetFeeFollowUpMonthCountForReportBySchoolId(SearchCriteriaParam, UnPaidMonths, CampusId, Convert.ToInt32(Session[iDAS.DAL.DALVariables.SchoolAccountId].ToString()));
            return lstFollowUpMonthCountReport;
        }
    }
}