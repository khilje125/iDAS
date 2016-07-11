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

namespace iDAS.ASPXReport
{
    public partial class StudentListClassWise : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            try
            {
                CrystalDecisions.CrystalReports.Engine.ReportDocument crystalReport = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
                string strRptPath = Server.MapPath("~/") + "Reports//" + "StudentClassReport.rpt";
                //crystalReport.Load(Server.MapPath("~/Reports/ReportStudentsInfo.rpt"));
                crystalReport.Load(strRptPath);
                if (!IsPostBack)
                {
                    //DSStudentClassInfo dsStudentClass = new DSStudentClassInfo();
                    //dsStudentClass.Tables.Add(getStudentInfoByDataTable());
                    crystalReport.SetDataSource(getStudentReportClassWise());
                    //crystalReport.SetDataSource(dsStudentClass);
                    CrystalReportViewer1.ReportSource = crystalReport;
                }
                else
                {
                    //DSStudentClassInfo dsStudentClass = new DSStudentClassInfo();
                    //dsStudentClass.Tables.Add(getStudentInfoByDataTable());
                    crystalReport.SetDataSource(getStudentReportClassWise());
                    CrystalReportViewer1.Dispose();
                    CrystalReportViewer1.ReportSource = crystalReport;
                }
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
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