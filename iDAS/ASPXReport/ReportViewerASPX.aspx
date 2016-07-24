<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta name="viewport" content="width=device-width" />
    <title>Report Viewer ASPX</title>
    <script type="text/javascript" runat="server">
        void Page_Load(object sender, EventArgs e)
        {
            
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CrystalDecisions.CrystalReports.Engine.ReportDocument crystalReport = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
                string strRptPath = Server.MapPath("~/") + "Reports//" + "ReportStudentsInfo.rpt";
                //crystalReport.Load(Server.MapPath("~/Reports/ReportStudentsInfo.rpt"));
                crystalReport.Load(strRptPath);

                iDAS.BLL.BLLStudent objBLLStudent = new iDAS.BLL.BLLStudent();
                List<iDAS.Models.ModelStudent> lstModelStudent = new List<iDAS.Models.ModelStudent>();

                string SearchCriteria = "Students.ClassId = 1 AND Students.SectionId = 1";

                lstModelStudent = objBLLStudent.GetStudentList(SearchCriteria, Convert.ToInt32(Session[iDAS.DAL.DALVariables.SchoolAccountId].ToString()));
                crystalReport.SetDataSource(lstModelStudent);
                CrystalReportViewer1.ReportSource = crystalReport;
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

        <div>
            <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true" />
        </div>
    </form>
</body>
</html>
