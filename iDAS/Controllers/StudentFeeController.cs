using iDAS.BLL;
using iDAS.DAL;
using iDAS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace iDAS.Controllers
{
    public class StudentFeeController : BootstrapBaseController
    {
        BLLStudentFee objBLLStudentFee = new BLLStudentFee();
                        
        // GET: /Search/
        [HttpGet]
        public ActionResult SearchStudentFee()
        {
            return View();
        }

        //
        // GET: /Search1/
        [HttpGet]
        public ActionResult SearchFeeReordPartial()
        {
            BLLStudent objBLLStudent = new BLLStudent();

            ViewBag.dpClass = objBLLStudent.GetClassDropdown(Convert.ToInt32(Session[DALVariables.SchoolAccountId]));
            ViewBag.dpSection = objBLLStudent.GetClassSectionDropdown(Convert.ToInt32(Session[DALVariables.SchoolAccountId]));

            return PartialView(customview("_SearchFormPartial", "StudentFee"));
        }

        [HttpPost]
        public ActionResult SearchFeeReordPartial(string ComputerCode, string RegNo, string StudentName, string FatherName, string StudentStatus, string dropDownClass, string dropDownSection)
        {
            StringBuilder strSearchCriteria = new StringBuilder();
            string searchCriteria = "";
            //If Computer Code Is Not Empty
            if ((!string.IsNullOrEmpty(RegNo.Trim())))
            {
                strSearchCriteria.Append(" Students.RegNo =" + RegNo.Trim() + " AND ");
            }

            //If Computer Code Is Not Empty
            if ((!string.IsNullOrEmpty(ComputerCode.Trim())))
            {
                strSearchCriteria.Append(" Students.ComputerCode =" + ComputerCode.Trim() + " AND ");
            }

            //If Student Name Is Not Empty
            if ((!string.IsNullOrEmpty(StudentName.Trim())))
            {
                strSearchCriteria.Append(" (Students.StudentName LIKE '%" + StudentName.Trim() + "%') AND ");
            }

            //If Father Name Is Not Empty
            if ((!string.IsNullOrEmpty(FatherName.Trim())))
            {
                strSearchCriteria.Append(" (Students.FatherName LIKE '%" + FatherName.Trim() + "%') AND ");
            }

            //If Student Statuse Is Not Empty
            if ((!string.IsNullOrEmpty(StudentStatus.Trim())))
            {
                int _studentStaus = Convert.ToInt32(StudentStatus.Trim());
                if (_studentStaus > 0)
                {
                    strSearchCriteria.Append(" Students.Status = " + _studentStaus + " AND ");
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
                DALUtility.InsertUserLog("Search Manage Short Code. Search Criteria - " + searchCriteria, "Student/Search", Convert.ToInt32(Session[DALVariables.UserAccountId].ToString()));
            }
            else
            {
                //User Log
                DALUtility.InsertUserLog("Search Manage Short Code. Search Criteria - Nothing", "Student/Search", Convert.ToInt32(Session[DALVariables.UserAccountId].ToString()));
            }

            try
            {
                List<ModelStudent> lstModelStudent = new List<ModelStudent>();

                BLLStudent objBLLStudent = new BLLStudent();
                Session[DALVariables.SearchCriteria] = searchCriteria;
                lstModelStudent = objBLLStudent.GetStudentList(searchCriteria, Convert.ToInt32(Session[DALVariables.SchoolAccountId].ToString()));
                return PartialView(customview("_SearchResultsPartial", "StudentFee"), lstModelStudent);
            }
            catch (Exception ex)
            {

                DALUtility.ErrorLog(ex.Message, "StudentController, Search1");
            }
            return PartialView("Error");
        }

        //
        // POST: /GetStudentFeeRecord/
        [HttpPost]
        public ActionResult GetStudentFeeRecord(string studentID)
        {
            ModelStudent objModelStudent = new ModelStudent();

            if (!String.IsNullOrEmpty(studentID.Trim()))
            {
                try
                {
                    BLLStudentFee objBLLStudentFee = new BLLStudentFee();

                    objModelStudent = objBLLStudentFee.GetStudentFeeInfoById(Convert.ToDecimal(studentID));
                }
                catch (Exception ex)
                {
                    DALUtility.ErrorLog(ex.Message, "StudentFeeController, GetStudentSingleFeeRecord");
                }
            }
            return PartialView(customview("_GetFormStudentFeeInfo", "StudentFee"), objModelStudent);
        }

        //
        // POST: /GetStudentSingleFeeRecord/
        [HttpPost]
        public ActionResult GetStudentSingleFeeRecord(string feeID)
        {
            ModelStudentFee objModelStudentFee = new ModelStudentFee();

            if (!String.IsNullOrEmpty(feeID.Trim()))
            {
                try
                {
                    BLLStudentFee objBLLStudentFee = new BLLStudentFee();

                    objModelStudentFee = objBLLStudentFee.GetStudentSinngleFeeInfoByFeeId(Convert.ToDecimal(feeID));
                }
                catch (Exception ex)
                {
                    DALUtility.ErrorLog(ex.Message, "StudentFeeController, GetStudentSingleFeeRecord");
                }
            }
            return PartialView(customview("_EditStudentMonthlyFeeInfo", "StudentFee"), objModelStudentFee);
        }

        //
        // POST: /EditStudent/
        [HttpPost]
        public ActionResult UpdateStudentMonthlyFeeRecord(ModelStudentFee objModelStudentFee)
        {
            if (ModelState.IsValid)
            {
                if (objModelStudentFee != null)
                {
                    try
                    {
                        objBLLStudentFee = new BLLStudentFee();
                        int _updateResult = 0;
                        _updateResult = objBLLStudentFee.UpdateStudentSinngleFeeInfoByFeeId(objModelStudentFee);

                        if (_updateResult > 0)
                        {
                            ModelStudent objModelStudent = new ModelStudent();

                            objBLLStudentFee = new BLLStudentFee();
                            objModelStudent = objBLLStudentFee.GetStudentFeeInfoById(Convert.ToDecimal(objModelStudentFee.StudentId));
                            Success("Fee Record Successfully Updated");
                            return PartialView(customview("_GetFormStudentFeeInfo", "StudentFee"), objModelStudent);
                        }
                        else
                        {
                            Error("Error occured while updating your fee record");
                            return PartialView(customview("_EditStudentMonthlyFeeInfo", "StudentFee"), objModelStudentFee); 
                        }
                    }
                    catch (Exception ex)
                    {
                        DALUtility.ErrorLog(ex.Message, "StudentFeeController, UpdateStudentMonthlyFeeRecord");
                    }
                }
            }
            Error("Record not Updated");
            return PartialView(customview("_EditStudentMonthlyFeeInfo", "StudentFee"), objModelStudentFee);
        }

        //
        // Custom View Controller
        public string customview(string view, string controller)
        {
            if (string.IsNullOrEmpty(controller))
                controller = Request.RequestContext.RouteData.Values["Controller"].ToString();
            return String.Format("~/Views/Shared/{0}/{1}.cshtml", controller, view);
        }
    }
}
