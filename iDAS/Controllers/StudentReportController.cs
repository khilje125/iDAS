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
using System.Text;

namespace iDAS.Controllers
{
    public class StudentReportController : BootstrapBaseController
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

        public ActionResult DataReports()
        {

            return View();
        }

        private void getdropdownData()
        {
            BLLStudent objBLLStudent = new BLLStudent();

            ViewBag.dpClass = objBLLStudent.GetClassDropdown(Convert.ToInt32(Session[DALVariables.SchoolAccountId]));
            ViewBag.dpSection = objBLLStudent.GetClassSectionDropdown(Convert.ToInt32(Session[DALVariables.SchoolAccountId]));
        }

        //
        // GET: /GetBankChallanVoucher/
        [HttpGet]
        public ActionResult GetBankChallanVoucher()
        {
            getdropdownData();
            return View();
        }
        //
        // POST: /GetBankChallanVoucher/
        [HttpPost]
        public ActionResult GetBankChallanVoucher(string txtVourcherDueDate, string dropDownClass, string dropDownSection)
        {
            getdropdownData();
            StringBuilder strSearchCriteria = new StringBuilder();
            string searchCriteria = "";

            //If Selected Class Is Not Empty
            if (string.IsNullOrEmpty(txtVourcherDueDate))
            {
                return Json(new { code = 1, css = "error", message = "Error occured ! Voucher Date not selected" });
            }

            //If Selected Class Is Empty
            if (!string.IsNullOrEmpty(dropDownClass.Trim()))
            {
                int _selectedClass = Convert.ToInt32(dropDownClass.Trim());
                if (_selectedClass <= 0)
                {
                    return Json(new { code = 1, css = "error", message = "Error occured ! Class not selected" });
                }
            }

            //If Selected Class Is Not Empty
            if ((!string.IsNullOrEmpty(dropDownClass.Trim())))
            {
                int _selectedClass = Convert.ToInt32(dropDownClass.Trim());
                if (_selectedClass > 0)
                {
                    strSearchCriteria.Append(" Students.ClassId = " + _selectedClass + " AND ");
                }
            }

            //If Class Section Is Not Empty
            if ((!string.IsNullOrEmpty(dropDownSection.Trim())))
            {
                int _selectedSection = Convert.ToInt32(dropDownSection.Trim());
                if (_selectedSection > 0)
                {
                    strSearchCriteria.Append(" Students.SectionId = " + _selectedSection + " AND ");
                }
            }

            //strSearchCriteria.Append(" ServiceTypeCategoryId = " + CategoryId + " AND ");

            //If Filter Expression Is Not Null Or Empty
            if ((!string.IsNullOrEmpty(strSearchCriteria.ToString())))
            {
                //Set Filter Expression
                searchCriteria = strSearchCriteria.ToString().Remove(strSearchCriteria.ToString().Length - 4, 4);

                //User Log
                DALUtility.InsertUserLog("Generate Bulk Fee Voucher. Search Criteria - " + searchCriteria, "Student/Search", Convert.ToInt32(Session[DALVariables.UserAccountId].ToString()));
            }
            else
            {
                //User Log
                DALUtility.InsertUserLog("Generate Bulk Fee Voucher. Search Criteria - Nothing", "Student/Search", Convert.ToInt32(Session[DALVariables.UserAccountId].ToString()));
            }

            try
            {
                string reportURL = "../../ASPXReport/StudentBankChallanReport.aspx?Date=" + HttpUtility.UrlEncode(EncryptDecrypt.Encrypt(txtVourcherDueDate)) + "&Search=" + HttpUtility.UrlEncode(EncryptDecrypt.Encrypt(searchCriteria));
                return Json(new { code = 3, css = "success", message = "Message ! Reridecting report URL", linkUrl = reportURL });
            }
            catch (Exception ex)
            {

                DALUtility.ErrorLog(ex.Message, "GenerateBulkFeeVouchers, StudentFee");
            }
           return View();
        }


        public ActionResult StudentClassReport()
        {
            BLLStudentReports objBLLStudentReports = new BLLStudentReports();
            ViewBag.dpReports = objBLLStudentReports.GetReportListDropdown(Convert.ToDouble(Session[DALVariables.SchoolAccountId]), 1);
            getdropdownData();
            return View();
        }
        [HttpPost]
        public ActionResult StudentClassReport(string dropDownClass, string dropDownSection, string dropDownReportTypeId, string dropDownCampusTypeId, string strReportTitle)
        {
            BLLStudentReports objBLLStudentReports = new BLLStudentReports();
            ViewBag.dpReports = objBLLStudentReports.GetReportListDropdown(Convert.ToDouble(Session[DALVariables.SchoolAccountId]), 1);
            getdropdownData();

            StringBuilder strSearchCriteria = new StringBuilder();
            string searchCriteria = "";

            //If Selected Class Is Empty
            if (!string.IsNullOrEmpty(dropDownCampusTypeId.Trim()))
            {
                int _selectedCampusTypeId = Convert.ToInt32(dropDownCampusTypeId.Trim());
                if (_selectedCampusTypeId <= 0)
                {
                    return Json(new { code = 1, css = "error", message = "Error occured ! Campus Type not selected" });
                }
            }

            //If Selected Class Is Empty
            if (!string.IsNullOrEmpty(dropDownClass.Trim()))
            {
                int _selectedClass = Convert.ToInt32(dropDownClass.Trim());
                if (_selectedClass <= 0)
                {
                    return Json(new { code = 1, css = "error", message = "Error occured ! Class not selected" });
                }
            }

            //If Report Type Is Empty
            if (!string.IsNullOrEmpty(dropDownReportTypeId.Trim()))
            {
                int _selectedClass = Convert.ToInt32(dropDownReportTypeId.Trim());
                if (_selectedClass <= 0)
                {
                    return Json(new { code = 1, css = "error", message = "Error occured ! Report not selected" });
                }
            }

            //If Selected Class Is Not Empty
            if ((!string.IsNullOrEmpty(dropDownClass.Trim())))
            {
                int _selectedClass = Convert.ToInt32(dropDownClass.Trim());
                if (_selectedClass > 0)
                {
                    strSearchCriteria.Append(" Students.ClassId = " + _selectedClass + " AND ");
                }
            }

            //If Class Section Is Not Empty
            if ((!string.IsNullOrEmpty(dropDownSection.Trim())))
            {
                int _selectedSection = Convert.ToInt32(dropDownSection.Trim());
                if (_selectedSection > 0)
                {
                    strSearchCriteria.Append(" Students.SectionId = " + _selectedSection + " AND ");
                }
            }

            //strSearchCriteria.Append(" ServiceTypeCategoryId = " + CategoryId + " AND ");

            //If Filter Expression Is Not Null Or Empty
            if ((!string.IsNullOrEmpty(strSearchCriteria.ToString())))
            {
                //Set Filter Expression
                searchCriteria = strSearchCriteria.ToString().Remove(strSearchCriteria.ToString().Length - 4, 4);

                //User Log
                DALUtility.InsertUserLog("Generate Bulk Fee Voucher. Search Criteria - " + searchCriteria, "Student/Search", Convert.ToInt32(Session[DALVariables.UserAccountId].ToString()));
            }
            else
            {
                //User Log
                DALUtility.InsertUserLog("Generate Bulk Fee Voucher. Search Criteria - Nothing", "Student/Search", Convert.ToInt32(Session[DALVariables.UserAccountId].ToString()));
            }

            try
            {
                string reportURL = "../../ASPXReport/StudentReport.aspx?Report=" + HttpUtility.UrlEncode(EncryptDecrypt.Encrypt(dropDownReportTypeId)) + "&CampId=" + HttpUtility.UrlEncode(EncryptDecrypt.Encrypt(dropDownCampusTypeId)) + "&Title=" + HttpUtility.UrlEncode(EncryptDecrypt.Encrypt(strReportTitle)) + "&Search=" + HttpUtility.UrlEncode(EncryptDecrypt.Encrypt(searchCriteria));
                return Json(new { code = 3, css = "success", message = "Message ! Reridecting report URL", linkUrl = reportURL });
            }
            catch (Exception ex)
            {

                DALUtility.ErrorLog(ex.Message, "GenerateBulkFeeVouchers, StudentFee");
            }

           
            return View();
        }

        public ActionResult StudentFeeFollowUpReport()
        {
            BLLStudentReports objBLLStudentReports = new BLLStudentReports();
            ViewBag.dpReports = objBLLStudentReports.GetReportListDropdown(Convert.ToDouble(Session[DALVariables.SchoolAccountId]), 1);
            getdropdownData();
            return View();
        }
        [HttpPost]
        public ActionResult StudentFeeFollowUpReport(string dropDownClass, string dropDownSection, string dropDownReportTypeId, string dropDownCampusTypeId, string strReportTitle)
        {
            BLLStudentReports objBLLStudentReports = new BLLStudentReports();
            ViewBag.dpReports = objBLLStudentReports.GetReportListDropdown(Convert.ToDouble(Session[DALVariables.SchoolAccountId]), 1);
            getdropdownData();

            StringBuilder strSearchCriteria = new StringBuilder();
            string searchCriteria = "";

            //If Selected Class Is Empty
            if (!string.IsNullOrEmpty(dropDownCampusTypeId.Trim()))
            {
                int _selectedCampusTypeId = Convert.ToInt32(dropDownCampusTypeId.Trim());
                if (_selectedCampusTypeId <= 0)
                {
                    return Json(new { code = 1, css = "error", message = "Error occured ! Campus Type not selected" });
                }
            }

            //If Selected Class Is Empty
            if (!string.IsNullOrEmpty(dropDownClass.Trim()))
            {
                int _selectedClass = Convert.ToInt32(dropDownClass.Trim());
                if (_selectedClass <= 0)
                {
                    return Json(new { code = 1, css = "error", message = "Error occured ! Class not selected" });
                }
            }

            //If Report Type Is Empty
            if (!string.IsNullOrEmpty(dropDownReportTypeId.Trim()))
            {
                int _selectedClass = Convert.ToInt32(dropDownReportTypeId.Trim());
                if (_selectedClass <= 0)
                {
                    return Json(new { code = 1, css = "error", message = "Error occured ! Report not selected" });
                }
            }

            //If Selected Class Is Not Empty
            if ((!string.IsNullOrEmpty(dropDownClass.Trim())))
            {
                int _selectedClass = Convert.ToInt32(dropDownClass.Trim());
                if (_selectedClass > 0)
                {
                    strSearchCriteria.Append(" Students.ClassId = " + _selectedClass + " AND ");
                }
            }

            //If Class Section Is Not Empty
            if ((!string.IsNullOrEmpty(dropDownSection.Trim())))
            {
                int _selectedSection = Convert.ToInt32(dropDownSection.Trim());
                if (_selectedSection > 0)
                {
                    strSearchCriteria.Append(" Students.SectionId = " + _selectedSection + " AND ");
                }
            }

            //strSearchCriteria.Append(" ServiceTypeCategoryId = " + CategoryId + " AND ");

            //If Filter Expression Is Not Null Or Empty
            if ((!string.IsNullOrEmpty(strSearchCriteria.ToString())))
            {
                //Set Filter Expression
                searchCriteria = strSearchCriteria.ToString().Remove(strSearchCriteria.ToString().Length - 4, 4);

                //User Log
                DALUtility.InsertUserLog("Generate Bulk Fee Voucher. Search Criteria - " + searchCriteria, "Student/Search", Convert.ToInt32(Session[DALVariables.UserAccountId].ToString()));
            }
            else
            {
                //User Log
                DALUtility.InsertUserLog("Generate Bulk Fee Voucher. Search Criteria - Nothing", "Student/Search", Convert.ToInt32(Session[DALVariables.UserAccountId].ToString()));
            }

            try
            {
                string reportURL = "../../ASPXReport/StudentFeeReport.aspx?Report=" + HttpUtility.UrlEncode(EncryptDecrypt.Encrypt(dropDownReportTypeId)) + "&CampId=" + HttpUtility.UrlEncode(EncryptDecrypt.Encrypt(dropDownCampusTypeId)) + "&Title=" + HttpUtility.UrlEncode(EncryptDecrypt.Encrypt(strReportTitle)) + "&Search=" + HttpUtility.UrlEncode(EncryptDecrypt.Encrypt(searchCriteria));
                return Json(new { code = 3, css = "success", message = "Message ! Reridecting report URL", linkUrl = reportURL });
            }
            catch (Exception ex)
            {

                DALUtility.ErrorLog(ex.Message, "GenerateBulkFeeVouchers, StudentFee");
            }


            return View();
        }
       
    }
}
