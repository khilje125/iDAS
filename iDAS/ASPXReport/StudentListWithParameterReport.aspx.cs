using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using iDAS.BLL;
using iDAS.DAL;
using iDAS.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace iDAS.ASPXReport
{
    public partial class StudentListWithParameterReport : System.Web.UI.Page
    {
        ReportDocument crystalReportDocument = new ReportDocument();

        public bool paramStudentId
        {
            get
            {
                if ((Request.QueryString["StdId"] != null))
                {
                    if (Convert.ToString(Request.QueryString["StdId"]) == "1")
                    {
                        return true;
                    }
                    //return Convert.ToString(EncryptDecrypt.Decrypt(Request.QueryString["Date"]));
                }

                return false;
            }
        }

        public bool paramComputerCode
        {
            get
            {
                if ((Request.QueryString["CompId"] != null))
                {
                    if (Convert.ToString(Request.QueryString["CompId"]) == "1")
                    {
                        return true;
                    }
                    //return Convert.ToString(EncryptDecrypt.Decrypt(Request.QueryString["Date"]));
                }

                return false;
            }
        }

        public bool paramDateOfBirth
        {
            get
            {
                if ((Request.QueryString["DOB"] != null))
                {
                    if (Convert.ToString(Request.QueryString["DOB"]) == "1")
                    {
                        return true;
                    }
                    //return Convert.ToString(EncryptDecrypt.Decrypt(Request.QueryString["Date"]));
                }

                return false;
            }

        }
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    if (Session[DALVariables.SchoolAccountId] == null)
                    {
                        Response.Redirect("../User/Login");
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

                string strRptPath = Server.MapPath("~/") + "Reports//" + "StudentListWithParameter.rpt";
                crystalReportDocument.Load(strRptPath);
                ParameterFields paramFields = new ParameterFields();
                ParameterField paramField;
                ParameterDiscreteValue paramDiscreteValue;
                List<ModelStudentDynamicReport> lstModelStudentDynamicReport = new List<ModelStudentDynamicReport>();
                bool isStudentIdColumn = paramStudentId;
                bool isComputerCodeColumn = paramComputerCode;
                bool isStudentNameColumn = true;
                bool isClassDateOfBirthColumn = paramDateOfBirth;
                if (isStudentIdColumn)
                {
                    paramField = new ParameterField();
                    paramField.Name = "StudentID";
                    paramDiscreteValue = new ParameterDiscreteValue();
                    paramDiscreteValue.Value = "StudentID";
                    paramField.CurrentValues.Add(paramDiscreteValue);
                    //Add the paramField to paramFields
                    paramFields.Add(paramField);
                }
                if (isStudentNameColumn)
                {
                    paramField = new ParameterField();
                    paramField.Name = "StudentName";
                    paramDiscreteValue = new ParameterDiscreteValue();
                    paramDiscreteValue.Value = "StudentName";
                    paramField.CurrentValues.Add(paramDiscreteValue);
                    //Add the paramField to paramFields
                    paramFields.Add(paramField);
                }
                if (isComputerCodeColumn)
                {
                    paramField = new ParameterField();
                    paramField.Name = "ComputerCode";
                    paramDiscreteValue = new ParameterDiscreteValue();
                    paramDiscreteValue.Value = "ComputerCode";
                    paramField.CurrentValues.Add(paramDiscreteValue);
                    //Add the paramField to paramFields
                    paramFields.Add(paramField);
                }
                if (isClassDateOfBirthColumn)
                {
                    paramField = new ParameterField();
                    paramField.Name = "DateOfBirth";
                    paramDiscreteValue = new ParameterDiscreteValue();
                    paramDiscreteValue.Value = "DateOfBirth";
                    paramField.CurrentValues.Add(paramDiscreteValue);
                    //Add the paramField to paramFields
                    paramFields.Add(paramField);
                }

                if (!IsPostBack)
                {
                    //lstModelStudentDynamicReport = getStudentList();
                    rptStudentList.ParameterFieldInfo = paramFields;
                    //DataSet Ds = new DataSet();
                    //Ds.Tables.Add(getStudentListTable(isStudentIdColumn, isComputerCodeColumn, isStudentNameColumn, isClassDateOfBirthColumn));
                    crystalReportDocument.SetDataSource(getStudentListTable(isStudentIdColumn, isComputerCodeColumn, isStudentNameColumn, isClassDateOfBirthColumn));
                    rptStudentList.Dispose();
                    rptStudentList.ReportSource = crystalReportDocument;
                }
                else
                {
                    //lstModelStudentDynamicReport = getStudentList();
                    rptStudentList.ParameterFieldInfo = paramFields;
                    //DataSet Ds = new DataSet();
                    //Ds.Tables.Add(getStudentListTable(isStudentIdColumn, isComputerCodeColumn, isStudentNameColumn, isClassDateOfBirthColumn));
                    crystalReportDocument.SetDataSource(getStudentListTable(isStudentIdColumn, isComputerCodeColumn, isStudentNameColumn, isClassDateOfBirthColumn));
                    rptStudentList.Dispose();
                    rptStudentList.ReportSource = crystalReportDocument;
                }
            }
            catch (Exception ex)
            {

                DALUtility.ErrorLog(ex.Message, "StudentBankChallanReport.aspx.cs, Page_PreInit");
            }
        }

        private List<ModelStudentDynamicReport> getStudentList()
        {
            List<ModelStudentDynamicReport> lstModelStudentDynamicReport = new List<ModelStudentDynamicReport>();
            BLLStudentReports objBLLStudentReports = new BLLStudentReports();
            lstModelStudentDynamicReport = objBLLStudentReports.GetStudentListForDynamicParameter(true, true, true, true);
            return lstModelStudentDynamicReport;
        }

        private DataTable getStudentListTable(bool isStdId, bool isCompCode, bool isStudentName, bool isDOB)
        {
            DataTable atable = new DataTable();
            BLLStudentReports objBLLStudentReports = new BLLStudentReports();
            atable = objBLLStudentReports.GetStudentListForDynamicParameterTable(isStdId, isCompCode, isStudentName, isDOB);
            return atable;
           
        }
    }
}