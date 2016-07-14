using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CrystalDecisions.CrystalReports.Design;
using iDAS.BLL;
using CrystalDecisions.CrystalReports.Engine;
using System.IO;
using iDAS.Models;
using iDAS.DAL;

namespace iDAS.Controllers
{
    public class StudentReportController : Controller
    {
        BLLStudent objBLLStudent = new BLLStudent();
        List<ModelStudent> lstModelStudent = new List<ModelStudent>();
        //
        // GET: /StudentReport/
        //Test Report PDF Download
        public ActionResult PrintStudentList()
        {
            objBLLStudent = new BLLStudent();
            lstModelStudent = new List<ModelStudent>();
            string SearchCriteria = "Students.ClassId = 1 AND Students.SectionId = 1";
            lstModelStudent = objBLLStudent.GetStudentList(SearchCriteria, Convert.ToInt32(Session[DALVariables.SchoolAccountId].ToString()));
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/Reports/ReportStudentsInfo.rpt")));
            rd.SetDataSource(lstModelStudent);
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            Stream objStream = rd.ExportToStream(
                CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            objStream.Seek(0, SeekOrigin.Begin);
            return File(objStream, "application/pdf", "StudentList-" + DateTime.Now.ToString() + ".pdf");
        }


        //
        // GET: /StudentFeeReport/
        [HttpGet]
        public void StudentFeeReport(string reportID)
        {
            Response.Redirect("~/ASPXReport/StudentListClassWise.aspx?rptID=" + HttpUtility.UrlEncode(reportID));
        }
    }
}
