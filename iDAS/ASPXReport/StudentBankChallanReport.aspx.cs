using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iDAS.BLL;
using iDAS.DAL;
using iDAS.Models;
using CrystalDecisions.CrystalReports.Engine;

namespace iDAS.ASPXReport
{
    public partial class StudentBankChallanReport : System.Web.UI.Page
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

                DALUtility.ErrorLog(ex.Message, "StudentBankChallanReport.aspx.cs, Page_Load");
            }
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            try
            {
                crystalReportDocument = new ReportDocument();

                string strRptPath = Server.MapPath("~/") + "Reports//" + "BankChallanForm.rpt";
                crystalReportDocument.Load(strRptPath);
                List<ModelStudentFee> lstModelStudentFee = new List<ModelStudentFee>();
                if (!IsPostBack)
                {
                    crystalReportDocument.SetDataSource(getStudentBankChallanStudentInfo());
                    lstModelStudentFee = getStudentBankChallanFeeList();
                    crystalReportDocument.Subreports[0].SetDataSource(lstModelStudentFee);
                    crystalReportDocument.Subreports[1].SetDataSource(lstModelStudentFee);
                    crystalReportDocument.Subreports[2].SetDataSource(lstModelStudentFee);
                    //crystalReport.SetDataSource(dsStudentClass);
                    rptStudentBankChallan.Dispose();
                    rptStudentBankChallan.ReportSource = crystalReportDocument;
                }
                else
                {
                    crystalReportDocument.SetDataSource(getStudentBankChallanStudentInfo());
                    lstModelStudentFee = getStudentBankChallanFeeList();
                    crystalReportDocument.Subreports[0].SetDataSource(lstModelStudentFee);
                    crystalReportDocument.Subreports[1].SetDataSource(lstModelStudentFee);
                    crystalReportDocument.Subreports[2].SetDataSource(lstModelStudentFee);
                    //getStudentBankChallanFeeList()
                    rptStudentBankChallan.Dispose();
                    rptStudentBankChallan.ReportSource = crystalReportDocument;
                }
            }
            catch (Exception ex)
            {

                DALUtility.ErrorLog(ex.Message, "StudentBankChallanReport.aspx.cs, Page_PreInit");
            }
        }

        public string DuaDateParam
        {
	        get {
		        if ((Request.QueryString["Date"] != null)) {
                    return Convert.ToString(EncryptDecrypt.Decrypt(Request.QueryString["Date"]));
		        }

		        return String.Empty;
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

        private List<ModelStudentBankChallan> getStudentBankChallanStudentInfo()
        {

            BLLStudentFee objBLLStudentFee = new BLLStudentFee();
            List<ModelStudentBankChallan> lstModelStudentBankChallan = new List<ModelStudentBankChallan>();
            //Testing
            //string SearchCriteria = "Students.ClassId = 1 AND Students.SectionId = 1";

            lstModelStudentBankChallan = objBLLStudentFee.GetUnPaidFeeStudentListWithVoucher(SearchCriteriaParam, DuaDateParam, Convert.ToInt32(Session[iDAS.DAL.DALVariables.SchoolAccountId].ToString()));
            return lstModelStudentBankChallan;
        }

        private List<ModelStudentFee> getStudentBankChallanFeeList()
        {

            BLLStudentFee objBLLStudentFee = new BLLStudentFee();
            List<ModelStudentFee> lstModelStudentFee = new List<ModelStudentFee>();
            //Testing
            //string SearchCriteria = "Students.ClassId = 1 AND Students.SectionId = 1";

            lstModelStudentFee = objBLLStudentFee.GetUnPaidFeeMonthsByDuaDateVoucher(DuaDateParam, Convert.ToDouble(Session[iDAS.DAL.DALVariables.SchoolAccountId].ToString()));
            return lstModelStudentFee;
        }
    }
}