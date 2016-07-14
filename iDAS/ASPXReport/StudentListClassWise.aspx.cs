using iDAS.BLL;
using iDAS.DAL;
using iDAS.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.CrystalReports.Engine;

namespace iDAS.ASPXReport
{
    public partial class StudentListClassWise : System.Web.UI.Page
    {
        ReportDocument crystalReportDocument = new ReportDocument();

        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            try
            {
                crystalReportDocument = new ReportDocument();
                //crystalReport.SummaryInfo.ReportTitle = "StudentCClassInfo-" + DateTime.Now.ToString();//ToString("MM-DD-YYYY hh-mm-ss");
                //WORKING REPORT//string strRptPath = Server.MapPath("~/") + "Reports//" + "StudentClassReport.rpt";
                //WORKING REPORT//string strRptPath = Server.MapPath("~/") + "Reports//" + "BankChallanPrintReport.rpt";
                string strRptPath = Server.MapPath("~/") + "Reports//" + "ChallanForm.rpt";

                
                //crystalReport.Load(Server.MapPath("~/Reports/ReportStudentsInfo.rpt"));
                crystalReportDocument.Load(strRptPath);
                if (!IsPostBack)
                {
                    //DSStudentClassInfo dsStudentClass = new DSStudentClassInfo();
                    //dsStudentClass.Tables.Add(getStudentInfoByDataTable());
                   //Working// crystalReport.SetDataSource(getStudentReportClassWise());

                    crystalReportDocument.SetDataSource(getStudentInfoByDataTable());
                    //crystalReport.SetDataSource(dsStudentClass);
                    CrystalReportViewer1.Dispose();
                    CrystalReportViewer1.ReportSource = crystalReportDocument;
                }
                else
                {
                    //DSStudentClassInfo dsStudentClass = new DSStudentClassInfo();
                    //dsStudentClass.Tables.Add(getStudentInfoByDataTable());
                    //DATA BLL for StudentClassReport.rpt
                    //crystalReport.SetDataSource(getStudentReportClassWise());
                    crystalReportDocument.SetDataSource(getStudentFeeBankChallanReport());
                    CrystalReportViewer1.Dispose();
                    CrystalReportViewer1.ReportSource = crystalReportDocument;
                }
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }

        protected void CrystalReportViewer1_Unload(object sender, EventArgs e) 
        {
            crystalReportDocument.Close();
            crystalReportDocument.Dispose();
        }

        private DataSet CreateDataSet()
        {
            //creating a DataSet object for tables
            DataSet dataset = new DataSet();
           
            // creating the student table
            DataTable Students = new DataTable();//= getStudentReportClassWise();
            dataset.Tables.Add(Students);
            return dataset;
        }

        private List<ModelStudent> getStudentReportClassWise()
        {

            iDAS.BLL.BLLStudentFee objBLLStudentFee = new iDAS.BLL.BLLStudentFee();
            List<ModelStudent> lstModelStudent = new List<ModelStudent>();
           //Testing
            //string SearchCriteria = "Students.ClassId = 1 AND Students.SectionId = 1";
            string SearchCriteria = String.Empty;
            if (Session[DALVariables.SearchCriteria] != null)
            {
                SearchCriteria = Session[DALVariables.SearchCriteria].ToString();
            }


            lstModelStudent = objBLLStudentFee.GetStudentListReport(SearchCriteria, Convert.ToInt32(Session[iDAS.DAL.DALVariables.SchoolAccountId].ToString()));
            return lstModelStudent;
        }

        private List<ModelBankFeeChallanReport> getStudentFeeBankChallanReport()
        {

            iDAS.BLL.BLLStudentReports objBLLStudentReports = new iDAS.BLL.BLLStudentReports();
            List<ModelBankFeeChallanReport> lstModelBankFeeChallanReport = new List<ModelBankFeeChallanReport>();

            lstModelBankFeeChallanReport = objBLLStudentReports.GetStudentListReport();
            return lstModelBankFeeChallanReport;
        }

        private DataTable getStudentInfoByDataTable()
        {
            DataTable aTable = new DataTable();
            BLLStudentFee objBLLStudentFee = new BLLStudentFee();
            string SearchCriteria = String.Empty;
            if (Session[DALVariables.SearchCriteria] != null)
            {
                SearchCriteria = Session[DALVariables.SearchCriteria].ToString();
            }
            aTable = objBLLStudentFee.GetStudentDataTableForReport(SearchCriteria, Convert.ToInt32(Session[iDAS.DAL.DALVariables.SchoolAccountId].ToString()));

            return aTable;
        }
    }
}