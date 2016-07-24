<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StudentFeeReport.aspx.cs" Inherits="iDAS.ASPXReport.StudentFeeReport" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

        <div>
            <CR:CrystalReportViewer ID="rptStudentFeeFollowUp" runat="server" AutoDataBind="true" EnableDatabaseLogonPrompt="False" EnableDrillDown="False" GroupTreeStyle-ShowLines="False" HasCrystalLogo="False" HasDrilldownTabs="False" HasDrillUpButton="False" HasToggleGroupTreeButton="False" Height="50px" PrintMode="ActiveX" ToolPanelView="None" Width="350px" />
        </div>
    </form>
</body>
</html>
